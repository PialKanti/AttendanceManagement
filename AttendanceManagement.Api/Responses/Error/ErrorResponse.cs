﻿namespace AttendanceManagement.Api.Responses.Error
{
    public class ErrorResponse
    {
        public string Type { get; set; }
        public string Title { get; set; }
        public int Status { get; set; }

        public ErrorResponse(ErrorResponseType type, int status)
        {
            Type = type.ToString();
            Status = status;
            Title = GetErrorTitle(type);
        }

        private string GetErrorTitle(ErrorResponseType type)
        {
            return type switch
            {
                ErrorResponseType.UserNotFound => "User not found",
                ErrorResponseType.WrongCredentials => "Incorrect username or password",
                ErrorResponseType.RegistrationConflict => "A user with that username or email already exists",
                ErrorResponseType.AttendanceNotFound => "Attendance entry does not exist",
                _ => string.Empty
            };
        }
    }
}