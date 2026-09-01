using System;
using System.Collections.Generic;
using System.Linq;
using InternalModding;
using InternalModding.Loading;
using InternalModding.Mods;

namespace Modding
{
	public static class Mods
	{
		private static List<Action<Guid>> onModLoadHandlers = new List<Action<Guid>>();

		public static event Action<Guid> OnModLoaded
		{
			add
			{
				onModLoadHandlers.Add(value);
				foreach (ModContainer item in ModManager.Mods.Where((ModContainer m) => m.IsEnabled || m.IsActive))
				{
					value(item.Info.Id);
				}
			}
			remove
			{
				onModLoadHandlers.Remove(value);
			}
		}

		public static bool IsModLoaded(Guid id)
		{
			ModContainer modById = ModIds.GetModById(id);
			return modById != null;
		}

		public static Version GetVersion(Guid id)
		{
			ModContainer modById = ModIds.GetModById(id);
			if (modById == null)
			{
				return null;
			}
			return modById.Info.Version;
		}

		internal static void InvokeModLoaded(ModContainer mod)
		{
			foreach (Action<Guid> onModLoadHandler in onModLoadHandlers)
			{
				onModLoadHandler(mod.Info.Id);
			}
		}
	}
}
