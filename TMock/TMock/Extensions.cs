using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMock
{
    public static class Extensions
    {
        public static bool HasProperty(this object obj, string propertyName)
        {
            return obj.GetType().GetProperty(propertyName) != null;
        }

        public static object GetPropValue(this object src, string propName)
        {
            return src.GetType().GetProperty(propName).GetValue(src, null);
        }

        public static string ToPlaneString(this IEnumerable<string> strings)
        {
            var sb = new StringBuilder();

            if (strings != null)
                foreach (var s in strings)
                {
                    sb.AppendLine(s);
                }

            return sb.ToString();
        }

    }
}
