using plog.unity.Models;

namespace plog.unity.Managers
{
	public static class UnityConfigurationManager
	{
		private static UnityConfiguration _unityConfiguration = UnityConfiguration.Default;

		public static void SetConfiguration(UnityConfiguration config)
		{
			_unityConfiguration = config;
		}

		public static UnityConfiguration GetConfiguration()
		{
			return _unityConfiguration;
		}

		public static bool IsConfigurationDefault()
		{
			return _unityConfiguration == UnityConfiguration.Default;
		}
	}
}
