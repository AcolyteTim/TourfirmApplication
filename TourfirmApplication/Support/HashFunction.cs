using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace TourfirmApplication.Support
{
    internal class HashFunction
    {
        public static string HashPassword(string password)
        {
            MD5 md5 = MD5.Create();
            byte[] bytes = Encoding.ASCII.GetBytes(password);
            byte[] hash = md5.ComputeHash(bytes);

            StringBuilder strbuilder = new StringBuilder();
            foreach (var a in hash)
                strbuilder.Append(a.ToString("X2"));

            return Convert.ToString(strbuilder);
        }
    }
}
