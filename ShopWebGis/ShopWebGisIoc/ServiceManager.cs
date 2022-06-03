/************************************************************************************

 * Copyright (c) 2022 All Rights Reserved.

 * CLR版本： 4.0.30319.42000

 *机器名称：LAPTOP-CC5K5UTK

 *命名空间：ShopWebGisIoc

 *文件名：  ServiceManager

 *版本号：  V1.0.0.0

 *当前的用户域：LAPTOP-CC5K5UTK

 *创建人：  智慧环保部-蔡显麒

 *创建时间：2022/5/23 10:09:34

 *描述： ServiceManager构建一个单例，用于寄放IServiceProvider

/************************************************************************************/

using System;
using System.Collections.Generic;
using System.Text;

namespace ShopWebGisIoc
{
    public class ServiceManager
    {
        public static IServiceProvider ServiceProvider { get; private set; }

        public static void Init(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }
    }
}
