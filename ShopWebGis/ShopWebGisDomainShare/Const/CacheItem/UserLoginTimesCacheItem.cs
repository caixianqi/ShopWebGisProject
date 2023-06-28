/************************************************************************************

 * Copyright (c) 2023 All Rights Reserved.

 * CLR版本： 4.0.30319.42000

 *机器名称：LAPTOP-CC5K5UTK

 *命名空间：ShopWebGisDomainShare.Const.CacheItem

 *文件名：  UserLoginCacheItem

 *版本号：  V1.0.0.0

 *当前的用户域：LAPTOP-CC5K5UTK

 *创建人：  蔡显麒

 *创建时间：2023/6/26 23:01:35

 *描述：用户登录限制次数CacheItem

/************************************************************************************/

using System;
using System.Collections.Generic;
using System.Text;

namespace ShopWebGisDomainShare.Const.CacheItem
{
    public class UserLoginTimesCacheItem
    {

        public UserLoginTimesCacheItem(int id)
        {
            Id = id;
        }

        public UserLoginTimesCacheItem()
        {

        }
        public int Id { get; set; }

        public int Times { get; set; }

        public override string ToString()
        {
            return $"{base.ToString()}_{Id.ToString()}";
        }
    }
}
