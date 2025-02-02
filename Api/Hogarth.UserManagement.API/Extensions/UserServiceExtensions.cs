using Hogarth.UserManagement.Application.IService.Users;
using Hogarth.UserManagement.Application.Service.Users;
using Hogarth.UserManagement.Domain.IRepository.Users;
using Hogarth.UserManagement.Infrastructure.Repository.Users;

namespace Hogarth.UserManagement.API.Extensions
{
    public static class UserServiceExtensions
    {
        public static IServiceCollection AddUserServiceExtensions(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserRepository, UserRepository>();

            return services;
        }
    }
}
