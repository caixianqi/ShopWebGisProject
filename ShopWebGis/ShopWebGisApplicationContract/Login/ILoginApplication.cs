/************************************************************************************

 * Copyright (c) 2022 All Rights Reserved.

 * CLR版本： 4.0.30319.42000

 *机器名称：LAPTOP-CC5K5UTK

 *命名空间：ShopWebGisApplicationContract.Login

 *文件名：  ILogin

 *版本号：  V1.0.0.0

 *当前的用户域：LAPTOP-CC5K5UTK

 *创建人： 智慧环保部-蔡显麒

 *创建时间：2022/5/9 15:52:26

 *描述：登录，注册等接口sss

/************************************************************************************/
using ShopWebGisApplicationContract.Login.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopWebGisApplicationContract.Login
{
    public interface ILoginApplication
    {
        string ShopWebGisILogin(UserDto user);

        void ShopWebGisILogin();
    }
}
