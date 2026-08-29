namespace SingularityGroup.HotReload
{
	public static class HotReloadLogging
	{
		public static void SetLogLevel(LogLevel level)
		{
			Log.minLevel = level;
		}
	}
}
