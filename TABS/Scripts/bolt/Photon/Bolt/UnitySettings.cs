using UnityEngine;

namespace Photon.Bolt
{
	public static class UnitySettings
	{
		public enum DotNetVersion
		{
			DOTNET_35 = 0,
			DOTNET_4x = 1
		}

		public static bool IsBuildMono;

		public static bool IsBuildDotNet;

		public static bool IsBuildIL2CPP;

		public static DotNetVersion CurrentDotNetVersion;

		public static RuntimePlatform CurrentPlatform;
	}
}
