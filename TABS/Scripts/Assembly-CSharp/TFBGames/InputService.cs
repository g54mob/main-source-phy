#define UNITY_STANDALONE
#define UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Diagnostics;
using InControl;
using Landfall.TABS;
using Landfall.TABS.UnitPlacement;
using Landfall.TABS_Input;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace TFBGames
{
	public class InputService : ServicePrefab
	{
		public enum InputState
		{
			Gameplay = 0,
			UI = 1
		}

		public const int MaxNumberOfPlayers = 2;

		private const int DelayCheckPlayerDevicesMax = 3;

		public InputState stateToChange;

		public InputType CurrentInputType = InputType.Any;

		private const int TICKS_TO_WAIT_AFTER_STORAGE_READY = 1;

		private const int KEYBOARD_UI = 1;

		private const string INPUT_MODE = "UI_INPUT_MODE";

		private const int TICKS_TO_WAIT_AFTER_FOCUS_GAINED = 1;

		private SettingsInstance m_inputModeOption;

		private const int FRAME_STATE_DELAY = 5;

		private int changeStateFrameCount;

		private ModalPanel popUpService;

		private InControlInputModule inputModule;

		private CursorController cursorController;

		private BindingSourceType lastBindingSource = BindingSourceType.MouseBindingSource;

		private InputDeviceStyle deviceStyle;

		private DeviceBindingSource leftBumper;

		private DeviceBindingSource rightBumper;

		private LocalMultiplayerStateMonitor localMultiplayerStateMonitor;

		private int currentTick = 2;

		private int currentFoscusTick = 2;

		private InputDevice lastDeviceWIthInput;

		private int? activeDeviceIndex;

		[SerializeField]
		private ControllerService ControllerService;

		private int stateCounter;

		private InputDevice[] playerInputDevices;

		private PlayerActions[] playerActionsGroup;

		private bool passAndPlayModeEnabled;

		private int? delayCheckPlayerDevices;

		private bool supportsCheckingIfPlayerDevicesChanged;

		public InputState CurrentState { get; private set; }

		public Player CurrentPlayer { get; private set; }

		public PlacementCursorState placementCursorState { get; private set; }

		public int currentNumberOfPlayers { get; private set; }

		public InputDeviceStyle inputDeviceStyle => deviceStyle;

		public bool PassAndPlayModeEnabled => passAndPlayModeEnabled;

		public InputDevice[] PlayerInputDevices => playerInputDevices;

		public PlayerActions[] PlayerActionsGroup => playerActionsGroup;

		public int? ActiveDeviceIndex => activeDeviceIndex;

		public event Action<InputType> InputChanged;

		public event Action<InputDeviceStyle> InputDeviceStyleChanged;

		public event Action CurrentNumberOfPlayersChanged;

		public event Action ClearedPlayerInputDevices;

		public override void OnAwake()
		{
			placementCursorState = PlacementCursorState.Hide;
			CurrentState = InputState.Gameplay;
			popUpService = ServiceLocator.GetService<ModalPanel>();
			cursorController = ServiceLocator.GetService<CursorController>();
			localMultiplayerStateMonitor = ServiceLocator.GetService<LocalMultiplayerStateMonitor>();
			playerInputDevices = new InputDevice[2];
			playerActionsGroup = new PlayerActions[2];
			CurrentPlayer = Player.Any;
			SettingsProfileManager service = ServiceLocator.GetService<SettingsProfileManager>();
			leftBumper = new DeviceBindingSource(InputControlType.LeftBumper);
			rightBumper = new DeviceBindingSource(InputControlType.RightBumper);
			passAndPlayModeEnabled = false;
			if (service?.CurrentSettingsProfile != null && service.CurrentSettingsProfile.EnforceControllerConnection)
			{
				supportsCheckingIfPlayerDevicesChanged = true;
				ServiceLocator.RegisterService(UnityEngine.Object.Instantiate(ControllerService));
			}
			SetInputManagerEventSubscriptions(subscribe: true);
		}

		public override void OnStart()
		{
			base.OnStart();
			if (popUpService == null)
			{
				popUpService = ServiceLocator.GetService<ModalPanel>();
			}
			PlayerActions.Instance.OnLastInputTypeChanged += OnInputBindingSourceChanged;
			SceneManager.sceneLoaded += OnSceneLoaded;
			if (SingletonMonoBehavior<InControlManager>.Instance != null)
			{
				inputModule = SingletonMonoBehavior<InControlManager>.Instance.GetComponent<InControlInputModule>();
			}
			SetMouseAvailablePerPlatform();
			popUpService.OnPopUpOpen += OnForceUIOpen;
			popUpService.OnPopUpClose += OnForceUIClose;
			ServiceLocator.GetService<WaitForStorage>().FireWhenReady(OnStorageReady);
			PopulateInputModeOption();
		}

		public override void OnUpdate()
		{
			if (Time.frameCount > changeStateFrameCount + 5)
			{
				CurrentState = stateToChange;
			}
			if (deviceStyle != PlayerActions.Instance.LastDeviceStyle)
			{
				deviceStyle = PlayerActions.Instance.LastDeviceStyle;
				this.InputDeviceStyleChanged?.Invoke(deviceStyle);
			}
			if (currentTick < 1)
			{
				currentTick++;
				if (currentTick == 1)
				{
					CheckPlayerActionsAgainstUiInputMode();
				}
			}
			if (currentFoscusTick < 1)
			{
				currentFoscusTick++;
				if (currentFoscusTick == 1)
				{
					CheckBindingSourceAndAdjustCursorVisibility();
				}
			}
			if (delayCheckPlayerDevices.HasValue)
			{
				delayCheckPlayerDevices--;
				if (delayCheckPlayerDevices.Value <= 0)
				{
					CheckPlayerDevices();
				}
			}
		}

		public void ClearInputsForMainMenu()
		{
			ClearPlayerInputDevices();
			SetPlayer(Player.Any);
		}

		public void ChangeToUI()
		{
			ChangeState(InputState.UI);
		}

		public void ChangeToGameplay()
		{
			ChangeState(InputState.Gameplay);
		}

		public void OnUIOpen()
		{
			ChangeToUI();
		}

		public void OnUIClose()
		{
			ChangeToGameplay();
		}

		[Conditional("UNITY_EDITOR")]
		[Conditional("UNITY_STANDALONE")]
		public void CheckPlayerActionsAgainstUiInputMode()
		{
			PopulateInputModeOption();
			if (m_inputModeOption.currentValue == 1)
			{
				PlayerActions.Instance.m_placeUnit.RemoveBinding(rightBumper);
				PlayerActions.Instance.m_removeUnit.RemoveBinding(leftBumper);
			}
			else
			{
				PlayerActions.Instance.m_placeUnit.AddBinding(rightBumper);
				PlayerActions.Instance.m_removeUnit.AddBinding(leftBumper);
			}
		}

		public void AddPlayerDevice(InputDevice activeDevice)
		{
			for (int i = 0; i < playerInputDevices.Length; i++)
			{
				if (playerInputDevices[i] == activeDevice)
				{
					return;
				}
			}
			if (currentNumberOfPlayers < playerInputDevices.Length)
			{
				playerInputDevices[currentNumberOfPlayers] = activeDevice;
				PlayerActions playerActions = PlayerActions.CreateWithDefaultBindings(activeDevice.DeviceClass == InputDeviceClass.Keyboard || activeDevice.DeviceClass == InputDeviceClass.Unknown);
				playerActions.Device = activeDevice;
				playerActionsGroup[currentNumberOfPlayers] = playerActions;
				SetCurrentNumberOfPlayers(currentNumberOfPlayers + 1);
			}
		}

		public void ClearPlayerInputDevices()
		{
			playerInputDevices = new InputDevice[2];
			playerActionsGroup = new PlayerActions[2];
			SetCurrentNumberOfPlayers(0);
			this.ClearedPlayerInputDevices?.Invoke();
		}

		public void OnInputBindingSourceChanged(BindingSourceType bindingSourceType)
		{
			InputType inputType = PlayerActions.Instance.GetInputType(bindingSourceType);
			if (TABSBooter._DisableGamepad && inputType == InputType.Controller)
			{
				PlayerActions.Instance.LastInputType = lastBindingSource;
				return;
			}
			AllowMouseInput(inputType == InputType.Keyboard);
			bool flag = IsTextInputCurrentlySelected() || IsModalPopupOpen();
			if (bindingSourceType == BindingSourceType.MouseBindingSource && !flag && EventSystem.current != null)
			{
				EventSystem.current.SetSelectedGameObject(null);
			}
			if ((lastBindingSource == BindingSourceType.KeyBindingSource || lastBindingSource == BindingSourceType.MouseBindingSource) && (bindingSourceType == BindingSourceType.KeyBindingSource || bindingSourceType == BindingSourceType.MouseBindingSource))
			{
				lastBindingSource = bindingSourceType;
				return;
			}
			Selectable selectableUnderCursor = GetSelectableUnderCursor();
			if (selectableUnderCursor != null && !flag)
			{
				selectableUnderCursor.Select();
				UnityEngine.Debug.LogFormat("Switched input to {0}. Selected <color=blue>{1}</color>", inputType.ToString(), selectableUnderCursor.name);
			}
			lastBindingSource = bindingSourceType;
			CurrentInputType = inputType;
			this.InputChanged?.Invoke(inputType);
		}

		public bool IsTextInputCurrentlySelected()
		{
			EventSystem current = EventSystem.current;
			if (current != null)
			{
				GameObject currentSelectedGameObject = current.currentSelectedGameObject;
				if (currentSelectedGameObject != null)
				{
					TMP_InputField component = currentSelectedGameObject.GetComponent<TMP_InputField>();
					InputField component2 = currentSelectedGameObject.GetComponent<InputField>();
					if (component != null || component2 != null)
					{
						return true;
					}
				}
			}
			return false;
		}

		public void SetPassAndPlayMode(bool enablePassAndPlay)
		{
			passAndPlayModeEnabled = enablePassAndPlay;
			if (enablePassAndPlay)
			{
				AddPlayerDevice(InputDevice.Null);
			}
		}

		public bool CheckIfReadyForLocalMultiplayer()
		{
			for (int i = 0; i < playerInputDevices.Length; i++)
			{
				if (playerInputDevices[i] == null)
				{
					return false;
				}
			}
			return true;
		}

		private void SetInputManagerEventSubscriptions(bool subscribe)
		{
			if (supportsCheckingIfPlayerDevicesChanged)
			{
				InputManager.OnDeviceAttached -= OnDeviceAttached;
				InputManager.OnDeviceDetached -= OnDeviceDetached;
				if (subscribe)
				{
					InputManager.OnDeviceAttached += OnDeviceAttached;
					InputManager.OnDeviceDetached += OnDeviceDetached;
				}
			}
		}

		private void OnDeviceAttached(InputDevice _)
		{
			ScheduleCheckPlayerDevices();
		}

		private void OnDeviceDetached(InputDevice _)
		{
			ScheduleCheckPlayerDevices();
		}

		private void ScheduleCheckPlayerDevices()
		{
			if (localMultiplayerStateMonitor != null && localMultiplayerStateMonitor.CurrentLocalMultiplayerState != LocalMultiplayerState.InLocalMultiplayerMatch)
			{
				delayCheckPlayerDevices = 3;
			}
		}

		private void CheckPlayerDevices()
		{
			delayCheckPlayerDevices = null;
			if (playerInputDevices == null || playerInputDevices.Length == 0)
			{
				return;
			}
			bool flag = true;
			int i = 0;
			for (int num = playerInputDevices.Length; i < num; i++)
			{
				InputDevice inputDevice = playerInputDevices[i];
				if (inputDevice == null)
				{
					continue;
				}
				bool flag2 = false;
				int j = 0;
				for (int count = InputManager.Devices.Count; j < count; j++)
				{
					if (InputManager.Devices[j] == inputDevice)
					{
						flag2 = true;
						break;
					}
				}
				if (!flag2)
				{
					flag = false;
					break;
				}
			}
			if (!flag)
			{
				ClearPlayerInputDevices();
			}
		}

		private int? CheckWhichDeviceItIs(InputDevice activeDevice)
		{
			int? result = null;
			for (int i = 0; i < InputManager.Devices.Count; i++)
			{
				if (InputManager.Devices[i] == activeDevice)
				{
					result = i;
					break;
				}
			}
			return result;
		}

		private void ChangeState(InputState state, bool forceState = false)
		{
			if (!forceState)
			{
				stateCounter = ((state == InputState.UI) ? (stateCounter + 1) : (stateCounter - 1));
				stateCounter = Mathf.Clamp(stateCounter, 0, int.MaxValue);
				if (stateCounter > 0 && state == InputState.Gameplay)
				{
					return;
				}
			}
			stateToChange = state;
			changeStateFrameCount = Time.frameCount;
		}

		private void OnForceUIClose()
		{
			ChangeState(InputState.Gameplay, forceState: true);
		}

		private void OnForceUIOpen()
		{
			ChangeState(InputState.UI, forceState: true);
		}

		private void OnEnable()
		{
			SetInputManagerEventSubscriptions(subscribe: true);
		}

		private void OnDisable()
		{
			if (popUpService != null)
			{
				popUpService.OnPopUpOpen -= OnForceUIOpen;
				popUpService.OnPopUpClose -= OnForceUIClose;
			}
			PlayerActions.Instance.OnLastInputTypeChanged -= OnInputBindingSourceChanged;
			SceneManager.sceneLoaded -= OnSceneLoaded;
			SetInputManagerEventSubscriptions(subscribe: false);
		}

		public void ResetStateCounter()
		{
			stateCounter = 0;
		}

		private bool IsModalPopupOpen()
		{
			if (popUpService != null)
			{
				return popUpService.IsPopupOpen;
			}
			return false;
		}

		public void SetCursorState(PlacementCursorState cursorState)
		{
			placementCursorState = cursorState;
		}

		private void SetMouseAvailablePerPlatform()
		{
			if (inputModule != null)
			{
				AllowMouseInput(allow: true);
			}
		}

		private void AllowMouseInput(bool allow)
		{
			if (inputModule != null)
			{
				inputModule.allowMouseInput = allow;
			}
		}

		private Selectable GetSelectableUnderCursor()
		{
			List<RaycastResult> list = new List<RaycastResult>();
			cursorController.GetObjectsBeneathPointer(list);
			if (list.Count > 0)
			{
				foreach (RaycastResult item in list)
				{
					Selectable component = item.gameObject.GetComponent<Selectable>();
					if (component != null)
					{
						UnityEngine.Debug.LogFormat("Selectable Found: {0}", component.name);
						return component;
					}
				}
			}
			return null;
		}

		private bool IsDeviceInUse(InputDevice inputDevice)
		{
			InputDevice[] array = playerInputDevices;
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i] == inputDevice && !passAndPlayModeEnabled)
				{
					return true;
				}
			}
			return false;
		}

		public void SetPlayer(Player player)
		{
			CurrentPlayer = player;
			if (player == Player.Any)
			{
				PlayerActions.Instance.Device = null;
			}
			else
			{
				PlayerActions.Instance.Device = playerInputDevices[(int)player];
			}
		}

		public PlayerActions GetPlayerActions(Player player)
		{
			switch (player)
			{
			case Player.Any:
				return PlayerActions.Instance;
			case Player.One:
				return playerActionsGroup[0];
			case Player.Two:
				return playerActionsGroup[1];
			default:
				throw new ArgumentOutOfRangeException("player", player, null);
			}
		}

		public InputDevice GetPlayerDevice(Player player)
		{
			switch (player)
			{
			case Player.One:
				return playerInputDevices[0];
			case Player.Two:
				return playerInputDevices[1];
			default:
				return null;
			}
		}

		private void OnStorageReady()
		{
			currentTick = 0;
		}

		private void PopulateInputModeOption()
		{
			if (m_inputModeOption == null)
			{
				m_inputModeOption = ServiceLocator.GetService<GlobalSettingsHandler>().GetSettingsInstance("UI_INPUT_MODE");
			}
		}

		private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
		{
			RunCursorVisibilityChecks();
		}

		private void OnApplicationFocus(bool hasFocus)
		{
			if (hasFocus)
			{
				RunCursorVisibilityChecks();
			}
		}

		private void RunCursorVisibilityChecks()
		{
			CheckBindingSourceAndAdjustCursorVisibility();
			currentFoscusTick = 0;
		}

		[Conditional("UNITY_STANDALONE")]
		private void CheckBindingSourceAndAdjustCursorVisibility()
		{
			OnInputBindingSourceChanged(PlayerActions.Instance.LastInputType);
			ServiceLocator.GetService<CursorVisibilityController>().SetLockStateAndVisibility(CursorLockMode.None, visible: true);
		}

		private void SetCurrentNumberOfPlayers(int value)
		{
			if (currentNumberOfPlayers != value)
			{
				currentNumberOfPlayers = value;
				this.CurrentNumberOfPlayersChanged?.Invoke();
			}
		}
	}
}
