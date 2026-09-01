using System;
using System.Diagnostics;
using UnityEngine;

namespace Kamgam.SettingsGenerator
{
	public class Logger
	{
		public delegate void LogCallback(string msg, LogLevel logLevel);

		public enum LogLevel
		{
			Log = 0,
			Warning = 1,
			Error = 2,
			Message = 3,
			NoLogs = 99
		}

		public const string Prefix = "SettingsGenerator: ";

		public static LogLevel CurrentLogLevel;

		public static Func<LogLevel> OnGetLogLevel;

		public static bool IsLogLevelVisible(LogLevel logLevel)
		{
			return false;
		}

		public static void UpdateCurrentLogLevel()
		{
		}

		[Conditional("UNITY_EDITOR")]
		public static void LogInEditorOnly(string message, UnityEngine.Object context = null)
		{
		}

		[Conditional("UNITY_EDITOR")]
		public static void LogWarningInEditorOnly(string message, UnityEngine.Object context = null)
		{
		}

		[Conditional("UNITY_EDITOR")]
		public static void LogErrorInEditorOnly(string message, UnityEngine.Object context = null)
		{
		}

		[Conditional("UNITY_EDITOR")]
		public static void LogMessageInEditorOnly(string message, UnityEngine.Object context = null)
		{
		}

		public static void Log(string message, UnityEngine.Object context = null)
		{
		}

		public static void LogWarning(string message, UnityEngine.Object context = null)
		{
		}

		public static void LogError(string message, UnityEngine.Object context = null)
		{
		}

		public static void LogMessage(string message, UnityEngine.Object context = null)
		{
		}
	}
}
