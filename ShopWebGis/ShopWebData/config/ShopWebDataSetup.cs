/************************************************************************************

 * Copyright (c) 2022 All Rights Reserved.

 * CLR版本： 4.0.30319.42000

 *机器名称：LAPTOP-CC5K5UTK

 *命名空间：ShopWebData.config

 *文件名：  ShopWebDataSetup

 *版本号：  V1.0.0.0

 *当前的用户域：LAPTOP-CC5K5UTK

 *创建人：  蔡显麒

 *创建时间：2022/11/25 10:44:30

 *描述：

/************************************************************************************/

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ShopWebData.SubTable;
using ShopWebData.SubTable.SubRule;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ShopWebData.config
{
    public static class ShopWebDataSetup
    {
        public static void Setup(this IServiceCollection services)
        {
            var jsonPath = "subsetting.json";
            var baseDirectory = Directory.GetCurrentDirectory();
            var path = Path.Combine(baseDirectory, jsonPath);
            if (File.Exists(path))
            {
                var builder = new ConfigurationBuilder()
                .SetBasePath(baseDirectory)
                .AddJsonFile(jsonPath, optional: false);

                var configuration = builder.Build();
                services.Configure<TableSubRuleOptions>(configuration);
            }
            services.AddTransient<ISubRuleResolver, SubRuleResolver>();
            services.AddTransient<ISubRule, DateSubRule>();
        }
    }
}
