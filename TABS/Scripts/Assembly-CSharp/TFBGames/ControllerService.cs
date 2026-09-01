using System;
using System.Collections.Generic;
using InControl;
using Landfall.TABS.GameMode;
using Landfall.TABS.GameState;
using Landfall.TABS_Input;
using UnityEngine;

namespace TFBGames
{
	public class ControllerService : ServicePrefab
	{
		private const float MaxShowPopUpDelay = 1f;

		[SerializeField]
		private GameObject canvasPopUpUiPrefab;

		private GameObject controllerPopUpUI;

		private CodeAnimation codeAnimation;

		private SettingsProfileManager _settingsProfileManager;

		private PlayerActions playerActions;

		private bool popUpIsOnScreen;

		private InputService inputService;

		private InputService.InputState inputControlStateBeforePopup;

		private UINavigationGroupManager navigationGroupManager;

		private LocalMultiplayerStateMonitor localMultiplayerStateMonitor;

		private float? showPopUpDelay;

		private int minimumNumberOfControllers = 1;

		private GameStateManager gameStateManager;

		private GameModeService gameModeService;

		private InputDevice lastDeviceToHaveInput;

		private bool waitingToReturnToMainMenu;

		public bool IsControllerDisconnectedPopupOnScreen => popUpIsOnScreen;

		public event Action RequestEndMatch;

		public override void OnStart()
		{
			_settingsProfileManager = ServiceLocator.GetService<SettingsProfileManager>();
			inputService = ServiceLocator.GetService<InputService>();
			localMultiplayerStateMonitor = ServiceLocator.GetService<LocalMultiplayerStateMonitor>();
			navigationGroupManager = UnityEngine.Object.FindObjectOfType<UINavigationGroupManager>();
			playerActions = PlayerActions.Instance;
			gameStateManager = ServiceLocator.GetService<GameStateManager>();
			gameModeService = ServiceLocator.GetService<GameModeService>();
			if (!(_settingsProfileManager?.CurrentSettingsProfile != null) || !_settingsProfileManager.CurrentSettingsProfile.EnforceControllerConnection || GlobalSettingsHandler.CurrentPlatform == SettingsInstance.Platform.Switch)
			{
				return;
			}
			if (localMultiplayerStateMonitor != null)
			{
				localMultiplayerStateMonitor.StateChanged += OnLocalMultiplayerSateChanged;
			}
			controllerPopUpUI = UnityEngine.Object.Instantiate(canvasPopUpUiPrefab);
			if (controllerPopUpUI != null)
			{
				controllerPopUpUI.SetActive(value: false);
				codeAnimation = controllerPopUpUI.GetComponentInChildren<CodeAnimation>();
				if (codeAnimation != null)
				{
					codeAnimation.AnimationComplete += OnAnimationComplete;
				}
				popUpIsOnScreen = false;
			}
		}

		private void OnDestroy()
		{
			if (codeAnimation != null)
			{
				codeAnimation.AnimationComplete -= OnAnimationComplete;
			}
		}

		public override void OnUpdate()
		{
			if (controllerPopUpUI == null || localMultiplayerStateMonitor == null || localMultiplayerStateMonitor.CurrentLocalMultiplayerState == LocalMultiplayerState.Initialising)
			{
				return;
			}
			CheckNumberOfDevices();
			if (showPopUpDelay.HasValue)
			{
				showPopUpDelay -= Time.unscaledDeltaTime;
				if (showPopUpDelay.Value <= 0f)
				{
					if (gameStateManager.GameState == GameState.BattleState && !gameModeService.CurrentGameMode.MatchOver)
					{
						this.RequestEndMatch?.Invoke();
					}
					PutUIOnScreen();
				}
			}
			InputDevice activeDevice = InputManager.ActiveDevice;
			bool anyKeyIsPressed = InputManager.AnyKeyIsPressed;
			bool flag = activeDevice.AnyButtonIsPressed || activeDevice.CommandIsPressed;
			if (anyKeyIsPressed || flag || (bool)playerActions.m_anybuttonpressed || (bool)playerActions.m_uiNavigation)
			{
				lastDeviceToHaveInput = activeDevice;
			}
			if (popUpIsOnScreen && (anyKeyIsPressed || flag || (bool)playerActions.m_anybuttonpressed || (bool)playerActions.m_uiNavigation))
			{
				if (localMultiplayerStateMonitor.CurrentLocalMultiplayerState == LocalMultiplayerState.InLocalMultiplayerMatch)
				{
					waitingToReturnToMainMenu = true;
				}
				RemoveUIFromScreen();
			}
		}

