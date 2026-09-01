using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using InternalModding.Blocks;
using InternalModding.LevelEntities;
using InternalModding.Loading;
using InternalModding.Misc;
using InternalModding.Mods;
using Microsoft.Win32;
using UnityEngine;
using mattmc3.dotmore.Collections.Generic;

namespace InternalModding.Projects
{
	public static class ModCreator
	{
		private static void OnAnyCommandExecute()
		{
			if (!OptionsMaster.BesiegeConfig.ShowDebugLogs)
			{
				OptionsMaster.BesiegeConfig.ShowDebugLogs = true;
				MLog.Info("Enabled log output to console after executing a create* command.\nCan be disabled with 'show_logs false'.");
			}
		}

		public static void CreateModCmd(string[] args)
		{
			if (args.Length != 1 && args.Length != 2)
			{
				ReferenceMaster.ConsoleController.AppendLogLine("Usage: createmod <name> [mod projects folder].");
				return;
			}
			OnAnyCommandExecute();
			CreateMod(args[0], (args.Length != 2) ? string.Empty : args[1], ReferenceMaster.ConsoleController.AppendLogLine);
		}

		public static void CreateBlockCmd(string[] args)
		{
			IConsoleController consoleController = ReferenceMaster.ConsoleController;
			if (args.Length != 2)
			{
				consoleController.AppendLogLine("Usage: createblock <modid | name> <block name>.");
				return;
			}
			ModContainer modContainer = ParseModArgument(args[0]);
			if (modContainer != null)
			{
				OnAnyCommandExecute();
				CreateBlock(modContainer, args[1], ReferenceMaster.ConsoleController.AppendLogLine);
			}
		}

		public static void CreateEntityCmd(string[] args)
		{
			IConsoleController consoleController = ReferenceMaster.ConsoleController;
			if (args.Length != 2)
			{
				consoleController.AppendLogLine("Usage: createentity <modid | name> <entity name>.");
				return;
			}
			ModContainer modContainer = ParseModArgument(args[0]);
			if (modContainer != null)
			{
				OnAnyCommandExecute();
				CreateEntity(modContainer, args[1], ReferenceMaster.ConsoleController.AppendLogLine);
			}
		}

		public static void CreateAssemblyCmd(string[] args)
		{
			IConsoleController consoleController = ReferenceMaster.ConsoleController;
			string message = "Usage: createassembly <modid | name> <compiled | script> <assembly name> <default namespace> [noUnityTools | forceUnityTools].";
			if (args.Length != 4 && args.Length != 5)
			{
				consoleController.AppendLogLine(message);
				return;
			}
			ModContainer modContainer = ParseModArgument(args[0]);
			if (modContainer == null)
			{
				return;
			}
			string text = args[2];
			if (text.EndsWith(".dll"))
			{
				text = text.Replace(".dll", string.Empty);
			}
			bool scriptAssembly;
			if (args[1] == "script")
			{
				scriptAssembly = true;
			}
			else
			{
				if (!(args[1] == "compiled"))
				{
					consoleController.AppendLogLine(message);
					return;
				}
				scriptAssembly = false;
			}
			bool noUnityTools = false;
			bool forceUnityTools = false;
			if (args.Length == 5)
			{
				if (args[4].Equals("nounitytools", StringComparison.InvariantCultureIgnoreCase))
				{
					noUnityTools = true;
				}
				else
				{
					if (!args[4].Equals("forceunitytools", StringComparison.InvariantCultureIgnoreCase))
					{
						consoleController.AppendLogLine(message);
						return;
					}
					forceUnityTools = true;
				}
			}
			OnAnyCommandExecute();
			CreateAssembly(modContainer, scriptAssembly, text, args[3], noUnityTools, forceUnityTools, ReferenceMaster.ConsoleController.AppendLogLine);
		}

