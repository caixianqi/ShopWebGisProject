/************************************************************************************

 * Copyright (c) 2023 All Rights Reserved.

 * CLR版本： 4.0.30319.42000

 *机器名称：LAPTOP-CC5K5UTK

 *命名空间：ShopWebGisDomain.Models

 *文件名：  PickOption

 *版本号：  V1.0.0.0

 *当前的用户域：LAPTOP-CC5K5UTK

 *创建人：  蔡显麒

 *创建时间：2023/6/5 22:35:04

 *描述：枚举类型选项

/************************************************************************************/

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ShopWebGisDomain.Models
{
    [Table("pickoption")]
    [Comment("枚举")]
    [Index(nameof(Code), IsUnique = true)]
    public class PickOption : BasicModel<int>
    {
        [Column("name")]
        [Required]
        /// <summary>
        /// 枚举选项名称
        /// </summary>
        public string Name { get; set; }

        [Column("name")]
        [MaxLength(20)]
        [Required]
        /// <summary>
        /// 枚举选项Code
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 枚举描述
        /// </summary>
        [Column("description")]
        [MaxLength(120)]
        [Required]
        public string Description { get; set; }


    }
}
