using AttendanceManagement.Api.Dtos;
using AttendanceManagement.Api.Entities;
using AttendanceManagement.Api.Enums;
using AutoMapper;
using Microsoft.AspNetCore.Identity;

namespace AttendanceManagement.Api.Repositories
{
    public class UsersRepository : IUsersRepository<ApplicationUser>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;

        public UsersRepository(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IMapper mapper)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
        }

        public async Task<IdentityResult> Create(RegisterUserDto userDto)
        {
            var user = _mapper.Map<ApplicationUser>(userDto);
            var result = await _userManager.CreateAsync(user, userDto.Password);
            if (result.Succeeded)
            {
                await AssignRole(user, Roles.Employee.ToString());
            }
            return result;
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

        public async Task<ApplicationUser?> GetByEmail(string? email)
        {
            if (string.IsNullOrEmpty(email))
                return null;

            return await _userManager.FindByEmailAsync(email);
        }

        public async Task<ApplicationUser?> GetByUserNameAsync(string? username)
        {
            if (string.IsNullOrEmpty(username))
                return null;

            return await _userManager.FindByNameAsync(username);
        }

        public async Task<bool> VerifyPasswordAsync(ApplicationUser user, string? password)
        {
            if (user == null)
                return false;

            if (string.IsNullOrEmpty(password))
                return false;

            return await _userManager.CheckPasswordAsync(user, password);
        }

        public async Task<IdentityResult> UpdateAsync(ApplicationUser user)
        {
            return await _userManager.UpdateAsync(user);
        }

        public Task<IdentityResult> ChangePasswordAsync(ApplicationUser user, string oldPassword, string newPassword)
        {
            return _userManager.ChangePasswordAsync(user, oldPassword, newPassword);
        }
    }
}
