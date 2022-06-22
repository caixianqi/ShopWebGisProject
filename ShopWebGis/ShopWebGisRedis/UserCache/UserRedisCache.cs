/************************************************************************************

 * Copyright (c) 2022 All Rights Reserved.

 * CLR版本： 4.0.30319.42000

 *机器名称：LAPTOP-CC5K5UTK

 *命名空间：ShopWebGisRedis.UserCache

 *文件名：  UserRedisCache

 *版本号：  V1.0.0.0

 *当前的用户域：LAPTOP-CC5K5UTK

 *创建人：  智慧环保部-蔡显麒

 *创建时间：2022/6/22 11:48:03

 *描述：Redis-User

/************************************************************************************/

using ShopWebGisCache.UserCache;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopWebGisRedis.UserCache
{
    public class UserRedisCache : IUserCache
    {
        public void LimitLoginTimes()
        {
            throw new NotImplementedException();
        }
    }
}
