using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Reflection;
using System.Text;
using ConsoleTables;
using InternalModding.Loading.Sources;
using InternalModding.Projects;
using Steamworks;
using UnityEngine;
using mattmc3.dotmore.Collections.Generic;

public class BesiegeConsoleController : ConsoleController, IConsoleController
{
	private const int MaxSayText = 200;

	private NetworkAuxAddPiece _networkAuxAddPiece;

	public static AdditionalDirsModSource AddModDirSource;

	private NetworkAuxAddPiece networkAuxAddPiece
	{
		get
		{
			return (!(_networkAuxAddPiece != null)) ? (_networkAuxAddPiece = NetworkAuxAddPiece.Instance) : _networkAuxAddPiece;
		}
	}

	protected override void RegisterCommands()
	{
		ParameterExpression parameterExpression = Expression.Parameter(typeof(BesiegeConfig), "x");
		ParameterExpression parameterExpression2 = Expression.Parameter(typeof(BesiegeConfig), "x");
		ParameterExpression parameterExpression3 = Expression.Parameter(typeof(StatMaster), "x");
		ParameterExpression parameterExpression4 = Expression.Parameter(typeof(Type), "x");
		ParameterExpression parameterExpression5 = Expression.Parameter(typeof(BesiegeConfig), "x");
		ParameterExpression parameterExpression6 = Expression.Parameter(typeof(BesiegeConfig), "x");
		RegisterCommand("disconnect", CmdDisconnect, "Disconnects from current game.");
		RegisterCommand("reconnect", CmdReconnect, "Reconnect to the last known ip/port.");
		RegisterCommand("rcon", CmdRcon, "Sends a console command.\n  Use `rcon help` to see what command are available.");
		RegisterCommand("connect", CmdConnect, "Connects to a server. Usage: connect <ip:port>.");
		RegisterCommand("clear", CmdClearConsole, "Clears the console.");
		RegisterCommand("coninfo", CmdShowConnectionInfo, "Shows connection info.");
		RegisterCommand("say", CmdSay, "Says something on the global chat. Usage: say <text>.");
		RegisterCommand("addmodsdir", CmdAddModsDir, "Adds a new directory the game should search for mods.\n  The directory is treated like the Besiege_Data/Mods/ directory is.\n  Usage: addmoddir <path>.");
		RegisterCommand("removemodsdir", CmdRemoveModsDir, "Removes a directory from the list of folder to search for mods.\n  Usage: removemoddir <path>.");
		RegisterCommand("listmodsdirs", CmdListModsDirs, "Outputs a list of additional directories searched for mods.");
		RegisterCommand("createmod", CmdCreateMod, "Creates a new mod project.\n  Usage: createmod <name> <mod projects folder>.");
		RegisterCommand("createblock", CmdCreateBlock, "Adds a new block to an existing mod.\n  Usage: createblock <modid | name> <block name>.");
		RegisterCommand("createentity", CmdCreateEntity, "Adds a new entity to an existing mod.\n  Usage: createentity <modid | name> <entity name>.");
		RegisterCommand("createassembly", CmdCreateAssembly, "Adds a new assembly to an existing mod.\n  Usage: createassembly <modid | name> <compiled | script> <assembly name> <default namespace>.");
		RegisterVariable("rconpassword", OptionsMaster.BesiegeConfig, Expression.Lambda<Func<BesiegeConfig, string>>(Expression.Property(parameterExpression, (MethodInfo)MethodBase.GetMethodFromHandle((RuntimeMethodHandle)/*OpCode not supported: LdMemberToken*/)), new ParameterExpression[1] { parameterExpression }), "Shows or sets the remote control password.\n  Usage: rconpassword <value>.");
		RegisterVariable("name", OptionsMaster.BesiegeConfig, Expression.Lambda<Func<BesiegeConfig, string>>(Expression.Property(parameterExpression2, (MethodInfo)MethodBase.GetMethodFromHandle((RuntimeMethodHandle)/*OpCode not supported: LdMemberToken*/)), new ParameterExpression[1] { parameterExpression2 }), "Shows or sets the player name.\n  Usage: name <value>.");
		RegisterVariable("developer", SingleInstance<StatMaster>.Instance, Expression.Lambda<Func<StatMaster, bool>>(Expression.Field(parameterExpression3, FieldInfo.GetFieldFromHandle((RuntimeFieldHandle)/*OpCode not supported: LdMemberToken*/)), new ParameterExpression[1] { parameterExpression3 }), "Shows or sets the developer option.\n  Usage: developer <true/false>.");
		RegisterVariable("show_net_stats", Expression.Lambda<Func<Type, bool>>(Expression.Field(null, FieldInfo.GetFieldFromHandle((RuntimeFieldHandle)/*OpCode not supported: LdMemberToken*/)), new ParameterExpression[1] { parameterExpression4 }), "Shows or sets the show network stats variable.\n  Usage: show_net_stats <true/false>.");
		RegisterVariable("show_logs", OptionsMaster.BesiegeConfig, Expression.Lambda<Func<BesiegeConfig, bool>>(Expression.Property(parameterExpression5, (MethodInfo)MethodBase.GetMethodFromHandle((RuntimeMethodHandle)/*OpCode not supported: LdMemberToken*/)), new ParameterExpression[1] { parameterExpression5 }), "Shows or sets whether to show log messages.\n Usage: show_logs <true/false>.");
		RegisterVariable("show_log_framenumber", OptionsMaster.BesiegeConfig, Expression.Lambda<Func<BesiegeConfig, bool>>(Expression.Property(parameterExpression6, (MethodInfo)MethodBase.GetMethodFromHandle((RuntimeMethodHandle)/*OpCode not supported: LdMemberToken*/)), new ParameterExpression[1] { parameterExpression6 }), "Shows or sets whether to add a frame number to log output.\n Usage: show_log_framenumber <true/false>.");
		RegisterRconCommand("help", RconCmdHelp, "Prints this help.");
		RegisterRconCommand("listlevels", RconCmdListLevels, "Lists all the levels on the server.");
		RegisterRconCommand("levellist", RconCmdListLevels, "Alias for listlevels.");
		RegisterRconCommand("maprotation", RconCmdMapRotation, "Lists all the levels in the maprotation.");
		RegisterRconCommand("addlevel", RconCmdAddLevel, "Adds a level to the maprotation. Usage: rcon addlevel <levelName>.\n  Use `rcon listlevels` to see what levels are available.");
		RegisterRconCommand("removelevel", RconCmdRemoveLevel, "Removes a level from the maprotation.\n  Usage: rcon removelevel <levelName>.");
		RegisterRconCommand("loadlevel", RconCmdLoadLevel, "Loads a level.\n  Usage: rcon load <levelName>.");
		RegisterRconCommand("leveleditor", RconCmdToggleLevelEditor, "Enables/disables the level editor.\n  Usage: rcon leveleditor <on/off>.");
		RegisterRconCommand("stopsim", RconCmdStopSimulation, "Stops all simulating players.");
		RegisterRconCommand("nextlevel", RconCmdNextLevel, "Loads the next level.");
		RegisterRconCommand("reload", RconCmdReloadLevel, "Reloads the level.");
		RegisterRconCommand("timescale", RconCmdSetTimescale, "Sets the timescale of the server.\n  Usage: rcon timescale <value>. It needs to be a value between 0 and 1.");
		RegisterRconCommand("status", RconCmdStatus, "Shows the status of server.");
		RegisterRconCommand("allready", RconCmdAllReady, "Forces everyone to ready/unready.\n Usage: rcon allready <on/off>");
		RegisterRconCommand("kick", RconCmdKick, "Kicks a player from the server.\n  Usage: rcon kick <playerId> (<reason>). The reason parameter is optional.\n  Use `rcon status` to get the playerId.");
		RegisterRconCommand("restart", RconCmdRestart, "Restarts the server.");
		base.RegisterCommands();
	}

