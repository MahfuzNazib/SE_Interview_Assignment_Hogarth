using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Hogarth.UserManagement.Infrastructure.DatabaseContext;
using Hogarth.UserManagement.Infrastructure.Repository.Users;
using Hogarth.UserManagement.Domain.IRepository.Users;
using Hogarth.UserManagement.Application.IService.Users;
using Hogarth.UserManagement.Application.Service.Users;

public static class UserServiceExtensions
{
    public static IServiceCollection AddUserServiceExtensions(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IUserService, UserService>();
        services.AddHttpContextAccessor(); 
        services.AddDbContext<ApplicationEFCoreDbContext>(); 

        services.AddScoped<IUserRepository>(provider =>
        {
            var httpContextAccessor = provider.GetRequiredService<IHttpContextAccessor>();
            var config = provider.GetRequiredService<IConfiguration>();
            var dbContext = provider.GetRequiredService<ApplicationEFCoreDbContext>();

            // Read database-type from request headers
            var databaseType = httpContextAccessor.HttpContext?.Request.Headers["database-type"].ToString() ?? "MSSQL";

            if (databaseType.Equals("JSON", StringComparison.OrdinalIgnoreCase))
            {
                return new UserJsonRepository(config);
            }

            return new UserRepository(dbContext);
        });

        return services;
    }
}
