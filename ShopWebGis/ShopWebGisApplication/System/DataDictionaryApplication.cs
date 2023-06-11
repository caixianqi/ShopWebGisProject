/************************************************************************************

 * Copyright (c) 2023 All Rights Reserved.

 * CLR版本： 4.0.30319.42000

 *机器名称：LAPTOP-CC5K5UTK

 *命名空间：ShopWebGisApplication.System

 *文件名：  DataDictionaryApplication

 *版本号：  V1.0.0.0

 *当前的用户域：LAPTOP-CC5K5UTK

 *创建人：  蔡显麒

 *创建时间：2023/6/9 23:59:27

 *描述：

/************************************************************************************/

using AutoMapper;
using IRepository;
using IRepository.Base;
using ShopWebGisApplication.Base;
using ShopWebGisApplicationContract.System;
using ShopWebGisApplicationContract.System.Dto;
using ShopWebGisDomain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopWebGisApplication.System
{
    public class DataDictionaryApplication: IDataDictionary
    {
        private readonly IRepository<int, DataDictionary> _repository;
        private readonly IMapper _mapper;
        public DataDictionaryApplication(IUnitOfWork iUnitOfWork, IMapper mapper) 
        {
            _repository = iUnitOfWork.Repositorys<int, DataDictionary>(); ;
            _mapper = mapper;
        }
    }
}
