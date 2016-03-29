using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ScriptCs.Contracts;
using ScriptCs.Engine.Roslyn;
using ScriptCs.Hosting;

namespace ScriptRunner
{
    class Program
    {
        static void Main(string[] args)
        {
            var console = (IConsole)new ScriptConsole();
            var logProvider = new ColoredConsoleLogProvider(LogLevel.Info, console);

            var builder = new ScriptServicesBuilder(console, logProvider);
            builder.ScriptEngine<RoslynScriptEngine>();

            var scriptcs = builder.Build();

            scriptcs.Executor.Initialize(new[] { "System.Web" }, Enumerable.Empty<IScriptPack>());

            scriptcs.Executor.AddReferences(Assembly.GetExecutingAssembly());

            scriptcs.Executor.Execute("test.csx");

            scriptcs.Executor.Terminate();
        }
    }
}
