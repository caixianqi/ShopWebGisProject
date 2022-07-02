/************************************************************************************

 * Copyright (c) 2022 All Rights Reserved.

 * CLR版本： 4.0.30319.42000

 *机器名称：LAPTOP-CC5K5UTK

 *命名空间：ShopWebGisApplication.User

 *文件名：  RoleApplication

 *版本号：  V1.0.0.0

 *当前的用户域：LAPTOP-CC5K5UTK

 *创建人：  智慧环保部-蔡显麒

 *创建时间：2022/7/2 11:04:36

 *描述：

/************************************************************************************/

using IRepository;
using ShopWebGisApplicationContract.User;
using ShopWebGisDomain.User;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShopWebGisApplication.User
{
    public class RoleApplication : IRoleApplication
    {
        private readonly IRepository<int, RoleInfo> _roleRepository;
        public RoleApplication(IRepository<int, RoleInfo> repository)
        {
            _roleRepository = repository;
        }
        public Task<List<RoleInfo>> GetRoleList(string name)
        {
            
        }
    }
}
