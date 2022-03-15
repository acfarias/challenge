using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Challenge.Api.Config
{
    public static class StartupSwagger
    {
        public static void AddSwagger(this IServiceCollection services, string apiDescription)
        {
            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = apiDescription,
                    Version = "V1"
                });
            });
        }

        public static void UseSwagger(this IApplicationBuilder app, string apiDescription)
        {
            app.UseSwagger();
            app.UseSwaggerUI(s => s.SwaggerEndpoint("/swagger/v1/swagger.json", apiDescription));
        }
    }
}
