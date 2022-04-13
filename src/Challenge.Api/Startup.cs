using Challenge.Api.Config;
using Challenge.Api.Filters;
using Challenge.Domain.Core.Configurations;
using Challenge.Infra.IoC;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace Challenge.Api
{
    public class Startup
    {
        public Startup(IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                    .SetBasePath(env.ContentRootPath)
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: false)
                    .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }


        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSwagger("Challenge.Api v1");

            services.AddControllers(c =>
            {
                c.Filters.Add(typeof(ActionFilter));
                c.Filters.Add(typeof(NotificationFilter));
            })
             .AddFluentValidation();

            services.RegisterLocalServices();
            services.Configure<AppSettingsConfigurations>(Configuration);
            services.AddMediatR(AppDomain.CurrentDomain.Load("Challenge.Services"));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger("Challenge.Api v1");
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
