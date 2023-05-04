using AttendanceManagement.Application.Dtos;
using AttendanceManagement.Application.Interfaces;
using AttendanceManagement.Domain.Entities;
using AttendanceManagement.Infrastructure.Identity;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AttendanceManagement.Infrastructure.Data.Repositories
{
    public class AttendanceRepository : IAttendanceRepository<Attendance>
    {
        private readonly AppIdentityDbContext _dbContext;
        private readonly IMapper _mapper;

        public AttendanceRepository(AppIdentityDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<AttendanceDto> CreateAsync(Attendance attendance)
        {
            attendance.Id = Guid.NewGuid().ToString();

            await _dbContext.Attendances.AddAsync(attendance);
            await _dbContext.SaveChangesAsync();

            return _mapper.Map<AttendanceDto>(attendance);
        }

        public async Task<Attendance?> GetAsync(string id)
        {
            return await _dbContext.Attendances
                .FirstOrDefaultAsync(attendance => attendance.Id == id);
        }

        public async Task<IEnumerable<AttendanceDto>> GetUserMonthlyAttendancesAsync(string userId, DateTime dateTime)
        {
            return await _dbContext.Attendances.Where(attendance =>
                    attendance.UserId == userId && attendance.Month == dateTime.Month &&
                    attendance.Year == dateTime.Year)
                .OrderByDescending(attendance => attendance.EntryDate)
                .Select(attendance => _mapper.Map<AttendanceDto>(attendance))
                .ToListAsync();
        }

        public async Task<IEnumerable<MonthlyAttendanceGroupDto>> GetUserYearlyAttendancesAsync(string userId, int year)
        {
            // TODO: Need to make the method awaitable and query simpler. Currently making this method awaitable throws an exception. 
            return await _dbContext.Attendances
                .Where(attendance => attendance.UserId == userId && attendance.Year == year)
                .GroupBy(attendance => attendance.Month)
                .Select(group => new MonthlyAttendanceGroupDto
                {
                    Month = group.Key,
                    Attendances = group.OrderBy(a => a.EntryTimestamp)
                        .Select(attendance => _mapper.Map<AttendanceDto>(attendance)).ToList()
                }).ToListAsync();
        }

        public async Task UpdateAsync(string id, Attendance attendance)
        {
            _dbContext.Entry(attendance).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }
    }
}
