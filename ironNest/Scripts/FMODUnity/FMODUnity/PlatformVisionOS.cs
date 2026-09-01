using System;
using FMOD;

namespace FMODUnity
{
	public class PlatformVisionOS : Platform
	{
		internal override string DisplayName => null;

		static PlatformVisionOS()
		{
		}

		internal override void DeclareRuntimePlatforms(Settings settings)
		{
		}

		internal override void LoadPlugins(FMOD.System coreSystem, Action<RESULT, string> reportResult)
		{
		}
	}
}
