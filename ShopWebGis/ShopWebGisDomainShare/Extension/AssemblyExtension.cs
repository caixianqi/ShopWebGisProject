/************************************************************************************

 * Copyright (c) 2023 All Rights Reserved.

 * CLR版本： 4.0.30319.42000

 *机器名称：LAPTOP-CC5K5UTK

 *命名空间：ShopWebGisDomainShare.Extension

 *文件名：  AssemblyExtension

 *版本号：  V1.0.0.0

 *当前的用户域：LAPTOP-CC5K5UTK

 *创建人：  蔡显麒

 *创建时间：2023/4/7 9:24:57

 *描述：程序集扩展方法

/************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ShopWebGisDomainShare.Extension
{
    public static class AssemblyExtension
    {
        /// <summary>
        /// 获取程序集下继承指定Type的类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="assembly">程序集</param>
        /// <param name="args">构造参数</param>
        /// <returns>返回指定的泛型类</returns>
        public static IEnumerable<T> GetInheritTypes<T>(this Assembly assembly, params object[] args) where T : class
        {
            return assembly.GetModules().SelectMany(module => module.GetTypes()).Where(type => type.IsSubclassOf(typeof(T))).Select(x => (T)Activator.CreateInstance(x, args));
        }
    }
}
