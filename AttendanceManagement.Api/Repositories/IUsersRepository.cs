using AttendanceManagement.Api.Dtos;
using AttendanceManagement.Api.Entities;
using Microsoft.AspNetCore.Identity;

namespace AttendanceManagement.Api.Repositories
{
    public interface IUsersRepository<T>
    {
        Task<IdentityResult> Create(RegisterUserDto userDto);
        Task<IdentityResult> Update(T user);
        Task<T?> GetByUserName(string? username);
        Task<T?> GetByEmail(string? email);
        Task<bool> VerifyPassword(T user, string? password);
    }
}