		public override void UnRegister()
		{
			if (_settingsProfileManager?.CurrentSettingsProfile != null && _settingsProfileManager.CurrentSettingsProfile.EnforceControllerConnection && localMultiplayerStateMonitor != null)
			{
				localMultiplayerStateMonitor.StateChanged -= OnLocalMultiplayerSateChanged;
			}
		}

		private void OnAnimationComplete(CodeAnimationInstance.AnimationUse animationState)
		{
			if (animationState == CodeAnimationInstance.AnimationUse.Out && waitingToReturnToMainMenu)
			{
				waitingToReturnToMainMenu = false;
				inputService.ClearInputsForMainMenu();
				ReturnToMainMenu();
			}
		}

		private void OnLocalMultiplayerSateChanged(LocalMultiplayerState state)
		{
			minimumNumberOfControllers = ((state == LocalMultiplayerState.OutOfLocalMultiplayer) ? 1 : 2);
		}

		private void CheckNumberOfDevices()
		{
			List<InputDevice> list = new List<InputDevice>();
			foreach (InputDevice device in InputManager.Devices)
			{
				if (device.DeviceClass == InputDeviceClass.Controller)
				{
					list.Add(device);
				}
			}
			if (list.Count < minimumNumberOfControllers)
			{
				if (!showPopUpDelay.HasValue && localMultiplayerStateMonitor.CurrentLocalMultiplayerState != LocalMultiplayerState.InLocalMultiplayerMenu)
				{
					showPopUpDelay = 1f;
				}
			}
			else if (IsLastDeviceWithInputStillConncted(list))
			{
				RemoveUIFromScreen();
			}
		}

		private bool IsLastDeviceWithInputStillConncted(List<InputDevice> connectedDevices)
		{
			bool flag = false;
			if (lastDeviceToHaveInput == null)
			{
				return false;
			}
			foreach (InputDevice connectedDevice in connectedDevices)
			{
				if (connectedDevice.GUID == lastDeviceToHaveInput.GUID)
				{
					flag = true;
					break;
				}
			}
			if (!flag && !showPopUpDelay.HasValue)
			{
				showPopUpDelay = 1f;
			}
			return flag;
		}

		private void PutUIOnScreen()
		{
			if (!popUpIsOnScreen)
			{
				showPopUpDelay = null;
				controllerPopUpUI.SetActive(value: true);
				codeAnimation.PlayIn();
				popUpIsOnScreen = true;
				inputControlStateBeforePopup = inputService.CurrentState;
				inputService.ChangeToUI();
				if (navigationGroupManager != null)
				{
					navigationGroupManager.SetAutoSelectInAllGroups(autoselect: false);
					return;
				}
				navigationGroupManager = UnityEngine.Object.FindObjectOfType<UINavigationGroupManager>();
				navigationGroupManager.SetAutoSelectInAllGroups(autoselect: false);
			}
		}

		private void RemoveUIFromScreen()
		{
			if (popUpIsOnScreen)
			{
				showPopUpDelay = null;
				codeAnimation.PlayOut();
				switch (inputControlStateBeforePopup)
				{
				case InputService.InputState.Gameplay:
					inputService.ChangeToGameplay();
					break;
				case InputService.InputState.UI:
					inputService.ChangeToUI();
					break;
				}
				if (navigationGroupManager != null)
				{
					navigationGroupManager.SetAutoSelectInAllGroups(autoselect: true);
				}
				popUpIsOnScreen = false;
			}
		}

		private void ReturnToMainMenu()
		{
			TABSSceneManager.LoadMainMenu();
		}
	}
}
