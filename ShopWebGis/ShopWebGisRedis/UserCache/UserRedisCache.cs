/************************************************************************************

 * Copyright (c) 2022 All Rights Reserved.

 * CLR版本： 4.0.30319.42000

 *机器名称：LAPTOP-CC5K5UTK

 *命名空间：ShopWebGisRedis.UserCache

 *文件名：  UserRedisCache

 *版本号：  V1.0.0.0

 *当前的用户域：LAPTOP-CC5K5UTK

 *创建人：  智慧环保部-蔡显麒

 *创建时间：2022/6/22 11:48:03

 *描述：Redis-User

/************************************************************************************/

using Microsoft.Extensions.Caching.Distributed;
using ShopWebGisCache.UserCache;
using ShopWebGisDomainShare.Const;
using ShopWebGisDomainShare.Const.CacheKey;
using ShopWebGisDomainShare.CustomException;
using ShopWebGisRedis.config;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShopWebGisRedis.UserCache
{
    public class UserRedisCache : IUserCache
    {
        private readonly IShopWebGisRedisDataBase _iShopWebGisRedis;
        public UserRedisCache(IShopWebGisRedisDataBase iShopWebGisRedis)
        {
            _iShopWebGisRedis = iShopWebGisRedis;
        }
        public async Task LimitLoginTimes(string userName)
        {
            var userNamekey = UserCacheKeys.LimitLoginPrefix + userName;
            int times = 0;

            if (!await _iShopWebGisRedis.KeyExistsAsync(userNamekey))
            {

                await _iShopWebGisRedis.StringSetAsync(userNamekey, 1, TimeSpan.FromSeconds(SystemConst.LimitLoginPeriod));
            }
            else
            {
                times = int.Parse(await _iShopWebGisRedis.StringGetAsync(userNamekey));
                // 超过错误次数，锁定用户名,否则登录失败次数加1
                if (times >= SystemConst.LimitLoginTimes)
                {
                    await _iShopWebGisRedis.StringSetAsync($"{UserCacheKeys.LimitLoginFreezePrefix + userName}", true, TimeSpan.FromMinutes(SystemConst.LoginFreezeTimeSpan));
                    throw new ShopWebGisCustomException($"{SystemConst.UserHaveBeenLock}请等候{SystemConst.LoginFreezeTimeSpan}分钟进行重试。");
                }
                else
                {
                    await _iShopWebGisRedis.StringIncrementAsync(userNamekey, 1);

                }
            }
            throw new ShopWebGisCustomException($"{SystemConst.LoginFailed},用户名密码不正确,剩余重试次数{ SystemConst.LimitLoginTimes - times  }次");
        }

        public async Task<bool> UserIsFreeze(string userName)
        {
            var freezeRedisValue = await _iShopWebGisRedis.StringGetWithExpiryAsync($"{UserCacheKeys.LimitLoginFreezePrefix}userName");
            if (freezeRedisValue.Value.HasValue)
            {
                double minutes = 0;
                if (freezeRedisValue.Expiry != null)
                {
                    minutes = freezeRedisValue.Expiry.Value.TotalMinutes;
                }
                throw new ShopWebGisCustomException($"{SystemConst.UserHaveBeenLock}请等候{minutes}分钟进行重试。");
            }
            else
            {
                //清除限制用户Key
                await _iShopWebGisRedis.KeyDelete(UserCacheKeys.LimitLoginPrefix + userName);

            }
            return true;
        }
    }
}
