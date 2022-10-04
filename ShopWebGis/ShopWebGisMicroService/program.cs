/************************************************************************************

 * Copyright (c) 2022 All Rights Reserved.

 * CLR版本： 4.0.30319.42000

 *机器名称：LAPTOP-CC5K5UTK

 *命名空间：ShopWebGisMicroService

 *文件名：  program

 *版本号：  V1.0.0.0

 *当前的用户域：LAPTOP-CC5K5UTK

 *创建人：  蔡显麒

 *创建时间：2022/9/29 14:16:31

 *描述：

/************************************************************************************/

using ShopWebGisMicroService.DynamicCodeGen;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopWebGisMicroService
{
    public class program
    {
        static void Main(string[] args)
        {
            var command = Proxy.Of<Command>();
            command.Execute();

            Console.WriteLine("Hi, Dennis, great, we got the interceptor works.");
            Console.ReadLine();
        }

        public class Command
        {
            public virtual void Execute()
            {
                Console.WriteLine("Command executing...");
                Console.WriteLine("Hello Kitty!");
                Console.WriteLine("Command executed.");
            }
        }
    }
}
