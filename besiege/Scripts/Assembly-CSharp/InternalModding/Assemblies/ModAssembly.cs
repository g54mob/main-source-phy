using System.Collections.Generic;
using System.Reflection;
using InternalModding.Mods;
using Modding;

namespace InternalModding.Assemblies
{
	public class ModAssembly
	{
		public ModInfo.AssemblyInfo Info { get; private set; }

		public Assembly Assembly { get; set; }

		public List<Assembly> AdditionalReferences { get; private set; }

		public bool HasModEntryPoint { get; set; }

		public ModEntryPoint ModEntryPoint { get; set; }

		public ModAssembly(ModInfo.AssemblyInfo info, List<Assembly> additionalReferences)
		{
			Info = info;
			AdditionalReferences = additionalReferences;
		}
	}
}
