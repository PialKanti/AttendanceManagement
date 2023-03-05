using System.ComponentModel.DataAnnotations;

namespace AttendanceManagement.Api.Entities
{
    public class Attendance
    {
        public string Id { get; set; }
        public ApplicationUser User { get; set; }
        public long EntryTimestamp { get; set; }
        public long ExitTimestamp { get; set; }
        public DateTime EntryDate { get; set; }
        public int Month { get; set; }
    }
}
