/************************************************************************************

 * Copyright (c) 2022 All Rights Reserved.

 * CLR版本： 4.0.30319.42000

 *机器名称：LAPTOP-CC5K5UTK

 *命名空间：ShopWebData.SubTable

 *文件名：  ISubRuleResolver

 *版本号：  V1.0.0.0

 *当前的用户域：LAPTOP-CC5K5UTK

 *创建人： 蔡显麒

 *创建时间：2022/11/25 10:55:33

 *描述：

/************************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopWebData.SubTable
{
    public interface ISubRuleResolver
    {
        /// <summary>
        /// 解析当前类型的分表规则
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        SubRuleContext Resolve<T>() where T : class;
    }
}
