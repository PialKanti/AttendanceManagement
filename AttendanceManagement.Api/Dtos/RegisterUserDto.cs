﻿using System.ComponentModel.DataAnnotations;

namespace AttendanceManagement.Api.Dtos
{
    public class RegisterUserDto
    {
        [Required]
        public string? FirstName { get; set; }
        [Required]
        public string? LastName { get; set; }
        [Required]
        public string? UserName { get; set; }
        [Required]
        public string? Password { get; set; }
        [Required]
        public string? Email { get; set; }
        public string? BirthDate { get; set; }
    }
}
