using System.Collections.Generic;

namespace FMODUnity
{
	public class PlatformLinux : Platform
	{
		private static List<CodecChannelCount> staticCodecChannels;

		internal override string DisplayName => null;

		internal override List<CodecChannelCount> DefaultCodecChannels => null;

		static PlatformLinux()
		{
		}

		internal override void DeclareRuntimePlatforms(Settings settings)
		{
		}

		internal override string GetPluginPath(string pluginName)
		{
			return null;
		}
	}
}
