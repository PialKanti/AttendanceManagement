using System.ComponentModel.DataAnnotations;

namespace AttendanceManagement.Domain.Entities
{
    public class BaseEntity
    {
        [Required]
        public string Id { get; set; }
    }
}
