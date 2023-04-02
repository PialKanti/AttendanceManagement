using AttendanceManagement.Api.Responses.Error;

namespace AttendanceManagement.Api.Dtos
{
    public class ErrorDto
    {
        public IList<ErrorResponse> Errors { get; }

        public ErrorDto(IList<ErrorResponse> errors)
        {
            Errors = errors;
        }
    }
}
