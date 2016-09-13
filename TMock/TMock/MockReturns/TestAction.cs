using System;

namespace TMock.MockReturns
{
    internal class TestAction : ITestAction
    {
        private readonly ExpectedArgument _argument;
        public TestAction(ExpectedArgument argument)
        {
            _argument = argument;
        }

        public IArgumentSetupAction Do(Action action)
        {
            _argument.Action = action;
            return new ArgumentSetupAction(_argument);
        }

        public IMethodTestSetupAction SetArguments(object arg)
        {
            _argument.ParamSetValue = arg;
            return new MethodTestSetupAction(_argument);
        }
    }
}
