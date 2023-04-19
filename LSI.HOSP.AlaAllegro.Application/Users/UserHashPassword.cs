using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace LSI.HOSP.AlaAllegro.Application.Users
{
    public class UserHashPassword
    {
        public static string HashPassword(string password)
        {
            SHA256 sha256 = SHA256.Create();
            UTF8Encoding objUtf8 = new UTF8Encoding();
            return Convert.ToBase64String(sha256.ComputeHash(objUtf8.GetBytes(password)));
        }
    }
}
