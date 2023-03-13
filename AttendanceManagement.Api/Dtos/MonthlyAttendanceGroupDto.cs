namespace AttendanceManagement.Api.Dtos
{
    public class MonthlyAttendanceGroupDto
    {
        public int Month { get; set; }
        public IEnumerable<AttendanceDto> Attendances { get; set; }
    }
}
