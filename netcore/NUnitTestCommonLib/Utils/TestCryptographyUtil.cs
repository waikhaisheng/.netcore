using Common.Utils;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace NUnitTestCommon.Utils
{
    /// <summary>
    /// Creater: Wai Khai Sheng
    /// Created: 20211227
    /// UpdatedBy: Wai Khai Sheng
    /// Updated: 20211228
    /// </summary>
    public class TestCryptographyUtil
    {
        /// <summary>
        /// Creater: Wai Khai Sheng
        /// Created: 20211227
        /// UpdatedBy: 
        /// Updated: 
        /// </summary>
        [SetUp]
        public void Setup()
        {

        }
        /// <summary>
        /// Creater: Wai Khai Sheng
        /// Created: 20211227
        /// UpdatedBy:
        /// Updated: 
        /// </summary>
        [Test]
        public void TestTestEncryptDecryptStringAES()
        {
            var key = "4512631236589784";
            var iv = "4512631236589784";
            var str = "test123";
            
            var en = TestEncryptStringAES(key, iv, str);
            var de = TestDecryptStringAES(key, iv, en);

            Assert.AreEqual(str, de);
        }
        /// <summary>
        /// Creater: Wai Khai Sheng
        /// Created: 20211227
        /// UpdatedBy: 
        /// Updated: 
        /// </summary>
        [Test]
        public void TestHashString()
        {
            var str = "test123";
            var ret = CryptographyUtil.HashString(str);
            Assert.IsTrue(ret.Length > 0);
        }
        /// <summary>
        /// Creater: Wai Khai Sheng
        /// Created: 20211228
        /// UpdatedBy:
        /// Updated: 
        /// </summary>
        [Test]
        public void TestEncryptDecryptStringToBytes()
        {
            var str = "test123";
            var keyIv = CryptographyUtil.GenerateKeyIv();
            var key = keyIv.Item1;
            var iv = keyIv.Item2;

            var en = TestEncryptStringToBytes(str, key, iv);
            var de = TestDecryptStringFromBytes(en, key, iv);

            Assert.AreEqual(str, de);
        }
        /// <summary>
        /// Creater: Wai Khai Sheng
        /// Created: 20211228
        /// UpdatedBy:
        /// Updated: 
        /// </summary>
        [Test]
        public void TestEncryptDecryptString()
        {
            var str = "test123";
            var keyIv = CryptographyUtil.GenerateKeyIvString();
            var key = keyIv.Item1;
            var iv = keyIv.Item2;

            var en = TestEncryptString(str, key, iv);
            var de = TestDecryptString(en, key, iv);

            Assert.AreEqual(str, de);
        }
        /// <summary>
        /// Creater: Wai Khai Sheng
        /// Created: 20211228
        /// UpdatedBy:
        /// Updated: 
        /// </summary>
        [Test]
        public void TestRsaKeys()
        {
            var keys = CryptographyUtil.RsaKeys();
            Assert.IsNotNull(keys);
        }
        /// <summary>
        /// Creater: Wai Khai Sheng
        /// Created: 20211228
        /// UpdatedBy:
        /// Updated: 
        /// </summary>
        [Test]
        public void TestRSAEncrypt()
        {
            var str = "test123";
            var bytes = Encoding.ASCII.GetBytes(str);
            var keys = CryptographyUtil.RsaKeys();
            var publicKey = keys.Item1;
            var privateKey = keys.Item2;

            var en = RSAEncrypt(bytes, publicKey, false);
            var de = RSADecrypt(en, privateKey, false);

            Assert.AreEqual(str, de);
        }
        /// <summary>
        /// Creater: Wai Khai Sheng
        /// Created: 20211228
        /// UpdatedBy:
        /// Updated: 
        /// </summary>
        [Test]
        public void TestRSAEncryptString()
        {
            var str = "test123";
            var keys = CryptographyUtil.RsaKeys();
            var publicKey = keys.Item1;
            var privateKey = keys.Item2;

            var en = RSAEncrypt(str, publicKey, false);
            var de = RSADecrypt(en, privateKey, false);

            Assert.AreEqual(str, de);
        }

        #region Private Testcases
        /// <summary>
        /// Creater: Wai Khai Sheng
        /// Created: 20211227
        /// UpdatedBy: 
        /// Updated: 
        /// </summary>
        private string TestEncryptStringAES(string key, string iVal, string plainText)
        {
            var ret = CryptographyUtil.EncryptStringAES(key, iVal, plainText);

            Assert.IsTrue(ret.Length > 0);

            return ret;
        }
        /// <summary>
        /// Creater: Wai Khai Sheng
        /// Created: 20211227
        /// UpdatedBy: 
        /// Updated: 
        /// </summary>
        private string TestDecryptStringAES(string key, string iVal, string cipherText)
        {
            var ret = CryptographyUtil.DecryptStringAES(key, iVal, cipherText);

            Assert.IsTrue(ret.Length > 0);

            return ret;
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
        private byte[] TestEncryptStringToBytes(string plainText, byte[] key, byte[] iv)
        {
            var ret = CryptographyUtil.EncryptStringToBytes(plainText, key, iv);
            
            Assert.IsTrue(ret.Length > 0);

            return ret;
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
        private string TestDecryptStringFromBytes(byte[] cipherText, byte[] key, byte[] iv)
        {
            var ret = CryptographyUtil.DecryptStringFromBytes(cipherText, key, iv);
            
            Assert.IsNotNull(ret);

            return ret;
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
        private string TestEncryptString(string plainText, string key, string iv)
        {
            var ret = CryptographyUtil.EncryptString(plainText, key, iv);

            Assert.IsTrue(ret.Length > 0);

            return ret;
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
        private string TestDecryptString(string cipherText, string key, string iv)
        {
            var ret = CryptographyUtil.DecryptString(cipherText, key, iv);

            Assert.IsNotNull(ret);

            return ret;
        }
        /// <summary>
        /// Creater: Wai Khai Sheng
        /// Created: 20211228
        /// UpdatedBy:
        /// Updated: 
        /// </summary>
        private byte[] RSAEncrypt(byte[] bytes, RSAParameters publicKey, bool DoOAEPPadding = false)
        {
            var ret = CryptographyUtil.RSAEncrypt(bytes, publicKey, DoOAEPPadding);
            
            Assert.IsTrue(true);

            return ret;
        }
        /// <summary>
        /// Creater: Wai Khai Sheng
        /// Created: 20211228
        /// UpdatedBy:
        /// Updated: 
        /// </summary>
        private string RSADecrypt(byte[] encry, RSAParameters privateKey, bool DoOAEPPadding = false)
        {
            var decry = CryptographyUtil.RSADecrypt(encry, privateKey, DoOAEPPadding);
            var ret = Encoding.ASCII.GetString(decry);
            
            Assert.IsNotNull(ret);

            return ret;
        }
        /// <summary>
        /// Creater: Wai Khai Sheng
        /// Created: 20211228
        /// UpdatedBy:
        /// Updated: 
        /// </summary>
        /// <param name="plainText"></param>
        /// <param name="publicKey"></param>
        /// <param name="DoOAEPPadding"></param>
        /// <returns></returns>
        private string RSAEncrypt(string plainText, RSAParameters publicKey, bool DoOAEPPadding = false)
        {
            var ret = CryptographyUtil.RSAEncrypt(plainText, publicKey, DoOAEPPadding);

            Assert.IsTrue(true);

            return ret;
        }
        /// <summary>
        /// Creater: Wai Khai Sheng
        /// Created: 20211228
        /// UpdatedBy:
        /// Updated: 
        /// </summary>
        /// <param name="cipherText"></param>
        /// <param name="privateKey"></param>
        /// <param name="DoOAEPPadding"></param>
        /// <returns></returns>
        private string RSADecrypt(string cipherText, RSAParameters privateKey, bool DoOAEPPadding = false)
        {
            var ret = CryptographyUtil.RSADecrypt(cipherText, privateKey, DoOAEPPadding);

            Assert.IsNotNull(ret);

            return ret;
        }
        #endregion
    }
}
