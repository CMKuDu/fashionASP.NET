using System.Text;
using XSystem.Security.Cryptography;
//using XSystem.Security.Cryptography;

namespace DTN4.Extension
{
    public static class HashMD5
    {
        public static string ToMD5(this string str)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] bHash = md5.ComputeHash(Encoding.UTF8.GetBytes(str));
            StringBuilder sbHash = new StringBuilder();
           foreach (byte b in bHash)
                sbHash.Append(string.Format("{0:x2}", b));
            return sbHash.ToString();
        }
   }
}
