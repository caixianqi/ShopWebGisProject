/************************************************************************************

 * Copyright (c) 2022 All Rights Reserved.

 * CLR版本： 4.0.30319.42000

 *机器名称：LAPTOP-CC5K5UTK

 *命名空间：ShopWebTest

 *文件名：  FreeSqlTest

 *版本号：  V1.0.0.0

 *当前的用户域：LAPTOP-CC5K5UTK

 *创建人：  蔡显麒

 *创建时间：2022/12/14 17:12:36

 *描述：

/************************************************************************************/

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ShopWebData.config;
using ShopWebGisFreeSql.config;
using ShopWebGisFreeSql.InterFace;
using ShopWebGisLogger.Factory;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xunit;

namespace ShopWebTest
{
    public class FreeSqlTest
    {
        private readonly IServiceProvider serviceProvider;
        public FreeSqlTest()
        {
            ServiceCollection services = new ServiceCollection();
            var jsonPath = "appsettings.json";
            var baseDirectory = Directory.GetCurrentDirectory();
            var path = Path.Combine(baseDirectory, jsonPath);
            if (File.Exists(path))
            {
                var builder = new ConfigurationBuilder()
                .SetBasePath(baseDirectory)
                .AddJsonFile(jsonPath, optional: false);

                var configuration = builder.Build();
                services.ShopWebGisFreeSqlSetup(configuration);
                services.AddSingleton<IConfiguration>(configuration);
                services.AddSingleton<IElasticSearchFactory, ElasticSearchFactory>();// ES工厂接口
                services.AddHttpClient();
            }
            var tt = Activator.CreateInstance(typeof(IElasticSearchFactory));
            serviceProvider = services.BuildServiceProvider();

        }

        [Fact]
        public void TestIFreeSqlSession()
        {
            var Singletons = serviceProvider.GetRequiredService<IFreesqlSession>();
            IFreeSql freeSql = Singletons.Get("Mysql");
        }
    }



}
