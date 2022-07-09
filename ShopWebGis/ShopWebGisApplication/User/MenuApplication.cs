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
using System;
using System.Collections.Generic;
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

        public Task<int> DsiableMenu(MenuDto menuDto)
        {
            throw new NotImplementedException();
        }

        public Task<MenuDto> GetMenu(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<IList<MenuDto>> GetMenuList(string query)
        {
            throw new NotImplementedException();
        }
    }
}
