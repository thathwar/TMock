using System;

namespace TMock.MockReturns
{
    internal class MethodTestSetupAction:IMethodTestSetupAction
    {
         private readonly ExpectedArgument _argument;
         public MethodTestSetupAction(ExpectedArgument argument)
        {
            _argument = argument;
        }

        public IArgumentSetupAction Do(Action action)
        {
            _argument.Action = action;
            return new ArgumentSetupAction(_argument);
        }
    }
}
