using System;
using FMOD;

namespace FMODUnity
{
	public class PlatformAppleTV : Platform
	{
		internal override string DisplayName => null;

		static PlatformAppleTV()
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
