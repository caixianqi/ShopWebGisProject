/************************************************************************************

 * Copyright (c) 2022 All Rights Reserved.

 * CLR版本： 4.0.30319.42000

 *机器名称：LAPTOP-CC5K5UTK

 *命名空间：ShopWebGisDomain.Base

 *文件名：  EntityBase

 *版本号：  V1.0.0.0

 *当前的用户域：LAPTOP-CC5K5UTK

 *创建人：  智慧环保部-蔡显麒

 *创建时间：2022/5/1 21:53:28

 *描述：EntityBase基础抽象类

/************************************************************************************/

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ShopWebGisDomain.Base
{
    public abstract class EntityBase<T> : IEntityBase<T>, ISoftDelete
    {
        public EntityBase(T Id)
        {
            this.Id = Id;
        }

        [Required]
        [Column("id")]
        public virtual T Id { get; protected set; }

        [Required]
        [Column("issoftdelete")]
        [Comment("是否删除")]
        /// <summary>
        /// 是否删除
        /// </summary>
        public bool isSoftDelete { get; set; } = false;

        [Comment("创建用户Id")]
        [Column("createuserid")]
        /// <summary>
        /// 创建用户Id
        /// </summary>
        public string CreateUserId { get; set; }

        [Comment("创建用户名称")]
        [Column("createusername")]
        /// <summary>
        /// 创建用户名称
        /// </summary>
        public string CreateUserName { get; set; }

        [Comment("更新操作用户名称")]
        [Column("updateusername")]
        /// <summary>
        /// 更新操作用户
        /// </summary>
        public string UpdateUserName { get; set; }

        [Comment("更新操作用户Id")]
        [Column("updateuserid")]
        /// <summary>
        /// 更新操作用户Id
        /// </summary>
        public string UpdateUserId { get; set; }
    }
}
