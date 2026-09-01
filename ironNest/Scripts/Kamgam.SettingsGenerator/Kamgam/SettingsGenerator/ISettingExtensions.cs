using UnityEngine;

namespace Kamgam.SettingsGenerator
{
	public static class ISettingExtensions
	{
		public static float GetFloatValue(this ISetting setting)
		{
			return 0f;
		}

		public static float GetIntValue(this ISetting setting)
		{
			return 0f;
		}

		public static bool GetBoolValue(this ISetting setting)
		{
			return false;
		}

		public static string GetStringValue(this ISetting setting)
		{
			return null;
		}

		public static Color GetColorValue(this ISetting setting)
		{
			return default(Color);
		}

		public static int GetColorOptionValue(this ISetting setting)
		{
			return 0;
		}

		public static KeyCombination GetKeyCombinationValue(this ISetting setting)
		{
			return default(KeyCombination);
		}

		public static int GetOptionValue(this ISetting setting)
		{
			return 0;
		}
	}
}
