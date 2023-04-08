/************************************************************************************

 * Copyright (c) 2023 All Rights Reserved.

 * CLR版本： 4.0.30319.42000

 *机器名称：LAPTOP-CC5K5UTK

 *命名空间：ShopWebCaching.Caching

 *文件名：  DistributedCacheSerializer

 *版本号：  V1.0.0.0

 *当前的用户域：LAPTOP-CC5K5UTK

 *创建人：  蔡显麒

 *创建时间：2023/4/8 0:37:58

 *描述：

/************************************************************************************/

using ShopWebJson;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopWebCaching.Caching
{
    public class DistributedCacheSerializer : IDistributedCacheSerializer
    {
        private readonly IJsonSerializer _jsonSerializer;

        public DistributedCacheSerializer(IJsonSerializer jsonSerializer)
        {
            _jsonSerializer = jsonSerializer;
        }
            
        public T DeSerialize<T>(byte[] bytes)
        {
            return _jsonSerializer.Deserialize<T>(Encoding.UTF8.GetString(bytes));
        }

        public byte[] Serialize<T>(T o)
        {
            return Encoding.UTF8.GetBytes(_jsonSerializer.Serialize(o));
        }
    }
}
