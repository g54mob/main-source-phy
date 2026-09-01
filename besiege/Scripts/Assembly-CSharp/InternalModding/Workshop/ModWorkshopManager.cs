using System;
using System.Collections.Generic;
using System.IO;
using InternalModding.Loading.Sources;
using InternalModding.Misc;
using InternalModding.Mods;
using UnityEngine;

namespace InternalModding.Workshop
{
	public class ModWorkshopManager
	{
		private static WorkshopModSource workshopSource;

		public static void Upload(UploadData uploadData)
		{
			if (ReferenceMaster.IsPlatformReady())
			{
				WorkshopManager instance = SingleInstance<WorkshopManager>.Instance;
				instance.CreateWorkshopMod(uploadData);
			}
		}

		public static void SetWorkshopSource(WorkshopModSource src)
		{
			workshopSource = src;
		}

		public static void LoadSubscribedMods(Action<DirectoryInfo, Action<ModContainer>> loadMod, Action allModsFound)
		{
			if (BesiegeLogFilter.logDev)
			{
				Debug.Log("[ModWorkshopManager::LoadSubscribedMods]");
			}
			if (!ReferenceMaster.IsPlatformReady())
			{
				if (BesiegeLogFilter.logDev)
				{
					Debug.Log("[ModWorkshopManager::LoadSubscribedMods] SteamManager not initialized");
				}
				allModsFound();
				return;
			}
			WorkshopManager instance = SingleInstance<WorkshopManager>.Instance;
			if (BesiegeLogFilter.logDev)
			{
				Debug.Log("[ModWorkshopManager::LoadSubscribedMods] Manager==" + instance);
			}
			instance.GetSubscribedWorkshopItemsAsync(WorkshopManager.ItemTypes.Mods, WorkshopManager.InstallType.Installed, delegate(List<WorkshopManager.WorkshopItem> items)
			{
				if (BesiegeLogFilter.logDev)
				{
					Debug.LogFormat("[ModWorkshopManager::LoadSubscribedMods] callback, got {0} items", items.Count);
				}
				foreach (WorkshopManager.WorkshopItem item in items)
				{
					LoadItem(item, loadMod);
				}
				allModsFound();
			});
		}

		public static void OnNewModInstalled(WorkshopManager.WorkshopItem item)
		{
			if (workshopSource == null)
			{
				Debug.LogError("OnNewModInstalled called before workshopSource was set!");
			}
			else
			{
				LoadItem(item, workshopSource.RegisterMod);
			}
		}

		private static void LoadItem(WorkshopManager.WorkshopItem item, Action<DirectoryInfo, Action<ModContainer>> loadMod)
		{
			DirectoryInfo directoryInfo = new DirectoryInfo(item.RootFolder);
			DirectoryInfo[] directories = directoryInfo.GetDirectories();
			if (directories.Length > 1)
			{
				MLog.Warn("Too many directories in mod workshop item!");
				return;
			}
			if (directories.Length == 0)
			{
				MLog.Warn("No directory in mod workshop item!");
				return;
			}
			loadMod(directories[0], delegate(ModContainer mod)
			{
				mod.Info.WorkshopId = item.WorkshopId;
				mod.Info.FromWorkshop = true;
			});
		}
	}
}
