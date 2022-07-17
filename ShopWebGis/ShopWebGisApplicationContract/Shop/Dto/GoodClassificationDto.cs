/************************************************************************************

 * Copyright (c) 2022 All Rights Reserved.

 * CLR版本： 4.0.30319.42000

 *机器名称：LAPTOP-CC5K5UTK

 *命名空间：ShopWebGisApplicationContract.Shop.Dto

 *文件名：  GoodClassificationDto

 *版本号：  V1.0.0.0

 *当前的用户域：LAPTOP-CC5K5UTK

 *创建人：  智慧环保部-蔡显麒

 *创建时间：2022/7/17 19:15:37

 *描述：商品分类

/************************************************************************************/

using System;
using System.Collections.Generic;
using System.Text;

namespace ShopWebGisApplicationContract.Shop.Dto
{
    public class GoodClassificationDto
    {
        public int Id { get; set; }
        /// <summary>
        /// 商品分类名称
        /// </summary>

        public string Name { get; set; }

        /// <summary>
        /// 父商品分类Id
        /// </summary>

        public int ParentId { get; set; }


        /// <summary>
        /// 排序
        /// </summary>

        public int Sort { get; set; }
    }
}
