/************************************************************************************

 * Copyright (c) 2022 All Rights Reserved.

 * CLR版本： 4.0.30319.42000

 *机器名称：LAPTOP-CC5K5UTK

 *命名空间：ShopWebGisFreeSql.Extension

 *文件名：  FreeSqlSubTableExtensions

 *版本号：  V1.0.0.0

 *当前的用户域：LAPTOP-CC5K5UTK

 *创建人：  蔡显麒

 *创建时间：2022/5/23 9:40:13

 *描述：Freesql扩展方法

/************************************************************************************/

using FreeSql;
using Microsoft.Extensions.DependencyInjection;
using ShopWebData.SubTable;
using ShopWebGisDomainShare.Utils;
using ShopWebGisFreeSql.InterFace;
using ShopWebGisIoc;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace ShopWebGisFreeSql.Extension
{
    public static class FreeSqlSubTableExtensions
    {
        public static ISelect<T> SubTableSelect<T>(this ISelect<T> select, Expression<Func<T, bool>> exp, IExtensionExpression extension = null) where T : class
        {
            ISubRuleResolver requiredService = ServiceManager.ServiceProvider.GetRequiredService<ISubRuleResolver>();
            SubRuleContext subRuleContext = requiredService.Resolve<T>();
            DataSubContext<T> context = new DataSubContext<T>(subRuleContext.DefaultName, subRuleContext.SubRoute, subRuleContext.SubRouteType, exp, extension);
            IList<string> tables = subRuleContext.SubRule.GetTables(context);
            foreach (string tableName in tables)
            {
                select.AsTable((Type type, string name) => tableName);
            }

            return select.Where(exp);
        }

        /// <summary>
        /// 分表插入, 根据分表规则寻找对于的表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="freesql"></param>
        /// <param name="source">插入的数据</param>
        public static IInsert<T> SubTableInsert<T>(this IFreeSql freeSql, [NotNull] T source, IExtensionExpression extension = null)
            where T : class
        {
            var ruleResolver = ServiceManager.ServiceProvider.GetRequiredService<ISubRuleResolver>();
            var ruleContext = ruleResolver.Resolve<T>();

            var insert = freeSql.Insert(source);

            var tableName = DefineTableName(source, ruleContext, extension);
            if (string.IsNullOrEmpty(tableName)) throw new Exception("存在分表字段错误!无法确定表名,插入失败!");
            return insert.AsTable(name => tableName);
        }

        /// <summary>
        /// 分表插入, 根据分表规则寻找对于的表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="freesql"></param>
        /// <param name="source">插入的数据集合</param>
        public static int SubTableInsertMany<T>(this IFreeSql freeSql, [NotNull] IEnumerable<T> source, IExtensionExpression extension = null)
            where T : class
        {
            if (!source.Any()) return 0;

            var ruleResolver = ServiceManager.ServiceProvider.GetRequiredService<ISubRuleResolver>();
            var ruleContext = ruleResolver.Resolve<T>();

            var grouped = source.GroupBy(d => DefineTableName(d, ruleContext, extension));
            if (grouped.Where(g => string.IsNullOrEmpty(g.Key)).Any()) throw new Exception("存在分表字段错误!无法确定表名,插入失败!");

            var count = 0;

            //使用 全局线程事务
            freeSql.Transaction(() =>
            {
                foreach (var item in grouped)
                {
                    count += freeSql.Insert(item.ToList()).AsTable(name => item.Key).ExecuteAffrows();
                }
            });
            return count;
        }

        /// <summary>
        /// 根据条件，更新相应的数据
        /// lambda表达式条件中必须包含分表字段以确定需要操作的表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="update"></param>
        /// <param name="exp"></param>
        /// <returns></returns>
        public static int SubTableUpdate<T>(this IFreeSql freeSql, Expression<Func<T, bool>> exp, Action<IUpdate<T>> action, IExtensionExpression extension = null) where T : class
        {
            var ruleResolver = ServiceManager.ServiceProvider.GetRequiredService<ISubRuleResolver>();
            var ruleContext = ruleResolver.Resolve<T>();

            var context = new DataSubContext<T>(ruleContext.DefaultName, ruleContext.SubRoute, ruleContext.SubRouteType, exp, extension);
            var tableNames = ruleContext.SubRule.GetTables(context);

            var count = 0;
            freeSql.Transaction(() => {
                foreach (var tableName in tableNames)
                {
                    var update = freeSql.Update<T>().AsTable(name => tableName).Where(exp);
                    action?.Invoke(update);
                    count += update.ExecuteAffrows();
                }
            });
            return count;
        }

        /// <summary>
        /// 根据条件，删除相应的数据
        /// lambda表达式条件中必须包含分表字段以确定需要操作的表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="delete"></param>
        /// <param name="exp"></param>
        /// <returns></returns>
        public static int SubTableDelete<T>(this IFreeSql freeSql, Expression<Func<T, bool>> exp, Action<IDelete<T>> action = null, IExtensionExpression extension = null) where T : class
        {
            var ruleResolver = ServiceManager.ServiceProvider.GetRequiredService<ISubRuleResolver>();
            var ruleContext = ruleResolver.Resolve<T>();

            var context = new DataSubContext<T>(ruleContext.DefaultName, ruleContext.SubRoute, ruleContext.SubRouteType, exp, extension);
            var tableNames = ruleContext.SubRule.GetTables(context);

            var count = 0;
            freeSql.Transaction(() => {
                foreach (var tableName in tableNames)
                {
                    var delete = freeSql.Delete<T>().Where(exp).AsTable(name => tableName);
                    action?.Invoke(delete);
                    count += delete.ExecuteAffrows();
                }
            });
            return count;
        }

        /// <summary>
        /// 根据当前数据和分表规则，确定表名
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="ruleContext"></param>
        /// <returns></returns>
        public static string DefineTableName<T>([NotNull] T source, [NotNull] SubRuleContext ruleContext, IExtensionExpression extension)
        {
            // 构建用于确定分表表名的表达式
            var subRoute = typeof(T).GetProperty(ruleContext.SubRoute);
            var value = subRoute.GetValue(source);
            if (value == null) throw new Exception("无法获取分表字段的值!无法确定对应表名!");

            var exp = LambdaUtils.CreateEqual<T>(ruleContext.SubRoute, value, ruleContext.SubRouteType);

            //确定表名
            var context = new DataSubContext<T>(ruleContext.DefaultName, ruleContext.SubRoute, ruleContext.SubRouteType, exp, extension);
            var tableNames = ruleContext.SubRule.GetTables(context);

            return tableNames.FirstOrDefault();
        }
    }
}
