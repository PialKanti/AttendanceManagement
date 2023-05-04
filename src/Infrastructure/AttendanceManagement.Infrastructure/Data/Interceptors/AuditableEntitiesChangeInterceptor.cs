using AttendanceManagement.Domain.Entities;
using AttendanceManagement.Infrastructure.ValueEncryptors;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace AttendanceManagement.Infrastructure.Data.Interceptors
{
    public class AuditableEntitiesChangeInterceptor : SaveChangesInterceptor
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuditableEntitiesChangeInterceptor(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            DbContext? dbContext = eventData.Context;

            if(dbContext == null)
            {
                return base.SavingChangesAsync(eventData, result, cancellationToken);
            }

            var entities = dbContext.ChangeTracker.Entries<IAuditableEntity>();

            var httpContext = _httpContextAccessor.HttpContext;
            var encryptor = new Base64ValueEncryptor();
            string username = encryptor.Decrypt(httpContext.Request.Cookies["X-User"]);

            foreach (var entity in entities)
            {
                if (entity.State == EntityState.Added)
                {
                    entity.Property(e => e.CreatedOn).CurrentValue = DateTime.UtcNow;
                    entity.Property(e => e.CreatedBy).CurrentValue = username;
                }
                else if(entity.State == EntityState.Modified)
                {
                    entity.Property(e => e.LastModifiedOn).CurrentValue = DateTime.UtcNow;
                    entity.Property(e => e.LastModifiedBy).CurrentValue = username;
                }
            }

            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }
    }
}
