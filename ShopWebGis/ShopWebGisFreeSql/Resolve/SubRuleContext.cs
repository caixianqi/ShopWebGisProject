/************************************************************************************

 * Copyright (c) 2022 All Rights Reserved.

 * CLR版本： 4.0.30319.42000

 *机器名称：LAPTOP-CC5K5UTK

 *命名空间：ShopWebGisFreeSql.Resolve

 *文件名：  SubRuleContext

 *版本号：  V1.0.0.0

 *当前的用户域：LAPTOP-CC5K5UTK

 *创建人：  智慧环保部-蔡显麒

 *创建时间：2022/5/20 17:23:20

 *描述：分表规则上下文

/************************************************************************************/

using ShopWebGisFreeSql.InterFace;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopWebGisFreeSql.Resolve
{
    public class SubRuleContext
    {
        public string DefaultName
        {
            get;
            set;
        }

        public string SubRoute
        {
            get;
            set;
        }

        public Type SubRouteType
        {
            get;
            set;
        }

        public ISubRule SubRule
        {
            get;
            set;
        }

        public SubRuleContext(string tableName, string subRoute, Type subRouteType, ISubRule subRule)
        {
            DefaultName = tableName;
            SubRoute = subRoute;
            SubRouteType = subRouteType;
            SubRule = subRule;
        }
    }
}
