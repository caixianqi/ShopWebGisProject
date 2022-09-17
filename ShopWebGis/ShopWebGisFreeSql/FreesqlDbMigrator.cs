/************************************************************************************

 * Copyright (c) 2022 All Rights Reserved.

 * CLR版本： 4.0.30319.42000

 *机器名称：LAPTOP-CC5K5UTK

 *命名空间：ShopWebGisFreeSql

 *文件名：  Migrator

 *版本号：  V1.0.0.0

 *当前的用户域：LAPTOP-CC5K5UTK

 *创建人：  蔡显麒

 *创建时间：2022/9/16 11:23:59

 *描述：

/************************************************************************************/

using Microsoft.Extensions.Options;
using ShopWebGisDomainShare.Common;
using ShopWebGisFreeSql.InterFace;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopWebGisFreeSql
{
    public class FreesqlDbMigrator : IMigrator
    {
        private readonly DbMigrationEntryOption _dbMigrationEntryOption;
        private readonly IFreesqlSession _freesqlSession;
        private readonly ISubRuleResolver _subRuleResolver;
        public FreesqlDbMigrator(IFreesqlSession freesqlSession, IOptions<DbMigrationEntryOption> options, ISubRuleResolver subRuleResolver)
        {
            _dbMigrationEntryOption = options.Value;
            _freesqlSession = freesqlSession;
            _subRuleResolver = subRuleResolver;
        }
        public void Migrate()
        {
            var freesql = _freesqlSession.Get(_dbMigrationEntryOption.ConnectString);
            foreach (var type in _dbMigrationEntryOption.types)
            {
                freesql.CodeFirst.SyncStructure(type);
                var subRuleContext = _subRuleResolver.Resolve(type);
            }
        }
    }
}
