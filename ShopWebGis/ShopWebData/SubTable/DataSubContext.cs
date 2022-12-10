/************************************************************************************

 * Copyright (c) 2022 All Rights Reserved.

 * CLR版本： 4.0.30319.42000

 *机器名称：LAPTOP-CC5K5UTK

 *命名空间：ShopWebData.SubTable

 *文件名：  DataSubContext

 *版本号：  V1.0.0.0

 *当前的用户域：LAPTOP-CC5K5UTK

 *创建人：  蔡显麒

 *创建时间：2022/11/25 11:33:49

 *描述：

/************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace ShopWebData.SubTable
{
    public class DataSubContext<T>
    {
        public DataSubContext(string tableName, string subRoute, Type subRouteType, Expression<Func<T, bool>> expression, IExtensionExpression extension)
        {
            DefaultName = tableName;
            SubRoute = subRoute;
            SubRouteType = subRouteType;
            Expression = expression;
            Extension = extension;
        }

        /// <summary>
        /// 默认表名
        /// </summary>
        public string DefaultName { get; set; }

        /// <summary>
        /// 分表字段名称
        /// </summary>
        public string SubRoute { get; set; }

        /// <summary>
        /// 分表字段类型
        /// </summary>
        public Type SubRouteType { get; set; }

        /// <summary>
        /// 查询Lambda表达式
        /// </summary>
        public Expression<Func<T, bool>> Expression { get; set; }

        /// <summary>
        /// 外部扩展条件
        /// </summary>
        public IExtensionExpression Extension { get; set; }
    }
}
