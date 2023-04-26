namespace AttendanceManagement.Application.Interfaces
{
    public interface IAttendanceRepository<T>
    {
        Task<T> CreateAsync(T attendance);
        Task UpdateAsync(string id, T attendance);
        Task<T?> GetAsync(string id);
        Task<IEnumerable<T>> GetUserMonthlyAttendancesAsync(string userId, DateTime datetime);
        //IEnumerable<MonthlyAttendanceGroupDto> GetUserYearlyAttendancesAsync(string username, int year);
    }
}
