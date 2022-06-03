/************************************************************************************

 * Copyright (c) 2022 All Rights Reserved.

 * CLR版本： 4.0.30319.42000

 *机器名称：LAPTOP-CC5K5UTK

 *命名空间：ShopWebGisDomain.config

 *文件名：  JwtConfig

 *版本号：  V1.0.0.0

 *当前的用户域：LAPTOP-CC5K5UTK

 *创建人：  智慧环保部-蔡显麒

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
        public string Issuer { get; set; }

        public string Key { get; set; }

        public int Expires { get; set; }
    }
}
