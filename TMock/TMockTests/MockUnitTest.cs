using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TMock;

namespace TMockTests
{
    [TestClass]
    public class MockUnitTest
    {
        [TestMethod]
        public void SetupTest()
        {
            var mock = new Mock<IMath>();
            mock.SetUp(f => f.Add(1, 2));
        }
    }
}
