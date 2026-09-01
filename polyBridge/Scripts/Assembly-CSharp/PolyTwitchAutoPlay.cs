using UnityEngine;

public class PolyTwitchAutoPlay
{
	public static bool m_Running;

	public static PolyTwitchSuggestion m_FirstSuggestion;

	public static bool m_CycleToNextLevel;

	public static bool m_SimStartedAutomatically;

	private static float m_SecondsBeforeStuckRetry;

	private static bool m_StuckRetryCountdownStarted;

	private static readonly float NUM_SECONDS_BEFORE_RETRY = 5f;

	private static float m_SecondsUntilNext;

	private static bool m_CycleCountdownStarted;

	private static readonly float CYCLE_DELAY_SECONDS = 5f;

	private static float m_SecondsSpentWaitingForNext;

	private static PolyTwitchSuggestionSlot m_CurrentSlot;

	private static int m_Count;

	public static void EnterSimulation()
	{
		m_CycleToNextLevel = false;
		m_CycleCountdownStarted = false;
		m_Count = 1;
	}

	public static void TurnOff()
	{
		GameUI.m_Instance.m_PolyTwitchMain.m_SettingsPanel.m_AutoPlayToggle.isOn = false;
		Profiles.m_ActiveProfile.m_TwitchAutoPlay = false;
		Stop();
		Profiles.SaveActiveProfile();
	}

	public static void UpdateManual()
	{
		if (!m_Running)
		{
			return;
		}
		if (!Profiles.m_ActiveProfile.m_TwitchAutoPlay)
		{
			Stop();
			if (GameStateManager.GetState() == GameState.SIM)
			{
				GameUI.m_Instance.m_TopBar.OnExitSim();
			}
			return;
		}
		if (m_StuckRetryCountdownStarted && GameUI.m_Instance.m_PopUpTwoChoices.gameObject.activeInHierarchy)
		{
			m_SecondsBeforeStuckRetry -= Time.unscaledDeltaTime;
			GameUI.m_Instance.m_PopUpTwoChoices.m_ChoiceAText.text = string.Format("{0} {1}", Localize.Get("POPUP_STUCK_POLYTWITCH_SKIP"), Mathf.Max(1f, Mathf.CeilToInt(m_SecondsBeforeStuckRetry)));
			if (m_SecondsBeforeStuckRetry < 0f)
			{
				GameUI.m_Instance.m_PolyTwitchMain.m_AutoPlayPanel.SkipToNextBridge();
				GameUI.m_Instance.m_PopUpTwoChoices.Close();
				m_StuckRetryCountdownStarted = false;
			}
		}
		if ((GameStateSim.m_LevelPassed || GameStateSim.m_LevelFailed || (GameStateSim.m_LevelHung && !m_StuckRetryCountdownStarted)) && !m_CycleCountdownStarted)
		{
			if (PolyTwitchSuggestions.GetNextAutoplayThatHasNotBeenSimulated(m_CurrentSlot.m_Suggestion) != null)
			{
				m_SecondsUntilNext = CYCLE_DELAY_SECONDS - m_SecondsSpentWaitingForNext;
				m_CycleCountdownStarted = true;
				m_CycleToNextLevel = false;
				m_SecondsSpentWaitingForNext = 0f;
			}
			else
			{
				m_SecondsSpentWaitingForNext += Time.unscaledDeltaTime;
			}
			if (GameStateSim.m_LevelPassed && Profiles.m_ActiveProfile.m_TwitchAutoAdvance && CanAdvanceToNextLevel())
			{
				m_SecondsUntilNext = CYCLE_DELAY_SECONDS;
				m_CycleCountdownStarted = true;
				m_CycleToNextLevel = true;
				m_SecondsSpentWaitingForNext = 0f;
			}
		}
		if (m_CycleCountdownStarted)
		{
			m_SecondsUntilNext -= Time.unscaledDeltaTime;
			if (m_SecondsUntilNext < 0f)
			{
				GameUI.m_Instance.m_PolyTwitchMain.m_AutoPlayPanel.SkipToNextBridge();
				m_CycleCountdownStarted = false;
			}
		}
	}

	public static void SetCurrentSlot(PolyTwitchSuggestionSlot slot)
	{
		m_CurrentSlot = slot;
	}