	protected override void HandleUnknownCommand(string command, string[] args)
	{
		string[] args2 = CombineCommandArgs(command, args);
		if (!ExecuteSayCmd(args2))
		{
			base.HandleUnknownCommand(command, args);
		}
	}

	private string[] CombineCommandArgs(string command, string[] args)
	{
		string[] array = new string[args.Length + 1];
		array[0] = command;
		if (args.Length > 0)
		{
			Array.Copy(args, 0, array, 1, args.Length);
		}
		return array;
	}

	private void CmdDisconnect(string[] args)
	{
		NetworkScene networkScene = NetworkScene.Instance;
		if (!(networkScene == null) && (StatMaster.isHosting || StatMaster.isClient))
		{
			HideConsole();
			networkScene.ManualStop();
		}
	}

	private void CmdReconnect(string[] args)
	{
		NetworkScene networkScene = NetworkScene.Instance;
		if (!(networkScene == null))
		{
			if (StatMaster.isHosting)
			{
				AppendLogLine("Host can not reconnect.");
				return;
			}
			HideConsole();
			networkScene.Reconnect();
		}
	}

	private void CmdRcon(string[] args)
	{
		string rconPassword = OptionsMaster.BesiegeConfig.RconPassword;
		if (args.Length == 0)
		{
			return;
		}
		if (!StatMaster.isMP || (!StatMaster.isClient && !StatMaster.isHosting))
		{
			AppendLogLine("Not connected to a server.");
			return;
		}
		if (string.IsNullOrEmpty(rconPassword))
		{
			AppendLogLine("Rconpassword is empty, please set it first.");
			return;
		}
		if (rconPassword.Length > 254)
		{
			AppendLogLine("Rconpassword too long.");
			return;
		}
		string text = args[0];
		if (text.Length > 254)
		{
			AppendLogLine("Command too long.");
			return;
		}
		string text2 = string.Empty;
		if (args.Length > 1)
		{
			text2 = string.Join("\n", args, 1, args.Length - 1);
			if (text2.Length > 254)
			{
				AppendLogLine("Arguments too long.");
				return;
			}
		}
		byte[] bytes = Encoding.UTF8.GetBytes(rconPassword);
		byte[] bytes2 = Encoding.UTF8.GetBytes(text);
		byte[] array = null;
		int num = 0;
		if (args.Length > 1)
		{
			array = Encoding.UTF8.GetBytes(text2);
			num = array.Length;
		}
		byte[] array2 = new byte[2 + bytes.Length + bytes2.Length + num];
		array2[0] = (byte)bytes.Length;
		Buffer.BlockCopy(bytes, 0, array2, 1, bytes.Length);
		array2[bytes.Length + 1] = (byte)bytes2.Length;
		Buffer.BlockCopy(bytes2, 0, array2, bytes.Length + 2, bytes2.Length);
		if (num > 0)
		{
			Buffer.BlockCopy(array, 0, array2, bytes2.Length + bytes.Length + 2, num);
		}
		networkAuxAddPiece.SendServerMessage(RPCMessageType.RconCommand, array2);
	}

