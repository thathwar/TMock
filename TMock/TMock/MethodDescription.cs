using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMock
{
    /// <summary>
    /// Acts as a carrier for method information.
    /// </summary>
    class MethodDescription
    {
        public string Name { get; set; }

        public bool IsProp { get; set; }    
    }
}
