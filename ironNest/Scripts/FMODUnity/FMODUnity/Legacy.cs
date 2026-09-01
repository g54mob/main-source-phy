using System;
using System.Collections.Generic;

namespace FMODUnity
{
	internal static class Legacy
	{
		[Serializable]
		public enum Platform
		{
			None = 0,
			PlayInEditor = 1,
			Default = 2,
			Desktop = 3,
			Mobile = 4,
			MobileHigh = 5,
			MobileLow = 6,
			Console = 7,
			Windows = 8,
			Mac = 9,
			Linux = 10,
			iOS = 11,
			Android = 12,
			Deprecated_1 = 13,
			XboxOne = 14,
			PS4 = 15,
			Deprecated_2 = 16,
			Deprecated_3 = 17,
			AppleTV = 18,
			UWP = 19,
			Switch = 20,
			WebGL = 21,
			Deprecated_4 = 22,
			Reserved_1 = 23,
			Reserved_2 = 24,
			Reserved_3 = 25,
			Count = 26
		}

		public class PlatformSettingBase
		{
			public Platform Platform;
		}

		public class PlatformSetting<T> : PlatformSettingBase
		{
			public T Value;
		}

		[Serializable]
		public class PlatformIntSetting : PlatformSetting<int>
		{
		}

		[Serializable]
		public class PlatformStringSetting : PlatformSetting<string>
		{
		}

		[Serializable]
		public class PlatformBoolSetting : PlatformSetting<TriStateBool>
		{
		}

		public static void CopySetting<T, U>(List<T> list, Platform fromPlatform, Platform toPlatform) where T : new()
		{
		}

		public static void CopySetting(List<PlatformBoolSetting> list, Platform fromPlatform, Platform toPlatform)
		{
		}

		public static void CopySetting(List<PlatformIntSetting> list, Platform fromPlatform, Platform toPlatform)
		{
		}

		public static string DisplayName(Platform platform)
		{
			return null;
		}

		public static float SortOrder(Platform legacyPlatform)
		{
			return 0f;
		}

		public static Platform Parent(Platform platform)
		{
			return default(Platform);
		}

		public static bool IsGroup(Platform platform)
		{
			return false;
		}
	}
}