	private void CmdConnect(string[] args)
	{
		if (args.Length == 0)
		{
			AppendLogLine("Usage: connect: <ip> or connect <ip:port>.");
			return;
		}
		IPEndPoint iPEndPoint = BesiegeArgumentsHelper.ParseIPPort(args[0]);
		if (iPEndPoint == null)
		{
			AppendLogLine("Could not parse ip:port. Usage: connect: <ip> or connect <ip:port>.");
			return;
		}
		HideConsole();
		Arguments args2 = new Arguments(new string[2]
		{
			"+connect",
			args[0]
		});
		BesiegeEntryPoint.CreateEntryPoint(args2);
	}

	private void CmdClearConsole(string[] args)
	{
		ClearConsole();
	}

	private void CmdShowConnectionInfo(string[] args)
	{
		NetworkAnalyser networkAnalyser = SingleInstanceFindOnly<NetworkAnalyser>.Instance;
		StringBuilder stringBuilder = new StringBuilder().AppendLine();
		if (SteamManager.Initialized)
		{
			ConsoleTable consoleTable = new ConsoleTable("Steam Connection State");
			if (StatMaster.isHosting)
			{
				consoleTable.AddRow("Hosting server with SteamID: " + SteamGameServer.GetSteamID());
			}
			else
			{
				BesiegeNetworkManager besiegeNetworkManager = BesiegeNetworkManager.Instance;
				if (StatMaster.networkActive)
				{
					P2PSessionState_t pConnectionState;
					if (SteamNetworking.GetP2PSessionState((CSteamID)besiegeNetworkManager.ServerID, out pConnectionState))
					{
						if (pConnectionState.m_bUsingRelay == 1)
						{
							consoleTable.AddRow("Connected to a server using a relay server");
						}
						else
						{
							consoleTable.AddRow("Connected directly to a server");
						}
					}
					else
					{
						consoleTable.AddRow("Not connected");
					}
				}
				else
				{
					consoleTable.AddRow("Not connected");
				}
			}
			stringBuilder.Append(consoleTable.ToString());
		}
		else
		{
			string text = ((!networkAnalyser.FacilitatorController.IsConnectedToFacilitator) ? "Not connected" : "Connected");
			ConsoleTable consoleTable2 = new ConsoleTable("Facilitator", "UPnP", "NAT");
			consoleTable2.Options.EnableCount = false;
			consoleTable2.AddRow(text, ReferenceMaster.UPNPStatus, networkAnalyser.NATConnectionTester.ConnectionTestResult.ToString());
			stringBuilder.Append(consoleTable2.ToString());
		}
		AppendLogLine(stringBuilder.ToString());
	}

