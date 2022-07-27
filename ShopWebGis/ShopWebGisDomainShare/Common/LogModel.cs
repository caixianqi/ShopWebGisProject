/************************************************************************************

 * Copyright (c) 2022 All Rights Reserved.

 * CLR版本： 4.0.30319.42000

 *机器名称：LAPTOP-CC5K5UTK

 *命名空间：ShopWebGisDomainShare.Common

 *文件名：  LogModel

 *版本号：  V1.0.0.0

 *当前的用户域：LAPTOP-CC5K5UTK

 *创建人：  蔡显麒

 *创建时间：2022/7/27 16:45:27

 *描述：日志传输类

/************************************************************************************/

using System;
using System.Collections.Generic;
using System.Text;

namespace ShopWebGisDomainShare.Common
{
    public class LogModel
    {
        /// <summary>
        /// 记录时间
        /// </summary>
        public DateTime LogDateTime { get; private set; } = DateTime.Now;

        /// <summary>
        /// 异常
        /// </summary>
        public Exception Exception { get; set; }

        /// <summary>
        /// 日志
        /// </summary>
        public object Msg { get; set; }

        /// <summary>
        /// 日志等级
        /// </summary>

        public string Level { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool Success { get; set; }
    }
}
