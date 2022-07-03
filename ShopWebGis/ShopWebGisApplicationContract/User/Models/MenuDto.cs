/************************************************************************************

 * Copyright (c) 2022 All Rights Reserved.

 * CLR版本： 4.0.30319.42000

 *机器名称：LAPTOP-CC5K5UTK

 *命名空间：ShopWebGisApplicationContract.User.Models

 *文件名：  MenuDto

 *版本号：  V1.0.0.0

 *当前的用户域：LAPTOP-CC5K5UTK

 *创建人：  智慧环保部-蔡显麒

 *创建时间：2022/7/3 12:45:17

 *描述：菜单DTO

/************************************************************************************/

using System;
using System.Collections.Generic;
using System.Text;

namespace ShopWebGisApplicationContract.User.Models
{
    public class MenuDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Url { get; set; }

        public string Sort { get; set; }

        public int ParentId { get; set; }

        public List<MenuDto> childs = new List<MenuDto>();
    }
}
