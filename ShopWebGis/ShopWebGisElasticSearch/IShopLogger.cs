/************************************************************************************

 * Copyright (c) 2022 All Rights Reserved.

 * CLR版本： 4.0.30319.42000

 *机器名称：LAPTOP-CC5K5UTK

 *命名空间：ShopWebGisElasticSearch

 *文件名：  IShopLogger

 *版本号：  V1.0.0.0

 *当前的用户域：LAPTOP-CC5K5UTK

 *创建人： 蔡显麒

 *创建时间：2022/7/25 11:57:17

 *描述：日志接口

/************************************************************************************/
using ShopWebGisDomainShare.Const.CacheKey;
using System;
using System.Collections.Generic;
using System.Text;


namespace ShopWebGisElasticSearch
{
    public interface IShopLogger
    {
        void LogInfo(string message, string index = CommonKeys.LoggerIndex);

        void LogInfo(object msg);

        void LogDebug(string message);

        void LogError(string msg);
        void LogException(Exception exception);
    }
}
