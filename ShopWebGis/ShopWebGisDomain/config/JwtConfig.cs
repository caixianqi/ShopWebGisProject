/************************************************************************************

 * Copyright (c) 2022 All Rights Reserved.

 * CLR版本： 4.0.30319.42000

 *机器名称：LAPTOP-CC5K5UTK

 *命名空间：ShopWebGisDomain.config

 *文件名：  JwtConfig

 *版本号：  V1.0.0.0

 *当前的用户域：LAPTOP-CC5K5UTK

 *创建人：  蔡显麒

 *创建时间：2022/5/9 15:06:05

 *描述：

/************************************************************************************/

using System;
using System.Collections.Generic;
using System.Text;

namespace ShopWebGisDomain.config
{
    public class Jwt
    {
        /// <summary>
        /// 发布者
        /// </summary>
        public string Issuer { get; set; }

        /// <summary>
        /// Key
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// AccessToken过期时间
        /// </summary>
        public int AccessTokenExpires { get; set; }

        /// <summary>
        /// RefreshToken过期时间
        /// </summary>
        public int RefreshTokenExpires { get; set; }
    }
}
