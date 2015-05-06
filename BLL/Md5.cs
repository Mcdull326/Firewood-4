using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Security.Cryptography;

namespace BLL
{
    public class Md5
    {
        /// <summary>
        /// md5加密
        /// </summary>
        /// <param name="Unsecure"></param>
        /// <returns></returns>
        public static string MD5_encrypt(string Unsecure)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();

            byte[] InBytes = Encoding.GetEncoding("GB2312").GetBytes(Unsecure);

            byte[] OutBytes = md5.ComputeHash(InBytes);

            string OutString = "";

            for (int i = 0; i < OutBytes.Length; i++)
            {
                OutString += OutBytes[i].ToString("x2");
            }

            return OutString;
        }
    }
}
