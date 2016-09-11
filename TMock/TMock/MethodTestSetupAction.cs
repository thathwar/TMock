using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMock
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
