/************************************************************************************

 * Copyright (c) 2022 All Rights Reserved.

 * CLR版本： 4.0.30319.42000

 *机器名称：LAPTOP-CC5K5UTK

 *命名空间：ShopWebGisMicroService

 *文件名：  MicroServiceResigter

 *版本号：  V1.0.0.0

 *当前的用户域：LAPTOP-CC5K5UTK

 *创建人：  蔡显麒

 *创建时间：2022/12/30 11:32:52

 *描述：微服务动态类注册器

/************************************************************************************/

using ShopWebGisMicroService.DynamicCodeGen;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopWebGisMicroService
{
    public static class MicroServiceResigter
    {
        /// <summary>
        /// 程序集名称
        /// </summary>
        private static List<string> assemblyNames = new List<string>();
        /// <summary>
        /// using引用
        /// </summary>
        private static List<string> usingNames = new List<string>();


        /// <summary>
        /// 添加编译所需的程序集
        /// </summary>
        /// <param name="assemblyName"></param>
        public static void AppendAssembly(string assemblyName)
        {
            assemblyNames.Add(assemblyName);
        }

        /// <summary>
        /// 添加类所需的引用
        /// </summary>
        /// <param name="usingName"></param>
        public static void AppendUsing(string usingName)
        {
            usingNames.Add(usingName);
        }

        public static void RegisterTypes(Type[] types)
        {
            CodeGenerator codeGenerator = new CodeGenerator();
            codeGenerator.AssemblyNames.AddRange(assemblyNames);
            codeGenerator.UsingNames.AddRange(usingNames);
            foreach (var type in types)
            {

            }
        }
    }
}
