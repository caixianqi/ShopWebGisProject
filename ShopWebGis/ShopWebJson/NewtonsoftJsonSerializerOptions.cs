/************************************************************************************

 * Copyright (c) 2023 All Rights Reserved.

 * CLR版本： 4.0.30319.42000

 *机器名称：LAPTOP-CC5K5UTK

 *命名空间：ShopWebJson

 *文件名：  NewtonsoftJsonSerializerOptions

 *版本号：  V1.0.0.0

 *当前的用户域：LAPTOP-CC5K5UTK

 *创建人：  蔡显麒

 *创建时间：2023/4/5 16:15:23

 *描述：

/************************************************************************************/

using ShopWebGisDomainShare.Collections;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace ShopWebJson
{
    public class NewtonsoftJsonSerializerOptions
    {
        public ITypeList<JsonConverter> Converters { get; }

        public NewtonsoftJsonSerializerOptions()
        {
            Converters = new TypeList<JsonConverter>();
        }
    }
}
