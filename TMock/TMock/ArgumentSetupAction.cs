using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMock
{
    internal class ArgumentSetupAction : IArgumentSetupAction
    {
        private readonly ExpectedArgument _argument;
        public ArgumentSetupAction(ExpectedArgument argument)
        {
            _argument = argument;
        }

        public IMethodTestSetupAction SetArguments(object arg)
        {
            _argument.ParamSetValue = arg;
           return new MethodTestSetupAction(_argument);
        }
    }
}