	private void CmdSay(string[] args)
	{
		ExecuteSayCmd(args);
	}

	private bool ExecuteSayCmd(string[] args)
	{
		if (args.Length == 0)
		{
			AppendLogLine("No arguments provided. Usage: say: <text>.");
			return false;
		}
		if (!StatMaster.isMP || (!StatMaster.isClient && !StatMaster.isHosting))
		{
			AppendLogLine("Not connected to a server.");
			return false;
		}
		string text = string.Join(" ", args, 0, args.Length);
		if (text.Length > 200)
		{
			AppendLogLine("Text is too long.");
			return true;
		}
		return true;
	}

	private void CmdAddModsDir(string[] args)
	{
		if (args.Length != 1)
		{
			AppendLogLine("Usage: addmoddir <path>");
			return;
		}
		if (!Directory.Exists(args[0]))
		{
			AppendLogLine("Directory does not exist!");
			return;
		}
		if (OptionsMaster.BesiegeConfig.AdditionalModsDirectories.Contains(args[0]))
		{
			AppendLogLine("Directory is already searched for mods!");
			return;
		}
		OptionsMaster.BesiegeConfig.AdditionalModsDirectories = OptionsMaster.BesiegeConfig.AdditionalModsDirectories.Append(args[0]).ToArray();
		AddModDirSource.AddDir(args[0]);
	}

	private void CmdRemoveModsDir(string[] args)
	{
		if (args.Length != 1)
		{
			AppendLogLine("Usage: removemoddir <path>");
			return;
		}
		if (!OptionsMaster.BesiegeConfig.AdditionalModsDirectories.Contains(args[0]))
		{
			AppendLogLine("Directory is not currently searched for mods!");
			return;
		}
		OptionsMaster.BesiegeConfig.AdditionalModsDirectories = OptionsMaster.BesiegeConfig.AdditionalModsDirectories.Where((string dir) => dir != args[0]).ToArray();
	}

	private void CmdListModsDirs(string[] args)
	{
		if (OptionsMaster.BesiegeConfig.AdditionalModsDirectories.Length == 0)
		{
			AppendLogLine("No additional directories!");
		}
		else
		{
			AppendLogLine(string.Join("\n", OptionsMaster.BesiegeConfig.AdditionalModsDirectories));
		}
	}

	private void CmdCreateMod(string[] args)
	{
		ModCreator.CreateModCmd(args);
	}

	private void CmdCreateBlock(string[] args)
	{
		ModCreator.CreateBlockCmd(args);
	}

	private void CmdCreateEntity(string[] args)
	{
		ModCreator.CreateEntityCmd(args);
	}

	private void CmdCreateAssembly(string[] args)
	{
		ModCreator.CreateAssemblyCmd(args);
	}

	private void RconCmdKick(ushort playerId, string command, string[] args)
	{
		ushort result;
		if (args.Length == 0)
		{
			SendRconHelp(playerId, command);
		}
		else if (!ushort.TryParse(args[0], out result))
		{
			SendRconHelp(playerId, command);
		}
		else if (args.Length > 1)
		{
			string reason = string.Join(" ", args, 1, args.Length - 1);
			networkAuxAddPiece.DropClient(result, reason);
		}
		else
		{
			networkAuxAddPiece.DropClient(result);
		}
	}

