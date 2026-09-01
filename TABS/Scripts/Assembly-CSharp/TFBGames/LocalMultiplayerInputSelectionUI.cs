using InControl;
using Landfall.TABS;
using Landfall.TABS_Input;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace TFBGames
{
	public class LocalMultiplayerInputSelectionUI : MonoBehaviour
	{
		[SerializeField]
		protected Sprite emptySprite;

		[SerializeField]
		protected Sprite redTeamIcon;

		[SerializeField]
		protected Sprite blueTeamIcon;

		[SerializeField]
		protected InputIcon[] inputIcons;

		[SerializeField]
		protected Image[] playerButtonImages;

		[SerializeField]
		protected Image[] playerReadyFadeImages;

		[SerializeField]
		protected TMP_Text[] playerLabels;

		[SerializeField]
		protected TMP_Text[] playerStateLabels;

		[SerializeField]
		protected Button okButton;

		[SerializeField]
		protected CodeAnimation codeAnimation;

		[SerializeField]
		protected Color waitingColor;

		[SerializeField]
		protected Color readyColor;

		private const string PlayerLabel = "MP_LABEL_PLAYER";

		private const string StateLabel = "<color=#{0}>{1}</color>";

		private const string Waiting = "MP_LABEL_PRESS_TO_JOIN";

		private const bool ShouldUseRedAndBlueTeamIcons = false;

		private const string Ready = "MP_LABEL_READY";

		private const int DisableInputDelay = 6;

		private InputService inputService;

		private PlayerActions playerActions;

		private ModalPanel modalPanel;

		private ControllerService controllerService;

		private Vector3 originalPlayerIconLocalScale;

		private Vector3 flippedIcon;

		private readonly bool[] isPlayerImageFlipped = new bool[2];

		private bool activateButton;

		private int? disableInputUntilFrame;

		private bool wasButtonHeldDown;

		private void Awake()
		{
			inputService = ServiceLocator.GetService<InputService>();
			playerActions = PlayerActions.Instance;
			modalPanel = ServiceLocator.GetService<ModalPanel>();
			controllerService = ServiceLocator.GetService<ControllerService>();
			originalPlayerIconLocalScale = playerButtonImages[0].rectTransform.localScale;
			flippedIcon = originalPlayerIconLocalScale;
			flippedIcon.x = originalPlayerIconLocalScale.x * -1f;
		}

		private void OnEnable()
		{
			TemporarilyDisableInput();
			inputService.SetPassAndPlayMode(enablePassAndPlay: false);
			inputService.ClearPlayerInputDevices();
			SetInputServiceEventSubscriptions(subscribe: true);
			int i = 0;
			for (int num = isPlayerImageFlipped.Length; i < num; i++)
			{
				isPlayerImageFlipped[i] = false;
			}
			GetIsButtonHeldDown(out wasButtonHeldDown, out var _, out var _);
			HandlePlayerIcons();
			inputService.SetPlayer(Player.Any);
			HandleOkayButton(isInitializing: true);
		}

		private void OnDisable()
		{
			SetInputServiceEventSubscriptions(subscribe: false);
		}

		private void Update()
		{
			if (playerActions == null || (codeAnimation != null && !codeAnimation.IsInAndNotPlaying))
			{
				return;
			}
			if (activateButton && okButton != null)
			{
				activateButton = false;
				okButton.interactable = true;
				EventSystem current = EventSystem.current;
				if (current != null)
				{
					current.SetSelectedGameObject(okButton.gameObject);
				}
			}
			UpdateInput();
		}

		private void UpdateInput()
		{
			bool flag = false;
			GetIsButtonHeldDown(out var isButtonOrKeyHeldDown, out var wasKeyboardPressed, out var activeDevice);
			if (wasButtonHeldDown != isButtonOrKeyHeldDown)
			{
				if (!wasButtonHeldDown)
				{
					flag = true;
				}
				wasButtonHeldDown = isButtonOrKeyHeldDown;
			}
			if ((modalPanel != null && modalPanel.IsPopupOpen) || (controllerService != null && controllerService.IsControllerDisconnectedPopupOnScreen))
			{
				TemporarilyDisableInput();
			}
			else
			{
				if (disableInputUntilFrame.HasValue && disableInputUntilFrame.Value > Time.frameCount)
				{
					return;
				}
				disableInputUntilFrame = null;
				if (flag)
				{
					if (wasKeyboardPressed)
					{
						activeDevice = InputDevice.Null;
					}
					inputService.AddPlayerDevice(activeDevice);
					UpdateUIElements();
				}
			}
		}

		private void GetIsButtonHeldDown(out bool isButtonOrKeyHeldDown, out bool wasKeyboardPressed, out InputDevice activeDevice)
		{
			activeDevice = InputManager.ActiveDevice;
			wasKeyboardPressed = InputManager.AnyKeyIsPressed;
			bool flag = activeDevice.AnyButtonIsPressed || activeDevice.CommandIsPressed;
			isButtonOrKeyHeldDown = wasKeyboardPressed || flag;
		}

		private void TemporarilyDisableInput()
		{
			disableInputUntilFrame = Time.frameCount + 6;
		}

		private void SetInputServiceEventSubscriptions(bool subscribe)
		{
			if (!(inputService == null))
			{
				inputService.CurrentNumberOfPlayersChanged -= OnCurrentNumberOfPlayersChanged;
				inputService.ClearedPlayerInputDevices -= OnClearedPlayerInputDevices;
				if (subscribe)
				{
					inputService.CurrentNumberOfPlayersChanged += OnCurrentNumberOfPlayersChanged;
					inputService.ClearedPlayerInputDevices += OnClearedPlayerInputDevices;
				}
			}
		}

		private void OnCurrentNumberOfPlayersChanged()
		{
			UpdateUIElements();
		}

		private void OnClearedPlayerInputDevices()
		{
			UpdateUIElements();
		}

		private void UpdateUIElements()
		{
			HandlePlayerIcons();
			HandleOkayButton();
		}

		private void HandlePlayerIcons()
		{
			for (int i = 0; i < 2; i++)
			{
				InputDevice inputDevice = ((inputService.PlayerInputDevices != null && i < inputService.PlayerInputDevices.Length) ? inputService.PlayerInputDevices[i] : null);
				bool isWaitingForPlayer = inputService.currentNumberOfPlayers <= i;
				InputIcon inputIcon = ((inputDevice != null) ? GetInputIcon(inputDevice.DeviceClass) : null);
				SetPlayerButtonImage(i, inputIcon, isWaitingForPlayer);
				SetPlayerLabels(i, isWaitingForPlayer);
				SetPlayerReadyFadeImage(i, isWaitingForPlayer);
			}
		}

		private void HandleOkayButton(bool isInitializing = false)
		{
			if (!(inputService == null))
			{
				ActivateOkButton(inputService.currentNumberOfPlayers == 2, isInitializing);
			}
		}

		private void ActivateOkButton(bool shouldActivate, bool isInitializing)
		{
			if (okButton == null)
			{
				return;
			}
			if (shouldActivate)
			{
				playerActions.ClearInputState();
				activateButton = true;
				return;
			}
			okButton.interactable = false;
			activateButton = false;
			if (isInitializing)
			{
				EventSystem current = EventSystem.current;
				if (current != null)
				{
					current.SetSelectedGameObject(null);
				}
			}
		}

		private InputIcon GetInputIcon(InputDeviceClass inputDeviceClass)
		{
			if (inputDeviceClass == InputDeviceClass.Unknown)
			{
				inputDeviceClass = InputDeviceClass.Keyboard;
			}
			int i = 0;
			for (int num = inputIcons.Length; i < num; i++)
			{
				InputIcon inputIcon = inputIcons[i];
				if (inputIcon != null && inputIcon.inputDeviceType == inputDeviceClass)
				{
					return inputIcon;
				}
			}
			return null;
		}

		private void SetPlayerButtonImage(int index, InputIcon inputIcon, bool isWaitingForPlayer)
		{
			Sprite icon = emptySprite;
			if (!isWaitingForPlayer && inputIcon != null)
			{
				icon = inputIcon.Icon;
			}
			playerButtonImages[index].sprite = icon;
			if (isWaitingForPlayer)
			{
				playerButtonImages[index].rectTransform.localScale = originalPlayerIconLocalScale;
				isPlayerImageFlipped[index] = false;
			}
		}

		private void SetPlayerLabels(int index, bool isWaitingForPlayer)
		{
			playerLabels[index].text = Localizer.GetSinglePhrase("MP_LABEL_PLAYER", (index + 1).ToString());
			if (isWaitingForPlayer)
			{
				playerStateLabels[index].text = string.Format("<color=#{0}>{1}</color>", ColorUtility.ToHtmlStringRGBA(waitingColor), Localizer.GetSinglePhrase("MP_LABEL_PRESS_TO_JOIN"));
			}
			else
			{
				playerStateLabels[index].text = string.Format("<color=#{0}>{1}</color>", ColorUtility.ToHtmlStringRGBA(readyColor), Localizer.GetSinglePhrase("MP_LABEL_READY"));
			}
		}

		private void SetPlayerReadyFadeImage(int index, bool isWaitingForPlayer)
		{
			playerReadyFadeImages[index].gameObject.SetActive(!isWaitingForPlayer);
		}

		private void EnablePassAndPlay()
		{
			inputService.SetPassAndPlayMode(enablePassAndPlay: true);
		}
	}
}
