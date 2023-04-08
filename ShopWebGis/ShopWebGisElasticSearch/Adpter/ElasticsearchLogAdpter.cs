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
using ShopWebGisDomainShare.Common;
using ShopWebGisDomainShare.Const;
using ShopWebGisDomainShare.Const.CacheKey;
using ShopWebGisElasticSearch;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace ShopWebGisLogger.Adpter
{
    public class ElasticsearchLogAdpter : IGisLogger
    {

        private readonly IOptions<EalsticSearchLogOption> _options;
        private readonly IHttpClientFactory _httpClientFactory;

        public ElasticsearchLogAdpter(IOptions<EalsticSearchLogOption> options, IHttpClientFactory httpClientFactory)
        {
            _options = options;
            _httpClientFactory = httpClientFactory;
        }

        public void LogDebug(string msg, string index = null)
        {
            new ELKConnectClient(_options, _httpClientFactory).Log(new LogModel()
            {
                Level = Enum.GetName(typeof(LogLevel), LogLevel.Debug),
                Msg = msg,
                Success = true,
                Index = !string.IsNullOrWhiteSpace(index) ? index : _options.Value.Index
            });

        }

        public void LogError(string msg, string index = null)
        {
            new ELKConnectClient(_options, _httpClientFactory).Log(new LogModel()
            {
                Level = Enum.GetName(typeof(LogLevel), LogLevel.Error),
                Msg = msg,
                Success = true,
                Index = !string.IsNullOrWhiteSpace(index) ? index : _options.Value.Index
            });

        }

        public void LogException(Exception exception, string index = null)
        {
            new ELKConnectClient(_options, _httpClientFactory).Log(new LogModel()
            {
                Level = Enum.GetName(typeof(LogLevel), LogLevel.Error),
                Exception = exception,
                Success = false,
                Index = !string.IsNullOrWhiteSpace(index) ? index : _options.Value.Index
            });

        }

        public void LogInfo(string msg, string index = null)
        {
            new ELKConnectClient(_options, _httpClientFactory).Log(new LogModel()
            {
                Level = Enum.GetName(typeof(LogLevel), LogLevel.Info),
                Msg = msg,
                Success = true,
                Index = !string.IsNullOrWhiteSpace(index) ? index : _options.Value.Index
            });

        }

        public void LogInfo(object msg, string index = null)
        {
            new ELKConnectClient(_options, _httpClientFactory).Log(new LogModel()
            {
                Level = Enum.GetName(typeof(LogLevel), LogLevel.Info),
                Msg = msg,
                Success = true,
                Index = !string.IsNullOrWhiteSpace(index) ? index : _options.Value.Index
            });
        }

    }
}
