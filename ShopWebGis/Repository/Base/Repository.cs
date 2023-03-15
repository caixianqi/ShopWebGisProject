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
using ShopWebGisDomain.Base;
using ShopWebGisDomain.config;
using ShopWebGisDomainShare.Common;
using ShopWebGisDomainShare.Const;
using ShopWebGisDomainShare.CustomException;
using ShopWebGisDomainShare.Extension;
using ShopWebGisEntityFrameWorkCore.EntityFrameWorkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Base
{
    public class Repository<TPrimaryKey, TEntity> : IRepository<TPrimaryKey, TEntity> where TEntity : EntityBase<TPrimaryKey>
    {
        private readonly DbContext _dbContext;
        private DbSet<TEntity> _dbSet => _dbContext.Set<TEntity>();
        public Repository(ShopWebGisDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public virtual void AttachIfNot(TEntity entity)
        {
            var entry = _dbContext.ChangeTracker.Entries().FirstOrDefault(x => x.Entity == entity);
            if (entry != null)
            {
                return;
            }
            _dbContext.Attach(entity);
        }

        public async Task<int> SaveAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

        public IQueryable<TEntity> GetAllIncluding(params Expression<Func<TEntity, object>>[] propertySelectors)
        {
            var iQueryAble = _dbSet.AsQueryable();
            foreach (var propertySelector in propertySelectors)
            {
                iQueryAble = _dbSet.Include(propertySelector);
            }
            return iQueryAble;
        }

        public async Task<List<TEntity>> GetAvailableListAsync()
        {
            return await _dbSet.Where(x => x.isSoftDelete == false).ToListAsync();
        }

        public async Task<List<TEntity>> GetAvailableListAsync(Expression<Func<TEntity, bool>> predicate)
        {
            var expression = predicate.Merge((x => x.isSoftDelete == false));
            return await _dbSet.Where(expression).ToListAsync();
        }

        public async Task<Page<TEntity>> GetAvailablePageListAsync(int pageIndex = 1, int pageSize = 20)
        {
            return await _dbSet.ToPagedListAsync(pageIndex, pageSize);
        }

        public async Task<Page<TEntity>> GetAvailablePageListAsync(Expression<Func<TEntity, bool>> predicate, int pageIndex = 1, int pageSize = 20)
        {
            var expression = predicate.Merge((x => x.isSoftDelete == false));
            return await _dbSet.Where(expression).OrderBy(x => x.Id).ToPagedListAsync(pageIndex, pageSize);
        }

        public async Task<TEntity> FindAsync([NotNull] TPrimaryKey id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbSet.SingleAsync(predicate);
        }


        public async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbSet.FirstOrDefaultAsync(predicate);
        }

        public async Task<int> InsertAsync([NotNull] TEntity entity)
        {
            await _dbSet.AddAsync(entity);
            var eddectRows = await SaveAsync();
            return eddectRows;
        }

        public async Task<int> InsertRangeAsync(IList<TEntity> list)
        {
            await _dbSet.AddRangeAsync(list);
            var eddectRows = await SaveAsync();
            return eddectRows;
        }

        public async Task<TPrimaryKey> InsertAsyncReturnId([NotNull] TEntity entity)
        {
            await _dbSet.AddAsync(entity);
            await SaveAsync();
            return entity.Id;
        }

        public async Task<int> UpdateAsync(TEntity entity)
        {
            //AttachIfNot(entity);
            //_dbContext.Entry(entity).State = EntityState.Modified;
            _dbSet.Update(entity);
            return await SaveAsync();
        }

        public async Task<int> UpdateRangeAsync(IList<TEntity> list)
        {
            //foreach (var enitty in list)
            //{
            //    _dbContext.Entry(enitty).State = EntityState.Modified;
            //}
            _dbSet.UpdateRange(list);
            return await SaveAsync();
        }

        public async Task<int> DeleteAsync(TEntity entity)
        {
            _dbSet.Remove(entity);
            var effectRows = await SaveAsync();
            if (effectRows <= 0)
            {
                throw new ShopWebGisCustomException(SystemConst.NotAffectedRow);
            }
            return effectRows;
        }

        public async Task<int> DeleteAsync([NotNull] TPrimaryKey id)
        {
            // 先查后删
            var entity = await _dbSet.FindAsync(id);
            if (entity == null)
            {
                throw new ShopWebGisCustomException($"{typeof(TEntity).Name}-{id}不存在,无法进行追踪删除!");
            }
            _dbSet.Remove(entity);
            return await SaveAsync();
        }

        public async Task<int> DeleteAsync(Expression<Func<TEntity, bool>> predicate)
        {
            var entities = _dbSet.Where(predicate);
            _dbSet.RemoveRange(entities);
            return await SaveAsync();
        }


        public async Task<int> SoftDeleteAsync([NotNull] TPrimaryKey id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity == null)
            {
                throw new ShopWebGisCustomException($"{typeof(TEntity).Name}-{id}不存在,无法进行追踪禁用!");
            }
            entity.isSoftDelete = true;
            _dbSet.Update(entity);
            return await SaveAsync();
        }

        public async Task<int> SoftDeleteAsync(Expression<Func<TEntity, bool>> predicate)
        {
            var entities = _dbSet.Where(predicate);
            foreach (var entity in entities)
            {
                entity.isSoftDelete = true;
                _dbSet.Update(entity);
            }
            return await SaveAsync();
        }

        public async Task<int> CountAsync()
        {
            return await _dbSet.CountAsync();
        }

        public async Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbSet.CountAsync(predicate);
        }

        public async Task<long> LongCountAsync()
        {
            return await _dbSet.LongCountAsync();
        }

        public async Task<long> LongCountAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbSet.LongCountAsync(predicate);
        }

        public Task<int> DeleteRangeAsync(IList<TEntity> list)
        {
            throw new NotImplementedException();
        }

        public async Task<TEntity> UpdateActionAsync([NotNull] TPrimaryKey id, Action<TEntity> action)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity == null)
            {
                throw new ShopWebGisCustomException($"{typeof(TEntity).Name}-{id}不存在,无法进行追踪更新!");
            }
            action?.Invoke(entity);
            await SaveAsync();
            return entity;
        }


    }
}
