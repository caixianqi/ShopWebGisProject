/************************************************************************************

 * Copyright (c) 2022 All Rights Reserved.

 * CLR版本： 4.0.30319.42000

 *机器名称：LAPTOP-CC5K5UTK

 *命名空间：ShopWebData.SubTable

 *文件名：  SubRuleResolver

 *版本号：  V1.0.0.0

 *当前的用户域：LAPTOP-CC5K5UTK

 *创建人：  蔡显麒

 *创建时间：2022/11/25 11:37:22

 *描述：

/************************************************************************************/

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using ShopWebData.SubTable.SubRule;
using ShopWebGisLogger.Factory;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ShopWebData.SubTable
{
    public sealed class SubRuleResolver : ISubRuleResolver
    {
        public static Type[] executingTypes = Assembly.GetExecutingAssembly().GetTypes();// 获取此运行下的类
        private readonly TableSubRuleOptions TableSubRuleOptions;
        private readonly IServiceProvider _serviceProvider;
        public SubRuleResolver(IOptions<TableSubRuleOptions> options, IServiceProvider serviceProvider)
        {
            TableSubRuleOptions = options.Value;
            _serviceProvider = serviceProvider;
        }

        public SubRuleContext Resolve<T>() where T : class
        {
            var type = typeof(T);
            return Resolve(type);
        }

        public SubRuleContext Resolve(Type type)
        {
            if (TableSubRuleOptions == null || TableSubRuleOptions.GetOrDefault(type.FullName) == null) throw new Exception("未设置分表规则!");
            var tableSubRule = TableSubRuleOptions.GetOrDefault(type.FullName);
            if (string.IsNullOrEmpty(tableSubRule.SubRoute)) throw new Exception("未设置分表字段!");
            if (string.IsNullOrEmpty(tableSubRule.SubRuleType)) throw new Exception("未设置分表解析类!");

            var subRouteProperty = type.GetProperty(tableSubRule.SubRoute);

            if (subRouteProperty == null) throw new Exception($"{type.FullName}类不包含设置的分表字段{tableSubRule.SubRoute}!");

            var basicTableName = type.Name;
            var tableAttribute = type.GetCustomAttributes(typeof(TableAttribute), true).FirstOrDefault();
            if (tableAttribute != null) basicTableName = ((TableAttribute)tableAttribute).Name;

            var subRuleType = executingTypes.FirstOrDefault(x => x.FullName == tableSubRule.SubRuleType);
            if (subRuleType == null) throw new Exception($"未找到分表规则解析类{tableSubRule.SubRuleType}");



            var iSubRule = (ISubRule)_serviceProvider.GetRequiredService(subRuleType);

            return new SubRuleContext(basicTableName, tableSubRule.SubRoute, subRouteProperty.PropertyType, iSubRule);
        }
    }
}
