/************************************************************************************

 * Copyright (c) 2023 All Rights Reserved.

 * CLR版本： 4.0.30319.42000

 *机器名称：LAPTOP-CC5K5UTK

 *命名空间：ShopWebGisDomain.Models

 *文件名：  DataDictionaryItem

 *版本号：  V1.0.0.0

 *当前的用户域：LAPTOP-CC5K5UTK

 *创建人：  蔡显麒

 *创建时间：2023/6/4 21:54:15

 *描述：数据字典

/************************************************************************************/

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ShopWebGisDomain.Models
{
    [Table("datadictionary")]
    [Comment("数据字典")]
    [Index(nameof(Code), Name = "Code_Unique_Index", IsUnique = true)]
    public class DataDictionary : BasicModel<int>
    {
        [Column("name")]
        [Required]
        /// <summary>
        /// 字段名称
        /// </summary>
        public string Name { get; set; }

        [Column("code")]
        [Required]
        /// <summary>
        /// 字段编码
        /// </summary>
        public string Code { get; set; }

        [Column("description")]
        [Required]
        /// <summary>
        /// 字段描述
        /// </summary>
        public string Description { get; set; }

        [Required]
        [Column("rank")]
        [Comment("排序")]
        [DefaultValue(0)]
        public int Rank { get; set; }


    }
}
