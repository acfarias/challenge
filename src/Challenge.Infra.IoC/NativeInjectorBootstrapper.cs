using Challenge.Domain.Interfaces;
using Challenge.Domain.Interfaces.Repository;
using Challenge.Domain.Notifications;
using Challenge.Infra.Data;
using Challenge.Infra.Data.Context;
using Challenge.Services.Dtos.Commands;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Challenge.Infra.IoC
{
    public static class NativeInjectorBootstrapper
    {
        public static void RegisterLocalServices(this IServiceCollection services)
        {
            services.AddScoped<NotificationContext>();
            services.AddScoped<ICensusContext, CensusContext>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<ICensusRepository, CensusRepository>();
            services.AddHttpClient();

            #region Validations
            services.AddTransient<IValidator<CreateCensusCommand>, CreateCensusCommandValidations>();
            #endregion
        }
    }
}
