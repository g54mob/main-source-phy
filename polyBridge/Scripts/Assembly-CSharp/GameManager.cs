using System.Globalization;
using System.Threading;
using Poly.Physics;
using UnityEngine;

public class GameManager
{
	public static bool m_Offline;

	private static GameMode m_GameMode;

	private static GameSubMode m_GameSubMode;

	private static bool m_MusicStarted;

	private static readonly float MAX_SECONDS_FOR_POLY_TWITCH_STREAM_TO_STOP = 3f;

	private static bool m_QuitWhenPolyTwitchStreamStopped;

	private static float m_TimePolyTwitchStreamStopIssued;

	private static int m_RecenterVirtualCursorWhenZero;

	public static void AwakeManual()
	{
		Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
		m_GameMode = GameMode.CAMPAIGN;
		m_GameSubMode = GameSubMode.NONE;
		SteamManager.Init();
	}

	public static void StartManual()
	{
		ConsoleCommands.Init();
		GameResolutions.Init();
		SetDefaultQualityLevel();
		GameRenderSettings.Init();
		LeaderboardBuckets.Init();
		WeeklyChallenges.Download();
		GameStateManager.Init();
		GameToolMode.Init();
		WorldBounds.Init(GameSettings.WorldWidth(), GameSettings.WorldMinY(), GameSettings.WorldMaxY());
		CinemaCamera.Init();
		Bindings.Init();
		PreviewCache.Init();
		Campaign.Init();
		Workshop.LoadSubscribedItemsFromDisk();
		ModApi.LoadLanguageCSVMapping();
		GamepadManager.Init(GameUI.m_Instance.m_VirtualMouseUI);
		Profiles.AssignSlots();
		Profiles.LoadActiveProfile();
		if (Game.IsRunningOnSteamDeck())
		{
			GameResolutions.SetGameToHighestResolution();
			m_RecenterVirtualCursorWhenZero = 2;
		}
		GamepadRepeater.Init();
		GamepadVirtualKeyboard.Init();
		GameInput.Init();
		AudioMixerManager.SetMusicVolume(0f);
		PolyTwitch.Init(Profiles.m_ActiveProfile.m_TwitchAuthorPanelPos, Profiles.m_ActiveProfile.m_TwitchStreamerWindowPos, Profiles.m_ActiveProfile.m_TwitchStreamerWindowHeight, Profiles.m_ActiveProfile.m_TwitchStreamerWindowCollapsed);
		GameRichPresence.Init();
		GameAchievements.Init();
		Cameras.Init();
		Version.Init();
		GameGrid.Init();
		GameUI.Init();
		ClipboardManager.Init();
		Bridge.Init();
		BridgeEffects.Init();
		BridgeMaterials.Init();
		BridgeJointPlacement.Init();
		BridgeJointMovement.Init();
		BridgeJointSelectors.Init();
		BridgePillarDistanceMarkers.Init();
		BridgePillarPlacement.Init();
		BridgeTrace.Init(Profiles.m_ActiveProfile.m_ArcShape, Profiles.m_ActiveProfile.m_ArcSnapToGrid);
		BridgePreviewMaker.Init();
		BridgeCheat.Init();
		Sandbox.Init();
		SandboxItems.Init();
		Spline.Init();
		Budget.Init();
		CampaignWorlds.Init();
		Music.Init();
		WorkshopCaches.Init();
		WorkshopSubmit.Init();
		WorkshopItemVotes.Init();
		WorkshopItemFavorites.Init();
		CloudinaryManager.Init();
		CustomShapesLibrary.Init();
		SaveSlotImageMaker.Init();
		WaterLine.Init();
		Replays.Init();
		Gallery.Init();
		LeaderboardReplay.Init();
		SteamStats.Init();
		MaterialOverrides.Init();
		LogCurrentVersion();
		GameStateManager.SwitchToStateImmediate(GameState.MAIN_MENU);
		Mods.Init();
		ModApi.Init();
		ModsSource.Init();
		Workshop.Init();
	}

	public static void UpdateManual()
	{
		if (m_QuitWhenPolyTwitchStreamStopped && PolyTwitchStreamStopped())
		{
			GameUI.m_Instance.m_Status.gameObject.SetActive(value: false);
			m_QuitWhenPolyTwitchStreamStopped = false;
			QuitWithoutConfirmation();
		}
		if (!m_MusicStarted)
		{
			Music.Start();
			m_MusicStarted = true;
		}
		GameStateManager.UpdateManual();
		ModApi.RunOnUpdate();
		m_RecenterVirtualCursorWhenZero--;
		if (m_RecenterVirtualCursorWhenZero == 0 && GamepadManager.m_VirtualMouseUI != null)
		{
			GamepadManager.m_VirtualMouseUI.ResetMouseToCenter();
		}
	}

	public static void LateUpdateManual()
	{
		GameStateManager.LateUpdateManual();
	}

