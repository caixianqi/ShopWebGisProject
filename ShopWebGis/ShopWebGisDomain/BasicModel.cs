/************************************************************************************

 * Copyright (c) 2022 All Rights Reserved.

 * CLR版本： 4.0.30319.42000

 *机器名称：LAPTOP-CC5K5UTK

 *命名空间：ShopWebGisDomain

 *文件名：  BasicModel

 *版本号：  V1.0.0.0

 *当前的用户域：LAPTOP-CC5K5UTK

 *创建人：  智慧环保部-蔡显麒

 *创建时间：2022/5/5 21:26:18

 *描述：基础实体类

/************************************************************************************/

using Microsoft.EntityFrameworkCore;
using ShopWebGisDomain.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ShopWebGisDomain
{
    public abstract class BasicModel<T> : EntityBase<T>
    {
        protected BasicModel(T Id) : base(Id)
        {
        }

        /// <summary>
        /// 创建时间
        /// </summary>
        [Column("createtime")]
        [Comment("创建时间")]
        public DateTime? CreateTime { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        [Column("updatetime")]
        [Comment("修改时间")]
        public DateTime? UpdateTime { get; set; }

    }
}
