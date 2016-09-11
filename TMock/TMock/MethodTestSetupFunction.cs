using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMock
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
