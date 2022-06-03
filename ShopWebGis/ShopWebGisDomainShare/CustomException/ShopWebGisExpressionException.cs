/************************************************************************************

 * Copyright (c) 2022 All Rights Reserved.

 * CLR版本： 4.0.30319.42000

 *机器名称：LAPTOP-CC5K5UTK

 *命名空间：ShopWebGisDomainShare.CustomException

 *文件名：  ShopWebGisExpressionException

 *版本号：  V1.0.0.0

 *当前的用户域：LAPTOP-CC5K5UTK

 *创建人：  智慧环保部-蔡显麒

 *创建时间：2022/5/30 16:48:56

 *描述：ShopWebGis表达树异常

/************************************************************************************/

using System;
using System.Collections.Generic;
using System.Text;

namespace ShopWebGisDomainShare.CustomException
{
    public class ShopWebGisExpressionException : Exception
    {
        public ShopWebGisExpressionException(string msg) : base(msg)
        {

        }
    }
}
