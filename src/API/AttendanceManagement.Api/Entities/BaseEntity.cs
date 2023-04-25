using System.ComponentModel.DataAnnotations;

namespace AttendanceManagement.Api.Entities
{
    public class BaseEntity
    {
        [Required]
        public string Id { get; set; }
    }
}
