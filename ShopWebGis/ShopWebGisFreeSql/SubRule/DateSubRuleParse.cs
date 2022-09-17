/************************************************************************************

 * Copyright (c) 2022 All Rights Reserved.

 * CLR版本： 4.0.30319.42000

 *机器名称：LAPTOP-CC5K5UTK

 *命名空间：ShopWebGisFreeSql.SubRule

 *文件名：  DateSubRuleResolve

 *版本号：  V1.0.0.0

 *当前的用户域：LAPTOP-CC5K5UTK

 *创建人：  蔡显麒

 *创建时间：2022/5/24 20:57:32

 *描述：时间分表解析器

/************************************************************************************/

using Microsoft.Extensions.Configuration;
using ShopWebGisDomainShare.Extension;
using ShopWebGisFreeSql.AbstarctClass;
using ShopWebGisFreeSql.InterFace;
using ShopWebGisFreeSql.Resolve;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace ShopWebGisFreeSql.SubRule
{
    public class DateSubRuleParse : SubRuleAbstarctClass, ISubRule
    {
        public IList<string> GetDefaultAllTables<T>(DataSubContext<T> context)
        {
            IList<string> tables = new List<string>();
            return tables;
        }

        public IList<string> GetTables<T>(DataSubContext<T> context)
        {
            IList<string> tables = new List<string>();
            var t = typeof(T);
            var fullName = $"{t.Namespace}.{t.Name}";
            var domainClassSection = SubSetting._configuration.GetSection(fullName);// 获取实体命名空间以及类名
            var defaultTableName = GetDefaultName<T>();
            List<DateSubRule> dateSubRules = (List<DateSubRule>)domainClassSection.GetSection($"{typeof(DateSubRule).Name}s").Get(typeof(List<DateSubRule>));
            ConditionBuilderVisitor visitor = new ConditionBuilderVisitor();
            visitor.Visit(context.Expression);

            return tables;
        }
    }
}
