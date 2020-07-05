using Huatek.Torch.Promotions.Infrastructure;
using Huatek.Torch.Promotions.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.Extensions.DependencyInjection;
using Huatek.Torch.Promotions.Service;
using MediatR;
using Huatek.Torch.Promotions.Domain.PromotionAggregate;
using Microsoft.Extensions.Configuration;

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


        public static IServiceCollection AddMySqlDomainContext(this IServiceCollection services, 
            string connectionString)
        {
            return services.AddDomainContext(builder =>
            {
                builder.UseSqlServer(connectionString);
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
    }

}
