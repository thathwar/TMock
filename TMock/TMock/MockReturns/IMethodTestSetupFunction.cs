using System;

namespace TMock.MockReturns
{
    public interface IMethodTestSetupFunction
    {
        IArgumentSetupFunction Do(Func<object> func);
    }
}
