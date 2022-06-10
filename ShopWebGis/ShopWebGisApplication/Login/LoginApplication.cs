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

using AutoMapper;
using IRepository;
using ShopWebGisApplicationContract.Login;
using ShopWebGisApplicationContract.Login.Models;
using ShopWebGisDomain.User;
using ShopWebGisDomainShare.CustomException;
using ShopWebGisFreeSql.InterFace;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopWebGisApplication.Login
{
    public class LoginApplication : ILoginApplication
    {
        private readonly IMapper _mapper;
        private readonly IRepository<int, UserInfo> _userRepository;
        public LoginApplication(IMapper mapper, IRepository<int, UserInfo> userRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public string ShopWebGisILogin(UserDto user)
        {
            throw new NotImplementedException();
        }

        public string ShopWebGisRegister(UserDto user)
        {
            var response = "";
            UserDtoValidator userValidator = new UserDtoValidator();
            var validateResult = userValidator.Validate(user);
            if (!validateResult.IsValid)
            {
                throw new ShopWebGisCustomException(validateResult.ToString());
            }
            return response;
        }
    }
}
