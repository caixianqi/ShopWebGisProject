/************************************************************************************

 * Copyright (c) 2022 All Rights Reserved.

 * CLR版本： 4.0.30319.42000

 *机器名称：LAPTOP-CC5K5UTK

 *命名空间：ShopWebGisFreeSql.InterFace

 *文件名：  ISubRule

 *版本号：  V1.0.0.0

 *当前的用户域：LAPTOP-CC5K5UTK

 *创建人： 蔡显麒

 *创建时间：2022/5/20 17:31:39

 *描述：分表规则接口

/************************************************************************************/
using ShopWebGisFreeSql.Resolve;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopWebGisFreeSql.InterFace
{
    public interface ISubRule
    {
       /// <summary>
       /// 获取操作的表
       /// </summary>
       /// <typeparam name="T"></typeparam>
       /// <param name="context"></param>
       /// <returns></returns>
        IList<string> GetTables<T>(DataSubContext<T> context);
    }
}
