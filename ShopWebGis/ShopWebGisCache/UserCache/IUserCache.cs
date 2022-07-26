/************************************************************************************

 * Copyright (c) 2022 All Rights Reserved.

 * CLR版本： 4.0.30319.42000

 *机器名称：LAPTOP-CC5K5UTK

 *命名空间：ShopWebGisCache.UserCache

 *文件名：  IUserCache

 *版本号：  V1.0.0.0

 *当前的用户域：LAPTOP-CC5K5UTK

 *创建人： 蔡显麒

 *创建时间：2022/6/22 11:46:04

 *描述：User-Cache接口

/************************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShopWebGisCache.UserCache
{
    public interface IUserCache
    {
        /// <summary>
        /// 限制登录次数
        /// </summary>
        ///  <param name="isMatch">是否用户名密码正确</param>
        /// <param name="userName">用户名</param>
        Task LimitLoginTimes(string userName);

        /// <summary>
        /// 用户是否冻结
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        Task<bool> UserIsFreeze(string userName);
    }
}
