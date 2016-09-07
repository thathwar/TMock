using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TMock;

namespace TMockTests
{
    [TestClass]
    public class ExtensionsTest
    {
        [TestMethod]
        public void HasPropertyTest()
        {
            Assert.IsTrue(new { A = 1 }.HasProperty("A"));
            Assert.IsFalse(new { A = 1 }.HasProperty("B"));
        }
    }
}
