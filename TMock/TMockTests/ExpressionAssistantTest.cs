using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TMock;

namespace TMockTests
{
    [TestClass]
    public class ExpressionAssistantTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            var s = ExpressionAssistant.GetMethod((ITestc p) => p.TestMethod(1));

            var result = ExpressionAssistant.ResolveArgs((ITestc p) => p.TestMethod(1));

            var prop = ExpressionAssistant.GetMethod((IMath m) => m.Result);
        }
    }

    public interface ITestc
    {
        int TestMethod(int i);
    }
}
