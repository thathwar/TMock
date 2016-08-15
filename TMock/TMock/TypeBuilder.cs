using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TMock
{
    public static class TypeBuilder
    {
        public static T Create<T>(object oputut)
        {
            T timplementor = default(T);

            var sb = new System.Text.StringBuilder();
            sb.Append(@"
            using System;
            using System.Collections.Generic;
            using System.Linq;

            namespace TMockDynamic
            {
                public class RuntimeImplementor:").Append(typeof(T).FullName).Append(@"
                {").Append(@"
                   
                    private object _data;

                  public RuntimeImplementor(object data){
                    _data=data;
                        }

                    public TestAssembly.Response Add(int i, int j)
                    {
                       return (TestAssembly.Response)_data;
                    }
               }

             }");

            var providerOptions = new Dictionary<string, string>();

            var versionSplits = typeof (string).Assembly.ImageRuntimeVersion.Split('.');
            providerOptions["CompilerVersion"] = string.Format("{0}.{1}", versionSplits[0], versionSplits[1]);

            var provider = new Microsoft.CSharp.CSharpCodeProvider(providerOptions);

            var parameters = new System.CodeDom.Compiler.CompilerParameters
            {
                GenerateExecutable = false,
                GenerateInMemory = true,
                IncludeDebugInformation = true
            };

            parameters.ReferencedAssemblies.Add("System.dll");

            foreach (string dll in Directory.GetFiles(Environment.CurrentDirectory, "*.dll"))
                parameters.ReferencedAssemblies.Add(dll);

            foreach (string dll in Directory.GetFiles(Environment.CurrentDirectory, "*.exe"))
                parameters.ReferencedAssemblies.Add(dll);

            parameters.ReferencedAssemblies.Add(typeof(System.Linq.Enumerable).Assembly.Location);

            System.CodeDom.Compiler.CompilerResults results = provider.CompileAssemblyFromSource(parameters, sb.ToString());

            if (results.Errors.Count == 0)
            {
                Type generated = results.CompiledAssembly.GetType("TMockDynamic.RuntimeImplementor");
                if (generated != null)
                {
                    var types = new Type[1];
                    types[0] = typeof (object);
                    var constructorInfo = generated.GetConstructor(types);
                    if (constructorInfo != null)
                    {
                        var objcts = new object[1];
                        objcts[0] = oputut;
                        timplementor = (T)constructorInfo.Invoke(objcts);
                       
                    }
                }
            }
            else
            {
                throw new Exception("Unable to mock " + typeof(T).FullName);
            }

            return timplementor;
        }
    }
}
