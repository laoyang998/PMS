using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace PMS.Common
{
    public class Encryption
    {
        public static string GetMD5Str(string password)
        {
            byte[] result = Encoding.Default.GetBytes(password);
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] output = md5.ComputeHash(result);
            string encryptResult = BitConverter.ToString(output).Replace("-", "");
            return encryptResult;
        }
        public static string GetMD5Str64(string str)
        {
            MD5 md5 = MD5.Create();
            byte[] encryBytes = md5.ComputeHash(Encoding.UTF8.GetBytes(str));
            return Convert.ToBase64String(encryBytes);
        }
    }
}
