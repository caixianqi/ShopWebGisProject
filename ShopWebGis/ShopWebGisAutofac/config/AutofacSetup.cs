/************************************************************************************

 * Copyright (c) 2022 All Rights Reserved.

 * CLR版本： 4.0.30319.42000

 *机器名称：LAPTOP-CC5K5UTK

 *命名空间：ShopWebGisDomainShare.Extension

 *文件名：  AutofacSetup

 *版本号：  V1.0.0.0

 *当前的用户域：LAPTOP-CC5K5UTK

 *创建人：  智慧环保部-蔡显麒

 *创建时间：2022/5/5 23:09:05

 *描述：AutoFac 自动注册IOC容器

/************************************************************************************/

using Autofac;
using IRepository;
using Repository;
using ShopWebGisDomain.User;
using ShopWebGisMongoDB.Base;
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
            containerBuilder.RegisterGeneric(typeof(Repository<,>)).As(typeof(IRepository<,>)).InstancePerLifetimeScope();//注册仓储
            //获取项目路径
            var basePath = AppContext.BaseDirectory;

            var RepositoryDllFile = Path.Combine(basePath, "Repository.dll");
            var RepositoryServices = Assembly.LoadFile(RepositoryDllFile);//直接采用加载文件的方法
            containerBuilder.RegisterAssemblyTypes(RepositoryServices).AsImplementedInterfaces().InstancePerLifetimeScope();
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
            containerBuilder.RegisterAssemblyTypes(ApplicationFileServices).AsImplementedInterfaces().InstancePerLifetimeScope();
        }
    }
}
