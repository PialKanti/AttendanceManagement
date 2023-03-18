using AttendanceManagement.Api.Utils;
using AutoMapper;

namespace AttendanceManagement.Api.Mappers.Formatters
{
    public class DateTimeToTimestampFormatter : IValueConverter<DateTime, long>
    {
        public long Convert(DateTime sourceMember, ResolutionContext context)
        {
            return CommonUtils.GetUtcTimestamp(sourceMember);
        }
    }
}
