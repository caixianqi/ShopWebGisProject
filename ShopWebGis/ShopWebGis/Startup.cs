using Autofac;
using DotXxlJob.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Nacos.AspNetCore.V2;
using Serilog;
using ShopWebGis.Filters;
using ShopWebGisDomain.config;
using ShopWebGisDomainShare.Extension;
using ShopWebGisEntityFrameWorkCore.EntityFrameWorkCore;
using ShopWebGisFreeSql.config;
using ShopWebGisFreeSql.InterFace;
using ShopWebGisIoc;
using ShopWebGisJwt.config;
using ShopWebGisMongoDB.MongoDBConfig;
using ShopWebGisXxlJob.config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopWebGis
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddDistributedRedisCache(options =>
            {
                options.Configuration = Configuration["Redis:Configuration"];
                options.InstanceName = "";
            });
            services.ShopWebGisFreeSqlSetup(Configuration);
            services.AddSingleton(new SubSetting());
            services.AddSingleton<ISubRuleResolver>(new SubRuleResolver(new ConfigurationBuilder().AddJsonFile("subsetting.json", true, true).Build()));
            services.ShopWebGisMongoDBConfigureServices(Configuration);
            services.AddDbContext<ShopWebGisDbContext>(options => options.UseMySQL(Configuration.GetConnectionString("Mysql")));
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("V1", new OpenApiInfo
                {
                    Title = "Swagger接口文档",
                    Version = "v1",
                    Description = $"Core.WebApi HTTP API V1",
                });
                c.OrderActionsBy(o => o.RelativePath);
            });
            services.Configure<Jwt>(Configuration.GetSection("Jwt"));
            services.ShopWebGisJwtSetup(Configuration); // JWT鉴权
            services.AddXxlJobExecutor(Configuration);// XXLJob执行器调度
            services.AddAutoRegistry(); // 自动注册
            services.XxlJobServiceSetup();// XXLJob定时任务注册
            //services.HangFireServiceSetup(Configuration);
            services.AddControllers();
            services.AddControllers(option =>
            {
                option.Filters.Add(new ResultFilter());
                option.Filters.Add(new CustomerExceptionFilter());
            });
            services.AddHttpContextAccessor();
            services.AddNacosAspNet(Configuration);// Nacos服务注册
            services.AddAutoMapper(typeof(Startup));
            //services.AddHangfireServer();//启动hangfire服务

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHostApplicationLifetime applicationLifetime)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSerilogRequestLogging();
            app.UseAuthentication();

            app.UseRouting();

            app.UseAuthorization();
            app.UseXxlJobExecutor();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseSwagger();
            app.ServiceRegister(applicationLifetime, Configuration);
            
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"/swagger/V1/swagger.json", "WebApi.Core V1");
                c.RoutePrefix = "";
            });
           // app.HangFireConfiguraSetup(Configuration);   //使用hangfire面板
            //HangFireSetup.HangFireJobsSetup();
            ServiceManager.Init(app.ApplicationServices);
        }

        public void ConfigureContainer(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterModule<RepositoryAutofacSetup>();
            containerBuilder.RegisterModule<MongoDBRepositoryAutofacSetup>();
            containerBuilder.RegisterModule<ApplicationAutofacSetup>();
        }
    }
}
