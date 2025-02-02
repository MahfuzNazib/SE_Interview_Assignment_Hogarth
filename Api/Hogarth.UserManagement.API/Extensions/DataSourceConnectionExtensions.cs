using Hogarth.UserManagement.Infrastructure.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace Hogarth.UserManagement.API.Extensions
{
    public static class DataSourceConnectionExtensions
    {
        public static IServiceCollection AddDatabaseConnectionExtensions(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationEFCoreDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"))
            );

            return services;
        }
    }
}
