using System.ComponentModel.DataAnnotations;

namespace AttendanceManagement.Api.Dtos.Request
{
    public class ChangePasswordRequest
    {
        [Required]
        public string? OldPassword { get; set; }
        [Required]
        public string? NewPassword { get; set; }
    }
}
