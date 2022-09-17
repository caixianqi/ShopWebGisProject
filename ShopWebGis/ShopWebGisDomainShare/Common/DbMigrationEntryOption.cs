/************************************************************************************

 * Copyright (c) 2022 All Rights Reserved.

 * CLR版本： 4.0.30319.42000

 *机器名称：LAPTOP-CC5K5UTK

 *命名空间：ShopWebGisDomainShare.Common

 *文件名：  EntryOption

 *版本号：  V1.0.0.0

 *当前的用户域：LAPTOP-CC5K5UTK

 *创建人：  蔡显麒

 *创建时间：2022/9/16 11:39:30

 *描述：实体配置类型

/************************************************************************************/

using System;
using System.Collections.Generic;
using System.Text;

namespace ShopWebGisDomainShare.Common
{
    public class DbMigrationEntryOption
    {
        /// <summary>
        /// 实体类型数组
        /// </summary>
        public List<Type> types;

        /// <summary>
        /// 连接字符串
        /// </summary>
        public string ConnectString;

        public DbMigrationEntryOption()
        {
            types = new List<Type>();
        }
        /// <summary>
        /// 配置实体
        /// </summary>
        /// <param name="type"></param>
        public void entry<T>()
        {
            if (!types.Contains(typeof(T)))
            {
                types.Add(typeof(T));
            }
        }
    }
}
