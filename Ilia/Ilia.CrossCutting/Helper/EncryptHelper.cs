using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Ilia.CrossCutting.Helper
{
    public static class EncryptHelper
    {
        public static string EncryptPassword(string login, string password)
        {
            var data = Encoding.ASCII.GetBytes($"{login}{password}");
            data = new SHA256Managed().ComputeHash(data);
            return Convert.ToBase64String(data);
        }
    }
}
