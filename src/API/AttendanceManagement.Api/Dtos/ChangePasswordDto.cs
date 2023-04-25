using System.ComponentModel.DataAnnotations;

namespace AttendanceManagement.Api.Dtos
{
    public class ChangePasswordDto
    {
        [Required]
        public string? OldPassword { get; set; }
        [Required]
        public string? NewPassword { get; set; }
    }
}
