using System;
using plog.Models;

namespace plog.Handlers
{
	public class SystemConsoleLogHandler : ILogHandler
	{
		public Log HandleRecord(Logger source, Log log)
		{
			if (source.Tag != null)
			{
				Console.ForegroundColor = source.Tag.Color.ToConsoleColor();
				Console.Write($"[{source.Tag}] ");
				Console.ResetColor();
			}
			Console.WriteLine(log.Message);
			return log;
		}
	}
}
