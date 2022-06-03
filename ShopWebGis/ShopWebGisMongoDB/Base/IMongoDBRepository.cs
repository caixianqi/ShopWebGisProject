/************************************************************************************

 * Copyright (c) 2022 All Rights Reserved.

 * CLR版本： 4.0.30319.42000

 *机器名称：LAPTOP-CC5K5UTK

 *命名空间：ShopWebGisMongoDB.Base

 *文件名：  IMongoDBRepository

 *版本号：  V1.0.0.0

 *当前的用户域：LAPTOP-CC5K5UTK

 *创建人： 智慧环保部-蔡显麒

 *创建时间：2022/5/17 14:43:08

 *描述：IMongoDBRepository接口仓储

/************************************************************************************/

using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShopWebGisMongoDB.Base
{
    public interface IMongoDBRepository<TPrimaryKey, TEntity, TCollectionName> where TEntity : class
    {
        Task<List<TEntity>> GetAllList(Action<FindOptions<TEntity, TEntity>> FindOptions);

        Task<List<TEntity>> GetList(FilterDefinition<TEntity> Filter, Action<FindOptions<TEntity, TEntity>> FindOptions = null);

        void InsertOneAsync(TEntity entity, Action<InsertOneOptions> InsertOptions=null);

        void InsertManyAsync(List<TEntity> entities, Action<InsertManyOptions> InsertManyOptions=null);

        Task<UpdateResult> Update(FilterDefinition<TEntity> filter, UpdateDefinition<TEntity> update, Action<UpdateOptions> UpdateOptions = null);

        Task<UpdateResult> UpdateMany(FilterDefinition<TEntity> filter, UpdateDefinition<TEntity> update, Action<UpdateOptions> UpdateOptions = null);

        Task<DeleteResult> Delete(FilterDefinition<TEntity> filter, Action<DeleteOptions> DeleteOptions=null);

        Task<DeleteResult> DeleteMany(FilterDefinition<TEntity> filter, Action<DeleteOptions> DeleteOptions=null);
    }
}