	public static void FixedUpdateManual()
	{
		GameStateManager.FixedUpdateManual();
		ModApi.RunOnFixedUpdate();
	}

	public static void SetGameMode(GameMode mode, GameSubMode submode)
	{
		m_GameMode = mode;
		m_GameSubMode = submode;
	}

	public static GameMode GetGameMode()
	{
		return m_GameMode;
	}

	public static GameSubMode GetGameSubMode()
	{
		return m_GameSubMode;
	}

	public static bool GameModeIsCampaignOrWorkshop()
	{
		if (m_GameMode != GameMode.CAMPAIGN)
		{
			return m_GameMode == GameMode.WORKSHOP;
		}
		return true;
	}

	public static bool CurrentLevelHasLeaderboards()
	{
		if (m_GameMode != GameMode.CAMPAIGN)
		{
			return WeeklyChallenges.IsAWeeklyChallenge(Game.GetLevelId());
		}
		return true;
	}

	public static void AutoSave(GameState nextState)
	{
		if (GameModeIsCampaignOrWorkshop() && !Mathf.Approximately(Budget.m_BridgeCost, 0f) && BridgeJoints.GetNumActiveNonAnchorJoints() != 0 && !DumpPreviewImages.m_Dumping && !DumpReplays.m_Dumping && !LayoutValidator.m_Validating && !LeaderboardReplay.IsActive() && (!PolyTwitch.m_StreamStarted || PolyTwitch.m_LastLoadedSuggestion == null || !(PolyTwitch.m_LastLoadedSuggestion.m_BridgeHash == PolyTwitch.m_BridgeHashForSimulation)) && (GetGameMode() != GameMode.CAMPAIGN || !Campaign.m_CurrentLevel.IsTutorial()) && GetGameSubMode() != GameSubMode.LEADERBOARD_REPLAY)
		{
			BridgeSaveSlots.SaveReserved(BridgeSaveSlots.GetDirectoryForSaveSlot(), ReservedSlot.AUTOSAVE, nextState);
		}
	}

	public static bool IsPaused()
	{
		return GameUI.m_Instance.m_PauseMenu.gameObject.activeInHierarchy;
	}

	public static int GetPhysicsEngineVersion()
	{
		return World.m_PhysicsEngineVersion;
	}

	public static void QuitWithoutConfirmation()
	{
		if (!m_QuitWhenPolyTwitchStreamStopped)
		{
			if (PolyTwitch.m_StreamStarted)
			{
				PolyTwitch.StopStreamSilent();
				m_QuitWhenPolyTwitchStreamStopped = true;
				m_TimePolyTwitchStreamStopIssued = Time.unscaledTime;
				GameUI.m_Instance.m_Status.Open(Localize.Get("UI_STATUS_STOPPING_TWITCH_STREAM"));
			}
			else
			{
				SteamRichPresence.Clear();
				Application.Quit();
			}
		}
	}

	public static bool IsSteamOffline()
	{
		if (Game.m_ForceOffline)
		{
			return true;
		}
		return !SteamManager.IsLoggedOn();
	}

	public static string GetSteamOfflineMessage()
	{
		return Localize.Get("UI_STEAM_OFFLINE");
	}

	public static bool IsSecretWorldUnlocked()
	{
		CampaignWorld[] worlds = CampaignWorlds.m_Instance.m_Worlds;
		foreach (CampaignWorld campaignWorld in worlds)
		{
			if (campaignWorld.m_NumStars != 5)
			{
				continue;
			}
			if (!Profiles.m_ActiveProfile.m_FiveStarUnlocks.Contains(campaignWorld.m_Id))
			{
				if (CampaignWorlds.m_Instance.CompletedAllLevelsAtStarLevel(1) && CampaignWorlds.m_Instance.CompletedAllLevelsAtStarLevel(2) && CampaignWorlds.m_Instance.CompletedAllLevelsAtStarLevel(3))
				{
					return CampaignWorlds.m_Instance.CompletedAllLevelsAtStarLevel(4);
				}
				return false;
			}
			return true;
		}
		return false;
	}

	private static void LogCurrentVersion()
	{
		if (!string.IsNullOrEmpty(GameUI.m_Instance.m_Version.text))
		{
			Debug.LogFormat($">>> GAME VERSION: {GameUI.m_Instance.m_Version.text}");
		}
	}

	private static void SetDefaultQualityLevel()
	{
		QualitySettings.SetQualityLevel(QualitySettings.names.Length - 1);
	}

	private static bool PolyTwitchStreamStopped()
	{
		if (Time.unscaledTime - m_TimePolyTwitchStreamStopIssued > MAX_SECONDS_FOR_POLY_TWITCH_STREAM_TO_STOP)
		{
			return true;
		}
		return !PolyTwitch.m_StreamStarted;
	}
}
