using System;
using System.Linq;
using UnityEngine;

namespace BitCode.Profiles
{
	[Serializable]
	public class PlatformCapabilitiesCollection
	{
		private sealed class ZXjJJXdfjUCYdxswWWjOSJImoRgO
		{
			public RuntimePlatform SFQiukZZFSozYOjTapriXcVSYiGD;

			internal bool LhTqeQdPOTkgPFLyFcngidXHGLVNA(PlatformCapabilities P_0)
			{
				return P_0.Platform == SFQiukZZFSozYOjTapriXcVSYiGD;
			}
		}

		internal const string CapabilitiesFieldName = "platformCapabilities";

		[SerializeField]
		private PlatformCapabilities[] platformCapabilities;

		public PlatformCapabilities GetCapabilitiesForPlatform(RuntimePlatform runtimePlatform)
		{
			ZXjJJXdfjUCYdxswWWjOSJImoRgO zXjJJXdfjUCYdxswWWjOSJImoRgO = new ZXjJJXdfjUCYdxswWWjOSJImoRgO();
			zXjJJXdfjUCYdxswWWjOSJImoRgO.SFQiukZZFSozYOjTapriXcVSYiGD = runtimePlatform;
			return platformCapabilities.FirstOrDefault(zXjJJXdfjUCYdxswWWjOSJImoRgO.LhTqeQdPOTkgPFLyFcngidXHGLVNA);
		}
	}
}