	private void RconCmdAllReady(ushort playerId, string command, string[] args)
	{
		if (!OptionsMaster.votingEnabled)
		{
			SendConsoleMessage(playerId, "Not in voting mode...");
			return;
		}
		if (args.Length == 0)
		{
			SendRconHelp(playerId, command);
			return;
		}
		string text = args[0].ToLower();
		bool flag = true;
		if (text.Equals("on"))
		{
			flag = true;
		}
		else
		{
			if (!text.Equals("off"))
			{
				SendRconHelp(playerId, command);
				return;
			}
			flag = false;
		}
		networkAuxAddPiece.SetPlayerReadyStateAll(flag);
	}

	private void RconCmdStatus(ushort playerId, string command, string[] args)
	{
		StringBuilder stringBuilder = new StringBuilder().AppendLine();
		stringBuilder.AppendLine("=============== Status ===============");
		stringBuilder.AppendFormat("Current level: {0}", LevelEditor.Instance.Settings.Name).AppendLine();
		stringBuilder.AppendLine("Player list:");
		BesiegeNetworkManager besiegeNetworkManager = BesiegeNetworkManager.Instance;
		ConsoleTable consoleTable = new ConsoleTable("Id", "Name", "Team", "Ping");
		foreach (PlayerData player in Playerlist.Players)
		{
			consoleTable.AddRow(player.networkId, player.name, player.team, besiegeNetworkManager.GetPlayerPing(player));
		}
		stringBuilder.Append(consoleTable.ToString());
		SendConsoleMessage(playerId, stringBuilder.ToString());
	}

	private void RconCmdSetTimescale(ushort playerId, string command, string[] args)
	{
		if (args.Length == 0)
		{
			SendRconHelp(playerId, command);
			return;
		}
		float result;
		if (!float.TryParse(args[0], out result))
		{
			SendRconHelp(playerId, command);
			return;
		}
		result = Mathf.Clamp(result, 0f, 1f);
		TimeSlider timeSlider = TimeSlider.Instance;
		timeSlider.SetPercentage(result);
		timeSlider.SendTimeScale(false);
	}

	private void RconCmdReloadLevel(ushort playerId, string command, string[] args)
	{
		if (!ReloadLevel())
		{
			SendConsoleMessage(playerId, "There isn't any level to reload. First load a level.");
		}
	}

	private void RconCmdNextLevel(ushort playerId, string command, string[] args)
	{
		ServerSettings serverSettings = NetworkScene.ServerSettings;
		if (StatMaster.Mode.levelEdit)
		{
			SendConsoleMessage(playerId, "Not in playlist mode...");
		}
		else if (serverSettings.playList.Count == 0)
		{
			SendConsoleMessage(playerId, "No levels in playlist...");
		}
		else
		{
			LevelEditor.Instance.winCondition.OnNext();
		}
	}

	private void RconCmdStopSimulation(ushort playerId, string command, string[] args)
	{
		networkAuxAddPiece.StopAllSimulation();
	}

	private void RconCmdToggleLevelEditor(ushort playerId, string command, string[] args)
	{
		if (args.Length == 0)
		{
			SendRconHelp(playerId, command);
			return;
		}
		string text = args[0].ToLower();
		bool flag = false;
		if (text.Equals("on"))
		{
			flag = true;
		}
		else
		{
			if (!text.Equals("off"))
			{
				SendRconHelp(playerId, command);
				return;
			}
			flag = false;
		}
		networkAuxAddPiece.SendToggleLevelEditor(flag);
	}

	private void RconCmdLoadLevel(ushort playerId, string command, string[] args)
	{
		if (args.Length == 0)
		{
			SendRconHelp(playerId, command);
			return;
		}
		string text = ((!args[0].Contains(".blv")) ? (args[0] + ".blv") : args[0]);
		string path = Path.Combine(StaticSettings.LevelPath, text);
		if (!File.Exists(path))
		{
			SendConsoleMessage(playerId, "Level \"" + text + "\" does not exist.");
			return;
		}
		SendConsoleMessage(playerId, "Loading level \"" + text + "\"...");
		string levelData = File.ReadAllText(path);
		networkAuxAddPiece.LoadLevel(levelData, Path.GetFileNameWithoutExtension(text));
	}

