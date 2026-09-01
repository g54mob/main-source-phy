using System.IO;
using InternalModding.Mods;
using Modding.Blocks;
using Modding.Levels;
using UnityEngine;

namespace Modding
{
	public abstract class ModEntryPoint
	{
		internal ModContainer ModContainer;

		public virtual string Path(bool cleanPath = false)
		{
			return (!cleanPath) ? ModContainer.Info.Directory : new DirectoryInfo(ModContainer.Info.Directory).FullName;
		}

		public virtual void OnLoad()
		{
		}

		public virtual bool IsRequiredForMachine(PlayerMachineInfo machine)
		{
			return false;
		}

		public virtual bool IsRequiredForLevel(Level editor)
		{
			return false;
		}

		public virtual void OnBlockPrefabCreation(int blockId, GameObject prefab, GameObject ghost)
		{
		}

		public virtual void OnEntityPrefabCreation(int entityId, GameObject prefab)
		{
		}
	}
}
