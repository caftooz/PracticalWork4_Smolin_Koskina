using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using PracticalWork4_Smolin_Koskina;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            int res = 2 + 2;
            Assert.AreEqual(4, res);
            Assert.AreNotEqual(5, res);
            Assert.IsFalse(res > 5);
            Assert.IsTrue(res < 5);
        }
    }
}