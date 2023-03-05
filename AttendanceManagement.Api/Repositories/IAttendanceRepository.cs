using AttendanceManagement.Api.Dtos;
using AttendanceManagement.Api.Entities;

namespace AttendanceManagement.Api.Repositories
{
    public interface IAttendanceRepository
    {
        Task Create(AttendanceCreateDto dtoModel, ApplicationUser user);
    }
}
