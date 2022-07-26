/************************************************************************************

 * Copyright (c) 2022 All Rights Reserved.

 * CLR版本： 4.0.30319.42000

 *机器名称：LAPTOP-CC5K5UTK

 *命名空间：ShopWebGisApplicationContract.Login.Models

 *文件名：  UserDto

 *版本号：  V1.0.0.0

 *当前的用户域：LAPTOP-CC5K5UTK

 *创建人：  蔡显麒

 *创建时间：2022/5/9 16:08:52

 *描述：用户DTO

/************************************************************************************/

using ShopWebGisDomain.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopWebGisApplicationContract.User.Models
{
    public class UserDto : IEntityDto<int>
    {
        public int Id { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 用户密码
        /// </summary>
        public string UserPassword { get; set; }

        /// <summary>
        /// 用户电话
        /// </summary>
        public string UserPhone { get; set; }

        /// <summary>
        /// 用户省份
        /// </summary>
        public string Province { get; set; }

        /// <summary>
        /// 用户城市
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// 用户区县
        /// </summary>
        public string County { get; set; }

        /// <summary>
        /// 详细地址
        /// </summary>
        public string AddressDetail { get; set; }

    }
}
