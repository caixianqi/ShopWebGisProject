/************************************************************************************

 * Copyright (c) 2023 All Rights Reserved.

 * CLR版本： 4.0.30319.42000

 *机器名称：LAPTOP-CC5K5UTK

 *命名空间：ShopWebCaching

 *文件名：  IDistributedCacheKeyNormalizer

 *版本号：  V1.0.0.0

 *当前的用户域：LAPTOP-CC5K5UTK

 *创建人： 蔡显麒

 *创建时间：2023/4/5 0:03:31

 *描述：缓存Key生成接口

/************************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopWebCaching
{
    public interface IDistributedCacheKeyNormalizer
    {
        /// <summary>
        /// 生成标准形式的Key
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        string NormalizeKey(DistributedCacheKeyNormalizeArgs args);
    }
}
