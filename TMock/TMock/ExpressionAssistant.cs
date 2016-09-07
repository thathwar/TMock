using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TMock
{
    internal static class ExpressionAssistant
    {
        public static string GetMethod<T, TResult>(Expression<Func<T, TResult>> expression) where T : class
        {
            return ((MethodCallExpression)expression.Body).Method.Name;
        }

        public static KeyValuePair<Type, object>[] ResolveArgs<T, TResult>(Expression<Func<T, TResult>> expression) where T : class
        {
            var body = (MethodCallExpression)expression.Body;

            var values = new List<KeyValuePair<Type, object>>();

            foreach (var argument in body.Arguments)
            {
                if (argument is ConstantExpression)
                {
                    var type = argument.Type;
                    var value = (((ConstantExpression)argument).Value);
                    values.Add(new KeyValuePair<Type, object>(type, value));
                }
                else
                {
                    throw new NotSupportedException(expression.ToString());
                }

            }

            return values.ToArray();
        }

    }
}
