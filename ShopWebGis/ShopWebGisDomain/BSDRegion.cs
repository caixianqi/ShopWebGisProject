/************************************************************************************

 * Copyright (c) 2022 All Rights Reserved.

 * CLR版本： 4.0.30319.42000

 *机器名称：LAPTOP-CC5K5UTK

 *命名空间：ShopWebGisDomain

 *文件名：  BSDRegion

 *版本号：  V1.0.0.0

 *当前的用户域：LAPTOP-CC5K5UTK

 *创建人：  蔡显麒

 *创建时间：2022/12/25 21:41:10

 *描述：省市区

/************************************************************************************/

using Microsoft.EntityFrameworkCore;
using ShopWebGisDomain.Base;
using ShopWebGisDomainShare.Const.Emun;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ShopWebGisDomain
{
    public class BSDRegion: BasicModel<int>
    {
        [Column("areacode")]
        [Required]
        [Comment("区域Code")]
        public int AreaCode { get; set; }

        [Column("areaname")]
        [Required]
        [Comment("区域名称")]
        public string AreaName { get; set; }

        [Column("parentid")]
        [Comment("父级")]
        public int ParendId { get; set; }

        [Column("level")]
        [Required]
        [Comment("区域划分等级")]
        public RegionLevelEmun Level { get; set; }

    }
}
