using Autofac;
using DotXxlJob.Core;
using DotXxlJob.Core.Config;
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
using Serilog;
using ShopWebCaching;
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
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using System.Threading.Tasks;
using static ShopWebGis.Controllers.WeatherForecastController;

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
            // ����ȫ�ֻ��滬��,����ʱ��
            //services.Configure<DistributedCacheOptions>(cacheOptions =>
            //{
            //    cacheOptions.GlobalCacheEntryOptions.SlidingExpiration = TimeSpan.FromMinutes(20);
            //}); 
            services.ShopWebGisFreeSqlSetup(Configuration);
            services.AddSingleton<IElasticSearchFactory, ElasticSearchFactory>();// ES�����ӿ�
            services.ShopWebGisMongoDBConfigureServices(Configuration);
            services.AddDbContext<ShopWebGisDbContext>(options => options.UseMySQL(Configuration.GetConnectionString("Mysql")));
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("V1", new OpenApiInfo
                {
                    Title = "Swagger�ӿ��ĵ�",
                    Version = "v1",
                    Description = $"Core.WebApi HTTP API V1",
                });
                c.OrderActionsBy(o => o.RelativePath);
                //���header��֤��Ϣ
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT��Ȩ(���ݽ�������ͷ�н��д���) ֱ�����¿�������Bearer {token}��ע������֮����һ���ո�\"",
                    Name = "Authorization",//jwtĬ�ϵĲ�������
                    In = ParameterLocation.Header,//jwtĬ�ϴ��Authorization��Ϣ��λ��(����ͷ��)
                    Type = SecuritySchemeType.ApiKey
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement {

                { new OpenApiSecurityScheme
                {
                Reference = new OpenApiReference()
                {
                Id = "Bearer",
                Type = ReferenceType.SecurityScheme
                }
                }, Array.Empty<string>() }
                });
            });
            services.Configure<Jwt>(Configuration.GetSection("Jwt"));
            services.Configure<RedisConfiguration>(Configuration.GetSection("RedisConfiguration"));
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = "127.0.0.1:6379";
                options.InstanceName = "App:";
            });
            services.Configure<EalsticSearchLogOption>(Configuration.GetSection("ElasticSearch"));
            services.ShopWebGisJwtSetup(Configuration); // JWT��Ȩ
            services.Configure<XxlJobExecutorOptions>(Configuration.GetSection("xxlJob"));
            services.XxlJobServiceSetup(Configuration);// XXLJob��ʱ����ע��
            services.AddAutoMapper(Assembly.Load("ShopWebGisApplicationContract"));
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
            services.AddNacosAspNetCore(Configuration);// Nacos����ע��
            services.AddHttpClient();
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

            ServiceManager.Init(app.ApplicationServices);



        }

        public void ConfigureContainer(ContainerBuilder containerBuilder)
        {
            var basePath = AppContext.BaseDirectory;
            var assemblyFilePath = Path.Combine(basePath, "ShopWebGisAutofac.dll");
            var assembly = Assembly.LoadFile(assemblyFilePath);//���س���
            foreach (var module in assembly.GetInheritTypes<Autofac.Module>())
            {
                containerBuilder.RegisterModule(module);
            }
        }
    }
}
