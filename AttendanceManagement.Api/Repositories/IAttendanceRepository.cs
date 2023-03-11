using AttendanceManagement.Api.Dtos;
using AttendanceManagement.Api.Entities;

namespace AttendanceManagement.Api.Repositories
{
    public interface IAttendanceRepository
    {
        Task<AttendanceDto> CreateAsync(AttendanceCreateDto dtoModel, ApplicationUser user);
        Task UpdateAsync(string id, Attendance attendance);
        Task<Attendance?> GetAsync(string id);
        Task<IEnumerable<AttendanceDto>> GetUserAttendancesByMonthAndYearAsync(string username, int month, int year);
    }
}
