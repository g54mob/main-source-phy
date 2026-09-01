using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LayoutValidator
{
	public static bool m_Validating;

	private static LayoutValidatorSubMode m_LayoutValidatorSubMode;

	private static bool m_WaitOneMoreFrame;

	private static List<string> m_LayoutsToValidate = new List<string>();

	private static int m_NextSlotToValidateIndex;

	private static int m_RestoreBridgeSimulationSpeedIndex;

	private static List<string> m_Passed = new List<string>();

	private static List<string> m_Failed = new List<string>();

	private static List<string> m_Hung = new List<string>();

	private static List<string> m_Breaks = new List<string>();

	public static void Validate(List<string> layouts)
	{
		m_LayoutsToValidate = new List<string>(layouts);
		m_NextSlotToValidateIndex = 0;
		m_Validating = true;
		m_LayoutValidatorSubMode = LayoutValidatorSubMode.PRELOAD_LEVEL;
		m_RestoreBridgeSimulationSpeedIndex = -1;
		m_Passed.Clear();
		m_Failed.Clear();
		m_Hung.Clear();
		m_Breaks.Clear();
		GameStatePreloadingAssets.PreloadLevel(GetLayoutPath(m_NextSlotToValidateIndex), null, PreloadCallback);
	}

	public static void Process()
	{
		if (!m_Validating)
		{
			return;
		}
		if (m_NextSlotToValidateIndex >= m_LayoutsToValidate.Count)
		{
			End();
			m_Validating = false;
			GameStateManager.SwitchToState(GameState.MAIN_MENU);
		}
		else if (m_WaitOneMoreFrame)
		{
			m_WaitOneMoreFrame = false;
		}
		else
		{
			if (m_LayoutValidatorSubMode == LayoutValidatorSubMode.PRELOAD_LEVEL)
			{
				return;
			}
			if (m_LayoutValidatorSubMode == LayoutValidatorSubMode.LOAD_LEVEL)
			{
				string layoutPath = GetLayoutPath(m_NextSlotToValidateIndex);
				if (layoutPath.EndsWith(SandboxLayout.SAVE_EXTENSION))
				{
					GameManager.SetGameMode(GameMode.SANDBOX, GameSubMode.NONE);
					if (GameStateManager.GetState() != GameState.BUILD)
					{
						GameStateManager.SwitchToState(GameState.BUILD);
					}
					Sandbox.LoadLayout(layoutPath);
				}
				else
				{
					GameManager.SetGameMode(GameMode.CAMPAIGN, GameSubMode.NONE);
					if (GameStateManager.GetState() != GameState.BUILD)
					{
						GameStateManager.SwitchToState(GameState.BUILD);
					}
					string levelIDFromLayout = GetLevelIDFromLayout(layoutPath);
					Campaign.LoadLevel(CampaignWorlds.m_Instance.GetLevelFromId(levelIDFromLayout));
				}
				m_LayoutValidatorSubMode = LayoutValidatorSubMode.START_SIM;
				m_WaitOneMoreFrame = true;
				return;
			}
			if (m_LayoutValidatorSubMode == LayoutValidatorSubMode.START_SIM)
			{
				if (m_LayoutsToValidate[m_NextSlotToValidateIndex].EndsWith(".slot"))
				{
					LoadSlot(m_LayoutsToValidate[m_NextSlotToValidateIndex]);
				}
				m_RestoreBridgeSimulationSpeedIndex = BridgeSimSpeed.m_SimulationSpeedIndex;
				BridgeSimSpeed.SetSimulationSpeedIndex(BridgeSimSpeed.m_SimulationSpeeds.Count - 1);
				GameStateManager.SwitchToState(GameState.SIM);
				GameStateSim.m_SkipBridgeRestoreOnExit = true;
				m_WaitOneMoreFrame = true;
				m_LayoutValidatorSubMode = LayoutValidatorSubMode.WAIT_FOR_LEVEL_COMPLETE;
				return;
			}
			if (m_LayoutValidatorSubMode == LayoutValidatorSubMode.WAIT_FOR_LEVEL_COMPLETE)
			{
				bool flag = GameStateSim.m_ElapsedSeconds > 20f;
				if (GameStateSim.m_LevelPassed || GameStateSim.m_LevelFailed || flag)
				{
					string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(m_LayoutsToValidate[m_NextSlotToValidateIndex]);
					if (GameStateSim.m_LevelPassed)
					{
						m_Passed.Add(fileNameWithoutExtension);
					}
					else if (GameStateSim.m_LevelFailed)
					{
						m_Failed.Add(fileNameWithoutExtension);
					}
					else if (flag)
					{
						m_Hung.Add(fileNameWithoutExtension);
					}
					if (GameStateSim.m_NumBridgeBreaks > 0)
					{
						m_Breaks.Add(fileNameWithoutExtension);
					}
					m_LayoutValidatorSubMode = LayoutValidatorSubMode.CLEAN_UP;
					return;
				}
			}
			if (m_LayoutValidatorSubMode != LayoutValidatorSubMode.CLEAN_UP)
			{
				return;
			}
			m_NextSlotToValidateIndex++;
			if (m_NextSlotToValidateIndex >= m_LayoutsToValidate.Count)
			{
				return;
			}
			GameStateSim.Exit(GameState.BUILD);
			GameStateManager.BashState(GameState.BUILD);
			m_LayoutValidatorSubMode = LayoutValidatorSubMode.PRELOAD_LEVEL;
			string layoutPath2 = GetLayoutPath(m_NextSlotToValidateIndex);
			if (string.IsNullOrEmpty(layoutPath2))
			{
				Debug.Log("Invalid " + m_LayoutsToValidate[m_NextSlotToValidateIndex]);
			}
			if (!string.IsNullOrEmpty(layoutPath2))
			{
				if (!Prefabs.m_Instance.IsLevelPreloaded(layoutPath2))
				{
					GameStatePreloadingAssets.PreloadLevel(layoutPath2, null, PreloadCallback);
				}
				else
				{
					PreloadCallback(layoutPath2, null);
				}
			}
			m_WaitOneMoreFrame = true;
		}
	}

	public static void End()
	{
		if (m_RestoreBridgeSimulationSpeedIndex != -1)
		{
			BridgeSimSpeed.SetSimulationSpeedIndex(m_RestoreBridgeSimulationSpeedIndex);
		}
		uConsole.Log($"NUM PASSED: {m_Passed.Count}");
		uConsole.Log($"NUM FAILED: {m_Failed.Count}");
		foreach (string item in m_Failed)
		{
			uConsole.Log("\t" + item);
		}
		uConsole.Log($"NUM HUNG: {m_Hung.Count}");
		foreach (string item2 in m_Hung)
		{
			uConsole.Log("\t" + item2);
		}
		uConsole.Log($"NUM BREAKS: {m_Breaks.Count}");
		foreach (string @break in m_Breaks)
		{
			uConsole.Log("\t" + @break);
		}
	}

	private static void PreloadCallback(string levelFilename, FileSlot slot)
	{
		m_LayoutValidatorSubMode = LayoutValidatorSubMode.LOAD_LEVEL;
	}

	private static void LoadSlot(string slotFilename)
	{
		string text = Path.Combine(Application.streamingAssetsPath, "LevelSolutions", "Slots", slotFilename);
		try
		{
			BridgeSaveSlotData bridgeSaveSlotData = BridgeSaveSlots.Load(text);
			if (bridgeSaveSlotData != null)
			{
				Bridge.ClearAndLoadBinary(bridgeSaveSlotData.m_Bridge);
			}
		}
		catch (Exception arg)
		{
			Debug.LogWarning($"Caught exception '{arg}' trying to load slot '{text}'");
		}
	}

	private static string GetLayoutPath(int index)
	{
		string text = m_LayoutsToValidate[index];
		if (text.EndsWith(SandboxLayout.SAVE_EXTENSION))
		{
			return text;
		}
		string levelIDFromLayout = GetLevelIDFromLayout(text);
		CampaignLevel levelFromId = CampaignWorlds.m_Instance.GetLevelFromId(levelIDFromLayout);
		if (!(levelFromId != null))
		{
			return string.Empty;
		}
		return levelFromId.GetLayoutPath();
	}

	private static string GetLevelIDFromLayout(string layout)
	{
		if (layout.EndsWith(SandboxLayout.SAVE_EXTENSION))
		{
			return Path.GetFileNameWithoutExtension(layout);
		}
		return layout.Substring(0, 3);
	}
}
