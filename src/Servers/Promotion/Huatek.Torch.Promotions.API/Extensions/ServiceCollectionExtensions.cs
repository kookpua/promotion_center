using Huatek.Torch.Promotions.Infrastructure;
using Huatek.Torch.Promotions.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.Extensions.DependencyInjection;
using Huatek.Torch.Promotions.Service;
using MediatR;
using Huatek.Torch.Promotions.Domain.PromotionAggregate;
using Microsoft.Extensions.Configuration;
using System.Reflection;
using System.IO;
using Huatek.Torch.Domain.Abstractions;
using Microsoft.OpenApi.Models;

namespace Huatek.Torch.Promotions.API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMediatRServices(this IServiceCollection services)
        {
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(PromotionContextTransactionBehavior<,>));
            return services.AddMediatR(typeof(Promotion).Assembly, typeof(Program).Assembly);
        }
        public static IServiceCollection AddDomainContext(this IServiceCollection services,
            Action<DbContextOptionsBuilder> optionsAction)
        {
            return services.AddDbContext<PromotionContext>(optionsAction);
        }

        /// <summary>
        /// sql server
        /// </summary>
        /// <param name="services"></param>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public static IServiceCollection AddMSSqlDomainContext(this IServiceCollection services, 
            string connectionString)
        {
            return services.AddDomainContext(builder =>
            {
                builder.UseSqlServer(connectionString);
            });
        }
        /// <summary>
         /// mysql
         /// </summary>
         /// <param name="services"></param>
         /// <param name="connectionString"></param>
         /// <returns></returns>
        public static IServiceCollection AddMySqlDomainContext(this IServiceCollection services,
           string connectionString)
        {
            return services.AddDomainContext(builder =>
            {
                builder.UseMySQL(connectionString);
            });
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IPromotionRepository, PromotionRepository>();
            return services;
        }
        public static IServiceCollection AddEventBus(this IServiceCollection services, IConfiguration configuration)
        {
            //services.AddTransient<ISubscriberService, SubscriberService>();
            services.AddCap(options =>
            {
                options.UseEntityFramework<PromotionContext>();

                //options.UseRabbitMQ(options =>
                //{
                //    configuration.GetSection("RabbitMQ").Bind(options);
                //});
                //options.UseDashboard();
            });

            return services;
        }
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IPromotionService, PromotionService>();
            return services;
        }

        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Promotion API", Version = "v1" });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            return services;

        }
    }

}
