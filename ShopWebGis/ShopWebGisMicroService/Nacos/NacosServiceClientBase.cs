/************************************************************************************

 * Copyright (c) 2022 All Rights Reserved.

 * CLR版本： 4.0.30319.42000

 *机器名称：LAPTOP-CC5K5UTK

 *命名空间：ShopWebGisMicroService.Nacos

 *文件名：  NacosClientBase

 *版本号：  V1.0.0.0

 *当前的用户域：LAPTOP-CC5K5UTK

 *创建人：  蔡显麒

 *创建时间：2022/12/30 14:30:56

 *描述：

/************************************************************************************/

using Microsoft.Extensions.DependencyInjection;
using Nacos.AspNetCore;
using Newtonsoft.Json;
using ShopWebGisDomainShare.Common;
using ShopWebGisLogger.Factory;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ShopWebGisMicroService.Nacos
{
    public abstract class NacosServiceClientBase
    {
        protected INacosServerManager _serverManager;
        protected IHttpClientFactory _clientFactory;
        protected IElasticSearchFactory _elasticSearchFactory;
        protected NacosAspNetCoreOptions _nacosOptions;

        public async Task<T> GetRequestAsync<T>(string serviceName, string url)
        {
            var baseUrl = await _serverManager.GetServerAsync(serviceName, _nacosOptions.GroupName, _nacosOptions.ClusterName, _nacosOptions.Namespace);
            var client = _clientFactory.CreateClient();
            // 设置请求超时时间
            client.Timeout = TimeSpan.FromMinutes(5);

            var getUrl = baseUrl + url;
            _elasticSearchFactory?.GetLogger().LogInfo("Nacos请求地址: " + getUrl);
            var result = await client.GetAsync(getUrl);
            var jsonResult = await result.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ResponseDto<T>>(jsonResult).ResultData;
        }

        public async Task<T> PostRequestAsync<T>(string serviceName, string url, object arg)
        {
            var baseUrl = await _serverManager.GetServerAsync(serviceName, _nacosOptions.GroupName, _nacosOptions.ClusterName, _nacosOptions.Namespace);
            var client = _clientFactory.CreateClient();
            // 设置请求超时时间
            client.Timeout = TimeSpan.FromMinutes(5);

            _elasticSearchFactory?.GetLogger().LogInfo("Nacos请求地址: " + baseUrl + url);
            HttpContent content = new StringContent(JsonConvert.SerializeObject(arg));
            var result = await client.PostAsync(baseUrl + url, content);
            var jsonResult = await result.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ResponseDto<T>>(jsonResult).ResultData;
        }
    }
}
