using System;
using System.Collections;
using InControl;
using Landfall.TABS.GameState;
using Landfall.TABS_Input;
using LevelCreator;
using TFBGames;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Landfall.TABS
{
	public class EscapeMenuHandler : InstancedHandler<EscapeMenuHandler>
	{
		private enum EscapeMenuState
		{
			Playing = 0,
			EscapeMenu = 1,
			Settings = 2
		}

		public CodeAnimation settingsAnim;

		public GameObject background;

		public GameObject group;

		[SerializeField]
		private Button m_buttonMainMenu;

		[SerializeField]
		private Button m_buttonResume;

		[SerializeField]
		private Button m_buttonSettings;

		[SerializeField]
		private CodeAnimation inputViewerAnimator;

		[SerializeField]
		private TextMeshProUGUI showControlsText;

		[SerializeField]
		private DMEditor dmEditor;

		private EscapeMenuState currentScapeMenuState;

		private PlayerActions m_playerActions;

		private Button[] m_menuButtons;

		private UIAutoFocusSelector m_autoFocusSelector;

		private SettingsHandler m_settingsHandler;

		private IUserProfileUI m_userProfileUI;

		private int m_OriginalSiblingIndex = -1;

		private InputService m_inputService;

		private INetworkService m_networkService;

		private INetworkQuitController m_networkQuit;

		private float fadeAnimTimer;

		[SerializeField]
		private float fadeAnimationTime = 1f;

		[SerializeField]
		private AnimationCurve fadeAnimationCurve;

		[SerializeField]
		private CanvasGroup buttonsCanvasGroup;

		private float invFadeAnimationTime;

		private const string showControlsWords = "HOLD {0} TO SHOW CONTROLS";

		private GlyphService glyphService;

		public static bool InMenu
		{
			get
			{
				EscapeMenuHandler escapeMenuHandler = InstancedHandler<EscapeMenuHandler>.Instance;
				if ((object)escapeMenuHandler == null)
				{
					return true;
				}
				return escapeMenuHandler.currentScapeMenuState != EscapeMenuState.Playing;
			}
		}

		public static bool InSettings
		{
			get
			{
				EscapeMenuHandler escapeMenuHandler = InstancedHandler<EscapeMenuHandler>.Instance;
				if ((object)escapeMenuHandler == null)
				{
					return false;
				}
				return escapeMenuHandler.currentScapeMenuState == EscapeMenuState.Settings;
			}
		}

		public event System.Action EscapeMenuOpened;

		public event System.Action EscapeMenuClosed;

		protected override void Awake()
		{
			base.Awake();
			m_buttonMainMenu.onClick.AddListener(GoToMainMenu);
			m_userProfileUI = ServiceLocator.GetService<IUserProfileUI>();
		}

		private void Start()
		{
			m_menuButtons = group.GetComponentsInChildren<Button>();
			UIHelpers.CreateExplicitLinearNavigation(m_menuButtons, horizontal: false);
			m_inputService = ServiceLocator.GetService<InputService>();
			glyphService = ServiceLocator.GetService<GlyphService>();
			m_networkService = ServiceLocator.GetService<INetworkService>();
			m_networkQuit = ServiceLocator.GetService<INetworkQuitController>();
			if (m_inputService != null)
			{
				m_inputService.InputChanged += OnInputSourceChanged;
				m_inputService.InputDeviceStyleChanged += OnInputDeviceStyleChanged;
			}
			m_playerActions = PlayerActions.Instance;
			m_settingsHandler = base.gameObject.GetComponentInChildren<SettingsHandler>(includeInactive: true);
			if (m_settingsHandler == null)
			{
				m_settingsHandler = base.gameObject.transform.parent.GetComponentInChildren<SettingsHandler>(includeInactive: true);
			}
			m_OriginalSiblingIndex = base.transform.GetSiblingIndex();
			invFadeAnimationTime = 1f / fadeAnimationTime;
			OnInputSourceChanged(PlayerActions.Instance.InputType);
			Resume();
		}

		private void Update()
		{
			if (!m_playerActions.IsListeningForBinding)
			{
				switch (currentScapeMenuState)
				{
				case EscapeMenuState.Playing:
					if (m_playerActions.m_menu.WasPressed)
					{
						GoToEscapeMenu();
					}
					break;
				case EscapeMenuState.EscapeMenu:
					if (!HandleControlsViewer() && (m_playerActions.m_menu.WasPressed || m_playerActions.m_back.WasPressed))
					{
						Resume();
					}
					break;
				case EscapeMenuState.Settings:
					if (m_playerActions.m_back.WasPressed)
					{
						if (m_settingsHandler.IsPopupOpen())
						{
							m_settingsHandler.CloseSettingsPopUp();
							break;
						}
						m_settingsHandler.DisableNavigation();
						GoToEscapeMenu();
					}
					break;
				}
			}
			if (settingsAnim.isPlaying)
			{
				return;
			}
			if (currentScapeMenuState == EscapeMenuState.Playing)
			{
				if (settingsAnim.gameObject.activeSelf)
				{
					settingsAnim.gameObject.SetActive(value: false);
				}
			}
			else if (!settingsAnim.gameObject.activeSelf)
			{
				settingsAnim.gameObject.SetActive(value: true);
			}
		}

		private void OnEnable()
		{
			dmEditor.OnEnterMenu += GoToEscapeMenu;
		}

		private void OnDisable()
		{
			if (m_inputService != null)
			{
				m_inputService.InputChanged -= OnInputSourceChanged;
				m_inputService.InputDeviceStyleChanged -= OnInputDeviceStyleChanged;
			}
			dmEditor.OnEnterMenu -= GoToEscapeMenu;
		}

		private void OnDestroy()
		{
			if (m_inputService != null)
			{
				m_inputService.OnUIClose();
			}
		}

		private bool HandleControlsViewer()
		{
			if (m_playerActions.m_showInput.WasPressed && inputViewerAnimator != null)
			{
				fadeAnimTimer = 0f;
				EventSystem.current.SetSelectedGameObject(null);
				inputViewerAnimator.PlayIn();
			}
			if (m_playerActions.m_showInput.WasReleased && inputViewerAnimator != null)
			{
				fadeAnimTimer = 0f;
				m_buttonResume.Select();
				inputViewerAnimator.PlayOut();
			}
			bool isPressed = m_playerActions.m_showInput.IsPressed;
			if (isPressed)
			{
				if (fadeAnimTimer <= fadeAnimationTime || buttonsCanvasGroup.alpha > 0f)
				{
					buttonsCanvasGroup.alpha = 1f - fadeAnimationCurve.Evaluate(fadeAnimTimer * invFadeAnimationTime);
					fadeAnimTimer += Time.unscaledDeltaTime;
					return isPressed;
				}
			}
			else if (buttonsCanvasGroup.alpha < 1f && fadeAnimTimer <= fadeAnimationTime)
			{
				buttonsCanvasGroup.alpha = fadeAnimationCurve.Evaluate(fadeAnimTimer * invFadeAnimationTime);
				fadeAnimTimer += Time.unscaledDeltaTime;
			}
			return isPressed;
		}

		private void OnInputSourceChanged(InputType type)
		{
			showControlsText.enabled = type == InputType.Controller;
			ChangeShowInputWords(type, m_playerActions.LastDeviceStyle);
			if (InMenu)
			{
				switch (type)
				{
				case InputType.Controller:
					m_buttonResume.Select();
					break;
				default:
					throw new ArgumentOutOfRangeException("type", type, null);
				case InputType.Keyboard:
				case InputType.Any:
					break;
				}
			}
		}

		private void OnInputDeviceStyleChanged(InputDeviceStyle deviceStyle)
		{
			ChangeShowInputWords(m_playerActions.InputType, deviceStyle);
		}

		private void ChangeShowInputWords(InputType inputType, InputDeviceStyle deviceStyle)
		{
			string actionGlyph = glyphService.GetActionGlyph(PlayerActions.Instance.m_showInput, inputType, deviceStyle);
			showControlsText.text = $"HOLD {actionGlyph} TO SHOW CONTROLS";
		}

		public void Resume()
		{
			background.SetActive(value: false);
			if (m_OriginalSiblingIndex > 0)
			{
				base.transform.SetSiblingIndex(m_OriginalSiblingIndex);
			}
			group.gameObject.SetActive(value: false);
			if (settingsAnim.currentState == CodeAnimationInstance.AnimationUse.In)
			{
				settingsAnim.PlayOut();
			}
			m_userProfileUI?.Hide();
			currentScapeMenuState = EscapeMenuState.Playing;
			EventSystem.current.SetSelectedGameObject(null);
			if (this.EscapeMenuClosed != null)
			{
				this.EscapeMenuClosed();
			}
			m_inputService.OnUIClose();
		}

		public void GoToEscapeMenu()
		{
			if (UIScreenInputBlocker.BlockInput || UIScreenInputBlocker.IsAnimatedMenuChangingState || UIMapSelector.IsOpen || DMEditor.BlockEscapeMenu)
			{
				return;
			}
			if (this.EscapeMenuOpened != null)
			{
				this.EscapeMenuOpened();
			}
			base.transform.SetAsLastSibling();
			background.SetActive(value: true);
			if (m_inputService.CurrentState == InputService.InputState.Gameplay)
			{
				m_inputService.OnUIOpen();
			}
			group.SetActive(value: true);
			if (m_playerActions.InputType == InputType.Controller)
			{
				if (currentScapeMenuState == EscapeMenuState.Settings)
				{
					m_buttonSettings.Select();
				}
				else
				{
					StartCoroutine(DelayedButtonSelect(0.175f));
				}
			}
			if (settingsAnim.currentState == CodeAnimationInstance.AnimationUse.In)
			{
				settingsAnim.PlayOut();
			}
			m_userProfileUI?.Show(canChangeProfile: false);
			currentScapeMenuState = EscapeMenuState.EscapeMenu;
		}

		private IEnumerator DelayedButtonSelect(float delay)
		{
			yield return new WaitForSecondsRealtime(delay);
			if (m_buttonResume != null && InMenu)
			{
				m_buttonResume.Select();
			}
		}

		public void GoToSettings()
		{
			if (settingsAnim.currentState == CodeAnimationInstance.AnimationUse.Out)
			{
				settingsAnim.PlayIn();
			}
			group.SetActive(value: false);
			m_settingsHandler.EnableNavigation();
			if ((bool)DMEditor.Instance)
			{
				m_settingsHandler.GetComponentInChildren<Selectable>().Select();
			}
			currentScapeMenuState = EscapeMenuState.Settings;
		}

		public void GoToMainMenu()
		{
			m_userProfileUI?.Hide();
			if (m_networkService.IsRunning)
			{
				m_networkQuit.QuitMultiplayerGame(loadMainMenu: false);
			}
			UIUtil.AskForConfirmationFirstIfEditorHasDirtyUserLevel(delegate
			{
				UIUtil.ClearEditorLevel();
				m_inputService.OnUIClose();
				background.SetActive(value: false);
				TABSSceneManager.LoadMainMenu();
			});
		}

		public void EndBattle()
		{
			ServiceLocator.GetService<GameStateManager>().EnterPlacementState();
			Resume();
		}
	}
}
