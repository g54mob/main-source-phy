using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DumpReplays
{
	public static bool m_Dumping;

	public static PointOfViewType m_PointOfViewType;

	private static DumpReplaySubMode m_DumpSubMode;

	private static int m_NextSlotToDumpIndex;

	private static int m_RestoreBridgeSimulationSpeedIndex;

	private static bool m_WaitOneMoreFrame;

	private static List<string> m_SlotsToDump = new List<string>();

	private static Dictionary<string, int> m_NumReplaysDumped = new Dictionary<string, int>();

	public static void Dump(List<string> slots, PointOfViewType pointOfViewType)
	{
		m_RestoreBridgeSimulationSpeedIndex = -1;
		m_SlotsToDump = new List<string>(slots);
		m_NextSlotToDumpIndex = 0;
		m_Dumping = true;
		m_DumpSubMode = DumpReplaySubMode.PRELOAD_LEVEL;
		m_PointOfViewType = pointOfViewType;
		m_NumReplaysDumped.Clear();
		CampaignLevel nextValidLevel = GetNextValidLevel();
		CampaignWorlds.m_Instance.GetWorldWithLevelId(nextValidLevel.m_Id);
		if (nextValidLevel != null)
		{
			GameStatePreloadingAssets.PreloadLevel(nextValidLevel.GetLayoutPath(), null, PreloadCallback);
		}
	}

	public static void Process()
	{
		if (!m_Dumping)
		{
			return;
		}
		if (m_NextSlotToDumpIndex >= m_SlotsToDump.Count)
		{
			End();
		}
		else
		{
			if (m_DumpSubMode == DumpReplaySubMode.PRELOAD_LEVEL)
			{
				return;
			}
			if (m_DumpSubMode == DumpReplaySubMode.LOAD_LEVEL)
			{
				CampaignLevel nextValidLevel = GetNextValidLevel();
				if (nextValidLevel == null)
				{
					End();
					return;
				}
				GameManager.SetGameMode(GameMode.CAMPAIGN, GameSubMode.NONE);
				GameStateManager.SwitchToState(GameState.BUILD);
				Campaign.LoadLevel(nextValidLevel);
				m_DumpSubMode = DumpReplaySubMode.START_SIM;
				m_WaitOneMoreFrame = true;
			}
			else if (m_WaitOneMoreFrame)
			{
				m_WaitOneMoreFrame = false;
			}
			else if (m_DumpSubMode == DumpReplaySubMode.START_SIM)
			{
				LoadSlot(m_SlotsToDump[m_NextSlotToDumpIndex]);
				m_RestoreBridgeSimulationSpeedIndex = BridgeSimSpeed.m_SimulationSpeedIndex;
				BridgeSimSpeed.SetSimulationSpeedIndex(BridgeSimSpeed.m_DefaultSimulationSpeedIndex + 1);
				if (Profiles.m_ActiveProfile.m_ReplayQuality != AsyncCaptureQuality.ULTRA)
				{
					Profiles.m_ActiveProfile.m_ReplayQuality = AsyncCaptureQuality.ULTRA;
					Cameras.m_AsyncCapture.Init(Profiles.m_ActiveProfile.m_ReplayQuality, Profiles.m_ActiveProfile.m_ReplayLengthSeconds);
				}
				GameStateBuild.Exit(GameState.INVALID);
				GameStateManager.BashState(GameState.SIM);
				GameStateSim.m_CapturingReplayForSolution = true;
				GameStateSim.m_SkipBridgeRestoreOnExit = true;
				GameStateSim.Enter(GameState.BUILD);
				m_DumpSubMode = DumpReplaySubMode.WAIT_FOR_LEVEL_COMPLETE;
			}
			else if (m_DumpSubMode == DumpReplaySubMode.WAIT_FOR_LEVEL_COMPLETE && !Cameras.IsRecordingReplay() && (GameStateSim.m_LevelPassed || GameStateSim.m_LevelFailed) && !Cameras.m_AsyncCapture.Aysnc_CaptureStillHasWorkToDo())
			{
				GameUI.m_Instance.m_ShareReplay.Show();
				GetNumReplaysDumped(Game.GetLevelId());
				GameUI.m_Instance.m_ShareReplay.OnSaveLocal();
				m_DumpSubMode = DumpReplaySubMode.WAIT_FOR_REPLAY_COMPRESSION_COMPLETE;
			}
			else if (m_DumpSubMode == DumpReplaySubMode.WAIT_FOR_REPLAY_COMPRESSION_COMPLETE && !GameUI.m_Instance.m_ShareReplay.IsCompressing() && GameUI.m_Instance.m_ShareReplay.m_Status.m_OK.gameObject.activeInHierarchy)
			{
				GameUI.m_Instance.m_ShareReplay.m_Status.m_OKCallback?.Invoke();
				m_DumpSubMode = DumpReplaySubMode.REPLAY_READY;
				IncrementNumReplaysDumped(Game.GetLevelId());
			}
			else
			{
				if (m_DumpSubMode != DumpReplaySubMode.REPLAY_READY)
				{
					return;
				}
				CopyReplay(m_SlotsToDump[m_NextSlotToDumpIndex]);
				m_NextSlotToDumpIndex++;
				if (m_NextSlotToDumpIndex < m_SlotsToDump.Count)
				{
					GameStateSim.Exit(GameState.BUILD);
					GameStateManager.BashState(GameState.BUILD);
					m_DumpSubMode = DumpReplaySubMode.PRELOAD_LEVEL;
					string levelIDFromSlot = GetLevelIDFromSlot(m_SlotsToDump[m_NextSlotToDumpIndex]);
					CampaignLevel levelFromId = CampaignWorlds.m_Instance.GetLevelFromId(levelIDFromSlot);
					if ((bool)levelFromId)
					{
						CampaignWorlds.m_Instance.GetWorldWithLevelId(levelFromId.m_Id);
						GameStatePreloadingAssets.PreloadLevel(levelFromId.GetLayoutPath(), null, PreloadCallback);
					}
				}
				GameUI.m_Instance.m_ShareReplay.m_Status.Close();
			}
		}
	}

	public static void End()
	{
		if (m_RestoreBridgeSimulationSpeedIndex != -1)
		{
			BridgeSimSpeed.SetSimulationSpeedIndex(m_RestoreBridgeSimulationSpeedIndex);
		}
		GameStateManager.SwitchToState(GameState.MAIN_MENU);
		m_Dumping = false;
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

	private static string GetLevelIDFromSlot(string slot)
	{
		return slot.Substring(0, 3);
	}

	private static void CopyReplay(string slotFilename)
	{
		string fullPathOfLastCompressedMovie = GameUI.m_Instance.m_ShareReplay.GetFullPathOfLastCompressedMovie();
		string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(slotFilename);
		string text = Path.Combine(Application.streamingAssetsPath, "LevelSolutions", "Videos", fileNameWithoutExtension + ".webm");
		try
		{
			File.Copy(fullPathOfLastCompressedMovie, text, overwrite: true);
		}
		catch (Exception arg)
		{
			Debug.LogWarning($"Caught exception '{arg}' trying to copy replay from '{fullPathOfLastCompressedMovie}' to '{text}'");
		}
	}

	private static CampaignLevel GetNextValidLevel()
	{
		while (m_NextSlotToDumpIndex < m_SlotsToDump.Count)
		{
			string levelIDFromSlot = GetLevelIDFromSlot(m_SlotsToDump[m_NextSlotToDumpIndex]);
			CampaignLevel levelFromId = CampaignWorlds.m_Instance.GetLevelFromId(levelIDFromSlot);
			if (levelFromId != null)
			{
				return levelFromId;
			}
			m_NextSlotToDumpIndex++;
		}
		return null;
	}

	private static void PreloadCallback(string levelFilename, FileSlot slot)
	{
		m_DumpSubMode = DumpReplaySubMode.LOAD_LEVEL;
	}

	private static int GetNumReplaysDumped(string levelID)
	{
		if (m_NumReplaysDumped.ContainsKey(levelID))
		{
			return m_NumReplaysDumped[levelID];
		}
		return 0;
	}

	private static void IncrementNumReplaysDumped(string levelID)
	{
		if (m_NumReplaysDumped.ContainsKey(levelID))
		{
			m_NumReplaysDumped[levelID]++;
		}
		else
		{
			m_NumReplaysDumped.Add(levelID, 1);
		}
	}
}
