namespace AttendanceManagement.Domain.Entities
{
    public interface IAttendee
    {
        public string Id { get; }
        string UserName { get; }
        string Email { get; }
        string? FirstName { get; }
        string? LastName { get; }
        DateTime? BirthDate { get; }
    }
}
