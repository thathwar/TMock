using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TMock;

namespace TMockTests
{
    [TestClass]
    public class TypeBuilderTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            var obj = TypeBuilder.Create<IMath>();
            Assert.AreEqual(obj.Add(1,2),3);
        }
    }

    public interface IMath
    {
        int Add(int a, int b);
    }
}
