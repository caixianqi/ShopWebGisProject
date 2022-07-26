/************************************************************************************

 * Copyright (c) 2022 All Rights Reserved.

 * CLR版本： 4.0.30319.42000

 *机器名称：LAPTOP-CC5K5UTK

 *命名空间：ShopWebGisFreeSql.Resolve

 *文件名：  SubRuleType

 *版本号：  V1.0.0.0

 *当前的用户域：LAPTOP-CC5K5UTK

 *创建人：  蔡显麒

 *创建时间：2022/5/24 16:24:17

 *描述：分表规则类型

/************************************************************************************/

using System;
using System.Collections.Generic;
using System.Text;

namespace ShopWebGisFreeSql.Resolve
{
    public class SubRuleType
    {

    }

    /// <summary>
    /// 时间分表
    /// </summary>
    public class DateSubRule
    {
        /// <summary>
        /// 后缀
        /// </summary>
        public string Suffix { get; set; }

        /// <summary>
        /// 开始时间(unix时间戳)
        /// </summary>
        public int StartTimeStamp { get; set; }

        /// <summary>
        /// 结束时间(unix时间戳)
        /// </summary>
        public int EndTimeStamp { get; set; }
    }

    /// <summary>
    /// 表达式树分表
    /// </summary>
    public class DateExpressionSubRule
    {
        public List<string> Expressions { get; set; }

        public List<DateSubRule> Rules { get; set; }
    }
}
