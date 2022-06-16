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

using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ShopWebGisApplicationContract.User;
using ShopWebGisDomain.User;
using ShopWebGisDomain.ValueObject;
using ShopWebGisDomainShare.Common;
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
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUser _user;
        public ShopWebGisDbContext(DbContextOptions<ShopWebGisDbContext> options, IConfiguration configuration, IUser user) : base(options)
        {
            _configuration = configuration;
            _user = user;
        }

        public DbSet<UserInfo> userInfos { get; set; }

        public DbSet<RoleInfo> roleInfos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //根据配置文件判断是否插入数据种子
            if (bool.TryParse(_configuration["Initialization:DataSeed"], out bool DataSeed) && DataSeed)
            {
            }
            modelBuilder.ConfigureShopWebGisEntity();
            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {

            var adminUser = userInfos.Where(x => x.UserName == "admin").First();
            var userId = _user.Id;
            var userName = _user.Name;
            if (string.IsNullOrWhiteSpace(userId) && string.IsNullOrWhiteSpace(userName))
            {
                userId = adminUser.Id.ToString();
                userName = adminUser.UserName;
            }
            //自动修改 CreateTime,UpdateTime,UserId,UserName
            var entityEntries = ChangeTracker.Entries().ToList();
            foreach (var entry in entityEntries)
            {
                if (entry.State == EntityState.Added)
                {
                    Entry(entry.Entity).Property("CreateTime").CurrentValue = DateTime.Now;
                    Entry(entry.Entity).Property("CreateUserId").CurrentValue = userId;
                    Entry(entry.Entity).Property("CreateUserName").CurrentValue = userName;

                }
                if (entry.State == EntityState.Modified)
                {
                    Entry(entry.Entity).Property("UpdateTime").CurrentValue = DateTime.Now;
                    Entry(entry.Entity).Property("UpdateUserId").CurrentValue = userId;
                    Entry(entry.Entity).Property("UpdateUserName").CurrentValue = userName;

                }
            }
            return base.SaveChanges();
        }


    }
}
