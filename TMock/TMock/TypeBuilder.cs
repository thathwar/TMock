using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CSharp;

namespace TMock
{
    internal static class TypeBuilder
    {
        
        public static T Create<T>(List<MethodInfo> methodInfos)
        {
            T timplementor = default(T);

            var sb = new System.Text.StringBuilder();

            BuildImplementoString(sb, typeof(T));
            
            var parameters = new System.CodeDom.Compiler.CompilerParameters
            {
                GenerateExecutable = false,
                GenerateInMemory = true,
                IncludeDebugInformation = true
            };

            BuildParameters(parameters);

            CompilerResults results = GetProvider().CompileAssemblyFromSource(parameters, sb.ToString());

            if (results.Errors.Count == 0)
            {
                Type generated = results.CompiledAssembly.GetType("TMockDynamic.RuntimeImplementor");
                if (generated != null)
                {
                    var types = new Type[1];
                    types[0] = typeof(List<MethodInfo>);
                    var constructorInfo = generated.GetConstructor(types);
                    if (constructorInfo != null)
                    {
                        var objcts = new object[1];
                        objcts[0] = methodInfos;
                        timplementor = (T)constructorInfo.Invoke(objcts);
                       
                    }
                }
            }
            else
            {
                throw new Exception("Unable to mock " + CSharpTypeNameBuilder.GetCSharpRepresentation(typeof(T),true));
            }

            return timplementor;
        }

        private static void BuildParameters(CompilerParameters parameters)
        {
            parameters.ReferencedAssemblies.Add("System.dll");

            foreach (string dll in Directory.GetFiles(Environment.CurrentDirectory, "*.dll"))
                parameters.ReferencedAssemblies.Add(dll);

            foreach (string dll in Directory.GetFiles(Environment.CurrentDirectory, "*.exe"))
                parameters.ReferencedAssemblies.Add(dll);

            parameters.ReferencedAssemblies.Add(typeof(System.Linq.Enumerable).Assembly.Location);
        }

        private static void BuildImplementoString(StringBuilder sb, Type t)
        {
            sb.Append(@"
            using System;
            using System.Collections.Generic;
            using System.Linq;
            using TMock;

            namespace TMockDynamic
            {
                public class RuntimeImplementor:").Append(CSharpTypeNameBuilder.GetCSharpRepresentation(t, true)).Append(@"
                {").Append(@"
                   
                    private List<MethodInfo> _data;

                    public RuntimeImplementor(object data){ _data=(List<MethodInfo>)data; }

//METHODS#

//PROPS#

               }

             }".Replace(@"//METHODS#", new StringMethodBuilder().BuildMethods(t).ToPlaneString()).Replace(@"//PROPS#", new StringPropertyBuilder().BuildProperty(t).ToPlaneString()));
        }

        private static CSharpCodeProvider GetProvider()
        {
            var providerOptions = new Dictionary<string, string>();

            var versionSplits = typeof(string).Assembly.ImageRuntimeVersion.Split('.');
            providerOptions["CompilerVersion"] = string.Format("{0}.{1}", versionSplits[0], versionSplits[1]);

            return new CSharpCodeProvider(providerOptions);
        }

    }
}
