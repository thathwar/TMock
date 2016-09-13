using System;
using System.Collections.Generic;
using System.Linq;

namespace TMock
{
    internal class CSharpTypeNameBuilder
    {
        /// <summary>
        /// Returns CShrap style type name for given type.
        /// </summary>
        /// <param name="t"></param>
        /// <param name="trimArgCount">trimArgCount</param>
        /// <returns>string</returns>
        public static string GetCSharpRepresentation(Type t, bool trimArgCount)
        {
            if (t.IsGenericType)
            {
                var genericArgs = t.GetGenericArguments().ToList();

                return GetCSharpRepresentation(t, trimArgCount, genericArgs);
            }

            return t.FullName;
        }

        //Resolves the type name to C# style, with resolution to generic type.
        private static string GetCSharpRepresentation(Type t, bool trimArgCount, List<Type> availableArguments)
        {
            if (t.IsGenericType)
            {
                string value = string.Format("{0}.{1}", t.Namespace, t.Name);

                if (trimArgCount && value.IndexOf("`") > -1)
                {
                    value = value.Substring(0, value.IndexOf("`"));
                }

                if (t.DeclaringType != null)
                {
                    // This is a nested type, build the nesting type first
                    value = GetCSharpRepresentation(t.DeclaringType, trimArgCount, availableArguments) + "+" + value;
                }

                // Build the type arguments (if any)
                string argString = "";
                var thisTypeArgs = t.GetGenericArguments();
                for (int i = 0; i < thisTypeArgs.Length && availableArguments.Count > 0; i++)
                {
                    if (i != 0) argString += ", ";

                    argString += GetCSharpRepresentation(availableArguments[0], trimArgCount);
                    availableArguments.RemoveAt(0);
                }

                // If there are type arguments, add them with < >
                if (argString.Length > 0)
                {
                    value += "<" + argString + ">";
                }

                return value;
            }

            return t.FullName;
        }
    }
}
