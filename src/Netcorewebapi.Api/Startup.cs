﻿using FluentValidation.AspNetCore;
using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Netcorewebapi.DataAccess.Data;
using NetcorewebApi.Api.Extensions;

namespace Netcorewebapi.Api
{
    public class Startup
    {
        private readonly IConfiguration _config;
        private readonly IHostEnvironment _env;

        public Startup(IConfiguration config, IHostEnvironment env)
        {
            _config = config;
            _env = env;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvcCore(options => options.EnableEndpointRouting = false)
                    .AddApiExplorer()
                    .AddFluentValidation(conf =>
                    {
                        conf.RegisterValidatorsFromAssemblyContaining<Startup>();
                    })
                    .AddNewtonsoftJson();

                    services.AddAppContext(_config)
                    .AddCustomServices(_env)
                    .AddSwagger();
            
            services.AddAuthorization();
            services.AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", opt =>
                {
                    opt.Authority = "http://localhost:5000";
                    opt.RequireHttpsMetadata = false;
                    opt.Audience = "netcoreapi";
                });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public static void Configure(IApplicationBuilder app, IHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                //app.UseDeveloperExceptionPage();
                using (var scope = app?.ApplicationServices.CreateScope())
                {
                    var seeder = scope.ServiceProvider.GetService<StoreTreat>();
                    seeder.Seed();
                }
            }
            else
            {
                app.UseHsts();
            }

            app.UseAuthentication();
            app.UseAuthorization();
            //app.UseMiddleware<ErrorHandlerMiddleware>();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            
            
            app.UseProblemDetails();


            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "NetcorewebApi");
            });
            app.UseMvc();

        }
    }
}
