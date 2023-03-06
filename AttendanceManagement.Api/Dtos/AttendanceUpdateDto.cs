using System.ComponentModel.DataAnnotations;

namespace AttendanceManagement.Api.Dtos
{
    public class AttendanceUpdateDto
    {
        [Required]
        public string Id { get; set; }
        [Required]
        public DateTime ExitDateTime { get; set; }
    }
}
