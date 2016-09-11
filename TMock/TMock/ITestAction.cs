using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMock
{
    public interface ITestAction : IMethodTestSetupAction, IArgumentSetupAction
    {
    }
}
