/************************************************************************************

 * Copyright (c) 2022 All Rights Reserved.

 * CLR版本： 4.0.30319.42000

 *机器名称：LAPTOP-CC5K5UTK

 *命名空间：ShopWebGisApplication.Shop

 *文件名：  GoodClassifyApplication

 *版本号：  V1.0.0.0

 *当前的用户域：LAPTOP-CC5K5UTK

 *创建人：  蔡显麒

 *创建时间：2022/7/23 13:25:47

 *描述：

/************************************************************************************/

using AutoMapper;
using IRepository;
using IRepository.Base;
using ShopWebGisApplication.Base;
using ShopWebGisApplicationContract.Shop;
using ShopWebGisApplicationContract.Shop.Dto;
using ShopWebGisDomain.Shop;
using ShopWebGisDomainShare.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShopWebGisApplication.Shop
{
    public class GoodClassifyApplication : CrudApplication<int, GoodClassification, GoodClassificationDto>, IGoodClassifyApplication
    {
        private readonly IRepository<int, GoodClassification> _repository;
        private readonly IMapper _mapper;
        public GoodClassifyApplication(IUnitOfWork iUnitOfWork, IMapper mapper) : base(iUnitOfWork, mapper)
        {
            _repository = iUnitOfWork.Repositorys<int, GoodClassification>();
            _mapper = mapper;
        }
    }
}
