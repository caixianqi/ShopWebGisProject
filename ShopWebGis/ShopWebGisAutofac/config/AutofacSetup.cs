/************************************************************************************

 * Copyright (c) 2022 All Rights Reserved.

 * CLR版本： 4.0.30319.42000

 *机器名称：LAPTOP-CC5K5UTK

 *命名空间：ShopWebGisDomainShare.Extension

 *文件名：  AutofacSetup

 *版本号：  V1.0.0.0

 *当前的用户域：LAPTOP-CC5K5UTK

 *创建人：  蔡显麒

 *创建时间：2022/5/5 23:09:05

 *描述：AutoFac 自动注册IOC容器

/************************************************************************************/

using Autofac;
using IRepository;
using IRepository.Base;
using Repository.Base;
using ShopWebCaching;
using ShopWebCaching.Caching;
using ShopWebGisApplication.Base;
using ShopWebGisApplicationContract.Base;
using ShopWebGisDomain.User;
using ShopWebGisMongoDB.Base;
using ShopWebGuids;
using ShopWebJson;
using ShopWebJson.Newtonsoft;
using ShopWebThread;
using ShopWebThread.Thread;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace ShopWebGisDomainShare.Extension
{
    /// <summary>
    ///  entityframeworkCore 仓储层
    /// </summary>
    public class RepositoryAutofacSetup : Autofac.Module
    {
        protected override void Load(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterGeneric(typeof(Repository<,>)).As(typeof(IRepository<,>)).InstancePerLifetimeScope().PropertiesAutowired();//注册仓储
            containerBuilder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope().PropertiesAutowired();
            //获取项目路径
            //var basePath = AppContext.BaseDirectory;

            //var RepositoryDllFile = Path.Combine(basePath, "Repository.dll");
            //var RepositoryServices = Assembly.LoadFile(RepositoryDllFile);//直接采用加载文件的方法 
            //containerBuilder.RegisterAssemblyTypes(RepositoryServices).PropertiesAutowired().Where(x => x.Name.EndsWith("")).AsImplementedInterfaces();
        }
    }

    /// <summary>
    /// MongoDB仓储层
    /// </summary>
    public class MongoDBRepositoryAutofacSetup : Autofac.Module
    {
        protected override void Load(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterGeneric(typeof(MongoDBRepository<,,>)).As(typeof(IMongoDBRepository<,,>)).InstancePerLifetimeScope();
        }
    }

    /// <summary>
    ///应用层注入
    /// </summary>
    public class ApplicationAutofacSetup : Autofac.Module
    {
        protected override void Load(ContainerBuilder containerBuilder)
        {
            var basePath = AppContext.BaseDirectory;

            var ApplicationFile = Path.Combine(basePath, "ShopWebGisApplication.dll");
            var ApplicationFileServices = Assembly.LoadFile(ApplicationFile);//直接采用加载文件的方法
            containerBuilder.RegisterAssemblyTypes(ApplicationFileServices).PublicOnly().PropertiesAutowired().AsImplementedInterfaces().InstancePerDependency();
            containerBuilder.RegisterGeneric(typeof(CrudApplication<,,>)).PropertiesAutowired().As(typeof(ICrudApplication<,,>));

        }
    }

    /// <summary>
    /// Redis应用层注入
    /// </summary>
    public class RedisCacheAutofacSetup : Autofac.Module
    {
        protected override void Load(ContainerBuilder containerBuilder)
        {
            var basePath = AppContext.BaseDirectory;

            var ApplicationFile = Path.Combine(basePath, "ShopWebGisRedis.dll");
            var ApplicationFileServices = Assembly.LoadFile(ApplicationFile);//直接采用加载文件的方法
            containerBuilder.RegisterAssemblyTypes(ApplicationFileServices).AsImplementedInterfaces().SingleInstance();
        }
    }

    public class NetJsonAutofacSetup : Autofac.Module
    {
        protected override void Load(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterType<NewtonsoftJsonSerializer>().As<IJsonSerializer>().PropertiesAutowired().InstancePerDependency();
        }
    }

    public class CachingAutofacSetup : Autofac.Module
    {
        protected override void Load(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterType<DistributedCacheKeyNormalizer>().As<IDistributedCacheKeyNormalizer>();
            containerBuilder.RegisterType<DistributedCacheSerializer>().As<IDistributedCacheSerializer>();
            containerBuilder.RegisterType<CommonDistributedCache>().As<ICommonDistributedCache>().PropertiesAutowired().SingleInstance();
        }
    }

    public class GuidAutofacSetup : Autofac.Module
    {
        protected override void Load(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterType<SequentialGuidGenerator>().As<IGuidGenerator>();
        }

    }

    public class ThreadAutofacSetup : Autofac.Module
    {
        protected override void Load(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterType<HttpContextCancellationTokenProvider>().As<ICancellationTokenProvider>();
        }
    }

}
