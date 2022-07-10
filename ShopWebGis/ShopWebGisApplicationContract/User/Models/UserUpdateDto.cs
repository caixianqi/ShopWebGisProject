/************************************************************************************

 * Copyright (c) 2022 All Rights Reserved.

 * CLR版本： 4.0.30319.42000

 *机器名称：LAPTOP-CC5K5UTK

 *命名空间：ShopWebGisApplicationContract.User.Models

 *文件名：  UserUpdateDto

 *版本号：  V1.0.0.0

 *当前的用户域：LAPTOP-CC5K5UTK

 *创建人：  智慧环保部-蔡显麒

 *创建时间：2022/7/10 10:59:47

 *描述：账号密码修改DTO

/************************************************************************************/

using System;
using System.Collections.Generic;
using System.Text;

namespace ShopWebGisApplicationContract.User.Models
{
    public class UserUpdateDto : UserDto
    {
        public string OriginalPassword { get; set; }
    }
}
