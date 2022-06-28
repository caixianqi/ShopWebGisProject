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
using ShopWebGisDomainShare.Common;
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

            int times = 0;
            // 如果缓存不存在，则新增缓存，否则在缓存上加1
            if ((await _iShopWebGisRedis.HashGetAsync(UserCacheKeys.LimitLoginPrefix, userName)).HasValue == false)
            {

                await _iShopWebGisRedis.HashSetAsync(UserCacheKeys.LimitLoginPrefix, userName, 1);
            }
            else
            {
                times = int.Parse(await _iShopWebGisRedis.HashGetAsync(UserCacheKeys.LimitLoginPrefix, userName));
                // 超过错误次数，锁定用户名,否则登录失败次数加1
                if (times >= SystemConst.LimitLoginTimes)
                {
                    await _iShopWebGisRedis.HashSetAsync(UserCacheKeys.LimitLoginFreezePrefix, userName, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    throw new ShopWebGisCustomException($"{SystemConst.UserHaveBeenLock}请等候{SystemConst.LoginFreezeTimeSpan}分钟进行重试。");
                }
                else
                {
                    await _iShopWebGisRedis.HashSetAsync(UserCacheKeys.LimitLoginPrefix, userName, times + 1);

                }
            }
            throw new ShopWebGisCustomException($"{SystemConst.LoginFailed},用户名密码不正确,剩余重试次数{ SystemConst.LimitLoginTimes - times - 1  }次");
        }

        public async Task<bool> UserIsFreeze(string userName)
        {
            var freezeRedisValue = await _iShopWebGisRedis.HashGetAsync(UserCacheKeys.LimitLoginFreezePrefix, userName);
            if (freezeRedisValue.HasValue)
            {

                var dateValue = Convert.ToDateTime(freezeRedisValue);
                var dateDiff = TimeHelper.DateDiff(DateTime.Now, dateValue);
                if (!(dateDiff.minutes > SystemConst.LoginFreezeTimeSpan))
                {
                    throw new ShopWebGisCustomException($"{SystemConst.UserHaveBeenLock}请等候{SystemConst.LoginFreezeTimeSpan - dateDiff.minutes}分钟进行重试。");
                }
                else
                {
                    await _iShopWebGisRedis.HashDeleteAsync(UserCacheKeys.LimitLoginFreezePrefix, userName);
                }

            }
            //清除限制用户Hash key
            await _iShopWebGisRedis.HashDeleteAsync(UserCacheKeys.LimitLoginPrefix, userName);
            return true;
        }
    }
}