	public static string GetTitleText()
	{
		if (m_CurrentSlot == null)
		{
			return string.Empty;
		}
		if (m_CycleCountdownStarted)
		{
			return string.Format(m_CycleToNextLevel ? Localize.Get("UI_POLYTWITCH_AUTOPLAY_NEXT_LEVEL") : Localize.Get("UI_POLYTWITCH_AUTOPLAY_NEXT"), Mathf.Clamp(Mathf.CeilToInt(m_SecondsUntilNext), 1, (int)CYCLE_DELAY_SECONDS));
		}
		int num = m_Count + PolyTwitchSuggestions.GetNumberOfAutoplaySuggestionsFollowing(m_CurrentSlot.m_Suggestion);
		return string.Format(Localize.Get("UI_POLYTWITCH_AUTOPLAY"), m_Count, num);
	}

	public static void StartStuckRetryCountdown()
	{
		m_SecondsBeforeStuckRetry = NUM_SECONDS_BEFORE_RETRY;
		m_StuckRetryCountdownStarted = true;
	}

	public static void MaybeLoadForSimulation()
	{
		if (m_Running && m_FirstSuggestion != null)
		{
			Bridge.ClearAndLoad(m_FirstSuggestion.m_BridgeSaveData);
			Budget.UpdateBridgeCost();
			GameUI.m_Instance.m_TopBar.UpdateManual();
			PolyTwitch.m_LastLoadedSuggestion = m_FirstSuggestion;
			PolyTwitch.m_BridgeHashForSimulation = m_FirstSuggestion.m_BridgeHash;
			if (!m_FirstSuggestion.HasPassedOrFailed())
			{
				m_FirstSuggestion.SetStatus(PolyTwitchSuggestionStatus.SIMULATED);
			}
		}
		else
		{
			PolyTwitch.m_BridgeHashForSimulation = Utils.MD5HashFor(BridgeSave.Serialize().SerializeBinary());
		}
	}

	public static void Start(PolyTwitchSuggestion firstSuggestion)
	{
		m_Running = true;
		m_FirstSuggestion = firstSuggestion;
		SetCurrentSlot(firstSuggestion.m_Slot);
	}

	public static void Stop()
	{
		if (!Game.m_TakingScreenshotForAutoSave)
		{
			m_Running = false;
			m_FirstSuggestion = null;
			m_SimStartedAutomatically = false;
		}
	}

	public static void MuteCurrentSlot()
	{
		if ((bool)m_CurrentSlot)
		{
			PolyTwitchBans.BanPlayer(m_CurrentSlot.m_Suggestion.m_Username, m_CurrentSlot.m_Suggestion.m_OwnerId);
			MoveToNextSuggestion();
		}
	}

	public static void MoveToNextLevel()
	{
		Stop();
		GameStateManager.SwitchToState(GameState.BUILD);
		GameStateBuild.m_LoadNextLevelOnEnter = true;
	}

	public static void MoveToNextSuggestion()
	{
		m_CycleCountdownStarted = false;
		if ((bool)m_CurrentSlot)
		{
			PolyTwitchSuggestion nextAutoplayThatHasNotBeenSimulated = PolyTwitchSuggestions.GetNextAutoplayThatHasNotBeenSimulated(m_CurrentSlot.m_Suggestion);
			if (nextAutoplayThatHasNotBeenSimulated != null)
			{
				int count = m_Count;
				GameStateManager.SwitchToStateImmediate(GameState.BUILD);
				Bridge.ClearAndLoad(nextAutoplayThatHasNotBeenSimulated.m_BridgeSaveData);
				Budget.UpdateBridgeCost();
				GameUI.m_Instance.m_TopBar.UpdateManual();
				Start(nextAutoplayThatHasNotBeenSimulated);
				GameStateManager.SwitchToStateImmediate(GameState.SIM);
				PointOfView pointOfView = PointsOfView.GetPointOfView(PointOfViewType.BUILD);
				PointsOfView.Set(PointOfViewType.BUILD_CUSTOM, pointOfView.m_Pivot, pointOfView.m_Pos, pointOfView.m_Rot, pointOfView.m_OrthographicsSize);
				m_CurrentSlot = nextAutoplayThatHasNotBeenSimulated.m_Slot;
				m_Count = count + 1;
			}
		}
	}

	private static bool CanAdvanceToNextLevel()
	{
		if (GameManager.GetGameMode() == GameMode.CAMPAIGN && Campaign.GetNextLayoutFilename() != string.Empty)
		{
			return true;
		}
		return false;
	}
}
