using System.ComponentModel.DataAnnotations;

namespace AttendanceManagement.Api.Dtos.Request
{
    public class AttendanceCreateRequest
    {
        [Required]
        public string? Username { get; set; }
        [Required]
        public DateTime EntryDateTime { get; set; }
    }
}
