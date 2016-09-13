using System;

namespace TMock.MockReturns
{
    internal class TestFunc : ITestFunc
    {
        private readonly ExpectedArgument _argument;
        public TestFunc(ExpectedArgument argument)
        {
            _argument = argument;
        }

        public IArgumentSetupFunction Do(Func<object> func)
        {
            _argument.Func = func;
            return new ArgumentSetupFunction(_argument);
        }

        public IMethodTestSetupFunction SetArguments(object arg)
        {
            _argument.ParamSetValue = arg;
            return new MethodTestSetupFunction(_argument);
        }
    }
}
