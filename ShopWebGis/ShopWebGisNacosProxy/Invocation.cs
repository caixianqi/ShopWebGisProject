/************************************************************************************

 * Copyright (c) 2022 All Rights Reserved.

 * CLR版本： 4.0.30319.42000

 *机器名称：LAPTOP-CC5K5UTK

 *命名空间：ShopWebGisNacosProxy

 *文件名：  Invocation

 *版本号：  V1.0.0.0

 *当前的用户域：LAPTOP-CC5K5UTK

 *创建人：  智慧环保部-蔡显麒

 *创建时间：2022/7/15 17:35:14

 *描述：调度器

/************************************************************************************/

using System;
using System.Collections.Generic;
using System.Text;

namespace ShopWebGisNacosProxy
{
    public class Invocation
    {
        public object[] Parameter { get; set; }
        public Delegate DelegateMethod { get; set; }
        public object Proceed()
        {
            return this.DelegateMethod.DynamicInvoke(Parameter);
        }

    }
}
