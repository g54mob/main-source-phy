using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using InternalModding.Misc;
using InternalModding.Mods;
using Mono.Cecil;
using Mono.Cecil.Cil;
using UnityEngine;

namespace InternalModding.Assemblies
{
	public class AssemblyScanner
	{
		private static readonly string[] BlacklistNamespaces = new string[21]
		{
			"System.IO", "System.Net", "System.Xml", "System.Reflection", "System.Runtime.InteropServices", "System.Diagnostics", "System.Security", "Mono.CSharp", "Mono.Cecil", "System.CodeDom.Compiler",
			"CSharpCompiler", "IKVM", "Microsoft", "Mono.CompilerServices", "UnityEngine.WWW", "UnityEngine.MasterServer", "PlayFab", "Steamworks", "GameGrind", "InternalModding",
			"BesiegeDlc"
		};

		private static readonly string[] BlacklistMethods = new string[4] { "XmlSaver.Save", "LevelXMLSaver.Create", "UnityEngine.AssetBundle.LoadFromFile", "UnityEngine.AssetBundle.LoadFromFileAsync" };

		private static readonly string[] WhitelistedTypes = new string[12]
		{
			"System.IO.Stream", "System.IO.TextWriter", "System.IO.TextReader", "System.IO.BinaryWriter", "System.IO.BinaryReader", "System.IO.MemoryStream", "System.IO.Path", "System.IO.SeekOrigin", "System.Diagnostics.Stopwatch", "System.Security.Cryptography",
			"Mono.CSharp.Tuple`2", "Mono.CSharp.Tuple`3"
		};

		private ModInfo.AssemblyInfo CurrentlyScanning;

		public static bool Scan(ModInfo.AssemblyInfo info, List<Assembly> additionalRefs)
		{
			if (BesiegeLogFilter.logDev)
			{
				Debug.LogFormat("[AssemblyScanner] Scanning {0}.", info.Path);
			}
			try
			{
				AssemblyScanner assemblyScanner = new AssemblyScanner();
				assemblyScanner.CurrentlyScanning = info;
				AssemblyDefinition assemblyDefinition = AssemblyDefinition.ReadAssembly(info.Path);
				Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
				if (BesiegeLogFilter.logDev)
				{
					Debug.LogFormat("[AssemblyScanner] Got loaded assembly list:\n{0}.", string.Join("\n", assemblies.Select((Assembly a) => a.FullName + "(" + a.CodeBase + ")").ToArray()));
				}
				foreach (ModuleDefinition module in assemblyDefinition.Modules)
				{
					AssemblyNameReference reference;
					foreach (AssemblyNameReference assemblyReference in module.AssemblyReferences)
					{
						reference = assemblyReference;
						if (BesiegeLogFilter.logDev)
						{
							Debug.LogFormat("[AssemblyScanner] Checking if reference {0} is loaded.", reference.FullName);
						}
						if (assemblies.Any((Assembly a) => a.GetName().Name == reference.Name))
						{
							if (BesiegeLogFilter.logDev)
							{
								Debug.LogFormat("[AssemblyScanner] Found matching loaded assembly!");
							}
							continue;
						}
						if (BesiegeLogFilter.logDev)
						{
							Debug.LogFormat("[AssemblyScanner] Did not find matching loaded assembly!");
						}
						Assembly assembly;
						try
						{
							assembly = Assembly.ReflectionOnlyLoad(reference.FullName);
						}
						catch (FileNotFoundException)
						{
							string text = Path.Combine(new FileInfo(info.Path).DirectoryName, reference.Name + ".dll");
							if (!File.Exists(text))
							{
								continue;
							}
							assembly = Assembly.ReflectionOnlyLoadFrom(text);
						}
						additionalRefs.Add(assembly);
						ModInfo.AssemblyInfo assemblyInfo = new ModInfo.AssemblyInfo();
						assemblyInfo.Path = assembly.Location;
						ModInfo.AssemblyInfo info2 = assemblyInfo;
						if (Scan(info2, additionalRefs))
						{
							continue;
						}
						MLog.Error("Referenced assembly did not validate: " + reference.Name);
						MLog.Error("Not loading " + assemblyDefinition.Name);
						return false;
					}
					foreach (TypeDefinition type in module.Types)
					{
						if (!assemblyScanner.ScanType(type))
						{
							return false;
						}
					}
				}
				return true;
			}
			catch (Exception exception)
			{
				Debug.LogError("Error scanning assembly, not loading!");
				Debug.LogException(exception);
				return false;
			}
		}

