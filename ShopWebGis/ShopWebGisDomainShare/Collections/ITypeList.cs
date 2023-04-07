/************************************************************************************

 * Copyright (c) 2023 All Rights Reserved.

 * CLR版本： 4.0.30319.42000

 *机器名称：LAPTOP-CC5K5UTK

 *命名空间：ShopWebCore.Collections

 *文件名：  ITypeList

 *版本号：  V1.0.0.0

 *当前的用户域：LAPTOP-CC5K5UTK

 *创建人： 蔡显麒

 *创建时间：2023/4/5 14:04:12

 *描述：Type类型List接口

/************************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopWebGisDomainShare.Collections
{
    public interface ITypeList<in BaseType> : IList<Type>
    {

        void Add<T>() where T : BaseType;


        bool TryAdd<T>() where T : BaseType;


        bool Contains<T>() where T : BaseType;


        void Remove<T>() where T : BaseType;
    }
}
