using System;
using System.IO;
using System.Reflection;
using Autofac;
using Huatek.Torch.Promotions.API.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

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
            services.AddControllers().AddNewtonsoftJson(); //支持构造函数序列化


            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Promotion API", Version = "v1" });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            //services.AddMediatRServices();
            services.AddMySqlDomainContext(Configuration.GetValue<string>("MsSql"));
            services.AddRepositories();
            //services.AddEventBus(Configuration); 
            //services.AddServices();

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
