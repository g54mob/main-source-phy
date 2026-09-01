using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using InternalModding.Assemblies;
using InternalModding.Mods;
using mattmc3.dotmore.Collections.Generic;

namespace Modding
{
	public static class ModConsole
	{
		private static Dictionary<ModContainer, List<CommandRegistration>> Commands = new Dictionary<ModContainer, List<CommandRegistration>>();

		private static BesiegeConsoleController consoleController;

		internal static void Initialize()
		{
			consoleController = (BesiegeConsoleController)ReferenceMaster.ConsoleController;
		}

		public static void Log(string log)
		{
			consoleController.AppendLogLine(log);
		}

		public static void Log(string format, params object[] args)
		{
			consoleController.AppendLogLine(string.Format(format, args));
		}

		public static void RegisterCommand(string command, CommandHandler handler, string help)
		{
			Assembly callingAssembly = Assembly.GetCallingAssembly();
			ModContainer modByAssembly = AssemblyLoader.GetModByAssembly(callingAssembly);
			if (modByAssembly == null)
			{
				throw new InvalidOperationException("ModConsole.RegisterCommand called from an assembly not listed in the mod manifest.");
			}
			if (command.Contains(":"))
			{
				throw new ArgumentException("Name may not contain a colon (:) character!", "command");
			}
			if (string.IsNullOrEmpty(help))
			{
				throw new ArgumentException("Help text may not be empty!", "help");
			}
			command = command.ToLower();
			if (Commands.ContainsKey(modByAssembly) && Commands[modByAssembly].Any((CommandRegistration c) => c.Name == command))
			{
				throw new ArgumentException("Cannot register the same command twice from the same mod (" + command + ")!", "command");
			}
			if (consoleController.HasCommand(command) && Commands.All((KeyValuePair<ModContainer, List<CommandRegistration>> p) => p.Value.All((CommandRegistration c) => c.Name != command)))
			{
				throw new ArgumentException("May not override a command included in the base game!", "command");
			}
			string command2 = modByAssembly.Info.Name.Replace(" ", string.Empty).ToLower() + ":" + command;
			string command3 = string.Concat(modByAssembly.Info.Id, ":", command);
			bool flag = consoleController.HasCommand(command);
			bool flag2 = consoleController.HasCommand(command2);
			if (!flag)
			{
				consoleController.RegisterCommand(command, handler, help);
			}
			else
			{
				IEnumerable<ModContainer> enumerable = (from p in Commands
					where p.Value.Any((CommandRegistration c) => c.Name == command)
					select p.Key).Append(modByAssembly);
				string[] modNamesWithCommand = GetModNamesWithCommand(enumerable, command, flag2);
				string infoText = FormatInfoText(modNamesWithCommand, command);
				consoleController.UpdateCommand(command, delegate
				{
					consoleController.AppendLogLine(infoText);
				}, infoText);
				if (!flag2)
				{
					foreach (ModContainer item in enumerable)
					{
						if (item != modByAssembly)
						{
							CommandRegistration commandRegistration = Commands[item].Find((CommandRegistration r) => r.Name == command);
							string command4 = item.Info.Name.Replace(" ", string.Empty) + ":" + command;
							consoleController.UpdateCommand(command4, commandRegistration.Handler, commandRegistration.Help);
						}
					}
				}
			}
			if (!flag2)
			{
				consoleController.RegisterCommand(command2, handler, (!flag) ? string.Empty : help);
			}
			else
			{
				IEnumerable<ModContainer> enumerable2 = (from p in Commands
					where p.Value.Any((CommandRegistration c) => c.Name == command)
					select p.Key).Append(modByAssembly);
				string[] modNamesWithCommand2 = GetModNamesWithCommand(enumerable2, command, true);
				string infoText2 = FormatInfoText(modNamesWithCommand2, command2);
				consoleController.UpdateCommand(command2, delegate
				{
					consoleController.AppendLogLine(infoText2);
				}, infoText2);
				foreach (ModContainer item2 in enumerable2)
				{
					if (item2 != modByAssembly)
					{
						CommandRegistration commandRegistration2 = Commands[item2].Find((CommandRegistration r) => r.Name == command);
						string command5 = string.Concat(item2.Info.Id, ":", command);
						consoleController.UpdateCommand(command5, commandRegistration2.Handler, commandRegistration2.Help);
					}
				}
			}
			consoleController.RegisterCommand(command3, handler, (!flag2) ? string.Empty : help);
			if (Commands.ContainsKey(modByAssembly))
			{
				Commands[modByAssembly].Add(new CommandRegistration(command, handler, help));
				return;
			}
			Commands.Add(modByAssembly, new List<CommandRegistration>
			{
				new CommandRegistration(command, handler, help)
			});
		}

		private static string[] GetModNamesWithCommand(IEnumerable<ModContainer> mods, string command, bool outputFullyQualified)
		{
			return mods.Select((ModContainer m) => ((!outputFullyQualified) ? m.Info.Name.Replace(" ", string.Empty) : m.Info.Id.ToString()) + ":" + command).ToArray();
		}

		private static string FormatInfoText(string[] modsWithCommand, string command)
		{
			return string.Format("{0} is added by more than one mod! Use {1}.", command, string.Join(", ", modsWithCommand, 0, modsWithCommand.Length - 1) + ", or " + modsWithCommand.Last());
		}
	}
}
