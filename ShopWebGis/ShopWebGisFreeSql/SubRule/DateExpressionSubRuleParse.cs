/************************************************************************************

 * Copyright (c) 2022 All Rights Reserved.

 * CLR版本： 4.0.30319.42000

 *机器名称：LAPTOP-CC5K5UTK

 *命名空间：ShopWebGisFreeSql.SubRule

 *文件名：  DateExpressionSubRuleResolve

 *版本号：  V1.0.0.0

 *当前的用户域：LAPTOP-CC5K5UTK

 *创建人：  蔡显麒

 *创建时间：2022/5/24 20:57:53

 *描述：时间表达树分表解析器

/************************************************************************************/

using ShopWebGisFreeSql.AbstarctClass;
using ShopWebGisFreeSql.InterFace;
using ShopWebGisFreeSql.Resolve;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopWebGisFreeSql.SubRule
{
    public class DateExpressionSubRuleParse : SubRuleAbstarctClass, ISubRule
    {
        public IList<string> GetDefaultAllTables<T>(DataSubContext<T> context)
        {
            IList<string> tables = new List<string>();
            return tables;
        }

        public IList<string> GetTables<T>(DataSubContext<T> context)
        {
            IList<string> tables = new List<string>();
            var defaultName = GetDefaultName<T>();
            return tables;
        }
    }
}
