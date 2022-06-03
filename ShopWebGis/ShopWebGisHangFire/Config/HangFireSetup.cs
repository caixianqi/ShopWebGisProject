/************************************************************************************

 * Copyright (c) 2022 All Rights Reserved.

 * CLR版本： 4.0.30319.42000

 *机器名称：LAPTOP-CC5K5UTK

 *命名空间：ShopWebGisHangFire.Config

 *文件名：  HangFireSetup

 *版本号：  V1.0.0.0

 *当前的用户域：LAPTOP-CC5K5UTK

 *创建人：  智慧环保部-蔡显麒

 *创建时间：2022/5/13 22:44:00

 *描述：HangFire任务调度配置

/************************************************************************************/

using Hangfire;
using Hangfire.Dashboard.BasicAuthorization;
using Hangfire.MySql.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;


namespace ShopWebGisHangFire.Config
{
    public static class HangFireSetup
    {
        public static void HangFireServiceSetup(this IServiceCollection services, IConfiguration configuration)
        {
            //hangfire的任务需要数据库持久化
            //Hangfire.AspNetCore
            //Hangfire.MySql.Core  mysql引用 大小写敏感

            //连接字符串必须加 Allow User Variables=true
            services.AddHangfire(x => x.UseStorage(new MySqlStorage(
                configuration["ConnectionStrings:Mysql"],
                new MySqlStorageOptions
                {
                    //TransactionIsolationLevel = , // 事务隔离级别。默认是读取已提交。
                    QueuePollInterval = TimeSpan.FromSeconds(15),             //- 作业队列轮询间隔。默认值为15秒。
                    JobExpirationCheckInterval = TimeSpan.FromHours(1),       //- 作业到期检查间隔（管理过期记录）。默认值为1小时。
                    CountersAggregateInterval = TimeSpan.FromMinutes(5),      //- 聚合计数器的间隔。默认为5分钟。
                    PrepareSchemaIfNecessary = true,                          //- 如果设置为true，则创建数据库表。默认是true。
                    DashboardJobListLimit = 50000,                            //- 仪表板作业列表限制。默认值为50000。
                    TransactionTimeout = TimeSpan.FromMinutes(1),             //- 交易超时。默认为1分钟。
                }
                )));

        }

        public static void HangFireConfiguraSetup(this IApplicationBuilder app, IConfiguration configuration)
        {

            //添加面板的打开权限。不是所有人都可以打开面板。可以操作后台任务。
            app.UseHangfireDashboard("/hangfire", new DashboardOptions
            {
                //方法1
                Authorization = new[]
               {
                 new BasicAuthAuthorizationFilter(new BasicAuthAuthorizationFilterOptions
                 {
                    SslRedirect = false,          // 是否将所有非SSL请求重定向到SSL URL
                    RequireSsl = false,           // 需要SSL连接才能访问HangFire Dahsboard。强烈建议在使用基本身份验证时使用SSL
                    LoginCaseSensitive = false,   //登录检查是否区分大小写
                    Users = new[]
                    {
                        new BasicAuthAuthorizationUser
                        {
                            Login =configuration["HangFire:user"],//用户名
                            PasswordClear=configuration["HangFire:password"]
                            // Password as SHA1 hash
                            //Password=new byte[]{ 0xf3,0xfa，，0xd1 }
                        }
                    }
                 })
               },

            });

        }

        /// <summary>
        /// Jobs配置
        /// </summary>
        public static void HangFireJobsSetup()
        { 
            var jobId = BackgroundJob.Schedule(
            () => Console.WriteLine("Delayed!"),
                TimeSpan.FromDays(7));
            RecurringJob.AddOrUpdate(() => Console.WriteLine("123"), Cron.Weekly);

            BackgroundJob.ContinueJobWith(jobId, () => Console.WriteLine("BackgroundJob"));
        }
    }
}
