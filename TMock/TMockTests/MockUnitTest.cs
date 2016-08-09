using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TMock;

namespace TMockTests
{
    [TestClass]
    public class MockUnitTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            var mock = new Mock<ITest>();
            mock.SetUp(f => f.Add(1, 2));
        }
    }

    public interface ITest
    {
        int Add(int i, int j);
    }
}
