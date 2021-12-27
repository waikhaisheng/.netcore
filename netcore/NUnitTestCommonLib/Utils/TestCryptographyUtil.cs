using Common.Utils;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NUnitTestCommon.Utils
{
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
        #endregion
    }
}
