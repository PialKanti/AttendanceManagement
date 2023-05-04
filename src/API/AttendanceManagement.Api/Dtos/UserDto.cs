using AttendanceManagement.Infrastructure.Identity;

namespace AttendanceManagement.Api.Dtos
{
    public class UserDto
    {
        public string UserName { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string Email { get; set; }
        public DateTime? BirthDate { get; set; }

        public UserDto(ApplicationUser user)
        {
            UserName = user.UserName;
            FirstName = user.FirstName;
            LastName = user.LastName;
            Email = user.Email;
            BirthDate = user.BirthDate;
        }
    }
}
