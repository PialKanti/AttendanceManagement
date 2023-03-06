using AttendanceManagement.Api.Dtos;
using AttendanceManagement.Api.Entities;

namespace AttendanceManagement.Api.Repositories
{
    public interface IAttendanceRepository
    {
        Task<Attendance> CreateAsync(AttendanceCreateDto dtoModel, ApplicationUser user);
        Task<Attendance?> GetAsync(string id);
    }
}
