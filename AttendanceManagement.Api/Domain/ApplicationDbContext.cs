using AttendanceManagement.Api.Entities;
using AttendanceManagement.Api.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AttendanceManagement.Api.Domain
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Attendance> Attendances { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            var admin = SeedDefaultAdminUser(builder);
            var rolesDictionary = GetRolesDictionary();
            SeedRoles(builder, rolesDictionary.Values.ToArray());
            SeedDefaultAdminUserRole(builder, admin.Id, rolesDictionary[Enums.Roles.Admin.ToString()].Id);
        }

        private ApplicationUser SeedDefaultAdminUser(ModelBuilder builder)
        {
            var user = new ApplicationUser
            {
                Id = Guid.NewGuid().ToString(),
                FirstName = "Default",
                LastName = "Admin",
                UserName = "admin",
                NormalizedUserName = "admin".ToUpper(),
                Email = "admin@test.com",
                NormalizedEmail = "admin@test.com".ToUpper(),
                LockoutEnabled = false,
            };

            var passwordHasher = new PasswordHasher<ApplicationUser>();
            user.PasswordHash = passwordHasher.HashPassword(user, "admin123");

            builder.Entity<ApplicationUser>().HasData(user);

            return user;
        }

        private void SeedRoles(ModelBuilder builder, IdentityRole[] roles)
        {
            builder.Entity<IdentityRole>().HasData(roles);
        }

        private void SeedDefaultAdminUserRole(ModelBuilder builder, string userId, string roleId)
        {
            builder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    UserId = userId,
                    RoleId = roleId
                });
        }

        private Dictionary<string,IdentityRole> GetRolesDictionary()
        {
            return Enum.GetValues<Roles>().Select(GetIdentityRole).ToDictionary(role => role.Name);
        }

        private IdentityRole GetIdentityRole(Roles role)
        {
            return new IdentityRole
            {
                Id = Guid.NewGuid().ToString(),
                Name = role.ToString(),
                NormalizedName = role.ToString().ToUpper()
            };
        }
    }
}
