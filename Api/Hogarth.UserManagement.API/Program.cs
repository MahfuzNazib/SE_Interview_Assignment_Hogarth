using Hogarth.UserManagement.API.AppExceptionHandler;
using Hogarth.UserManagement.API.Extensions;
using Hogarth.UserManagement.Application.Mapping;

namespace Hogarth.UserManagement.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Host.AddSerilogLogging();

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            #region Dependency Injection Extensions Register
            builder.Services.AddDatabaseConnectionExtensions(builder.Configuration);
            builder.Services.AddAutoMapper(typeof(MappingProfile));
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddUserServiceExtensions(builder.Configuration);
            builder.Services.AddCorsPolicyServiceExtensions("corspolicy", "http://localhost:4200");
            builder.Services.AddFluentValidatorServiceExtension();
            #endregion

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseCors("corspolicy");
            app.UseMiddleware<ExceptionHandlingMiddleware>();
            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}
