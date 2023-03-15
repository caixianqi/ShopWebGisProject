/************************************************************************************

 * Copyright (c) 2022 All Rights Reserved.

 * CLR版本： 4.0.30319.42000

 *机器名称：LAPTOP-CC5K5UTK

 *命名空间：IRepository

 *文件名：  IUnitOfWork

 *版本号：  V1.0.0.0

 *当前的用户域：LAPTOP-CC5K5UTK

 *创建人： 蔡显麒

 *创建时间：2022/5/1 22:02:00

 *描述：工作单元接口

/************************************************************************************/
using IRepository.Base;
using Microsoft.EntityFrameworkCore;
using ShopWebGisDomain.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace IRepository
{
    public interface IUnitOfWork:IDisposable
    {
        DbContext GetDbContext();

        void BeginTran();

        void CommitTran();
        void RollbackTran();

        /// <summary>
        /// 获取仓储
        /// </summary>
        /// <typeparam name="TPrimaryKey"></typeparam>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        IRepository<TPrimaryKey, TEntity> Repositorys<TPrimaryKey, TEntity>() where TEntity : EntityBase<TPrimaryKey>;
    }
}
