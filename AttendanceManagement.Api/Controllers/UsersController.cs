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
        private readonly IUsersRepository<ApplicationUser> _repository;
        private readonly JwtTokenService _tokenService;

        public UsersController(ApplicationDbContext dbContext, IUsersRepository<ApplicationUser> repository, JwtTokenService tokenService)
        {
            _dbContext = dbContext;
            _repository = repository;
            _tokenService = tokenService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] RegisterUserDto userDto)
        {
            if (_repository.GetByUserName(userDto.UserName).Result != null)
            {
                return BadRequest(new { error = "User username already exists" });
            }

            if (_repository.GetByEmail(userDto.Email).Result != null)
            {
                return BadRequest(new { error = "User email already exists" });
            }

            var result = await _repository.Create(userDto);

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
            var user = await _repository.GetByUserName(userDto.Username);
            if (user == null)
            {
                return BadRequest("User not found");
            }

            var isPasswordValid = await _repository.VerifyPassword(user, userDto.Password);
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
    }
}
