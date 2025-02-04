namespace Hogarth.UserManagement.API.Extensions
{
    public static class CorsPolicyExtensions
    {
        public static IServiceCollection AddCorsPolicyServiceExtensions(this IServiceCollection services, string policyName, string origin)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(policyName, policyBuilder =>
                {
                    policyBuilder.WithOrigins(origin)
                                 .AllowAnyMethod()
                                 .AllowAnyHeader();
                });
            });

            return services;
        }
    }
}
