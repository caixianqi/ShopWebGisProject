/************************************************************************************

 * Copyright (c) 2022 All Rights Reserved.

 * CLR版本： 4.0.30319.42000

 *机器名称：LAPTOP-CC5K5UTK

 *命名空间：ShopWebGisApplicationContract.Login

 *文件名：  UserDtoValidator

 *版本号：  V1.0.0.0

 *当前的用户域：LAPTOP-CC5K5UTK

 *创建人：  智慧环保部-蔡显麒

 *创建时间：2022/6/9 17:00:50

 *描述：用户验证

/************************************************************************************/

using FluentValidation;
using ShopWebGisApplicationContract.Login.Models;
using System;

using System.Collections.Generic;
using System.Text;
namespace ShopWebGisApplicationContract.Login
{
    public class UserDtoValidator : AbstractValidator<UserDto>
    {
        public UserDtoValidator()
        {
            RuleFor(x => x.UserName).NotNull().NotEmpty();
            RuleFor(x => x.UserPassword).NotNull().NotEmpty();
            RuleFor(x => x.UserPhone).NotNull().NotEmpty();
            RuleFor(x => x.Province).NotNull().NotEmpty();
            RuleFor(x => x.City).NotNull().NotEmpty();
            RuleFor(x => x.County).NotNull().NotEmpty();
            RuleFor(x => x.AddressDetail).NotNull().NotEmpty();
        }
    }
}
