using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using ShopWebGisDomainShare.Common;
using ShopWebGisDomainShare.CustomException;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopWebGis.Filters
{
    public class CustomerExceptionFilter : IAsyncExceptionFilter
    {
        private readonly ILogger<CustomerExceptionFilter> _logger;
        public CustomerExceptionFilter(ILogger<CustomerExceptionFilter> logger)
        {
            _logger = logger;
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
                }
                else
                {
                    _logger.LogError(context.Exception, context.Exception.Message);
                    // 记录到elasticsearch
                    respnse.ResultCode = 500;
                    respnse.ErrorMessage = "内部发生错误!";
                    context.Result = new JsonResult(respnse);

                }

            }
            // 设置为true，表示异常已经被处理了
            context.ExceptionHandled = true;
            return Task.CompletedTask;
        }
    }
}
