using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class uConsoleLog
{
	private static List<string> m_Log = new List<string>();

	private static string m_Filename = "ConsoleLog";

	private static int m_MaxNumberOfLines;

	public static void Clear()
	{
		m_Log.Clear();
	}

	public static void Add(string text)
	{
		m_Log.Add(text);
		if (m_Log.Count > m_MaxNumberOfLines)
		{
			m_Log.RemoveAt(0);
		}
		if ((bool)uConsole.m_GUI)
		{
			uConsole.m_GUI.RefreshLogText();
		}
	}

	public static void SetMaxNumberOfLines(int count)
	{
		m_MaxNumberOfLines = count;
		while (m_Log.Count > m_MaxNumberOfLines)
		{
			m_Log.RemoveAt(0);
		}
	}

	public static int GetNumLines()
	{
		return m_Log.Count;
	}

	public static string GetLine(int index)
	{
		if (index < m_Log.Count)
		{
			return m_Log[index];
		}
		return "";
	}

	public static void Save()
	{
		string persistentDataPath = Application.persistentDataPath;
		if (!Directory.Exists(persistentDataPath))
		{
			Directory.CreateDirectory(persistentDataPath);
		}
		StreamWriter streamWriter = File.CreateText(persistentDataPath + "/" + m_Filename);
		if (streamWriter != null)
		{
			for (int i = 0; i < m_Log.Count; i++)
			{
				streamWriter.WriteLine(m_Log[i]);
			}
			streamWriter.Close();
		}
	}

	public static void Restore()
	{
		string persistentDataPath = Application.persistentDataPath;
		if (!Directory.Exists(persistentDataPath))
		{
			return;
		}
		string path = persistentDataPath + "/" + m_Filename;
		if (!File.Exists(path))
		{
			return;
		}
		StreamReader streamReader = File.OpenText(path);
		if (streamReader == null)
		{
			return;
		}
		string text = null;
		while (true)
		{
			text = streamReader.ReadLine();
			if (text == null)
			{
				break;
			}
			m_Log.Add(text);
		}
		streamReader.Close();
	}

	public static void HandleLogMessagesFromUnity(string logString, string stackTrace, LogType type)
	{
		if (logString.Length > 128)
		{
			Add(logString.Substring(0, 128));
		}
		else
		{
			Add(logString);
		}
	}
}
