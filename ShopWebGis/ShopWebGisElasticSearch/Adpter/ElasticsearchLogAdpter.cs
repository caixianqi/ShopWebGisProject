/************************************************************************************

 * Copyright (c) 2022 All Rights Reserved.

 * CLR版本： 4.0.30319.42000

 *机器名称：LAPTOP-CC5K5UTK

 *命名空间：ShopWebGisLogger.Adpter

 *文件名：  ElasticsearchLogAdpter

 *版本号：  V1.0.0.0

 *当前的用户域：LAPTOP-CC5K5UTK

 *创建人：  蔡显麒

 *创建时间：2022/7/25 16:09:08

 *描述：ES-Logger 适配器

/************************************************************************************/

using Microsoft.Extensions.Options;
using ShopWebGisCache;
using ShopWebGisDomain.config;
using ShopWebGisElasticSearch;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopWebGisLogger.Adpter
{
    public class ElasticsearchLogAdpter : IShopLogger
    {
        private readonly IShopWebGisCache _cache;
        private readonly IOptions<EalsticSearchConfig> _options;

        public ElasticsearchLogAdpter(IShopWebGisCache cache, IOptions<EalsticSearchConfig> options)
        {
            _cache = cache;
            _options = options;
        }

        public void LogDebug(string message, string index = "logger_Index")
        {
            throw new NotImplementedException();
        }

        public void LogError(string msg, string index = "logger_Index")
        {
            throw new NotImplementedException();
        }

        public void LogException(Exception exception, string index = "logger_Index")
        {
            throw new NotImplementedException();
        }

        public void LogInfo(string message, string index = "logger_Index")
        {
            throw new NotImplementedException();
        }

        public void LogInfo(object msg, string index = "logger_Index")
        {
            throw new NotImplementedException();
        }
    }
}
