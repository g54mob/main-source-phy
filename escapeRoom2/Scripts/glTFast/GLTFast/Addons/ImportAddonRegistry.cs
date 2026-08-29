using System.Collections.Generic;

namespace GLTFast.Addons
{
	public static class ImportAddonRegistry
	{
		private static List<ImportAddon> s_Addons;

		public static void RegisterImportAddon(ImportAddon addon)
		{
			CertifyDefaultAddonsRegistered();
			s_Addons.Add(addon);
		}

		internal static void InjectAllAddons(GltfImportBase gltfImport)
		{
			CertifyDefaultAddonsRegistered();
			foreach (ImportAddon s_Addon in s_Addons)
			{
				s_Addon.CreateImportInstance(gltfImport);
			}
		}

		private static void CertifyDefaultAddonsRegistered()
		{
			if (s_Addons == null)
			{
				s_Addons = new List<ImportAddon>();
			}
		}
	}
}
