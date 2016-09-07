using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Services;
using System.Text;
using System.Threading.Tasks;

namespace TMock
{
    internal class StringMethodBuilder
    {
        public IEnumerable<string> BuildMethods(Type t)
        {
            var methodInfos = t.GetMethods();

            foreach (System.Reflection.MethodInfo methodInfo in methodInfos)
            {
                var methodBuilder = new StringBuilder();

                var paramList = new List<Param>();
                var argument = BuildParameters(methodInfo, paramList);

                methodBuilder.AppendFormat("{0} {1}({2})", methodInfo.ReturnType == typeof(void) ? "void" : methodInfo.ReturnType.FullName, methodInfo.Name, argument);
                methodBuilder.AppendLine();
                methodBuilder.AppendLine("{");

                methodBuilder.AppendLine("  if(_data != null)");
                methodBuilder.AppendLine("  {");

                methodBuilder.AppendLine(string.Format("        var first = _data.FirstOrDefault(f=>f.Method==\"{0}\");", methodInfo.ToString()));
                methodBuilder.AppendLine("      if(first!=null)");
                methodBuilder.AppendLine("      {");

                methodBuilder.AppendLine("          if(first.ExpectedArgument!=null)");
                methodBuilder.AppendLine("          {");
                methodBuilder.AppendLine("              bool isMatch = true;");
                int count = 0;
                foreach (var param in paramList)
                {
                    methodBuilder.AppendLine(string.Format("              if(!first.ExpectedArgument.Arguments[{0}].IsAny && ({1})first.ExpectedArgument.Arguments[{0}].Value != {2})",count,param.Type.ToString().TrimEnd('&'),param.Name) + " { isMatch =false; }");
                    count++;
                }
                methodBuilder.AppendLine("                  if(isMatch)");
                methodBuilder.AppendLine("                  {");
                methodBuilder.AppendLine("                      if(first.ExpectedArgument.ParamSetValue !=null)");
                methodBuilder.AppendLine("                      {");
                int pcount = 0;
                foreach (var param in paramList)
                {
                    methodBuilder.AppendLine(string.Format("                        if(first.ExpectedArgument.ParamSetValue.HasProperty(\"{0}\"))", param.Name) + "{" + param.Name + " = (" + param.Type.ToString().TrimEnd('&') + ") first.ExpectedArgument.ParamSetValue.GetPropValue(\""+param.Name+"\"); }");
                }
                methodBuilder.AppendLine("                      }");
                if (methodInfo.ReturnType != typeof (void))
                {
                    methodBuilder.AppendLine(string.Format("                      if(first.ExpectedArgument.Func !=null) return ({0})first.ExpectedArgument.Func()", methodInfo.ReturnType.ToString()));
                }
                else
                {
                    methodBuilder.AppendLine("                      if(first.ExpectedArgument.Action !=null) first.ExpectedArgument.Action()");
                }
                methodBuilder.AppendLine("                  }");
                methodBuilder.AppendLine("                  else { throw new TMock.ArgumentsNotMatchedException(\"Arguments passed in for the method '"+ methodInfo.Name +"' does not match the expected arguments. \"); }");
                
                methodBuilder.AppendLine("          }");
                if (methodInfo.ReturnType != typeof(void))
                {
                    methodBuilder.AppendFormat("            return default({0});", methodInfo.ReturnType.FullName);
                    methodBuilder.AppendLine();
                }
                methodBuilder.AppendLine("      }");
                if (methodInfo.ReturnType != typeof(void))
                {
                    methodBuilder.AppendFormat("        return default({0});", methodInfo.ReturnType.FullName);
                    methodBuilder.AppendLine();
                }

                methodBuilder.AppendLine("  } ");

                if (methodInfo.ReturnType != typeof(void))
                {
                    methodBuilder.AppendFormat("    return default({0});", methodInfo.ReturnType.FullName);
                    methodBuilder.AppendLine();
                }

                methodBuilder.AppendLine();
                methodBuilder.AppendLine("}");

                yield return methodBuilder.ToString();
            }
        }

        private string BuildParameters(System.Reflection.MethodInfo methodInfo, ICollection<Param> paramList)
        {
            var argumentBuilder = new StringBuilder();
            foreach (var param in methodInfo.GetParameters())
            {
                var paramString = param.ToString();
                if (param.ParameterType.ToString().EndsWith("&"))
                {
                    if (param.IsOut)
                    {
                        paramString = "out " + param.ParameterType.ToString().TrimEnd('&') + " " + param.Name;
                    }
                    else
                    {
                        paramString = "ref " + param.ParameterType.ToString().TrimEnd('&') + " " + param.Name;
                    }
                }

                paramList.Add(new Param() { Name = param.Name, Type = param.ParameterType });
                argumentBuilder.AppendFormat("{0},", paramString);
            }

            return argumentBuilder.ToString().TrimEnd(',');
        }

    }

    internal class Param
    {
        public Type Type { get; set; }

        public string Name { get; set; }
    }
}
