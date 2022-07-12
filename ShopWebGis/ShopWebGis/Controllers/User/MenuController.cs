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

        [HttpGet(nameof(GetMenuList))]
        /// <summary>
        /// 获取菜单列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<IList<MenuDto>> GetMenuList(string Query)
        {
            return await _menuApplication.GetMenuList(Query);
        }

        [HttpPost(nameof(AddMenu))]
        /// <summary>
        /// 添加菜单
        /// </summary>
        /// <param name="menuDto"></param>
        /// <returns></returns>
        public async Task<int> AddMenu(MenuDto MenuDto)
        {
            return await _menuApplication.AddMenu(MenuDto);
        }

        [HttpDelete(nameof(DisableMenu))]
        /// <summary>
        /// 禁用菜单
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<int> DisableMenu(int Id)
        {
            return await _menuApplication.DisableMenu(Id);
        }

        [HttpPut(nameof(UpdateMenu))]
        /// <summary>
        /// 修改菜单
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public async Task<MenuDto> UpdateMenu(MenuDto MenuDto)
        {
            return await _menuApplication.UpdateMenu(MenuDto);
        }

        /// <summary>
        /// 获取树形菜单列表
        /// </summary>
        /// <returns></returns>
        [HttpGet(nameof(GetTreeList))]
        public async Task<IList<MenuDto>> GetTreeList()
        {
            return await _menuApplication.GetTreeList(0);
        }
    }
}
