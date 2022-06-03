/************************************************************************************

 * Copyright (c) 2022 All Rights Reserved.

 * CLR版本： 4.0.30319.42000

 *机器名称：LAPTOP-CC5K5UTK

 *命名空间：Repository.Base

 *文件名：  UnitOfWork

 *版本号：  V1.0.0.0

 *当前的用户域：LAPTOP-CC5K5UTK

 *创建人：  智慧环保部-蔡显麒

 *创建时间：2022/5/5 21:37:04

 *描述：工作单元实现类

/************************************************************************************/

using IRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ShopWebGisEntityFrameWorkCore.EntityFrameWorkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Base
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _dbContext;
        private readonly ILogger _logger;
        public UnitOfWork(ShopWebGisDbContext dbContext, ILogger<UnitOfWork> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }
        public void BeginTran()
        {
            _dbContext.Database.BeginTransaction();
        }

        public void CommitTran()
        {
            try
            {
                _dbContext.Database.CommitTransaction();
            }
            catch (Exception ex)
            {
                RollbackTran();
                _logger.LogError($"{ex.Message}\r\n{ex.InnerException}");
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
    }
}
