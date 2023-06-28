/************************************************************************************

 * Copyright (c) 2022 All Rights Reserved.

 * CLR版本： 4.0.30319.42000

 *机器名称：LAPTOP-CC5K5UTK

 *命名空间：ShopWebGisDomainShare.Const.RedisKey

 *文件名：  UserRedisKeys

 *版本号：  V1.0.0.0

 *当前的用户域：LAPTOP-CC5K5UTK

 *创建人：  蔡显麒

 *创建时间：2022/6/24 15:29:37

 *描述：用户Key

/************************************************************************************/

using System;
using System.Collections.Generic;
using System.Text;

namespace ShopWebGisDomainShare.Const.CacheKey
{
    public class UserCacheKeys
    {
        /// <summary>
        /// cache key 限制用户登录缓存Key前缀
        /// </summary>
        public const string LimitLoginPrefix = "limitLogin_";

        /// <summary>
        /// cache key 冻结用户缓存key前缀
        /// </summary>
        public const string LimitLoginFreezePrefix = "limitLoginFreeze_";
    }
}
