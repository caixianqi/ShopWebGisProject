/************************************************************************************

 * Copyright (c) 2023 All Rights Reserved.

 * CLR版本： 4.0.30319.42000

 *机器名称：LAPTOP-CC5K5UTK

 *命名空间：ShopWebGisDomainShare.Const.Emun

 *文件名：  SequentialGuidType

 *版本号：  V1.0.0.0

 *当前的用户域：LAPTOP-CC5K5UTK

 *创建人：  蔡显麒

 *创建时间：2023/6/19 10:20:41

 *描述：连续Guid类型枚举

/************************************************************************************/

using System;
using System.Collections.Generic;
using System.Text;

namespace ShopWebGisDomainShare.Const.Emun
{
    public enum SequentialGuidType
    {
        /// <summary>
        /// The GUID should be sequential when formatted using the
        /// <see cref="Guid.ToString()" /> method.
        /// </summary>
        SequentialAsString,

        /// <summary>
        /// The GUID should be sequential when formatted using the
        /// <see cref="Guid.ToByteArray" /> method.
        /// </summary>
        SequentialAsBinary,

        /// <summary>
        /// The sequential portion of the GUID should be located at the end
        /// of the Data4 block.
        /// </summary>
        SequentialAtEnd
    }
}
