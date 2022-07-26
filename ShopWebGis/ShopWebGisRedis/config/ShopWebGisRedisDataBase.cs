/************************************************************************************

 * Copyright (c) 2022 All Rights Reserved.

 * CLR版本： 4.0.30319.42000

 *机器名称：LAPTOP-CC5K5UTK

 *命名空间：ShopWebGisRedis.config

 *文件名：  ShopWebGisRedisDataBase

 *版本号：  V1.0.0.0

 *当前的用户域：LAPTOP-CC5K5UTK

 *创建人：  蔡显麒

 *创建时间：2022/6/24 23:46:33

 *描述：

/************************************************************************************/

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ShopWebGisCache;
using ShopWebGisDomain.config;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShopWebGisRedis.config
{
    public class ShopWebGisRedisDataBase : IShopWebGisCache
    {
        private readonly IDatabase _database;
        private readonly ILogger<ShopWebGisRedisDataBase> _logger;
        public ShopWebGisRedisDataBase(IOptions<RedisConfiguration> _redisOptions, IOptions<Jwt> options, ILogger<ShopWebGisRedisDataBase> logger)
        {
            _logger = logger;
            ConfigurationOptions configurationOptions = GetRedisConfigurationOptions(_redisOptions.Value);
            RedisManager redisManager = new RedisManager(configurationOptions);
            _database = redisManager._redisConnect.GetDatabase();

        }
        private ConfigurationOptions GetRedisConfigurationOptions(RedisConfiguration redisConfigurationOption)
        {
            try
            {
                ConfigurationOptions configurationOptions = new ConfigurationOptions
                {
                    DefaultDatabase = redisConfigurationOption.Db,
                    ServiceName = redisConfigurationOption.Name,
                    EndPoints = { { redisConfigurationOption.Ip, redisConfigurationOption.Port } },
                    ConnectTimeout = redisConfigurationOption.Timeout
                };
                return configurationOptions;
            }
            catch (Exception ex)
            {
                _logger.LogCritical("获取Redis配置失败!,服务无法启动", ex);
                return null;
            }


        }

        public async Task<RedisValue> StringGetAsync(RedisKey key)
        {
            return await _database.StringGetAsync(key);
        }

        public async Task<RedisValueWithExpiry> StringGetWithExpiryAsync(RedisKey key)
        {
            return await _database.StringGetWithExpiryAsync(key);
        }

        public async Task<bool> StringSetAsync(RedisKey key, RedisValue value, TimeSpan? expiry = null)
        {
            return await _database.StringSetAsync(key, value, expiry);
        }

        public async Task<bool> KeyExistsAsync(RedisKey key)
        {
            return await _database.KeyExistsAsync(key);
        }

        public async Task<long> StringIncrementAsync(RedisKey redisKey, long num)
        {
            return await _database.StringIncrementAsync(redisKey, num);
        }

        public async Task<bool> KeyDelete(RedisKey redisKey)
        {
            return await _database.KeyDeleteAsync(redisKey);
        }

        public async Task HashSetAsync(RedisKey redisKey, HashEntry[] hashFields)
        {
            await _database.HashSetAsync(redisKey, hashFields);
        }

        public async Task HashSetAsync(RedisKey redisKey, RedisValue hashField, RedisValue value)
        {
            await _database.HashSetAsync(redisKey, hashField, value);
        }

        public async Task<RedisValue> HashGetAsync(RedisKey redisKey, RedisValue hashField)
        {
            return await _database.HashGetAsync(redisKey, hashField);
        }

        public async Task<bool> HashDeleteAsync(RedisKey redisKey, RedisValue hashField)
        {
            return await _database.HashDeleteAsync(redisKey, hashField);
        }


    }
}
