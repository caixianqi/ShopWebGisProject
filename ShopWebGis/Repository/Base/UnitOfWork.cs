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
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Repository.Base
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _dbContext;
        private readonly ILogger _logger;
        private readonly IServiceProvider _serviceProvider;
        private Hashtable repositorys;
        private IDbContextTransaction _dbTransaction { get; set; }
        public UnitOfWork(ShopWebGisDbContext dbContext, IServiceProvider serviceProvider, ILogger<UnitOfWork> logger)
        {
            _dbContext = dbContext;
            _serviceProvider = serviceProvider;
            _logger = logger;
        }
        public void BeginTran()
        {
            _dbTransaction = _dbContext.Database.BeginTransaction();
        }

        public void CommitTran()
        {
            try
            {
                if (_dbTransaction != null)

                    _dbContext.Database.CommitTransaction();
            }
            catch (Exception ex)
            {
                RollbackTran();
                _logger.LogError($"Commit 异常：{ex.InnerException}/r{ ex.Message}");
                throw new Exception($"Commit 异常：{ex.InnerException}/r{ ex.Message}");
            }
        }

        public DbContext GetDbContext()
        {
            return _dbContext;
        }

        public void RollbackTran()
        {
            _dbContext.Database.RollbackTransaction();
        }

        public void Dispose()
        {
            _dbTransaction.Dispose();
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
    }
}
