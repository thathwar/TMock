using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TMock
{
    public static class ExpressionAssistant
    {
        public static string GetMethod<T, TResult>(Expression<Func<T, TResult>> expression) where T : class
        {
            return ((MethodCallExpression)expression.Body).Method.Name;
        }

        public static KeyValuePair<Type, object>[] ResolveArgs<T, TResult>(Expression<Func<T, TResult>> expression) where T : class
        {
            var body = (System.Linq.Expressions.MethodCallExpression)expression.Body;
            var values = new List<KeyValuePair<Type, object>>();

            foreach (var argument in body.Arguments)
            {
                var exp = ResolveMemberExpression(argument);
                if (exp is ConstantExpression)
                {
                    var type = argument.Type;

                    var value = (((ConstantExpression)exp).Value);

                    values.Add(new KeyValuePair<Type, object>(type, value));
                }
                else
                {
                    var type = argument.Type;

                    var value = GetValue((MemberExpression)exp);

                    values.Add(new KeyValuePair<Type, object>(type, value));
                }

            }

            return values.ToArray();
        }

        public static Expression ResolveMemberExpression(Expression expression)
        {

            if (expression is MemberExpression)
            {
                return (MemberExpression)expression;
            }
            else if (expression is UnaryExpression)
            {
                // if casting is involved, Expression is not x => x.FieldName but x => Convert(x.Fieldname)
                return (MemberExpression)((UnaryExpression)expression).Operand;
            }
            else if (expression is ConstantExpression)
            {
                return (ConstantExpression)expression;
            }
            else
            {
                throw new NotSupportedException(expression.ToString());
            }
        }

        private static object GetValue(MemberExpression exp)
        {
            // expression is ConstantExpression or FieldExpression
            if (exp.Expression is ConstantExpression)
            {
                return (((ConstantExpression)exp.Expression).Value)
                        .GetType()
                        .GetField(exp.Member.Name)
                        .GetValue(((ConstantExpression)exp.Expression).Value);
            }
            else if (exp.Expression is MemberExpression)
            {
                return GetValue((MemberExpression)exp.Expression);
            }
            else
            {
                throw new NotImplementedException();
            }
        }
    }
}
