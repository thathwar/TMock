using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMock
{
    internal class ArgumentSetupFunction:IArgumentSetupFunction
    { 
        private readonly ExpectedArgument _argument;

        public ArgumentSetupFunction(ExpectedArgument argument)
        {
            _argument = argument;
        }

        public IMethodTestSetupFunction SetArguments(object arg)
        {
            _argument.ParamSetValue = arg;
            return new MethodTestSetupFunction(_argument);
        }
    }
}
