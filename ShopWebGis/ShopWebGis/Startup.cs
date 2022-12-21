using Autofac;
using DotXxlJob.Core;
using FastLambda;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Nacos.AspNetCore.V2;
using Serilog;
using ShopWebData.config;
using ShopWebGis.Filters;
using ShopWebGis.HttApi.Host.Extension;
using ShopWebGisDomain.config;
using ShopWebGisDomainShare.Extension;
using ShopWebGisElasticSearch;
using ShopWebGisEntityFrameWorkCore.EntityFrameWorkCore;
using ShopWebGisFreeSql.config;
using ShopWebGisFreeSql.InterFace;
using ShopWebGisIoc;
using ShopWebGisJwt.config;
using ShopWebGisLogger.Adpter;
using ShopWebGisLogger.Factory;
using ShopWebGisMongoDB.MongoDBConfig;
using ShopWebGisRedis.config;
using ShopWebGisXxlJob.config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.Encodings.Web;
using System.Text.Unicode;
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
            
            services.ShopWebGisRedisSetup(Configuration);
            services.ShopWebGisFreeSqlSetup(Configuration);
            services.AddSingleton<IElasticSearchFactory, ElasticSearchFactory>();// ES工厂接口
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
            services.Configure<RedisConfiguration>(Configuration.GetSection("RedisConfiguration"));
            services.Configure<EalsticSearchLogOption>(Configuration.GetSection("ElasticSearch"));
            services.ShopWebGisJwtSetup(Configuration); // JWT鉴权
            services.AddXxlJobExecutor(Configuration);// XXLJob执行器调度
            services.AddAutoRegistry(); // 自动注册
            services.XxlJobServiceSetup();// XXLJob定时任务注册
            services.AddAutoMapper(Assembly.Load("ShopWebGisApplicationContract"));
            //services.HangFireServiceSetup(Configuration);
            services.Configure<KestrelServerOptions>(x => x.AllowSynchronousIO = true);
            services.AddSingleton<IEvaluator, FastEvaluator>();
            services.DataSetup();
            services.AddControllersWithViews().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Encoder = JavaScriptEncoder.Create(UnicodeRanges.All);
            });
            services.AddControllers(option =>
            {
                option.Filters.Add(new ResultFilter());
                option.Filters.Add(typeof(CustomerExceptionFilter));
            });
            services.AddHttpContextAccessor();
            services.AddNacosAspNet(Configuration);// Nacos服务注册

            //services.AddHangfireServer();//启动hangfire服务

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHostApplicationLifetime applicationLifetime)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseElasticSearchRequestLogging(option =>
            {
                option.Url = Configuration["ElasticSearch:Url"];
            });
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
            //app.ServiceRegister(applicationLifetime, Configuration);

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
            containerBuilder.RegisterModule<RedisCacheAutofacSetup>();
        }
    }
}
