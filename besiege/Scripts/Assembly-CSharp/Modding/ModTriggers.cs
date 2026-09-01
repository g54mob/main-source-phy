using System;
using System.Reflection;
using InternalModding.Assemblies;
using InternalModding.Mods;
using InternalModding.Triggers;
using Modding.Levels;

namespace Modding
{
	public static class ModTriggers
	{
		public delegate void OnTriggerChanged(Entity entity, Action activate, bool removed);

		public static Action GetCallback(int id)
		{
			ModContainer modByAssembly = AssemblyLoader.GetModByAssembly(Assembly.GetCallingAssembly());
			if (modByAssembly == null)
			{
				throw new InvalidOperationException("ModTriggers.GetCallback called by an assembly not listed in the manifest.");
			}
			return SingleInstanceFindOnly<TriggerLoader>.Instance.GetGlobalCallback(modByAssembly, id);
		}

		public static void RegisterCallback(int id, OnTriggerChanged callback)
		{
			ModContainer modByAssembly = AssemblyLoader.GetModByAssembly(Assembly.GetCallingAssembly());
			if (modByAssembly == null)
			{
				throw new InvalidOperationException("ModTriggers.RegisterCallback called by an assembly not listed in the manifest.");
			}
			SingleInstanceFindOnly<TriggerLoader>.Instance.RegisterLocalCallback(modByAssembly, id, callback);
		}
	}
}
