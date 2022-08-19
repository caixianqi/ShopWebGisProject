/************************************************************************************

 * Copyright (c) 2022 All Rights Reserved.

 * CLR版本： 4.0.30319.42000

 *机器名称：LAPTOP-CC5K5UTK

 *命名空间：ShopWebGisLogger.Factory

 *文件名：  ElasticFactory

 *版本号：  V1.0.0.0

 *当前的用户域：LAPTOP-CC5K5UTK

 *创建人：  蔡显麒

 *创建时间：2022/7/27 16:38:00

 *描述：ES工厂
/************************************************************************************/

using Microsoft.Extensions.Options;
using ShopWebGisDomain.config;
using ShopWebGisElasticSearch;
using ShopWebGisLogger.Adpter;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace ShopWebGisLogger.Factory
{
    public class ElasticSearchFactory : IElasticSearchFactory
    {
        private readonly IOptions<EalsticSearchLogOption> _option;
        private readonly IHttpClientFactory _httpClientFactory;
        public ElasticSearchFactory(IOptions<EalsticSearchLogOption> option, IHttpClientFactory httpClientFactory)
        {
            _option = option;
            _httpClientFactory = httpClientFactory;
        }
        /// <summary>
        /// 获取日志接口
        /// </summary>
        /// <returns></returns>
        public IGisLogger GetLogger()
        {
            return new ElasticsearchLogAdpter(_option, _httpClientFactory);
        }
    }
}
