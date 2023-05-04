using AttendanceManagement.Api.Utils;
using AutoMapper;

namespace AttendanceManagement.Api.Mappers.Formatters
{
    public class TimestampToDateTimeFormatter : IValueConverter<long?, DateTime?>
    {
        public DateTime? Convert(long? sourceMember, ResolutionContext context)
        {
            if(sourceMember == null) 
                return null;

            return CommonUtils.GetDateTimeFromTimestamp(sourceMember);
        }
    }
}
