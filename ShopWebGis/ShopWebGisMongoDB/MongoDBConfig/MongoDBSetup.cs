/************************************************************************************

 * Copyright (c) 2022 All Rights Reserved.

 * CLR版本： 4.0.30319.42000

 *机器名称：LAPTOP-CC5K5UTK

 *命名空间：ShopWebGisMongoDB.MongoDBConfig

 *文件名：  MongoDBSetup

 *版本号：  V1.0.0.0

 *当前的用户域：LAPTOP-CC5K5UTK

 *创建人：  蔡显麒

 *创建时间：2022/5/17 16:27:17

 *描述：MongoDB配置

/************************************************************************************/

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace ShopWebGisMongoDB.MongoDBConfig
{
    public static class MongoDBSetup
    {
        public static void ShopWebGisMongoDBConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ShopWebGisDatabaseSettings>(configuration.GetSection(nameof(ShopWebGisDatabaseSettings)));
            services.AddSingleton<IShopWebGisDatabaseSettings>(sp =>
            sp.GetRequiredService<IOptions<ShopWebGisDatabaseSettings>>().Value);            
        }
    }
}
