/************************************************************************************

 * Copyright (c) 2023 All Rights Reserved.

 * CLR版本： 4.0.30319.42000

 *机器名称：LAPTOP-CC5K5UTK

 *命名空间：ShopWebCaching.Caching

 *文件名：  DistributedCacheKeyNormalizer

 *版本号：  V1.0.0.0

 *当前的用户域：LAPTOP-CC5K5UTK

 *创建人：  蔡显麒

 *创建时间：2023/4/5 0:04:16

 *描述：

/************************************************************************************/

using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopWebCaching.Caching
{
    public class DistributedCacheKeyNormalizer : IDistributedCacheKeyNormalizer
    {
        private readonly DistributedCacheOptions _distributedCacheOptions;

        public DistributedCacheKeyNormalizer(IOptions<DistributedCacheOptions> distributedCacheOptions)
        {
            _distributedCacheOptions = distributedCacheOptions.Value;
        }

        public string NormalizeKey(DistributedCacheKeyNormalizeArgs args)
        {
            return $"c:{args.CacheName},k:{_distributedCacheOptions.KeyPrefix}{args.Key}";
        }
    }
}
