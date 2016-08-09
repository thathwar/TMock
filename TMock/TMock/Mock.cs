using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TMock
{
    public class Mock<T>
    {
        private T _object;
        public T Object
        {
            get
            {
                return _object;                
            }
        }

        public Mock<T>SetUp<TResult>(Expression<Func<T, TResult>> f)
        {
            if (!typeof(T).IsInterface)
            {
                throw new InvalidOperationException(typeof(T).Name
                    + " is not an interface");
            }

            return this;
        } 
    }
}
