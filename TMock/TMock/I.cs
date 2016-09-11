using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMock
{
    public static class I
    {
        public static T Any<T>()
        {
            return default(T);
        }
    }
}
