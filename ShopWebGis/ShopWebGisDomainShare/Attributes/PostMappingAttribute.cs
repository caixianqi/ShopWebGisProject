/************************************************************************************

 * Copyright (c) 2022 All Rights Reserved.

 * CLR版本： 4.0.30319.42000

 *机器名称：LAPTOP-CC5K5UTK

 *命名空间：ShopWebGisDomainShare.Attributes

 *文件名：  PostMappingAttribute

 *版本号：  V1.0.0.0

 *当前的用户域：LAPTOP-CC5K5UTK

 *创建人：  蔡显麒

 *创建时间：2022/9/9 17:14:24

 *描述：Post请求特性

/************************************************************************************/

using System;
using System.Collections.Generic;
using System.Text;

namespace ShopWebGisDomainShare.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class PostMappingAttribute : MappingAttribute
    {

    }
}
