/************************************************************************************

 * Copyright (c) 2022 All Rights Reserved.

 * CLR版本： 4.0.30319.42000

 *机器名称：LAPTOP-CC5K5UTK

 *命名空间：ShopWebData.SubTable

 *文件名：  TableSubRule

 *版本号：  V1.0.0.0

 *当前的用户域：LAPTOP-CC5K5UTK

 *创建人：  蔡显麒

 *创建时间：2022/11/25 10:48:36

 *描述：

/************************************************************************************/

using System;
using System.Collections.Generic;
using System.Text;

namespace ShopWebData.SubTable
{
    public class TableSubRule
    {
        /// <summary>
        /// 分表解析类
        /// </summary>
        public string SubRuleType { get; set; }

        /// <summary>
        /// 分表字段
        /// </summary>
        public string SubRoute { get; set; }

        public List<DateRuleContent> DateRules { get; set; }

        public IList<DateExpressionRuleContent> DateExpressionRules { get; set; }
    }

    public class DateRuleContent
    {
        /// <summary>
        /// 后缀
        /// </summary>
        public string Suffix { get; set; }

        /// <summary>
        /// 起始时间戳
        /// </summary>
        public int StartTimeStamp { get; set; }

        /// <summary>
        /// 结束时间戳
        /// </summary>
        public int EndTimeStamp { get; set; }
    }

    public class DateExpressionRuleContent
    {
        public IList<string> Expressions { get; set; }

        public IList<DateRuleContent> Rules { get; set; }
    }
}
