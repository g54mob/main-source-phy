using System.Collections.Generic;

namespace Kamgam.SettingsGenerator
{
	public static class SettingCollectionExtensions
	{
		public static IList<ISetting> PullFromConnection(this IList<ISetting> settings)
		{
			return null;
		}

		public static IList<ISetting> PushToConnection(this IList<ISetting> settings)
		{
			return null;
		}

		public static IList<ISetting> RefreshRegisteredResolvers(this IList<ISetting> settings, Settings settingsObj)
		{
			return null;
		}

		public static IList<ISetting> RefreshRegisteredResolvers(this IList<ISetting> settings, SettingsProvider provider)
		{
			return null;
		}
	}
}
