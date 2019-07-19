using AutoMapper;
using FluentValidation.AspNetCore;
using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Netcorewebapi.Api;
using Netcorewebapi.DataAccess.Data;
using Netcorewebapi.DataAccess.Persistence;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Swagger;
using System;
using NetcorewebApi.DataAccess.Core;
using NetcorewebApi.DataAccess.Persistence.Repositories;

namespace NetcorewebApi.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCustomConfig(this IServiceCollection services)
        {
            services
                .AddMvcCore()
                .AddApiExplorer()
                .AddFluentValidation(conf=>
                {
                    conf.RegisterValidatorsFromAssemblyContaining<Startup>();
                })
                .AddJsonFormatters()
                .AddJsonOptions(opt => opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);
            return services;
        }

        public static IServiceCollection AddAppContext(this IServiceCollection services,IConfiguration config)
        {
            services.AddDbContext<ApplicationDbContext>(cfg =>
            {
                cfg.UseSqlServer(config.GetConnectionString("DefaultConnection"));
            });

            return services;
        }

        public static IServiceCollection AddCustomServices(this IServiceCollection services,IHostingEnvironment env)
        {
            services.AddProblemDetails(
                setup =>
                {
                    setup.IncludeExceptionDetails = _ => env.IsDevelopment(); 

                });


            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddTransient<StoreTreat>();
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();

            services.AddScoped<DbContext,ApplicationDbContext>();
            services.AddScoped<IProductRepository,ProductRepository>();
            services.AddScoped<IOrderRepository,OrderRepository>();

            services.AddScoped<IUrlHelper>(implementationFactory =>
            {
                var actionContext = implementationFactory.GetService<IActionContextAccessor>()
                    .ActionContext;
                return new UrlHelper(actionContext);
            });

            return services;
        }

        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info{ Title = "NetcorewebApi", Version = "v1" });
            });
            return services;
        }
    }
}
