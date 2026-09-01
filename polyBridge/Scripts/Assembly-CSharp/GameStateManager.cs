using UnityEngine;

public class GameStateManager
{
	private static GameState m_PrevGameState;

	private static GameState m_GameState;

	private static GameState m_PendingGameState;

	public static void Init()
	{
		m_PendingGameState = GameState.INVALID;
		m_PrevGameState = GameState.INVALID;
		m_GameState = GameState.INVALID;
	}

	public static void BashState(GameState state)
	{
		m_GameState = state;
	}

	public static void SwitchToState(GameState state)
	{
		m_PendingGameState = state;
	}

	public static void SwitchToStateImmediate(GameState state)
	{
		m_PendingGameState = GameState.INVALID;
		ChangeState(state);
	}

	public static GameState GetState()
	{
		return m_GameState;
	}

	public static GameState GetPendingState()
	{
		return m_PendingGameState;
	}

	public static GameState GetPrevState()
	{
		return m_PrevGameState;
	}

	public static void UpdateManual()
	{
		if (m_PendingGameState != GameState.INVALID)
		{
			if (m_GameState == GameState.SIM)
			{
				PointsOfView.PanPivot(Cameras.MainCamera().ScreenToWorldPoint(GameInput.GetMousePosition()), force: true);
			}
			ChangeState(m_PendingGameState);
			m_PendingGameState = GameState.INVALID;
		}
		AudioVolume.UpdateManual();
		DebugInput.UpdateManual();
		GameScreenshot.UpdateManual();
		if (GameInput.GetActiveGameDevice() == GameDevice.Gamepad)
		{
			GamepadRepeater.UpdateManual();
		}
		else
		{
			KeyboardRepeater.UpdateManual();
		}
		BridgePreviewMaker.UpdateManual();
		PolyTwitch.UpdateManual();
		PolyTwitchAutoPlay.UpdateManual();
		GalleryPreviewRequests.UpdateManual();
		SteamManager.UpdateManual();
		WeeklyChallenges.UpdateManual();
		if (GetState() == GameState.SANDBOX || GetState() == GameState.BUILD)
		{
			GameUI.m_Instance.m_Recenter.UpdateManual();
		}
		if (GameManager.GetGameMode() == GameMode.CAMPAIGN)
		{
			Campaign.UpdateManual();
		}
		if (m_GameState == GameState.MAIN_MENU || m_GameState == GameState.BUILD || m_GameState == GameState.SIM || m_GameState == GameState.SANDBOX)
		{
			GameRichPresence.UpdateManual();
		}
		bool flag = Profiles.m_ActiveProfile.m_Replays && !Game.IsCurrentLevelTutorial() && Cameras.m_AsyncCapture.m_Initialized && m_GameState == GameState.SIM;
		if (flag != Cameras.m_Instance.m_Replay.gameObject.activeInHierarchy)
		{
			Cameras.m_Instance.m_Replay.gameObject.SetActive(flag);
		}
		switch (m_GameState)
		{
		case GameState.MAIN_MENU:
			GameStateMainMenu.UpdateManual();
			break;
		case GameState.BUILD:
			GameStateBuild.UpdateManual();
			break;
		case GameState.SIM:
			GameStateSim.UpdateManual();
			break;
		case GameState.SANDBOX:
			GameStateSandbox.UpdateManual();
			break;
		case GameState.PRELOADING_LAYOUT_ASSETS:
			GameStatePreloadingAssets.UpdateManual();
			break;
		case GameState.DECOR:
			GameStateDecor.UpdateManual();
			break;
		case GameState.PHOTO:
			GameStatePhoto.UpdateManual();
			break;
		default:
			Debug.LogErrorFormat("Unsupported game state: {0}", m_GameState.ToString());
			break;
		case GameState.LOADING_LEVEL_IMMEDIATE:
			break;
		}
	}

	public static void LateUpdateManual()
	{
		PulseIcons.UpdateManual();
		switch (m_GameState)
		{
		case GameState.MAIN_MENU:
			GameStateMainMenu.LateUpdateManual();
			break;
		case GameState.BUILD:
			GameStateBuild.LateUpdateManual();
			break;
		case GameState.SIM:
			GameStateSim.LateUpdateManual();
			break;
		case GameState.SANDBOX:
			GameStateSandbox.LateUpdateManual();
			break;
		case GameState.DECOR:
			GameStateDecor.LateUpdateManual();
			break;
		case GameState.PHOTO:
			GameStatePhoto.LateUpdateManual();
			break;
		default:
			Debug.LogErrorFormat("Unsupported game state: {0}", m_GameState.ToString());
			break;
		case GameState.LOADING_LEVEL_IMMEDIATE:
		case GameState.PRELOADING_LAYOUT_ASSETS:
			break;
		}
	}

