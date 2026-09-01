using System.Collections.Generic;
using System.Reflection;
using InternalModding.Assemblies;
using InternalModding.Events;
using InternalModding.Mods;
using Modding.Levels;

namespace Modding
{
	public static class ModEvents
	{
		public delegate void OnEventExecute(LogicChain logic, IDictionary<string, EventProperty> properties);

		public static void RegisterCallback(int id, OnEventExecute callback)
		{
			ModContainer modByAssembly = AssemblyLoader.GetModByAssembly(Assembly.GetCallingAssembly());
			SingleInstanceFindOnly<EventLoader>.Instance.RegisterCallback(modByAssembly, id, callback);
		}
	}
}
