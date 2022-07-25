/************************************************************************************

 * Copyright (c) 2022 All Rights Reserved.

 * CLR版本： 4.0.30319.42000

 *机器名称：LAPTOP-CC5K5UTK

 *命名空间：ShopWebGisApplication.Shop

 *文件名：  GoodClassifyApplication

 *版本号：  V1.0.0.0

 *当前的用户域：LAPTOP-CC5K5UTK

 *创建人：  智慧环保部-蔡显麒

 *创建时间：2022/7/23 13:25:47

 *描述：

/************************************************************************************/

using IRepository.Base;
using ShopWebGisApplicationContract.Shop;
using ShopWebGisDomain.Shop;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShopWebGisApplication.Shop
{
    public class GoodClassifyApplication : IGoodClassifyApplication
    {
        private readonly IRepository<int, GoodClassification> _repository;
        public GoodClassifyApplication(IRepository<int, GoodClassification> repository)
        {
            _repository = repository;
        }
        public Task<IList<GoodClassification>> GetGoodsClassifyList(string query)
        {
            throw new NotImplementedException();
        }
    }
}
