/************************************************************************************

 * Copyright (c) 2023 All Rights Reserved.

 * CLR版本： 4.0.30319.42000

 *机器名称：LAPTOP-CC5K5UTK

 *命名空间：ShopWebCaching

 *文件名：  DistributedCacheKeyNormalizeArgs

 *版本号：  V1.0.0.0

 *当前的用户域：LAPTOP-CC5K5UTK

 *创建人：  蔡显麒

 *创建时间：2023/4/4 15:13:16

 *描述：缓存Key参数

/************************************************************************************/

using System;
using System.Collections.Generic;
using System.Text;

namespace ShopWebCaching
{
    public class DistributedCacheKeyNormalizeArgs
    {

        public DistributedCacheKeyNormalizeArgs(string key, string cacheName)
        {
            Key = key;
            CacheName = cacheName;
        }
        public string Key { get; set; }

        public string CacheName { get; set; }


    }
}
