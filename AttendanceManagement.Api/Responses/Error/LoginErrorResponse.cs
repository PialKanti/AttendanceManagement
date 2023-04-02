namespace AttendanceManagement.Api.Responses.Error
{
    public class LoginErrorResponse : ErrorResponse
    {
        public LoginErrorResponse(ErrorResponseType type, int statusCode) : base(type, statusCode)
        {
        }
    }
}
