using System;
using System.IO;
using InternalModding.Mods;
using InternalModding.Workshop;
using UnityEngine;

namespace InternalModding.Loading.Sources
{
	public class WorkshopModSource : IModSource
	{
		public Action<DirectoryInfo, Action<ModContainer>> RegisterMod;

		public WorkshopModSource()
		{
			ModWorkshopManager.SetWorkshopSource(this);
		}

		public void GetMods(Action<DirectoryInfo, Action<ModContainer>> registerMod, Action allModsFound)
		{
			RegisterMod = registerMod;
			if (BesiegeLogFilter.logDev)
			{
				Debug.LogFormat("[WorkshopModSource::GetMods] calling LoadSubscribedMods");
			}
			ModWorkshopManager.LoadSubscribedMods(registerMod, allModsFound);
		}
	}
}
