/************************************************************************************

 * Copyright (c) 2022 All Rights Reserved.

 * CLR版本： 4.0.30319.42000

 *机器名称：LAPTOP-CC5K5UTK

 *命名空间：ShopWebGisDomain.Base

 *文件名：  ISoftDelete

 *版本号：  V1.0.0.0

 *当前的用户域：LAPTOP-CC5K5UTK

 *创建人： 智慧环保部-蔡显麒

 *创建时间：2022/5/4 13:02:19

 *描述：软删除接口

/************************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopWebGisDomain.Base
{
   public interface ISoftDelete
    {
        public bool isSoftDelete { get; set; } 
    }
}
