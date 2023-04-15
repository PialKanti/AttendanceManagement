using System.ComponentModel.DataAnnotations;

namespace AttendanceManagement.Api.Dtos
{
    public class UserUpdateDto
    {
        [Required]
        public string? UserName { get; set; }
        [Required]
        public string? FistName { get; set; }
        [Required]
        public string? LastName { get; set; }
        [Required]
        public string? Email { get; set; }
    }
}
