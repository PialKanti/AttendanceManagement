using AttendanceManagement.Api.Dtos;
using AttendanceManagement.Api.Entities;

namespace AttendanceManagement.Api.Repositories
{
    public interface IAttendanceRepository
    {
        Task<AttendanceDto> CreateAsync(AttendanceCreateDto dtoModel, ApplicationUser user);
        Task<AttendanceDto?> GetAsync(string id);
    }
}
