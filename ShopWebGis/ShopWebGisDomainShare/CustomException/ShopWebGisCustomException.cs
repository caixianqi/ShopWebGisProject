/************************************************************************************

 * Copyright (c) 2022 All Rights Reserved.

 * CLR版本： 4.0.30319.42000

 *机器名称：LAPTOP-CC5K5UTK

 *命名空间：ShopWebGisDomainShare.CustomException

 *文件名：  ShopWebGisCustomException

 *版本号：  V1.0.0.0

 *当前的用户域：LAPTOP-CC5K5UTK

 *创建人：  蔡显麒

 *创建时间：2022/5/9 16:22:19

 *描述：ShopWebGis自定义异常

/************************************************************************************/

using System;
using System.Collections.Generic;
using System.Text;

namespace ShopWebGisDomainShare.CustomException
{
    public class ShopWebGisCustomException:Exception
    {
        public ShopWebGisCustomException(string message):base(message)
        {
                
        }
    }
}
