/************************************************************************************

 * Copyright (c) 2022 All Rights Reserved.

 * CLR版本： 4.0.30319.42000

 *机器名称：LAPTOP-CC5K5UTK

 *命名空间：ShopWebGisRedis.UserCache

 *文件名：  UserRedisCache

 *版本号：  V1.0.0.0

 *当前的用户域：LAPTOP-CC5K5UTK

 *创建人：  蔡显麒

 *创建时间：2022/6/22 11:48:03

 *描述：Redis-User

/************************************************************************************/

using Microsoft.Extensions.Caching.Distributed;
using ShopWebCaching;
using ShopWebGisCache;
using ShopWebGisCache.UserCache;
using ShopWebGisDomainShare.Common;
using ShopWebGisDomainShare.Const;
using ShopWebGisDomainShare.Const.CacheItem;
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
        private readonly IShopWebGisCache _iShopWebGisRedis;
        private readonly ICommonDistributedCache _distributedCache;
        public UserRedisCache(IShopWebGisCache iShopWebGisRedis, ICommonDistributedCache distributedCache)
        {
            _iShopWebGisRedis = iShopWebGisRedis;
            _distributedCache = distributedCache;
        }
        public async Task LimitLoginTimes(int userId)
        {

            int times = 0;
            var limitKey = UserCacheKeys.LimitLoginPrefix + userId;
            var userCacheItem = new UserLoginTimesCacheItem(userId);
            // 如果缓存不存在，则新增缓存，错误次数为1
            userCacheItem = await _distributedCache.GetAsync<UserLoginTimesCacheItem, UserLoginTimesCacheItem>(userCacheItem, Cache.UserCacheName);
            if (userCacheItem == null)
            {
                userCacheItem = new UserLoginTimesCacheItem();
                userCacheItem.Id = userId;
                userCacheItem.Times = 1;
                await _distributedCache.SetAsync(userCacheItem, Cache.UserCacheName, userCacheItem);
            }
            else
            {
                times = userCacheItem.Times;
                // 超过错误次数，锁定用户名,添加冻结用户缓存,否则登录失败次数加1
                if (times >= SystemConst.LimitLoginTimes)
                {
                    var freezeKey = UserCacheKeys.LimitLoginFreezePrefix + userId;
                    UserLoginFreezeCacheItem userLoginFreezeCacheItem = new UserLoginFreezeCacheItem();
                    userLoginFreezeCacheItem.Id = userId;
                    userLoginFreezeCacheItem.FreezeExpire = DateTime.Now;
                    await _distributedCache.SetAsync(userLoginFreezeCacheItem, Cache.UserCacheName, userLoginFreezeCacheItem);
                    throw new ShopWebGisCustomException($"{SystemConst.UserHaveBeenLock}请等候{SystemConst.LoginFreezeTimeSpan}分钟进行重试。");
                }
                else
                {
                    userCacheItem.Times = times + 1;
                    await _distributedCache.SetAsync(userCacheItem, Cache.UserCacheName, userCacheItem);
                }
            }
            throw new ShopWebGisCustomException($"{SystemConst.LoginFailed},用户名密码不正确,剩余重试次数{ SystemConst.LimitLoginTimes - times - 1  }次");
        }

        public async Task<bool> UserIsFreeze(int userId)
        {
            var userFreezeItem = new UserLoginFreezeCacheItem(userId);
            userFreezeItem = await _distributedCache.GetAsync<UserLoginFreezeCacheItem, UserLoginFreezeCacheItem>(userFreezeItem, Cache.UserCacheName);
            if (userFreezeItem != null)
            {

                var expire = userFreezeItem.FreezeExpire;
                var dateDiff = TimeHelper.DateDiff(DateTime.Now, expire);
                if (!(dateDiff.minutes > SystemConst.LoginFreezeTimeSpan))
                {
                    throw new ShopWebGisCustomException($"{SystemConst.UserHaveBeenLock}请等候{SystemConst.LoginFreezeTimeSpan - dateDiff.minutes}分钟进行重试。");
                }

                else
                {
                    await _distributedCache.RemoveAsync(userFreezeItem, Cache.UserCacheName);
                }

            }
            //清除限制用户Hash key
            var userCacheItem = new UserLoginTimesCacheItem(userId);
            await _distributedCache.RemoveAsync(userCacheItem, Cache.UserCacheName);
            return true;
        }
    }
}
