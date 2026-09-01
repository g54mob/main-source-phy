using System;
using System.IO;
using UnityEngine;

public class Version
{
	public static int m_BuildNumber;

	public static string m_DisplayName;

	private static string VERSION_FILENAME = "version.txt";

	public static void Init()
	{
		m_DisplayName = "v" + Application.version.ToString();
		m_BuildNumber = 0;
		TryLoadVersionFile();
	}

	private static void TryLoadVersionFile()
	{
		try
		{
			string[] array = File.ReadAllLines(Path.Combine(Directory.GetCurrentDirectory(), VERSION_FILENAME));
			if (array.Length != 0)
			{
				string text = string.Empty;
				string empty = string.Empty;
				if (!string.IsNullOrEmpty(array[0]))
				{
					text = array[0];
					int.TryParse(text, out m_BuildNumber);
				}
				if (string.IsNullOrEmpty(empty))
				{
					m_DisplayName = $"v{Application.version.ToString()} (Build {text})";
					return;
				}
				int length = Mathf.Min(empty.Length, 8);
				m_DisplayName = $"v{Application.version.ToString()} ({text}:{empty.Substring(0, length)})";
			}
		}
		catch (Exception ex)
		{
			Debug.LogWarningFormat("Caught Exception '{0}' trying to read version.txt", ex.Message.ToString());
		}
	}
}
