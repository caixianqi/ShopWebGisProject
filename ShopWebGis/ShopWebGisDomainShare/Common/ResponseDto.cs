/************************************************************************************

 * Copyright (c) 2022 All Rights Reserved.

 * CLR版本： 4.0.30319.42000

 *机器名称：LAPTOP-CC5K5UTK

 *命名空间：ShopWebGisDomainShare.Common

 *文件名：  ResponseDto

 *版本号：  V1.0.0.0

 *当前的用户域：LAPTOP-CC5K5UTK

 *创建人：  蔡显麒

 *创建时间：2022/12/30 14:28:19

 *描述：

/************************************************************************************/

using System;
using System.Collections.Generic;
using System.Text;

namespace ShopWebGisDomainShare.Common
{
    public class ResponseDto<T>
    {
        /// <summary>
        /// 数据
        /// </summary>
        public T ResultData { get; set; }

        /// <summary>
        /// HTTP响应码
        /// </summary>
        public int ResultCode { get; set; }

        /// <summary>
        ///  错误信息
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// 是否调用成功
        /// </summary>
        public bool Success { get; set; }
    }
}
