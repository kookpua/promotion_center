using System;
using Autofac;
using AutoMapper;
using Huatek.Torch.Promotions.API.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Huatek.Torch.Promotions.API
{
    public class Startup
    {
        public Startup(IWebHostEnvironment env)
        {
            // In ASP.NET Core 3.0 `env` will be an IWebHostEnvironment, not IHostingEnvironment.
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            this.Configuration = builder.Build();
        }


        public IConfigurationRoot Configuration { get; private set; }

        public ILifetimeScope AutofacContainer { get; private set; }

        // ConfigureServices is where you register dependencies. This gets
        // called by the runtime before the ConfigureContainer method, below.
        public void ConfigureServices(IServiceCollection services)
        {

            // Add services to the collection. Don't build or return
            // any IServiceProvider or the ConfigureContainer method
            // won't get called.
            services.AddControllers(options =>
            {
                /*
                 * https://stackoverflow.com/questions/59288259/asp-net-core-3-0-createdataction-returns-no-route-matches-the-supplied-values
                 * https://github.com/dotnet/aspnetcore/issues/15316
                */
                options.SuppressAsyncSuffixInActionNames = false;
            }) .AddNewtonsoftJson(); //支持构造函数序列化


            //services.AddMediatRServices();
            services.AddRepositories();
            services.AddSwagger();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            //services.AddEventBus(Configuration); 
            //services.AddServices();

            if (Configuration.GetValue<string>("WhoSql").Equals("MsSql"))
            {
                services.AddMSSqlDomainContext(Configuration.GetValue<string>("MsSql"));
            }
            else
            {
                services.AddMySqlDomainContext(Configuration.GetValue<string>("MySql"));
            }

        }


        /// <summary>
        /// autofac
        /// https://autofaccn.readthedocs.io/zh/latest/lifetime/index.html
        /// https://autofaccn.readthedocs.io/zh/latest/lifetime/instance-scope.html
        /// ConfigureContainer is where you can register things directly
        /// with Autofac. This runs after ConfigureServices so the things
        /// here will override registrations made in ConfigureServices.
        /// Don't build the container; that gets done for you by the factory.
        /// </summary>
        /// <param name="builder"></param>
        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new PromotionModule());
        }


        // This method gets called by the runtime.
        //Use this method to configure the HTTP request pipeline.
        // Configure is where you add middleware. This is called after
        // ConfigureContainer. You can use IApplicationBuilder.ApplicationServices
        // here if you need to resolve things from the container.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Promotion API V1");
                });
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
