using UnityEngine;
using plog.Handlers;
using plog.Models;
using plog.unity.Extensions;
using plog.unity.Helpers;
using plog.unity.Managers;
using plog.unity.Models;

namespace plog.unity.Handlers
{
	public class UnityProxy : plog.Handlers.ILogHandler
	{
		public static readonly Logger ProxyLogger = new Logger();

		private bool _capturingStackTrace;

		private string? _lastStacktrace;

		public bool SuppressingUnityLogs { get; set; }

		[HideInCallstack]
		public Log HandleRecord(Logger source, Log log)
		{
			if (source == ProxyLogger)
			{
				return log;
			}
			UnityConfiguration configuration = UnityConfigurationManager.GetConfiguration();
			string text4;
			if (source.Tag != null)
			{
				(string, string) htmlColorPair = UnityColorHelper.GetHtmlColorPair(source.Tag.Color);
				string item = htmlColorPair.Item1;
				string item2 = htmlColorPair.Item2;
				string text = (configuration.ShowLoggerTags ? $"[{source.Tag}] " : "");
				string text2 = (configuration.ColorizeLoggerTags ? ("<color=#" + item + ">" + text + "</color>") : text);
				string text3 = (configuration.ColorizeMessages ? ("<color=#" + item2 + ">" + log.Message + "</color>") : log.Message);
				text4 = text2 + text3;
			}
			else
			{
				text4 = log.Message;
			}
			SuppressingUnityLogs = true;
			_lastStacktrace = null;
			_capturingStackTrace = log.StackTrace == null;
			LogOption logOptions = ((!_capturingStackTrace) ? LogOption.NoStacktrace : LogOption.None);
			switch (log.Level)
			{
			case Level.Info:
				Debug.LogFormat(LogType.Log, logOptions, null, "{0}", text4);
				break;
			case Level.Warning:
				Debug.LogFormat(LogType.Warning, logOptions, null, "{0}", text4);
				break;
			case Level.Error:
				Debug.LogFormat(LogType.Error, logOptions, null, "{0}", text4);
				break;
			default:
				Debug.LogFormat(LogType.Log, logOptions, null, "{0}", text4);
				break;
			case Level.Off:
				break;
			}
			SuppressingUnityLogs = false;
			if (!_capturingStackTrace)
			{
				return log;
			}
			_capturingStackTrace = false;
			if (_lastStacktrace != null)
			{
				log = log with
				{
					StackTrace = _lastStacktrace
				};
			}
			return log;
		}

		[HideInCallstack]
		public void LogMessageReceived(string message, string stacktrace, LogType type)
		{
			if (!Debug.unityLogger.IsLogTypeAllowed(type))
			{
				return;
			}
			if (SuppressingUnityLogs)
			{
				if (_capturingStackTrace)
				{
					_lastStacktrace = stacktrace;
				}
			}
			else
			{
				ProxyLogger.Record(message, type.ToPLogLevel(), null, stacktrace);
			}
		}
	}
}
