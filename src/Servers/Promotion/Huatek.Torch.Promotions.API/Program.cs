using System.IO;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.MSSqlServer.Sinks.MSSqlServer.Options;

namespace Huatek.Torch.Promotions.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //拿到appsettings.json的配置
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            var config = builder.Build();

            /*
             Install-Package Serilog
             Install-Package Serilog.AspNetCore
             Install-Package Serilog.Settings.Configuration
             Install-Package Serilog.Sinks.Console
             Install-Package Serilog.Sinks.MssqlServer
             Install-Package Serilog.Sinks.Email
             Install-Package Serilog.Sinks.File
             Install-Package Serilog.Sinks.RollingFile
             Install-Package Serilog.Sinks.Elasticsearch
             */
            //Serilog与微软ILogger进行整合
            Log.Logger = new LoggerConfiguration()
                //.MinimumLevel.Information()
                //.MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .ReadFrom.Configuration(config)
                .WriteTo.MSSqlServer(config["MsSql"],
                    sinkOptions: new SinkOptions { TableName = "logs", AutoCreateSqlTable = true },
                    restrictedToMinimumLevel: LogEventLevel.Information)
               .CreateLogger();


            #region Serilog demo output json str 已注释
            //var position = new { Latitude = 25, Longitude = 134 };
            //var elapsedMs = 34;
            //Log.Information("Processed {@Position} in {Elapsed:000} ms.", position, elapsedMs);
            #endregion

            //不初始化数据
            CreateHostBuilder(args).Build().Run();

            #region 已使用数据迁移功能，暂不使用此处的初始化数据 已注释
            //var host = CreateHostBuilder(args).Build();

            //using (var scope = host.Services.CreateScope())
            //{
            //    var services = scope.ServiceProvider;
            //    try
            //    {
            //        var context = services.GetRequiredService<PromotionContext>();
            //        DbDataInitializer.Initialize(context);
            //    }
            //    catch (Exception ex)
            //    {
            //        var logger = services.GetRequiredService<ILogger<Program>>();
            //        logger.LogError(ex, "An error occurred while seeding the database.");
            //    }
            //}

            //host.Run();
            #endregion

        }

        /// <summary>
        /// UseSerilog()在宿主机启动的时候配置serilog,与微软ILogger进行整合
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder
                        .UseContentRoot(Directory.GetCurrentDirectory())
                        .UseSerilog(dispose:true)
                        .UseStartup<Startup>();
                });
    }
}
