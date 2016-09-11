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
            object obj = new {A = 1};
            Assert.IsTrue(obj.HasProperty("A"));
            Assert.AreEqual(1,obj.GetPropValue("A"));

            Assert.IsFalse(new { A = 1 }.HasProperty("B"));
        }
    }
}
