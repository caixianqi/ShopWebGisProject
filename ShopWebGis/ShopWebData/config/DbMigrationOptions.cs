/************************************************************************************

 * Copyright (c) 2022 All Rights Reserved.

 * CLR版本： 4.0.30319.42000

 *机器名称：LAPTOP-CC5K5UTK

 *命名空间：ShopWebData.config

 *文件名：  DbMigrationOptions

 *版本号：  V1.0.0.0

 *当前的用户域：LAPTOP-CC5K5UTK

 *创建人：  蔡显麒

 *创建时间：2022/12/12 16:35:40

 *描述：

/************************************************************************************/

using ShopWebGisDomainShare.Extension;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopWebData.config
{
    /// <summary>
    /// 数据库迁移配置
    /// </summary>
    public class DbMigrationOptions
    {
        public Dictionary<string, EntityEntryOptions> EntityEntryOptions { get; }

        public DbMigrationOptions()
        {
            EntityEntryOptions = new Dictionary<string, EntityEntryOptions>();
        }

        public EntityEntryOptions Config(string connnectionStringName)
        {
            if (!EntityEntryOptions.ContainsKey(connnectionStringName))
            {
                EntityEntryOptions[connnectionStringName] = new EntityEntryOptions();
            }
            return EntityEntryOptions[connnectionStringName];
        }
    }

    /// <summary>
    /// 实体迁移配置
    /// </summary>
    public class EntityEntryOptions
    {
        public List<Type> EntityConfigs { get; }


        public EntityEntryOptions()
        {
            EntityConfigs = new List<Type>();
        }

        public EntityEntryOptions Entity<T>()
        {
            EntityConfigs.AddIfNotContains(typeof(T));       
            return this;
        }
    }
}
