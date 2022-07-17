/************************************************************************************

 * Copyright (c) 2022 All Rights Reserved.

 * CLR版本： 4.0.30319.42000

 *机器名称：LAPTOP-CC5K5UTK

 *命名空间：ShopWebGisApplication.User

 *文件名：  MenuApplication

 *版本号：  V1.0.0.0

 *当前的用户域：LAPTOP-CC5K5UTK

 *创建人：  智慧环保部-蔡显麒

 *创建时间：2022/6/30 18:06:30

 *描述：

/************************************************************************************/

using AutoMapper;
using IRepository;
using IRepository.Base;
using ShopWebGisApplicationContract.User;
using ShopWebGisApplicationContract.User.Models;
using ShopWebGisDomain.User;
using ShopWebGisDomainShare.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopWebGisApplication.User
{
    public class MenuApplication : IMenuApplication
    {
        private readonly IRepository<int, MenuInfo> _repository;
        private readonly IMapper _mapper;
        public MenuApplication(IRepository<int, MenuInfo> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<int> AddMenu(MenuDto menuDto)
        {
            var menu = _mapper.Map<MenuDto, MenuInfo>(menuDto);
            return await _repository.InsertAsync(menu);
        }

        public async Task<int> DeleteMenu(int Id)
        {
            return await _repository.SoftDeleteAsync(Id);
        }

        public async Task<int> DisableMenu(int Id)
        {
            return await _repository.SoftDeleteAsync(Id);
        }

        public async Task<MenuDto> GetMenu(int Id)
        {
            var menu = await _repository.FindAsync(Id);
            return _mapper.Map<MenuInfo, MenuDto>(menu);
        }

        public async Task<Page<MenuDto>> GetMenuList(string query, int pageIndex, int pageSize)
        {
            var data = await _repository.GetAvailablePageListAsync(x => string.IsNullOrWhiteSpace(query) ? true : x.Name.Contains(query.Trim()), pageIndex, pageSize);
            return _mapper.Map<Page<MenuInfo>, Page<MenuDto>>(data);
        }


        public async Task<MenuDto> UpdateMenu(MenuDto menuDto)
        {
            var menu = await _repository.UpdateActionAsync(menuDto.Id, x =>
             {
                 x.Name = menuDto.Name;
                 x.Sort = menuDto.Sort;
             });
            return _mapper.Map<MenuInfo, MenuDto>(menu);
        }

        public async Task<IList<MenuDto>> GetTreeList(int parentId)
        {
            IList<MenuDto> datas = new List<MenuDto>();
            var menuList = await _repository.GetAvailableListAsync(x => x.ParentId == parentId.ToString());
            foreach (var memu in menuList)
            {
                var memuDto = _mapper.Map<MenuInfo, MenuDto>(memu);
                memuDto.children = await GetTreeList(memuDto.Id);
                datas.Add(memuDto);
            }
            return datas.OrderBy(x => x.Sort).ToList();
        }

        public async Task<MenuDto> GetMenuById(int Id)
        {
            var menu = await _repository.FindAsync(Id);
            return _mapper.Map<MenuInfo, MenuDto>(menu);
        }
    }
}