	private void RconCmdRemoveLevel(ushort playerId, string command, string[] args)
	{
		if (args.Length == 0)
		{
			SendRconHelp(playerId, command);
			return;
		}
		string text = ((!args[0].Contains(".blv")) ? (args[0] + ".blv") : args[0]);
		ServerSettings serverSettings = NetworkScene.ServerSettings;
		if (!serverSettings.playList.Contains(text))
		{
			SendConsoleMessage(playerId, "Level \"" + text + "\" is not in the map rotation");
			return;
		}
		serverSettings.playList.Remove(text);
		NetworkScene.Instance.UpdateSettings(serverSettings);
		SendConsoleMessage(playerId, "Level \"" + text + "\" was removed from the map rotation");
	}

	private void RconCmdAddLevel(ushort playerId, string command, string[] args)
	{
		if (args.Length == 0)
		{
			SendRconHelp(playerId, command);
			return;
		}
		string text = args[0];
		ServerSettings serverSettings = NetworkScene.ServerSettings;
		if (!AddLevel(serverSettings, text))
		{
			SendConsoleMessage(playerId, "Level " + text + " does not exist.");
			return;
		}
		NetworkScene.Instance.UpdateSettings(serverSettings);
		SendConsoleMessage(playerId, "Level \"" + text + "\" was added to the map rotation");
	}

	private void RconCmdListLevels(ushort playerId, string command, string[] args)
	{
		StringBuilder stringBuilder = new StringBuilder();
		DirectoryInfo directoryInfo = new DirectoryInfo(StaticSettings.LevelPath);
		foreach (FileInfo item in from x in directoryInfo.GetFiles("*.blv", SearchOption.AllDirectories)
			orderby x.FullName
			select x)
		{
			stringBuilder.AppendLine(item.FullName.Substring(directoryInfo.FullName.Length + 1, item.FullName.Length - directoryInfo.FullName.Length - 5));
		}
		SendConsoleMessage(playerId, stringBuilder.ToString());
	}

	private void RconCmdMapRotation(ushort playerId, string command, string[] args)
	{
		ServerSettings serverSettings = NetworkScene.ServerSettings;
		StringBuilder stringBuilder = new StringBuilder();
		if (serverSettings.playList.Count == 0)
		{
			stringBuilder.AppendLine("Server has no levels in the playlist");
		}
		else
		{
			stringBuilder.AppendLine("Levels in map rotation: " + serverSettings.playList.Count);
			foreach (string play in serverSettings.playList)
			{
				stringBuilder.AppendLine(" - " + play);
			}
		}
		SendConsoleMessage(playerId, stringBuilder.ToString());
	}

	private void RconCmdHelp(ushort playerId, string command, string[] args)
	{
		StringBuilder stringBuilder = new StringBuilder().AppendLine();
		foreach (RemoteCommandRegistration item in rconCommands.Values.OrderBy((RemoteCommandRegistration x) => x.Name))
		{
			stringBuilder.AppendLine(string.Format("- {0}: {1}", item.Name, item.Help));
		}
		SendConsoleMessage(playerId, stringBuilder.ToString());
	}

	private void RconCmdRestart(ushort playerId, string command, string[] args)
	{
		if (!Application.isEditor)
		{
			NetworkScene.Instance.SaveServerConfig();
			RestartProcess();
			Application.Quit();
		}
	}

	private void RestartProcess()
	{
		Process process = new Process();
		string fileName = Process.GetCurrentProcess().MainModule.FileName;
		string[] commandLineArgs = Environment.GetCommandLineArgs();
		try
		{
			process.StartInfo.FileName = fileName;
			process.StartInfo.Arguments = string.Join(" ", commandLineArgs);
			process.StartInfo.UseShellExecute = false;
			process.StartInfo.CreateNoWindow = true;
			process.Start();
		}
		catch (Exception ex)
		{
			Console.WriteLine(ex.Message);
		}
	}

	private bool ReloadLevel()
	{
		return networkAuxAddPiece.ReloadLevel(false);
	}

	private bool AddLevel(ServerSettings settings, string levelName)
	{
		levelName = ((!levelName.Contains(".blv")) ? (levelName + ".blv") : levelName);
		string text = Path.Combine(StaticSettings.LevelPath, levelName);
		if (!File.Exists(text))
		{
			return false;
		}
		settings.playList.Add(text);
		return true;
	}

	virtual void IConsoleController.AppendLogLine(string message)
	{
		AppendLogLine(message);
	}

	virtual void IConsoleController.HandleRconCommand(ushort playerId, string password, string command, string[] args)
	{
		HandleRconCommand(playerId, password, command, args);
	}
}
