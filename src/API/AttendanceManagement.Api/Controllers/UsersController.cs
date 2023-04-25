﻿using AttendanceManagement.Api.Dtos;
using AttendanceManagement.Api.Entities;
using AttendanceManagement.Api.Repositories;
using AttendanceManagement.Api.Responses.Error;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace AttendanceManagement.Api.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersRepository<ApplicationUser> _userRepository;
        private readonly IAttendanceRepository _attendanceRepository;

        public UsersController(IUsersRepository<ApplicationUser> userRepository, IAttendanceRepository attendanceRepository)
        {
            _userRepository = userRepository;
            _attendanceRepository = attendanceRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] RegisterUserDto userDto)
        {
            if (_userRepository.GetByUserNameAsync(userDto.UserName).Result != null)
            {
                List<ErrorResponse> errors = new()
                {
                    new ErrorResponse(ErrorResponseType.RegistrationConflict,
                        (int)HttpStatusCode.Conflict)
                };
                return Conflict(new ErrorDto(errors));
            }

            if (_userRepository.GetByEmailAsync(userDto.Email).Result != null)
            {
                List<ErrorResponse> errors = new()
                {
                    new ErrorResponse(ErrorResponseType.RegistrationConflict,
                        (int)HttpStatusCode.Conflict)
                };
                return Conflict(new ErrorDto(errors));
            }

            var result = await _userRepository.CreateAsync(userDto);

            if (!result.Succeeded)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return Ok(result);
        }

        [HttpPut("{username}")]
        public async Task<IActionResult> Update(string? username, UserUpdateDto userDto)
        {
            if (!string.Equals(username, userDto.UserName))
            {
                return BadRequest(new ErrorDto(new List<ErrorResponse> { new ErrorResponse(ErrorResponseType.UsernameInvalid, (int)HttpStatusCode.BadRequest) }));
            }

            var user = await _userRepository.GetByUserNameAsync(username);
            if (user == null)
            {
                return NotFound(new ErrorDto(new List<ErrorResponse> { new ErrorResponse(ErrorResponseType.UserNotFound, (int)HttpStatusCode.NotFound) }));
            }

            user.FirstName = userDto.FirstName;
            user.LastName = userDto.LastName;
            user.Email = userDto.Email;

            var result = await _userRepository.UpdateAsync(user);

            if (!result.Succeeded)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return Ok(result);
        }

        [Authorize]
        [HttpGet("{username}/attendances")]
        public async Task<ActionResult<IEnumerable<AttendanceDto>>> Attendances(string username, [FromQuery] int? month, [FromQuery] int? year)
        {
            year ??= DateTime.UtcNow.Year;
            if (month == null)
            {
                return Ok(_attendanceRepository.GetUserYearlyAttendancesAsync(username, (int) year));
            }

            var attendances = await _attendanceRepository.GetUserMonthlyAttendancesAsync(username, (int)month, (int)year);
            return Ok(attendances);
        }

        [Authorize]
        [HttpPost("{username}/password")]
        public async Task<IActionResult> ChangePassword(string username, [FromBody]ChangePasswordDto dtoModel)
        {
            var user = await _userRepository.GetByUserNameAsync(username);
            if (user == null)
            {
                return NotFound(new ErrorDto(new List<ErrorResponse> { new ErrorResponse(ErrorResponseType.UserNotFound, (int)HttpStatusCode.NotFound) }));
            }

            bool isVerified = await _userRepository.VerifyPasswordAsync(user, dtoModel.OldPassword);
            if (!isVerified)
            {
                return BadRequest(new ErrorDto(new List<ErrorResponse> { new ErrorResponse(ErrorResponseType.WrongOldPassword, (int)HttpStatusCode.BadRequest) }));
            }

            var result = await _userRepository.ChangePasswordAsync(user, dtoModel.OldPassword, dtoModel.NewPassword);

            if (!result.Succeeded)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return Ok(result);
        }
    }
}