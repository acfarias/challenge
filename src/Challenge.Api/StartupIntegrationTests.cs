using Challenge.Api.Filters;
using Challenge.Domain.Core.Configurations;
using Challenge.Infra.IoC;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Challenge.Api
{
    public class StartupIntegrationTests
    {
        private readonly IConfiguration configuration;

        public StartupIntegrationTests(IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile("appsettings.IntegrationTesting.json")
                .AddEnvironmentVariables();

            configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(c =>
            {
                c.Filters.Add(typeof(ActionFilter));
                c.Filters.Add(typeof(NotificationFilter));
            })
             .AddFluentValidation();

            services.RegisterLocalServices();
            services.Configure<AppSettingsConfigurations>(configuration);
            services.AddMediatR(AppDomain.CurrentDomain.Load("Challenge.Services"));
        }

        public void Configure(IApplicationBuilder app)
        {

            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}