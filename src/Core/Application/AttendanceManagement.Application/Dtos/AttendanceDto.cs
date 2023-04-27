namespace AttendanceManagement.Application.Dtos
{
    public class AttendanceDto
    {
        public string Id { get; set; }
        public DateTime? EntryDateTime { get; set; }
        public DateTime? ExitDateTime { get; set; }
    }
}
