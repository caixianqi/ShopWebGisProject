/************************************************************************************

 * Copyright (c) 2022 All Rights Reserved.

 * CLR版本： 4.0.30319.42000

 *机器名称：LAPTOP-CC5K5UTK

 *命名空间：ShopWebGisDomain.Shop

 *文件名：  Good

 *版本号：  V1.0.0.0

 *当前的用户域：LAPTOP-CC5K5UTK

 *创建人：  蔡显麒

 *创建时间：2022/7/17 16:29:40

 *描述：商品分类实体

/************************************************************************************/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ShopWebGisDomain.Shop
{
    [Table("goodClassification")]
    [Description("商品分类")]
    public class GoodClassification : BasicModel<int>
    {
        public GoodClassification(int Id) : base(Id)
        {

        }

        /// <summary>
        /// 商品分类名称
        /// </summary>
        [Required]
        [Column("name")]
        public string Name { get; set; }

        /// <summary>
        /// 父商品分类Id
        /// </summary>
        [Required]
        [Column("parentId")]
        public int ParentId { get; set; }
            
        /// <summary>
        /// 排序
        /// </summary>
        [Required]
        [Column("sort")]
        public int Sort { get; set; }
    }
}
