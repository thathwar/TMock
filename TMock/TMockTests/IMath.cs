using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestAssembly;

namespace TMockTests
{
    public interface IMath
    {
        Response Add(int a, int b);

        void Subtract(decimal a, decimal b);

        void Multiply(decimal a, decimal b, out decimal c);

        void Devide(decimal a, decimal b, ref decimal c);
    }
}
