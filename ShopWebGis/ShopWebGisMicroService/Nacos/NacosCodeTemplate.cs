/************************************************************************************

 * Copyright (c) 2022 All Rights Reserved.

 * CLR版本： 4.0.30319.42000

 *机器名称：LAPTOP-CC5K5UTK

 *命名空间：ShopWebGisMicroService

 *文件名：  CodeTemplate

 *版本号：  V1.0.0.0

 *当前的用户域：LAPTOP-CC5K5UTK

 *创建人：  蔡显麒

 *创建时间：2022/12/30 14:13:01

 *描述：代码字符串模板

/************************************************************************************/

using System;
using System.Collections.Generic;
using System.Text;

namespace ShopWebGisMicroService
{
    public class NacosCodeTemplate : AbstractCodeTemplate
    {
        /// <summary>
        /// 获取类代码字符串
        /// {0} usingName {1} 接口名 {2} 服务名 {3} 方法代码
        /// </summary>
        /// <returns></returns>
        public override string GetClassTemplate()
        {
            return @"
using Microsoft.Extensions.Options;
using Nacos.AspNetCore;
using ShopWebGisLogger.Factory;
using ShopWebGisMicroService.Nacos;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
{0}
namespace ShopWebGisMicroService
        public class {1}Service:NacosServiceClientBase,{1}
        {
            private const string Host={2}
            public {1}Service(INacosServerManager serverManager, IHttpClientFactory clientFactory, IOptions<NacosAspNetCoreOptions> nacosOptions, IElasticSearchFactory elasticSearchFactory)
            {
            this._serverManager = serverManager;
            this._clientFactory = clientFactory;
            this._nacosOptions = nacosOptions.Value;
            this._elasticSearchFactory = elasticSearchFactory;
            }

            {3}
        }
";
        }

        /// <summary>
        /// 获取Get方法模板
        /// {0} 输出类 {1} 方法名 {2}输入类 {3} api地址 {4}Get拼接的参数?xxx=abc&xxx=bva {5} 输出的泛型类
        /// </summary>
        /// <returns></returns>
        public override string GetMethodTemplate()
        {
            return @"
            public async {0} {1}({2})
            {
                string url = {3};
                url += {4};
                return await GetRequestAsync<{5}>(Host, url);
            }
";
        }

        /// <summary>
        /// 获取Post方法模板
        /// {0} 输出类 {1} 方法名 {2}输入类 {3} api地址 {4} 输出的泛型类
        /// </summary>
        /// <returns></returns>
        public override string PostMethodTemplate()
        {
            return @"
            public async {0} {1}({2})
            {
                string url = {3};
                return await PostRequestAsync<{4}>(Host, url,{2});
            }
";
        }
    }
}
