using System.Linq.Expressions;
using AttendanceManagement.Api.Domain;
using AttendanceManagement.Api.Dtos;
using AttendanceManagement.Api.Entities;
using AttendanceManagement.Api.Utils;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AttendanceManagement.Api.Repositories
{
    public class AttendanceRepository : IAttendanceRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public AttendanceRepository(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<AttendanceDto> CreateAsync(AttendanceCreateDto dtoModel, ApplicationUser user)
        {
            var attendance = _mapper.Map<Attendance>(dtoModel);

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
                .Select(attendance => _mapper.Map<AttendanceDto>(attendance))
                .ToListAsync();
        }

        public IEnumerable<MonthlyAttendanceGroupDto> GetUserYearlyAttendancesAsync(string username, int year)
        {
            return _dbContext.Attendances.Include(attendance => attendance.User)
                .Where(attendance => attendance.User.UserName == username && attendance.Year == year)
                .GroupBy(attendance => attendance.Month)
                .Select(group => new MonthlyAttendanceGroupDto
                {
                    Month = group.Key,
                    Attendances = group.OrderBy(a => a.EntryTimestamp).Select(attendance => _mapper.Map<AttendanceDto>(attendance)).ToList()
                }).ToList();
        }

        public async Task UpdateAsync(string id, Attendance attendance)
        {
            _dbContext.Entry(attendance).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }
    }
}
