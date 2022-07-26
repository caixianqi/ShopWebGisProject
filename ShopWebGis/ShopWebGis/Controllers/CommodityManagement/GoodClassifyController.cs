using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopWebGisApplicationContract.Shop;
using ShopWebGisApplicationContract.Shop.Dto;
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
    public class GoodClassifyController : ControllerBase
    {
        private readonly IGoodClassifyApplication _goodClassifyApplication;
        public GoodClassifyController(IGoodClassifyApplication goodClassifyApplication)
        {
            _goodClassifyApplication = goodClassifyApplication;
        }

        [HttpGet(nameof(GetGoodsClassifyList))]
        public async Task<Page<GoodClassificationDto>> GetGoodsClassifyList(int pageIndex, int pageSize)
        {
            return await _goodClassifyApplication.GetPageListAsync(pageIndex, pageSize);
        }
    }
}
