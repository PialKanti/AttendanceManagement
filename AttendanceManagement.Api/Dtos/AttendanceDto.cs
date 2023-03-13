namespace AttendanceManagement.Api.Dtos
{
    public class AttendanceDto
    {
        public string Id { get; set; }
        public string? Username { get; set; }
        public DateTime? EntryDateTime { get; set; }
        public DateTime? ExitDateTime { get; set; }
    }
}
