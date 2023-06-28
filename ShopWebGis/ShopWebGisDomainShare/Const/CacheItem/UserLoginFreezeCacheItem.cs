/************************************************************************************

 * Copyright (c) 2023 All Rights Reserved.

 * CLR版本： 4.0.30319.42000

 *机器名称：LAPTOP-CC5K5UTK

 *命名空间：ShopWebGisDomainShare.Const.CacheItem

 *文件名：  CacheLoginFreezeCacheItem

 *版本号：  V1.0.0.0

 *当前的用户域：LAPTOP-CC5K5UTK

 *创建人：  蔡显麒

 *创建时间：2023/6/26 23:25:04

 *描述：

/************************************************************************************/

using System;
using System.Collections.Generic;
using System.Text;

namespace ShopWebGisDomainShare.Const.CacheItem
{
    public class UserLoginFreezeCacheItem
    {
        public UserLoginFreezeCacheItem(int id)
        {
            Id = id;
        }

        public UserLoginFreezeCacheItem()
        {

        }

        public int Id { get; set; }

        /// <summary>
        /// 冻结时间
        /// </summary>
        public DateTime FreezeExpire { get; set; }

        public override string ToString()
        {
            return base.ToString() + Id.ToString();
        }
    }
}
