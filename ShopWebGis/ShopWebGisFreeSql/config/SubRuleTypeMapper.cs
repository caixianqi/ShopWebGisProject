/************************************************************************************

 * Copyright (c) 2022 All Rights Reserved.

 * CLR版本： 4.0.30319.42000

 *机器名称：LAPTOP-CC5K5UTK

 *命名空间：ShopWebGisFreeSql.config

 *文件名：  SubRuleTypeMapper

 *版本号：  V1.0.0.0

 *当前的用户域：LAPTOP-CC5K5UTK

 *创建人：  智慧环保部-蔡显麒

 *创建时间：2022/5/24 17:23:46

 *描述：SubRuleTyp映射

/************************************************************************************/

using ShopWebGisFreeSql.Resolve;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopWebGisFreeSql.config
{
    internal class SubRuleTypeMapper
    {
        public static readonly Dictionary<string, Type> SubRuleTypeMapperDic = new Dictionary<string, Type>()
        {
            {$"{typeof(DateSubRule).Namespace}.{typeof(DateSubRule).Name}",typeof(DateSubRule) },
            {$"{typeof(DateExpressionSubRule).Namespace}.{typeof(DateExpressionSubRule).Name}",typeof(DateExpressionSubRule) }

        };

    }
}
