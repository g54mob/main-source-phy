using GamepadUI.StateManager.Core;
using InControl;
using Landfall.TABS.GameMode;
using Landfall.TABS.Services;
using Landfall.TABS_Input;
using TFBGames;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class InputViewer : UIComponent
{
	[SerializeField]
	private GameObject switchInputScreen;

	[SerializeField]
	private GameObject battleModeTabOff;

	[SerializeField]
	private GameObject battleModeTabOn;

	[SerializeField]
	private GameObject placementModeTabOff;

	[SerializeField]
	private GameObject placementModeTabOn;

	[SerializeField]
	private GameObject battleModeLabel;

	[SerializeField]
	private GameObject placementModeLabel;

	[SerializeField]
	private GameObject backButton;

	[SerializeField]
	private GameObject switchBackIcon;

	[SerializeField]
	private Image battleModeXbox;

	[FormerlySerializedAs("battleModePS4")]
	[SerializeField]
	private Image battleModePS;

	[SerializeField]
	private Image battleModeSwitch;

	[SerializeField]
	private Image placementModeXbox;

	[FormerlySerializedAs("placementModePS4")]
	[SerializeField]
	private Image placementModePS;

	[SerializeField]
	private Image placementModeSwitch;

	[SerializeField]
	private Button exitInputViewer;

	[SerializeField]
	private Button exitToPlacementUI;

	[SerializeField]
	private Button exitToEscapeMenu;

	private InputService inputService;

	private PlayerActions playerActions;

	private bool openedFromPlacementUI;

	private CodeAnimation stateCodeAnimation;

	private INetworkService m_networkService;

	private bool isOpen;

	private InputDeviceStyle currentDeviceStyle;

	public bool OpenedFromPlacementUI
	{
		get
		{
			return openedFromPlacementUI;
		}
		set
		{
			openedFromPlacementUI = value;
		}
	}

	protected override void Awake()
	{
		base.Awake();
		inputService = ServiceLocator.GetService<InputService>();
		stateCodeAnimation = base.gameObject.GetComponent<CodeAnimation>();
		playerActions = PlayerActions.Instance;
		OnInputStyleChanged(PlayerActions.Instance.LastDeviceStyle);
		m_networkService = ServiceLocator.GetService<INetworkService>();
	}

	protected override void OnOpen()
	{
		base.OnOpen();
		if (inputService != null)
		{
			inputService.InputDeviceStyleChanged += OnInputStyleChanged;
		}
		if (!OpenedFromPlacementUI)
		{
			SetPause();
		}
		stateCodeAnimation.PlayIn();
		isOpen = true;
	}

	protected override void OnClose()
	{
		base.OnClose();
		if (inputService != null)
		{
			inputService.InputDeviceStyleChanged -= OnInputStyleChanged;
		}
		stateCodeAnimation.PlayOut();
		isOpen = false;
	}

	protected override void Update()
	{
		base.Update();
		if (playerActions.m_placementShowInput.WasReleased || playerActions.m_showInput.WasReleased)
		{
			Exit();
		}
	}

	public void Exit()
	{
		exitInputViewer.onClick.Invoke();
	}

	public void OnExitInputViewerClicked()
	{
		if (OpenedFromPlacementUI)
		{
			exitToPlacementUI.onClick.Invoke();
		}
		else
		{
			exitToEscapeMenu.onClick.Invoke();
		}
	}

	private void OnInputStyleChanged(InputDeviceStyle deviceStyle)
	{
		currentDeviceStyle = deviceStyle;
		battleModeXbox.gameObject.SetActive(value: false);
		battleModePS.gameObject.SetActive(value: false);
		battleModeSwitch.gameObject.SetActive(value: false);
		placementModeXbox.gameObject.SetActive(value: false);
		placementModePS.gameObject.SetActive(value: false);
		placementModeSwitch.gameObject.SetActive(value: false);
		switchInputScreen.SetActive(value: false);
		battleModeLabel.SetActive(value: false);
		placementModeLabel.SetActive(value: false);
		backButton.SetActive(value: false);
		switchBackIcon.SetActive(value: false);
		switch (deviceStyle)
		{
		case InputDeviceStyle.NintendoSwitch:
			switchInputScreen.SetActive(value: true);
			battleModeSwitch.gameObject.SetActive(value: true);
			switchBackIcon.SetActive(value: true);
			break;
		case InputDeviceStyle.PlayStation2:
		case InputDeviceStyle.PlayStation3:
		case InputDeviceStyle.PlayStation4:
		case InputDeviceStyle.PlayStation5:
			battleModePS.gameObject.SetActive(value: true);
			placementModePS.gameObject.SetActive(value: true);
			battleModeLabel.SetActive(value: true);
			placementModeLabel.SetActive(value: false);
			break;
		default:
			battleModeLabel.SetActive(value: true);
			placementModeLabel.SetActive(value: true);
			battleModeXbox.gameObject.SetActive(value: true);
			placementModeXbox.gameObject.SetActive(value: true);
			break;
		}
	}

	private void SetPause()
	{
		BaseGameMode currentGameMode = ServiceLocator.GetService<GameModeService>().CurrentGameMode;
		currentGameMode.PlacementCamera.AllowMovement(allow: false);
		if (!m_networkService.IsRunning)
		{
			ITimeService timeService = currentGameMode.TimeService;
			timeService.Unlock();
			timeService.SetState(0f, 0f);
			timeService.Pause();
			timeService.Lock();
		}
	}
}
