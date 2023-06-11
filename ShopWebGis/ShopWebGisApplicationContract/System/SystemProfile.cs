/************************************************************************************

 * Copyright (c) 2023 All Rights Reserved.

 * CLR版本： 4.0.30319.42000

 *机器名称：LAPTOP-CC5K5UTK

 *命名空间：ShopWebGisApplicationContract.System

 *文件名：  SystemProfile

 *版本号：  V1.0.0.0

 *当前的用户域：LAPTOP-CC5K5UTK

 *创建人：  蔡显麒

 *创建时间：2023/6/11 1:38:23

 *描述：

/************************************************************************************/

using AutoMapper;
using ShopWebGisApplicationContract.System.Dto;
using ShopWebGisDomain.Models;
using ShopWebGisDomainShare.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopWebGisApplicationContract.System
{
    public class SystemProfile: Profile
    {
        public SystemProfile()
        {
            CreateMap<Page<DataDictionary>, Page<DataDictionaryDto>>().ReverseMap();
        }
    }
}
