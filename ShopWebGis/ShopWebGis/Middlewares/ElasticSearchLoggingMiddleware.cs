/************************************************************************************

 * Copyright (c) 2022 All Rights Reserved.

 * CLR版本： 4.0.30319.42000

 *机器名称：LAPTOP-CC5K5UTK

 *命名空间：ShopWebGisElasticSearch.Logger

 *文件名：  ElasticSearchLoggingMiddleware

 *版本号：  V1.0.0.0

 *当前的用户域：LAPTOP-CC5K5UTK

 *创建人：  蔡显麒

 *创建时间：2022/7/28 15:43:07

 *描述：ES日志中间件

/************************************************************************************/

using Microsoft.AspNetCore.Http;
using ShopWebGisDomain.config;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace ShopWebGisElasticSearch.Logger
{
    public class ElasticSearchLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly EalsticSearchLogOption _option;
        private readonly IGisLogger _logger;
        public ElasticSearchLoggingMiddleware(RequestDelegate next, EalsticSearchLogOption option, IGisLogger logger)
        {
            if (option == null) throw new ArgumentNullException(nameof(option));
            _next = next ?? throw new ArgumentNullException(nameof(next));
            if (logger == null) throw new ArgumentNullException(nameof(option));
            _next = next;
            _option = option;
            _logger = logger;

        }
        public async Task Invoke(HttpContext httpContext)
        {
            if (httpContext == null) throw new ArgumentNullException(nameof(httpContext));
            var _stopwatch = new Stopwatch();
            HttpRequestLogModel httpRequestLogModel = new HttpRequestLogModel();
            _stopwatch.Restart();
            var request = httpContext.Request;
            httpRequestLogModel.RequestUrl = request.Path;
            httpRequestLogModel.RequestMethod = request.Method;
            var method = request.Method.ToLower();
            //获取请求body内容
            if (method.Equals("post") || method.Equals("put"))
            {
                // 启用倒带功能，就可以让 Request.Body 可以再次读取
                request.EnableBuffering();

                Stream stream = request.Body;
                byte[] buffer = new byte[request.ContentLength.Value];
                stream.Read(buffer, 0, buffer.Length);
                httpRequestLogModel.RequestParameter = Encoding.UTF8.GetString(buffer);
                request.Body.Position = 0;

            }
            else if (method.Equals("get") || method.Equals("delete"))
            {
                httpRequestLogModel.RequestParameter = request.QueryString.Value;
            }
            var originalBodyStream = httpContext.Response.Body;
            HttpReponseLogModel httpReponseLogModel = new HttpReponseLogModel();
            using (var responseBody = new MemoryStream())
            {
                httpContext.Response.Body = responseBody;

                await _next(httpContext);

                var reponseText = await GetResponse(httpContext.Response);
                httpReponseLogModel.ReponseBody = reponseText.body;
                httpReponseLogModel.ReponseStatusCode = reponseText.statusCode;
                await responseBody.CopyToAsync(originalBodyStream);
            }

            // 响应完成记录时间和存入日志
            httpContext.Response.OnCompleted(() =>
            {
                _stopwatch.Stop();
                httpReponseLogModel.ReponseTime = _stopwatch.ElapsedMilliseconds;
                var logMessage = string.Format(_option.LogMessagetemplate, httpRequestLogModel.ToString(), httpReponseLogModel.ToString());
                _logger.LogInfo(logMessage);
                return Task.CompletedTask;
            });
        }


        /// <summary>
        /// 获取响应内容
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        private async Task<(string body, int statusCode)> GetResponse(HttpResponse response)
        {
            response.Body.Seek(0, SeekOrigin.Begin);
            var text = await new StreamReader(response.Body).ReadToEndAsync();
            response.Body.Seek(0, SeekOrigin.Begin);
            return (text, response.StatusCode);
        }


    }
}
