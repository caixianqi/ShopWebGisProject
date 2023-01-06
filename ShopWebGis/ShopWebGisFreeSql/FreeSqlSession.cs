/************************************************************************************

 * Copyright (c) 2022 All Rights Reserved.

 * CLR版本： 4.0.30319.42000

 *机器名称：LAPTOP-CC5K5UTK

 *命名空间：ShopWebGisFreeSql

 *文件名：  FreeSqlSession

 *版本号：  V1.0.0.0

 *当前的用户域：LAPTOP-CC5K5UTK

 *创建人：  蔡显麒

 *创建时间：2022/9/15 15:49:27

 *描述：

/************************************************************************************/

using FreeSql;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using ShopWebGisFreeSql.config;
using ShopWebGisFreeSql.Extension;
using ShopWebGisFreeSql.InterFace;
using ShopWebGisLogger.Factory;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace ShopWebGisFreeSql
{
    public class FreeSqlSession : IFreesqlSession
    {
        private IFreeSql _iFreesql;

        private bool disposed = false;

        private readonly IConfiguration _configuration;

        private readonly IElasticSearchFactory _elasticSearchFactory;

        private readonly FreeSqlConnectionOptions _freeSqlConnectionOptions;

        protected ConcurrentDictionary<string, IFreeSql> Connections { get; }

        public FreeSqlSession(IConfiguration configuration, IElasticSearchFactory elasticSearchFactory, IOptions<FreeSqlConnectionOptions> options)
        {
            if (options.Value == null) throw new ArgumentException("FreeSqlConnectionOptions is Null !");
            _freeSqlConnectionOptions = options.Value;
            _configuration = configuration;
            _elasticSearchFactory = elasticSearchFactory;
            Connections = new ConcurrentDictionary<string, IFreeSql>();
        }

        public IFreeSql Get(string connectStringName)
        {
            if (string.IsNullOrWhiteSpace(connectStringName))
            {
                throw new Exception("空数据库连接!");
            }
            return Connections.GetOrAdd(connectStringName,
                 CreateConnection(connectStringName)
            );
        }

        /// <summary>
        /// 创建Ifreesql连接
        /// </summary>
        /// <param name="connectionName"></param>
        /// <returns></returns>
        private IFreeSql CreateConnection(string connectStringName)
        {
            FreeSqlContextAction freeSqlContextAction = new FreeSqlContextAction(_configuration.GetConnectionString(connectStringName));
            if (_freeSqlConnectionOptions.DefacultFreeSqlAction == null)
            {
                throw new ArgumentException("FreeSqlConnectionOptions 需要配置对应的数据库DataType!");
            }
            else
            {
                _freeSqlConnectionOptions.DefacultFreeSqlAction.Invoke(freeSqlContextAction);
            }
            var freeSqlBuilder = freeSqlContextAction.freeSqlBuilder;
            freeSqlBuilder.UseLogger(_elasticSearchFactory.GetLogger());
            return freeSqlBuilder.Build();
        }

        /// <summary>
        /// 释放Ifreesql连接
        /// </summary>
        public void Dispose()
        {
            if (disposed)
                return;
            disposed = true;
            try
            {
                foreach (var connection in Connections.Values)
                {
                    connection.Dispose();
                }
            }
            catch
            {

            }

            Connections.Clear();
        }
    }
}
