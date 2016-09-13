using System;

namespace TMock.MockReturns
{
    public interface IMethodTestSetupAction
    {
        IArgumentSetupAction Do(Action action);
    }
}
