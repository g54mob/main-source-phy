using System;
using Steamworks;

namespace Heathen.SteamworksIntegration
{
	[Serializable]
	public struct ScreenshotReady
	{
		public ScreenshotReady_t Data;

		public readonly ScreenshotHandle Handle => default(ScreenshotHandle);

		public readonly EResult Result => default(EResult);

		public static implicit operator ScreenshotReady(ScreenshotReady_t native)
		{
			return default(ScreenshotReady);
		}

		public static implicit operator ScreenshotReady_t(ScreenshotReady heathen)
		{
			return default(ScreenshotReady_t);
		}
	}
}
