/************************************************************************************

 * Copyright (c) 2022 All Rights Reserved.

 * CLR版本： 4.0.30319.42000

 *机器名称：LAPTOP-CC5K5UTK

 *命名空间：ShopWebGisApplicationContract.User

 *文件名：  IMenuApplication

 *版本号：  V1.0.0.0

 *当前的用户域：LAPTOP-CC5K5UTK

 *创建人： 智慧环保部-蔡显麒

 *创建时间：2022/6/30 18:04:52

 *描述：菜单接口

/************************************************************************************/
using ShopWebGisApplicationContract.User.Models;
using ShopWebGisDomainShare.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShopWebGisApplicationContract.User
{
    public interface IMenuApplication
    {
        /// <summary>
        /// 新增菜单
        /// </summary>
        /// <param name="menuDto"></param>
        /// <returns></returns>
        Task<int> AddMenu(MenuDto menuDto);

        /// <summary>
        /// 禁用菜单
        /// </summary>
        /// <param name="menuDto"></param>
        /// <returns></returns>
        Task<int> DisableMenu(int Id);

        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task<int> DeleteMenu(int Id);

        /// <summary>
        /// 获取菜单列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<Page<MenuDto>> GetMenuList(string query, int pageIndex, int pageSize);

        /// <summary>
        /// 获取菜单详情
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task<MenuDto> GetMenu(int Id);

        /// <summary>
        /// 更新菜单
        /// </summary>
        /// <param name="menuDto"></param>
        /// <returns></returns>
        Task<MenuDto> UpdateMenu(MenuDto menuDto);

        /// <summary>
        /// 获取树形菜单
        /// </summary>
        /// <returns></returns>
        /// <param name="parentId">父节点Id</param>
        Task<IList<MenuDto>> GetTreeList(int parentId);

    }
}
