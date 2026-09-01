using System;
using System.IO;
using InternalModding.Mods;

namespace InternalModding.Loading.Sources
{
	public interface IModSource
	{
		void GetMods(Action<DirectoryInfo, Action<ModContainer>> registerMod, Action allModsFound);
	}
}
