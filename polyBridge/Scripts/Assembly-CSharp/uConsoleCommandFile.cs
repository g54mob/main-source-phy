using System.Collections.Generic;

public class uConsoleCommandFile
{
	private static bool m_ProcessedMainConfigFile;

	private static string[] m_PendingScriptCommands;

	private static float m_ApplyScriptCommandsTime;

	private static int m_ScriptCommandIndex;

	public static void Initialize()
	{
	}

	public static void DoFrame()
	{
	}

	public static void RegisterPendingCommands(string[] commandLines)
	{
		if (m_PendingScriptCommands == null)
		{
			m_PendingScriptCommands = commandLines;
			return;
		}
		List<string> list = new List<string>();
		list.AddRange(m_PendingScriptCommands);
		list.AddRange(commandLines);
		m_PendingScriptCommands = list.ToArray();
	}

	private static void ReadConfigFile(string filename)
	{
	}

	private static void ProcessScriptCommands()
	{
	}
}
