using System.Collections.Generic;
using UnityEngine;

public class uConsoleCommands
{
	public static void RegisterBuiltInCommands()
	{
		uConsole.RegisterCommand("clear", "clears uConsole log", ClearLog);
		uConsole.RegisterCommand("search", "usage: search <command>", SearchForCommand);
		uConsole.RegisterCommand("help", "usage: help [command]", ShowHelp);
		uConsole.RegisterCommand("quit", "immediately quit, without confirmation", Quit);
		uConsole.RegisterCommand("version", "show uConsole version", ShowVersion);
	}

	public static void ClearLog()
	{
		uConsoleLog.Clear();
		if ((bool)uConsole.m_GUI)
		{
			uConsole.m_GUI.RefreshLogText();
		}
	}

	public static void ShowVersion()
	{
		uConsoleLog.Add("uConsole Version " + uConsole.m_Version);
	}

	public static void SearchForCommand()
	{
		string value = uConsole.GetString();
		if (string.IsNullOrEmpty(value))
		{
			uConsoleLog.Add("Usage: search <name>");
			return;
		}
		foreach (string key in uConsole.m_CommandsDict.Keys)
		{
			if (key.IndexOf(value) >= 0)
			{
				uConsole.ShowHelp(key);
			}
		}
	}

	public static void ShowHelp()
	{
		string text = uConsole.GetString();
		if (!string.IsNullOrEmpty(text))
		{
			uConsole.ShowHelp(text);
			return;
		}
		List<string> list = new List<string>();
		foreach (string key in uConsole.m_CommandsDict.Keys)
		{
			list.Add(key);
		}
		list.Sort();
		foreach (string item in list)
		{
			uConsole.ShowHelp(item);
		}
	}

	public static void Quit()
	{
		Application.Quit();
	}
}
