/************************************************************************************

 * Copyright (c) 2022 All Rights Reserved.

 * CLR版本： 4.0.30319.42000

 *机器名称：LAPTOP-CC5K5UTK

 *命名空间：ShopWebGisRedis.config

 *文件名：  IRedisManager

 *版本号：  V1.0.0.0

 *当前的用户域：LAPTOP-CC5K5UTK

 *创建人： 智慧环保部-蔡显麒

 *创建时间：2022/6/24 23:26:49

 *描述：Redis 通用接口

/************************************************************************************/
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShopWebGisRedis.config
{
    public interface IShopWebGisRedisDataBase
    {
        /// <summary>
        /// 获取String缓存
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<RedisValue> StringGetAsync(RedisKey key);

        /// <summary>
        /// 获取Key和Expire
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<RedisValueWithExpiry> StringGetWithExpiryAsync(RedisKey key);

        /// <summary>
        /// 设置String缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expiry"></param>
        /// <returns></returns>
        Task<bool> StringSetAsync(RedisKey key, RedisValue value, TimeSpan? expiry = null);

        /// <summary>
        /// 判断Key是否存在
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<bool> KeyExistsAsync(RedisKey key);

        /// <summary>
        /// 将 key 中储存的数字值增一
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        Task<long> StringIncrementAsync(RedisKey redisKey, long num);

        /// <summary>
        /// 删除key
        /// </summary>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        Task<bool> KeyDelete(RedisKey redisKey);

        /// <summary>
        /// 设置Hash缓存(数组)
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="hashFields"></param>
        /// <returns></returns>
        Task HashSetAsync(RedisKey redisKey, HashEntry[] hashFields);

        /// <summary>
        /// 设置Hash缓存
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="hashField"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        Task HashSetAsync(RedisKey redisKey, RedisValue hashField, RedisValue value);

        /// <summary>
        /// 获取Hash缓存
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="hashField"></param>
        /// <returns></returns>
        Task<RedisValue> HashGetAsync(RedisKey redisKey, RedisValue hashField);

        /// <summary>
        /// 删除Hash缓存 Key
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="hashField"></param>
        /// <returns></returns>
        Task<bool> HashDeleteAsync(RedisKey redisKey, RedisValue hashField);

    }
}
