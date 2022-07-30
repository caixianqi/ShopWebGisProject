/************************************************************************************

 * Copyright (c) 2022 All Rights Reserved.

 * CLR版本： 4.0.30319.42000

 *机器名称：LAPTOP-CC5K5UTK

 *命名空间：ShopWebGisLogger.Factory

 *文件名：  IElasticSearchFactory

 *版本号：  V1.0.0.0

 *当前的用户域：LAPTOP-CC5K5UTK

 *创建人： 蔡显麒

 *创建时间：2022/7/27 17:12:48

 *描述 ES工厂接口

/************************************************************************************/
using ShopWebGisElasticSearch;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopWebGisLogger.Factory
{
    public interface IElasticSearchFactory
    {
        IShopLogger GetLogger();
    }
}
