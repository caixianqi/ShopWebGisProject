/************************************************************************************

 * Copyright (c) 2022 All Rights Reserved.

 * CLR版本： 4.0.30319.42000

 *机器名称：LAPTOP-CC5K5UTK

 *命名空间：ShopWebGisApplicationContract.Shop.Dto

 *文件名：  ShopProfile

 *版本号：  V1.0.0.0

 *当前的用户域：LAPTOP-CC5K5UTK

 *创建人：  智慧环保部-蔡显麒

 *创建时间：2022/7/17 19:17:17

 *描述：Shop AutoMapper

/************************************************************************************/

using AutoMapper;
using ShopWebGisDomain.Shop;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopWebGisApplicationContract.Shop.Dto
{
    public class ShopProfile : Profile
    {
        public ShopProfile()
        {
            CreateMap<GoodClassificationDto, GoodClassification>().ReverseMap();
        }
    }
}
