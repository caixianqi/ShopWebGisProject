using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Compact;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ShopWebGis
{
    public class Program
    {
        
        public static void Main(string[] args)
        {
            SerilogSetup();
            try
            {
                Log.Information("Starting WebGebGis.Host");
                CreateHostBuilder(args).Build().Run();

            }catch(Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly!");
            }
            finally{
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseUrls("http://*:5000").UseStartup<Startup>();
                }).UseServiceProviderFactory(new AutofacServiceProviderFactory()).UseSerilog();

        /// <summary>
        ///  serilog配置
        /// </summary>
        public static void SerilogSetup()
        {
            string LogFilePath(string LogEvent) => $@"{AppContext.BaseDirectory}Logs\{LogEvent}\log.log";
            // Serilog 配置信息加载
            var serilogConfig = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("serilog.json")
                .Build();
            string SerilogOutputTemplate = "{NewLine}{NewLine}Date：{Timestamp:yyyy-MM-dd HH:mm:ss.fff}{NewLine}LogLevel：{Level}{NewLine}Message：{Message}{NewLine}{Exception}" + new string('-', 50);

            Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(serilogConfig)
                         .MinimumLevel.Debug()
                         .Enrich.FromLogContext()//使用Serilog.Context.LogContext中的属性丰富日志事件。
                         .WriteTo.Console()
                         .WriteTo.Logger(lg => lg.Filter.ByIncludingOnly(p => p.Level == LogEventLevel.Debug).WriteTo.File(LogFilePath("Debug"), rollingInterval: RollingInterval.Day, outputTemplate: SerilogOutputTemplate))
                         .WriteTo.Logger(lg => lg.Filter.ByIncludingOnly(p => p.Level == LogEventLevel.Information).WriteTo.File(LogFilePath("Information"), rollingInterval: RollingInterval.Day, outputTemplate: SerilogOutputTemplate))
                         .WriteTo.Logger(lg => lg.Filter.ByIncludingOnly(p => p.Level == LogEventLevel.Warning).WriteTo.File(LogFilePath("Warning"), rollingInterval: RollingInterval.Day, outputTemplate: SerilogOutputTemplate))
                         .WriteTo.Logger(lg => lg.Filter.ByIncludingOnly(p => p.Level == LogEventLevel.Error).WriteTo.File(LogFilePath("Error"), rollingInterval: RollingInterval.Day, outputTemplate: SerilogOutputTemplate))
                         .WriteTo.Logger(lg => lg.Filter.ByIncludingOnly(p => p.Level == LogEventLevel.Fatal).WriteTo.File(LogFilePath("Fatal"), rollingInterval: RollingInterval.Day, outputTemplate: SerilogOutputTemplate))
                         .CreateLogger();
        }
    }
}
