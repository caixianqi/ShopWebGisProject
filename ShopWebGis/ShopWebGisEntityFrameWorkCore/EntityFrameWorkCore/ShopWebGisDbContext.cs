/************************************************************************************

 * Copyright (c) 2022 All Rights Reserved.

 * CLR版本： 4.0.30319.42000

 *机器名称：LAPTOP-CC5K5UTK

 *命名空间：ShopWebGisEntityFrameWorkCore.EntityFrameWorkCore

 *文件名：  ShopWebGisDbContext

 *版本号：  V1.0.0.0

 *当前的用户域：LAPTOP-CC5K5UTK

 *创建人：  蔡显麒

 *创建时间：2022/4/29 14:24:52

 *描述：EntityframeWorkCore DBContext上下文

/************************************************************************************/

using Microsoft.EntityFrameworkCore;
using ShopWebGisDomain.User;
using ShopWebGisDomain.ValueObject;
using ShopWebGisEntityFrameWorkCore.EntityConfiguration;
using ShopWebGisEntityFrameWorkCore.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShopWebGisEntityFrameWorkCore.EntityFrameWorkCore
{
    public class ShopWebGisDbContext : DbContext
    {
        public ShopWebGisDbContext(DbContextOptions<ShopWebGisDbContext> options) : base(options)
        {

        }

        public DbSet<UserInfo> userInfos { get; set; }

        public DbSet<RoleInfo> roleInfos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.ConfigureShopWebGisEntity();
        }

        public override int SaveChanges()
        {
            //自动修改 CreateTime,UpdateTime
            var entityEntries = ChangeTracker.Entries().ToList();
            foreach (var entry in entityEntries)
            {
                if (entry.State == EntityState.Added)
                    Entry(entry.Entity).Property("CreateTime").CurrentValue = DateTime.Now;

                if (entry.State == EntityState.Modified)
                    Entry(entry.Entity).Property("UpdateTime").CurrentValue = DateTime.Now;
            }
            return base.SaveChanges();
        }
    }
}
