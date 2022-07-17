/************************************************************************************

 * Copyright (c) 2022 All Rights Reserved.

 * CLR版本： 4.0.30319.42000

 *机器名称：LAPTOP-CC5K5UTK

 *命名空间：ShopWebGisXxlJob.config

 *文件名：  XxlJobSetup

 *版本号：  V1.0.0.0

 *当前的用户域：LAPTOP-CC5K5UTK

 *创建人：  智慧环保部-蔡显麒

 *创建时间：2022/6/2 15:03:29

 *描述：XxlJob服务注册

/************************************************************************************/

using DotXxlJob.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using ShopWebGisXxlJob.Jobs;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopWebGisXxlJob.config
{
    public static class XxlJobSetup
    {
        public static void  XxlJobServiceSetup(this IServiceCollection services)
        {
            services.AddSingleton<IJobHandler, DemoJobHandler>();
            services.AddSingleton<IJobHandler, CrawlerJDGoodsClassJob>();
        }

        public static IApplicationBuilder UseXxlJobExecutor(this IApplicationBuilder @this)
        {
            return @this.UseMiddleware<XxlJobExecutorMiddleware>();
        }

    }
}
