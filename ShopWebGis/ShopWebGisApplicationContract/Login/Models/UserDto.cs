/************************************************************************************

 * Copyright (c) 2022 All Rights Reserved.

 * CLR版本： 4.0.30319.42000

 *机器名称：LAPTOP-CC5K5UTK

 *命名空间：ShopWebGisApplicationContract.Login.Models

 *文件名：  UserDto

 *版本号：  V1.0.0.0

 *当前的用户域：LAPTOP-CC5K5UTK

 *创建人：  智慧环保部-蔡显麒

 *创建时间：2022/5/9 16:08:52

 *描述：用户DTO

/************************************************************************************/

using System;
using System.Collections.Generic;
using System.Text;

namespace ShopWebGisApplicationContract.Login.Models
{
    public class UserDto
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string UserPassword { get; set; }

        public string UserPhone { get; set; }

        public string Province { get; set; }

        public string City { get; set; }

        public string County { get; set; }

        public string AddressDetail { get; set; }
    }
}
