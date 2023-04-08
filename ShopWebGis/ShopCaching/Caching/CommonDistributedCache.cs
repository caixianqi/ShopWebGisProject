/************************************************************************************

 * Copyright (c) 2023 All Rights Reserved.

 * CLR版本： 4.0.30319.42000

 *机器名称：LAPTOP-CC5K5UTK

 *命名空间：ShopWebCaching.Caching

 *文件名：  DistributedCache

 *版本号：  V1.0.0.0

 *当前的用户域：LAPTOP-CC5K5UTK

 *创建人：  蔡显麒

 *创建时间：2023/4/7 21:50:48

 *描述：

/************************************************************************************/

using JetBrains.Annotations;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using ShopWebGisElasticSearch;
using ShopWebGisLogger.Factory;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ShopWebCaching.Caching
{
    public class CommonDistributedCache : ICommonDistributedCache
    {
        private readonly DistributedCacheOptions _distributedCacheOption;

        private readonly IDistributedCacheKeyNormalizer _distributedCacheKeyNormalizer;
        private readonly Microsoft.Extensions.Caching.Distributed.IDistributedCache _cache;
        private readonly IDistributedCacheSerializer _distributedCacheSerializer;
        private readonly IGisLogger _logger;
        public CommonDistributedCache(IOptions<DistributedCacheOptions> distributedCacheOption, IDistributedCacheKeyNormalizer distributedCacheKeyNormalizer,
            Microsoft.Extensions.Caching.Distributed.IDistributedCache cache,
            IDistributedCacheSerializer distributedCacheSerializer,
            IElasticSearchFactory elasticSearchFactory
            )
        {
            _distributedCacheOption = distributedCacheOption.Value;
            _distributedCacheKeyNormalizer = distributedCacheKeyNormalizer;
            _distributedCacheSerializer = distributedCacheSerializer;
            _cache = cache;
            _logger = elasticSearchFactory.GetLogger();
        }

        /// <summary>
        /// 获取CacheName对应的缓存选项
        /// </summary>
        /// <param name="cacheName"></param>
        protected virtual DistributedCacheEntryOptions SetDefaultDistributedCacheOption(string cacheName)
        {
            if (_distributedCacheOption.CacheNameCacheEntryOptions.ContainsKey(cacheName))
            {
                return _distributedCacheOption.CacheNameCacheEntryOptions[cacheName];
            }
            return _distributedCacheOption.GlobalCacheEntryOptions;
        }

        protected virtual string NormalizeKey<TCacheKey>(TCacheKey key, string cacheName)
        {
            return _distributedCacheKeyNormalizer.NormalizeKey(
                new DistributedCacheKeyNormalizeArgs(
                    key.ToString(),
                    cacheName
                )
            );
        }

        protected virtual TCacheItem ByteToCacheItem<TCacheItem>(byte[] bytes) where TCacheItem : class
        {
            if (bytes == null)
            {
                return null;
            }

            return _distributedCacheSerializer.DeSerialize<TCacheItem>(bytes);
        }

        public TCacheItem Get<TCacheItem, TCacheKey>(TCacheKey key, string cacheName) where TCacheItem : class
        {
            byte[] cachedBytes = default;
            try
            {
                cachedBytes = _cache.Get(NormalizeKey(key, cacheName));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }



            return ByteToCacheItem<TCacheItem>(cachedBytes);
        }

        public async Task<TCacheItem> GetAsync<TCacheItem, TCacheKey>(TCacheKey key, string cacheName, CancellationToken token = default) where TCacheItem : class
        {
            byte[] cachedBytes = default;
            try
            {
                cachedBytes = await _cache.GetAsync(NormalizeKey(key, cacheName), token);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return ByteToCacheItem<TCacheItem>(cachedBytes);
        }

        public void Set<TCacheKey, TCacheItem>(TCacheKey key, string cacheName, TCacheItem value, DistributedCacheEntryOptions options = null)
        {
            try
            {
                _cache.Set(NormalizeKey(key, cacheName),
                    _distributedCacheSerializer.Serialize(value),
                    options ?? SetDefaultDistributedCacheOption(cacheName) // 缓存选项为空从配置中获取
                    );
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }

        public async Task SetAsync<TCacheKey, TCacheItem>(TCacheKey key, string cacheName, TCacheItem value, DistributedCacheEntryOptions options = null, CancellationToken token = default)
        {
            try
            {
                await _cache.SetAsync(NormalizeKey(key, cacheName),
                    _distributedCacheSerializer.Serialize(value),
                    options ?? SetDefaultDistributedCacheOption(cacheName), // 缓存选项为空从配置中获取
                    token
                    );
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }

        public TCacheItem GetOrAdd<TCacheItem, TCacheKey>(TCacheKey key, Func<TCacheItem> factory, string cacheName, Func<Microsoft.Extensions.Caching.Distributed.DistributedCacheEntryOptions> optionsFactory = null) where TCacheItem : class
        {
            var value = Get<TCacheItem, TCacheKey>(key, cacheName);
            if (value != null)
            {
                return value;
            }

            value = factory?.Invoke();
            Set(key, cacheName, value, optionsFactory?.Invoke());
            return value;
        }

        public async Task<TCacheItem> GetOrAddAsync<TCacheItem, TCacheKey>(TCacheKey key, Func<TCacheItem> factory, string cacheName, Func<Microsoft.Extensions.Caching.Distributed.DistributedCacheEntryOptions> optionsFactory = null, CancellationToken token = default) where TCacheItem : class
        {
            var value = await GetAsync<TCacheItem, TCacheKey>(key, cacheName, token);
            if (value != null)
            {
                return value;
            }
            value = factory?.Invoke();
            await SetAsync(key, cacheName, value, optionsFactory?.Invoke(), token);
            return value;
        }

        public void Refresh<TCacheKey>(TCacheKey key, string cacheName)
        {
            try
            {
                _cache.Refresh(NormalizeKey(key, cacheName));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }

        public async Task RefreshAsync<TCacheKey>(TCacheKey key, string cacheName, CancellationToken token = default)
        {
            try
            {
                await _cache.RefreshAsync(NormalizeKey(key, cacheName), token);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }

        public void Remove<TCacheKey>(TCacheKey key, string cacheName)
        {
            try
            {
                _cache.Remove(NormalizeKey(key, cacheName));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }

        public async Task RemoveAsync<TCacheKey>(TCacheKey key, string cacheName, CancellationToken token = default)
        {
            try
            {
                await _cache.RemoveAsync(NormalizeKey(key, cacheName));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }


    }
}
