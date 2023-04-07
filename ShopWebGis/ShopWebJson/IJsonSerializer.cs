/************************************************************************************

 * Copyright (c) 2023 All Rights Reserved.

 * CLR版本： 4.0.30319.42000

 *机器名称：LAPTOP-CC5K5UTK

 *命名空间：ShopWebJson

 *文件名：  IJsonSerializer

 *版本号：  V1.0.0.0

 *当前的用户域：LAPTOP-CC5K5UTK

 *创建人： 蔡显麒

 *创建时间：2023/4/5 14:44:00

 *描述：序列化接口

/************************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopWebJson
{
    public interface IJsonSerializer
    {
        string Serialize(object obj, bool camelCase = true, bool indented = false);

        T Deserialize<T>(string jsonString, bool camelCase = true);
    }
}
