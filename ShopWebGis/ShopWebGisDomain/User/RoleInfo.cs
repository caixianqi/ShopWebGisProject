/************************************************************************************

 * Copyright (c) 2022 All Rights Reserved.

 * CLR版本： 4.0.30319.42000

 *机器名称：LAPTOP-CC5K5UTK

 *命名空间：ShopWebGisDomain.User

 *文件名：  Role

 *版本号：  V1.0.0.0

 *当前的用户域：LAPTOP-CC5K5UTK

 *创建人：  蔡显麒

 *创建时间：2022/5/5 22:11:18

 *描述：角色

/************************************************************************************/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ShopWebGisDomain.User
{
    [Table("role")]
    [Description("角色")]
    public class RoleInfo : BasicModel<int>
    {
        public RoleInfo(int Id) : base(Id)
        {
            Menus = new List<MenuInfo>();
            Users = new List<UserInfo>();
        }

        /// <summary>
        /// 角色名称
        /// </summary>
        [Required]
        [StringLength(50)]
        [Column("rolename")]
        public string RoleName { get; set; }
        /// <summary>
        /// 角色描述
        /// </summary>
        [StringLength(1000)]
        public string RoleDescription { get; set; }

        [Required]
        /// <summary>
        /// 角色排序
        /// </summary>
        public int RoleShot { get; set; }

        /// <summary>
        /// 用户
        /// </summary>
        public List<UserInfo> Users { get; set; }

        /// <summary>
        /// 菜单
        /// </summary>
        public List<MenuInfo> Menus { get; set; }
    }
}
