using System;

namespace BitCode.Logging
{
	public interface ILogger
	{
		LogSeverity Verbosity { get; set; }

		void RegisterLogWriter(ILogWriter writer);

		void DeregisterLogWriter(ILogWriter writer);

		void Log(LogSeverity severity, string message);

		void LogException(Exception ex);
	}
}
