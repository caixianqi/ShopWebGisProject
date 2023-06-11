/************************************************************************************

 * Copyright (c) 2023 All Rights Reserved.

 * CLR版本： 4.0.30319.42000

 *机器名称：LAPTOP-CC5K5UTK

 *命名空间：ShopWebGisDomain.Models

 *文件名：  PickSelect

 *版本号：  V1.0.0.0

 *当前的用户域：LAPTOP-CC5K5UTK

 *创建人：  蔡显麒

 *创建时间：2023/6/5 22:33:38

 *描述：枚举相关选项值

/************************************************************************************/

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ShopWebGisDomain.Models
{
    [Table("pickselect")]
    [Comment("枚举选项值")]
    [Index(nameof(Value), nameof(PickOptionId), IsUnique = true)]
    public class PickSelect : BasicModel<int>
    {
        [Column("name")]
        [MaxLength(30)]
        [Required]
        /// <summary>
        /// 选项名称
        /// </summary>
        public string Name { get; set; }

        [Column("value")]
        [Required]
        /// <summary>
        /// 选项值
        /// </summary>
        public int Value { get; set; }

        [Column("pickOptionid")]
        [Required]
        /// <summary>
        /// 枚举Id
        /// </summary>
        public int PickOptionId { get; set; }
    }
}
