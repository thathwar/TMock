using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMock
{
    public static class TypeBuilder
    {
        public static T Create<T>()
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

                    public int Add(int i, int j)
                    {
                       return i+j;
                    }
               }

             }");

            var providerOptions = new Dictionary<string, string>();
            providerOptions["CompilerVersion"] = "v4.0"; //Need to pass the right version here dynamically
            var provider = new Microsoft.CSharp.CSharpCodeProvider(providerOptions);

            var parameters = new System.CodeDom.Compiler.CompilerParameters
            {
                GenerateExecutable = false,
                GenerateInMemory = true,
                IncludeDebugInformation = true
            };
            parameters.ReferencedAssemblies.Add("System.dll");
            parameters.ReferencedAssemblies.Add(typeof(System.Linq.Enumerable).Assembly.Location);
            parameters.ReferencedAssemblies.Add(System.Reflection.Assembly.GetCallingAssembly().Location);
            parameters.ReferencedAssemblies.Add(System.Reflection.Assembly.GetExecutingAssembly().Location);

            System.CodeDom.Compiler.CompilerResults results = provider.CompileAssemblyFromSource(parameters, sb.ToString());

            if (results.Errors.Count == 0)
            {
                Type generated = results.CompiledAssembly.GetType("TMockDynamic.RuntimeImplementor");
                if (generated != null)
                {
                    var constructorInfo = generated.GetConstructor(Type.EmptyTypes);
                    if (constructorInfo != null)
                        timplementor = (T)constructorInfo.Invoke(null);
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
