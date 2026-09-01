using System;
using System.Collections.Generic;
using FMOD;

namespace FMODUnity
{
	public class PlatformPlayInEditor : Platform
	{
		private static List<CodecChannelCount> staticCodecChannels;

		internal override string DisplayName => null;

		internal override bool IsIntrinsic => false;

		internal override List<CodecChannelCount> DefaultCodecChannels => null;

		internal override void DeclareRuntimePlatforms(Settings settings)
		{
		}

		internal override string GetBankFolder()
		{
			return null;
		}

		internal override void LoadStaticPlugins(FMOD.System coreSystem, Action<RESULT, string> reportResult)
		{
		}

		internal override void InitializeProperties()
		{
		}
	}
}
