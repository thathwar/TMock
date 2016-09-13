using System.Collections.Generic;
using System.Text;

namespace TMock
{
    /// <summary>
    /// Provides extension methods for the TMock framework.
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Checks and returns whether the property name exists in the given object.
        /// </summary>
        /// <param name="obj">obj</param>
        /// <param name="propertyName">propertyName</param>
        /// <returns>bool</returns>
        public static bool HasProperty(this object obj, string propertyName)
        {
            return obj.GetType().GetProperty(propertyName) != null;
        }

        /// <summary>
        /// Returns the property value from the object for a given property name.
        /// </summary>
        /// <param name="src">src</param>
        /// <param name="propName">propName</param>
        /// <returns>object</returns>
        public static object GetPropValue(this object src, string propName)
        {
            return src.GetType().GetProperty(propName).GetValue(src, null);
        }

        /// <summary>
        /// Returns the concated string for given enumertion of strings
        /// </summary>
        /// <param name="strings">strings</param>
        /// <returns>string</returns>
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
