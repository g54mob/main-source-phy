using System.Collections.Generic;
using System.Linq;
using System.Text;
using InternalModding;
using InternalModding.Assemblies;
using UnityEngine;
using mattmc3.dotmore.Extensions;

public class LogController : SingleInstance<LogController>
{
	private Dictionary<LogType, string> colors = new Dictionary<LogType, string>
	{
		{
			LogType.Log,
			"white"
		},
		{
			LogType.Warning,
			"yellow"
		},
		{
			LogType.Error,
			"red"
		},
		{
			LogType.Exception,
			"red"
		},
		{
			LogType.Assert,
			"red"
		}
	};

	private string prevMessage = string.Empty;

	private bool repeatedPrevMessage;

	public override string Name
	{
		get
		{
			return "LogConsoleController";
		}
	}

	public void Awake()
	{
		Application.logMessageReceived += HandleLog;
	}

	public void OnDestroy()
	{
		Application.logMessageReceived -= HandleLog;
	}

	private void HandleLog(string log, string stackTrace, LogType type)
	{
		if ((type == LogType.Exception || OptionsMaster.BesiegeConfig.ShowDebugLogs) && ShouldPrintMessage(log, stackTrace, type))
		{
			string message = FormatMessage(log, stackTrace, type);
			ReferenceMaster.ConsoleController.AppendLogLine(message);
		}
	}

	private void HandleAnalytics(string log, string stackTrace, LogType type)
	{
		if (!IsModLogMessage(log, stackTrace) && ModManager.Mods.Count != 0)
		{
			log = "[Modded] " + log;
		}
	}

	private bool ShouldPrintMessage(string log, string stackTrace, LogType type)
	{
		if (log + stackTrace == prevMessage)
		{
			if (!repeatedPrevMessage)
			{
				ReferenceMaster.ConsoleController.AppendLogLine("The above message was repeated (possibly many times).");
				repeatedPrevMessage = true;
			}
			return false;
		}
		if (type == LogType.Exception)
		{
			prevMessage = log + stackTrace;
			repeatedPrevMessage = false;
		}
		else if (log.StartsWith("[Callback Exception] "))
		{
			prevMessage = log + stackTrace;
			repeatedPrevMessage = false;
		}
		else
		{
			prevMessage = string.Empty;
			repeatedPrevMessage = false;
		}
		return true;
	}

	private string FormatMessage(string log, string stackTrace, LogType type)
	{
		StringBuilder stringBuilder = new StringBuilder();
		if (OptionsMaster.BesiegeConfig.ShowLogFrameNumber)
		{
			stringBuilder.Append("[");
			stringBuilder.Append(Time.frameCount);
			stringBuilder.Append("] ");
		}
		stringBuilder.Append("<color=\"");
		stringBuilder.Append(colors[type]);
		stringBuilder.Append("\">");
		if (log.StartsWith("[Mods] "))
		{
			log = log.Replace("[Mods] ", string.Empty);
		}
		if (type == LogType.Exception)
		{
			stringBuilder.AppendLine(log);
			stringBuilder.Append(stackTrace);
			stringBuilder.Append("</color>");
		}
		else if (log.StartsWith("[Callback Exception] "))
		{
			stringBuilder.AppendLine(string.Join("\n", (from line in log.SplitLines()
				where !line.Contains("InternalModding.ModdingUtil.PerformCallback")
				select line).ToArray()));
			stringBuilder.Append(string.Join("\n", (from line in stackTrace.SplitLines()
				where !line.Contains("InternalModding.ModdingUtil:PerformCallback") && !line.Contains("UnityEngine.Debug:LogError")
				select line).ToArray()));
			stringBuilder.Append("</color>");
		}
		else
		{
			stringBuilder.Append(log);
			stringBuilder.Append("</color>");
		}
		return stringBuilder.ToString();
	}

	private bool IsModLogMessage(string log, string trace)
	{
		if (log.StartsWith("[Callback Exception] "))
		{
			return true;
		}
		if (log.StartsWith("[Mods] "))
		{
			return true;
		}
		string[] array = trace.SplitLines();
		string[] array2 = array;
		foreach (string text in array2)
		{
			if (!string.IsNullOrEmpty(text))
			{
				string[] array3 = text.Split('.', ':');
				string text2 = array3[0];
				int num = 1;
				while (!AssemblyLoader.IsModType(text2) && num < array3.Length)
				{
					text2 = text2 + "." + array3[num];
					num++;
				}
				if (num <= array3.Length)
				{
					return true;
				}
			}
		}
		return false;
	}
}
