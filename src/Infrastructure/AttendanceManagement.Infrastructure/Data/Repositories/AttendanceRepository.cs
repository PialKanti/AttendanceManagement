using AttendanceManagement.Application.Interfaces;
using AttendanceManagement.Domain.Entities;
using AttendanceManagement.Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;

namespace AttendanceManagement.Infrastructure.Data.Repositories
{
    public class AttendanceRepository : IAttendanceRepository<Attendance>
    {
        private readonly AppIdentityDbContext _dbContext;

        public AttendanceRepository(AppIdentityDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Attendance> CreateAsync(Attendance attendance)
        {
            attendance.Id = Guid.NewGuid().ToString();

            await _dbContext.Attendances.AddAsync(attendance);
            await _dbContext.SaveChangesAsync();

            return attendance;
        }

        public async Task<Attendance?> GetAsync(string id)
        {
            return await _dbContext.Attendances
                .FirstOrDefaultAsync(attendance => attendance.Id == id);
        }

        public async Task<IEnumerable<Attendance>> GetUserMonthlyAttendancesAsync(string userId, DateTime dateTime)
        {
            return await _dbContext.Attendances.Where(attendance =>
                    attendance.UserId == userId && attendance.Month == dateTime.Month &&
                    attendance.Year == dateTime.Year)
                .OrderByDescending(attendance => attendance.EntryDate)
                .ToListAsync();
        }

        public async Task UpdateAsync(string id, Attendance attendance)
        {
            _dbContext.Entry(attendance).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }
    }
}
