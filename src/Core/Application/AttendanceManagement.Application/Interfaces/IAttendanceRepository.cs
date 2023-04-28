using AttendanceManagement.Application.Dtos;

namespace AttendanceManagement.Application.Interfaces
{
    public interface IAttendanceRepository<T>
    {
        Task<AttendanceDto> CreateAsync(T attendance);
        Task UpdateAsync(string id, T attendance);
        Task<T?> GetAsync(string id);
        Task<IEnumerable<AttendanceDto>> GetUserMonthlyAttendancesAsync(string userId, DateTime datetime);
        IEnumerable<MonthlyAttendanceGroupDto> GetUserYearlyAttendancesAsync(string username, int year);
    }
}
