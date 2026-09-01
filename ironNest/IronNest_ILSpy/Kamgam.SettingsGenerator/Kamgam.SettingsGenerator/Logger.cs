using System;
using Cpp2ILInjected;
using UnityEngine;

namespace Kamgam.SettingsGenerator;

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

	public static LogLevel CurrentLogLevel = LogLevel.Warning;

	public static Func<LogLevel> OnGetLogLevel = null;

	public static bool IsLogLevelVisible(LogLevel logLevel)
	{
		//IL_0018: Expected O, but got I4
		//IL_0026: Expected O, but got I4
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Expected O, but got Unknown
		object obj = logLevel - CurrentLogLevel;
		object obj2 = logLevel ^ CurrentLogLevel;
		object obj3 = logLevel ^ obj;
		object obj4 = obj2 & obj3;
		bool flag = (nint)obj4 < 0;
		bool flag2 = (nint)obj < 0;
		return flag2 == flag;
	}

	public static void UpdateCurrentLogLevel()
	{
		if (OnGetLogLevel != null)
		{
			Func<LogLevel> onGetLogLevel = OnGetLogLevel;
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v99 @ rcx_v6 (System.Func`1<Kamgam.SettingsGenerator.Logger+LogLevel>)+18] (should have been resolved before IL gen)");
			LogLevel currentLogLevel = default(LogLevel);
			CurrentLogLevel = currentLogLevel;
		}
	}

	public static void LogInEditorOnly(string message, UnityEngine.Object context = null)
	{
		UpdateCurrentLogLevel();
		if (CurrentLogLevel <= LogLevel.Log)
		{
			string message2 = "SettingsGenerator: " + message + "\nYou can change the verbosity of logs in the Settings under Tools > Settings Generator > Settings : LogLevel";
			Debug.Log(message2, context);
		}
	}

	public static void LogWarningInEditorOnly(string message, UnityEngine.Object context = null)
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Invalid instruction: 42 Invalid \"Jump target not found in method: 0x180A12A50\"");
	}

	public static void LogErrorInEditorOnly(string message, UnityEngine.Object context = null)
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Invalid instruction: 42 Invalid \"Jump target not found in method: 0x180A12590\"");
	}

	public static void LogMessageInEditorOnly(string message, UnityEngine.Object context = null)
	{
		UpdateCurrentLogLevel();
		if (CurrentLogLevel <= LogLevel.Message)
		{
			string message2 = "SettingsGenerator: " + message + "\nYou can change the verbosity of logs in the Settings under Tools > Settings Generator > Settings : LogLevel";
			Debug.Log(message2, context);
		}
	}

	public static void Log(string message, UnityEngine.Object context = null)
	{
		UpdateCurrentLogLevel();
		if (CurrentLogLevel <= LogLevel.Log)
		{
			string message2 = "SettingsGenerator: " + message + "\nYou can change the verbosity of logs in the Settings under Tools > Settings Generator > Settings : LogLevel";
			Debug.Log(message2, context);
		}
	}

	public static void LogWarning(string message, UnityEngine.Object context = null)
	{
		UpdateCurrentLogLevel();
		if (CurrentLogLevel <= LogLevel.Warning)
		{
			string message2 = "SettingsGenerator: " + message + "\nYou can change the verbosity of logs in the Settings under Tools > Settings Generator > Settings : LogLevel";
			Debug.LogWarning(message2, context);
		}
	}

	public static void LogError(string message, UnityEngine.Object context = null)
	{
		UpdateCurrentLogLevel();
		if (CurrentLogLevel <= LogLevel.Error)
		{
			string message2 = "SettingsGenerator: " + message + "\nYou can change the verbosity of logs in the Settings under Tools > Settings Generator > Settings : LogLevel";
			Debug.LogError(message2, context);
		}
	}

	public static void LogMessage(string message, UnityEngine.Object context = null)
	{
		UpdateCurrentLogLevel();
		if (CurrentLogLevel <= LogLevel.Message)
		{
			string message2 = "SettingsGenerator: " + message + "\nYou can change the verbosity of logs in the Settings under Tools > Settings Generator > Settings : LogLevel";
			Debug.Log(message2, context);
		}
	}
}
