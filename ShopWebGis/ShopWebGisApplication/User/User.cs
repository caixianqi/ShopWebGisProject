/************************************************************************************

 * Copyright (c) 2022 All Rights Reserved.

 * CLR版本： 4.0.30319.42000

 *机器名称：LAPTOP-CC5K5UTK

 *命名空间：ShopWebGisApplication.User

 *文件名：  User

 *版本号：  V1.0.0.0

 *当前的用户域：LAPTOP-CC5K5UTK

 *创建人：  智慧环保部-蔡显麒

 *创建时间：2022/6/14 11:00:13

 *描述：用户接口实现类

/************************************************************************************/

using Microsoft.AspNetCore.Http;
using ShopWebGisApplicationContract.User;
using ShopWebGisDomainShare.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace ShopWebGisApplication.User
{
    public class User : IUser
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public User(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// 用户Id
        /// </summary>
        public virtual string Id
        {
            get
            {
                var id = _httpContextAccessor?.HttpContext?.User?.FindFirst(ClaimAttributes.UserId);
                if (id != null && !string.IsNullOrWhiteSpace(id.Value))
                {
                    return id.Value;
                }
                return "";
            }
        }

        public virtual string Name
        {
            get
            {
                var id = _httpContextAccessor?.HttpContext?.User?.FindFirst(ClaimAttributes.UserName);
                if (id != null && !string.IsNullOrWhiteSpace(id.Value))
                {
                    return id.Value;
                }
                return "";
            }
        }

        /// <summary>
        /// 角色
        /// </summary>
        public IEnumerable<string> Roles
        {
            get
            {
                var roles = _httpContextAccessor?.HttpContext?.User?.FindAll(ClaimTypes.Role).Select(x => x.Value);
                return roles;
            }
        }
    }
}
