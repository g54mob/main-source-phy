using System;
using System.IO;
using InternalModding.Mods;

namespace InternalModding.Loading.Sources
{
	public class DefaultModSource : IModSource
	{
		public void GetMods(Action<DirectoryInfo, Action<ModContainer>> registerMod, Action allModsFound)
		{
			DirectoryInfo directoryInfo = new DirectoryInfo(ModManager.DefaultModPath);
			DirectoryInfo[] directories = directoryInfo.GetDirectories();
			foreach (DirectoryInfo directoryInfo2 in directories)
			{
				if (File.Exists(Path.Combine(directoryInfo2.FullName, "Mod.xml")))
				{
					registerMod(directoryInfo2, null);
					continue;
				}
				DirectoryInfo[] directories2 = directoryInfo2.GetDirectories();
				foreach (DirectoryInfo directoryInfo3 in directories2)
				{
					if (File.Exists(Path.Combine(directoryInfo3.FullName, "Mod.xml")))
					{
						registerMod(directoryInfo3, null);
					}
				}
			}
			allModsFound();
		}
	}
}
