/************************************************************************************

 * Copyright (c) 2023 All Rights Reserved.

 * CLR版本： 4.0.30319.42000

 *机器名称：LAPTOP-CC5K5UTK

 *命名空间：ShopWebGisFreeSql.config

 *文件名：  FreeSqlConnectionOptions

 *版本号：  V1.0.0.0

 *当前的用户域：LAPTOP-CC5K5UTK

 *创建人：  蔡显麒

 *创建时间：2023/1/4 14:36:10

 *描述：FreeSqlOption配置类

/************************************************************************************/

using FreeSql;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopWebGisFreeSql.config
{
    public class FreeSqlConnectionOptions
    {
        public Action<FreeSqlContextAction> DefacultFreeSqlAction { get; set; }
    }
}
