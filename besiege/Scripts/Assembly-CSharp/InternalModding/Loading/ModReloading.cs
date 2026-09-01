using System;
using System.Collections.Generic;
using System.IO;
using InternalModding.Misc;
using InternalModding.Mods;

namespace InternalModding.Loading
{
	public static class ModReloading
	{
		private static List<FileSystemWatcher> fileWatchers = new List<FileSystemWatcher>();

		public static event Action<ModContainer, ModInfo> OnModReload;

		public static void Initialize()
		{
			ModManager.OnModLoad += OnModLoad;
		}

		public static void OnQuit()
		{
			foreach (FileSystemWatcher fileWatcher in fileWatchers)
			{
				fileWatcher.Dispose();
			}
			fileWatchers.Clear();
		}

		private static void OnModLoad(ModContainer mod)
		{
			if (!mod.Info.DebugEnabled)
			{
				return;
			}
			FileSystemWatcher fileSystemWatcher = new FileSystemWatcher(mod.Info.Directory);
			fileSystemWatcher.Filter = "*.xml";
			fileSystemWatcher.NotifyFilter = NotifyFilters.LastWrite;
			fileSystemWatcher.IncludeSubdirectories = true;
			FileSystemWatcher fileSystemWatcher2 = fileSystemWatcher;
			fileSystemWatcher2.Changed += delegate
			{
				UnityMainThreadDispatcher.Instance().Enqueue(delegate
				{
					ReloadMod(mod);
				});
			};
			fileSystemWatcher2.EnableRaisingEvents = true;
			fileWatchers.Add(fileSystemWatcher2);
		}

		private static void ReloadMod(ModContainer mod)
		{
			if (!mod.Info.DebugEnabled || !SingleInstanceFindOnly<AddPiece>.hasInstance())
			{
				return;
			}
			ModInfo modInfo = ModInfo.LoadFromFile(Path.Combine(mod.Info.Directory, "Mod.xml"), true);
			if (modInfo == null)
			{
				MLog.Warn("Not reloading " + mod.Info.Name);
				return;
			}
			if (ModReloading.OnModReload != null)
			{
				ModReloading.OnModReload(mod, modInfo);
			}
			MLog.Info("Reloaded " + mod.Info.Name);
		}
	}
}
