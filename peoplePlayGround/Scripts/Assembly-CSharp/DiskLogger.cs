using System;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DiskLogger : MonoBehaviour
{
	private StreamWriter writer;

	private const string dir = "Logs\\";

	private const int MaxLogFileCount = 25;

	private void Awake()
	{
		if (!Directory.Exists("Logs\\"))
		{
			Directory.CreateDirectory("Logs\\");
		}
		else
		{
			string[] files = Directory.GetFiles("Logs\\", "*.log");
			if (files.Length > 25)
			{
				int num = 0;
				foreach (string item in files.OrderBy((string f) => File.GetCreationTime(f)))
				{
					try
					{
						File.Delete(item);
						num++;
						if (files.Length - num <= 25)
						{
							break;
						}
					}
					catch (Exception message)
					{
						Debug.LogError(message);
					}
				}
				Debug.Log($"{num} old log files deleted");
			}
		}
		writer = new StreamWriter(GetPath());
		Debug.Log("Timestamp: " + DateTime.UtcNow.ToString());
		Debug.Log("Current version: 1.27.11");
		Application.logMessageReceived += LogMessageReceived;
	}

	private void LogMessageReceived(string condition, string stackTrace, LogType type)
	{
		writer.Write(' ');
		writer.Write(GetPrefix(type));
		writer.Write(' ');
		writer.Write(condition);
		if (type != LogType.Log && !string.IsNullOrWhiteSpace(stackTrace))
		{
			writer.WriteLine();
			writer.Write('\t');
			writer.WriteLine(stackTrace);
		}
		writer.WriteLine();
	}

	private static string GetPrefix(LogType type)
	{
		return type switch
		{
			LogType.Error => "[ERROR]", 
			LogType.Assert => "[ASSERT]", 
			LogType.Warning => "[WARNING]", 
			LogType.Log => "[LOG]", 
			LogType.Exception => "[EXCEPTION]", 
			_ => "[UNKNOWN]", 
		};
	}

	private void OnDestroy()
	{
		writer.Dispose();
		Application.logMessageReceived -= LogMessageReceived;
	}

	private string GetPath()
	{
		return "Logs\\" + DateTimeOffset.Now.ToUnixTimeSeconds() + "-" + SceneManager.GetActiveScene().name + ".log";
	}
}
