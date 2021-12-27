using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace Common.Utils
{
    /// <summary>
    /// Creater: Wai Khai Sheng
    /// Created: 20211227
    /// Creater: 
    /// Updated: 
    /// </summary>
    internal static class CryptographySha256
    {
        /// <summary>
        /// Creater: Wai Khai Sheng
        /// Created: 20211227
        /// Creater: 
        /// Updated: 
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        internal static byte[] HashBytes(byte[] bytes)
        {
            byte[] hashValue;
            using (var sha256Alg = SHA256.Create())
            {
                hashValue = sha256Alg.ComputeHash(bytes).ToArray();
            }
            return hashValue;
        }
        /// <summary>
        /// Creater: Wai Khai Sheng
        /// Created: 20211227
        /// Creater: 
        /// Updated: 
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        internal static string HashString(string str)
        {
            byte[] bytes = Encoding.ASCII.GetBytes(str);
            byte[] hashValue;
            using (var sha256Alg = SHA256.Create())
            {
                hashValue = sha256Alg.ComputeHash(bytes).ToArray();
            }
            return Convert.ToBase64String(hashValue);
        }
    }
}
