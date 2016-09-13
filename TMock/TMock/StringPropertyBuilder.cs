using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMock
{
    internal class StringPropertyBuilder
    {
        /// <summary>
        /// Builds and returns properties in a given type.
        /// </summary>
        /// <param name="t">t</param>
        /// <returns>IEnumerable of string</returns>
        public IEnumerable<string> BuildProperty(Type t)
        {
            var propInfos = t.GetProperties();
            foreach (System.Reflection.PropertyInfo propInfo in propInfos)
            {
                var propBuilder = new StringBuilder();

                propBuilder.AppendLine(string.Format("      private {0} _{1};", propInfo.PropertyType.FullName, propInfo.Name));
                propBuilder.AppendLine(string.Format("      public {0} {1}", propInfo.PropertyType.FullName, propInfo.Name));
                propBuilder.AppendLine("        {");
                if (propInfo.CanRead)
                {
                    propBuilder.AppendLine("            get {");
                    propBuilder.AppendLine("                if(_data != null)");
                    propBuilder.AppendLine("                {");
                    propBuilder.AppendLine(string.Format("              var first = _data.FirstOrDefault(f=>f.Method==\"{0}\");", propInfo.ToString()));
                    propBuilder.AppendLine("                    if(first!=null)");
                    propBuilder.AppendLine("                    {");
                    propBuilder.AppendLine("                        if(first.ExpectedArgument!=null)");
                    propBuilder.AppendLine("                        {");
                    propBuilder.AppendLine(string.Format("                          if(first.ExpectedArgument.Func !=null) _{1} = ({0})first.ExpectedArgument.Func();", propInfo.PropertyType.ToString(), propInfo.Name));
                    propBuilder.AppendLine("                        }");
                    propBuilder.AppendLine("                    }");
                    propBuilder.AppendLine("                }");
                    propBuilder.AppendLine(string.Format("          return _{0};", propInfo.Name));
                    propBuilder.AppendLine("                }");
                }
                if (propInfo.CanWrite)
                {
                    propBuilder.AppendLine("            set {");
                    propBuilder.AppendLine("                if(_data != null)");
                    propBuilder.AppendLine("                {");
                    propBuilder.AppendLine(string.Format("              var first = _data.FirstOrDefault(f=>f.Method==\"{0}\");", propInfo.ToString()));
                    propBuilder.AppendLine("                    if(first!=null)");
                    propBuilder.AppendLine("                    {");
                    propBuilder.AppendLine("                        if(first.ExpectedArgument!=null)");
                    propBuilder.AppendLine("                        {");
                    propBuilder.AppendLine(string.Format("                          if(first.ExpectedArgument.Func !=null) value = ({0})first.ExpectedArgument.Func();", propInfo.PropertyType.ToString()));
                    propBuilder.AppendLine("                        }");
                    propBuilder.AppendLine("                    }");
                    propBuilder.AppendLine("                }");
                    propBuilder.AppendLine(string.Format("          _{0} = value;", propInfo.Name));
                    propBuilder.AppendLine("                }");
                }
                propBuilder.AppendLine("        }");
                yield return propBuilder.ToString();
            }
        }
    }
}
