using NUnit.Framework;
using System;

namespace NUnitTestWebApplication
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            var d = DateTime.Now.ToString("O");
            Assert.Pass();
        }
    }
}