/************************************************************************************

 * Copyright (c) 2022 All Rights Reserved.

 * CLR版本： 4.0.30319.42000

 *机器名称：LAPTOP-CC5K5UTK

 *命名空间：ShopWebGisApplicationContract.User

 *文件名：  IUser

 *版本号：  V1.0.0.0

 *当前的用户域：LAPTOP-CC5K5UTK

 *创建人： 智慧环保部-蔡显麒

 *创建时间：2022/6/14 10:57:14

 *描述：

/************************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopWebGisApplicationContract.User
{
    public interface IUser
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        string Id { get; }

        /// <summary>
        /// 用户名
        /// </summary>
        string Name { get; }
    }
}
