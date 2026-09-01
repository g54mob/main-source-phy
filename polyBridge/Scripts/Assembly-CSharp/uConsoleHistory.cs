using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class uConsoleHistory
{
	private static List<string> m_History = new List<string>();

	private static int m_MaxNumberOfLines;

	private static string m_Filename = "ConsoleHistory";

	public static void Clear()
	{
		m_History.Clear();
	}

	public static void SetMaxNumberOfLines(int count)
	{
		m_MaxNumberOfLines = count;
		while (m_History.Count > m_MaxNumberOfLines)
		{
			m_History.RemoveAt(m_History.Count - 1);
		}
	}

	public static string GetLine(int index)
	{
		if (index < m_History.Count)
		{
			return m_History[index];
		}
		return "";
	}

	public static int GetNumLines()
	{
		return m_History.Count;
	}

	public static void Add(string text)
	{
		if (m_History.Count <= 0 || !(m_History[0] == text))
		{
			m_History.Insert(0, text);
			if (m_History.Count > m_MaxNumberOfLines)
			{
				m_History.RemoveAt(m_History.Count - 1);
			}
			Save();
		}
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
			for (int i = 0; i < m_History.Count; i++)
			{
				streamWriter.WriteLine(m_History[i]);
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
		for (int i = 0; i < m_MaxNumberOfLines; i++)
		{
			text = streamReader.ReadLine();
			if (text == null)
			{
				break;
			}
			m_History.Add(text);
		}
		streamReader.Close();
	}
}
