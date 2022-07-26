/************************************************************************************

 * Copyright (c) 2022 All Rights Reserved.

 * CLR版本： 4.0.30319.42000

 *机器名称：LAPTOP-CC5K5UTK

 *命名空间：ShopWebGisDomain.User

 *文件名：  UserInfo

 *版本号：  V1.0.0.0

 *当前的用户域：LAPTOP-CC5K5UTK

 *创建人：  蔡显麒

 *创建时间：2022/5/5 21:59:33

 *描述：用户实体

/************************************************************************************/

using Microsoft.EntityFrameworkCore;
using ShopWebGisDomain.ValueObject;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ShopWebGisDomain.User
{
    [Table("user")]
    [Comment("用户")]
    public class UserInfo : BasicModel<int>
    {
        public UserInfo(int Id) : base(Id)
        {
            Roles = new List<RoleInfo>();
        }
        [Required]
        [StringLength(25)]
        [Column("username")]
        [Comment("用户名")]
        /// <summary>
        ///  用户名
        /// </summary>
        public string UserName { get; set; }
        
        [Comment("用户密码")]
        [Required]
        [Column("userpassword")]
        /// <summary>
        /// 用户密码
        /// </summary>
        public string UserPassword { get; set; }

        [Comment("用户号码")]
        [StringLength(25)]
        [Required]
        [Column("userphone")]
        /// <summary>
        /// 用户号码
        /// </summary>
        public string UserPhone { get; set; }
      
        /// <summary>
        /// 用户地址
        /// </summary>
        
        public Address Address { get; set; }

        /// <summary>
        /// 角色
        /// </summary>
        public List<RoleInfo> Roles { get; set; }
    }
}
