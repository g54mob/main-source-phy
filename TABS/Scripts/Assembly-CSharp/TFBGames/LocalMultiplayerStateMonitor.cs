using System;
using GamepadUI.StateManager.Core;
using Landfall.TABS.GameMode;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TFBGames
{
	public class LocalMultiplayerStateMonitor : IService
	{
		private LocalMultiplayerState currentLocalMultiplayerState;

		private GameModeService gameModeService;

		private UISubMenu localMultiplayerUIHandler;

		private UISubMenu multiplayerSettingsMenu;

		public LocalMultiplayerState CurrentLocalMultiplayerState => currentLocalMultiplayerState;

		public event Action<LocalMultiplayerState> StateChanged;

		public void OnAwake()
		{
			SetState(LocalMultiplayerState.Initialising);
		}

		public void OnStart()
		{
			SceneManager.sceneLoaded += OnSceneLoaded;
			gameModeService = ServiceLocator.GetService<GameModeService>();
			if (gameModeService == null)
			{
				Debug.LogError("gameModeService is null.");
			}
			else
			{
				gameModeService.GameModeSet += OnGameModeSet;
			}
		}

		public void UnRegister()
		{
			SceneManager.sceneLoaded -= OnSceneLoaded;
			if (gameModeService != null)
			{
				gameModeService.GameModeSet -= OnGameModeSet;
			}
		}

		private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
		{
			if (loadSceneMode != LoadSceneMode.Single || !TABSSceneManager.IsInMainMenuScene())
			{
				return;
			}
			SetState(LocalMultiplayerState.OutOfLocalMultiplayer);
			localMultiplayerUIHandler = ProjectMarsHandler.Instance.LocalMultiplayerUIHandler;
			if (localMultiplayerUIHandler == null)
			{
				Debug.LogError("localMultiplayerUIHandler was not found");
				return;
			}
			localMultiplayerUIHandler.MenuOpened += OnLocalMultiplayerMenuOpened;
			localMultiplayerUIHandler.MenuClosed += OnLocalMultiplayerMenuClosed;
			localMultiplayerUIHandler.MenuDestroyed += OnLocalMultiplayerMenuDestroyed;
			multiplayerSettingsMenu = ProjectMarsHandler.Instance.MultiplayerSettingsMenu;
			if (multiplayerSettingsMenu == null)
			{
				Debug.LogError("multiplayerSettingsMenu was not found");
				return;
			}
			multiplayerSettingsMenu.MenuOpened += OnMultiplayerSettingsMenuOpened;
			multiplayerSettingsMenu.MenuClosed += OnMultiplayerSettingsMenuClosed;
			multiplayerSettingsMenu.MenuDestroyed += OnMultiplayerSettingsMenuDestroyed;
		}

		private void SetState(LocalMultiplayerState state)
		{
			LocalMultiplayerState localMultiplayerState = currentLocalMultiplayerState;
			currentLocalMultiplayerState = state;
			if (currentLocalMultiplayerState != localMultiplayerState)
			{
				this.StateChanged?.Invoke(state);
			}
		}

		private void OnLocalMultiplayerMenuOpened(IMenuWithEvents menuWithEvents)
		{
			SetState(LocalMultiplayerState.InLocalMultiplayerMenu);
		}

		private void OnLocalMultiplayerMenuClosed(IMenuWithEvents menuWithEvents)
		{
			SetState(LocalMultiplayerState.OutOfLocalMultiplayer);
		}

		private void OnLocalMultiplayerMenuDestroyed()
		{
			if (!(localMultiplayerUIHandler == null))
			{
				localMultiplayerUIHandler.MenuOpened -= OnLocalMultiplayerMenuOpened;
				localMultiplayerUIHandler.MenuClosed -= OnLocalMultiplayerMenuClosed;
				localMultiplayerUIHandler.MenuDestroyed -= OnLocalMultiplayerMenuDestroyed;
				localMultiplayerUIHandler = null;
			}
		}

		private void OnMultiplayerSettingsMenuOpened(IMenuWithEvents menuWithEvents)
		{
			SetState(LocalMultiplayerState.InMultiplayerSettingsMenu);
		}

		private void OnMultiplayerSettingsMenuClosed(IMenuWithEvents menuWithEvents)
		{
			SetState(LocalMultiplayerState.OutOfLocalMultiplayer);
		}

		private void OnMultiplayerSettingsMenuDestroyed()
		{
			if (!(multiplayerSettingsMenu == null))
			{
				multiplayerSettingsMenu.MenuOpened -= OnMultiplayerSettingsMenuOpened;
				multiplayerSettingsMenu.MenuClosed -= OnMultiplayerSettingsMenuClosed;
				multiplayerSettingsMenu.MenuDestroyed -= OnMultiplayerSettingsMenuDestroyed;
				multiplayerSettingsMenu = null;
			}
		}

		private void OnGameModeSet(BaseGameMode gamemode)
		{
			if (gamemode is LocalMultiplayerGameMode)
			{
				SetState(LocalMultiplayerState.InLocalMultiplayerMatch);
			}
			else
			{
				SetState(LocalMultiplayerState.OutOfLocalMultiplayer);
			}
		}

		public void OnRegister()
		{
		}

		public void OnUpdate()
		{
		}

		public void OnFixedUpdate()
		{
		}

		public void OnLateUpdate()
		{
		}
	}
}
