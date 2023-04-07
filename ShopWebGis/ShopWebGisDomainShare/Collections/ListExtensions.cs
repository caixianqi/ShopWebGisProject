/************************************************************************************

 * Copyright (c) 2023 All Rights Reserved.

 * CLR版本： 4.0.30319.42000

 *机器名称：LAPTOP-CC5K5UTK

 *命名空间：ShopWebGisDomainShare.Collections

 *文件名：  ListExtensions

 *版本号：  V1.0.0.0

 *当前的用户域：LAPTOP-CC5K5UTK

 *创建人：  蔡显麒

 *创建时间：2023/4/5 16:54:11

 *描述：List拓展方法

/************************************************************************************/

using System;
using System.Collections.Generic;
using System.Text;

namespace ShopWebGisDomainShare.Collections
{
    public static class ListExtensions
    {
        /// <summary>
        /// 根据起始索引位置插入列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="index"></param>
        /// <param name="items"></param>
        public static void InsertRange<T>(this IList<T> source, int index, IEnumerable<T> items)
        {
            foreach (var item in items)
            {
                source.Insert(index++, item);
            }
        }
    }
}
