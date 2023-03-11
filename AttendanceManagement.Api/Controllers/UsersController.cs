using AttendanceManagement.Api.Dtos;
using AttendanceManagement.Api.Entities;
using AttendanceManagement.Api.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
            if (_userRepository.GetByUserName(userDto.UserName).Result != null)
            {
                return BadRequest(new { error = "User username already exists" });
            }

            if (_userRepository.GetByEmail(userDto.Email).Result != null)
            {
                return BadRequest(new { error = "User email already exists" });
            }

            var result = await _userRepository.Create(userDto);

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
            month ??= DateTime.UtcNow.Month;
            year ??= DateTime.UtcNow.Year;
            var attendances = await _attendanceRepository.GetUserAttendancesByMonthAndYearAsync(username, (int)month, (int)year);

            return Ok(attendances);
        }
    }
}
