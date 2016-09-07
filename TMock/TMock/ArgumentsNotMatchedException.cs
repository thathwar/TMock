using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMock
{
    public class ArgumentsNotMatchedException : Exception
    {
        public ArgumentsNotMatchedException(string message) : base(message) { }
    }
}
