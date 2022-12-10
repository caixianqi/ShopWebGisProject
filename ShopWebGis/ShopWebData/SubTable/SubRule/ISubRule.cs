/************************************************************************************

 * Copyright (c) 2022 All Rights Reserved.

 * CLR版本： 4.0.30319.42000

 *机器名称：LAPTOP-CC5K5UTK

 *命名空间：ShopWebData.SubTable.SubRule

 *文件名：  ISubRule

 *版本号：  V1.0.0.0

 *当前的用户域：LAPTOP-CC5K5UTK

 *创建人： 蔡显麒

 *创建时间：2022/11/25 14:54:53

 *描述：

/************************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopWebData.SubTable.SubRule
{
    public interface ISubRule
    {
        /// <summary>
        /// 根据分表规则解析当前操作的表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="context"></param>
        /// <returns></returns>
        IList<string> GetTables<T>(DataSubContext<T> context);

        /// <summary>
        /// 根据配置获取所有的表名
        /// </summary>
        /// <returns></returns>
        IList<string> GetDefaultTables(Type type, string defaultName);
    }
}
