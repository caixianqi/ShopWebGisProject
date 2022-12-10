/************************************************************************************

 * Copyright (c) 2022 All Rights Reserved.

 * CLR版本： 4.0.30319.42000

 *机器名称：LAPTOP-CC5K5UTK

 *命名空间：ShopWebData.SubTable

 *文件名：  TableSubRuleOptions

 *版本号：  V1.0.0.0

 *当前的用户域：LAPTOP-CC5K5UTK

 *创建人：  蔡显麒

 *创建时间：2022/11/25 11:48:16

 *描述：

/************************************************************************************/

using System;
using System.Collections.Generic;
using System.Text;

namespace ShopWebData.SubTable
{
    /// <summary>
    /// 分表配置选项
    /// </summary>
    public class TableSubRuleOptions : Dictionary<string, TableSubRule>
    {
        /// <summary>
        /// 获取分表规则
        /// </summary>
        /// <param name="entryType">查询类型的全类名</param>
        /// <returns></returns>
        public TableSubRule GetOrDefault(string entryType)
        {
            if (TryGetValue(entryType, out var tableSubRule))
            {
                return tableSubRule;
            }
            return default;
        }
    }
}
