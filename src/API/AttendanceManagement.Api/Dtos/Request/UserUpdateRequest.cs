using System.ComponentModel.DataAnnotations;

namespace AttendanceManagement.Api.Dtos.Request
{
    public class UserUpdateRequest
    {
        [Required]
        public string? UserName { get; set; }
        [Required]
        public string? FirstName { get; set; }
        [Required]
        public string? LastName { get; set; }
        [Required]
        public string? Email { get; set; }
    }
}
