using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Utils
{
    /// <summary>
    /// Creater: Wai Khai Sheng
    /// Created: 20211227
    /// UpdatedBy: 
    /// Updated: 
    /// </summary>
    public static class CryptographyUtil
    {
        /// <summary>
        /// Creater: Wai Khai Sheng
        /// Created: 20211227
        /// UpdatedBy: 
        /// Updated: 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="iVal"></param>
        /// <param name="plainText"></param>
        /// <returns></returns>
        public static string EncryptStringAES(string key, string iVal, string plainText)
        {
            var keybytes = Encoding.UTF8.GetBytes(key);
            var iv = Encoding.UTF8.GetBytes(iVal);

            var encryoFromJavascript = CryptographyAes.EncryptStringToBytes(plainText, keybytes, iv);
            return Convert.ToBase64String(encryoFromJavascript);
        }
        /// <summary>
        /// Creater: Wai Khai Sheng
        /// Created: 20211227
        /// UpdatedBy: 
        /// Updated: 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="iVal"></param>
        /// <param name="cipherText"></param>
        /// <returns></returns>
        public static string DecryptStringAES(string key, string iVal, string cipherText)
        {
            var keybytes = Encoding.UTF8.GetBytes(key);
            var iv = Encoding.UTF8.GetBytes(iVal);

            var encrypted = Convert.FromBase64String(cipherText);
            var decriptedFromJavascript = CryptographyAes.DecryptStringFromBytes(encrypted, keybytes, iv);
            return decriptedFromJavascript;
        }
        /// <summary>
        /// Creater: Wai Khai Sheng
        /// Created: 20211227
        /// UpdatedBy: 
        /// Updated: 
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string HashString(this string str)
        {
            return CryptographySha256.HashString(str);
        }
    }
}
