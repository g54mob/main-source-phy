using System.IO;
using InternalModding.Blocks;
using InternalModding.Misc;
using InternalModding.Mods;
using Modding;

namespace InternalModding.Loading
{
	public static class Maintenance
	{
		private const string ConfigKey = "maintenance-lastMods";

		private const string GameVersionConfigKey = "maintenance-lastGameVersion";

		private static ModList lastMods;

		private static string lastGameVersion;

		private static string currentGameVersion;

		public static void Initialize()
		{
			XDataHolder data = Configuration.GetData();
			if (!data.HasKey("maintenance-lastMods"))
			{
				lastMods = ModList.GetEmpty();
			}
			else
			{
				lastMods = ModList.FromStringArray(data.ReadStringArray("maintenance-lastMods"));
			}
			if (!data.HasKey("maintenance-lastGameVersion"))
			{
				lastGameVersion = VersionNumber.GetVersionString();
			}
			else
			{
				lastGameVersion = data.ReadString("maintenance-lastGameVersion");
			}
			currentGameVersion = VersionNumber.GetVersionString();
			data.Write("maintenance-lastGameVersion", currentGameVersion);
			ModManager.OnPreModLoad += OnModInfoLoad;
			ModManager.OnInitialModsLoaded += OnAllModsFound;
		}

		public static void OnAllModsFound()
		{
			ModList localAll = ModList.GetLocalAll();
			foreach (ModList.Mod mod in lastMods.Mods)
			{
				bool flag = false;
				foreach (ModList.Mod mod2 in localAll.Mods)
				{
					if (mod.Id == mod2.Id)
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					ModRemoved(mod);
				}
			}
			XDataHolder data = Configuration.GetData();
			data.Write("maintenance-lastMods", localAll.GetStringArray());
		}

		public static void OnModInfoLoad(ModContainer mod)
		{
			foreach (ModList.Mod mod3 in lastMods.Mods)
			{
				if (mod3.Id == mod.Info.Id)
				{
					ModList.Mod mod2 = ModList.Mod.FromContainer(mod);
					if (mod3.Name != mod2.Name)
					{
						NameChanged(mod2, mod3);
					}
					if (mod3.Version != mod2.Version || mod.Info.DebugEnabled || lastGameVersion != currentGameVersion)
					{
						VersionChanged(mod2, mod3, mod.Info.DebugEnabled && lastGameVersion == currentGameVersion);
					}
					break;
				}
			}
		}

		private static void NameChanged(ModList.Mod newMod, ModList.Mod oldMod)
		{
			MLog.Info("[Mod Maintenance] Detected a name change: " + oldMod.Name + " -> " + newMod.Name);
			string path = Configuration.GetPath(newMod.Name, newMod.Id.ToString());
			string path2 = Configuration.GetPath(oldMod.Name, oldMod.Id.ToString());
			if (File.Exists(path2))
			{
				File.Move(path2, path);
			}
		}

		private static void VersionChanged(ModList.Mod newMod, ModList.Mod oldMod, bool fromDebug)
		{
			if (!fromDebug)
			{
				MLog.Info("[Mod Maintenance] Detected a version change: " + newMod.Name + " (" + oldMod.Version + " -> " + newMod.Version + ")");
			}
			else
			{
				MLog.Info("[Mod Maintenance] Treating " + newMod.Name + " as version change because Debug is enabled.");
			}
			RemoveBlockTypeIcons(oldMod);
			RemoveCompiledAssemblies(oldMod);
		}

		private static void ModRemoved(ModList.Mod oldMod)
		{
			MLog.Info("[Mod Maintenance] Detected a removed mod: " + oldMod.Name);
			RemoveBlockTypeIcons(oldMod);
			string path = Configuration.GetPath(oldMod.Name, oldMod.Id.ToString());
			if (File.Exists(path))
			{
				File.Delete(path);
			}
			RemoveCompiledAssemblies(oldMod);
		}

		private static void RemoveBlockTypeIcons(ModList.Mod mod)
		{
			string thumbnailDirectory = BlockTypeIconCreator.GetThumbnailDirectory();
			DirectoryInfo directoryInfo = new DirectoryInfo(thumbnailDirectory);
			if (directoryInfo.Exists)
			{
				FileInfo[] files = directoryInfo.GetFiles(string.Concat(mod.Id, "_*"));
				FileInfo[] array = files;
				foreach (FileInfo fileInfo in array)
				{
					fileInfo.Delete();
				}
			}
		}

		private static void RemoveCompiledAssemblies(ModList.Mod mod)
		{
			string assemblyDirectory = ModPaths.GetAssemblyDirectory();
			DirectoryInfo directoryInfo = new DirectoryInfo(assemblyDirectory);
			if (directoryInfo.Exists)
			{
				FileInfo[] files = directoryInfo.GetFiles(string.Concat(mod.Id, "_*"));
				FileInfo[] array = files;
				foreach (FileInfo fileInfo in array)
				{
					fileInfo.Delete();
				}
			}
		}
	}
}
