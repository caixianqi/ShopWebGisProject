/************************************************************************************

 * Copyright (c) 2023 All Rights Reserved.

 * CLR版本： 4.0.30319.42000

 *机器名称：LAPTOP-CC5K5UTK

 *命名空间：ShopWebCaching

 *文件名：  IDistributedCache

 *版本号：  V1.0.0.0

 *当前的用户域：LAPTOP-CC5K5UTK

 *创建人： 蔡显麒

 *创建时间：2023/4/7 20:49:29

 *描述：缓存封装

/************************************************************************************/
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ShopWebCaching
{
    public interface ICommonDistributedCache
    {
        /// <summary>
        /// 获取缓存值
        /// </summary>
        /// <typeparam name="TCacheItem"></typeparam>
        /// <typeparam name="TCacheKey"></typeparam>
        /// <param name="key"></param>
        /// <param name="cacheName"></param>
        /// <returns></returns>
        TCacheItem Get<TCacheKey, TCacheItem>(TCacheKey key, string cacheName) where TCacheItem : class;

        /// <summary>
        /// 异步获取缓存值
        /// </summary>
        /// <typeparam name="TCacheItem"></typeparam>
        /// <typeparam name="TCacheKey"></typeparam>
        /// <param name="key"></param>
        /// <param name="cacheName"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<TCacheItem> GetAsync<TCacheKey, TCacheItem>(TCacheKey key,
            string cacheName,
            CancellationToken token = default
            ) where TCacheItem : class;

        /// <summary>
        /// 获取添加缓存值
        /// </summary>
        /// <typeparam name="TCacheItem"></typeparam>
        /// <typeparam name="TCacheKey"></typeparam>
        /// <param name="key"></param>
        /// <param name="factory"></param>
        /// <param name="cacheName"></param>
        /// <param name="optionsFactory"></param>
        /// <returns></returns>
        TCacheItem GetOrAdd<TCacheKey, TCacheItem>(TCacheKey key,
            Func<TCacheItem> factory,
            string cacheName,
            Func<DistributedCacheEntryOptions> optionsFactory = null) where TCacheItem : class;

        /// <summary>
        /// 异步获取添加缓存值
        /// </summary>
        /// <typeparam name="TCacheItem"></typeparam>
        /// <typeparam name="TCacheKey"></typeparam>
        /// <param name="key"></param>
        /// <param name="factory"></param>
        /// <param name="cacheName"></param>
        /// <param name="optionsFactory"></param>
        /// <returns></returns>
        Task<TCacheItem> GetOrAddAsync<TCacheKey, TCacheItem>(TCacheKey key,
            Func<TCacheItem> factory,
            string cacheName,
            Func<DistributedCacheEntryOptions> optionsFactory = null,
            CancellationToken token = default) where TCacheItem:class;

        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <typeparam name="TCacheKey"></typeparam>
        /// <typeparam name="TCacheItem"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="options"></param>
        /// <param name="hideErrors"></param>
        /// <param name="considerUow"></param>
        void Set<TCacheKey, TCacheItem>(
            TCacheKey key,
            string cacheName,
            TCacheItem value,
            DistributedCacheEntryOptions options = null
        );

        /// <summary>
        /// 异步设置缓存
        /// </summary>
        /// <typeparam name="TCacheKey"></typeparam>
        /// <typeparam name="TCacheItem"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="options"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task SetAsync<TCacheKey, TCacheItem>(
             TCacheKey key,
             string cacheName,
             TCacheItem value,
            DistributedCacheEntryOptions options = null,
            CancellationToken token = default
        );

        /// <summary>
        /// 移除Key
        /// </summary>
        /// <typeparam name="TCacheKey"></typeparam>
        /// <param name="key"></param>
        /// <param name="cacheName"></param>
        /// <param name="hideErrors"></param>
        /// <param name="considerUow"></param>
        void Remove<TCacheKey>(
            TCacheKey key,
            string cacheName
        );

        /// <summary>
        /// 异步移除Key
        /// </summary>
        /// <typeparam name="TCacheKey"></typeparam>
        /// <param name="key"></param>
        /// <param name="cacheName"></param>
        /// <param name="hideErrors"></param>
        /// <param name="considerUow"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task RemoveAsync<TCacheKey>(
            TCacheKey key,
             string cacheName,
            CancellationToken token = default
        );

        /// <summary>
        /// 刷新缓存
        /// </summary>
        /// <typeparam name="TCacheKey"></typeparam>
        /// <param name="key"></param>
        /// <param name="cacheName"></param>
        void Refresh<TCacheKey>(TCacheKey key,
        string cacheName);

        /// <summary>
        /// 异步刷新缓存
        /// </summary>
        /// <typeparam name="TCacheKey"></typeparam>
        /// <param name="key"></param>
        /// <param name="cacheName"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task RefreshAsync<TCacheKey>(
        TCacheKey key,
        string cacheName,
        CancellationToken token = default);
    }
}
