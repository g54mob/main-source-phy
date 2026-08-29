using System;
using JetBrains.Annotations;
using UnityEngine;

namespace SingularityGroup.HotReload
{
	public static class Log
	{
		public static LogLevel minLevel = LogLevel.Info;

		private const string TAG = "[HotReload] ";

		public static void Debug(string message)
		{
			if (minLevel <= LogLevel.Debug)
			{
				UnityEngine.Debug.LogFormat(LogType.Log, LogOption.NoStacktrace, null, "{0}{1}", "[HotReload] ", message);
			}
		}

		[StringFormatMethod("message")]
		public static void Debug(string message, params object[] args)
		{
			if (minLevel <= LogLevel.Debug)
			{
				UnityEngine.Debug.LogFormat(LogType.Log, LogOption.NoStacktrace, null, "[HotReload] " + message, args);
			}
		}

		public static void Info(string message)
		{
			if (minLevel <= LogLevel.Info)
			{
				UnityEngine.Debug.LogFormat(LogType.Log, LogOption.NoStacktrace, null, "{0}{1}", "[HotReload] ", message);
			}
		}

		[StringFormatMethod("message")]
		public static void Info(string message, params object[] args)
		{
			if (minLevel <= LogLevel.Info)
			{
				UnityEngine.Debug.LogFormat(LogType.Log, LogOption.NoStacktrace, null, "[HotReload] " + message, args);
			}
		}

		public static void Warning(string message)
		{
			if (minLevel <= LogLevel.Warning)
			{
				UnityEngine.Debug.LogFormat(LogType.Warning, LogOption.NoStacktrace, null, "{0}{1}", "[HotReload] ", message);
			}
		}

		[StringFormatMethod("message")]
		public static void Warning(string message, params object[] args)
		{
			if (minLevel <= LogLevel.Warning)
			{
				UnityEngine.Debug.LogFormat(LogType.Warning, LogOption.NoStacktrace, null, "[HotReload] " + message, args);
			}
		}

		public static void Error(string message)
		{
			if (minLevel <= LogLevel.Error)
			{
				UnityEngine.Debug.LogFormat(LogType.Error, LogOption.NoStacktrace, null, "{0}{1}", "[HotReload] ", message);
			}
		}

		[StringFormatMethod("message")]
		public static void Error(string message, params object[] args)
		{
			if (minLevel <= LogLevel.Error)
			{
				UnityEngine.Debug.LogFormat(LogType.Error, LogOption.NoStacktrace, null, "[HotReload] " + message, args);
			}
		}

		public static void Exception(Exception exception)
		{
			if (minLevel <= LogLevel.Exception)
			{
				UnityEngine.Debug.LogException(exception);
			}
		}
	}
}
