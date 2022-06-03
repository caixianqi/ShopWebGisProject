/************************************************************************************

 * Copyright (c) 2022 All Rights Reserved.

 * CLR版本： 4.0.30319.42000

 *机器名称：LAPTOP-CC5K5UTK

 *命名空间：ShopWebGisFreeSql.Resolve

 *文件名：  ExpressionResult

 *版本号：  V1.0.0.0

 *当前的用户域：LAPTOP-CC5K5UTK

 *创建人：  智慧环保部-蔡显麒

 *创建时间：2022/5/31 10:53:21

 *描述：解析expression结果

/************************************************************************************/

using System;
using System.Collections.Generic;
using System.Text;

namespace ShopWebGisFreeSql.Resolve
{
    public class ExpressionResult
    {
        /// <summary>
        /// 参数
        /// </summary>
        public string parameter { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        public object value { get; set; }

        /// <summary>
        /// 操作符
        /// </summary>
        public string expressionOperator { get; set; }
    }
}
