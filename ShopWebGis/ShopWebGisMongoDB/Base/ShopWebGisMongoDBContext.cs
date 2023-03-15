/************************************************************************************

 * Copyright (c) 2022 All Rights Reserved.

 * CLR版本： 4.0.30319.42000

 *机器名称：LAPTOP-CC5K5UTK

 *命名空间：ShopWebGisMongoDB.Base

 *文件名：  ShopWebGisMongoDBContext

 *版本号：  V1.0.0.0

 *当前的用户域：LAPTOP-CC5K5UTK

 *创建人：  蔡显麒

 *创建时间：2022/5/17 17:05:02

 *描述：MongoDB上下文

/************************************************************************************/

using MongoDB.Driver;
using ShopWebGisMongoDB.MongoDBConfig;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopWebGisMongoDB.Base
{
    public abstract class ShopWebGisMongoDBContext<TEntity, ICollectionName>
    {
        protected IMongoCollection<TEntity> _mongoDBCollection;
        protected FindOptions<TEntity, TEntity> _mongoDBFindOptions;
        protected IMongoDatabase dataBase { get; }
        public ShopWebGisMongoDBContext(IShopWebGisDatabaseSettings shopWebGisDatabaseSettings)
        {
            var client = new MongoClient(shopWebGisDatabaseSettings.ConnectionString);
            dataBase = client.GetDatabase(shopWebGisDatabaseSettings.DatabaseName);
            _mongoDBCollection = dataBase.GetCollection<TEntity>(typeof(ICollectionName).Name);
            _mongoDBFindOptions = new FindOptions<TEntity, TEntity>();
        }
    }
}