		private static ModContainer ParseModArgument(string arg)
		{
			ModContainer mod;
			int modByIdOrName = ModIds.GetModByIdOrName(arg, out mod);
			if (mod == null)
			{
				switch (modByIdOrName)
				{
				case -1:
					ReferenceMaster.ConsoleController.AppendLogLine("Can't find mod: " + arg);
					break;
				case -2:
					ReferenceMaster.ConsoleController.AppendLogLine("Name is ambiguous: " + arg + ". Try using the ID.");
					break;
				}
				return null;
			}
			return mod;
		}

		public static void CreateMod(string name, string rootModsDir, Action<string> output)
		{
			string[] array = name.Split(' ');
			string text = array[0];
			for (int i = 1; i < array.Length; i++)
			{
				text = text + char.ToUpper(array[i][0]) + array[i].Substring(1);
			}
			if (string.IsNullOrEmpty(rootModsDir))
			{
				rootModsDir = ModManager.DefaultModPath;
			}
			if (!Directory.Exists(rootModsDir))
			{
				output("Error: Root directory " + rootModsDir + " does not exist.");
				return;
			}
			DirectoryInfo directoryInfo = new DirectoryInfo(Path.Combine(rootModsDir, text + " Project"));
			if (directoryInfo.Exists)
			{
				output("Error: Mod directory " + directoryInfo.FullName + " already exists!");
				return;
			}
			directoryInfo.Create();
			string path = Path.Combine(directoryInfo.FullName, text);
			DirectoryInfo directoryInfo2 = new DirectoryInfo(path);
			if (directoryInfo2.Exists)
			{
				output("Error: Mod with the given name already exists!");
				return;
			}
			directoryInfo2.Create();
			Guid guid = Guid.NewGuid();
			string text2 = UnityEngine.Resources.Load<TextAsset>("Modding/ProjectTemplates/Manifest").text;
			string contents = text2.Replace("%NAME%", name).Replace("%ID%", ModInfo.GetNewIdText(guid));
			File.WriteAllText(Path.Combine(directoryInfo2.FullName, "Mod.xml"), contents);
			Directory.CreateDirectory(Path.Combine(directoryInfo2.FullName, "Resources"));
			OptionsMaster.BesiegeConfig.AdditionalModsDirectories = OptionsMaster.BesiegeConfig.AdditionalModsDirectories.Append(directoryInfo.FullName).ToArray();
			output("Created mod with ID " + guid);
			output("Mod was placed in " + directoryInfo2.FullName);
			output("Remember to open the manifest file and modify as appropriate!");
			output("Now loading the new mod.");
			BesiegeConsoleController.AddModDirSource.AddDir(directoryInfo2.Parent.FullName + "/");
		}

		public static void CreateBlock(ModContainer mod, string name, Action<string> output)
		{
			if (mod.Info.FromWorkshop)
			{
				output("Error: Cannot modify mod downloaded from workshop.");
				return;
			}
			if (mod.Blocks.Any((ModdedBlock block) => block.Name == name))
			{
				output("Error: Block with the given name already exists in the mod!");
				return;
			}
			string directory = mod.Info.Directory;
			string text = name.Replace(" ", string.Empty) + ".xml";
			FileInfo fileInfo = new FileInfo(Path.Combine(directory, text));
			if (fileInfo.Exists)
			{
				output("Error: " + text + " already exists in the mod!");
				return;
			}
			int id;
			for (id = 1; mod.Blocks.Any((ModdedBlock block) => block.LocalId == id); id++)
			{
			}
			string text2 = UnityEngine.Resources.Load<TextAsset>("Modding/ProjectTemplates/Block").text;
			string contents = text2.Replace("%NAME%", name).Replace("%ID%", id.ToString());
			File.WriteAllText(fileInfo.FullName, contents);
			string path = Path.Combine(directory, "Mod.xml");
			string[] lineArr = File.ReadAllLines(path);
			string elementText = "<Block path=\"" + fileInfo.Name + "\" />";
			string[] newText;
			if (!ManifestInsertInto(lineArr, "Blocks", elementText, out newText, output))
			{
				output("Could not add block to manifest. You must add the block manually.");
			}
			else
			{
				File.WriteAllLines(path, newText);
				output("Added block to manifest.");
			}
			output("Created block. Remember to open the definition file and insert appropriate values.");
			output("After doing that, you will need to restart the game to load the new block.");
		}

