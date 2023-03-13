using System.Linq.Expressions;
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
                Month = dtoModel.EntryDateTime.Month,
                Year = dtoModel.EntryDateTime.Year
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

        public async Task<IEnumerable<AttendanceDto>> GetUserMonthlyAttendancesAsync(string username, int month, int year)
        {
            return await _dbContext.Attendances.Include(attendance => attendance.User)
                .Where(attendance => attendance.User.UserName == username && attendance.Month == month && attendance.Year == year)
                .OrderByDescending(attendance => attendance.EntryDate)
                .Select(attendance =>
                        new AttendanceDto
                        {
                            Id = attendance.Id,
                            Username = attendance.User.UserName,
                            EntryDateTime = CommonUtils.GetDateTimeFromTimestamp(attendance.EntryTimestamp),
                            ExitDateTime = CommonUtils.GetDateTimeFromTimestamp(attendance.ExitTimestamp)
                        })
                .ToListAsync();
        }

        public IEnumerable<MonthlyAttendanceGroupDto> GetUserYearlyAttendancesAsync(string username, int year)
        {
            return _dbContext.Attendances.Include(attendance => attendance.User)
                .Where(attendance => attendance.User.UserName == username && attendance.Year == year)
                .GroupBy(attendance => attendance.Month)
                .Select(group => new MonthlyAttendanceGroupDto
                {
                    Month = group.Key, Attendances = group.OrderBy(a => a.EntryTimestamp).Select(attendance =>
                        new AttendanceDto
                        {
                            Id = attendance.Id,
                            Username = attendance.User.UserName,
                            EntryDateTime = CommonUtils.GetDateTimeFromTimestamp(attendance.EntryTimestamp),
                            ExitDateTime = CommonUtils.GetDateTimeFromTimestamp(attendance.ExitTimestamp)
                        }).ToList()
                }).ToList();
        }

        public async Task UpdateAsync(string id, Attendance attendance)
        {
            _dbContext.Entry(attendance).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }
    }
}
