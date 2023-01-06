/************************************************************************************

 * Copyright (c) 2022 All Rights Reserved.

 * CLR版本： 4.0.30319.42000

 *机器名称：LAPTOP-CC5K5UTK

 *命名空间：ShopWebGisFreeSql.InterFace

 *文件名：  IFreesqlSession

 *版本号：  V1.0.0.0

 *当前的用户域：LAPTOP-CC5K5UTK

 *创建人： 蔡显麒

 *创建时间：2022/9/15 15:47:21

 *描述：IFreesqlSession接口

/************************************************************************************/
using FreeSql;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopWebGisFreeSql.InterFace
{
    public interface IFreesqlSession : IDisposable
    {
        /// <summary>
        /// 获取Ifreesql
        /// </summary>
        /// <param name="connectString">数据库连接名</param>
        /// <returns></returns>
        IFreeSql Get(string connectStringName);
    }
}
