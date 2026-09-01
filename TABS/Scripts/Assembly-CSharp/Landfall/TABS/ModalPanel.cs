using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using DM;
using Landfall.TABS.GameMode;
using Landfall.TABS.Workshop;
using Landfall.TABS_Input;
using LevelCreator;
using ModIO.UI;
using TFBGames;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Landfall.TABS
{
	public class ModalPanel : ServicePrefab
	{
		private struct SecretUnlockInfo
		{
			public string m_description;

			public Sprite m_icon;
		}

		public struct ContentDisplayParameters
		{
			public UnitBlueprint unit;

			public Faction faction;

			public TABSCampaignAsset campaign;

			public TABSCampaignLevelAsset battle;

			public CustomMap level;
		}

		public struct InputFieldParameters
		{
			public string header;

			public string question;

			public Action<string> onFinish;

			public string yesButton;

			public string noButton;

			public string startInput;

			public bool isMultiline;

			public string singlelineHeader;

			public string multilineHeader;

			public TMP_InputField.ContentType contentType;

			public Func<string, bool> inputValidation;

			public bool isNegativeAction;
		}

		public const int PopupFadeInDurationMilliSeconds = 500;

		public const float PopupFadeInDuration = 0.5f;

		[SerializeField]
		private LocalizeText m_Header;

		[SerializeField]
		private LocalizeText m_Question;

		[SerializeField]
		private Button m_YesButton;

		[SerializeField]
		private Button m_NoButton;

		[SerializeField]
		private Button m_CancelButton;

		[SerializeField]
		private Button m_CustomButton;

		[SerializeField]
		private GameObject m_PanelObject;

		[SerializeField]
		private UINavigationGroup m_PanelNavigationGroup;

		[SerializeField]
		private GameObject m_unlockPanelObject;

		[SerializeField]
		private LocalizeText m_unlockText;

		[SerializeField]
		private Image m_unlockImage;

		[SerializeField]
		private Button m_unlockButton;

		[SerializeField]
		private NavigableTMPTextInput m_InputField;

		[SerializeField]
		private NavigableTMPTextInput m_MultilineInputField;

		[SerializeField]
		private LocalizeText m_SingleLineHeader;

		[SerializeField]
		private LocalizeText m_MultiLineHeader;

		[SerializeField]
		private Transform m_ButtonContainer;

		[SerializeField]
		private Image m_BackButtonImage;

		[SerializeField]
		[Tooltip("Dummy button to gain focus when a wait pop-up is shown via WaitPopUpWithFocus.")]
		private Button m_DummyButton;

		[Header("Content Displays")]
		[SerializeField]
		private GameObject m_contentDisplay;

		[SerializeField]
		private TMP_Text m_contentName;

		[SerializeField]
		private UnitButtonBase m_unitDisplay;

		[SerializeField]
		private CustomContentCampaignButton m_campaignDisplay;

		[SerializeField]
		private CustomContentBattleButton m_battleDisplay;

		[SerializeField]
		private CustomContentFactionButton m_factionDisplay;

		[SerializeField]
		private CustomContentLevelButton m_levelDisplay;

		[SerializeField]
		private GameObject m_background;

		private Color targetColor;

		[Header("Color Schemes")]
		[SerializeField]
		private SelectableColorScheme m_buttonScheme;

		[SerializeField]
		private SelectableColorScheme m_negativeButtonScheme;

		[Header("Settings proposal panel")]
		[SerializeField]
		private GameObject m_proposalPanel;

		[SerializeField]
		private LocalizeText m_turnStyle;

		[SerializeField]
		private LocalizeText m_timedPlacement;

		[SerializeField]
		private TMP_Text m_time;

		[SerializeField]
		private TMP_Text m_budget;

		[SerializeField]
		private TMP_Text m_unitCap;

		[SerializeField]
		private Button m_acceptProposal;

		[SerializeField]
		private Button m_rejectProposal;

		[SerializeField]
		private TMP_Text m_autoAcceptTimer;

		[SerializeField]
		private GameObject timePlacementObject;

		[SerializeField]
		private GameObject timeObject;

		private bool isOnline;

		private float autoAcceptTimer;

		private InputService m_inputService;

		private GameModeService m_gameModeService;

		private DelaySelectable delaySelectable = new DelaySelectable();

		private int m_LastFrameOpen;

		private bool m_delayUnlock;

		private bool m_pauseGame;

		private bool isShowingDisconectPopUp;

		private bool isShowingWaitingToShutdownPopup;

		private GameObject m_preOpenFocusedObject;

		private float m_headerFontSizeMax = 44f;

		private const string turnStyleNormal = "MP_SETTING_NORMAL";

		private const string turnStyleBlind = "MP_SETTING_BLINDMODE";

		private bool resetProgressPanelOpen;

		private Queue<SecretUnlockInfo> m_secretUnlockQueue = new Queue<SecretUnlockInfo>();

		public bool PauseGame => m_pauseGame;

		public bool IsPopupOpen
		{
			get
			{
				if (m_PanelObject != null && m_unlockPanelObject != null && m_proposalPanel != null)
				{
					if (!m_PanelObject.activeSelf && !m_unlockPanelObject.activeSelf)
					{
						return m_proposalPanel.activeSelf;
					}
					return true;
				}
				if (m_PanelObject == null)
				{
					Debug.LogError("m_PanelObject is null");
				}
				if (m_unlockPanelObject == null)
				{
					Debug.LogError("m_unlockPanelObject is null");
				}
				if (m_proposalPanel == null)
				{
					Debug.LogError("m_proposalPanel is null");
				}
				Debug.LogError("Important Modal Panel elements are null, please check that the prefab is not broken");
				return false;
			}
		}

		public int OpenId { get; private set; }

		private GameObject inputFieldParent => m_InputField.transform.parent.gameObject;

		private GameObject multilineInputFieldParent => m_MultilineInputField.transform.parent.gameObject;

		public event System.Action OnPopUpOpen;

		public event System.Action OnPopUpClose;

		public override void OnAwake()
		{
			base.OnAwake();
			if (m_Header != null && m_Header.Text != null)
			{
				m_headerFontSizeMax = m_Header.Text.fontSizeMax;
			}
			RectTransform obj = (RectTransform)m_DummyButton.transform;
			obj.anchoredPosition = new Vector2(obj.anchoredPosition.x, -10000f);
		}

		private void Start()
		{
			m_inputService = ServiceLocator.GetService<InputService>();
			if (m_inputService != null)
			{
				m_inputService.InputChanged += OnInputSourceChanged;
			}
			targetColor = m_background.GetComponent<Image>().color;
			m_gameModeService = ServiceLocator.GetService<GameModeService>();
			isOnline = m_gameModeService.CurrentGameMode is OnlineMultiplayerGameMode;
		}

		public int Inputfield(string question, BattleCreatorAssetUICellBase content, UnityAction OnFinish, UnityAction<BattleCreatorAssetUICellBase, string, System.Action> OnFinishCallback)
		{
			ResetButtonTexts();
			ResetElementConfiguration();
			m_YesButton.GetComponentInChildren<TextMeshProUGUI>().text = "Rename";
			int result = OpenPanel();
			m_CancelButton.onClick.RemoveAllListeners();
			m_CancelButton.onClick.AddListener(delegate
			{
				OnFinish?.Invoke();
				ClosePanel();
			});
			m_YesButton.onClick.RemoveAllListeners();
			m_YesButton.onClick.AddListener(async delegate
			{
				if (await ValidateTextInput(isMultiline: false))
				{
					OnFinishCallback(content, m_InputField.text, delegate
					{
						inputFieldParent.SetActive(value: false);
						OnFinish?.Invoke();
						ClosePanel();
					});
				}
			});
			inputFieldParent.SetActive(value: true);
			m_InputField.text = content?.ContentName;
			if (PlayerActions.Instance.InputType == InputType.Controller)
			{
				m_InputField.Select();
			}
			SetHeaderText(question);
			SetQuestionText(string.Empty);
			m_YesButton.gameObject.SetActive(value: true);
			m_CancelButton.gameObject.SetActive(value: true);
			this.OnPopUpOpen?.Invoke();
			SetupExplicitNavigation();
			return result;
		}

		private void SetContentDisplay(ContentDisplayParameters c)
		{
			m_contentDisplay.SetActive(value: true);
			m_unitDisplay.gameObject.SetActive(c.unit != null);
			m_factionDisplay.gameObject.SetActive(c.faction != null);
			m_campaignDisplay.gameObject.SetActive(c.campaign != null);
			m_battleDisplay.gameObject.SetActive(c.battle != null);
			m_levelDisplay.gameObject.SetActive(c.level != null);
			if (c.unit != null)
			{
				m_contentName.text = c.unit.Entity.Name;
				m_unitDisplay.Setup(c.unit);
			}
			else if (c.faction != null)
			{
				m_contentName.text = c.faction.Entity.Name;
				m_factionDisplay.Setup(c.faction);
			}
			else if (c.campaign != null)
			{
				m_contentName.text = c.campaign.Entity.Name;
				m_campaignDisplay.Setup(c.campaign);
			}
			else if (c.battle != null)
			{
				m_contentName.text = c.battle.Entity.Name;
				m_battleDisplay.Setup(c.battle);
			}
			else if (c.level != null)
			{
				m_contentName.text = c.level.Entity.Name;
				m_levelDisplay.Setup(c.level);
			}
		}

		public int Inputfield(InputFieldParameters p, ContentDisplayParameters c)
		{
			int result = Inputfield(p);
			SetContentDisplay(c);
			return result;
		}

		public int Inputfield(InputFieldParameters p, params string[] args)
		{
			ResetButtonTexts();
			ResetElementConfiguration();
			NavigableTMPTextInput inputField = (p.isMultiline ? m_MultilineInputField : m_InputField);
			inputField.GetComponentInChildren<TMP_InputField>().contentType = p.contentType;
			m_YesButton.GetComponentInChildren<LocalizeText>().LocaleID = p.yesButton ?? "BUTTON_CONFIRM";
			m_NoButton.GetComponentInChildren<LocalizeText>().LocaleID = p.noButton ?? "BUTTON_CANCEL";
			m_SingleLineHeader.LocaleID = p.singlelineHeader;
			m_SingleLineHeader.gameObject.SetActive(!string.IsNullOrEmpty(p.singlelineHeader));
			m_MultiLineHeader.LocaleID = p.multilineHeader;
			m_MultiLineHeader.gameObject.SetActive(!string.IsNullOrEmpty(p.multilineHeader));
			int result = OpenPanel();
			m_CancelButton.onClick.RemoveAllListeners();
			m_CancelButton.onClick.AddListener(delegate
			{
				ClosePanel();
			});
			m_YesButton.onClick.RemoveAllListeners();
			m_YesButton.onClick.AddListener(async delegate
			{
				if (!ServiceLocator.GetService<IPlatformUtils>().IsUIOpenOrLostFocus && ((p.inputValidation == null) ? (await ValidateTextInput(p.isMultiline)) : p.inputValidation(inputField.text)))
				{
					inputField.transform.parent.gameObject.SetActive(value: false);
					p.onFinish?.Invoke(inputField.text);
					ClosePanel();
				}
			});
			inputField.transform.parent.gameObject.SetActive(value: true);
			inputField.text = p.startInput ?? "";
			inputField.EnableTextInput();
			SetHeaderText(p.header, args);
			SetQuestionText(p.question, args);
			SetYesButtonScheme(p.isNegativeAction);
			m_YesButton.gameObject.SetActive(value: true);
			m_CancelButton.gameObject.SetActive(value: true);
			m_YesButton.Select();
			this.OnPopUpOpen?.Invoke();
			SetupExplicitNavigation();
			return result;
		}

		public void ProposeMarsSettings(ProjectMarsGameSettingsAsset proposedSettings, Action<ProjectMarsGameSettingsAsset> onAccept, System.Action onReject)
		{
			OpenProposePanel();
			ConfigureListenersForProjectMarsSettings(proposedSettings, onAccept, onReject);
			if (proposedSettings != null)
			{
				string localeID = string.Empty;
				if (proposedSettings.ProjectMarsTurnStyle == ProjectMarsGameSettingsAsset.TurnStyle.Normal)
				{
					localeID = "MP_SETTING_NORMAL";
				}
				else if (proposedSettings.ProjectMarsTurnStyle == ProjectMarsGameSettingsAsset.TurnStyle.Blind)
				{
					localeID = "MP_SETTING_BLINDMODE";
				}
				m_turnStyle.LocaleID = localeID;
				m_budget.text = proposedSettings.GameBudget.current.ToString();
				m_unitCap.text = proposedSettings.UnitCap.current.ToString();
			}
			this.OnPopUpOpen?.Invoke();
		}

		private void OpenProposePanel()
		{
			autoAcceptTimer = 0f;
			isOnline = m_gameModeService.CurrentGameMode is OnlineMultiplayerGameMode;
			m_proposalPanel.SetActive(value: true);
			if (isOnline)
			{
				timePlacementObject.SetActive(value: false);
				timeObject.SetActive(value: false);
			}
			UINavigationGroup component = m_proposalPanel.GetComponent<UINavigationGroup>();
			if (component != null)
			{
				component.MakeModal();
				component.EnableNavigation();
			}
		}

		private void CloseProposePanel()
		{
			autoAcceptTimer = 0f;
			m_proposalPanel.SetActive(value: false);
			UINavigationGroup component = m_proposalPanel.GetComponent<UINavigationGroup>();
			if (component != null)
			{
				component.DisableNavigation();
			}
			this.OnPopUpClose?.Invoke();
		}

		public override void OnUpdate()
		{
			base.OnUpdate();
			PlayerActions instance = PlayerActions.Instance;
			if (instance.m_accept.WasPressed && m_unlockPanelObject.activeSelf)
			{
				m_unlockButton.onClick?.Invoke();
			}
			if (IsPopupOpen)
			{
				if (m_proposalPanel.activeSelf)
				{
					if (instance.m_back.WasPressed)
					{
						m_rejectProposal.onClick.Invoke();
					}
					else if (instance.m_accept.WasPressed)
					{
						m_acceptProposal.onClick.Invoke();
					}
					autoAcceptTimer += Time.unscaledDeltaTime;
					m_autoAcceptTimer.text = (30f - autoAcceptTimer).ToString("F0");
					if (autoAcceptTimer >= 30f)
					{
						m_acceptProposal.onClick.Invoke();
					}
				}
				else if (instance.m_back.WasPressed)
				{
					if (resetProgressPanelOpen)
					{
						ClosePanel();
						return;
					}
					if (m_NoButton != null && m_NoButton.gameObject.activeSelf)
					{
						m_NoButton.onClick?.Invoke();
					}
					else if (m_CancelButton != null && m_CancelButton.gameObject.activeSelf)
					{
						m_CancelButton.onClick?.Invoke();
					}
				}
			}
			if (delaySelectable.IsDelayed && Time.frameCount > delaySelectable.FrameDelay)
			{
				EventSystem current = EventSystem.current;
				if (current != null && delaySelectable.GameObjectToSelect != null)
				{
					current.SetSelectedGameObject(delaySelectable.GameObjectToSelect);
					delaySelectable.GameObjectToSelect = null;
				}
				delaySelectable.IsDelayed = false;
			}
			if (m_PanelObject.activeSelf)
			{
				UpdatePreOpenFocusedObject();
			}
		}

		private async Task<bool> ValidateTextInput(bool isMultiline)
		{
			string text = (isMultiline ? m_MultilineInputField.text : m_InputField.text);
			if (string.IsNullOrEmpty(text))
			{
				return false;
			}
			if (DMProfanityFilter.ShouldFilter())
			{
				string text2 = await DMProfanityFilter.MaskCensoredWordsPlatformAsync(text);
				(isMultiline ? m_MultilineInputField : m_InputField).text = text2;
			}
			return true;
		}

		private void SetYesButtonScheme(bool isNegative)
		{
			m_YesButton.GetComponent<SelectableColorApplicator>()?.SetColorScheme(isNegative ? m_negativeButtonScheme : m_buttonScheme);
		}

		private void SetQuestionText(string text, params string[] args)
		{
			m_Question.gameObject.SetActive(!string.IsNullOrEmpty(text));
			m_Question.Args = args;
			m_Question.LocaleID = text;
			Canvas.ForceUpdateCanvases();
		}

		private void SetHeaderText(string text, params string[] args)
		{
			m_Header.Args = args;
			m_Header.LocaleID = text;
			Canvas.ForceUpdateCanvases();
		}

		public int PopUp(string popUptext, params string[] args)
		{
			return PopUp(popUptext, null, args);
		}

		public int PopUp(string popUptext, UnityAction onOk = null, params string[] args)
		{
			return PopUp(popUptext, onOk, -1f, invokeExistingDismissAction: true, args);
		}

		public int PopUp(string popUptext, ContentDisplayParameters c, UnityAction onOk = null, float fontSizeMax = -1f, bool invokeExistingDismissAction = true, params string[] args)
		{
			int result = PopUp(popUptext, onOk, fontSizeMax, invokeExistingDismissAction, args);
			SetContentDisplay(c);
			return result;
		}

		public int PopUp(string popUptext, UnityAction onOk = null, float fontSizeMax = -1f, bool invokeExistingDismissAction = true, params string[] args)
		{
			if (onOk == null)
			{
				onOk = delegate
				{
				};
			}
			fontSizeMax = ((fontSizeMax > 0f) ? fontSizeMax : m_headerFontSizeMax);
			int result = Choice(popUptext, "", onOk, "BUTTON_OK", fontSizeMax, isNegativeAction: false, invokeExistingDismissAction, args);
			System.Action action = this.OnPopUpOpen;
			if (action != null)
			{
				action();
				return result;
			}
			return result;
		}

		public int PopUp(string popUptext, UnityAction onOk = null, string okText = "", float fontSizeMax = -1f, bool invokeExistingDismissAction = true, params string[] args)
		{
			if (onOk == null)
			{
				onOk = delegate
				{
				};
			}
			fontSizeMax = ((fontSizeMax > 0f) ? fontSizeMax : m_headerFontSizeMax);
			int result = Choice(popUptext, "", onOk, okText, fontSizeMax, isNegativeAction: false, invokeExistingDismissAction, args);
			System.Action action = this.OnPopUpOpen;
			if (action != null)
			{
				action();
				return result;
			}
			return result;
		}

		public void PopUp(string popUpText, string yesText, string noText, string customText, UnityAction onYesEvent = null, UnityAction onNoEvent = null, UnityAction onCustomEvent = null, float fontSizeMax = -1f, bool isNegativeAction = false, bool hasCustomAction = false)
		{
			if (onYesEvent == null)
			{
				onYesEvent = delegate
				{
				};
			}
			if (onNoEvent == null)
			{
				onNoEvent = delegate
				{
				};
			}
			if (onCustomEvent == null)
			{
				onCustomEvent = delegate
				{
				};
			}
			fontSizeMax = ((fontSizeMax > 0f) ? fontSizeMax : m_headerFontSizeMax);
			Choice(popUpText, "", onYesEvent, onNoEvent, onCustomEvent, yesText, noText, customText, isNegativeAction, hasCustomAction);
			this.OnPopUpOpen?.Invoke();
		}

		public int WaitPopUp(string waitText, float maxSeconds = -1f, Func<bool> closeCondition = null, System.Action onFinished = null, params string[] args)
		{
			return WaitPopUpWithFocus(waitText, maxSeconds, closeCondition, onFinished, giveInputFocusToPopup: false, args);
		}

		public int WaitPopUpWithFocus(string waitText, float maxSeconds = -1f, Func<bool> closeCondition = null, System.Action onFinished = null, bool giveInputFocusToPopup = true, params string[] args)
		{
			return WaitPopUpWithFocus(waitText, invokeExistingDismissAction: true, maxSeconds, closeCondition, onFinished, giveInputFocusToPopup, args);
		}

		public int WaitPopUpWithFocus(string waitText, bool invokeExistingDismissAction, float maxSeconds = -1f, Func<bool> closeCondition = null, System.Action onFinished = null, bool giveInputFocusToPopup = true, params string[] args)
		{
			SetHeaderText(waitText, args);
			SetQuestionText(string.Empty, args);
			int result = OpenPanel(invokeExistingDismissAction);
			ResetElementConfiguration();
			if (giveInputFocusToPopup)
			{
				m_DummyButton.gameObject.SetActive(value: true);
				m_DummyButton.Select();
				EventSystem.current.SetSelectedGameObject(m_DummyButton.gameObject);
			}
			if (maxSeconds > 0f || (closeCondition != null && closeCondition()))
			{
				StartCoroutine(WaitForThen(maxSeconds, delegate
				{
					CloseWaitPopup();
					onFinished?.Invoke();
				}));
			}
			System.Action action = this.OnPopUpOpen;
			if (action != null)
			{
				action();
				return result;
			}
			return result;
		}

		private IEnumerator WaitForThen(float time)
		{
			yield return new WaitForSeconds(time);
		}

		private IEnumerator WaitForThen(float time, System.Action a)
		{
			yield return new WaitForSeconds(time);
			a();
		}

		public void CloseWaitPopup(bool restorePreviouslySelectedObject = true)
		{
			if (m_PanelObject.activeInHierarchy)
			{
				ClosePanel(restorePreviouslySelectedObject);
			}
		}

		public void CloseRulePopUps()
		{
			if (!isShowingDisconectPopUp && !isShowingWaitingToShutdownPopup)
			{
				CloseProposePanel();
				CloseWaitPopup();
			}
		}

		public void SetForDisconnectPopUp(bool showForDisconnect)
		{
			isShowingDisconectPopUp = showForDisconnect;
		}

		public void SetForWaitingToShutdownPopup(bool showForWaitingToShutdown)
		{
			isShowingWaitingToShutdownPopup = showForWaitingToShutdown;
		}

		private int Choice(string header, string question, UnityAction cancelEvent, string cancelText, float fontSizeMax, bool isNegativeAction = false, bool invokeExistingDismissAction = true, params string[] args)
		{
			ResetButtonTexts();
			m_CancelButton.GetComponentInChildren<LocalizeText>().LocaleID = cancelText;
			int result = OpenPanel(invokeExistingDismissAction);
			m_CancelButton.onClick.RemoveAllListeners();
			m_CancelButton.onClick.AddListener(delegate
			{
				ClosePanel();
			});
			if (cancelEvent != null)
			{
				m_CancelButton.onClick.AddListener(cancelEvent);
			}
			SetHeaderText(header, args);
			m_Header.Text.fontSizeMax = fontSizeMax;
			SetQuestionText(question, args);
			SetYesButtonScheme(isNegativeAction);
			ResetElementConfiguration();
			m_CancelButton.gameObject.SetActive(value: true);
			m_CancelButton.Select();
			EventSystem.current.SetSelectedGameObject(m_CancelButton.gameObject);
			SetupExplicitNavigation();
			return result;
		}

		public int Choice(string header, string question, ContentDisplayParameters c, UnityAction yesEvent, UnityAction noEvent, string yesText = "", string noText = "", bool isNegativeAction = false, bool invokeExistingDismissAction = true, params string[] args)
		{
			int result = Choice(header, question, yesEvent, null, noEvent, yesText, noText, string.Empty, isNegativeAction, invokeExistingDismissAction: false, args);
			SetContentDisplay(c);
			return result;
		}

		public int Choice(string header, string question, UnityAction yesEvent, UnityAction noEvent, params string[] args)
		{
			return Choice(header, question, yesEvent, noEvent, null, string.Empty, string.Empty, string.Empty, isNegativeAction: false, invokeExistingDismissAction: false, args);
		}

		public int Choice(string header, string question, UnityAction yesEvent, UnityAction noEvent, string yesText = "", string noText = "", bool isNegativeAction = false, params string[] args)
		{
			return Choice(header, question, yesEvent, noEvent, null, yesText, noText, string.Empty, isNegativeAction, invokeExistingDismissAction: false, args);
		}

		public int Choice(string header, string question, ContentDisplayParameters c, UnityAction yesEvent, UnityAction noEvent, UnityAction cancelEvent, string yesText = "", string noText = "", string cancelText = "", bool isNegativeAction = false, bool invokeExistingDismissAction = true, params string[] args)
		{
			int result = Choice(header, question, yesEvent, noEvent, cancelEvent, yesText, noText, cancelText, isNegativeAction, invokeExistingDismissAction, args);
			SetContentDisplay(c);
			return result;
		}

		public int Choice(string header, string question, UnityAction yesEvent, UnityAction noEvent, UnityAction cancelEvent, string yesText = "", string noText = "", string cancelText = "", bool isNegativeAction = false, bool invokeExistingDismissAction = true, params string[] args)
		{
			ResetButtonTexts();
			SetButtonTexts(yesText, noText, cancelText);
			ResetElementConfiguration();
			int result = OpenPanel(invokeExistingDismissAction);
			m_YesButton.onClick.RemoveAllListeners();
			m_YesButton.onClick.AddListener(delegate
			{
				ClosePanel();
			});
			if (yesEvent != null)
			{
				m_YesButton.onClick.AddListener(yesEvent);
			}
			m_NoButton.onClick.RemoveAllListeners();
			m_NoButton.onClick.AddListener(delegate
			{
				ClosePanel();
			});
			if (noEvent != null)
			{
				m_NoButton.onClick.AddListener(noEvent);
			}
			m_CancelButton.onClick.RemoveAllListeners();
			m_CancelButton.onClick.AddListener(delegate
			{
				ClosePanel();
			});
			if (cancelEvent != null)
			{
				m_CancelButton.onClick.AddListener(cancelEvent);
				m_CancelButton.gameObject.SetActive(value: true);
			}
			SetHeaderText(header, args);
			SetQuestionText(question, args);
			SetYesButtonScheme(isNegativeAction);
			m_YesButton.gameObject.SetActive(value: true);
			m_NoButton.gameObject.SetActive(value: true);
			if (PlayerActions.Instance.InputType == InputType.Controller)
			{
				QueueNextDelayedSelectable(m_YesButton.gameObject);
			}
			this.OnPopUpOpen?.Invoke();
			SetupExplicitNavigation();
			return result;
		}

		public int ResetProgressChoice(string header, string question, UnityAction yesEvent, UnityAction noEvent, UnityAction cancelEvent, string yesText = "", string noText = "", string cancelText = "")
		{
			resetProgressPanelOpen = true;
			return Choice(header, question, yesEvent, noEvent, cancelEvent, yesText, noText, cancelText, false, false, (string[])null);
		}

		public void ForcePopUpClose()
		{
			ClosePanel();
		}

		private void SetButtonTexts(string yes = "", string no = "", string cancel = "", string custom = "")
		{
			if (!string.IsNullOrWhiteSpace(yes))
			{
				m_YesButton.GetComponentInChildren<LocalizeText>().LocaleID = yes;
			}
			if (!string.IsNullOrWhiteSpace(no))
			{
				m_NoButton.GetComponentInChildren<LocalizeText>().LocaleID = no;
			}
			if (!string.IsNullOrWhiteSpace(cancel))
			{
				m_CancelButton.GetComponentInChildren<LocalizeText>().LocaleID = cancel;
			}
			if (!string.IsNullOrWhiteSpace(custom))
			{
				m_CustomButton.GetComponentInChildren<TextMeshProUGUI>().text = custom;
			}
		}

		private void ResetButtonTexts()
		{
			SetHeaderText(string.Empty);
			SetQuestionText(string.Empty);
			m_YesButton.GetComponentInChildren<LocalizeText>().LocaleID = "BUTTON_YES";
			m_NoButton.GetComponentInChildren<LocalizeText>().LocaleID = "BUTTON_NO";
			m_CancelButton.GetComponentInChildren<LocalizeText>().LocaleID = "BUTTON_CANCEL";
			m_CustomButton.GetComponentInChildren<TextMeshProUGUI>().text = "";
		}

		private void ResetElementConfiguration()
		{
			inputFieldParent.SetActive(value: false);
			multilineInputFieldParent.SetActive(value: false);
			m_YesButton.gameObject.SetActive(value: false);
			m_NoButton.gameObject.SetActive(value: false);
			m_CancelButton.gameObject.SetActive(value: false);
			m_CustomButton.gameObject.SetActive(value: false);
			m_SingleLineHeader.gameObject.SetActive(value: false);
			m_MultiLineHeader.gameObject.SetActive(value: false);
			m_DummyButton.gameObject.SetActive(value: false);
			m_contentDisplay.SetActive(value: false);
			m_unitDisplay.gameObject.SetActive(value: false);
			m_battleDisplay.gameObject.SetActive(value: false);
			m_factionDisplay.gameObject.SetActive(value: false);
			m_levelDisplay.gameObject.SetActive(value: false);
		}

		private int OpenPanel(bool invokeExistingDismissAction = true)
		{
			if (DMUIManager.Instance != null && DMUIManager.Instance.currentPanel != null)
			{
				DMUIManager.Instance.currentPanel.m_lastSelectedObject = EventSystem.current.currentSelectedGameObject;
			}
			m_LastFrameOpen = Time.frameCount;
			OpenId++;
			if (IsPopupOpen && invokeExistingDismissAction)
			{
				m_CancelButton.onClick?.Invoke();
			}
			EventSystem current = EventSystem.current;
			GameObject gameObject = ((current != null) ? current.currentSelectedGameObject : null);
			bool flag = gameObject != null && gameObject.transform.IsChildOf(base.transform);
			if (gameObject != null && !flag)
			{
				m_preOpenFocusedObject = gameObject;
			}
			else if (gameObject == null || !flag)
			{
				m_preOpenFocusedObject = null;
			}
			DMEditor instance = DMEditor.Instance;
			if (instance != null)
			{
				instance.SetControllerMode(DMEditor.ControllerMode.mouseCursor);
			}
			if (m_PanelNavigationGroup != null)
			{
				m_PanelNavigationGroup.MakeModal();
				m_PanelNavigationGroup.EnableNavigation();
			}
			StartCoroutine(FadeInBackground());
			m_PanelObject.SetActive(value: true);
			return OpenId;
		}

		private void ClosePanel(bool restorePreviouslySelectedObject = true)
		{
			if (resetProgressPanelOpen)
			{
				resetProgressPanelOpen = false;
			}
			if (m_LastFrameOpen != Time.frameCount)
			{
				m_PanelObject.SetActive(value: false);
			}
			EventSystem current = EventSystem.current;
			if (restorePreviouslySelectedObject && current != null)
			{
				current.SetSelectedGameObject(null);
				if (m_preOpenFocusedObject != null)
				{
					UINavigationGroup componentInParent = m_preOpenFocusedObject.GetComponentInParent<UINavigationGroup>();
					if (componentInParent == null || componentInParent.NavigationEnabled)
					{
						QueueNextDelayedSelectable(m_preOpenFocusedObject);
					}
				}
				else
				{
					DMEditor instance = DMEditor.Instance;
					if (instance != null)
					{
						instance.SetControllerMode(DMEditor.ControllerMode.firstPersonMovement);
					}
				}
			}
			m_preOpenFocusedObject = null;
			this.OnPopUpClose?.Invoke();
		}

		private void QueueNextDelayedSelectable(GameObject selectableGameObject)
		{
			delaySelectable.SetDelay(selectableGameObject);
		}

		private IEnumerator FadeInBackground()
		{
			Image image = m_background.GetComponent<Image>();
			image.color = Color.clear;
			float t = 0f;
			while (t <= 1f)
			{
				t += Time.unscaledDeltaTime * 9f;
				image.color = Color.Lerp(Color.clear, targetColor, t);
				yield return null;
			}
		}

		private void UpdatePreOpenFocusedObject()
		{
			EventSystem current = EventSystem.current;
			GameObject gameObject = ((current != null) ? current.currentSelectedGameObject : null);
			if (gameObject != null && gameObject != m_preOpenFocusedObject && !gameObject.transform.IsChildOf(base.transform))
			{
				m_preOpenFocusedObject = gameObject;
			}
		}

		public void OpenUnlockPanel(string unlockKey, Sprite unlockIcon)
		{
			m_secretUnlockQueue.Enqueue(new SecretUnlockInfo
			{
				m_description = unlockKey,
				m_icon = unlockIcon
			});
			StartCoroutine(DelayUnlockPanel(1f));
			this.OnPopUpOpen?.Invoke();
		}

		private IEnumerator DelayUnlockPanel(float delayTime)
		{
			if (m_delayUnlock)
			{
				yield break;
			}
			m_delayUnlock = true;
			yield return new WaitForSecondsRealtime(delayTime);
			m_delayUnlock = false;
			if (m_secretUnlockQueue.Count < 1)
			{
				yield break;
			}
			SecretUnlockInfo secretUnlockInfo = m_secretUnlockQueue.Dequeue();
			m_unlockText.LocaleID = secretUnlockInfo.m_description;
			m_unlockImage.sprite = secretUnlockInfo.m_icon;
			m_LastFrameOpen = Time.frameCount;
			m_unlockButton.onClick.RemoveAllListeners();
			m_unlockButton.onClick.AddListener(CloseUnlockPanel);
			m_unlockPanelObject.SetActive(value: true);
			m_unlockPanelObject.GetComponent<CodeAnimation>().PlayIn();
			AudioSource component = GetComponent<AudioSource>();
			if (component != null)
			{
				component.Play();
			}
			if (PlayerActions.Instance.InputType == InputType.Controller)
			{
				m_unlockButton.Select();
				if (m_BackButtonImage != null)
				{
					m_BackButtonImage.enabled = false;
				}
			}
			else if (m_BackButtonImage != null && !m_BackButtonImage.isActiveAndEnabled)
			{
				m_BackButtonImage.enabled = true;
			}
			m_pauseGame = true;
		}

		private void CloseUnlockPanel()
		{
			if (m_secretUnlockQueue.Count > 0)
			{
				SecretUnlockInfo secretUnlockInfo = m_secretUnlockQueue.Dequeue();
				m_unlockText.LocaleID = secretUnlockInfo.m_description;
				m_unlockImage.sprite = secretUnlockInfo.m_icon;
				AudioSource component = GetComponent<AudioSource>();
				if (component != null)
				{
					component.Play();
				}
			}
			else
			{
				m_pauseGame = false;
				m_unlockPanelObject.GetComponent<CodeAnimation>().PlayOut();
				this.OnPopUpClose?.Invoke();
			}
		}

		public void SetUnlockpanelActive(bool active)
		{
			m_unlockPanelObject.SetActive(active);
		}

		private void SetupExplicitNavigation()
		{
			List<Selectable> list = new List<Selectable>();
			foreach (Transform item in m_ButtonContainer)
			{
				Selectable component = item.GetComponent<Selectable>();
				if (component != null)
				{
					list.Add(component);
				}
			}
			if (list.Count <= 0)
			{
				return;
			}
			UIHelpers.CreateExplicitLinearNavigation(list, horizontal: true);
			Selectable selectable = (multilineInputFieldParent.activeSelf ? m_MultilineInputField : m_InputField);
			foreach (Selectable item2 in list)
			{
				if (!(item2 == null) && !(item2 == selectable))
				{
					Navigation navigation = item2.navigation;
					navigation.selectOnUp = selectable;
					item2.navigation = navigation;
				}
			}
		}

		private void OnInputSourceChanged(InputType type)
		{
			if (IsPopupOpen)
			{
				switch (type)
				{
				case InputType.Controller:
					SelectFirstItem();
					break;
				default:
					throw new ArgumentOutOfRangeException("type", type, null);
				case InputType.Keyboard:
				case InputType.Any:
					break;
				}
			}
		}

		private void SelectFirstItem()
		{
			foreach (Transform item in m_ButtonContainer)
			{
				Selectable component = item.GetComponent<Selectable>();
				if (component.IsActive())
				{
					component.Select();
					EventSystem.current.SetSelectedGameObject(m_CancelButton.gameObject);
					break;
				}
			}
		}

		private void OnEnable()
		{
			OnPopUpOpen += UpdateCanvas;
		}

		private void OnDisable()
		{
			InputService service = ServiceLocator.GetService<InputService>();
			if (service != null)
			{
				service.InputChanged -= OnInputSourceChanged;
			}
			OnPopUpOpen -= UpdateCanvas;
		}

		private void UpdateCanvas()
		{
			Canvas component = GetComponent<Canvas>();
			component.sortingOrder++;
			component.sortingOrder--;
		}

		private void ConfigureListenersForProjectMarsSettings(ProjectMarsGameSettingsAsset proposedSettings, Action<ProjectMarsGameSettingsAsset> onAccept, System.Action onReject)
		{
			m_acceptProposal.onClick.RemoveAllListeners();
			m_rejectProposal.onClick.RemoveAllListeners();
			m_acceptProposal.onClick.AddListener(delegate
			{
				onAccept(proposedSettings);
			});
			m_acceptProposal.onClick.AddListener(delegate
			{
				CloseProposePanel();
			});
			m_rejectProposal.onClick.AddListener(delegate
			{
				onReject();
			});
			m_rejectProposal.onClick.AddListener(delegate
			{
				CloseProposePanel();
			});
		}

		public void ClearDelaySelectableGameObject()
		{
			if (delaySelectable != null)
			{
				delaySelectable.GameObjectToSelect = null;
			}
		}
	}
}
