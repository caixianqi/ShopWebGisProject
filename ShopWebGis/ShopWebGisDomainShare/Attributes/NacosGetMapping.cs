/************************************************************************************

 * Copyright (c) 2022 All Rights Reserved.

 * CLR版本： 4.0.30319.42000

 *机器名称：LAPTOP-CC5K5UTK

 *命名空间：ShopWebGisDomainShare.Attributes

 *文件名：  NacosGetMapping

 *版本号：  V1.0.0.0

 *当前的用户域：LAPTOP-CC5K5UTK

 *创建人：  蔡显麒

 *创建时间：2022/7/15 15:25:13

 *描述：

/************************************************************************************/

using System;
using System.Collections.Generic;
using System.Text;

namespace ShopWebGisDomainShare.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class NacosGetMapping : Attribute
    {
        /// <summary>
        /// 访问接口路径Url
        /// </summary>
        public string Route { get; set; }
    }
}
