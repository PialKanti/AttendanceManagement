namespace AttendanceManagement.Application.Interfaces
{
    public interface IAttendanceRepository<T>
    {
        Task<T> CreateAsync(T dtoModel, string userId);
        Task UpdateAsync(string id, T attendance);
        Task<T?> GetAsync(string id);
        Task<IEnumerable<T>> GetUserMonthlyAttendancesAsync(string username, int month, int year);
        //IEnumerable<MonthlyAttendanceGroupDto> GetUserYearlyAttendancesAsync(string username, int year);
    }
}
