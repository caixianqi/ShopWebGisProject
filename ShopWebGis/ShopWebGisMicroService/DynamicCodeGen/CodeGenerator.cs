/************************************************************************************

 * Copyright (c) 2022 All Rights Reserved.

 * CLR版本： 4.0.30319.42000

 *机器名称：LAPTOP-CC5K5UTK

 *命名空间：ShopWebGisMicroService.DynamicCodeGen

 *文件名：  CodeGenerator

 *版本号：  V1.0.0.0

 *当前的用户域：LAPTOP-CC5K5UTK

 *创建人：  蔡显麒

 *创建时间：2022/12/30 10:33:03

 *描述：动态类构造器

/************************************************************************************/

using System;
using System.Collections.Generic;
using System.Text;

namespace ShopWebGisMicroService.DynamicCodeGen
{
    public class CodeGenerator
    {
        DynamicCompiler compiler = new DynamicCompiler();
        public  List<string> AssemblyNames = new List<string>();
        public  List<string> UsingNames = new List<string>();
    }
}
