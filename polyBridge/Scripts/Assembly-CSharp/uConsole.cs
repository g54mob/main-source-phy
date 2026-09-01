using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class uConsole : MonoBehaviour
{
	public delegate void DebugCommand();

	public delegate object DebugCommandReturn();

	public struct CommandDelegate
	{
		private DebugCommand m_Command;

		private DebugCommandReturn m_CommandReturn;

		public CommandDelegate(DebugCommand cmd)
		{
			m_Command = cmd;
			m_CommandReturn = null;
		}

		public CommandDelegate(DebugCommandReturn cmd)
		{
			m_CommandReturn = cmd;
			m_Command = null;
		}

		public bool IsValid()
		{
			if (m_Command == null)
			{
				return m_CommandReturn != null;
			}
			return true;
		}

		public object Call()
		{
			if (m_CommandReturn != null)
			{
				return m_CommandReturn();
			}
			m_Command();
			return null;
		}
	}

	[Header("Appearance")]
	[Range(0f, 1f)]
	public float m_ConsoleHeightNormalized = 0.33f;

	public TMP_FontAsset m_LogFont;

	public int m_LogFontSize;

	public Color m_LogBackGroundColor = new Color(1f, 1f, 1f, 0.4f);

	public Color m_LogFontColor = new Color(0f, 0f, 0f, 1f);

	[Header("Bindings")]
	public KeyCode m_Activate = KeyCode.BackQuote;

	public KeyCode m_Submit = KeyCode.Return;

	public KeyCode m_SubmitAlt = KeyCode.KeypadEnter;

	public KeyCode m_AutoComplete = KeyCode.Tab;

	public KeyCode m_HistoryUp = KeyCode.UpArrow;

	public KeyCode m_HistoryDown = KeyCode.DownArrow;

	public KeyCode m_ScrollLogUp = KeyCode.PageUp;

	public KeyCode m_ScrollLogDown = KeyCode.PageDown;

	[Header("Settings")]
	public float m_SecondsToAnimateDown = 0.2f;

	public float m_SecondsToAnimateUp = 0.1f;

	public int m_MaxHistoryLines = 128;

	public int m_MaxLogLines = 2048;

	public bool m_DoNotDestroy = true;

	public bool m_LogUnityMessages = true;

	[Header("Input Field")]
	public int m_InputFieldHeight;

	public TMP_FontAsset m_InputFieldFont;

	public int m_InputFieldFontSize;

	public Color m_InputFieldBackGroundColor = new Color(1f, 1f, 1f, 1f);

	public Color m_InputFieldFontColor = new Color(0f, 0f, 0f, 1f);

	public static uConsole m_Instance;

	public static uConsoleGUI m_GUI;

	public static Dictionary<string, CommandDelegate> m_CommandsDict = new Dictionary<string, CommandDelegate>();

	public static List<string> m_CommandsList = new List<string>();

	public static Dictionary<string, string> m_CommandsHelp = new Dictionary<string, string>();

	public static string m_Version = "1.00";

	private static List<string> m_Argv = new List<string>();

	private static int m_IndexOfNextArgToProcess;

	private static bool m_On;

	private void Awake()
	{
		if (m_DoNotDestroy)
		{
			UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
		}
		if (m_LogUnityMessages)
		{
			Application.logMessageReceived += uConsoleLog.HandleLogMessagesFromUnity;
		}
		m_Instance = this;
	}

	private void Start()
	{
		InstantiateGUIPrefab();
		uConsoleInput.Initialize();
		uConsoleCommands.RegisterBuiltInCommands();
		uConsoleHistory.SetMaxNumberOfLines(m_MaxHistoryLines);
		uConsoleHistory.Restore();
		uConsoleLog.SetMaxNumberOfLines(m_MaxLogLines);
		m_GUI.RefreshLogText();
		uConsoleCommandFile.Initialize();
	}

	private void Update()
	{
		uConsoleInput.DoFrame();
		uConsoleCommandFile.DoFrame();
	}

	public static void RegisterCommand(string command, DebugCommand commandCallback)
	{
		if (CommandAlreadyRegistered(command))
		{
			Debug.LogWarning("Command already registered with uConsole: " + command);
			return;
		}
		m_CommandsDict[command.ToLower()] = new CommandDelegate(commandCallback);
		m_CommandsList.Add(command.ToLower());
	}

	public static void RegisterCommand(string command, string help, DebugCommand commandCallback)
	{
		if (CommandAlreadyRegistered(command))
		{
			Debug.LogWarning("Command already registered with uConsole: " + command);
			return;
		}
		m_CommandsDict[command.ToLower()] = new CommandDelegate(commandCallback);
		m_CommandsList.Add(command.ToLower());
		m_CommandsHelp[command.ToLower()] = help;
	}

	public static void RegisterCommandReturn(string command, DebugCommandReturn commandCallback)
	{
		if (CommandAlreadyRegistered(command))
		{
			Debug.LogWarning("Command already registered with uConsole: " + command);
			return;
		}
		m_CommandsDict[command.ToLower()] = new CommandDelegate(commandCallback);
		m_CommandsList.Add(command.ToLower());
	}

	public static void RegisterCommandReturn(string command, string help, DebugCommandReturn commandCallback)
	{
		if (CommandAlreadyRegistered(command))
		{
			Debug.LogWarning("Command already registered with uConsole: " + command);
			return;
		}
		m_CommandsDict[command.ToLower()] = new CommandDelegate(commandCallback);
		m_CommandsHelp[command.ToLower()] = help;
		m_CommandsList.Add(command.ToLower());
	}

	public static void UnRegisterCommand(string command)
	{
		if (m_CommandsDict.ContainsKey(command.ToLower()))
		{
			m_CommandsDict.Remove(command.ToLower());
		}
		if (m_CommandsHelp.ContainsKey(command.ToLower()))
		{
			m_CommandsHelp.Remove(command.ToLower());
		}
		if (m_CommandsList.Contains(command.ToLower()))
		{
			m_CommandsList.Remove(command.ToLower());
		}
	}

	public static object RunCommand(string commandWithArgs)
	{
		commandWithArgs = commandWithArgs.Trim();
		if (string.IsNullOrEmpty(commandWithArgs))
		{
			return null;
		}
		uConsoleHistory.Add(commandWithArgs);
		BuildArgListFromTokens(commandWithArgs.Split(uConsoleInput.m_DelimterChars, StringSplitOptions.RemoveEmptyEntries));
		string key = m_Argv[0].ToLower();
		if (!m_CommandsDict.ContainsKey(key))
		{
			uConsoleLog.Add("Unregistered Command: " + m_Argv[0]);
			return null;
		}
		uConsoleLog.Add(commandWithArgs);
		m_IndexOfNextArgToProcess = 1;
		if (m_CommandsDict[key].IsValid())
		{
			object obj = m_CommandsDict[key].Call();
			if (obj != null)
			{
				uConsoleLog.Add("Return: " + obj.GetType()?.ToString() + " = " + obj.ToString());
			}
			return obj;
		}
		return null;
	}

	public static object RunCommandSilent(string commandWithArgs)
	{
		commandWithArgs = commandWithArgs.Trim();
		if (string.IsNullOrEmpty(commandWithArgs))
		{
			return null;
		}
		BuildArgListFromTokens(commandWithArgs.Split(uConsoleInput.m_DelimterChars, StringSplitOptions.RemoveEmptyEntries));
		string key = m_Argv[0].ToLower();
		if (!m_CommandsDict.ContainsKey(key))
		{
			Debug.LogError("Error Console Command failed to run. Command:" + commandWithArgs);
			return null;
		}
		m_IndexOfNextArgToProcess = 1;
		if (m_CommandsDict[key].IsValid())
		{
			return m_CommandsDict[key].Call();
		}
		return null;
	}

	public static void Log(string text)
	{
		uConsoleLog.Add(text);
	}

	public static void TurnOn()
	{
		m_On = true;
	}

	public static void TurnOff()
	{
		m_On = false;
	}

	public static bool IsOn()
	{
		return m_On;
	}

	public static bool CommandIsUnabmiguousAutoComplete(string command)
	{
		string[] array = command.Split(uConsoleInput.m_DelimterChars, StringSplitOptions.RemoveEmptyEntries);
		return array.Length switch
		{
			1 => CommandIsRegistered(command), 
			2 => CommandAndParameterRegistered(array[0], array[1]), 
			_ => CommandIsRegistered(array[0]), 
		};
	}

	public static void ShowHelp(string command)
	{
		if (!m_CommandsDict.ContainsKey(command))
		{
			uConsoleLog.Add(command + " not registered");
		}
		else if (m_CommandsHelp.ContainsKey(command))
		{
			string text = m_CommandsHelp[command];
			if (string.IsNullOrEmpty(text))
			{
				uConsoleLog.Add(command);
			}
			else
			{
				uConsoleLog.Add(command + ": " + text);
			}
		}
		else
		{
			uConsoleLog.Add(command);
		}
	}

	public static int GetNumParameters()
	{
		return m_Argv.Count - 1;
	}

	public static string GetLastCommand()
	{
		if (m_Argv.Count > 0)
		{
			return m_Argv[0].ToLower();
		}
		return "";
	}

	public static List<string> GetAllParameters()
	{
		List<string> list = new List<string>();
		for (int i = 1; i < m_Argv.Count; i++)
		{
			list.Add(m_Argv[i]);
		}
		return list;
	}

	public static bool NextParameterIsBool()
	{
		if (m_IndexOfNextArgToProcess >= m_Argv.Count)
		{
			return false;
		}
		if (m_Argv[m_IndexOfNextArgToProcess].ToLower() == "true")
		{
			return true;
		}
		if (m_Argv[m_IndexOfNextArgToProcess].ToLower() == "false")
		{
			return true;
		}
		return false;
	}

	public static bool GetBool()
	{
		if (m_IndexOfNextArgToProcess >= m_Argv.Count)
		{
			return false;
		}
		m_IndexOfNextArgToProcess++;
		if (m_Argv[m_IndexOfNextArgToProcess - 1].ToLower() == "true")
		{
			return true;
		}
		_ = m_Argv[m_IndexOfNextArgToProcess - 1].ToLower() == "false";
		return false;
	}

	public static bool NextParameterIsInt()
	{
		if (m_IndexOfNextArgToProcess >= m_Argv.Count)
		{
			return false;
		}
		int result = 0;
		if (int.TryParse(m_Argv[m_IndexOfNextArgToProcess], out result))
		{
			return true;
		}
		return false;
	}

	public static int GetInt()
	{
		if (m_IndexOfNextArgToProcess >= m_Argv.Count)
		{
			return -1;
		}
		m_IndexOfNextArgToProcess++;
		int result = 0;
		if (int.TryParse(m_Argv[m_IndexOfNextArgToProcess - 1], out result))
		{
			return result;
		}
		return -1;
	}

	public static bool NextParameterIsFloat()
	{
		if (m_IndexOfNextArgToProcess >= m_Argv.Count)
		{
			return false;
		}
		float result = 0f;
		if (float.TryParse(m_Argv[m_IndexOfNextArgToProcess], out result))
		{
			return true;
		}
		return false;
	}

	public static float GetFloat()
	{
		if (m_IndexOfNextArgToProcess >= m_Argv.Count)
		{
			return -1f;
		}
		m_IndexOfNextArgToProcess++;
		float result = 0f;
		if (float.TryParse(m_Argv[m_IndexOfNextArgToProcess - 1], out result))
		{
			return result;
		}
		return -1f;
	}

	public bool NextParameterExists()
	{
		if (m_IndexOfNextArgToProcess >= m_Argv.Count)
		{
			return false;
		}
		return true;
	}

	public static string GetString()
	{
		if (m_IndexOfNextArgToProcess >= m_Argv.Count)
		{
			return null;
		}
		m_IndexOfNextArgToProcess++;
		return m_Argv[m_IndexOfNextArgToProcess - 1];
	}

	private void OnApplicationQuit()
	{
		uConsoleHistory.Save();
		uConsoleLog.Save();
	}

	private static void BuildArgListFromTokens(string[] tokens)
	{
		m_Argv.Clear();
		bool flag = false;
		string text = "";
		for (int i = 0; i < tokens.Length; i++)
		{
			if (flag)
			{
				if (tokens[i][tokens[i].Length - 1] == '"')
				{
					text = text + " " + tokens[i].Substring(0, tokens[i].Length - 1);
					m_Argv.Add(text);
					flag = false;
				}
				else
				{
					text = text + " " + tokens[i];
				}
			}
			else if (tokens[i].Length > 0 && tokens[i][0] == '"')
			{
				flag = true;
				text = tokens[i].Substring(1);
			}
			else
			{
				m_Argv.Add(tokens[i]);
			}
		}
		if (flag)
		{
			if (text[text.Length - 1] == '"')
			{
				m_Argv.Add(text.Substring(0, text.Length - 1));
			}
			else
			{
				m_Argv.Add(text);
			}
		}
	}

	private static bool CommandAlreadyRegistered(string command)
	{
		if (m_CommandsDict.ContainsKey(command.ToLower()) && m_CommandsDict[command.ToLower()].IsValid())
		{
			return true;
		}
		return false;
	}

	private void InstantiateGUIPrefab()
	{
		UnityEngine.Object obj = Resources.Load("uConsoleGUI");
		if (!obj)
		{
			Debug.LogWarning("Unable to load prefab: uConsoleGUI_Unity");
			return;
		}
		GameObject gameObject = UnityEngine.Object.Instantiate(obj) as GameObject;
		if (!gameObject)
		{
			Debug.LogWarning("Unable to instantiate prefab: uConsoleGUI_Unity");
			return;
		}
		gameObject.name = obj.name;
		gameObject.transform.SetParent(base.transform, worldPositionStays: false);
		m_GUI = gameObject.GetComponent<uConsoleGUI>();
	}

	private static bool CommandAndParameterRegistered(string command, string parameter)
	{
		if (!CommandIsRegistered(command))
		{
			return false;
		}
		if (!ParameterIsRegistered(command, parameter))
		{
			return false;
		}
		return true;
	}

	private static bool CommandIsRegistered(string command)
	{
		if (!m_CommandsDict.ContainsKey(command))
		{
			return false;
		}
		foreach (string key in m_CommandsDict.Keys)
		{
			if (key != command && key.IndexOf(command) == 0)
			{
				return false;
			}
		}
		return true;
	}

	private static bool ParameterIsRegistered(string command, string parameter)
	{
		for (int i = 0; i < uConsoleAutoComplete.m_CommandParameterSets.Count; i++)
		{
			uConsoleCommandParameterSet uConsoleCommandParameterSet2 = uConsoleAutoComplete.m_CommandParameterSets[i];
			for (int j = 0; j < uConsoleCommandParameterSet2.m_Commands.Count; j++)
			{
				if (!(command == uConsoleCommandParameterSet2.m_Commands[j]))
				{
					continue;
				}
				if (!uConsoleCommandParameterSet2.m_AllowedParameters.Contains(parameter))
				{
					return false;
				}
				for (int k = 0; k < uConsoleCommandParameterSet2.m_AllowedParameters.Count; k++)
				{
					if (uConsoleCommandParameterSet2.m_AllowedParameters[k] != parameter && uConsoleCommandParameterSet2.m_AllowedParameters[k].IndexOf(parameter) == 0)
					{
						return false;
					}
				}
				return true;
			}
		}
		return false;
	}
}
