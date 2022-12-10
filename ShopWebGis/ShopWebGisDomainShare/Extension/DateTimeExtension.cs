/************************************************************************************

 * Copyright (c) 2022 All Rights Reserved.

 * CLR版本： 4.0.30319.42000

 *机器名称：LAPTOP-CC5K5UTK

 *命名空间：ShopWebGisDomainShare.Extension

 *文件名：  DateTimeExtension

 *版本号：  V1.0.0.0

 *当前的用户域：LAPTOP-CC5K5UTK

 *创建人：  蔡显麒

 *创建时间：2022/11/28 11:29:28

 *描述：

/************************************************************************************/

using System;
using System.Collections.Generic;
using System.Text;

namespace ShopWebGisDomainShare.Extension
{
    public static class DateTimeExtension
    {
        public static int ToTimeStamp(this DateTime dateTime)
        {
            if (dateTime == null || dateTime == DateTime.MinValue) return -1;

            DateTime Jan1st1970 = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return (int)(dateTime.AddHours(-8) - Jan1st1970).TotalSeconds;
        }
    }
}
