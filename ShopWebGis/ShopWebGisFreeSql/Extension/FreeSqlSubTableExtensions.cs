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
using ShopWebGisFreeSql.InterFace;
using ShopWebGisFreeSql.Resolve;
using ShopWebGisIoc;
using System;
using System.Collections.Generic;
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
    }
}
