/************************************************************************************

 * Copyright (c) 2022 All Rights Reserved.

 * CLR版本： 4.0.30319.42000

 *机器名称：LAPTOP-CC5K5UTK

 *命名空间：ShopWebGisFreeSql.Extension

 *文件名：  FreeSqlBuilderExtensions

 *版本号：  V1.0.0.0

 *当前的用户域：LAPTOP-CC5K5UTK

 *创建人：  蔡显麒

 *创建时间：2022/9/15 15:18:59

 *描述：FreeSqlBuilder扩展类

/************************************************************************************/

using FreeSql;
using Microsoft.Extensions.Logging;
using ShopWebGisElasticSearch;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace ShopWebGisFreeSql.Extension
{
    public static class FreeSqlBuilderExtensions
    {
        /// <summary>
        ///  配置freesql日志记录
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="factory">日志接口</param>
        /// <returns></returns>
        public static FreeSqlBuilder UseLogger(this FreeSqlBuilder builder, IGisLogger logger)
        {
            return builder.UseMonitorCommand(delegate (DbCommand cmd)
            {
                logger.LogInfo($"Sql:{cmd.CommandText}");
            });
        }
    }
}
