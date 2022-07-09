using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopWebGisApplicationContract.User;
using ShopWebGisApplicationContract.User.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopWebGis.HttApi.Host.Controllers
{
    [ApiController]
    [Route("api/Menu")]
    [Authorize]
    public class MenuController : ControllerBase
    {
        private readonly IMenuApplication _menuApplication;
        public MenuController(IMenuApplication menuApplication)
        {
            _menuApplication = menuApplication;
        }

        /// <summary>
        /// 获取菜单列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<IList<MenuDto>> GetMenuList(string query)
        {
            return await _menuApplication.GetMenuList(query);
        }

        /// <summary>
        /// 添加菜单
        /// </summary>
        /// <param name="menuDto"></param>
        /// <returns></returns>
        public async Task<int> AddMenu(MenuDto menuDto)
        {
            return await _menuApplication.AddMenu(menuDto);
        }
    }
}
