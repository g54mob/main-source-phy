using System;
using System.IO;
using InternalModding.Misc;
using InternalModding.Mods;
using UnityEngine;

namespace InternalModding.Loading.Sources
{
	public class AdditionalDirsModSource : IModSource
	{
		private Action<DirectoryInfo> registerModAction;

		public AdditionalDirsModSource()
		{
			BesiegeConsoleController.AddModDirSource = this;
		}

		public void AddDir(string path)
		{
			DirectoryInfo directoryInfo = new DirectoryInfo(path);
			if (!directoryInfo.Exists)
			{
				return;
			}
			DirectoryInfo[] directories = directoryInfo.GetDirectories();
			foreach (DirectoryInfo directoryInfo2 in directories)
			{
				if (File.Exists(Path.Combine(directoryInfo2.FullName, "Mod.xml")))
				{
					registerModAction(directoryInfo2);
					continue;
				}
				DirectoryInfo[] directories2 = directoryInfo2.GetDirectories();
				foreach (DirectoryInfo directoryInfo3 in directories2)
				{
					if (File.Exists(Path.Combine(directoryInfo3.FullName, "Mod.xml")))
					{
						registerModAction(directoryInfo3);
					}
				}
			}
		}

		public void GetMods(Action<DirectoryInfo, Action<ModContainer>> registerMod, Action allModsFound)
		{
			registerModAction = delegate(DirectoryInfo dir)
			{
				registerMod(dir, null);
			};
			string[] additionalModsDirectories = OptionsMaster.BesiegeConfig.AdditionalModsDirectories;
			string[] array = additionalModsDirectories;
			foreach (string path in array)
			{
				AddDir(path);
			}
			BesiegeEntryPoint besiegeEntryPoint = UnityEngine.Object.FindObjectOfType<BesiegeEntryPoint>();
			if (besiegeEntryPoint != null)
			{
				Arguments args = besiegeEntryPoint.Args;
				if (args.Exists("mod"))
				{
					foreach (string item in args["mod"])
					{
						if (File.Exists(Path.Combine(item, "Mod.xml")))
						{
							registerMod(new DirectoryInfo(item), null);
						}
						else
						{
							MLog.Warn("-mod argument: " + item + " is not a mod directory!");
						}
					}
				}
			}
			allModsFound();
		}
	}
}
