using System;

namespace BitCode.Logging
{
	public interface ILogWriter
	{
		void Write(LogSeverity severity, string message);

		void WriteException(Exception ex);
	}
}
