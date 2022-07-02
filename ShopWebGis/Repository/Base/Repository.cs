/************************************************************************************

 * Copyright (c) 2022 All Rights Reserved.

 * CLR版本： 4.0.30319.42000

 *机器名称：LAPTOP-CC5K5UTK

 *命名空间：Repository

 *文件名：  Repository

 *版本号：  V1.0.0.0

 *当前的用户域：LAPTOP-CC5K5UTK

 *创建人：  智慧环保部-蔡显麒

 *创建时间：2022/5/1 22:10:03

 *描述：仓储接口实现类

/************************************************************************************/

using IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Repository.Base;
using ShopWebGisDomain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class Repository<TPrimaryKey, TEntity> : RepositoryBase<TPrimaryKey, TEntity> where TEntity : EntityBase<TPrimaryKey>
    {
        public readonly DbContext _dbContext;
        private  DbSet<TEntity> _dbSet;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public Repository(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = unitOfWork.GetDbContext();
            _dbSet = _dbContext.Set<TEntity>();
            _httpContextAccessor = httpContextAccessor;
        }

        public override int Count()
        {
            return _dbSet.Count();
        }

        public override int Count(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbSet.Count(predicate);
        }

        public override void Delete(TEntity entity)
        {
            _dbSet.Remove(entity);
            _dbContext.SaveChanges();
        }

        public override void Delete(TPrimaryKey id)
        {
            var entity = _dbSet.Find(id);
            if (entity == null)
            {
                throw new Exception("xxx");
            }
            _dbSet.Remove(entity);
            _dbContext.SaveChanges();
        }

        public override void Delete(Expression<Func<TEntity, bool>> predicate)
        {
            var entities = _dbSet.Where(predicate);
            foreach (var enttiy in entities)
            {
                _dbSet.Remove(enttiy);
            }
            _dbContext.SaveChanges();
        }

        public override TEntity FirstOrDefault(TPrimaryKey id)
        {
            throw new NotImplementedException();
        }

        public override TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbSet.FirstOrDefault(predicate);
        }

        public override TEntity Find(TPrimaryKey id)
        {
            return _dbSet.Find(id);
        }

        public override async Task<TEntity> FindAsync(TPrimaryKey id)
        {
            return await _dbSet.FindAsync(id);
        }

        public override IQueryable<TEntity> GetQuery()
        {
            return _dbSet;
        }

        public override IQueryable<TEntity> GetAllIncluding(params Expression<Func<TEntity, object>> [] propertySelectors)
        {
            var Queryable = _dbSet.AsQueryable();
            foreach (var propertySelector in propertySelectors)
            {
                Queryable = Queryable.Include(propertySelector);
            }
             
            return Queryable;
        }

        public override List<TEntity> GetAllList(Expression<Func<TEntity, bool>> predicate)
        {
            Func<TEntity, bool> func = predicate.Compile();
            return _dbSet.Where(func).ToList();
        }

        public override int Insert(TEntity entity)
        {
            _dbSet.Add(entity);
            return _dbContext.SaveChanges();
        }

        public override long LongCount()
        {
            return _dbSet.LongCount();
        }

        public override long LongCount(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbSet.LongCount(predicate);
        }

        public override T Query<T>(Func<IQueryable<TEntity>, T> queryMethod)
        {
            if (queryMethod == null)
            {
                throw new Exception("XXX");
            }
            return queryMethod.Invoke(_dbSet);
        }

        public override TEntity Single(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbSet.Single(predicate);
        }

        public override TEntity Update(TEntity entity)
        {
            var updateEntity = _dbSet.Update(entity);
            _dbContext.SaveChanges();
            return updateEntity.Entity;
        }

        public override TEntity Update(TPrimaryKey id, Action<TEntity> updateAction)
        {
            var entity = _dbSet.Find(id);
            if (entity == null)
            {
                throw new Exception("xxx");
            }
            updateAction?.Invoke(entity);
            var updateEntity = _dbSet.Update(entity);
            _dbContext.SaveChanges();
            return updateEntity.Entity;
        }

        public override async Task<TEntity> UpdateAsync(TPrimaryKey id, Func<TEntity, Task> updateAction)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity == null)
            {
                throw new Exception("xxx");
            }
            updateAction?.Invoke(entity);
            var updateEntity = _dbSet.Update(entity);
            _dbContext.SaveChanges();
            return updateEntity.Entity;
        }

        public override int SoftDelete(TEntity entity)
        {
            entity.isSoftDelete = true;
            _dbSet.Update(entity);
            return _dbContext.SaveChanges();

        }

        public override int SoftDelete(TPrimaryKey id)
        {
            var entity = _dbSet.Find(id);
            if (entity == null)
            {
                throw new Exception("xxx");
            }
            entity.isSoftDelete = true;
            _dbSet.Update(entity);
            return _dbContext.SaveChanges();

        }

        public override int SoftDelete(Expression<Func<TEntity, bool>> predicate)
        {
            var entities = _dbSet.Where(predicate);
            foreach (var entity in entities)
            {
                entity.isSoftDelete = true;
            }
            _dbSet.UpdateRange(entities);
            return _dbContext.SaveChanges();
        }

        public override Task<TEntity> InsertAsync(TEntity entity)
        {
            var Entity = _dbSet.AddAsync(entity).Result.Entity;
            _dbContext.SaveChanges();
            return Task.FromResult(Entity);
        }
    }
}
