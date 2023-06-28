using AutoMapper;
using IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopWebGisApplicationContract.System.Dto;
using ShopWebGisDomain.Base;
using ShopWebGisDomain.Models;
using ShopWebGisDomainShare.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopWebGis.HttApi.Host.Controllers.System
{
    [ApiController]
    [Authorize]
    public class DataDictionaryController: CrudBaseController<int, DataDictionary, DataDictionaryDto>
    {
        public DataDictionaryController(IUnitOfWork iUnitOfWork, IMapper mapper)
        {
            
        }
       
    }
}
