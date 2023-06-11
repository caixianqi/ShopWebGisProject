/************************************************************************************

 * Copyright (c) 2023 All Rights Reserved.

 * CLR版本： 4.0.30319.42000

 *机器名称：LAPTOP-CC5K5UTK

 *命名空间：ShopWebGisDomain.Models

 *文件名：  DataDictionaryItem

 *版本号：  V1.0.0.0

 *当前的用户域：LAPTOP-CC5K5UTK

 *创建人：  蔡显麒

 *创建时间：2023/6/4 23:12:41

 *描述：数据字典项

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
    [Table("datadictionaryitem")]
    [Comment("数据字典项")]
    [Index(nameof(Code),nameof(DataDictionaryId), Name = "Code_DataDictionaryId_Unique_Index", IsUnique = true)]
    public class DataDictionaryItem : BasicModel<int>
    {
        /// <summary>
        /// 名称
        /// </summary>
        [Column("name")]
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        [Column("code")]
        [Required]
        public string Code { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        [Column("value")]
        [Required]
        public string Value { get; set; }

        /// <summary>
        /// 是否启用(默认启用)
        /// </summary>
        [Column("isenabled")]
        [Required]
        [DefaultValue(true)]
        public bool IsEnabled { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        [Column("rank")]
        [Required]
        [DefaultValue(0)]
        public int Rank { get; set; }

        /// <summary>
        /// 数据字典关联Id
        /// </summary>
        [Column("datadictionaryid")]
        [Required]
        public int DataDictionaryId { get; set; }



    }
}
