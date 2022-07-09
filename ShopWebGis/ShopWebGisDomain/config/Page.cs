/************************************************************************************

 * Copyright (c) 2022 All Rights Reserved.

 * CLR版本： 4.0.30319.42000

 *机器名称：LAPTOP-CC5K5UTK

 *命名空间：ShopWebGisDomain.config

 *文件名：  Page

 *版本号：  V1.0.0.0

 *当前的用户域：LAPTOP-CC5K5UTK

 *创建人：  智慧环保部-蔡显麒

 *创建时间：2022/7/7 22:56:20

 *描述：

/************************************************************************************/

using System;
using System.Collections.Generic;
using System.Text;

namespace ShopWebGisDomain.config
{

    public class Page<T>
    {
        /// <summary>
        /// 当前页
        /// </summary>
        public int PageIndex { get; set; }
        /// <summary>
        /// 总页数
        /// </summary>
        public int TotalPages { get; set; }
        /// <summary>
        /// 集合总数
        /// </summary>
        public int TotalRows { get; set; }
        /// <summary>
        /// 每页项数
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// 集合
        /// </summary>
        public IList<T> LsList { get; set; }

    }
}
