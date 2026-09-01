using System.Collections.Generic;

namespace TND.Upscaling.Framework
{
	public static class UpscalerPluginRegistry
	{
		private static readonly List<IUpscalerPlugin> UpscalerPlugins;

		public static IReadOnlyList<IUpscalerPlugin> GetUpscalerPlugins()
		{
			return null;
		}

		public static bool AnySupported()
		{
			return false;
		}

		public static void Cleanup()
		{
		}

		public static void RegisterUpscalerPlugin(IUpscalerPlugin upscalerPlugin)
		{
		}

		public static IUpscalerPlugin FindUpscalerPlugin(string upscalerPluginIdentifier)
		{
			return null;
		}

		public static IUpscalerPlugin FindUpscalerPlugin(UpscalerName upscalerName)
		{
			return null;
		}
	}
}
