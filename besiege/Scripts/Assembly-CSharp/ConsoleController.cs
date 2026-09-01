using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ConsoleController
{
	public delegate void LogChangedHandler(string logEntry);

	public delegate void RequestClearLogHandler();

	public delegate void VisibilityChangedHandler(bool visible);

	private const int scrollbackSize = 200;

	private const string repeatCmdName = "!!";

	private List<string> commandHistory = new List<string>();

	protected Dictionary<string, CommandRegistration> commands = new Dictionary<string, CommandRegistration>();

	protected Dictionary<string, VariableRegistration> variables = new Dictionary<string, VariableRegistration>();

	protected Dictionary<string, RemoteCommandRegistration> rconCommands = new Dictionary<string, RemoteCommandRegistration>();

	private int historyIndex = -1;

	private static ConsoleController instance;

	public string[] Log { get; private set; }

	public event LogChangedHandler LogChanged;

	public event RequestClearLogHandler RequestClearLog;

	public event VisibilityChangedHandler VisibilityChanged;

	public ConsoleController()
	{
		instance = this;
	}

	protected virtual void RegisterCommands()
	{
		RegisterCommand("babble", CmdBabble, "Example command that demonstrates how to parse arguments. babble [word] [# of times to repeat]");
		RegisterCommand("echo", CmdEcho, "echoes arguments back as array (for testing argument parser)");
		RegisterCommand("help", CmdHelp, "Print this help.");
		RegisterCommand("hide", CmdHide, "Hide the console.");
		RegisterCommand("!!", CmdRepeatCommand, "Repeat last command.");
		RegisterCommand("reload", CmdReload, "Reload game.");
		RegisterCommand("resetprefs", CmdResetPrefs, "Reset & saves PlayerPrefs.");
	}

	protected void ClearConsole()
	{
		if (this.RequestClearLog != null)
		{
			this.RequestClearLog();
		}
	}

	public void Initialize()
	{
		RegisterCommands();
	}

	public static void ShowMessage(string message)
	{
		if (instance != null)
		{
			instance.AppendLogLine(message);
		}
	}

	public static void ShowServerMessage(string message)
	{
		if (instance != null)
		{
			instance.AppendServerMessage(message);
		}
	}

	public void AppendServerMessage(string message)
	{
		AppendLogLine("Server: " + message);
	}

	public void HandleRconCommand(ushort playerId, string password, string command, string[] args)
	{
		if (string.IsNullOrEmpty(OptionsMaster.BesiegeConfig.RconPassword))
		{
			SendConsoleMessage(playerId, "Server rconpassword is not set");
		}
		else if (!OptionsMaster.BesiegeConfig.RconPassword.Equals(password))
		{
			SendConsoleMessage(playerId, "Invalid rconpassword");
		}
		else
		{
			RunRconCommand(playerId, command, args);
		}
	}

	public string GetPreviousCommand(bool upInHistory)
	{
		if (commandHistory.Count == 0)
		{
			return string.Empty;
		}
		if (historyIndex == -1)
		{
			historyIndex = commandHistory.Count - 1;
			return commandHistory[historyIndex];
		}
		if (historyIndex == commandHistory.Count && !upInHistory)
		{
			return string.Empty;
		}
		historyIndex += ((!upInHistory) ? 1 : (-1));
		if (historyIndex < 0)
		{
			historyIndex = 0;
		}
		else if (historyIndex > commandHistory.Count - 1)
		{
			return string.Empty;
		}
		return commandHistory[historyIndex];
	}

	public string AutoComplete(string inputCommand)
	{
		StringBuilder stringBuilder = new StringBuilder();
		List<string> list = commands.Keys.Where((string x) => x.StartsWith(inputCommand)).Concat(variables.Keys.Where((string x) => x.StartsWith(inputCommand))).ToList();
		foreach (string item in list)
		{
			stringBuilder.AppendLine(item);
		}
		if (list.Count == 1)
		{
			return list[0];
		}
		if (stringBuilder.Length > 0)
		{
			AppendLogLine(stringBuilder.ToString().TrimEnd());
		}
		return inputCommand;
	}

	public void RunCommandString(string commandString)
	{
		AppendLogLine("<b>$ " + commandString + "</b>");
		string[] array = ParseArguments(commandString);
		string[] array2 = new string[0];
		if (array.Length < 1)
		{
			AppendLogLine(string.Format("Unable to process command '{0}'", commandString));
			return;
		}
		if (array.Length >= 2)
		{
			int num = array.Length - 1;
			array2 = new string[num];
			Array.Copy(array, 1, array2, 0, num);
		}
		string text = array[0].ToLower();
		if (!RunVariable(text, array2))
		{
			RunCommand(text, array2);
		}
		commandHistory.Add(commandString);
		historyIndex = -1;
	}

	public void AppendLogLine(string line)
	{
		if (this.LogChanged != null)
		{
			this.LogChanged(line);
		}
	}

	protected void RunRconCommand(ushort playerId, string command, string[] args)
	{
		RemoteCommandRegistration value = null;
		if (!rconCommands.TryGetValue(command, out value))
		{
			SendConsoleMessage(playerId, string.Format("Unknown rcon command '{0}', type 'rcon help' for list.", command));
			return;
		}
		if (value.Handler == null)
		{
			SendConsoleMessage(playerId, string.Format("Unable to process command '{0}', handler was null.", command));
			return;
		}
		try
		{
			value.Handler(playerId, command, args);
		}
		catch (Exception ex)
		{
			Debug.LogError("Exception caught while processing command '" + command + "': " + ex.ToString());
			SendConsoleMessage(playerId, "<color=\"red\">Error while processing rcon command.</color>");
		}
	}

	protected void RunCommand(string command, string[] args)
	{
		CommandRegistration value = null;
		if (!commands.TryGetValue(command, out value))
		{
			HandleUnknownCommand(command, args);
		}
		else if (value.Handler == null)
		{
			AppendLogLine(string.Format("Unable to process command '{0}', handler was null.", command));
		}
		else
		{
			value.Handler(args);
		}
	}

	protected virtual void HandleUnknownCommand(string command, string[] args)
	{
		AppendLogLine(string.Format("Unknown command '{0}', type 'help' for list.", command));
	}

	protected bool RunVariable(string variable, string[] args)
	{
		VariableRegistration value = null;
		if (!variables.TryGetValue(variable, out value))
		{
			return false;
		}
		if (args.Length == 0)
		{
			AppendLogLine(string.Format("\"{0}\" is: \"{1}\"", variable, value.Variable.Value));
		}
		else
		{
			object obj = args[0];
			value.Variable.Value = obj;
			AppendLogLine(string.Format("\"{0}\" is now: \"{1}\"", variable, obj));
		}
		return true;
	}

	private static string[] ParseArguments(string commandString)
	{
		LinkedList<char> linkedList = new LinkedList<char>(commandString.ToCharArray());
		bool flag = false;
		LinkedListNode<char> linkedListNode = linkedList.First;
		while (linkedListNode != null)
		{
			LinkedListNode<char> next = linkedListNode.Next;
			if (linkedListNode.Value == '"')
			{
				flag = !flag;
				linkedList.Remove(linkedListNode);
			}
			if (!flag && linkedListNode.Value == ' ')
			{
				linkedListNode.Value = '\n';
			}
			linkedListNode = next;
		}
		char[] array = new char[linkedList.Count];
		linkedList.CopyTo(array, 0);
		return new string(array).Split(new char[1] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
	}

	private PropertyInfo GetPropertyInfo<TSource, TProperty>(TSource source, Expression<Func<TSource, TProperty>> propertyLambda)
	{
		Type typeFromHandle = typeof(TSource);
		MemberExpression memberExpression = propertyLambda.Body as MemberExpression;
		if (memberExpression == null)
		{
			throw new ArgumentException(string.Format("Expression '{0}' refers to a method, not a property.", propertyLambda.ToString()));
		}
		PropertyInfo propertyInfo = memberExpression.Member as PropertyInfo;
		if (propertyInfo == null)
		{
			throw new ArgumentException(string.Format("Expression '{0}' refers to a field, not a property.", propertyLambda.ToString()));
		}
		if (typeFromHandle != propertyInfo.ReflectedType && !typeFromHandle.IsSubclassOf(propertyInfo.ReflectedType))
		{
			throw new ArgumentException(string.Format("Expresion '{0}' refers to a property that is not from type {1}.", propertyLambda.ToString(), typeFromHandle));
		}
		return propertyInfo;
	}

	protected void RegisterVariable<TProperty>(string variable, Expression<Func<Type, TProperty>> propertyLambda) where TProperty : IConvertible
	{
		RegisterVariable(variable, propertyLambda, null);
	}

	protected void RegisterVariable<TProperty>(string variable, Expression<Func<Type, TProperty>> propertyLambda, string help) where TProperty : IConvertible
	{
		MemberExpression memberExpression = propertyLambda.Body as MemberExpression;
		if (memberExpression == null)
		{
			throw new ArgumentException(string.Format("Expression '{0}' refers to a method, not a property.", propertyLambda.ToString()));
		}
		PropertyInfo propInfo = memberExpression.Member as PropertyInfo;
		FieldInfo fieldInfo = memberExpression.Member as FieldInfo;
		if (propInfo != null)
		{
			RegisterVariable(variable, () => (TProperty)propInfo.GetValue(null, null), delegate(TProperty x)
			{
				propInfo.SetValue(null, x, null);
			});
		}
		else if (fieldInfo != null)
		{
			RegisterVariable(variable, () => (TProperty)fieldInfo.GetValue(null), delegate(TProperty x)
			{
				fieldInfo.SetValue(null, x);
			});
		}
		else
		{
			Debug.LogError("Could not register variable, is not a field or property");
		}
	}

	protected void RegisterVariable<TSource, TProperty>(string variable, TSource source, Expression<Func<TSource, TProperty>> propertyLambda, string help) where TProperty : IConvertible
	{
		Type typeFromHandle = typeof(TSource);
		MemberExpression memberExpression = propertyLambda.Body as MemberExpression;
		if (memberExpression == null)
		{
			throw new ArgumentException(string.Format("Expression '{0}' refers to a method, not a property.", propertyLambda.ToString()));
		}
		PropertyInfo propInfo = memberExpression.Member as PropertyInfo;
		FieldInfo fieldInfo = memberExpression.Member as FieldInfo;
		if (propInfo != null)
		{
			if (typeFromHandle != propInfo.ReflectedType && !typeFromHandle.IsSubclassOf(propInfo.ReflectedType))
			{
				throw new ArgumentException(string.Format("Expresion '{0}' refers to a property that is not from type {1}.", propertyLambda.ToString(), typeFromHandle));
			}
			RegisterVariable(variable, () => (TProperty)propInfo.GetValue(source, null), delegate(TProperty x)
			{
				propInfo.SetValue(source, x, null);
			}, help);
		}
		else if (fieldInfo != null)
		{
			if (typeFromHandle != fieldInfo.ReflectedType && !typeFromHandle.IsSubclassOf(fieldInfo.ReflectedType))
			{
				throw new ArgumentException(string.Format("Expresion '{0}' refers to a property that is not from type {1}.", propertyLambda.ToString(), typeFromHandle));
			}
			RegisterVariable(variable, () => (TProperty)fieldInfo.GetValue(source), delegate(TProperty x)
			{
				fieldInfo.SetValue(source, x);
			}, help);
		}
		else
		{
			Debug.LogError("Could not register variable, is not a field or property");
		}
	}

	protected void RegisterVariable<T>(string variable, Func<T> get, Action<T> set) where T : IConvertible
	{
		RegisterVariable(variable, get, set, null);
	}

	protected void RegisterVariable<T>(string variable, Func<T> get, Action<T> set, string help) where T : IConvertible
	{
		if (string.IsNullOrEmpty(help))
		{
			help = string.Format("{0} variable. Usage: {0} [value]", variable);
		}
		MutableWrapper wrapper = new MutableWrapper<T>(get, set);
		variables.Add(variable, new VariableRegistration(variable, wrapper, help));
	}

	internal void RegisterCommand(string command, CommandHandler handler, string help)
	{
		commands.Add(command, new CommandRegistration(command, handler, help));
	}

	protected void RegisterRconCommand(string command, RconCommandHandler handler, string help)
	{
		rconCommands.Add(command, new RemoteCommandRegistration(command, handler, help));
	}

	internal void UpdateCommand(string command, CommandHandler handler, string help)
	{
		commands[command] = new CommandRegistration(command, handler, help);
	}

	public bool HasCommand(string command)
	{
		return commands.ContainsKey(command);
	}

	protected void HideConsole()
	{
		if (this.VisibilityChanged != null)
		{
			this.VisibilityChanged(false);
		}
	}

	private void CmdBabble(string[] args)
	{
		if (args.Length < 2)
		{
			AppendLogLine("Expected 2 arguments.");
			return;
		}
		string text = args[0];
		if (string.IsNullOrEmpty(text))
		{
			AppendLogLine("Expected arg1 to be text.");
			return;
		}
		int result = 0;
		if (!int.TryParse(args[1], out result))
		{
			AppendLogLine("Expected an integer for arg2.");
			return;
		}
		for (int i = 0; i < result; i++)
		{
			AppendLogLine(string.Format("{0} {1}", text, i));
		}
	}

	private void CmdEcho(string[] args)
	{
		StringBuilder stringBuilder = new StringBuilder();
		foreach (string arg in args)
		{
			stringBuilder.AppendFormat("{0},", arg);
		}
		stringBuilder.Remove(stringBuilder.Length - 1, 1);
		AppendLogLine(stringBuilder.ToString());
	}

	private void CmdHelp(string[] args)
	{
		StringBuilder stringBuilder = new StringBuilder();
		IEnumerable<AbstractRegistration> enumerable = from x in commands.Values.Cast<AbstractRegistration>().Concat(variables.Values.Cast<AbstractRegistration>())
			orderby x.Name.Split(':').Last()
			select x;
		foreach (AbstractRegistration item in enumerable)
		{
			if (!string.IsNullOrEmpty(item.Help))
			{
				stringBuilder.AppendLine(string.Format("- {0}: {1}", item.Name, item.Help));
			}
		}
		AppendLogLine(stringBuilder.ToString());
	}

	private void CmdHide(string[] args)
	{
		HideConsole();
	}

	private void CmdRepeatCommand(string[] args)
	{
		for (int num = commandHistory.Count - 1; num >= 0; num--)
		{
			string text = commandHistory[num];
			if (!string.Equals("!!", text))
			{
				RunCommandString(text);
				break;
			}
		}
	}

	private void CmdReload(string[] args)
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	private void CmdResetPrefs(string[] args)
	{
		PlayerPrefs.DeleteAll();
		PlayerPrefs.Save();
	}

	protected void SendRconHelp(ushort playerId, string command)
	{
		StringBuilder stringBuilder = new StringBuilder("Invalid arguments provided.").AppendLine();
		stringBuilder.AppendLine(string.Format("- {0}: {1}", rconCommands[command].Name, rconCommands[command].Help));
		SendConsoleMessage(playerId, stringBuilder.ToString());
	}

	protected void SendConsoleMessage(ushort playerId, string message)
	{
		NetworkAuxAddPiece.Instance.SendConsolePrint(playerId, message);
	}

	protected void SendConsoleMessage(string message)
	{
		NetworkAuxAddPiece.Instance.SendConsolePrint(message);
	}
}
