using System.ComponentModel.DataAnnotations;

namespace AttendanceManagement.Api.Dtos
{
    public class AttendanceCreateDto
    {
        [Required]
        public string? Username { get; set; }
        [Required]
        public DateTime EntryDateTime { get; set; }
    }
}
