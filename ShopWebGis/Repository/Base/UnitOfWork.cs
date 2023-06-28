/************************************************************************************

 * Copyright (c) 2022 All Rights Reserved.

 * CLR版本： 4.0.30319.42000

 *机器名称：LAPTOP-CC5K5UTK

 *命名空间：Repository.Base

 *文件名：  UnitOfWork

 *版本号：  V1.0.0.0

 *当前的用户域：LAPTOP-CC5K5UTK

 *创建人：  蔡显麒

 *创建时间：2022/5/5 21:37:04

 *描述：工作单元实现类

/************************************************************************************/

using IRepository;
using IRepository.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using ShopWebGisDomain.Base;
using ShopWebGisEntityFrameWorkCore.EntityFrameWorkCore;
using ShopWebThread;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Repository.Base
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _dbContext;
        private readonly ILogger _logger;
        private readonly IServiceProvider _serviceProvider;
        private Hashtable repositorys;
        private readonly ICancellationTokenProvider _cancellationTokenProvider;
        private IDbContextTransaction _dbTransaction { get; set; }
        public UnitOfWork(ShopWebGisDbContext dbContext, IServiceProvider serviceProvider, ILogger<UnitOfWork> logger, ICancellationTokenProvider cancellationTokenProvider)
        {
            _dbContext = dbContext;
            _serviceProvider = serviceProvider;
            _logger = logger;
            _cancellationTokenProvider = cancellationTokenProvider;
        }
        public virtual void BeginTran()
        {
            _dbTransaction = _dbContext.Database.BeginTransaction();
        }

        public async Task BeginTranAsync(CancellationToken cancellationToken = default)
        {
            _dbTransaction = await _dbContext.Database.BeginTransactionAsync(_cancellationTokenProvider.FallbackToProvider(cancellationToken));
        }

        public DbContext GetDbContext()
        {
            return _dbContext;
        }

        public void RollbackTran()
        {
            _dbContext.Database.RollbackTransaction();
        }

        public async Task RollbackTranAsync(CancellationToken cancellationToken = default)
        {
            await _dbContext.Database.RollbackTransactionAsync(_cancellationTokenProvider.FallbackToProvider(cancellationToken));
        }

        public void Dispose()
        {

            if (_dbTransaction != null)
            {
                _dbTransaction.Dispose();
            }
            //垃圾回收
            _dbContext.Dispose();
            GC.SuppressFinalize(this);
        }

        public IRepository<TPrimaryKey, TEntity> Repositorys<TPrimaryKey, TEntity>() where TEntity : EntityBase<TPrimaryKey>
        {
            if (repositorys == null)
                repositorys = new Hashtable();
            var entityType = typeof(TEntity);
            var keyType = typeof(TPrimaryKey);
            if (!repositorys.ContainsKey(entityType.Name))
            {
                var baseType = typeof(Repository<,>);
                var repositoryInstance = Activator.CreateInstance(baseType.MakeGenericType(keyType, entityType), _dbContext);
                repositorys.Add(entityType.Name, repositoryInstance);
            }

            return (IRepository<TPrimaryKey, TEntity>)repositorys[entityType.Name];
        }

        public int CommitTran()
        {
            var result = 0;
            try
            {
                result = _dbContext.SaveChanges();
                if (_dbTransaction != null)

                    _dbContext.Database.CommitTransaction();
            }
            catch (Exception ex)
            {
                result = -1;
                RollbackTran();
                _logger.LogError($"Commit 异常：{ex.InnerException}/r{ ex.Message}");
                throw new Exception($"Commit 异常：{ex.InnerException}/r{ ex.Message}");
            }
            return result;
        }

        public async Task<int> CommitTranAsync(CancellationToken cancellationToken = default)
        {
            var result = 0;
            try
            {
                result = await _dbContext.SaveChangesAsync(_cancellationTokenProvider.FallbackToProvider(cancellationToken));
                if (_dbTransaction != null)

                    await _dbContext.Database.CommitTransactionAsync(_cancellationTokenProvider.FallbackToProvider(cancellationToken));
            }
            catch (Exception ex)
            {
                result = -1;
                RollbackTran();
                _logger.LogError($"Commit 异常：{ex.InnerException}/r{ ex.Message}");
                throw new Exception($"Commit 异常：{ex.InnerException}/r{ ex.Message}");
            }
            return await Task.FromResult(result); ;
        }
    }
}
