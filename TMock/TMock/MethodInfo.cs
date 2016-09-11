using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMock
{
    public class MethodInfo
    {
        public bool IsProp { get; set; }    

        public string Method { get; set; }

        public ExpectedArgument ExpectedArgument { get; set; }

        public bool IsExecuted { get; set; }
    }

    public class ExpectedArgument
    {
        public List<Argument> Arguments { get; set; }

        public Action Action { get; set; }

        public Func<object> Func { get; set; }

        public object ParamSetValue { get; set; }
    }

    public class Argument
    {
        public object Value { get; set; }

        public bool IsAny { get; set; }
    }

}
