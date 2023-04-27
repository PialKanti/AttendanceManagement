using AttendanceManagement.Domain.Entities;

namespace AttendanceManagement.Infrastructure.Identity
{
    public sealed class IdentityUserAttendee : IAttendee
    {
        private readonly ApplicationUser _user;

        public IdentityUserAttendee(ApplicationUser user)
        {
            _user = user;
        }

        public string Id => _user.Id;
        public string UserName => _user.UserName;
        public string Email => _user.Email;
        public string? FirstName => _user.FirstName;
        public string? LastName => _user.LastName;
        public DateTime? BirthDate => _user.BirthDate;
    }
}
