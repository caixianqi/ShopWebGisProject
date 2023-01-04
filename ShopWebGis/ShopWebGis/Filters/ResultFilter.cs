using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using ShopWebGis.Attributes;
using ShopWebGisDomainShare.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ShopWebGis.Filters
{
    public class ResultFilter : IResultFilter
    {
        public void OnResultExecuted(ResultExecutedContext context)
        {

        }

        public void OnResultExecuting(ResultExecutingContext context)
        {
            // 不对ApiIgnoreAttribute特性进行封装
            var controllerActionDescriptor = context.ActionDescriptor as ControllerActionDescriptor;
            if (controllerActionDescriptor != null)
            {
                var isDefined = controllerActionDescriptor.EndpointMetadata.Any(a => a.GetType().Equals(typeof(ApiIgnoreAttribute)));
                if (isDefined)
                {
                    return;
                }

            }
            var respnse = new ResponseDto<object>
            {
                ResultCode = context.HttpContext.Response.StatusCode,
                ErrorMessage = "",
                Success = true
            };
            if (context.Result!= null)
            {
                if (context.Result is ObjectResult)
                {
                    var result = context.Result as ObjectResult;
                    respnse.ResultData = result.Value;
                    context.Result = new JsonResult(respnse);

                }
                else if (context.Result is EmptyResult)
                {
                    context.Result = new JsonResult(respnse);
                }
                else if (context.Result is ContentResult)
                {
                    var result = context.Result as ContentResult;
                    respnse.ResultData = result.Content;
                    context.Result = new JsonResult(respnse);
                }
            }
        }
    }
}
