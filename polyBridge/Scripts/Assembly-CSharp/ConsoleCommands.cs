using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ConsoleCommands
{
	private static bool m_Initialized;

	private static Dictionary<string, int> m_LuaCommandNumParamDict = new Dictionary<string, int>();

	private static List<string> m_LayoutsToSave = new List<string>();

	private static string m_LayoutsToSaveDirectory;

	private static List<DecorProxy> m_DecorProxies = new List<DecorProxy>();

	public static void Init()
	{
		if (!m_Initialized)
		{
			RegisterCommands();
			m_Initialized = true;
		}
	}

	public static void ClearLuaCommands()
	{
		if (uConsole.IsOn())
		{
			uConsole.TurnOff();
		}
		foreach (string key in m_LuaCommandNumParamDict.Keys)
		{
			if (uConsole.m_CommandsDict.ContainsKey(key))
			{
				uConsole.m_CommandsDict.Remove(key);
			}
			if (uConsole.m_CommandsHelp.ContainsKey(key))
			{
				uConsole.m_CommandsHelp.Remove(key);
			}
			if (uConsole.m_CommandsList.Contains(key))
			{
				uConsole.m_CommandsList.Remove(key);
			}
		}
		m_LuaCommandNumParamDict.Clear();
	}

	public static void AddLuaCommand(string command, string help, int numParameters)
	{
		if (m_LuaCommandNumParamDict.ContainsKey(command))
		{
			Debug.Log("Trying to add duplicate console command: " + command);
			return;
		}
		m_LuaCommandNumParamDict.Add(command, numParameters);
		uConsole.RegisterCommand(command, help, RunLuaCommand);
	}

	private static void RegisterCommands()
	{
		uConsole.RegisterCommand("bridge_export", bridge_export);
		uConsole.RegisterCommand("bridge_import", bridge_import);
		uConsole.RegisterCommand("force_workshop_id", force_workshop_id);
		uConsole.RegisterCommand("force_workshop_id_clear", force_workshop_id_clear);
		uConsole.RegisterCommand("show_vehicle_com", show_vehicle_com);
		uConsole.RegisterCommand("terrain_lights", terrain_lights);
	}

	private static void RunLuaCommand()
	{
		string lastCommand = uConsole.GetLastCommand();
		if (string.IsNullOrEmpty(lastCommand))
		{
			Debug.Log("Error detecting console command");
			return;
		}
		if (!m_LuaCommandNumParamDict.ContainsKey(lastCommand))
		{
			Debug.Log("Invalid console command: " + lastCommand);
			return;
		}
		List<string> allParameters = uConsole.GetAllParameters();
		if (allParameters.Count < m_LuaCommandNumParamDict[lastCommand])
		{
			Debug.Log($"Console command [{lastCommand}] requires {m_LuaCommandNumParamDict[lastCommand]} parameters");
		}
		else
		{
			ModApi.RunConsoleCommand(lastCommand, allParameters);
		}
	}

	private static void bridge_export()
	{
		if (GameManager.GetGameMode() != GameMode.SANDBOX || GameStateManager.GetState() != GameState.BUILD)
		{
			uConsole.Log("Must be in Sandbox Build Mode to export a bridge slot from a Sandbox layout");
			return;
		}
		string text = string.Empty;
		if (uConsole.GetNumParameters() == 0)
		{
			if (Sandbox.m_CurrentLayoutData == null || string.IsNullOrEmpty(Sandbox.m_CurrentLayoutName))
			{
				uConsole.Log("Save Sandbox level first or specificy name to use for export like: export_bridge <name>");
			}
			else
			{
				text = Path.GetFileNameWithoutExtension(Sandbox.m_CurrentLayoutName);
			}
		}
		else
		{
			text = uConsole.GetString();
		}
		if (string.IsNullOrEmpty(text))
		{
			uConsole.Log("Cannot export bridge to empty filename");
			return;
		}
		if (!text.EndsWith(BridgeSaveSlots.SAVE_EXTENSION))
		{
			text += BridgeSaveSlots.SAVE_EXTENSION;
		}
		string text2 = "Exported";
		if (Sandbox.m_CurrentLayoutData != null && !string.IsNullOrEmpty(Sandbox.m_CurrentLayoutData.m_Workshop.m_Id))
		{
			text2 = Sandbox.m_CurrentLayoutData.m_Workshop.m_Id;
		}
		try
		{
			BridgeSaveSlotData bridgeSaveSlotData = BridgeSaveSlots.Create(Path.GetFileName(text), BridgeSaveSlots.NUM_RESERVED_SLOTS + 1);
			bridgeSaveSlotData.m_Bridge = BridgeSave.SerializeBinary();
			bridgeSaveSlotData.m_Budget = Mathf.RoundToInt(Budget.m_BridgeCost);
			bridgeSaveSlotData.m_Thumb = SaveSlotImageMaker.CaptureImage(GameStateManager.GetState());
			bridgeSaveSlotData.m_LevelID = Game.GetLevelId();
			BridgeSaveSlots.Save(text2, bridgeSaveSlotData);
			uConsole.Log("Slot saved as " + Path.Combine(BridgeSaveSlots.GetSavePath(Profiles.GetActiveProfileName()), text2, text));
		}
		catch (Exception ex)
		{
			uConsole.Log("Exception trying to save slot: " + ex.Message);
		}
	}

	private static void bridge_import()
	{
		if (GameManager.GetGameMode() != GameMode.SANDBOX)
		{
			uConsole.Log("Must be in Sandbox to import a bridge from a save slot");
			return;
		}
		string empty = string.Empty;
		if (uConsole.GetNumParameters() != 1)
		{
			uConsole.Log("Specify the save slot filename to use, as a relative path to SaveSlots (e.g., 001/Auto-Save.slot)");
			return;
		}
		empty = uConsole.GetString();
		if (!empty.EndsWith(BridgeSaveSlots.SAVE_EXTENSION))
		{
			empty += BridgeSaveSlots.SAVE_EXTENSION;
		}
		string text = Path.Combine(BridgeSaveSlots.GetSavePath(Profiles.GetActiveProfileName()), empty);
		if (!Utils.FileExists(text))
		{
			uConsole.Log("Slot '" + text + "' not found");
			return;
		}
		BridgeSaveSlotData bridgeSaveSlotData = BridgeSaveSlots.Load(text);
		if (bridgeSaveSlotData == null)
		{
			uConsole.Log("Failed to load '" + text + "'");
			return;
		}
		BridgeSaveData bridgeSaveData = new BridgeSaveData();
		int offset = 0;
		bridgeSaveData.DeserializeBinary(bridgeSaveSlotData.m_Bridge, ref offset);
		Bridge.ClearAndLoad(bridgeSaveData);
	}

	private static void force_workshop_id()
	{
		if (uConsole.GetNumParameters() == 0)
		{
			if (string.IsNullOrEmpty(Workshop.m_ForceWorkshopID))
			{
				uConsole.Log("Force Workshop ID is not set.  Usage: force_workshop_id <ID>");
			}
			else
			{
				uConsole.Log("Force Workshop ID is set to " + Workshop.m_ForceWorkshopID);
			}
		}
		Workshop.m_ForceWorkshopID = uConsole.GetString();
	}

	private static void force_workshop_id_clear()
	{
		Workshop.m_ForceWorkshopID = string.Empty;
		uConsole.Log("Force Workshop ID cleared");
	}

	private static void show_vehicle_com()
	{
		if (uConsole.GetNumParameters() == 0)
		{
			Sandbox.m_ShowVehicleCenterOfMass = !Sandbox.m_ShowVehicleCenterOfMass;
		}
		if (uConsole.GetNumParameters() == 1)
		{
			Sandbox.m_ShowVehicleCenterOfMass = uConsole.GetBool();
		}
		if (uConsole.GetNumParameters() == 0 || uConsole.GetNumParameters() == 1)
		{
			if (GameStateManager.GetState() == GameState.SANDBOX || GameStateManager.GetState() == GameState.BUILD)
			{
				Vehicles.ShowCenterOfMass(Sandbox.m_ShowVehicleCenterOfMass);
			}
			Debug.LogFormat("Show Vehicle Center Of Mass {0}", Sandbox.m_ShowVehicleCenterOfMass ? "ENABLED" : "DISABLED");
		}
	}

	private static void terrain_lights()
	{
		if (uConsole.GetNumParameters() == 0)
		{
			Profiles.m_ActiveProfile.m_TerrainLights = !Profiles.m_ActiveProfile.m_TerrainLights;
		}
		if (uConsole.GetNumParameters() == 1)
		{
			Profiles.m_ActiveProfile.m_TerrainLights = uConsole.GetBool();
		}
		if (uConsole.GetNumParameters() == 0 || uConsole.GetNumParameters() == 1)
		{
			if (GameStateManager.GetState() == GameState.SIM || GameStateManager.GetState() == GameState.MAIN_MENU)
			{
				TerrainLights.TurnOn(Profiles.m_ActiveProfile.m_TerrainLights);
			}
			Debug.LogFormat("Terrain Lights {0}", Profiles.m_ActiveProfile.m_TerrainLights ? "ENABLED" : "DISABLED");
		}
		Profiles.SaveActiveProfile();
	}
}