		public static void CreateEntity(ModContainer mod, string name, Action<string> output)
		{
			if (mod.Info.FromWorkshop)
			{
				output("Error: Cannot modify mod downloaded from workshop.");
				return;
			}
			if (mod.Entities.Any((ModdedEntity entity) => entity.Name == name))
			{
				output("Error: Entity with the given name already exists in the mod!");
				return;
			}
			string directory = mod.Info.Directory;
			string text = name.Replace(" ", string.Empty) + ".xml";
			FileInfo fileInfo = new FileInfo(Path.Combine(directory, text));
			if (fileInfo.Exists)
			{
				output("Error: " + text + " already exists in the mod!");
				return;
			}
			int id;
			for (id = 1; mod.Entities.Any((ModdedEntity entity) => entity.LocalId == id); id++)
			{
			}
			string text2 = UnityEngine.Resources.Load<TextAsset>("Modding/ProjectTemplates/Entity").text;
			string contents = text2.Replace("%NAME%", name).Replace("%ID%", id.ToString());
			File.WriteAllText(fileInfo.FullName, contents);
			string path = Path.Combine(directory, "Mod.xml");
			string[] lineArr = File.ReadAllLines(path);
			string elementText = "<Entity path=\"" + fileInfo.Name + "\" />";
			string[] newText;
			if (!ManifestInsertInto(lineArr, "Entities", elementText, out newText, output))
			{
				output("Could not add the entity to the manifest. You must add the entity manually.");
			}
			else
			{
				File.WriteAllLines(path, newText);
				output("Added entity to manifest.");
			}
			output("Created entity. Remember to open the definition file and insert appropriate values.");
			output("After doing that, you will need to restart the game to load the new entity.");
		}

