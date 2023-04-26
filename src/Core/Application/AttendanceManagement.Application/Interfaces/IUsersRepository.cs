namespace AttendanceManagement.Application.Interfaces
{
    public interface IUsersRepository<T>
    {
        Task<T> CreateAsync(T user, string password);
        Task<T> UpdateAsync(T user);
        Task<T?> GetByUserNameAsync(string? username);
        Task<T?> GetByEmailAsync(string? email);
        Task<bool> VerifyPasswordAsync(T user, string? password);
        Task<bool> ChangePasswordAsync(T user, string oldPassword, string newPassword);
    }
}
