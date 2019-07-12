using AutoMapper;
using FluentValidation.AspNetCore;
using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Netcorewebapi.DataAccess.Core;
using Netcorewebapi.DataAccess.Data;
using Netcorewebapi.DataAccess.Persistence;
using Newtonsoft.Json;
using System;

namespace Netcorewebapi.Api
{
    public class Startup
    {
        private readonly IConfiguration _config;
        private readonly IHostingEnvironment _env;

        public Startup(IConfiguration config, IHostingEnvironment env)
        {
            _config = config;
            _env = env;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddMvcCore()
                .AddFluentValidation(conf=>
                {
                    conf.RegisterValidatorsFromAssemblyContaining<Startup>();
                })
                .AddJsonFormatters()
                .AddJsonOptions(opt => opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);
            services.AddDbContext<ApplicationDbContext>(cfg =>
            {
                cfg.UseSqlServer(_config.GetConnectionString("DefaultConnection"));
            });


            services.AddProblemDetails(
                setup =>
                {
                    setup.IncludeExceptionDetails = _ => _env.IsDevelopment(); 

                });

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
           
            //services.AddSingleton<IHttpErrorFactory,DefaultHttpErrorFactory>();
            services.AddTransient<StoreTreat>();
            services.AddScoped<IRepository, Repository>();
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();

            services.AddScoped<IUrlHelper>(implementationFactory =>
            {
                var actionContext = implementationFactory.GetService<IActionContextAccessor>()
                    .ActionContext;
                return new UrlHelper(actionContext);
            });
            


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                //app.UseDeveloperExceptionPage();
                using (var scope = app.ApplicationServices.CreateScope())
                {
                    var seeder = scope.ServiceProvider.GetService<StoreTreat>();
                    seeder.Seed();
                }
            }
            else
            {
                app.UseHsts();
            }


            //app.UseMiddleware<ErrorHandlerMiddleware>();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseProblemDetails();
            app.UseMvc();

        }
    }
}
