/************************************************************************************

 * Copyright (c) 2022 All Rights Reserved.

 * CLR版本： 4.0.30319.42000

 *机器名称：LAPTOP-CC5K5UTK

 *命名空间：ShopWebGisNacosProxy

 *文件名：  IInterceptor

 *版本号：  V1.0.0.0

 *当前的用户域：LAPTOP-CC5K5UTK

 *创建人： 智慧环保部-蔡显麒

 *创建时间：2022/7/15 17:32:35

 *描述：拦截器

/************************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopWebGisNacosProxy
{
    public interface IInterceptor
    {
        object Intercept(Invocation invocation);
    }
}
