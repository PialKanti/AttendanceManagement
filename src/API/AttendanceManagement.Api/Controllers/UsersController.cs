using AttendanceManagement.Api.Dtos;
using AttendanceManagement.Api.Dtos.Request;
using AttendanceManagement.Api.Responses.Error;
using AttendanceManagement.Application.Dtos;
using AttendanceManagement.Application.Interfaces;
using AttendanceManagement.Domain.Entities;
using AttendanceManagement.Infrastructure.Identity;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace AttendanceManagement.Api.Controllers
{
    public class UsersController : BaseApiController
    {
        private readonly IUsersRepository<ApplicationUser> _userRepository;
        private readonly IAttendanceRepository<Attendance> _attendanceRepository;
        private readonly IMapper _mapper;

        public UsersController(IUsersRepository<ApplicationUser> userRepository,
            IAttendanceRepository<Attendance> attendanceRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _attendanceRepository = attendanceRepository;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] RegisterUserRequest userDto)
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

            var user = _mapper.Map<ApplicationUser>(userDto);

            var result = await _userRepository.CreateAsync(user, userDto.Password);

            return Ok(result);
        }

        [HttpPut("{username}")]
        public async Task<IActionResult> Update(string? username, UserUpdateRequest userDto)
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

            return Ok(result);
        }

        [Authorize]
        [HttpGet("{username}/attendances")]
        public async Task<ActionResult<IEnumerable<AttendanceDto>>> Attendances(string username, [FromQuery] int? month, [FromQuery] int? year)
        {
            var user = await _userRepository.GetByUserNameAsync(username);
            if (user == null)
            {
                return NotFound(new ErrorDto(new List<ErrorResponse> { new ErrorResponse(ErrorResponseType.UserNotFound, (int)HttpStatusCode.NotFound) }));
            }

            year ??= DateTime.UtcNow.Year;
            if (month == null)
            {
                return Ok(_attendanceRepository.GetUserYearlyAttendancesAsync(user.Id, (int)year));
            }

            var dateTime = new DateTime((int)year, (int)month, 1);

            var attendances = await _attendanceRepository.GetUserMonthlyAttendancesAsync(user.Id, dateTime);
            return Ok(attendances);
        }

        [Authorize]
        [HttpPost("{username}/password")]
        public async Task<IActionResult> ChangePassword(string username, [FromBody]ChangePasswordRequest dtoModel)
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

            if (!result)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return Ok(result);
        }
    }
}