		private bool ScanType(TypeDefinition type)
		{
			foreach (FieldDefinition field in type.Fields)
			{
				if (IsForbidden(field.FieldType))
				{
					LogError(field, field.FieldType);
					return false;
				}
			}
			foreach (MethodDefinition method in type.Methods)
			{
				if (method.HasPInvokeInfo)
				{
					LogError("You are not allowed to use PInvoke!");
					return false;
				}
				if (!method.HasBody)
				{
					continue;
				}
				foreach (VariableDefinition variable in method.Body.Variables)
				{
					if (IsForbidden(variable.VariableType))
					{
						LogError(method, variable.VariableType);
						return false;
					}
				}
				foreach (Instruction instruction in method.Body.Instructions)
				{
					if (instruction.OpCode == OpCodes.Ldfld)
					{
						FieldReference fieldReference = (FieldReference)instruction.Operand;
						if (IsForbidden(fieldReference.DeclaringType))
						{
							LogError(method, fieldReference.DeclaringType);
						}
						if (IsForbidden(fieldReference.FieldType))
						{
							LogError(method, fieldReference.FieldType);
							return false;
						}
					}
					else if (instruction.OpCode == OpCodes.Call || instruction.OpCode == OpCodes.Callvirt || instruction.OpCode == OpCodes.Calli || instruction.OpCode == OpCodes.Newobj)
					{
						MethodReference methodReference = (MethodReference)instruction.Operand;
						if (IsForbidden(methodReference))
						{
							LogError(method, methodReference);
							return false;
						}
						GenericInstanceMethod genericInstanceMethod = methodReference as GenericInstanceMethod;
						if (genericInstanceMethod == null)
						{
							continue;
						}
						foreach (TypeReference genericArgument in genericInstanceMethod.GenericArguments)
						{
							if (IsForbidden(genericArgument))
							{
								LogError(method, genericArgument);
								return false;
							}
						}
					}
					else if (instruction.OpCode == OpCodes.Newarr)
					{
						TypeReference typeReference = (TypeReference)instruction.Operand;
						if (IsForbidden(typeReference))
						{
							LogError(method, typeReference);
							return false;
						}
					}
					else
					{
						if (!(instruction.OpCode == OpCodes.Ldtoken))
						{
							continue;
						}
						IMetadataTokenProvider metadataTokenProvider = (IMetadataTokenProvider)instruction.Operand;
						switch (metadataTokenProvider.MetadataToken.TokenType)
						{
						case TokenType.TypeRef:
						{
							TypeReference typeReference2 = (TypeReference)metadataTokenProvider;
							if (IsForbidden(typeReference2))
							{
								LogError(method, typeReference2);
								return false;
							}
							break;
						}
						case TokenType.MemberRef:
						{
							MemberReference memberReference = (MemberReference)metadataTokenProvider;
							if (IsForbidden(memberReference.DeclaringType))
							{
								LogError(method, memberReference.DeclaringType);
								return false;
							}
							break;
						}
						}
					}
				}
			}
			foreach (TypeDefinition nestedType in type.NestedTypes)
			{
				if (!ScanType(nestedType))
				{
					return false;
				}
			}
			return true;
		}

		private static bool IsForbidden(TypeReference type)
		{
			return IsForbiddenType(type.Namespace, type.Name);
		}

		private static bool IsForbidden(MethodReference method)
		{
			return IsForbiddenMethod(method.DeclaringType.Namespace, method.DeclaringType.Name, method.Name);
		}

		private static bool IsForbiddenType(string nspace, string name)
		{
			if (BlacklistNamespaces.Any((nspace + "." + name).StartsWith))
			{
				return !WhitelistedTypes.Contains(nspace + "." + name);
			}
			return false;
		}

		private static bool IsForbiddenMethod(string nspace, string type, string method)
		{
			if (IsForbiddenType(nspace, type))
			{
				return true;
			}
			if (BlacklistMethods.Contains(nspace + "." + type + "." + method))
			{
				return true;
			}
			return false;
		}

		private void LogError(string msg)
		{
			MLog.Error("[Security] " + msg);
			MLog.Error("[Security] Not loading " + CurrentlyScanning.Path);
		}

		private void LogError(string location, string forbiddenType)
		{
			LogError("You are not allowed to use " + forbiddenType + " (used in " + location + ").");
		}

		private void LogError(MemberReference location, TypeReference forbiddenType)
		{
			LogError(location.FullName, forbiddenType.FullName);
		}

		private void LogError(MemberReference location, MethodReference forbiddenMethod)
		{
			LogError(location.FullName, forbiddenMethod.FullName);
		}
	}
}
