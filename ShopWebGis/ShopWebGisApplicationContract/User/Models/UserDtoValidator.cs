/************************************************************************************

 * Copyright (c) 2022 All Rights Reserved.

 * CLR版本： 4.0.30319.42000

 *机器名称：LAPTOP-CC5K5UTK

 *命名空间：ShopWebGisApplicationContract.Login.Models

 *文件名：  UserDtoValidator

 *版本号：  V1.0.0.0

 *当前的用户域：LAPTOP-CC5K5UTK

 *创建人：  蔡显麒

 *创建时间：2022/6/13 16:52:12

 *描述：用户类相关DTO验证规则

/************************************************************************************/

using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopWebGisApplicationContract.User.Models
{
    public class UserDtoValidator : AbstractValidator<UserDto>
    {
        public UserDtoValidator()
        {
            RuleFor(x => x.UserName).NotNull().NotEmpty();
            RuleFor(x => x.UserPassword).NotNull().NotEmpty();
            RuleFor(x => x.UserPhone).NotNull().NotEmpty();
            //RuleFor(x => x.Province).NotNull().NotEmpty();
            //RuleFor(x => x.City).NotNull().NotEmpty();
            //RuleFor(x => x.County).NotNull().NotEmpty();
            //RuleFor(x => x.AddressDetail).NotNull().NotEmpty();
        }
    }
}
