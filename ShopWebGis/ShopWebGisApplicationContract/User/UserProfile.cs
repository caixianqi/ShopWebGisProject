/************************************************************************************

 * Copyright (c) 2022 All Rights Reserved.

 * CLR版本： 4.0.30319.42000

 *机器名称：LAPTOP-CC5K5UTK

 *命名空间：ShopWebGisApplicationContract.Login

 *文件名：  LoginProfile

 *版本号：  V1.0.0.0

 *当前的用户域：LAPTOP-CC5K5UTK

 *创建人：  蔡显麒

 *创建时间：2022/6/9 16:56:03

 *描述：User AutoMapper

/************************************************************************************/

using AutoMapper;
using ShopWebGisApplicationContract.User.Models;
using ShopWebGisDomain.User;
using ShopWebGisDomainShare.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopWebGisApplicationContract.User
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserDto, UserInfo>().ReverseMap();
            CreateMap<MenuDto, MenuInfo>().ReverseMap();
            CreateMap<UserUpdateDto, UserInfo>();
            CreateMap<Page<MenuInfo>, Page<MenuDto>>();
        }
    }
}
