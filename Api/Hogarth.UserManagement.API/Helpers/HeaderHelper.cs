namespace Hogarth.UserManagement.API.Helpers
{
    public static class HeaderHelper
    {
        public static string? GetDataSourceType(IHttpContextAccessor httpContextAccessor)
        {
            var httpContext = httpContextAccessor.HttpContext;
            return httpContext?.Request.Headers["database-type"].ToString();
        }
    }
}
