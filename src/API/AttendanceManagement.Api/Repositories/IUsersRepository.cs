using AttendanceManagement.Api.Dtos;
using AttendanceManagement.Api.Entities;
using Microsoft.AspNetCore.Identity;

namespace AttendanceManagement.Api.Repositories
{
    public interface IUsersRepository<T>
    {
        Task<IdentityResult> CreateAsync(RegisterUserDto userDto);
        Task<IdentityResult> UpdateAsync(T user);
        Task<T?> GetByUserNameAsync(string? username);
        Task<T?> GetByEmailAsync(string? email);
        Task<bool> VerifyPasswordAsync(T user, string? password);
        Task<IdentityResult> ChangePasswordAsync(T user, string oldPassword, string newPassword);
    }
}
