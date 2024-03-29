﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopWebGisApplicationContract.Shop;
using ShopWebGisApplicationContract.Shop.Dto;
using ShopWebGisDomain.Shop;
using ShopWebGisDomainShare.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopWebGis.HttApi.Host.Controllers.CommodityManagement
{
    [ApiController]
    [Route("api/GoodClassify")]
    [Authorize]
    public class GoodClassifyController : CrudBaseController<int, GoodClassification, GoodClassificationDto>
    {
      
    }
}
