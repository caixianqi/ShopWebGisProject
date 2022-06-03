/************************************************************************************

 * Copyright (c) 2022 All Rights Reserved.

 * CLR版本： 4.0.30319.42000

 *机器名称：LAPTOP-CC5K5UTK

 *命名空间：ShopWebGisEntityFrameWorkCore.EntityConfiguration

 *文件名：  PrivatePropertyEntityConfiguration

 *版本号：  V1.0.0.0

 *当前的用户域：LAPTOP-CC5K5UTK

 *创建人：  智慧环保部-蔡显麒

 *创建时间：2022/4/29 15:17:01

 *描述：私有属性映射

/************************************************************************************/

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace ShopWebGisEntityFrameWorkCore.EntityConfiguration
{
    public class PrivatePropertyEntityConfiguration<T> : IEntityTypeConfiguration<T> where T : class
    {
        public void Configure(EntityTypeBuilder<T> builder)
        {
            var nonPublicProperties = builder.Metadata.ClrType.GetProperties(BindingFlags.NonPublic | BindingFlags.Instance);
            foreach (var p in nonPublicProperties)
            {
                builder.Property(p.Name);
            }
        }
    }
}
