using AttendanceManagement.Api.Dtos;

namespace AttendanceManagement.Api.Repositories
{
    public interface IAttendanceRepository
    {
        void Create(AttendanceCreateDto dtoModel);
    }
}
