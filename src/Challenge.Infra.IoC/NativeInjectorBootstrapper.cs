using Challenge.Domain.Notifications;
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


            #region Validations
            services.AddTransient<IValidator<CreateCensusCommand>, CreateCensusCommandValidations>();
            #endregion
        }
    }
}
