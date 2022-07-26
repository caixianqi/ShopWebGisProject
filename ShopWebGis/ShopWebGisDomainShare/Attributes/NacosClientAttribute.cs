/************************************************************************************

 * Copyright (c) 2022 All Rights Reserved.

 * CLR版本： 4.0.30319.42000

 *机器名称：LAPTOP-CC5K5UTK

 *命名空间：ShopWebGisDomainShare.Attribute

 *文件名：  NacosClientAttribute

 *版本号：  V1.0.0.0

 *当前的用户域：LAPTOP-CC5K5UTK

 *创建人：  蔡显麒

 *创建时间：2022/7/15 15:20:58

 *描述：

/************************************************************************************/

using System;
using System.Collections.Generic;
using System.Text;

namespace ShopWebGisDomainShare.Attributes
{
    [AttributeUsage(AttributeTargets.Interface)]
    public class NacosClientAttribute : Attribute
    {
        /// <summary>
        /// nacos服务名
        /// </summary>
        public string Name { get; set; }
    }
}
