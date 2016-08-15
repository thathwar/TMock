using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestAssembly;
using TestAssemblySecondStage;
using TMock;

namespace TMockTests
{
    [TestClass]
    public class TypeBuilderTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            var obj = TypeBuilder.Create<IMath>(new Response { OutPut = new SecondStage() { OutPut = 3 } });
            Assert.AreEqual(obj.Add(1, 2).OutPut.OutPut, 3);
            //var obj = TypeBuilder.Create<IMath>(null);
            //Assert.AreEqual(obj.Add(1, 2), null);
        }
    }

   
}
