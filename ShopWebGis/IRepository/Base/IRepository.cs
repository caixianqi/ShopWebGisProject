/************************************************************************************

 * Copyright (c) 2022 All Rights Reserved.

 * CLR版本： 4.0.30319.42000

 *机器名称：LAPTOP-CC5K5UTK

 *命名空间：IRepository

 *文件名：  IRepository

 *版本号：  V1.0.0.0

 *当前的用户域：LAPTOP-CC5K5UTK

 *创建人： 智慧环保部-蔡显麒

 *创建时间：2022/5/1 21:58:48

 *描述：泛型仓储接口

/************************************************************************************/
using ShopWebGisDomain.Base;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
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

        Task<int> SaveAsync();

        /// <summary>
        /// 双表关联
        /// </summary>
        /// <param name="propertySelectors"></param>
        /// <returns></returns>
        IQueryable<TEntity> GetAllIncluding(params Expression<Func<TEntity, object>>[] propertySelectors);


        Task<List<TEntity>> GetAvailableListAsync();

        Task<List<TEntity>> GetAvailableListAsync(Expression<Func<TEntity, bool>> predicate);

        Task<TEntity> FindAsync(TPrimaryKey id);

        Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> predicate);

        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);


        #endregion

        #region Insert

        Task<int> InsertAsync([NotNull]TEntity entity);

        Task<int> InsertRangeAsync(IList<TEntity> list);

        #endregion

        #region Update


        Task<int> UpdateAsync(TEntity entity);


        Task<int> UpdateRangeAsync(IList<TEntity> list);

        Task<TEntity> UpdateActionAsync(TPrimaryKey id, Action<TEntity> action);

        #endregion

        #region Delete


        Task<int> DeleteAsync(TPrimaryKey id);

        Task<int> DeleteAsync(Expression<Func<TEntity, bool>> predicate);



        #endregion

        #region SoftDelete



        // 软删除
        Task<int> SoftDeleteAsync(TPrimaryKey id);


        Task<int> SoftDeleteAsync(Expression<Func<TEntity, bool>> predicate);
        #endregion


        #region Aggregates


        Task<int> CountAsync();


        Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate);


        Task<long> LongCountAsync();


        Task<long> LongCountAsync(Expression<Func<TEntity, bool>> predicate);

        #endregion
    }
}
