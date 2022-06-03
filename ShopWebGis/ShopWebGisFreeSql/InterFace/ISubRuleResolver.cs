/************************************************************************************

 * Copyright (c) 2022 All Rights Reserved.

 * CLR版本： 4.0.30319.42000

 *机器名称：LAPTOP-CC5K5UTK

 *命名空间：ShopWebGisFreeSql.InterFace

 *文件名：  ISubRuleResolver

 *版本号：  V1.0.0.0

 *当前的用户域：LAPTOP-CC5K5UTK

 *创建人： 智慧环保部-蔡显麒

 *创建时间：2022/5/20 17:21:51

 *描述：分表解析处理接口

/************************************************************************************/
using ShopWebGisFreeSql.Resolve;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopWebGisFreeSql.InterFace
{
    public interface ISubRuleResolver
    {
        //
        // 摘要:
        //     解析当前类型的分表规则
        //
        // 类型参数:
        //   T:
        SubRuleContext Resolve<T>() where T : class;

        //
        // 摘要:
        //     解析当前类型的分表规则
        //
        // 参数:
        //   type:
        SubRuleContext Resolve(Type type);

        bool GetReturn();
    }
}
