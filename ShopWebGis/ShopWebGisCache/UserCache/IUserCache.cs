/************************************************************************************

 * Copyright (c) 2022 All Rights Reserved.

 * CLR版本： 4.0.30319.42000

 *机器名称：LAPTOP-CC5K5UTK

 *命名空间：ShopWebGisCache.UserCache

 *文件名：  IUserCache

 *版本号：  V1.0.0.0

 *当前的用户域：LAPTOP-CC5K5UTK

 *创建人： 智慧环保部-蔡显麒

 *创建时间：2022/6/22 11:46:04

 *描述：User-Cache接口

/************************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopWebGisCache.UserCache
{
    public interface IUserCache
    {
        /// <summary>
        /// 限制登录次数
        /// </summary>
        void LimitLoginTimes();
    }
}
