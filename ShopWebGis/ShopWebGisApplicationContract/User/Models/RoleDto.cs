/************************************************************************************

 * Copyright (c) 2022 All Rights Reserved.

 * CLR版本： 4.0.30319.42000

 *机器名称：LAPTOP-CC5K5UTK

 *命名空间：ShopWebGisApplicationContract.User.Models

 *文件名：  RoleDto

 *版本号：  V1.0.0.0

 *当前的用户域：LAPTOP-CC5K5UTK

 *创建人：  蔡显麒

 *创建时间：2022/7/2 18:31:55

 *描述：角色DTO

/************************************************************************************/

using System;
using System.Collections.Generic;
using System.Text;

namespace ShopWebGisApplicationContract.User.Models
{
    public class RoleDto
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 角色名称
        /// </summary>
        public string RoleName { get; set; }

        /// <summary>
        /// 角色描述
        /// </summary>
        public string RoleDescription { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int RoleShot { get; set; }
    }
}
