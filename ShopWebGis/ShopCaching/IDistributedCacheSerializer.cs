/************************************************************************************

 * Copyright (c) 2023 All Rights Reserved.

 * CLR版本： 4.0.30319.42000

 *机器名称：LAPTOP-CC5K5UTK

 *命名空间：ShopWebCaching

 *文件名：  IDistributedCacheSerializer

 *版本号：  V1.0.0.0

 *当前的用户域：LAPTOP-CC5K5UTK

 *创建人： 蔡显麒

 *创建时间：2023/4/7 23:57:32

 *描述：缓存序列化

/************************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopWebCaching
{
    public interface IDistributedCacheSerializer
    {
        byte []  Serialize<T>(T o);

        T DeSerialize<T>(byte[] bytes);

    }
}
