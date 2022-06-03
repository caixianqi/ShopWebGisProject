/************************************************************************************

 * Copyright (c) 2022 All Rights Reserved.

 * CLR版本： 4.0.30319.42000

 *机器名称：LAPTOP-CC5K5UTK

 *命名空间：ShopWebGisFreeSql.Resolve

 *文件名：  DataSubContext

 *版本号：  V1.0.0.0

 *当前的用户域：LAPTOP-CC5K5UTK

 *创建人：  智慧环保部-蔡显麒

 *创建时间：2022/5/20 17:25:00

 *描述：分表解析数据上下文

/************************************************************************************/

using ShopWebGisFreeSql.InterFace;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace ShopWebGisFreeSql.Resolve
{
    public class DataSubContext<T>
    {        //
        // 摘要:
        //     默认表名
        public string DefaultName
        {
            get;
            set;
        }

        //
        // 摘要:
        //     分表字段名称
        public string SubRoute
        {
            get;
            set;
        }

        //
        // 摘要:
        //     分表字段类型
        public Type SubRouteType
        {
            get;
            set;
        }

        //
        // 摘要:
        //     查询Lambda表达式
        public Expression<Func<T, bool>> Expression
        {
            get;
            set;
        }

        //
        // 摘要:
        //     外部扩展条件
        public IExtensionExpression Extension
        {
            get;
            set;
        }

        public DataSubContext(string tableName, string subRoute, Type subRouteType, Expression<Func<T, bool>> expression, IExtensionExpression extension)
        {
            DefaultName = tableName;
            SubRoute = subRoute;
            SubRouteType = subRouteType;
            Expression = expression;
            Extension = extension;
        }
    }
}
