using AttendanceManagement.Api.Dtos;
using AttendanceManagement.Api.Dtos.Request;
using AttendanceManagement.Api.Responses.Error;
using AttendanceManagement.Api.Services;
using AttendanceManagement.Application.Interfaces;
using AttendanceManagement.Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace AttendanceManagement.Api.Controllers
{
    public class AuthController : BaseApiController
    {
        private readonly IUsersRepository<ApplicationUser> _userRepository;
        private readonly JwtTokenService _tokenService;

        public AuthController(IUsersRepository<ApplicationUser> userRepository, JwtTokenService tokenService)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserRequest userDto)
        {
            var user = await _userRepository.GetByUserNameAsync(userDto.Username);
            if (user == null)
            {
                List<ErrorResponse> errors = new() { new ErrorResponse(ErrorResponseType.UserNotFound, (int)HttpStatusCode.NotFound) };
                return NotFound(new ErrorDto(errors));
            }

            var isPasswordValid = await _userRepository.VerifyPasswordAsync(user, userDto.Password);
            if (!isPasswordValid)
            {
                List<ErrorResponse> errors = new() { new ErrorResponse(ErrorResponseType.WrongCredentials, (int)HttpStatusCode.Unauthorized) };
                return Unauthorized(new ErrorDto(errors));
            }

            var accessToken = _tokenService.CreateToken(user);

            HttpContext.Response.Cookies.Append("X-Access-Token", accessToken, new CookieOptions
            {
                Expires = DateTime.Now.AddMinutes(2),
                HttpOnly = true,
                IsEssential = true,
                Path = "/",
                SameSite = SameSiteMode.None,
                Secure = true
            });

            return Ok(new
            {
                message = "Login successful",
                user = new UserDto(user)
            });
        }

        [Authorize]
        [HttpGet("logout")]
        public IActionResult Logout()
        {
            HttpContext.Response.Cookies.Append("X-Access-Token", string.Empty, new CookieOptions
            {
                Expires = DateTime.Now.AddDays(-7),
                HttpOnly = true,
                IsEssential = true,
                Path = "/",
                SameSite = SameSiteMode.None,
                Secure = true
            });

            HttpContext.Response.Cookies.Delete("X-Access-Token");

            return Ok();
        }
    }
}
