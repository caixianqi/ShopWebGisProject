/************************************************************************************

 * Copyright (c) 2022 All Rights Reserved.

 * CLR版本： 4.0.30319.42000

 *机器名称：LAPTOP-CC5K5UTK

 *命名空间：ShopWebGisCache

 *文件名：  IShopWebGisCache

 *版本号：  V1.0.0.0

 *当前的用户域：LAPTOP-CC5K5UTK

 *创建人： 蔡显麒

 *创建时间：2022/7/14 9:27:47

 *描述：Cache操作接口

/************************************************************************************/
using Microsoft.Extensions.Caching.Distributed;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ShopWebGisCache
{
    public interface IShopWebGisCache
    {
        /// <summary>
        /// 获取String缓存
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<RedisValue> StringGetAsync(RedisKey key);

        /// <summary>
        /// 获取Key和Expire
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<RedisValueWithExpiry> StringGetWithExpiryAsync(RedisKey key);

        /// <summary>
        /// 设置String缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expiry"></param>
        /// <returns></returns>
        Task<bool> StringSetAsync(RedisKey key, RedisValue value, TimeSpan? expiry = null);

        /// <summary>
        /// 判断Key是否存在
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<bool> KeyExistsAsync(RedisKey key);

        /// <summary>
        /// 将 key 中储存的数字值增一
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        Task<long> StringIncrementAsync(RedisKey redisKey, long num);

        /// <summary>
        /// 删除key
        /// </summary>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        Task<bool> KeyDelete(RedisKey redisKey);

        /// <summary>
        /// 设置Hash缓存(数组)
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="hashFields"></param>
        /// <returns></returns>
        Task HashSetAsync(RedisKey redisKey, HashEntry[] hashFields);

        /// <summary>
        /// 设置Hash缓存
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="hashField"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        Task HashSetAsync(RedisKey redisKey, RedisValue hashField, RedisValue value);

        /// <summary>
        /// 获取Hash缓存
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="hashField"></param>
        /// <returns></returns>
        Task<RedisValue> HashGetAsync(RedisKey redisKey, RedisValue hashField);

        /// <summary>
        /// 删除Hash缓存 Key
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="hashField"></param>
        /// <returns></returns>
        Task<bool> HashDeleteAsync(RedisKey redisKey, RedisValue hashField);

        ///// <summary>
        ///// 获取缓存值
        ///// </summary>
        ///// <typeparam name="TCacheItem"></typeparam>
        ///// <typeparam name="TCacheKey"></typeparam>
        ///// <param name="key"></param>
        ///// <param name="cacheName"></param>
        ///// <returns></returns>
        //TCacheItem Get<TCacheItem, TCacheKey>(TCacheKey key, string cacheName) where TCacheItem : class;

        ///// <summary>
        ///// 异步获取缓存值
        ///// </summary>
        ///// <typeparam name="TCacheItem"></typeparam>
        ///// <typeparam name="TCacheKey"></typeparam>
        ///// <param name="key"></param>
        ///// <param name="cacheName"></param>
        ///// <param name="token"></param>
        ///// <returns></returns>
        //TCacheItem GetAsync<TCacheItem, TCacheKey>(TCacheKey key, string cacheName, CancellationToken token = default) where TCacheItem : class;

        ///// <summary>
        ///// 获取添加缓存值
        ///// </summary>
        ///// <typeparam name="TCacheItem"></typeparam>
        ///// <typeparam name="TCacheKey"></typeparam>
        ///// <param name="key"></param>
        ///// <param name="factory"></param>
        ///// <param name="cacheName"></param>
        ///// <param name="optionsFactory"></param>
        ///// <returns></returns>
        //TCacheItem GetOrAdd<TCacheItem, TCacheKey>(TCacheKey key, Func<TCacheItem> factory, string cacheName, Func<DistributedCacheEntryOptions> optionsFactory = null);

        ///// <summary>
        ///// 异步获取添加缓存值
        ///// </summary>
        ///// <typeparam name="TCacheItem"></typeparam>
        ///// <typeparam name="TCacheKey"></typeparam>
        ///// <param name="key"></param>
        ///// <param name="factory"></param>
        ///// <param name="cacheName"></param>
        ///// <param name="optionsFactory"></param>
        ///// <returns></returns>
        //TCacheItem GetOrAddAsync<TCacheItem, TCacheKey>(TCacheKey key, Func<TCacheItem> factory, string cacheName, Func<DistributedCacheEntryOptions> optionsFactory = null, CancellationToken token = default);
    }
}
