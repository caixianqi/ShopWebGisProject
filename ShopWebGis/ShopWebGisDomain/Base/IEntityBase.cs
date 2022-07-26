/************************************************************************************

 * Copyright (c) 2022 All Rights Reserved.

 * CLR版本： 4.0.30319.42000

 *机器名称：LAPTOP-CC5K5UTK

 *命名空间：ShopWebGisDomain.Base

 *文件名：  IEntityBase

 *版本号：  V1.0.0.0

 *当前的用户域：LAPTOP-CC5K5UTK

 *创建人： 蔡显麒

 *创建时间：2022/5/1 21:51:45

 *描述：基础EntityBase接口

/************************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopWebGisDomain.Base
{
    public interface IEntityBase<T>
    {
        public T Id { get; }
    }
}
