using System.ComponentModel.DataAnnotations;

namespace AttendanceManagement.Api.Dtos.Request
{
    public class LoginUserRequest
    {
        [Required]
        public string? Username { get; set; }
        [Required]
        public string? Password { get; set; }
    }
}
