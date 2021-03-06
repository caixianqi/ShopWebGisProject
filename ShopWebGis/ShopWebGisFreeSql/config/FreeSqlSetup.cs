/************************************************************************************

 * Copyright (c) 2022 All Rights Reserved.

 * CLR版本： 4.0.30319.42000

 *机器名称：LAPTOP-CC5K5UTK

 *命名空间：ShopWebGisFreeSql.config

 *文件名：  FreeSqlSetup

 *版本号：  V1.0.0.0

 *当前的用户域：LAPTOP-CC5K5UTK

 *创建人：  智慧环保部-蔡显麒

 *创建时间：2022/5/20 16:42:37

 *描述：Freesql配置

/************************************************************************************/

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ShopWebGisFreeSql.Aop;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace ShopWebGisFreeSql.config
{
    public static class FreeSqlSetup
    {
        public static void ShopWebGisFreeSqlSetup(this IServiceCollection services, IConfiguration configuration)
        {
            IFreeSql fsql = new FreeSql.FreeSqlBuilder()
             .UseConnectionString(FreeSql.DataType.MySql, configuration.GetConnectionString("Mysql"))
            // .UseAutoSyncStructure(true) //自动同步实体结构到数据库，FreeSql不会扫描程序集，只有CRUD时才会生成表。
            .Build();
            services.AddSingleton<IFreeSql>(fsql);
            services.AddScoped<FreeSqlCrudBeforeLog>();
            fsql.Aop.CurdBefore += (s, e) =>
            {               
                FreeSqlCrudBeforeLog.Current.Value?._logger.LogInformation(e.Sql);// AOP记录日志
            };                       
        }
    }


}
