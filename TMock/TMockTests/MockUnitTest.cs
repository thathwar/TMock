using System;
using System.Runtime.Remoting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestAssembly;
using TestAssemblySecondStage;
using TMock;

namespace TMockTests
{
    [TestClass]
    public class MockUnitTest
    {
        [TestMethod]
        public void SetupTestDo()
        {
            var mock = new Mock<IMath>();
            mock.SetUp(f => f.Add(I.Any<int>(), I.Any<int>())).Do(() => new Response() { OutPut = new SecondStage() { OutPut = 123 } });

            var cal = new Calc(mock.Object);
            var r = cal.CalciAdd(1, 2);
            Assert.AreEqual(r.OutPut.OutPut, 123);
        }

        [TestMethod]
        public void SetupTestDoActionWithRef()
        {
            decimal[] c = {1};
            var mock = new Mock<IMath>();
            mock.SetUp(f => f.Devide(1, 2, ref c[0])).SetArguments(new { c = 123m });
            var cal = new Calc(mock.Object);
            cal.CalciDevide(1, 2, ref c[0]);
            Assert.AreEqual(c[0], 123);
        }

        [TestMethod]
        public void SetupInerfaces()
        {
            decimal[] c = { 1 };
            var mock = new Mock<ICalc<Math>>();
            mock.SetUp(f => f.Get()).Do(() => new Math() { I = 123 });

            var u = new User(mock.Object);
            Assert.AreEqual(u.CreateMath().I, 123);

        }

        [TestMethod]
        public void SetupProp()
        {
            var mock = new Mock<IMath>();
            mock.SetUp(f => f.Result).Do(() => 345);
            Assert.AreEqual(mock.Object.Result, 345);
        }
    }

    #region Helpers
    public class Calc
    {
        private readonly IMath _math;

        public Calc(IMath math)
        {
            _math = math;
        }

        public Response CalciAdd(int a, int c)
        {
            decimal d = 0;
            return _math.Add(a, c);
        }

        public void CalciDevide(decimal a, decimal c, ref decimal f)
        {
            _math.Devide(a, c, ref f);
        }
    }

    public class Math : IMath
    {
        public int I { get; set; }
        public Response Add(int a, int b)
        {
            return new Response();
        }

        public void Subtract(decimal a, decimal b)
        {

        }

        public void Multiply(decimal a, decimal b, out decimal c)
        {
            c = a - b;
        }

        public void Devide(decimal a, decimal b, ref decimal c)
        {
            c = 0;
        }

        public int Result { get; set; }
    }

    public class User
    {
        private ICalc<Math> _calc;

        public User(ICalc<Math> calc)
        {
            _calc = calc;
        }

        public Math CreateMath()
        {
            return _calc.Get();
        }
    }

    public interface ICalc<T> where T:class 
    {
        T Get();
    }
    #endregion
}
