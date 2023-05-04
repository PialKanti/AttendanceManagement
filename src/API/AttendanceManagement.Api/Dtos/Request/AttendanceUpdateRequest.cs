using System.ComponentModel.DataAnnotations;

namespace AttendanceManagement.Api.Dtos.Request
{
    public class AttendanceUpdateRequest
    {
        [Required]
        public string Id { get; set; }
        [Required]
        public DateTime ExitDateTime { get; set; }
    }
}
