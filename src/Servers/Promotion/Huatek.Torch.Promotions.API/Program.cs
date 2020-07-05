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
            //�õ�appsettings.json������
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            var config = builder.Build();

            #region   Serilog��΢��ILogger��������

            #region Install-Package
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
            #endregion

            if (config["WhoSql"].Equals("MsSql"))
            {
                Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(config)
                .WriteTo.MSSqlServer(config["MsSql"],
                    sinkOptions: new SinkOptions { TableName = "logs", AutoCreateSqlTable = true },
                    restrictedToMinimumLevel: LogEventLevel.Information)
               .CreateLogger();
            }
            else
            {
                Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(config)
                .WriteTo.MySQL(config["MySql"], "logs", restrictedToMinimumLevel: LogEventLevel.Information)
               .CreateLogger();
            }
            #endregion


            #region Serilog demo output json str ��ע��
            //var position = new { Latitude = 25, Longitude = 134 };
            //var elapsedMs = 34;
            //Log.Information("Processed {@Position} in {Elapsed:000} ms.", position, elapsedMs);
            #endregion

            //����ʼ������
            CreateHostBuilder(args).Build().Run();

            #region ע�͵Ĵ���
            //var host = CreateHostBuilder(args).Build();

            //using (var scope = host.Services.CreateScope())
            //{
            //    var services = scope.ServiceProvider;
            //    try
            //    {
            //        var context = services.GetRequiredService<PromotionContext>();
            //        //��ʼ�����ݿ� 
            //        //��ʹ������Ǩ�ƹ��ܣ��ݲ�ʹ�ô˴��ĳ�ʼ������ ��ע��
            //        DbDataInitializer.Initialize(context);

            //        //ɾ�����ؽ����ݿ�
            //        //context.Database.EnsureDeleted();
            //        //context.Database.Migrate();


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
        /// UseSerilog()��������������ʱ������serilog,��΢��ILogger��������
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
