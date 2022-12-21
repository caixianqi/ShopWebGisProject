/************************************************************************************

 * Copyright (c) 2022 All Rights Reserved.

 * CLR版本： 4.0.30319.42000

 *机器名称：LAPTOP-CC5K5UTK

 *命名空间：ShopWebGisDomainShare.Extension

 *文件名：  CommonEnumerableExtensions

 *版本号：  V1.0.0.0

 *当前的用户域：LAPTOP-CC5K5UTK

 *创建人：  蔡显麒

 *创建时间：2022/7/27 10:30:44

 *描述：EnumerableExtensions 扩展类

/************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShopWebGisDomainShare.Extension
{
    public static class CommonEnumerableExtensions
    {
        public static IEnumerable<T> WhereIf<T>(this IEnumerable<T> source, bool condition, Func<T, bool> predicate)
        {
            if (!condition)
            {
                return source;
            }

            return source.Where(predicate);
        }

        public static bool AddIfNotContains<T>(this ICollection<T> source, T item)
        {
            if (source.Contains(item))
            {
                return false;
            }
            source.Add(item);
            return true;
        }
    }
}
