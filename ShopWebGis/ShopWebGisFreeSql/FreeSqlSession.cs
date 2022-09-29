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
using ShopWebGisFreeSql.Extension;
using ShopWebGisFreeSql.InterFace;
using ShopWebGisLogger.Factory;
using System;
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

        public FreeSqlSession(IConfiguration configuration, IElasticSearchFactory elasticSearchFactory)
        {
            _configuration = configuration;
            _elasticSearchFactory = elasticSearchFactory;
        }

        public IFreeSql Get(string connectStringName)
        {
            if (string.IsNullOrWhiteSpace(connectStringName))
            {
                throw new Exception("空数据库连接!");
            }
            if (_iFreesql == null)
            {
                _iFreesql = CreateConnection(connectStringName);
            }
            return _iFreesql;
        }

        /// <summary>
        /// 创建Ifreesql连接
        /// </summary>
        /// <param name="connectionName"></param>
        /// <returns></returns>
        private IFreeSql CreateConnection(string connectStringName)
        {
            var freeSqlBuilder = new FreeSql.FreeSqlBuilder();
            freeSqlBuilder.UseLogger(_elasticSearchFactory.GetLogger());
            var connectString = _configuration.GetConnectionString(connectStringName);
            return freeSqlBuilder.UseConnectionString(DataType.MySql, connectString).Build();
        }

        /// <summary>
        /// 释放Ifreesql连接
        /// </summary>
        public void Dispose()
        {
            if (disposed)
                return;
            try
            {
                _iFreesql.Dispose();
                _iFreesql = null;
            }
            catch
            {

            }
            disposed = true;
        }
    }
}
