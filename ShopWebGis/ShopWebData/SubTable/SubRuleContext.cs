/************************************************************************************

 * Copyright (c) 2022 All Rights Reserved.

 * CLR版本： 4.0.30319.42000

 *机器名称：LAPTOP-CC5K5UTK

 *命名空间：ShopWebData.SubTable

 *文件名：  SubRuleContext

 *版本号：  V1.0.0.0

 *当前的用户域：LAPTOP-CC5K5UTK

 *创建人：  蔡显麒

 *创建时间：2022/11/25 10:56:12

 *描述：

/************************************************************************************/

using ShopWebData.SubTable.SubRule;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopWebData.SubTable
{
    public class SubRuleContext
    {
        public SubRuleContext(string tableName, string subRoute, Type subRouteType, ISubRule subRule)
        {
            DefaultName = tableName;
            SubRoute = subRoute;
            SubRouteType = subRouteType;
            SubRule = subRule;
        }

        public string DefaultName { get; set; }

        public string SubRoute { get; set; }

        public Type SubRouteType { get; set; }

        public ISubRule SubRule { get; set; }
    }
}
