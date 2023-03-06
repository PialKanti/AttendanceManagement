using AttendanceManagement.Api.Domain;
using AttendanceManagement.Api.Dtos;
using AttendanceManagement.Api.Entities;
using AttendanceManagement.Api.Repositories;
using AttendanceManagement.Api.Responses;
using AttendanceManagement.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace AttendanceManagement.Api.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IUsersRepository<ApplicationUser> _userRepository;
        private readonly IAttendanceRepository _attendanceRepository;
        private readonly JwtTokenService _tokenService;

        public UsersController(ApplicationDbContext dbContext, IUsersRepository<ApplicationUser> userRepository,
            IAttendanceRepository attendanceRepository, JwtTokenService tokenService)
        {
            _dbContext = dbContext;
            _userRepository = userRepository;
            _attendanceRepository = attendanceRepository;
            _tokenService = tokenService;
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

        [HttpPost]
        [Route("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] LoginUserDto userDto)
        {
            var user = await _userRepository.GetByUserName(userDto.Username);
            if (user == null)
            {
                return BadRequest("User not found");
            }

            var isPasswordValid = await _userRepository.VerifyPassword(user, userDto.Password);
            if (!isPasswordValid)
            {
                return BadRequest("Username/Password wrong");
            }

            var accessToken = _tokenService.CreateToken(user);

            await _dbContext.SaveChangesAsync();

            return Ok(new AuthenticationResponse
            {
                Username = user.UserName,
                Email = user.Email,
                Token = accessToken
            });
        }

        [HttpGet("{username}/attendances")]

        public async Task<ActionResult<IEnumerable<AttendanceDto>>> Attendances(string username,[FromQuery]int? month)
        {
            month ??= DateTime.UtcNow.Month;
            var attendances = await _attendanceRepository.GetByUsernameAndMonthAsync(username, (int)month);

            return Ok(attendances);
        }
    }
}
