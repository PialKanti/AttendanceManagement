using AttendanceManagement.Api.Dtos;
using Microsoft.AspNetCore.Identity;

namespace AttendanceManagement.Api.Repositories
{
    public interface IUsersRepository<T>
    {
        Task<IdentityResult> Create(RegisterUserDto userDto);
        Task<T?> GetByUserName(string? username);
        Task<T?> GetByEmail(string? email);
        Task<bool> VerifyPassword(T user, string? password);
    }
}
