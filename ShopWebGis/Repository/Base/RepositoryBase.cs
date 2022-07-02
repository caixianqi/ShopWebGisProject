/************************************************************************************

 * Copyright (c) 2022 All Rights Reserved.

 * CLR版本： 4.0.30319.42000

 *机器名称：LAPTOP-CC5K5UTK

 *命名空间：Repository.Base

 *文件名：  RepositoryBase

 *版本号：  V1.0.0.0

 *当前的用户域：LAPTOP-CC5K5UTK

 *创建人：  智慧环保部-蔡显麒

 *创建时间：2022/5/1 22:22:00

 *描述：仓储抽象类

/************************************************************************************/

using IRepository;
using ShopWebGisDomain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Base
{
    public abstract class RepositoryBase<TPrimaryKey, TEntity> : IRepository<TPrimaryKey, TEntity> where TEntity : EntityBase<TPrimaryKey>
    {
        public abstract int Count();


        public abstract int Count(Expression<Func<TEntity, bool>> predicate);


        public virtual async Task<int> CountAsync()
        {
            return await Task.FromResult(Count());
        }

        public async Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await Task.FromResult(Count(predicate));
        }

        public abstract void Delete(TEntity entity);


        public abstract void Delete(TPrimaryKey id);


        public abstract void Delete(Expression<Func<TEntity, bool>> predicate);


        public virtual async Task DeleteAsync(TEntity entity)
        {
            Delete(entity);
            await Task.CompletedTask;
        }

        public virtual async Task DeleteAsync(TPrimaryKey id)
        {
            Delete(id);
            await Task.CompletedTask;
        }

        public virtual async Task DeleteAsync(Expression<Func<TEntity, bool>> predicate)
        {
            Delete(predicate);
            await Task.CompletedTask;
        }

        public abstract TEntity FirstOrDefault(TPrimaryKey id);


        public abstract TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate);

        public virtual async Task<TEntity> FirstOrDefaultAsync(TPrimaryKey id)
        {
            return await Task.FromResult(FirstOrDefault(id));
        }

        public virtual async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await Task.FromResult(FirstOrDefault(predicate));
        }

        public abstract TEntity Find(TPrimaryKey id);

        public abstract Task<TEntity> FindAsync(TPrimaryKey id);


        public abstract IQueryable<TEntity> GetQuery();

         
        public abstract IQueryable<TEntity> GetAllIncluding(params Expression<Func<TEntity, object>> [] propertySelectors);


        public virtual List<TEntity> GetAllList()
        {
            return GetQuery().ToList();
        }

        public abstract List<TEntity> GetAllList(Expression<Func<TEntity, bool>> predicate);


        public virtual async Task<List<TEntity>> GetAllListAsync()
        {
            return await Task.FromResult(GetAllList());
        }

        public virtual async Task<List<TEntity>> GetAllListAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await Task.FromResult(GetAllList(predicate));
        }


        public abstract int Insert(TEntity entity);

        public abstract Task<TEntity> InsertAsync(TEntity entity);


        public abstract long LongCount();


        public abstract long LongCount(Expression<Func<TEntity, bool>> predicate);


        public virtual async Task<long> LongCountAsync()
        {
            return await Task.FromResult(LongCount());
        }

        public virtual async Task<long> LongCountAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await Task.FromResult(LongCount(predicate));
        }

        public abstract T Query<T>(Func<IQueryable<TEntity>, T> queryMethod);


        public abstract TEntity Single(Expression<Func<TEntity, bool>> predicate);


        public virtual async Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await Task.FromResult(Single(predicate));
        }

        public abstract int SoftDelete(TEntity entity);


        public abstract int SoftDelete(TPrimaryKey id);


        public abstract int SoftDelete(Expression<Func<TEntity, bool>> predicate);


        public virtual Task<int> SoftDeleteAsync(TEntity entity)
        {

            return Task.FromResult(SoftDelete(entity));
        }

        public Task<int> SoftDeleteAsync(TPrimaryKey id)
        {
            return Task.FromResult(SoftDelete(id));
        }

        public Task<int> SoftDeleteAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return Task.FromResult(SoftDelete(predicate));
        }

        public abstract TEntity Update(TEntity entity);


        public abstract TEntity Update(TPrimaryKey id, Action<TEntity> updateAction);

        public virtual async Task<TEntity> UpdateAsync(TEntity entity)
        {
            return await Task.FromResult(Update(entity));
        }

        public abstract Task<TEntity> UpdateAsync(TPrimaryKey id, Func<TEntity, Task> updateAction);

    }
}
