using System;
using System.CodeDom.Compiler;
using System.IO;
using System.Linq;
using System.Reflection;
using CSharpCompiler;
using InternalModding.Misc;
using InternalModding.Mods;

namespace InternalModding.Assemblies
{
	public static class AssemblyCompiler
	{
		public static string ResolveScriptAssembly(string codeDir, ModContainer mod)
		{
			DirectoryInfo directoryInfo = new DirectoryInfo(codeDir);
			if (!directoryInfo.Exists)
			{
				MLog.Error("Code directory " + codeDir + " does not exist!");
				return string.Empty;
			}
			string assemblyPath = ModPaths.GetAssemblyPath(mod, directoryInfo.Name);
			if (File.Exists(assemblyPath))
			{
				return assemblyPath;
			}
			CompilerParameters compilerParameters = new CompilerParameters();
			compilerParameters.GenerateExecutable = false;
			compilerParameters.GenerateInMemory = false;
			compilerParameters.OutputAssembly = assemblyPath;
			CompilerParameters compilerParameters2 = compilerParameters;
			compilerParameters2.ReferencedAssemblies.AddRange((from a in AppDomain.CurrentDomain.GetAssemblies().Where(delegate(Assembly a)
				{
					try
					{
						return !string.IsNullOrEmpty(a.Location);
					}
					catch (NotSupportedException)
					{
						return false;
					}
				})
				select a.Location).ToArray());
			string[] array = (from f in directoryInfo.GetFiles("*.cs", SearchOption.AllDirectories)
				select f.FullName).ToArray();
			if (array.Length == 0)
			{
				MLog.Error("Code directory " + codeDir + " does not contain any source files!");
			}
			CSharpCompiler.CodeCompiler codeCompiler = new CSharpCompiler.CodeCompiler();
			CompilerResults compilerResults = codeCompiler.CompileAssemblyFromFileBatch(compilerParameters2, array);
			foreach (CompilerError error in compilerResults.Errors)
			{
				string message = error.ToString();
				if (error.IsWarning)
				{
					MLog.Warn(message);
				}
				else
				{
					MLog.Error(message);
				}
			}
			if (compilerResults.Errors.HasErrors)
			{
				MLog.Error("There were errors compiling the ScriptAssembly at " + codeDir + "!");
			}
			return assemblyPath;
		}
	}
}
