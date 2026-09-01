using System.Collections.Generic;

namespace FMODUnity
{
	public class PlatformMac : Platform
	{
		private static List<CodecChannelCount> staticCodecChannels;

		internal override string DisplayName => null;

		internal override List<CodecChannelCount> DefaultCodecChannels => null;

		static PlatformMac()
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
