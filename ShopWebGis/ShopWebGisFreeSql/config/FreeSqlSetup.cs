/************************************************************************************

 * Copyright (c) 2022 All Rights Reserved.

 * CLR版本： 4.0.30319.42000

 *机器名称：LAPTOP-CC5K5UTK

 *命名空间：ShopWebGisFreeSql.config

 *文件名：  FreeSqlSetup

 *版本号：  V1.0.0.0

 *当前的用户域：LAPTOP-CC5K5UTK

 *创建人：  蔡显麒

 *创建时间：2022/5/20 16:42:37

 *描述：Freesql配置

/************************************************************************************/

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ShopWebData.config;
using ShopWebGisDomain.ObservationData;
using ShopWebGisDomain.User;
using ShopWebGisDomainShare.Common;
using ShopWebGisFreeSql.Aop;
using ShopWebGisFreeSql.InterFace;
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
            services.Configure<DbMigrationOptions>(x =>
            {
                x.Config("Mysql").Entity<ObservationDataDO>();
            });
            services.AddSingleton<IFreesqlSession, FreeSqlSession>();

        }


    }


}
