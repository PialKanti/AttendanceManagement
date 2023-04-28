using AttendanceManagement.Infrastructure.Data.Interceptors;
using AttendanceManagement.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AttendanceManagement.Infrastructure
{
    public static class Dependencies
    {
        public static void ConfigureServices(IConfiguration configuration, IServiceCollection services)
        {
            services.AddSingleton<AuditableEntitiesChangeInterceptor>();

            services.AddDbContext<AppIdentityDbContext>((serviceProvider, options) =>
            {
                var interceptor = serviceProvider.GetService<AuditableEntitiesChangeInterceptor>();

                options.UseSqlServer(configuration.GetConnectionString("AttendanceManagementContext"))
                        .AddInterceptors(interceptor);
            });

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<AppIdentityDbContext>();
        }
    }
}
