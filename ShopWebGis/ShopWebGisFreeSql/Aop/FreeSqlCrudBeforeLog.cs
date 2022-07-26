/************************************************************************************

 * Copyright (c) 2022 All Rights Reserved.

 * CLR版本： 4.0.30319.42000

 *机器名称：LAPTOP-CC5K5UTK

 *命名空间：ShopWebGisFreeSql.Aop

 *文件名：  FreeSqlCrudBefore

 *版本号：  V1.0.0.0

 *当前的用户域：LAPTOP-CC5K5UTK

 *创建人：  蔡显麒

 *创建时间：2022/6/17 15:04:39

 *描述：FreeSql CrudBefore （操作前AOP记录日志）

/************************************************************************************/

using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace ShopWebGisFreeSql.Aop
{
    public class FreeSqlCrudBeforeLog : IDisposable
    {
        public static AsyncLocal<FreeSqlCrudBeforeLog> Current = new AsyncLocal<FreeSqlCrudBeforeLog>();
        public StringBuilder Sb { get; } = new StringBuilder();

        public ILogger<FreeSqlCrudBeforeLog> _logger;

        public FreeSqlCrudBeforeLog(ILogger<FreeSqlCrudBeforeLog> logger)
        {
            Current.Value = this;
            _logger = logger;
        }

        public void Dispose()
        {
            Sb.Clear();
            Current.Value = null;
        }
    }
}
