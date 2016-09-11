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
        public static MethodDescription GetMethod<T, TResult>(Expression<Func<T, TResult>> expression) 
        {
            if (expression.Body as MemberExpression != null)
            {
                var pmember = ((MemberExpression) expression.Body).Member;
                return new MethodDescription(){IsProp = true,Name = pmember.ToString()};
            }

            var mmember = ((MethodCallExpression) expression.Body).Method;
            return new MethodDescription() { IsProp = false, Name = mmember.ToString() };

        }

        public static MethodDescription GetMethod<T>(Expression<Action<T>> expression) 
        {
            if (expression.Body as MemberExpression != null)
            {
                var pmember = ((MemberExpression)expression.Body).Member;
                return new MethodDescription() { IsProp = true, Name = pmember.ToString() };
            }

            var mmember = ((MethodCallExpression)expression.Body).Method;
            return new MethodDescription() { IsProp = false, Name = mmember.ToString() };
        }

        public static List<Argument> ResolveArgs<T, TResult>(Expression<Func<T, TResult>> expression) 
        {
            var body = (MethodCallExpression)expression.Body;

            return BuildArgWithValues(body);
        }

        public static List<Argument> ResolveArgs<T>(Expression<Action<T>> expression)
        {
            var body = (MethodCallExpression)expression.Body;

            return BuildArgWithValues(body);

        }

        private static List<Argument> BuildArgWithValues(MethodCallExpression body)
        {
            var values = new List<Argument>();
            foreach (var argument in body.Arguments)
            {
                if (argument is ConstantExpression)
                {
                    var value = (((ConstantExpression)argument).Value);
                    values.Add(new Argument() { Value = value });
                }
                else
                {
                    var comp = Expression.Lambda<Func<object>>(Expression.Convert(argument, typeof(object))).Compile();
                    var result = comp();
                    values.Add(new Argument() { Value = result, IsAny = argument.ToString() == "Any()" });
                }

            }

            return values;
        }

    }
}
