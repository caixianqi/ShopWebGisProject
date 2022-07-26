/************************************************************************************

 * Copyright (c) 2022 All Rights Reserved.

 * CLR版本： 4.0.30319.42000

 *机器名称：LAPTOP-CC5K5UTK

 *命名空间：ShopWebGisEntityFrameWorkCore.Extension

 *文件名：  EntityFrameWorkCoreExtensions

 *版本号：  V1.0.0.0

 *当前的用户域：LAPTOP-CC5K5UTK

 *创建人：  蔡显麒

 *创建时间：2022/5/6 22:21:31

 *描述：

/************************************************************************************/

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ShopWebGisEntityFrameWorkCore.Extension
{
    public static class EntityFrameWorkCoreExtensions
    {
        public static void TryConfigureComment(this EntityTypeBuilder b)
        {
            Type clrType = b.Metadata.ClrType;
            CommentAttribute customAttribute = clrType.GetCustomAttribute<CommentAttribute>();
            if (customAttribute != null)
            {
                b.HasComment(customAttribute.Comment);
            }

            IEnumerable<PropertyInfo> enumerable = from p in clrType.GetProperties()
                                                   where p.IsDefined(typeof(CommentAttribute), inherit: false)
                                                   select p;
            foreach (PropertyInfo item in enumerable)
            {
                CommentAttribute customAttribute2 = item.GetCustomAttribute<CommentAttribute>();
                if (customAttribute2 != null)
                {
                    b.Property(item.Name).HasComment(customAttribute2.Comment);
                }
            }
        }
    }
}
