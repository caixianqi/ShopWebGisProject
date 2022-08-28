/************************************************************************************

 * Copyright (c) 2022 All Rights Reserved.

 * CLR版本： 4.0.30319.42000

 *机器名称：LAPTOP-CC5K5UTK

 *命名空间：ShopWebGisElasticSearch

 *文件名：  ELKConnectClient

 *版本号：  V1.0.0.0

 *当前的用户域：LAPTOP-CC5K5UTK

 *创建人：  蔡显麒

 *创建时间：2022/8/8 20:51:04

 *描述：ELK连接类

/************************************************************************************/

using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ShopWebGisDomain.config;
using ShopWebGisDomainShare.Common;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ShopWebGisElasticSearch
{
    public class ELKConnectClient
    {
        private Uri baseUrl;
        private string index;
        private static HashSet<string> existsIndex = new HashSet<string>(); // hashset集合存储索引
        IOptions<EalsticSearchLogOption> _elkOption;
        private readonly IHttpClientFactory _httpClientFactory;
        public ELKConnectClient(IOptions<EalsticSearchLogOption> elkOption, IHttpClientFactory httpClientFactory)
        {
            _elkOption = elkOption;
            var elkConfig = elkOption.Value;
            baseUrl = new Uri(elkConfig.Url);
            index = elkConfig.Index.ToLower() + DateTime.Now.ToString("yyyy-MM");// 利用时间作为索引后缀
            _httpClientFactory = httpClientFactory;
        }

        public void Log(LogModel msg)
        {
            try
            {
                Thread.Sleep(1);
                ThreadPool.QueueUserWorkItem(state =>
                {
                    PutMsg(msg);
                });

            }
            catch
            {
                // 不抛出异常 
            }
        }

        /// <summary>
        /// 发送请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        private async Task<string> Post(Uri url, Func<string> func)
        {

            using (var httpClient = _httpClientFactory.CreateClient())
            {
                HttpRequestMessage request = new HttpRequestMessage();
                request.Method = HttpMethod.Post;
                request.RequestUri = url;
                string param = func?.Invoke();
                request.Content = new StringContent(param);
                request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                request.Headers.Add("kbn-xsrf", "kibana");
                httpClient.Timeout = TimeSpan.FromSeconds(1000);//超过一秒就不管
                var response = await httpClient.SendAsync(request);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    string data = await response.Content.ReadAsStringAsync();
                    return data;
                }
                else
                {
                    return "";
                }
            }


        }

        private async void PutMsg(LogModel msg)
        {
            if (!existsIndex.Contains(index))
            {
                if (!await QueryIndexExists(index))
                {
                    //查询索引是否存在,不存在则尝试新建
                    CreateIndex(index);
                }
                else
                {
                    existsIndex.Add(index);
                }
            }
            Uri proxyUrl = new Uri(baseUrl, $"/api/console/proxy?path={index}/_doc/{Guid.NewGuid()}&method=PUT");
            await Post(proxyUrl, () =>
            {
                string param = JsonConvert.SerializeObject(msg, _elkOption.Value.JsonSerializerSettings);
                return param;
            });
        }

        // 创建索引
        private async void CreateIndex(string indexName)
        {
            //太多换行,先编码再解码
            string createIndexString = @"ew0KCSJhbGlhc2VzIjoge30sDQoJIm1hcHBpbmdzIjogew0KCQkiZHluYW1pYyI6
ICJ0cnVlIiwNCgkJIl9zb3VyY2UiOiB7DQoJCQkiaW5jbHVkZXMiOiBbXSwNCgkJ
CSJleGNsdWRlcyI6IFtdDQoJCX0sDQoJCSJkeW5hbWljX2RhdGVfZm9ybWF0cyI6
IFsNCgkJCSJzdHJpY3RfZGF0ZV9vcHRpb25hbF90aW1lIiwNCgkJCSJ5eXl5L01N
L2RkIEhIOm1tOnNzfHx5eXl5L01NL2RkfHx5eXl5LU1NLWRkIEhIOm1tOnNzfHx5
eXl5LU1NLWRkIEhIOm1tOnNzfHx5eXl5LU1NLWRkIEhIOm1tOnNzLlNTUyINCgkJ
XSwNCgkJImRhdGVfZGV0ZWN0aW9uIjogdHJ1ZSwNCgkJIm51bWVyaWNfZGV0ZWN0
aW9uIjogZmFsc2UsDQoJCSJwcm9wZXJ0aWVzIjogew0KCQkJImxldmVsIjogew0K
CQkJCSJ0eXBlIjogInRleHQiLA0KCQkJCSJmaWVsZHMiOiB7DQoJCQkJCSJrZXl3
b3JkIjogew0KCQkJCQkJInR5cGUiOiAia2V5d29yZCIsDQoJCQkJCQkiaWdub3Jl
X2Fib3ZlIjogMjU2DQoJCQkJCX0NCgkJCQl9DQoJCQl9LA0KCQkJImxvZ1RpbWUi
OiB7DQoJCQkJInR5cGUiOiAiZGF0ZSIsDQoJCQkJImZvcm1hdCI6ICJ5eXl5LU1N
LWRkIEhIOm1tOnNzfHx5eXl5LU1NLWRkIEhIOm1tOnNzLlNTU3x8eXl5eS1NTS1k
ZHx8ZXBvY2hfbWlsbGlzIg0KCQkJfSwNCgkJCSJtc2ciOiB7DQoJCQkJInR5cGUi
OiAidGV4dCIsDQoJCQkJImZpZWxkcyI6IHsNCgkJCQkJImtleXdvcmQiOiB7DQoJ
CQkJCQkidHlwZSI6ICJrZXl3b3JkIiwNCgkJCQkJCSJpZ25vcmVfYWJvdmUiOiA4
MTAwDQoJCQkJCX0NCgkJCQl9DQoJCQl9LA0KCQkJInN1Y2Nlc3MiOiB7DQoJCQkJ
InR5cGUiOiAiYm9vbGVhbiINCgkJCX0NCgkJfQ0KCX0NCn0=";

            Uri url = new Uri(baseUrl, $"/api/console/proxy?path={indexName}&method=PUT");
            byte[] bytes = Convert.FromBase64String(createIndexString);
            string decodeCreateIndexStr = Encoding.UTF8.GetString(bytes);

            string res = await Post(url, () => decodeCreateIndexStr);
            if (!string.IsNullOrEmpty(res))
            {
                JObject result = JsonConvert.DeserializeObject<JObject>(res);
                var flag = result.SelectToken("acknowledged").Value<bool>();
                if (flag)
                {
                    existsIndex.Add(index);
                }
            }
        }

        // 查询索引
        private async Task<bool> QueryIndexExists(string indexName)
        {
            Uri url = new Uri(baseUrl, $"/api/console/proxy?path={indexName}&method=GET");
            string res = await Post(url, () => "");
            return !string.IsNullOrEmpty(res);
        }
    }
}

