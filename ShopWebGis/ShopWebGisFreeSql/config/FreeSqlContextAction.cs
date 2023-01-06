/************************************************************************************

 * Copyright (c) 2023 All Rights Reserved.

 * CLR版本： 4.0.30319.42000

 *机器名称：LAPTOP-CC5K5UTK

 *命名空间：ShopWebGisFreeSql.config

 *文件名：  FreeSqlContextAction

 *版本号：  V1.0.0.0

 *当前的用户域：LAPTOP-CC5K5UTK

 *创建人：  蔡显麒

 *创建时间：2023/1/4 14:49:25

 *描述：

/************************************************************************************/

using FreeSql;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopWebGisFreeSql.config
{
    public class FreeSqlContextAction
    {
        public DataType DataType { private get; set; }

        public FreeSqlBuilder freeSqlBuilder;

        public FreeSqlContextAction(string ConnectStringName)
        {
            freeSqlBuilder = new FreeSqlBuilder().UseConnectionString(DataType, ConnectStringName);
        }
    }
}
