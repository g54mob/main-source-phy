namespace plog.Models
{
	public static class LogLevelProperties
	{
		public static readonly Level[] All = new Level[8]
		{
			Level.Debug,
			Level.Info,
			Level.Fine,
			Level.Warning,
			Level.Error,
			Level.Exception,
			Level.CommandLine,
			Level.Config
		};
	}
}
