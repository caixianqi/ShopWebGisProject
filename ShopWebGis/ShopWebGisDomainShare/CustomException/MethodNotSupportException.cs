/************************************************************************************

 * Copyright (c) 2022 All Rights Reserved.

 * CLR版本： 4.0.30319.42000

 *机器名称：LAPTOP-CC5K5UTK

 *命名空间：ShopWebGisDomainShare.CustomException

 *文件名：  MethodNotSupportException

 *版本号：  V1.0.0.0

 *当前的用户域：LAPTOP-CC5K5UTK

 *创建人：  蔡显麒

 *创建时间：2022/11/28 11:13:43

 *描述：

/************************************************************************************/

using System;
using System.Collections.Generic;
using System.Text;

namespace ShopWebGisDomainShare.CustomException
{
    public class MethodNotSupportException: Exception
    {
        public MethodNotSupportException()
        {

        }
        public MethodNotSupportException(string msg):base(msg)
        {
                
        }

        public MethodNotSupportException(string msg, Exception innerExpression) : base(msg, innerExpression)
        {

        }
    }
}
