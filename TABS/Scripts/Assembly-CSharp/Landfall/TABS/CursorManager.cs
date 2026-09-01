using Landfall.TABS.GameMode;
using Landfall.TABS.GameState;
using Landfall.TABS.Services;
using TFBGames;
using UnityEngine;

namespace Landfall.TABS
{
	public class CursorManager : MonoBehaviour
	{
		private GameStateManager m_gameStateManager;

		private SandboxPlacementCamera m_unitPlacementCamera;

		private GameModeService m_gameModeService;

		private ModalPanel m_modalPanel;

		private CursorVisibilityController m_cursorVisibility;

		private ITimeService m_timeService;

		private void Awake()
		{
			m_gameStateManager = ServiceLocator.GetService<GameStateManager>();
		}

		private void Start()
		{
			m_unitPlacementCamera = Object.FindObjectOfType<SandboxPlacementCamera>();
			m_modalPanel = ServiceLocator.GetService<ModalPanel>();
			m_cursorVisibility = ServiceLocator.GetService<CursorVisibilityController>();
			m_timeService = ServiceLocator.GetService<ITimeService>();
			m_gameModeService = ServiceLocator.GetService<GameModeService>();
		}

		private void Update()
		{
			if (m_gameStateManager.GameState != Landfall.TABS.GameState.GameState.BattleState && !m_unitPlacementCamera.IsInFreeLook() && !m_unitPlacementCamera.IsReturning && !m_unitPlacementCamera.IsResettingRotation)
			{
				m_cursorVisibility.SetLockStateAndVisibility(CursorLockMode.None, visible: true);
			}
			else if (m_gameModeService.CurrentGameMode.IsMenuOpen() || m_modalPanel.PauseGame || m_modalPanel.IsPopupOpen || m_timeService.IsPaused())
			{
				m_cursorVisibility.SetLockStateAndVisibility(CursorLockMode.None, visible: true);
			}
			else
			{
				m_cursorVisibility.SetLockStateAndVisibility(CursorLockMode.Locked, visible: false);
			}
		}
	}
}
