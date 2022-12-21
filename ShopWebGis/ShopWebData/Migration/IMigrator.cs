/************************************************************************************

 * Copyright (c) 2022 All Rights Reserved.

 * CLR版本： 4.0.30319.42000

 *机器名称：LAPTOP-CC5K5UTK

 *命名空间：ShopWebData.Migration

 *文件名：  IMigrator

 *版本号：  V1.0.0.0

 *当前的用户域：LAPTOP-CC5K5UTK

 *创建人： 蔡显麒

 *创建时间：2022/12/12 16:32:58

 *描述：结构迁移接口

/************************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopWebData.Migration
{
    public interface IMigrator
    {
        void Migrate();
    }
}
