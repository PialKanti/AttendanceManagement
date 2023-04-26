namespace AttendanceManagement.Application.Dtos
{
    public class MonthlyAttendanceGroupDto
    {
        public int Month { get; set; }
        public IEnumerable<AttendanceDto> Attendances { get; set; }
    }
}
