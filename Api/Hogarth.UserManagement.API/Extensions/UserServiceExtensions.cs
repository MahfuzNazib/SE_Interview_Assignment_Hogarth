using Hogarth.UserManagement.Application.IService.Users;
using Hogarth.UserManagement.Application.Service.Users;
using Hogarth.UserManagement.Domain.IRepository.Users;
using Hogarth.UserManagement.Infrastructure.Repository.Users;

namespace Hogarth.UserManagement.API.Extensions
{
    public static class UserServiceExtensions
    {
        public static IServiceCollection AddUserServiceExtensions(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IUserService, UserService>();

            var dataSource = configuration["DataSource"];

            if (dataSource == "MSSQL")
            {
                services.AddScoped<IUserRepository, UserRepository>();
            }
            if (dataSource == "JSON")
            {
                services.AddScoped<IUserRepository, UserJsonRepository>(provider =>
                    new UserJsonRepository(configuration));
            }

            return services;
        }
    }
}
