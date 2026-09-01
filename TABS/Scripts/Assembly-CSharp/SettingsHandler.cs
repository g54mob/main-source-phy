#define UNITY_EDITOR
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using BitCode.Networking;
using Landfall.TABS;
using Landfall.TABS.GameMode;
using Landfall.TABS.GameState;
using Landfall.TABS.Services;
using Landfall.TABS_Input;
using TFBGames;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SettingsHandler : UIComponentMainMenu, ISettingsItemHandler
{
	private enum GetBugsState
	{
		Idle = 0,
		NoUser = 1
	}

	private const string GetBugsIdleMessage = "SETTINGS_LABEL_BUGS_IDLE";

	private const string GetBugsNoUserMessage = "SETTINGS_LABEL_BUGS_NO_USER";

	private const string BugsUnavailableMessage = "SETTINGS_LABEL_BUGS_UNAVAILABLE";

	[SerializeField]
	private Toggle m_tabVideo;

	[SerializeField]
	private Toggle m_tabAudio;

	[SerializeField]
	private Toggle m_tabGameplay;

	[SerializeField]
	private Toggle m_tabControls;

	[SerializeField]
	private Toggle m_tabBugs;

	[SerializeField]
	private Toggle m_tabTwitch;

	[SerializeField]
	private List<GameObject> m_dots = new List<GameObject>();

	[SerializeField]
	private Button m_buttonDefault;

	[SerializeField]
	private Button m_buttonApply;

	[SerializeField]
	private Button m_buttonBack;

	[SerializeField]
	private Button m_buttonControlMap;

	[SerializeField]
	private ScrollRect m_scrollRect;

	[SerializeField]
	private SafeAreaMarker safeAreaMarker;

	[SerializeField]
	private Button m_buttonApplyGamepad;

	[SerializeField]
	private LocalizeText m_centerMessage;

	[SerializeField]
	[Tooltip("Button used for general actions for the current tab.")]
	private Button m_generalButton;

	[Space(20f)]
	[SerializeField]
	private GameObject m_optionsTemplate;

	[SerializeField]
	private GameObject m_sliderTemplate;

	[SerializeField]
	private GameObject m_controlTemplate;

	[SerializeField]
	private UICaptionedImageViewer m_ButtonMapImageViewer;

	[SerializeField]
	private GameObject toolTipObject;

	public GameObject groupObject;

	public CodeAnimation popUpAnim;

	public GameObject popUpGroup;

	public GameObject popUpButtonPrefab;

	[SerializeField]
	private UIMouseOver m_popupMouseOver;

	private GameStateManager m_gameStateManager;

	private GlobalSettingsHandler m_settingsHandler;

	private int m_currentOpenedTab;

	private ControlOptions m_controlOptions;

	private TwitchOptions m_twitchOptions;

	private PlayerActions m_playerActions;

	private Selectable popupSelectable;

	private IUISelectionItem selectedItem;

	private Toggle[] m_topButtons;

	private UIScaleJiggle[] m_topButtonsScaleJiggles;

	private List<Selectable> m_optionsList = new List<Selectable>();

	private InputService m_inputService;

	private ScaleJiggle m_applyButtonJiggle;

	private ScaleJiggle m_defaultButtonJiggle;

	private const float m_responsiveJiggleForce = 3f;

	private SettingsProfileManager m_SettingsProfileManager;

	private GameModeService m_gameModeService;

	private INetworkService m_networkService;

	private SettingsInstance[] m_emptySettings = new SettingsInstance[0];

	private TextMeshProUGUI m_generalButtonText;

	private AccountManager m_accountManager;

	private IDlcManagerService m_dlcManager;

	private GetBugsState m_getBugsState;

	private List<Selectable> m_currentPopupList;

	private List<SettingsInstance> m_disableSettings = new List<SettingsInstance>();

	public Button ButtonBack => m_buttonBack;

	public void SelectedItem(IUISelectionItem item)
	{
		selectedItem = item;
	}

	public void DeselectedItem(IUISelectionItem item)
	{
		selectedItem = null;
	}

	public bool IsPopupOpen()
	{
		if (popUpAnim.currentState != CodeAnimationInstance.AnimationUse.Out)
		{
			return popUpAnim.transform.localScale.magnitude > 0.1f;
		}
		return false;
	}

	protected override void Awake()
	{
		base.Awake();
		m_settingsHandler = ServiceLocator.GetService<GlobalSettingsHandler>();
		m_controlOptions = GetComponent<ControlOptions>();
		m_twitchOptions = GetComponent<TwitchOptions>();
		m_gameStateManager = ServiceLocator.GetService<GameStateManager>();
		m_SettingsProfileManager = ServiceLocator.GetService<SettingsProfileManager>();
		m_gameModeService = ServiceLocator.GetService<GameModeService>();
		m_playerActions = PlayerActions.Instance;
		m_topButtons = new Toggle[6] { m_tabVideo, m_tabAudio, m_tabGameplay, m_tabControls, m_tabBugs, m_tabTwitch };
		m_topButtonsScaleJiggles = new UIScaleJiggle[m_topButtons.Length];
		for (int i = 0; i < m_topButtonsScaleJiggles.Length; i++)
		{
			if (!(m_topButtons[i] == null))
			{
				m_topButtonsScaleJiggles[i] = m_topButtons[i].GetComponent<UIScaleJiggle>();
			}
		}
		for (int j = 0; j < m_topButtons.Length; j++)
		{
			if (m_topButtons[j] == null)
			{
				continue;
			}
			int tabIndex = j;
			m_topButtons[j].onValueChanged.AddListener(delegate(bool isOn)
			{
				if (isOn)
				{
					OpenSettingsMenu(tabIndex);
				}
			});
		}
		m_buttonDefault.onClick.AddListener(ResetSettings);
		m_buttonApply.onClick.AddListener(ApplySettings);
		m_inputService = ServiceLocator.GetService<InputService>();
		m_applyButtonJiggle = m_buttonApply.GetComponent<ScaleJiggle>();
		m_defaultButtonJiggle = m_buttonDefault.GetComponent<ScaleJiggle>();
		if (m_inputService != null)
		{
			m_inputService.InputChanged += OnInputSourceChanged;
		}
		if (m_generalButton != null)
		{
			m_generalButtonText = m_generalButton.GetComponentInChildren<TextMeshProUGUI>();
		}
		m_networkService = ServiceLocator.GetService<INetworkService>();
		ShowCenterMessage(visible: false, null);
		ShowGeneralButton(visible: false, null, null);
	}

	protected override void Start()
	{
		base.Start();
		m_accountManager = ServiceLocator.GetService<AccountManager>();
		m_dlcManager = ServiceLocator.GetService<IDlcManagerService>();
		if (m_dlcManager != null)
		{
			m_dlcManager.GotAccessToDlc += OnGotAccessToDlc;
			m_dlcManager.LostAccessToAllDlc += OnLostAccessToAllDlc;
		}
		OpenSettingsMenu(0);
		if (NetworkBattleUICloser.Instance != null)
		{
			NetworkBattleUICloser.Instance.RegisterComponent(this, OnMultiplayerMatchEnded);
		}
	}

	private void EnableTab(Toggle tab, bool enabled)
	{
		tab.gameObject.SetActive(enabled);
		int num = m_topButtons.ToList().IndexOf(tab);
		m_dots[num - 1].SetActive(enabled);
	}

	protected override void OnEnable()
	{
		base.OnEnable();
		if (base.IsActive && autoHighlightSelectableWhenActive)
		{
			m_topButtons[m_currentOpenedTab].Select();
		}
		bool? flag = null;
		if (m_gameModeService != null && !m_gameModeService.IsCurrentBaseGameModeType<OnlineMultiplayerGameMode>())
		{
			flag = Bugs._DLC_ACTIVATED;
		}
		if (m_gameModeService != null && !m_gameModeService.IsCurrentBaseGameModeType<LocalMultiplayerGameMode>())
		{
			flag = Bugs._DLC_ACTIVATED;
		}
		if (!Bugs.EnabledOnPlatform())
		{
			flag = false;
		}
		if (flag.HasValue)
		{
			EnableTab(m_tabBugs, flag.Value);
		}
		EnableTab(m_tabTwitch, enabled: true);
	}

	protected override void OnOpen()
	{
		base.OnOpen();
		BaseGameMode currentGameMode = m_gameModeService.CurrentGameMode;
		if (currentGameMode is CampaignGameMode || currentGameMode is SandboxGameMode || currentGameMode is LocalMultiplayerGameMode)
		{
			SetPause();
		}
	}

	protected override void OnClose()
	{
		base.OnClose();
		if (IsPopupOpen())
		{
			CloseSettingsPopUp();
		}
		ServiceLocator.GetService<InputService>()?.CheckPlayerActionsAgainstUiInputMode();
		PlayerActions.Instance.SaveBindings(savePlayerPrefsImmediately: true);
	}

	private void OnInputSourceChanged(InputType type)
	{
		switch (type)
		{
		case InputType.Controller:
		{
			autoHighlightSelectableWhenActive = true;
			if (base.IsActive && m_currentPopupList != null)
			{
				m_currentPopupList[0].Select();
			}
			bool flag = GlobalSettingsHandler.CurrentPlatform == SettingsInstance.Platform.Desktop;
			if (m_buttonApplyGamepad != null)
			{
				bool flag2 = m_currentOpenedTab == 0;
				m_buttonApplyGamepad.gameObject.SetActive(flag && flag2);
			}
			break;
		}
		case InputType.Keyboard:
		case InputType.Any:
			ResetTabButtonScales();
			autoHighlightSelectableWhenActive = false;
			break;
		default:
			throw new ArgumentOutOfRangeException("type", type, null);
		}
	}

	private void ResetCurrentToDefault()
	{
		SettingsInstance[] settingsForTab = GetSettingsForTab(m_currentOpenedTab);
		for (int i = 0; i < settingsForTab.Length; i++)
		{
			settingsForTab[i].ResetToDefault();
		}
		if (m_currentOpenedTab == 3)
		{
			m_controlOptions.ResetToDefault();
			m_controlOptions.UpdateControls();
		}
		else if (m_currentOpenedTab == 5)
		{
			m_twitchOptions.ResetToDefault();
		}
	}

	private void ApplySettings()
	{
		int currentValue = m_settingsHandler.GetSettingsInstance("VIDEO_RESOLUTION").currentValue;
		int currentValue2 = m_settingsHandler.GetSettingsInstance("VIDEO_WINDOW_MODE").currentValue;
		Resolution[] resolutionsWithHighestRefreshRate = GlobalSettingsHandler.GetResolutionsWithHighestRefreshRate();
		currentValue = Mathf.Clamp(currentValue, 0, resolutionsWithHighestRefreshRate.Length - 1);
		Resolution resolution = resolutionsWithHighestRefreshRate[currentValue];
		FullScreenMode fullScreenMode = GlobalSettingsHandler.GetFullScreenMode(currentValue2);
		Screen.SetResolution(resolution.width, resolution.height, fullScreenMode);
	}

	private void InitSettingsButton(GameObject newButton)
	{
		newButton.transform.localScale = Vector3.one;
		newButton.SetActive(value: true);
	}

	public void ClickSettingsPopUp(SettingsPopUpButton button)
	{
		button.targetSettingsButton.UpdateValue(button.value);
		CloseSettingsPopUp();
		if (!Input.GetMouseButtonUp(0))
		{
			popupSelectable.Select();
		}
	}

	protected override void Update()
	{
		base.Update();
		if (MainMenuStateHandler.Instance != null && MainMenuStateHandler.Instance.m_CurrentMenuState != MenuState.Settings && !EscapeMenuHandler.InSettings)
		{
			return;
		}
		if (IsPopupOpen() && ((Input.GetMouseButtonUp(0) && !m_popupMouseOver.IsMouseOver) || m_playerActions.m_cycleTabs.WasPressed))
		{
			CloseSettingsPopUp();
		}
		if (m_playerActions.m_cycleTabs.WasPressed)
		{
			do
			{
				int num = m_topButtons.Length;
				int num2 = Mathf.RoundToInt(m_playerActions.m_cycleTabs.Value);
				int currentOpenedTab = m_currentOpenedTab;
				m_currentOpenedTab = (m_currentOpenedTab + num2) % num;
				m_currentOpenedTab = ((m_currentOpenedTab < 0) ? (num - 1) : m_currentOpenedTab);
				m_topButtons[m_currentOpenedTab].isOn = true;
				FreezeTopTabButtonScaleJiggle(m_currentOpenedTab, currentOpenedTab);
			}
			while (!m_topButtons[m_currentOpenedTab].gameObject.activeSelf);
		}
		if (m_playerActions.m_applySettings.WasPressed)
		{
			if (m_currentOpenedTab == 0)
			{
				m_buttonApply.onClick.Invoke();
			}
			m_applyButtonJiggle.AddForce(3f);
		}
		if (m_playerActions.m_resetSettings.WasPressed)
		{
			ResetSettings();
		}
		if (m_playerActions.m_back.WasPressed)
		{
			OkGoBack();
		}
		if (selectedItem != null)
		{
			selectedItem.HandleInput(m_playerActions);
		}
	}

	private void ResetSettings()
	{
		m_defaultButtonJiggle.AddForce(3f);
		ResetCurrentToDefault();
	}

	private void FreezeTopTabButtonScaleJiggle(int currentIndex, int previousIndex)
	{
		UIScaleJiggle uIScaleJiggle = m_topButtonsScaleJiggles[currentIndex];
		if (uIScaleJiggle != null)
		{
			uIScaleJiggle.FreezeScaleJiggle();
		}
		UIScaleJiggle uIScaleJiggle2 = m_topButtonsScaleJiggles[previousIndex];
		if (uIScaleJiggle2 != null)
		{
			uIScaleJiggle2.UnFreezeScaleJiggle();
		}
	}

	private void ResetTabButtonScales()
	{
		UIScaleJiggle[] topButtonsScaleJiggles = m_topButtonsScaleJiggles;
		foreach (UIScaleJiggle uIScaleJiggle in topButtonsScaleJiggles)
		{
			if (uIScaleJiggle != null)
			{
				uIScaleJiggle.UnFreezeScaleJiggle();
			}
		}
	}

	public void OpenSettingsPopUp(SettingsInstance setting, MenuSettingsButton settingsButton)
	{
		if (selectedItem != null)
		{
			popupSelectable = selectedItem.Selectable;
		}
		popUpAnim.PlayIn();
		for (int i = 0; i < popUpGroup.transform.childCount; i++)
		{
			UnityEngine.Object.Destroy(popUpGroup.transform.GetChild(i).gameObject);
		}
		m_currentPopupList = new List<Selectable>();
		for (int num = setting.options.Length - 1; num >= 0; num--)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate(popUpButtonPrefab, popUpGroup.transform);
			InitSettingsButton(gameObject);
			SettingsPopUpButton component = gameObject.GetComponent<SettingsPopUpButton>();
			component.settingsHandler = this;
			component.value = num;
			component.targetSettingsButton = settingsButton;
			component.GetComponentInChildren<LocalizeText>().LocaleID = setting.options[num];
			Selectable component2 = gameObject.GetComponent<Selectable>();
			m_currentPopupList.Add(component2);
		}
		if (m_currentPopupList.Count > 0)
		{
			UIHelpers.CreateExplicitLinearNavigation(m_currentPopupList, horizontal: false);
			if (base.IsActive && PlayerActions.Instance.InputType == InputType.Controller)
			{
				m_currentPopupList[0].Select();
			}
		}
		StartCoroutine(ResetScroll());
	}

	public void OnShowAnimation()
	{
		OpenSettingsMenu(m_currentOpenedTab);
	}

	private IEnumerator ResetScroll()
	{
		yield return new WaitForEndOfFrame();
		(popUpGroup.transform as RectTransform).anchoredPosition = Vector2.zero;
	}

	public void CloseSettingsPopUp()
	{
		popUpAnim.PlayOut();
		if (!Input.GetMouseButtonUp(0))
		{
			if (popupSelectable != null)
			{
				popupSelectable.Select();
			}
			m_currentPopupList = null;
		}
	}

	public void ToggleToolTip(SettingsInstance settingsInstance, bool openState)
	{
		toolTipObject.GetComponentInChildren<LocalizeText>().LocaleID = (openState ? settingsInstance.toolTip : string.Empty);
		toolTipObject.SetActive(openState);
	}

	public void ToggleDisabledToolTip(SettingsInstance settingsInstance, bool openState)
	{
		toolTipObject.GetComponentInChildren<LocalizeText>().LocaleID = (openState ? settingsInstance.DisabledMessageKey : string.Empty);
		toolTipObject.SetActive(openState);
	}

	private SettingsInstance[] GetSettingsForTab(int tabID)
	{
		switch (tabID)
		{
		case 0:
			return m_settingsHandler.VideoSettings;
		case 1:
			return m_settingsHandler.AudioSettings;
		case 2:
			return m_settingsHandler.GameplaySettings;
		case 3:
			return m_settingsHandler.ControlSettings;
		case 4:
			if (!m_tabBugs.gameObject.activeSelf)
			{
				return m_settingsHandler.TwitchSettings;
			}
			if (!Bugs.EnabledOnPlatform())
			{
				return m_emptySettings;
			}
			if (m_gameModeService == null)
			{
				if (!Bugs._DLC_ACTIVATED)
				{
					return m_emptySettings;
				}
				return m_settingsHandler.BugsSettings;
			}
			if (m_gameModeService.IsCurrentBaseGameModeType<OnlineMultiplayerGameMode>())
			{
				ShowCenterMessage(visible: true, "SETTINGS_LABEL_BUGS_UNAVAILABLE");
				return m_emptySettings;
			}
			if (m_gameModeService.IsCurrentBaseGameModeType<LocalMultiplayerGameMode>())
			{
				ShowCenterMessage(visible: true, "SETTINGS_LABEL_BUGS_UNAVAILABLE");
				return m_emptySettings;
			}
			if (!Bugs._DLC_ACTIVATED)
			{
				return m_emptySettings;
			}
			return m_settingsHandler.BugsSettings;
		case 5:
			return m_settingsHandler.TwitchSettings;
		default:
			return m_emptySettings;
		}
	}

	private void OpenSettingsMenu(int tabID)
	{
		ShowCenterMessage(visible: false, null);
		m_currentOpenedTab = tabID;
		if (m_topButtons[m_currentOpenedTab] == null)
		{
			return;
		}
		SettingsInstance[] settingsForTab = GetSettingsForTab(tabID);
		OpenSettingsMenu(tabID, settingsForTab);
		if (tabID == 3)
		{
			m_controlOptions.SetupControls();
		}
		m_buttonApply.gameObject.SetActive(tabID == 0);
		if (m_buttonApplyGamepad != null)
		{
			m_buttonApplyGamepad.gameObject.SetActive(tabID == 0);
		}
		if (tabID == 5)
		{
			m_twitchOptions.SetupTwitch();
		}
		if (tabID == 4)
		{
			bool flag = false;
			if (m_gameModeService != null && m_gameModeService.IsCurrentBaseGameModeType<OnlineMultiplayerGameMode>())
			{
				flag = true;
				ShowCenterMessage(visible: true, "SETTINGS_LABEL_BUGS_UNAVAILABLE");
			}
			if (m_gameModeService != null && m_gameModeService.IsCurrentBaseGameModeType<LocalMultiplayerGameMode>())
			{
				flag = true;
				ShowCenterMessage(visible: true, "SETTINGS_LABEL_BUGS_UNAVAILABLE");
			}
			if (!flag)
			{
				if (!Bugs._DLC_ACTIVATED && m_dlcManager != null && m_accountManager != null)
				{
					if (m_dlcManager.NeedsUserForDlc && m_accountManager.ActiveAccount == null)
					{
						SetGetBugsState(GetBugsState.NoUser);
					}
					else
					{
						SetGetBugsState(GetBugsState.Idle);
					}
				}
				else
				{
					ShowCenterMessage(visible: false, null);
				}
			}
		}
		if (base.IsActive)
		{
			m_topButtons[m_currentOpenedTab].Select();
		}
		m_optionsList.Clear();
		IUISelectionItem[] componentsInChildren = groupObject.GetComponentsInChildren<IUISelectionItem>();
		foreach (IUISelectionItem iUISelectionItem in componentsInChildren)
		{
			_ = iUISelectionItem.Selectable.gameObject;
			if (!(iUISelectionItem.Selectable != null))
			{
				continue;
			}
			m_optionsList.Add(iUISelectionItem.Selectable);
			MenuSettingsButton componentInParent = iUISelectionItem.Selectable.GetComponentInParent<MenuSettingsButton>();
			if (!(componentInParent != null))
			{
				continue;
			}
			ToggleToolTip(componentInParent.settingsInstance, openState: false);
			if (componentInParent.settingsInstance.m_settingsKey == SafeArea.SAFE_AREA_SETTINGS_KEY)
			{
				if (safeAreaMarker != null)
				{
					safeAreaMarker.LinkSliderToSafeAreaMarker(componentInParent);
				}
				else
				{
					Debug.LogError("Safe space markers missing");
				}
			}
		}
		UpdateButtonNavigation();
	}

	private void OpenSettingsMenu(int tabID, SettingsInstance[] settingsToOpen)
	{
		for (int num = groupObject.transform.childCount - 1; num >= 0; num--)
		{
			GameObject obj = groupObject.transform.GetChild(num).gameObject;
			obj.transform.SetParent(null);
			UnityEngine.Object.Destroy(obj);
		}
		m_disableSettings.Clear();
		BaseGameMode currentGameMode = m_gameModeService.CurrentGameMode;
		bool flag = currentGameMode?.Brush != null && currentGameMode.Brush.IsRunningPlacementRoutine;
		bool flag2 = true;
		bool flag3 = true;
		if (currentGameMode is OnlineMultiplayerGameMode)
		{
			flag3 = false;
			flag2 = false;
		}
		if (!flag3)
		{
			HideSetting("GAMEPLAY_RESTRICT_UNITS");
			HideSetting("GAMEPLAY_DELAYGAMEOVER");
			HideSetting("GAMEPLAY_EYES");
			HideSetting("GAMEPLAY_HIDE_CLOTHES");
			HideSetting("ALLOW_UGC");
		}
		if (!flag2 || (m_gameStateManager.GameState == GameState.BattleState && MainMenuStateHandler.Instance == null) || flag)
		{
			HideSetting("GAMEPLAY_FLIP_COLORS");
		}
		if (!(m_SettingsProfileManager?.CurrentSettingsProfile == null))
		{
			SettingsProfileManager settingsProfileManager = m_SettingsProfileManager;
			if (settingsProfileManager != null && settingsProfileManager.CurrentSettingsProfile.UserPlacementMaxUnits.HasValue)
			{
				goto IL_0142;
			}
		}
		HideSetting("GAMEPLAY_OVERRIDE_PLACEMENT_LIMIT");
		goto IL_0142;
		IL_0142:
		if (!m_gameModeService.IsCurrentBaseGameModeType<MainMenuGameMode>())
		{
			HideSetting("UI_INPUT_MODE");
		}
		SettingsInstance.Platform currentPlatform = GlobalSettingsHandler.CurrentPlatform;
		foreach (SettingsInstance settingsInstance in settingsToOpen)
		{
			bool flag4 = m_disableSettings.Contains(settingsInstance);
			GameModeState currentGameModeState = CampaignPlayerDataHolder.CurrentGameModeState;
			GameModeState excludeFromGameModeState = settingsInstance.ExcludeFromGameModeState;
			if (!(settingsInstance.m_hideSetting || (settingsInstance.m_platform & currentPlatform) == 0 || flag4) && (excludeFromGameModeState & currentGameModeState) == 0)
			{
				GameObject gameObject = UnityEngine.Object.Instantiate((settingsInstance.settingsType == SettingsInstance.SettingsType.Options) ? m_optionsTemplate : m_sliderTemplate, groupObject.transform);
				InitSettingsButton(gameObject);
				gameObject.GetComponent<MenuSettingsButton>().Init(settingsInstance);
			}
		}
		void HideSetting(string settingKey)
		{
			if (!(m_settingsHandler == null))
			{
				SettingsInstance settingsInstance2 = m_settingsHandler.GetSettingsInstance(settingKey);
				if (settingsInstance2 != null)
				{
					m_disableSettings.Add(settingsInstance2);
				}
			}
		}
	}

	private void EveryOtherPattern()
	{
		int childCount = groupObject.transform.childCount;
		for (int i = 0; i < childCount; i++)
		{
			groupObject.transform.GetChild(i).GetComponent<Image>().enabled = i % 2 == 0;
		}
	}

	private void UpdateButtonNavigation()
	{
		int count = m_optionsList.Count;
		Button button = null;
		if (m_buttonApply != null && m_buttonApply.gameObject.activeInHierarchy)
		{
			button = m_buttonApply;
		}
		else if (m_generalButton != null && m_generalButton.gameObject.activeInHierarchy)
		{
			button = m_generalButton;
		}
		Selectable selectable = ((count > 0) ? m_optionsList[0] : null);
		if (count > 1)
		{
			_ = m_optionsList[count - 1];
		}
		if (m_currentOpenedTab >= 0 && m_currentOpenedTab < m_topButtons.Length)
		{
			_ = m_topButtons[m_currentOpenedTab];
		}
		Toggle[] topButtons = m_topButtons;
		foreach (Toggle toggle in topButtons)
		{
			if (!(toggle == null))
			{
				Navigation navigation = toggle.navigation;
				navigation.selectOnDown = ((selectable != null) ? selectable : ((button != null) ? button : m_buttonDefault));
				toggle.navigation = navigation;
			}
		}
		UIHelpers.CreateExplicitLinearNavigation(m_optionsList, horizontal: false);
	}

	public void AddSafeAreaMarkers(SafeAreaMarker safeAreaMarker)
	{
		this.safeAreaMarker = safeAreaMarker;
	}

	protected override void OnDestroy()
	{
		base.OnDestroy();
		if (m_inputService != null)
		{
			m_inputService.InputChanged -= OnInputSourceChanged;
		}
		if (m_dlcManager != null)
		{
			m_dlcManager.GotAccessToDlc -= OnGotAccessToDlc;
			m_dlcManager.LostAccessToAllDlc -= OnLostAccessToAllDlc;
		}
		if (NetworkBattleUICloser.Instance != null)
		{
			NetworkBattleUICloser.Instance.UnregisterComponent(this);
		}
	}

	private void ShowCenterMessage(bool visible, string message)
	{
		if (!(m_centerMessage == null))
		{
			bool flag = !string.IsNullOrEmpty(message);
			visible = flag && visible;
			m_centerMessage.LocaleID = (flag ? message : string.Empty);
			if (m_centerMessage.gameObject.activeSelf != visible)
			{
				m_centerMessage.gameObject.SetActive(visible);
			}
		}
	}

	private void ShowGeneralButton(bool visible, string text, UnityAction clickAction)
	{
		if (!(m_generalButton == null))
		{
			if (m_generalButtonText != null)
			{
				m_generalButtonText.text = (string.IsNullOrEmpty(text) ? string.Empty : text);
			}
			m_generalButton.onClick.RemoveAllListeners();
			if (clickAction != null)
			{
				m_generalButton.onClick.AddListener(clickAction);
			}
			if (m_generalButton.gameObject.activeSelf != visible)
			{
				m_generalButton.gameObject.SetActive(visible);
			}
		}
	}

	private void SetGetBugsState(GetBugsState newState)
	{
		m_getBugsState = newState;
		if (m_currentOpenedTab == 4 && m_tabBugs.gameObject.activeSelf)
		{
			string message = null;
			switch (m_getBugsState)
			{
			case GetBugsState.Idle:
				message = "SETTINGS_LABEL_BUGS_IDLE";
				break;
			case GetBugsState.NoUser:
				message = "SETTINGS_LABEL_BUGS_NO_USER";
				break;
			}
			ShowCenterMessage(visible: true, message);
		}
	}

	private void OnGotAccessToDlc(string dlcId)
	{
		if (m_currentOpenedTab == 4 && m_dlcManager != null && !string.IsNullOrEmpty(dlcId) && dlcId.Equals(m_dlcManager.AprilFoolsBugsDlcId, StringComparison.InvariantCultureIgnoreCase))
		{
			OpenSettingsMenu(4);
		}
	}

	private void OnLostAccessToAllDlc()
	{
		if (m_currentOpenedTab == 4)
		{
			OpenSettingsMenu(4);
		}
	}

	public void OkGoBack()
	{
		if (!GlobalSettingsHandler.DoesPlatformAllowRapidFileSystemAccess())
		{
			GlobalSettingsHandler service = ServiceLocator.GetService<GlobalSettingsHandler>();
			if (service != null)
			{
				service.SaveAllSettings();
			}
		}
		DisableNavigation();
		ResetTabButtonScales();
		base.Close();
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

	protected override void OnReceivedInvitation(IGameInvitation invite)
	{
		SafeClose();
	}

	private void OnMultiplayerMatchEnded()
	{
		SafeClose();
	}

	private void SafeClose()
	{
		if (base.IsActive)
		{
			OkGoBack();
		}
	}
}
