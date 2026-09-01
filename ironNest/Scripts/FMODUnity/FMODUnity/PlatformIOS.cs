using System;
using FMOD;

namespace FMODUnity
{
	public class PlatformIOS : Platform
	{
		internal override string DisplayName => null;

		static PlatformIOS()
		{
		}

		internal override void DeclareRuntimePlatforms(Settings settings)
		{
		}

		internal override void LoadPlugins(FMOD.System coreSystem, Action<RESULT, string> reportResult)
		{
		}

		public static void StaticLoadPlugins(Platform platform, FMOD.System coreSystem, Action<RESULT, string> reportResult)
		{
		}
	}
}
