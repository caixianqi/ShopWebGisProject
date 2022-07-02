/************************************************************************************

 * Copyright (c) 2022 All Rights Reserved.

 * CLR版本： 4.0.30319.42000

 *机器名称：LAPTOP-CC5K5UTK

 *命名空间：ShopWebGisApplicationContract.User

 *文件名：  IRoleApplication

 *版本号：  V1.0.0.0

 *当前的用户域：LAPTOP-CC5K5UTK

 *创建人： 智慧环保部-蔡显麒

 *创建时间：2022/7/2 11:03:59

 *描述：角色应用

/************************************************************************************/
using ShopWebGisDomain.User;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShopWebGisApplicationContract.User
{
    public interface IRoleApplication
    {
        /// <summary>
        /// 获取角色
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        Task<List<RoleInfo>> GetRoleList(string name);
    }
}