		public static void CreateAssembly(ModContainer mod, bool scriptAssembly, string assemblyName, string defaultNamespace, bool noUnityTools, bool forceUnityTools, Action<string> output)
		{
			if (assemblyName.Contains(" "))
			{
				output("Error: Assembly name should not contain spaces.");
				return;
			}
			if (mod.Info.FromWorkshop)
			{
				output("Error: Cannot modify mod downloaded from workshop.");
				return;
			}
			if (mod.Info.Assemblies.Any((ModInfo.AssemblyInfo assembly) => new FileInfo(assembly.Path).Name.Equals(assemblyName, StringComparison.InvariantCultureIgnoreCase)))
			{
				output("Error: Assembly with the given name already exists in the mod!");
				return;
			}
			string directory = mod.Info.Directory;
			if (!scriptAssembly && string.Equals(new DirectoryInfo(directory).Parent.FullName.TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar), new DirectoryInfo(ModManager.DefaultModPath).FullName.TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar), StringComparison.InvariantCultureIgnoreCase))
			{
				output("Error: The createassembly command can only be used for a compiled assembly if the mod uses the recommended project structure.");
				return;
			}
			if (!scriptAssembly)
			{
				DirectoryInfo directoryInfo = new DirectoryInfo(directory).Parent.CreateSubdirectory("src");
				DirectoryInfo directoryInfo2 = new DirectoryInfo(Path.Combine(directoryInfo.FullName, assemblyName));
				if (directoryInfo2.Exists)
				{
					output("Error: The directory " + directoryInfo2.FullName + " already exists!");
					return;
				}
				directoryInfo2.Create();
				bool flag = !noUnityTools;
				if (!forceUnityTools && flag && !CheckForUnityProfile())
				{
					output("Warning: It looks like the Unity Tools for Visual Studio may not be installed.\n\t\t The project will be generated using the default .NET 3.5 Profile instead.\n\t\t If you do have the tools installed, you can re-create the assembly with the\n\t\t forceUnityTools argument to bypass this check.\n\t\t See the createassembly documentation for more details.");
					flag = false;
				}
				string text = UnityEngine.Resources.Load<TextAsset>("Modding/ProjectTemplates/ModEntryPoint").text;
				string text2 = UnityEngine.Resources.Load<TextAsset>("Modding/ProjectTemplates/AssemblyProjectInfo").text;
				string text3 = UnityEngine.Resources.Load<TextAsset>("Modding/ProjectTemplates/AssemblyProjectFile").text;
				string text4 = UnityEngine.Resources.Load<TextAsset>("Modding/ProjectTemplates/AssemblyProjectUserFile").text;
				Guid assemblyGuid = Guid.NewGuid();
				string name = new DirectoryInfo(directory).Name;
				string text5 = Application.dataPath;
				if (Application.platform == RuntimePlatform.OSXPlayer)
				{
					text5 += "MacOS/Besiege";
				}
				else if (Application.platform == RuntimePlatform.WindowsPlayer)
				{
					text5 += "/../Besiege.exe";
				}
				string contents = text.Replace("%NAMESPACE%", defaultNamespace);
				string contents2 = text2.Replace("%GUID%", assemblyGuid.ToString()).Replace("%ASSEMBLYNAME%", assemblyName).Replace("%MODNAME%", name);
				string contents3 = text3.Replace("%GUID%", assemblyGuid.ToString("B")).Replace("%NAMESPACE%", defaultNamespace).Replace("%ASSEMBLYNAME%", assemblyName)
					.Replace("%MODNAME%", name)
					.Replace("%TARGETFRAMEWORKPROFILE%", (!flag) ? string.Empty : "Unity Full v3.5");
				string contents4 = text4.Replace("%BESIEGEPATH%", text5);
				DirectoryInfo directoryInfo3 = directoryInfo2.CreateSubdirectory("Properties");
				File.WriteAllText(Path.Combine(directoryInfo2.FullName, "Mod.cs"), contents);
				File.WriteAllText(Path.Combine(directoryInfo2.FullName, assemblyName + ".csproj"), contents3);
				File.WriteAllText(Path.Combine(directoryInfo2.FullName, assemblyName + ".csproj.user"), contents4);
				File.WriteAllText(Path.Combine(directoryInfo3.FullName, "AssemblyInfo.cs"), contents2);
				string text6 = UnityEngine.Resources.Load<TextAsset>("Modding/ProjectTemplates/AssemblySolution").text;
				string text7 = Path.Combine(directoryInfo.FullName, name + ".sln");
				if (!File.Exists(text7))
				{
					string contents5 = text6.Replace("%SOLUTIONGUID%", Guid.NewGuid().ToString("B"));
					File.WriteAllText(text7, contents5);
				}
				InsertProjectIntoSolution(text7, assemblyGuid, assemblyName, output);
				output("Created Visual Studio / MonoDevelop project.");
				string path = Path.Combine(directory, "Mod.xml");
				string[] lineArr = File.ReadAllLines(path);
				string elementText = "<Assembly path=\"" + assemblyName + ".dll\" />";
				string[] newText;
				if (!ManifestInsertInto(lineArr, "Assemblies", elementText, out newText, output))
				{
					output("Could not add assembly to manifest. You must add the assembly manually.");
					return;
				}
				File.WriteAllLines(path, newText);
				output("Added assembly to manifest.");
				return;
			}
			DirectoryInfo directoryInfo4 = new DirectoryInfo(Path.Combine(directory, assemblyName + "/"));
			if (directoryInfo4.Exists)
			{
				output("Source directory already exists, not creating assembly! (" + directoryInfo4.FullName + ")");
				return;
			}
			directoryInfo4.Create();
			string text8 = UnityEngine.Resources.Load<TextAsset>("Modding/ProjectTemplates/ModEntryPoint").text;
			string contents6 = text8.Replace("%NAMESPACE%", defaultNamespace);
			File.WriteAllText(Path.Combine(directoryInfo4.FullName, "Mod.cs"), contents6);
			output("Created ScriptAssembly in " + directoryInfo4);
			string path2 = Path.Combine(directory, "Mod.xml");
			string[] lineArr2 = File.ReadAllLines(path2);
			string elementText2 = "<ScriptAssembly path=\"" + assemblyName + "/\" />";
			string[] newText2;
			if (!ManifestInsertInto(lineArr2, "Assemblies", elementText2, out newText2, output))
			{
				output("Could not add assembly to manifest. You must add the assembly manually.");
				return;
			}
			File.WriteAllLines(path2, newText2);
			output("Added assembly to manifest.");
		}

		private static void InsertProjectIntoSolution(string solPath, Guid assemblyGuid, string assemblyName, Action<string> output)
		{
			string text = UnityEngine.Resources.Load<TextAsset>("Modding/ProjectTemplates/SolutionProjectBlock").text;
			string text2 = UnityEngine.Resources.Load<TextAsset>("Modding/ProjectTemplates/SolutionProjectConfigBlock").text;
			string item = text.Replace("%PROJECTGUID%", Guid.NewGuid().ToString("B")).Replace("%ASSEMBLYNAME%", assemblyName).Replace("%GUID%", assemblyGuid.ToString("B"));
			string item2 = text2.Replace("%GUID%", assemblyGuid.ToString("B"));
			List<string> list = File.ReadAllLines(solPath).ToList();
			int num = list.FindIndex((string line) => line.Equals("Global"));
			if (num == -1)
			{
				output("Could not read solution file! You must create a solution / add the project to the solution manually.");
				return;
			}
			list.Insert(num, item);
			num = list.FindIndex((string line) => line.Trim().StartsWith("GlobalSection(ProjectConfigurationPlatforms)"));
			if (num == -1)
			{
				output("Could not read solution file! You must create a solution / add the project to the solution manually.");
				return;
			}
			list.Insert(num + 1, item2);
			File.WriteAllLines(solPath, list.ToArray());
		}

		private static bool ManifestInsertInto(string[] lineArr, string parentName, string elementText, out string[] newText, Action<string> output)
		{
			List<string> list = lineArr.ToList();
			int num = list.FindIndex((string line) => line.TrimStart().TrimEnd().Equals("</" + parentName + ">"));
			if (num == -1)
			{
				num = list.FindIndex((string line) => line.StartsWith("<!-- This value is automatically generated."));
				if (num == -1)
				{
					output("Error: Mod Manifest was modified in such a way that it cannot be automatically edited.");
					newText = null;
					return false;
				}
				num--;
				list.Insert(num, "\t<" + parentName + ">");
				list.Insert(num + 1, "\t</" + parentName + ">");
				num++;
			}
			list.Insert(num, "\t\t" + elementText);
			newText = list.ToArray();
			return true;
		}

		private static bool CheckForUnityProfile()
		{
			try
			{
				string text = (string)Registry.GetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\.NETFramework\\AssemblyFolders\\v3.5", "All Assemblies In", string.Empty);
				if (string.IsNullOrEmpty(text))
				{
					return false;
				}
				DirectoryInfo directoryInfo = new DirectoryInfo(Path.Combine(text, "..\\.NETFramework\\v3.5\\Profile\\Unity Full v3.5"));
				if (directoryInfo.Exists)
				{
					return true;
				}
				directoryInfo = new DirectoryInfo(directoryInfo.FullName.Replace("Program Files", "Program Files (x86)"));
				if (directoryInfo.Exists)
				{
					return true;
				}
				return false;
			}
			catch (Exception)
			{
				return false;
			}
		}
	}
}
