/************************************************************************************

 * Copyright (c) 2022 All Rights Reserved.

 * CLR版本： 4.0.30319.42000

 *机器名称：LAPTOP-CC5K5UTK

 *命名空间：ShopWebGisMicroService

 *文件名：  ExampleClassNacosService

 *版本号：  V1.0.0.0

 *当前的用户域：LAPTOP-CC5K5UTK

 *创建人：  蔡显麒

 *创建时间：2022/12/30 17:47:29

 *描述：模拟动态实现类的代码

/************************************************************************************/

using Microsoft.Extensions.Options;
using Nacos.AspNetCore;
using ShopWebGisLogger.Factory;
using ShopWebGisMicroService.Nacos;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ShopWebGisMicroService
{
    /// <summary>
    /// 模拟实现类的代码
    /// </summary>
    public class ExampleClassNacosService : NacosServiceClientBase, IClass
    {
        private const string Host = "serviceName";

        public ExampleClassNacosService(INacosServerManager serverManager, IHttpClientFactory clientFactory, IOptions<NacosAspNetCoreOptions> nacosOptions, IElasticSearchFactory elasticSearchFactory)
        {
            this._serverManager = serverManager;
            this._clientFactory = clientFactory;
            this._nacosOptions = nacosOptions.Value;
            this._elasticSearchFactory = elasticSearchFactory;
        }

        public async Task<OutPut> GetXXXMethod(string input)
        {
            string url = "/api/test";
            url += input;
            return await GetRequestAsync<OutPut>(Host, url);
        }

        public async Task<OutPut> PostXXXMethod(Input input)
        {
            string url = "/api/test";
            return await PostRequestAsync<OutPut>(Host, url, input);
        }
    }

    public class OutPut
    {

    }

    public class Input
    {

    }

    /// <summary>
    /// 接口
    /// </summary>
    public interface IClass
    {

    }
}
