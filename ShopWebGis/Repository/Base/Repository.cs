/************************************************************************************

 * Copyright (c) 2022 All Rights Reserved.

 * CLR版本： 4.0.30319.42000

 *机器名称：LAPTOP-CC5K5UTK

 *命名空间：Repository

 *文件名：  Repository

 *版本号：  V1.0.0.0

 *当前的用户域：LAPTOP-CC5K5UTK

 *创建人：  蔡显麒

 *创建时间：2022/5/1 22:10:03

 *描述：仓储接口实现类

/************************************************************************************/

using IRepository;
using IRepository.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using ShopWebGisDomain.Base;
using ShopWebGisDomain.config;
using ShopWebGisDomainShare.Common;
using ShopWebGisDomainShare.Const;
using ShopWebGisDomainShare.CustomException;
using ShopWebGisDomainShare.Extension;
using ShopWebGisEntityFrameWorkCore.EntityFrameWorkCore;
using ShopWebThread;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Repository.Base
{
    public class Repository<TPrimaryKey, TEntity> : IRepository<TPrimaryKey, TEntity> where TEntity : EntityBase<TPrimaryKey>
    {
        private readonly DbContext _dbContext;
        private DbSet<TEntity> _dbSet => _dbContext.Set<TEntity>();
        private ICancellationTokenProvider _cancellationTokenProvider { get; set; }

        public Repository(ShopWebGisDbContext dbContext)
        {
            _dbContext = dbContext;
            _cancellationTokenProvider = NullCancellationTokenProvider.Instance;
        }

        public virtual void AttachIfNot(TEntity entity)
        {
            var entry = _dbContext.ChangeTracker.Entries().FirstOrDefault(x => x.Entity.GetType() == entity.GetType());
            if (entry != null)
            {
                throw new ShopWebGisCustomException($"{typeof(TEntity).Name}实体类型不存在,无法进行追踪!");
            }
            _dbContext.Attach(entity);
        }


        public async Task<int> SaveAsync(CancellationToken cancellationToken = default)
        {
            return await _dbContext.SaveChangesAsync(GetCancellationToken(cancellationToken));
        }


        public IQueryable<TEntity> IQueryable => _dbSet.AsQueryable();


        public IQueryable<TEntity> GetAllIncluding(Expression<Func<TEntity, object>>[] propertySelectors, CancellationToken cancellationToken = default)
        {
            var iQueryAble = _dbSet.AsQueryable();
            foreach (var propertySelector in propertySelectors)
            {
                iQueryAble = _dbSet.Include(propertySelector);
            }
            return iQueryAble;
        }

        public async Task<List<TEntity>> GetAvailableListAsync(CancellationToken cancellationToken = default)
        {
            return await _dbSet.Where(x => x.IsSoftDelete == false).ToListAsync(GetCancellationToken(cancellationToken));
        }

        public async Task<List<TEntity>> GetAvailableListAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
        {
            var expression = predicate.Merge((x => x.IsSoftDelete == false));
            return await _dbSet.Where(expression).ToListAsync(GetCancellationToken(cancellationToken));
        }

        public async Task<Page<TEntity>> GetAvailablePageListAsync(int pageIndex = 1, int pageSize = 20, CancellationToken cancellationToken = default)
        {
            return await _dbSet.ToPagedListAsync(pageIndex, pageSize);
        }

        public async Task<Page<TEntity>> GetAvailablePageListAsync(Expression<Func<TEntity, bool>> predicate, int pageIndex = 1, int pageSize = 20, CancellationToken cancellationToken = default)
        {
            var expression = predicate.Merge((x => x.IsSoftDelete == false));
            return await _dbSet.Where(expression).OrderBy(x => x.Id).ToPagedListAsync(pageIndex, pageSize, GetCancellationToken(cancellationToken));
        }

        public async Task<TEntity> FindAsync([NotNull] TPrimaryKey id, CancellationToken cancellationToken = default)
        {
            return await _dbSet.FindAsync(id, GetCancellationToken(cancellationToken));
        }

        public async Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return await _dbSet.SingleAsync(predicate, GetCancellationToken(cancellationToken));
        }


        public async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return await _dbSet.FirstOrDefaultAsync(predicate, GetCancellationToken(cancellationToken));
        }

        public async Task<EntityEntry<TEntity>> InsertAsync([NotNull] TEntity entity, CancellationToken cancellationToken = default)
        {
            var valueTask = await _dbSet.AddAsync(entity, GetCancellationToken(cancellationToken));
            await SaveAsync(cancellationToken);
            return valueTask;
        }

        public async Task InsertRangeAsync(IList<TEntity> list, CancellationToken cancellationToken = default)
        {
            await _dbSet.AddRangeAsync(list, GetCancellationToken(cancellationToken));
            await SaveAsync(cancellationToken);
        }

        public async Task<EntityEntry<TEntity>> UpdateAsync([NotNull] TEntity entity, CancellationToken cancellationToken = default)
        {
            AttachIfNot(entity);
            var entityEntry = _dbSet.Update(entity);
            await SaveAsync(cancellationToken);
            return entityEntry;
        }

        public async Task UpdateRangeAsync([NotNull] IList<TEntity> list, CancellationToken cancellationToken = default)
        {
            _dbSet.UpdateRange(list);
            await SaveAsync(cancellationToken);
            await Task.CompletedTask;
        }

        public async Task DeleteAsync([NotNull] TEntity entity, CancellationToken cancellationToken = default)
        {
            _dbSet.Remove(entity);
            await SaveAsync(cancellationToken);
            await Task.CompletedTask;
        }

        public async Task<EntityEntry<TEntity>> DeleteAsync([NotNull] TPrimaryKey id, CancellationToken cancellationToken = default)
        {
            // 先查后删
            var entity = await _dbSet.FindAsync(id, GetCancellationToken(cancellationToken));
            if (entity == null)
            {
                throw new ShopWebGisCustomException($"{typeof(TEntity).Name}-{id}不存在,无法进行追踪删除!");
            }
            var entityEntry = _dbSet.Remove(entity);
            await SaveAsync(cancellationToken);
            return entityEntry;
        }

        public async Task DeleteAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
        {
            var entities = _dbSet.Where(predicate);
            _dbSet.RemoveRange(entities);
            await SaveAsync(cancellationToken);
            await Task.CompletedTask;
        }

        public async Task DeleteManyAsync(TPrimaryKey[] ids, CancellationToken cancellationToken = default)
        {
            var entities = _dbSet.Where(x => ids.Contains(x.Id));
            _dbSet.RemoveRange(entities);
            await SaveAsync(cancellationToken);
            await Task.CompletedTask;
        }

        public async Task<EntityEntry<TEntity>> SoftDeleteAsync([NotNull] TPrimaryKey id, CancellationToken cancellationToken = default)
        {
            var entity = await _dbSet.FindAsync(id, GetCancellationToken(cancellationToken));
            if (entity == null)
            {
                throw new ShopWebGisCustomException($"{typeof(TEntity).Name}-{id}不存在,无法进行追踪禁用!");
            }
            entity.IsSoftDelete = true;
            var entityEntry = _dbSet.Update(entity);
            await SaveAsync(cancellationToken);
            return entityEntry;
        }

        public async Task SoftDeleteAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
        {
            var entities = _dbSet.Where(predicate);
            foreach (var entity in entities)
            {
                entity.IsSoftDelete = true;
                _dbSet.Update(entity);
            }
            await SaveAsync(cancellationToken);
        }

        public async Task SoftDeleteManyAsync(TPrimaryKey[] ids, CancellationToken cancellationToken = default)
        {
            var entities = _dbSet.Where(x => ids.Contains(x.Id));
            foreach (var entity in entities)
            {
                entity.IsSoftDelete = true;
                _dbSet.Update(entity);
            }
            await SaveAsync(cancellationToken);


        }

        public async Task<int> CountAsync(CancellationToken cancellationToken = default)
        {
            return await _dbSet.CountAsync(GetCancellationToken(cancellationToken));
        }

        public async Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return await _dbSet.CountAsync(predicate, GetCancellationToken(cancellationToken));
        }

        public async Task<long> LongCountAsync(CancellationToken cancellationToken = default)
        {
            return await _dbSet.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        public async Task<long> LongCountAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return await _dbSet.LongCountAsync(predicate, GetCancellationToken(cancellationToken));
        }

        public Task<int> DeleteRangeAsync(IList<TEntity> list, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<EntityEntry<TEntity>> UpdateActionAsync([NotNull] TPrimaryKey id, Action<TEntity> action, CancellationToken cancellationToken = default)
        {
            var entity = await _dbSet.FindAsync(id, GetCancellationToken(cancellationToken));
            if (entity == null)
            {
                throw new ShopWebGisCustomException($"{typeof(TEntity).Name}-{id}不存在,无法进行追踪更新!");
            }
            action?.Invoke(entity);
            var entityEntry = _dbSet.Update(entity);
            await SaveAsync(cancellationToken);
            return entityEntry;
        }

        protected virtual CancellationToken GetCancellationToken(CancellationToken preferredValue = default)
        {
            return _cancellationTokenProvider.FallbackToProvider(preferredValue);
        }
    }
}
