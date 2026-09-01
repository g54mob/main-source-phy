using System;
using System.Collections.Generic;

public class uConsoleAutoComplete
{
	public static List<uConsoleCommandParameterSet> m_CommandParameterSets = new List<uConsoleCommandParameterSet>();

	public static void CreateCommandParameterSet(string command, List<string> parameters)
	{
		CreateCommandParameterSet(new List<string> { command }, parameters);
	}

	public static void CreateCommandParameterSet(List<string> commands, List<string> parameters)
	{
		uConsoleCommandParameterSet uConsoleCommandParameterSet2 = new uConsoleCommandParameterSet();
		uConsoleCommandParameterSet2.m_Commands = commands;
		uConsoleCommandParameterSet2.m_AllowedParameters = parameters;
		m_CommandParameterSets.Add(uConsoleCommandParameterSet2);
	}

	public static string GetBestCompletion(string partialCommand)
	{
		string[] array = partialCommand.Split(uConsoleInput.m_DelimterChars, StringSplitOptions.RemoveEmptyEntries);
		return array.Length switch
		{
			1 => GetBestMatchFromList(partialCommand, uConsole.m_CommandsList), 
			2 => GetBestCommandWithParameterCompletion(array[0], array[1]), 
			_ => partialCommand, 
		};
	}

	public static void DisplayPossibleMatches(string command)
	{
		string[] array = command.Split(uConsoleInput.m_DelimterChars, StringSplitOptions.RemoveEmptyEntries);
		switch (array.Length)
		{
		case 1:
			DisplayStringsStartingWithMatch(command, uConsole.m_CommandsList);
			break;
		case 2:
			DisplayParametersStartingWithMatch(array[0], array[1]);
			break;
		}
	}

	public static void DisplayStringsStartingWithMatch(string match, List<string> list)
	{
		int num = 0;
		for (int i = 0; i < list.Count; i++)
		{
			if (list[i].IndexOf(match) == 0)
			{
				num++;
			}
		}
		if (num < 2)
		{
			return;
		}
		num = 0;
		for (int j = 0; j < list.Count; j++)
		{
			if (list[j].IndexOf(match) == 0)
			{
				if (num == 0)
				{
					uConsole.Log("Possible Matches:");
				}
				uConsole.Log(list[j]);
				num++;
			}
		}
	}

	private static void DisplayParametersStartingWithMatch(string command, string parameter)
	{
		for (int i = 0; i < m_CommandParameterSets.Count; i++)
		{
			uConsoleCommandParameterSet uConsoleCommandParameterSet2 = m_CommandParameterSets[i];
			for (int j = 0; j < uConsoleCommandParameterSet2.m_Commands.Count; j++)
			{
				if (command == uConsoleCommandParameterSet2.m_Commands[j])
				{
					DisplayStringsStartingWithMatch(parameter, uConsoleCommandParameterSet2.m_AllowedParameters);
				}
			}
		}
	}

	private static bool CommonCharacterAtIndex(int index, List<string> strings)
	{
		if (index >= strings[0].Length)
		{
			return false;
		}
		char c = strings[0][index];
		for (int i = 1; i < strings.Count; i++)
		{
			if (index >= strings[i].Length)
			{
				return false;
			}
			if (strings[i][index] != c)
			{
				return false;
			}
		}
		return true;
	}

	private static string GetBestMatchFromList(string pattern, List<string> list)
	{
		List<string> list2 = new List<string>();
		for (int i = 0; i < list.Count; i++)
		{
			if (list[i].IndexOf(pattern) == 0)
			{
				list2.Add(list[i]);
			}
		}
		if (list2.Count == 0)
		{
			return pattern;
		}
		if (list2.Count == 1)
		{
			return list2[0];
		}
		int j;
		for (j = pattern.Length; CommonCharacterAtIndex(j, list2); j++)
		{
		}
		return list2[0].Substring(0, j);
	}

	private static string GetBestCommandWithParameterCompletion(string command, string partialParameter)
	{
		for (int i = 0; i < m_CommandParameterSets.Count; i++)
		{
			uConsoleCommandParameterSet uConsoleCommandParameterSet2 = m_CommandParameterSets[i];
			for (int j = 0; j < uConsoleCommandParameterSet2.m_Commands.Count; j++)
			{
				if (command == uConsoleCommandParameterSet2.m_Commands[j])
				{
					return command + " " + GetBestMatchFromList(partialParameter, uConsoleCommandParameterSet2.m_AllowedParameters);
				}
			}
		}
		return null;
	}
}
