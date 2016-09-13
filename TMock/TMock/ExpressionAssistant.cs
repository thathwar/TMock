using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace TMock
{
    /// <summary>
    /// Helper class containing static methods for lambda expression helpers
    /// </summary>
    internal static class ExpressionAssistant
    {
        /// <summary>
        /// Gets the method name and also returns an indication whether method is property for a given expression with Func.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TResult">TResult</typeparam>
        /// <param name="expression">expression</param>
        /// <returns>MethodDescription</returns>
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

        /// <summary>
        /// Gets the method name and also returns an indication whether method is property for a given expression with Action.
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <param name="expression">expression</param>
        /// <returns>MethodDescription</returns>
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

        /// <summary>
        /// Gets all arguments passed in a method for the given expression of Func type.
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <typeparam name="TResult">TResult</typeparam>
        /// <param name="expression">expression</param>
        /// <returns>List of Argument</returns>
        public static List<Argument> ResolveArgs<T, TResult>(Expression<Func<T, TResult>> expression) 
        {
            var body = (MethodCallExpression)expression.Body;

            return BuildArgWithValues(body);
        }

        /// <summary>
        /// Gets all arguments passed in a method for the given expression of Action type.
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <param name="expression">expression</param>
        /// <returns>List of Argument</returns>
        public static List<Argument> ResolveArgs<T>(Expression<Action<T>> expression)
        {
            var body = (MethodCallExpression)expression.Body;

            return BuildArgWithValues(body);

        }

        //Builds argument values for the given expression
        private static List<Argument> BuildArgWithValues(MethodCallExpression body)
        {
            var values = new List<Argument>();
            foreach (var argument in body.Arguments)
            {
                var expression = argument as ConstantExpression;
                if (expression != null)
                {
                    var value = (expression.Value);
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
