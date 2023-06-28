using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using ShopWebGisDomainShare.Common;
using ShopWebGisDomainShare.CustomException;
using ShopWebGisElasticSearch;
using ShopWebGisLogger.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopWebGis.Filters
{
    public class CustomerExceptionFilter : IAsyncExceptionFilter
    {
        private readonly ILogger<CustomerExceptionFilter> _logger;
        private readonly IGisLogger _elasticlogger;
        public CustomerExceptionFilter(ILogger<CustomerExceptionFilter> logger, IElasticSearchFactory elasticSearchFactory)
        {
            _logger = logger;
            _elasticlogger = elasticSearchFactory.GetLogger();
        }
        public Task OnExceptionAsync(ExceptionContext context)
        {

            var respnse = new ResponseDto<object>
            {
                ResultCode = context.HttpContext.Response.StatusCode,
                ErrorMessage = "",
                Success = false
            };
            if (context.ExceptionHandled == false)
            {
                if (context.Exception.GetType() == typeof(ShopWebGisCustomException))
                {
                    respnse.ResultCode = 200;
                    respnse.ErrorMessage = context.Exception.Message;
                    context.Result = new JsonResult(respnse);
                    _logger.LogError(context.Exception.Message);
                }
                else
                {
                    
                    // 500未捕获的异常记录到ealsticsearch,并且打上Exception标识
                    respnse.ResultCode = 500;
                    respnse.ErrorMessage = "内部发生错误!";
                    context.Result = new JsonResult(respnse);
                    _elasticlogger.LogException(context.Exception);
                    _logger.LogError(context.Exception, context.Exception.Message);
                    
                }

            }
            // 设置为true，表示异常已经被处理了
            context.ExceptionHandled = true;
            return Task.CompletedTask;
        }
    }
}
