using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
        /// <summary>
        /// Creater: Wai Khai Sheng
        /// Created: 20211228
        /// UpdatedBy:
        /// Updated: 
        /// </summary>
        /// <returns></returns>
        public static (byte[], byte[]) GenerateKeyIv()
        {
            return CryptographyAes.GenerateAesKeyIv();
        }
        /// <summary>
        /// Creater: Wai Khai Sheng
        /// Created: 20211228
        /// UpdatedBy:
        /// Updated: 
        /// </summary>
        /// <returns></returns>
        public static (string, string) GenerateKeyIvString()
        {
            return CryptographyAes.GenerateAesKeyIvString();
        }
        /// <summary>
        /// Creater: Wai Khai Sheng
        /// Created: 20211228
        /// UpdatedBy:
        /// Updated: 
        /// </summary>
        /// <param name="plainText"></param>
        /// <param name="key"></param>
        /// <param name="iv"></param>
        /// <returns></returns>
        public static byte[] EncryptStringToBytes(this string plainText, byte[] key, byte[] iv)
        {
            return CryptographyAes.EncryptStringToBytesAes(plainText, key, iv);
        }
        /// <summary>
        /// Creater: Wai Khai Sheng
        /// Created: 20211228
        /// UpdatedBy:
        /// Updated: 
        /// </summary>
        /// <param name="cipherText"></param>
        /// <param name="key"></param>
        /// <param name="iv"></param>
        /// <returns></returns>
        public static string DecryptStringFromBytes(this byte[] cipherText, byte[] key, byte[] iv)
        {
            return CryptographyAes.DecryptStringFromBytesAes(cipherText, key, iv);
        }
        /// <summary>
        /// Creater: Wai Khai Sheng
        /// Created: 20211228
        /// UpdatedBy:
        /// Updated: 
        /// </summary>
        /// <param name="plainText"></param>
        /// <param name="key"></param>
        /// <param name="iv"></param>
        /// <returns></returns>
        public static string EncryptString(this string plainText, string key, string iv)
        {
            return CryptographyAes.EncryptStringAes(plainText, key, iv);
        }
        /// <summary>
        /// Creater: Wai Khai Sheng
        /// Created: 20211228
        /// UpdatedBy:
        /// Updated: 
        /// </summary>
        /// <param name="cipherText"></param>
        /// <param name="key"></param>
        /// <param name="iv"></param>
        /// <returns></returns>
        public static string DecryptString(this string cipherText, string key, string iv)
        {
            return CryptographyAes.DecryptStringAes(cipherText, key, iv);
        }
        /// <summary>
        /// Creater: Wai Khai Sheng
        /// Created: 20211228
        /// UpdatedBy:
        /// Updated: 
        /// </summary>
        /// <returns></returns>
        public static (RSAParameters, RSAParameters) RsaKeys()
        {
            return CryptographyRsa.RasKeys();
        }
        /// <summary>
        /// Creater: Wai Khai Sheng
        /// Created: 20211228
        /// UpdatedBy:
        /// Updated: 
        /// </summary>
        /// <param name="DataToEncrypt"></param>
        /// <param name="RSAKeyInfo"></param>
        /// <param name="DoOAEPPadding"></param>
        /// <returns></returns>
        public static byte[] RSAEncrypt(this byte[] DataToEncrypt, RSAParameters RSAKeyInfo, bool DoOAEPPadding)
        {
            return CryptographyRsa.RSAEncrypt(DataToEncrypt, RSAKeyInfo, DoOAEPPadding);
        }
        /// <summary>
        /// Creater: Wai Khai Sheng
        /// Created: 20211228
        /// UpdatedBy:
        /// Updated: 
        /// </summary>
        /// <param name="DataToDecrypt"></param>
        /// <param name="RSAKeyInfo"></param>
        /// <param name="DoOAEPPadding"></param>
        /// <returns></returns>
        public static byte[] RSADecrypt(this byte[] DataToDecrypt, RSAParameters RSAKeyInfo, bool DoOAEPPadding)
        {
            return CryptographyRsa.RSADecrypt(DataToDecrypt, RSAKeyInfo, DoOAEPPadding);
        }
        /// <summary>
        /// Creater: Wai Khai Sheng
        /// Created: 20211228
        /// UpdatedBy:
        /// Updated: 
        /// </summary>
        /// <param name="plainText"></param>
        /// <param name="RSAKeyInfo"></param>
        /// <param name="DoOAEPPadding"></param>
        /// <returns></returns>
        public static string RSAEncrypt(this string plainText, RSAParameters RSAKeyInfo, bool DoOAEPPadding)
        {
            return CryptographyRsa.RSAEncrypt(plainText, RSAKeyInfo, DoOAEPPadding);
        }
        /// <summary>
        /// Creater: Wai Khai Sheng
        /// Created: 20211228
        /// UpdatedBy:
        /// Updated: 
        /// </summary>
        /// <param name="cipherText"></param>
        /// <param name="RSAKeyInfo"></param>
        /// <param name="DoOAEPPadding"></param>
        /// <returns></returns>
        public static string RSADecrypt(this string cipherText, RSAParameters RSAKeyInfo, bool DoOAEPPadding)
        {
            return CryptographyRsa.RSADecrypt(cipherText, RSAKeyInfo, DoOAEPPadding);
        }
    }
}
