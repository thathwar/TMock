using System;
using System.Runtime.CompilerServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestAssembly;
using TestAssemblySecondStage;
using TMock;

namespace TMockTests
{
    [TestClass]
    public class TypeBuilderTest
    {
        delegate void TestDelegate(out int x);
        [TestMethod]
        public void CreateTest()
        {
            //var obj = TypeBuilder.Create<IMath>(new Response { OutPut = new SecondStage() { OutPut = 3 } });
            // Assert.AreEqual(obj.Add(1, 2).OutPut.OutPut, 3);
            //var obj = TypeBuilder.Create<IMath>(null);
            //Assert.AreEqual(obj.Add(1, 2), null);
        }

        [TestMethod]
        public void StringMethodsBuilderTest()
        {
            
            foreach (var buildMethod in new StringMethodBuilder().BuildMethods(typeof(IMath)))
            {

            }
        }

    }


}
