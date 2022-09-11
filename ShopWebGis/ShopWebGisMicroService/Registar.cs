/************************************************************************************

 * Copyright (c) 2022 All Rights Reserved.

 * CLR版本： 4.0.30319.42000

 *机器名称：LAPTOP-CC5K5UTK

 *命名空间：ShopWebGisMicroService

 *文件名：  Registar

 *版本号：  V1.0.0.0

 *当前的用户域：LAPTOP-CC5K5UTK

 *创建人：  蔡显麒

 *创建时间：2022/9/9 17:03:47

 *描述：动态类注册

/************************************************************************************/

using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopWebGisMicroService
{
    public class Registar
    {
        public static void Register(IServiceCollection services, params Type[] types)
        {

        }
    }
}
