using Steamworks;

namespace Heathen.SteamworksIntegration.API
{
	public static class Screenshots
	{
		public static class Client
		{
			public static bool IsScreenshotsHooked
			{
				get
				{
					return false;
				}
				set
				{
				}
			}

			public static ScreenshotHandle AddScreenshotToLibrary(string imageFilename, string thumbnailFileName, int width, int height)
			{
				return default(ScreenshotHandle);
			}

			public static ScreenshotHandle AddVRScreenshotToLibrary(EVRScreenshotType type, string imageFilename, string vrFilename)
			{
				return default(ScreenshotHandle);
			}

			public static void HookScreenshots(bool hook)
			{
			}

			public static bool SetLocation(ScreenshotHandle handle, string location)
			{
				return false;
			}

			public static bool TagPublishedFile(ScreenshotHandle handle, PublishedFileId_t ugcFileId)
			{
				return false;
			}

			public static bool TagUser(ScreenshotHandle handle, CSteamID userId)
			{
				return false;
			}

			public static void TriggerScreenshot()
			{
			}

			public static ScreenshotHandle WriteScreenshot(byte[] data, int width, int height)
			{
				return default(ScreenshotHandle);
			}
		}
	}
}
