using System;

namespace TMock.MockReturns
{
    internal class MethodTestSetupFunction : IMethodTestSetupFunction
    {
        private readonly ExpectedArgument _argument;

        public MethodTestSetupFunction(ExpectedArgument argument)
        {
            _argument = argument;
        }

        public IArgumentSetupFunction Do(Func<object> func)
        {
            _argument.Func = func;
            return new ArgumentSetupFunction(_argument);
        }
    }
}
