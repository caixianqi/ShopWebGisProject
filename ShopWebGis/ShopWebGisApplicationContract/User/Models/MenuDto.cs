/************************************************************************************

 * Copyright (c) 2022 All Rights Reserved.

 * CLR版本： 4.0.30319.42000

 *机器名称：LAPTOP-CC5K5UTK

 *命名空间：ShopWebGisApplicationContract.User.Models

 *文件名：  MenuDto

 *版本号：  V1.0.0.0

 *当前的用户域：LAPTOP-CC5K5UTK

 *创建人：  蔡显麒

 *创建时间：2022/7/3 12:45:17

 *描述：菜单DTO

/************************************************************************************/

using ShopWebGisDomain.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopWebGisApplicationContract.User.Models
{
    public class MenuDto : IEntityDto<int>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Url { get; set; }

        public int Sort { get; set; }

        public int ParentId { get; set; }

        public string IconClass { get; set; }

        /// <summary>
        /// 子节点
        /// </summary>
        public IList<MenuDto> children { get; set; }
    }
}
