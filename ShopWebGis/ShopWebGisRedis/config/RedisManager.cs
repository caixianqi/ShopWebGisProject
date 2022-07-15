/************************************************************************************

 * Copyright (c) 2022 All Rights Reserved.

 * CLR版本： 4.0.30319.42000

 *机器名称：LAPTOP-CC5K5UTK

 *命名空间：ShopWebGisRedis.config

 *文件名：  RedisManage

 *版本号：  V1.0.0.0

 *当前的用户域：LAPTOP-CC5K5UTK

 *创建人：  智慧环保部-蔡显麒

 *创建时间：2022/6/24 23:27:15

 *描述：Redis管理器

/************************************************************************************/

using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopWebGisRedis.config
{
    public  sealed class RedisManager
    {
        public volatile ConnectionMultiplexer _redisConnect;
        private readonly object redisConnectLock = new object();
        public RedisManager(ConfigurationOptions configurationOptions)
        {
            _redisConnect = RedisConnect(configurationOptions);
            //_redisConnect.GetDatabase();
        }

        private ConnectionMultiplexer RedisConnect(ConfigurationOptions configurationOptions)
        {
            if (_redisConnect != null && _redisConnect.IsConnected)
            {
                return _redisConnect;
            }
            lock (redisConnectLock)
            {
                // 释放,重连
                if (_redisConnect != null)
                {
                    _redisConnect.Dispose();

                }
                try
                {
                    _redisConnect = ConnectionMultiplexer.Connect(configurationOptions);
                }
                catch (Exception ex)
                {
                    throw new Exception("Redis服务启动失败", ex);
                }
            }
            return _redisConnect;
        }
    }
}
