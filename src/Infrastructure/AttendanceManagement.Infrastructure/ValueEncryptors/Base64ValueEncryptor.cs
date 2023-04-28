using AttendanceManagement.Application.Interfaces;
using System.Text;

namespace AttendanceManagement.Infrastructure.ValueEncryptors
{
    public class Base64ValueEncryptor : IValueEncryptor
    {
        public string Decrypt(string encryptedValue)
        {
            if(string.IsNullOrEmpty(encryptedValue))
                return string.Empty;

            var base64EncodedBytes = Convert.FromBase64String(encryptedValue);
            return Encoding.UTF8.GetString(base64EncodedBytes);
        }

        public string Encrypt(string value)
        {
            var plainTextBytes = Encoding.UTF8.GetBytes(value);
            return Convert.ToBase64String(plainTextBytes);
        }
    }
}
