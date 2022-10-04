/************************************************************************************

 * Copyright (c) 2022 All Rights Reserved.

 * CLR版本： 4.0.30319.42000

 *机器名称：LAPTOP-CC5K5UTK

 *命名空间：ShopWebGisMicroService.Interface

 *文件名：  Interceptor

 *版本号：  V1.0.0.0

 *当前的用户域：LAPTOP-CC5K5UTK

 *创建人： 蔡显麒

 *创建时间：2022/9/29 11:41:11

 *描述：定义一个测试接口

/************************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopWebGisMicroService.Interface
{
    public class Interceptor
    {
        public object Invoke(object @object, string method, object[] parameters)
        {
            Console.WriteLine(
              string.Format("Interceptor does something before invoke [{0}]...", method));

            var retObj = @object.GetType().GetMethod(method).Invoke(@object, parameters);

            Console.WriteLine(
              string.Format("Interceptor does something after invoke [{0}]...", method));
            return retObj;
        }
    }
}
