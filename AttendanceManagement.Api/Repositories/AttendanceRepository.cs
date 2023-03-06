using AttendanceManagement.Api.Domain;
using AttendanceManagement.Api.Dtos;
using AttendanceManagement.Api.Entities;
using AttendanceManagement.Api.Utils;
using Microsoft.EntityFrameworkCore;

namespace AttendanceManagement.Api.Repositories
{
    public class AttendanceRepository : IAttendanceRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public AttendanceRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<AttendanceDto> CreateAsync(AttendanceCreateDto dtoModel, ApplicationUser user)
        {
            var attendance = new Attendance()
            {
                Id = Guid.NewGuid().ToString(),
                User = user,
                EntryTimestamp = CommonUtils.GetUtcTimestamp(dtoModel.EntryDateTime),
                EntryDate = dtoModel.EntryDateTime.Date,
                Month = dtoModel.EntryDateTime.Month
            };

            await _dbContext.Attendances.AddAsync(attendance);
            await _dbContext.SaveChangesAsync();

            return new AttendanceDto
            {
                Id = attendance.Id,
                Username = user.UserName,
                EntryDateTime = CommonUtils.GetDateTimeFromTimestamp(attendance.EntryTimestamp)
            };
        }

        public async Task<Attendance?> GetAsync(string id)
        {
            return await _dbContext.Attendances.Include(attendance => attendance.User)
                .FirstOrDefaultAsync(attendance => attendance.Id == id);
        }

        public async Task<IEnumerable<AttendanceDto>> GetByUsernameAndMonthAsync(string username, int month)
        {
            return await _dbContext.Attendances.Include(attendance => attendance.User)
                .Where(attendance => attendance.User.UserName == username && attendance.Month == month).Select(
                    attendance =>
                        new AttendanceDto
                        {
                            Id = attendance.Id,
                            Username = attendance.User.UserName,
                            EntryDateTime = CommonUtils.GetDateTimeFromTimestamp(attendance.EntryTimestamp)
                        })
                .ToListAsync();
        }

        public async Task UpdateAsync(string id, Attendance attendance)
        {
            _dbContext.Entry(attendance).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }
    }
}
