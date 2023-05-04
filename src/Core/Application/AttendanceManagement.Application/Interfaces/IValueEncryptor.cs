using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceManagement.Application.Interfaces
{
    public interface IValueEncryptor
    {
        string Encrypt(string value);
        string Decrypt(string encryptedValue);
    }
}
