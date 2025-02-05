using FluentValidation;
using FluentValidation.AspNetCore;
using Hogarth.UserManagement.Application.DTOs.User;

namespace Hogarth.UserManagement.API.Extensions
{
    public static class FluentValidatorServiceExtension
    {
        public static IServiceCollection AddFluentValidatorServiceExtension(this IServiceCollection services)
        {
            services.AddFluentValidationAutoValidation(config =>
            {
                config.DisableDataAnnotationsValidation = true;
            });

            services.AddValidatorsFromAssemblyContaining<UserDto>();

            return services;
        }
    }
}
