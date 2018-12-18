using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Netcorewebapi.DataAccess.Core;
using Netcorewebapi.DataAccess.Data;
using Netcorewebapi.DataAccess.Persistence;
using Netcorewebapi.Infrastructure.HttpErrors;
using Netcorewebapi.Middleware;
using Newtonsoft.Json;

namespace Netcorewebapi
{
    public class Startup
    {
        private readonly IConfiguration _config;

        public Startup(IConfiguration config)
        {
            _config = config;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvcCore()
                .AddJsonFormatters()
                .AddJsonOptions(opt => opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);
            services.AddDbContext<ApplicationDbContext>(cfg =>
            {
                cfg.UseSqlServer(_config.GetConnectionString("DefaultConnection"));
            });

            services.AddAutoMapper();

            services.AddSingleton<IHttpErrorFactory,DefaultHttpErrorFactory>();
            services.AddTransient<StoreTreat>();
            services.AddScoped<IRepository, Repository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
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


            app.UseMiddleware<ErrorHandlerMiddleware>();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseMvc();

        }
    }
}
