/************************************************************************************

 * Copyright (c) 2022 All Rights Reserved.

 * CLR版本： 4.0.30319.42000

 *机器名称：LAPTOP-CC5K5UTK

 *命名空间：IRepository

 *文件名：  IRepository

 *版本号：  V1.0.0.0

 *当前的用户域：LAPTOP-CC5K5UTK

 *创建人： 蔡显麒

 *创建时间：2022/5/1 21:58:48

 *描述：泛型仓储接口

/************************************************************************************/
using Microsoft.EntityFrameworkCore.ChangeTracking;
using ShopWebGisDomain.Base;
using ShopWebGisDomain.config;
using ShopWebGisDomainShare.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IRepository.Base
{
    public interface IRepository<TPrimaryKey, TEntity> where TEntity : class
    {
        #region Select/Get/Query

        /// <summary>
        /// 添加至上下文
        /// </summary>
        /// <param name="entity"></param>
        void AttachIfNot(TEntity entity);

        Task<int> SaveAsync(CancellationToken cancellationToken = default);

        IQueryable<TEntity> IQueryable { get; }

        /// <summary>
        /// 双表关联
        /// </summary>
        /// <param name="propertySelectors"></param>
        /// <returns></returns>
        IQueryable<TEntity> GetAllIncluding(Expression<Func<TEntity, object>>[] propertySelectors, CancellationToken cancellationToken = default);

        Task<List<TEntity>> GetAvailableListAsync(CancellationToken cancellationToken = default);

        Task<List<TEntity>> GetAvailableListAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);

        Task<Page<TEntity>> GetAvailablePageListAsync(int pageIndex = 1, int pageSize = 20, CancellationToken cancellationToken = default);

        Task<Page<TEntity>> GetAvailablePageListAsync(Expression<Func<TEntity, bool>> predicate, int pageIndex = 1, int pageSize = 20, CancellationToken cancellationToken = default);

        Task<TEntity> FindAsync(TPrimaryKey id, CancellationToken cancellationToken = default);

        Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);

        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);


        #endregion

        #region Insert

        Task<EntityEntry<TEntity>> InsertAsync([NotNull] TEntity entity, CancellationToken cancellationToken = default);

        Task InsertRangeAsync(IList<TEntity> list, CancellationToken cancellationToken = default);


        #endregion

        #region Update


        Task<EntityEntry<TEntity>> UpdateAsync([NotNull] TEntity entity, CancellationToken cancellationToken = default);


        Task UpdateRangeAsync(IList<TEntity> list, CancellationToken cancellationToken = default);

        Task<EntityEntry<TEntity>> UpdateActionAsync(TPrimaryKey id, Action<TEntity> action, CancellationToken cancellationToken = default);

        #endregion

        #region Delete


        Task<EntityEntry<TEntity>> DeleteAsync(TPrimaryKey id, CancellationToken cancellationToken = default);

        Task DeleteAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);


        Task DeleteManyAsync(TPrimaryKey[] ids, CancellationToken cancellationToken = default);


        #endregion

        #region SoftDelete



        // 软删除
        Task<EntityEntry<TEntity>> SoftDeleteAsync(TPrimaryKey id, CancellationToken cancellationToken = default);


        Task SoftDeleteAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);

        Task SoftDeleteManyAsync(TPrimaryKey[] ids, CancellationToken cancellationToken = default);
        #endregion


        #region Aggregates


        Task<int> CountAsync(CancellationToken cancellationToken = default);


        Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);


        Task<long> LongCountAsync(CancellationToken cancellationToken = default);


        Task<long> LongCountAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);

        #endregion
    }
}
