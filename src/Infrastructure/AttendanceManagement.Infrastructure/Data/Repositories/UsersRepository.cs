using AttendanceManagement.Application.Interfaces;
using AttendanceManagement.Infrastructure.Enums;
using AttendanceManagement.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;

namespace AttendanceManagement.Infrastructure.Data.Repositories
{
    public class UsersRepository : IUsersRepository<ApplicationUser>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UsersRepository(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<bool> ChangePasswordAsync(ApplicationUser user, string oldPassword, string newPassword)
        {
            var result = await _userManager.ChangePasswordAsync(user, oldPassword, newPassword);
            return result?.Succeeded ?? false;
        }

        public async Task<ApplicationUser> CreateAsync(ApplicationUser user, string password)
        {
            var result = await _userManager.CreateAsync(user, password);
            if (result.Succeeded)
            {
                await AssignRole(user, Roles.Employee.ToString());
            }
            return user;
        }

        private async Task AssignRole(ApplicationUser user, string roleName)
        {
            var role = await _roleManager.FindByNameAsync(roleName);
            if (role != null)
            {
                await _userManager.AddToRoleAsync(user, roleName);
            }
            else
            {
                throw new Exception("Role is not created");
            }
        }

        public async Task<ApplicationUser?> GetByEmailAsync(string? email)
        {
            return string.IsNullOrEmpty(email) ? null : await _userManager.FindByEmailAsync(email);
        }

        public async Task<ApplicationUser?> GetByUserNameAsync(string? username)
        {
            return string.IsNullOrEmpty(username) ? null : await _userManager.FindByNameAsync(username);
        }

        public async Task<ApplicationUser> UpdateAsync(ApplicationUser user)
        {
            await _userManager.UpdateAsync(user);
            return user;
        }

        public async Task<bool> VerifyPasswordAsync(ApplicationUser user, string? password)
        {
            return !string.IsNullOrEmpty(password) && await _userManager.CheckPasswordAsync(user, password);
        }
    }
}
