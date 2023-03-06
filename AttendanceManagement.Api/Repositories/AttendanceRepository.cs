﻿using AttendanceManagement.Api.Domain;
using AttendanceManagement.Api.Dtos;
using AttendanceManagement.Api.Entities;
using AttendanceManagement.Api.Utils;
using AutoMapper.Internal;
using Microsoft.AspNetCore.Identity;
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

        public async Task<Attendance> CreateAsync(AttendanceCreateDto dtoModel, ApplicationUser user)
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

            return attendance;
        }

        public async Task<Attendance?> GetAsync(string id)
        {
            return await _dbContext.Attendances.Include(attendance => attendance.User).FirstOrDefaultAsync(attendance => attendance.Id == id);
        }
    }
}
