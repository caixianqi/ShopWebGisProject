/************************************************************************************

 * Copyright (c) 2022 All Rights Reserved.

 * CLR版本： 4.0.30319.42000

 *机器名称：LAPTOP-CC5K5UTK

 *命名空间：ShopWebGisEntityFrameWorkCore.EntityFrameWorkCore

 *文件名：  ShopWebGisDbContextModelCreatingExtension

 *版本号：  V1.0.0.0

 *当前的用户域：LAPTOP-CC5K5UTK

 *创建人：  蔡显麒

 *创建时间：2022/4/29 14:46:56

 *描述：EntityFrameWork 实体创建DBSet

/************************************************************************************/

using Microsoft.EntityFrameworkCore;
using ShopWebGisDomain.Shop;
using ShopWebGisDomain.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopWebGisEntityFrameWorkCore.EntityFrameWorkCore
{
    public static class ShopWebGisDbContextModelCreatingExtension
    {
        /// <summary>
        /// 配置实体
        /// </summary>
        /// <param name="modelBuilder"></param>
        public static void ConfigureShopWebGisEntity(this ModelBuilder modelBuilder)
        {
        }
    }
}
