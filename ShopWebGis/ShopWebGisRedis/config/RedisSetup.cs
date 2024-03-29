/************************************************************************************

 * Copyright (c) 2022 All Rights Reserved.

 * CLR版本： 4.0.30319.42000

 *机器名称：LAPTOP-CC5K5UTK

 *命名空间：ShopWebGisRedis.config

 *文件名：  RedisConfig

 *版本号：  V1.0.0.0

 *当前的用户域：LAPTOP-CC5K5UTK

 *创建人：  蔡显麒

 *创建时间：2022/6/22 10:58:15

 *描述：Redis配置

/************************************************************************************/

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ShopWebGisCache;
using ShopWebGisDomain.config;

namespace ShopWebGisRedis.config
{
    public static class RedisSetup
    {
        public static void ShopWebGisRedisSetup(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IShopWebGisCache, ShopWebGisRedisDataBase>();
        }
    }
}
