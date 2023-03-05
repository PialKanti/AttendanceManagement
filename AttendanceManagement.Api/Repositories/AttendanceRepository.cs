using AttendanceManagement.Api.Domain;
using AttendanceManagement.Api.Dtos;
using AttendanceManagement.Api.Entities;
using AttendanceManagement.Api.Utils;
using Microsoft.AspNetCore.Identity;

namespace AttendanceManagement.Api.Repositories
{
    public class AttendanceRepository : IAttendanceRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;

        public AttendanceRepository(ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }

        public async void Create(AttendanceCreateDto dtoModel)
        {
            var attendance = new Attendance()
            {
                User = await _userManager.FindByNameAsync(dtoModel.Username),
                EntryTimestamp = CommonUtils.GetUtcTimestamp(dtoModel.EntryDateTime),
                EntryDate = dtoModel.EntryDateTime.Date,
                Month = dtoModel.EntryDateTime.Month
            };

            await _dbContext.Attendances.AddAsync(attendance);
            await _dbContext.SaveChangesAsync();
        }
    }
}
