/************************************************************************************

 * Copyright (c) 2023 All Rights Reserved.

 * CLR版本： 4.0.30319.42000

 *机器名称：LAPTOP-CC5K5UTK

 *命名空间：ShopWebGisDomain.config

 *文件名：  SequentialGuidGeneratorOptions

 *版本号：  V1.0.0.0

 *当前的用户域：LAPTOP-CC5K5UTK

 *创建人：  蔡显麒

 *创建时间：2023/6/19 10:20:09

 *描述：SequentialGuidGenerator配置项

/************************************************************************************/

using ShopWebGisDomainShare.Const.Emun;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopWebGisDomain.config
{
    public class SequentialGuidGeneratorOptions
    {
        public SequentialGuidType? DefaultSequentialGuidType { get; set; }

        public SequentialGuidType GetDefaultSequentialGuidType()
        {
            return DefaultSequentialGuidType ?? SequentialGuidType.SequentialAtEnd;
        }
    }
}
