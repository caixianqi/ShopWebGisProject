/************************************************************************************

 * Copyright (c) 2022 All Rights Reserved.

 * CLR版本： 4.0.30319.42000

 *机器名称：LAPTOP-CC5K5UTK

 *命名空间：ShopWebGisFreeSql.Migration

 *文件名：  FreeSqlDbMigrator

 *版本号：  V1.0.0.0

 *当前的用户域：LAPTOP-CC5K5UTK

 *创建人：  蔡显麒

 *创建时间：2022/12/22 11:13:18

 *描述：

/************************************************************************************/

using Microsoft.Extensions.Options;
using ShopWebData.config;
using ShopWebData.Migration;
using ShopWebData.SubTable;
using ShopWebGisFreeSql.Extension;
using ShopWebGisFreeSql.InterFace;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopWebGisFreeSql.Migration
{
    public class FreeSqlDbMigrator : IMigrator
    {
        private readonly IFreesqlSession _freeSqlSession;
        private readonly DbMigrationOptions _dbMigrationOptions;
        private readonly TableSubRuleOptions _tableSubRuleOptions;
        private readonly ISubRuleResolver _subRuleResolver;
        public FreeSqlDbMigrator(IFreesqlSession freeSqlSession, IOptions<DbMigrationOptions> options, IOptions<TableSubRuleOptions> subOptions, ISubRuleResolver subRuleResolver)
        {
            _freeSqlSession = freeSqlSession;
            _dbMigrationOptions = options.Value;
            _tableSubRuleOptions = subOptions.Value;
            _subRuleResolver = subRuleResolver;
        }

        /// <summary>
        /// 数据库迁移，默认数据库已经存在
        /// </summary>
        public void Migrate()
        {
            foreach (var item in _dbMigrationOptions.EntityEntryOptions)
            {
                // 数据表迁移
                var freeSql = _freeSqlSession.Get(item.Key);
                // 设置aop,进行自定义特性的写入
                freeSql.AddCustomAttributesAop();
                foreach (var entityType in item.Value.EntityConfigs)
                {
                    // 迁移表结构
                    freeSql.CodeFirst.SyncStructure(entityType);

                    // 若已配置分表规则, 按照分表规则创建表
                    if (_tableSubRuleOptions != null)
                    {
                        var context = _subRuleResolver.Resolve(entityType);
                        var tableNames = context.SubRule.GetDefaultTables(entityType, context.DefaultName);
                        foreach (var tableName in tableNames)
                        {
                            freeSql.CodeFirst.SyncStructure(entityType, tableName);
                        }
                    }
                }

            }
        }
    }
}
