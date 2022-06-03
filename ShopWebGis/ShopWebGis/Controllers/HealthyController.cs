using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopWebGis.Controllers
{
    [Produces("application/json")]
    [Route("api/Health")]
    public class HealthyController:ControllerBase
    {
        [HttpGet]
        public IActionResult Get() => Ok("ok");
    }
}
