using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMock
{
    internal class MethodInfo
    {
        public string Method { get; set; }

        public ExpectedArgument ExpectedArgument { get; set; }

        public bool IsExecuted { get; set; }
    }

    internal class ExpectedArgument
    {
        public List<Argument> Arguments { get; set; }

        public Action Action { get; set; }

        public Func<object> Func { get; set; }

        public dynamic ParamSetValue { get; set; }
    }

    internal class Argument
    {
        public object Value { get; set; }

        public bool IsAny { get; set; }
    }

}