	public static void FixedUpdateManual()
	{
		switch (m_GameState)
		{
		case GameState.MAIN_MENU:
			GameStateMainMenu.FixedUpdateManual();
			break;
		case GameState.BUILD:
			GameStateBuild.FixedUpdateManual();
			break;
		case GameState.SIM:
			GameStateSim.FixedUpdateManual();
			break;
		case GameState.SANDBOX:
			GameStateSandbox.FixedUpdateManual();
			break;
		case GameState.DECOR:
			GameStateDecor.FixedUpdateManual();
			break;
		case GameState.PHOTO:
			GameStatePhoto.FixedUpdateManual();
			break;
		default:
			Debug.LogErrorFormat("Unsupported game state: {0}", m_GameState.ToString());
			break;
		case GameState.LOADING_LEVEL_IMMEDIATE:
		case GameState.PRELOADING_LAYOUT_ASSETS:
			break;
		}
	}

	public static bool AllowedToLoad()
	{
		if (GetState() != GameState.BUILD)
		{
			return GetState() == GameState.SANDBOX;
		}
		return true;
	}

	public static bool AllowedToSave()
	{
		if (GetState() != GameState.BUILD)
		{
			return GetState() == GameState.SANDBOX;
		}
		return true;
	}

	public static PointOfViewType GetDefaultPointOfViewType()
	{
		switch (m_GameState)
		{
		case GameState.LOADING_LEVEL_IMMEDIATE:
		case GameState.BUILD:
		case GameState.PRELOADING_LAYOUT_ASSETS:
			return PointOfViewType.BUILD;
		case GameState.SIM:
			if (!Profiles.m_ActiveProfile.m_LockBuildCamera)
			{
				return Profiles.m_ActiveProfile.m_PointOfViewType;
			}
			return PointOfViewType.SIM_CENTER;
		case GameState.MAIN_MENU:
		case GameState.DECOR:
			return Profiles.m_ActiveProfile.m_PointOfViewType;
		case GameState.SANDBOX:
			return PointOfViewType.BUILD;
		case GameState.PHOTO:
			return PointOfViewType.SIM_LEFT;
		default:
			Debug.LogErrorFormat("Unsupported game state: {0}", m_GameState.ToString());
			return PointOfViewType.BUILD;
		}
	}

	public static void OnLayoutLoaded()
	{
		GameStateBuild.OnLayoutLoaded();
		GameStateSim.OnLayoutLoaded();
		GameStateSandbox.OnLayoutLoaded();
		GameStateDecor.OnLayoutLoaded();
	}

	public static bool AllowedToPanCameraWithMouse()
	{
		switch (m_GameState)
		{
		case GameState.LOADING_LEVEL_IMMEDIATE:
		case GameState.MAIN_MENU:
		case GameState.PRELOADING_LAYOUT_ASSETS:
			return false;
		case GameState.BUILD:
			return GameStateBuild.AllowedToPanCameraWithMouse();
		case GameState.SANDBOX:
		case GameState.DECOR:
			return GameStateSandbox.AllowedToPanCameraWithMouse();
		case GameState.SIM:
		case GameState.PHOTO:
			return GameInput.GetActiveGameDevice() == GameDevice.KeyboardAndMouse;
		default:
			Debug.LogErrorFormat("Unsupported game state: {0}", m_GameState.ToString());
			return false;
		}
	}

	private static void ChangeState(GameState state)
	{
		if (m_GameState != state)
		{
			ExitState(m_GameState, state);
			m_PrevGameState = m_GameState;
			m_GameState = state;
			EnterState(m_GameState, m_PrevGameState);
		}
	}

	private static void EnterState(GameState newState, GameState prevState)
	{
		switch (newState)
		{
		case GameState.MAIN_MENU:
			GameStateMainMenu.Enter(prevState);
			break;
		case GameState.BUILD:
			GameStateBuild.Enter(prevState);
			break;
		case GameState.SIM:
			GameStateSim.Enter(prevState);
			break;
		case GameState.SANDBOX:
			GameStateSandbox.Enter(prevState);
			break;
		case GameState.DECOR:
			GameStateDecor.Enter(prevState);
			break;
		case GameState.PHOTO:
			GameStatePhoto.Enter(prevState);
			break;
		default:
			Debug.LogErrorFormat("Unsupported game state: {0}", m_GameState.ToString());
			break;
		case GameState.LOADING_LEVEL_IMMEDIATE:
		case GameState.PRELOADING_LAYOUT_ASSETS:
			break;
		}
	}

	private static void ExitState(GameState currentState, GameState nextState)
	{
		switch (currentState)
		{
		case GameState.MAIN_MENU:
			GameStateMainMenu.Exit(nextState);
			break;
		case GameState.BUILD:
			GameStateBuild.Exit(nextState);
			break;
		case GameState.SIM:
			GameStateSim.Exit(nextState);
			break;
		case GameState.SANDBOX:
			GameStateSandbox.Exit(nextState);
			break;
		case GameState.DECOR:
			GameStateDecor.Exit(nextState);
			break;
		case GameState.PHOTO:
			GameStatePhoto.Exit(nextState);
			break;
		default:
			Debug.LogErrorFormat("Unsupported game state: {0}", m_GameState.ToString());
			break;
		case GameState.INVALID:
		case GameState.LOADING_LEVEL_IMMEDIATE:
		case GameState.PRELOADING_LAYOUT_ASSETS:
			break;
		}
	}
}
