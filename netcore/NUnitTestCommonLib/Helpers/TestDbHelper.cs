using Common.Helpers;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NUnitTestCommon.Helpers
{
    /// <summary>
    /// Creater: Wai Khai Sheng
    /// Created: 20211227
    /// UpdatedBy: 
    /// Updated: 
    /// </summary>
    public class TestDbHelper
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
        public void TestObjectOrDefaultDBNull()
        {
            var objInt = 1;
            var retInt = objInt.ObjectOrDefaultDBNull<int>();
            Assert.AreEqual(1, retInt);

            var objDbNull = DBNull.Value;
            var retDbNull = objDbNull.ObjectOrDefaultDBNull<int>();
            Assert.AreEqual(0, retDbNull);
        }
    }
}
