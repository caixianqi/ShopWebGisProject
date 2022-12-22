/************************************************************************************

 * Copyright (c) 2022 All Rights Reserved.

 * CLR版本： 4.0.30319.42000

 *机器名称：LAPTOP-CC5K5UTK

 *命名空间：ShopWebGisFreeSql.Extension

 *文件名：  FreeSqlExtensions

 *版本号：  V1.0.0.0

 *当前的用户域：LAPTOP-CC5K5UTK

 *创建人：  蔡显麒

 *创建时间：2022/12/22 13:07:42

 *描述：

/************************************************************************************/


using ShopWebGisDomainShare.Attributes;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace ShopWebGisFreeSql.Extension
{
    public static class FreeSqlExtensions
    {
        public static void AddCustomAttributesAop(this IFreeSql freeSql)
        {
            // 小数类型精度
            freeSql.Aop.ConfigEntityProperty += (s, e) =>
            {
                var attr = e.Property.GetCustomAttribute<DecimalPrecisionAttribute>();
                if (attr != null)
                {
                    e.ModifyResult.Precision = attr.Precision;
                    e.ModifyResult.Scale = attr.Scale;
                }
            };

            // 索引
            freeSql.Aop.ConfigEntity += (s, e) =>
            {
                var indexes = e.EntityType.GetCustomAttributes<IndexAttribute>();
                foreach (var index in indexes)
                {
                    var fields = string.Join(",", index.Fields);
                    var name = string.IsNullOrEmpty(index.Name) ? $"{string.Join("_", index.Fields).ToLower()}_index" : index.Name;
                    e.ModifyIndexResult.Add(new FreeSql.DataAnnotations.IndexAttribute(name, fields, index.Unique));
                }
            };
        }
    }
}
