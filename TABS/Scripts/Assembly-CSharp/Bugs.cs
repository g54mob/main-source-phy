using System;
using Landfall.TABS.GameMode;
using Landfall.TABS.GameState;
using TFBGames;
using UnityEngine;

public class Bugs : GameStateListener
{
	public static bool gravityBug = false;

	private static bool m_dlcActivated = true;

	[SerializeField]
	private GameObject m_debugToolsObject;

	[SerializeField]
	private GameObject m_bugBlackHole;

	[SerializeField]
	private GameObject m_bugSpin;

	[SerializeField]
	private GameObject m_bugWings;

	private bool m_allowDebugTools;

	private bool m_enableDebugTools;

	private IDlcManagerService m_dlcManager;

	private bool m_subscribedToEvents;

	private GameModeService m_gameModeService;

	private bool isRestrictedInGameMode;

	public static bool _DLC_ACTIVATED
	{
		get
		{
			if (EnabledOnPlatform())
			{
				return m_dlcActivated;
			}
			return false;
		}
		set
		{
			m_dlcActivated = EnabledOnPlatform() && value;
		}
	}

	public static bool EnabledOnPlatform()
	{
		return true;
	}

	private void Start()
	{
		m_gameModeService = ServiceLocator.GetService<GameModeService>();
		isRestrictedInGameMode = m_gameModeService.IsGameModeRestricted();
		if (!EnabledOnPlatform())
		{
			_DLC_ACTIVATED = false;
		}
		else if ((!(m_gameModeService != null) || !m_gameModeService.IsCurrentBaseGameModeType<OnlineMultiplayerGameMode>()) && (!(m_gameModeService != null) || !m_gameModeService.IsCurrentBaseGameModeType<LocalMultiplayerGameMode>()))
		{
			m_dlcManager = ServiceLocator.GetService<IDlcManagerService>();
			if (m_dlcManager != null)
			{
				m_dlcManager.GotAccessToDlc += OnGotAccessToDlc;
				m_dlcManager.LostAccessToAllDlc += OnLostAccessToAllDlc;
			}
			if (_DLC_ACTIVATED && !isRestrictedInGameMode)
			{
				SubscribeToEvents();
			}
		}
	}

	protected override void OnDestroy()
	{
		if (m_dlcManager != null)
		{
			m_dlcManager.GotAccessToDlc -= OnGotAccessToDlc;
			m_dlcManager.LostAccessToAllDlc -= OnLostAccessToAllDlc;
		}
		if (m_subscribedToEvents)
		{
			UnsubscribeFromEvents();
		}
		base.OnDestroy();
	}

	private void SubscribeToEvents()
	{
		if (!m_subscribedToEvents)
		{
			m_subscribedToEvents = true;
			GlobalSettingsHandler service = ServiceLocator.GetService<GlobalSettingsHandler>();
			if (!(service == null))
			{
				SettingsInstance settingsInstance = service.GetSettingsInstance("BUG_DEBUG");
				settingsInstance.OnValueChanged += OnDebugTools;
				OnDebugTools(settingsInstance.currentValue);
				settingsInstance = service.GetSettingsInstance("BUG_BLACKHOLE");
				settingsInstance.OnValueChanged += OnBugBlackHole;
				OnBugBlackHole(settingsInstance.currentValue);
				settingsInstance = service.GetSettingsInstance("BUG_SPIN");
				settingsInstance.OnValueChanged += OnBugSpin;
				OnBugSpin(settingsInstance.currentValue);
				settingsInstance = service.GetSettingsInstance("BUG_WINGS");
				settingsInstance.OnValueChanged += OnBugWings;
				OnBugWings(settingsInstance.currentValue);
			}
		}
	}

	private void UnsubscribeFromEvents()
	{
		if (m_subscribedToEvents)
		{
			m_subscribedToEvents = false;
			GlobalSettingsHandler service = ServiceLocator.GetService<GlobalSettingsHandler>();
			if (!(service == null))
			{
				service.GetSettingsInstance("BUG_DEBUG").OnValueChanged -= OnDebugTools;
				service.GetSettingsInstance("BUG_BLACKHOLE").OnValueChanged -= OnBugBlackHole;
				service.GetSettingsInstance("BUG_SPIN").OnValueChanged -= OnBugSpin;
				service.GetSettingsInstance("BUG_WINGS").OnValueChanged -= OnBugWings;
			}
		}
	}

	private void OnGotAccessToDlc(string dlcId)
	{
		if (m_dlcManager != null && string.Compare(dlcId, m_dlcManager.AprilFoolsBugsDlcId, StringComparison.InvariantCultureIgnoreCase) == 0)
		{
			_DLC_ACTIVATED = true;
			if (!isRestrictedInGameMode)
			{
				SubscribeToEvents();
			}
		}
	}

	private void OnLostAccessToAllDlc()
	{
		_DLC_ACTIVATED = false;
		UnsubscribeFromEvents();
	}

	private void OnDebugTools(int value)
	{
		m_enableDebugTools = value == 1;
		UpdateDebugTools();
	}

	private void OnBugBlackHole(int value)
	{
		m_bugBlackHole.SetActive(value == 1);
	}

	private void OnBugSpin(int value)
	{
		m_bugSpin.SetActive(value == 1);
	}

	private void OnBugWings(int value)
	{
		m_bugWings.SetActive(value == 1);
	}

	private void UpdateDebugTools()
	{
		m_debugToolsObject.SetActive(m_allowDebugTools && m_enableDebugTools);
	}

	public override void OnEnterPlacementState()
	{
		m_allowDebugTools = false;
		UpdateDebugTools();
	}

	public override void OnEnterBattleState()
	{
		m_allowDebugTools = true;
		UpdateDebugTools();
	}
}
