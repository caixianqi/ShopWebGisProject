/************************************************************************************

 * Copyright (c) 2022 All Rights Reserved.

 * CLR版本： 4.0.30319.42000

 *机器名称：LAPTOP-CC5K5UTK

 *命名空间：ShopWebGisApplication.Login

 *文件名：  LoginApplication

 *版本号：  V1.0.0.0

 *当前的用户域：LAPTOP-CC5K5UTK

 *创建人：  智慧环保部-蔡显麒

 *创建时间：2022/5/9 15:58:22

 *描述：登录应用

/************************************************************************************/

using ShopWebGisApplicationContract.Login;
using ShopWebGisApplicationContract.Login.Models;
using ShopWebGisFreeSql.InterFace;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopWebGisApplication.Login
{
    public class LoginApplication : ILoginApplication
    {
        private readonly ISubRuleResolver _subRuleResolver;
        public LoginApplication(ISubRuleResolver subRuleResolver)
        {
            _subRuleResolver = subRuleResolver;
        }
        public bool ShopWebGisILogin()
        {
            return _subRuleResolver.GetReturn();
        }

        public string ShopWebGisILogin(UserDto user)
        {
            throw new NotImplementedException();
        }

        void ILoginApplication.ShopWebGisILogin()
        {
            throw new NotImplementedException();
        }
    }
}
