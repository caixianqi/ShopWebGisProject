/************************************************************************************

 * Copyright (c) 2023 All Rights Reserved.

 * CLR版本： 4.0.30319.42000

 *机器名称：LAPTOP-CC5K5UTK

 *命名空间：ShopWebCaching

 *文件名：  DistributedCacheOptions

 *版本号：  V1.0.0.0

 *当前的用户域：LAPTOP-CC5K5UTK

 *创建人：  蔡显麒

 *创建时间：2023/4/4 15:11:32

 *描述：

/************************************************************************************/

using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopWebCaching
{
    public class DistributedCacheOptions
    {
        public string KeyPrefix { get; set; }

        public DistributedCacheEntryOptions GlobalCacheEntryOptions { get; set; }

        /// <summary>
        /// CacheName对应的缓存选项设置
        /// </summary>
        public IDictionary<string, DistributedCacheEntryOptions> CacheNameCacheEntryOptions { get; set; }

        public DistributedCacheOptions()
        {
            GlobalCacheEntryOptions = new DistributedCacheEntryOptions();
            CacheNameCacheEntryOptions = new Dictionary<string, DistributedCacheEntryOptions>();
            KeyPrefix = "";
        }
    }
}
