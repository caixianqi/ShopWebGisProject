/************************************************************************************

 * Copyright (c) 2022 All Rights Reserved.

 * CLR版本： 4.0.30319.42000

 *机器名称：LAPTOP-CC5K5UTK

 *命名空间：ShopWebGisMongoDB.Base

 *文件名：  MongoDBRepository

 *版本号：  V1.0.0.0

 *当前的用户域：LAPTOP-CC5K5UTK

 *创建人：  智慧环保部-蔡显麒

 *创建时间：2022/5/17 14:57:04

 *描述：MongoDBRepository接口仓储实现类

/************************************************************************************/


using MongoDB.Driver;
using ShopWebGisDomain.Base;
using ShopWebGisMongoDB.MongoDBConfig;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShopWebGisMongoDB.Base
{
    public class MongoDBRepository<TPrimaryKey, TEntity, TCollectionName> : ShopWebGisMongoDBContext<TEntity, TCollectionName>, IMongoDBRepository<TPrimaryKey, TEntity, TCollectionName> where TEntity : class

    {

        public MongoDBRepository(IShopWebGisDatabaseSettings shopWebGisDatabaseSettings) : base(shopWebGisDatabaseSettings)
        {

        }
        public async Task<List<TEntity>> GetAllList(Action<FindOptions<TEntity, TEntity>> FindOptions = null)
        {
            FindOptions?.Invoke(_mongoDBFindOptions);
            return (await _mongoDBCollection.FindAsync(x => true, _mongoDBFindOptions)).ToList();
        }

        public async Task<List<TEntity>> GetList(FilterDefinition<TEntity> Filter, Action<FindOptions<TEntity, TEntity>> FindOptions = null)
        {
            FindOptions?.Invoke(_mongoDBFindOptions);
            return (await _mongoDBCollection.FindAsync(Filter, _mongoDBFindOptions)).ToList();
        }

        public async Task<UpdateResult> Update(FilterDefinition<TEntity> filter, UpdateDefinition<TEntity> update, Action<UpdateOptions> UpdateOptions = null)
        {
            var mongoDBUpdateOptions = new UpdateOptions();
            UpdateOptions?.Invoke(mongoDBUpdateOptions);
            return await _mongoDBCollection.UpdateOneAsync(filter, update, mongoDBUpdateOptions);
        }

        public async Task<UpdateResult> UpdateMany(FilterDefinition<TEntity> filter, UpdateDefinition<TEntity> update, Action<UpdateOptions> UpdateOptions = null)
        {
            var mongoDBUpdateOptions = new UpdateOptions();
            UpdateOptions?.Invoke(mongoDBUpdateOptions);
            return await _mongoDBCollection.UpdateOneAsync(filter, update, mongoDBUpdateOptions);
        }

        public async Task<DeleteResult> Delete(FilterDefinition<TEntity> filter, Action<DeleteOptions> DeleteOptions = null)
        {
            var deleteOptions = new DeleteOptions();
            DeleteOptions?.Invoke(deleteOptions);
            return await _mongoDBCollection.DeleteOneAsync(filter, deleteOptions);
        }

        public async Task<DeleteResult> DeleteMany(FilterDefinition<TEntity> filter, Action<DeleteOptions> DeleteOptions = null)
        {
            var deleteOptions = new DeleteOptions();
            DeleteOptions?.Invoke(deleteOptions);
            return await _mongoDBCollection.DeleteManyAsync(filter, deleteOptions);
        }


        public async void InsertOneAsync(TEntity entity, Action<InsertOneOptions> InsertOptions = null)
        {
            var insertOptions = new InsertOneOptions();
            InsertOptions?.Invoke(insertOptions);
            await _mongoDBCollection.InsertOneAsync(entity, insertOptions);
        }

        public async void InsertManyAsync(List<TEntity> entities, Action<InsertManyOptions> InsertManyOptions = null)
        {
            var insertManyOptions = new InsertManyOptions();
            InsertManyOptions?.Invoke(insertManyOptions);
            await _mongoDBCollection.InsertManyAsync(entities, insertManyOptions);
        }
    }
}
