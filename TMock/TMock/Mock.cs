using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using TMock.MockReturns;

namespace TMock
{
    public class Mock<T>
    {
        #region Storage
        private readonly List<MethodInfo> _data;
        #endregion

        #region Props
        private readonly T _object;
        public T Object
        {
            get
            {
                return _object;
            }
        }
        #endregion

        #region .ctor
        public Mock()
        {
            if (!typeof(T).IsInterface)
            {
                throw new InvalidOperationException(typeof(T).Name
                    + " is not an interface");
            }

            _data = new List<MethodInfo>();
            _object = TypeBuilder.Create<T>(_data);

        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Moock a method
        /// </summary>
        /// <typeparam name="TResult">TResult</typeparam>
        /// <param name="f">f</param>
        /// <returns>ITestFunc</returns>
        public ITestFunc SetUp<TResult>(Expression<Func<T, TResult>> f)
        {

            var result = ExpressionAssistant.GetMethod(f);
            var slts = new List<Argument>();

            if (!result.IsProp)
            {
                slts.AddRange(ExpressionAssistant.ResolveArgs(f));
            }

            var earg = new ExpectedArgument() { Arguments = slts };

            _data.Add(new MethodInfo()
            {
                ExpectedArgument = earg,
                Method = result.Name,
                IsProp = result.IsProp
            });

            ITestFunc testFunc = new TestFunc(earg);
            return testFunc;
        }

        /// <summary>
        /// Mock a method
        /// </summary>
        /// <param name="f">f</param>
        /// <returns>ITestAction</returns>
        public ITestAction SetUp(Expression<Action<T>> f)
        {
            var result = ExpressionAssistant.GetMethod(f);
            var slts = new List<Argument>();

            if (!result.IsProp)
            {
                slts.AddRange(ExpressionAssistant.ResolveArgs(f));
            }

            var earg = new ExpectedArgument() { Arguments = slts };

            _data.Add(new MethodInfo()
            {
                ExpectedArgument = earg,
                Method = result.Name,
                IsProp = result.IsProp
            });

            ITestAction testAction = new TestAction(earg);
            return testAction;
        }
        #endregion
    }
}
