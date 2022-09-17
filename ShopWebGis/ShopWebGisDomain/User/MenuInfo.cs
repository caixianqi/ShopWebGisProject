/************************************************************************************

 * Copyright (c) 2022 All Rights Reserved.

 * CLR版本： 4.0.30319.42000

 *机器名称：LAPTOP-CC5K5UTK

 *命名空间：ShopWebGisDomain.User

 *文件名：  MenuInfo

 *版本号：  V1.0.0.0

 *当前的用户域：LAPTOP-CC5K5UTK

 *创建人：  蔡显麒

 *创建时间：2022/6/29 22:04:23

 *描述：菜单实体

/************************************************************************************/

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ShopWebGisDomain.User
{
    [Table("menu")]
    [Comment("菜单")]
    [Index(nameof(Url), Name = "Index_Url")]
    public class MenuInfo : BasicModel<int>
    {
        public MenuInfo(int Id) : base(Id)
        {
            Roles = new List<RoleInfo>();
        }

        [Required]
        [StringLength(50)]
        [Column("name")]
        [Comment("菜单名")]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        [Column("parentId")]
        [Comment("父菜单Id")]

        public string ParentId { get; set; }

        [Required]
        [Column("sort")]
        [Comment("排序")]
        public int Sort { get; set; }

        [Required]
        [Column("url")]
        [Comment("Url 对应前端路由")]
        public string Url { get; set; }

        [Column("icon")]
        [Comment("菜单图标")]
        public string IconClass { get; set; }

        /// <summary>
        /// 角色
        /// </summary>
        public List<RoleInfo> Roles { get; set; }
    }
}
