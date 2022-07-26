/************************************************************************************

 * Copyright (c) 2022 All Rights Reserved.

 * CLR版本： 4.0.30319.42000

 *机器名称：LAPTOP-CC5K5UTK

 *命名空间：ShopWebGisApplicationContract.User.Models

 *文件名：  ComplexToken

 *版本号：  V1.0.0.0

 *当前的用户域：LAPTOP-CC5K5UTK

 *创建人：  蔡显麒

 *创建时间：2022/6/16 16:13:56

 *描述：混合Token（双Token）

/************************************************************************************/

using System;
using System.Collections.Generic;
using System.Text;

namespace ShopWebGisApplicationContract.User.Models
{
    public class ComplexToken
    {
        /// <summary>
        /// Token
        /// </summary>
        public string AccessToken { get; set; }
        
        /// <summary>
        /// 刷新Token
        /// </summary>
        public string RefreshToken { get; set; }
    }
}
