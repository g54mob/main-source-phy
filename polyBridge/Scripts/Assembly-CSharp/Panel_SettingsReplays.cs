using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Panel_SettingsReplays : MonoBehaviour
{
	public Transform m_Content;

	[Header("Replays")]
	public Toggle m_CuratedReplaysToggle;

	public Toggle m_ReplaysToggle;

	public TMP_Dropdown m_ReplayQualityDropdown;

	public Slider m_ReplayLengthSlider;

	public TextMeshProUGUI m_ReplayLengthPreview;

	public TextMeshProUGUI m_ReplayLocation;

	public Button m_OpenReplaysFolderButton;

	public Button m_ChangeReplaysFolderButton;

	[Header("Footer")]
	public Button m_ResetToDefaults;

	public GameObject m_NoSyncWarning;

	[Header("SteamDeck")]
	public GameObject[] m_HideOnSteamDeck;

	[Header("Child Panels")]
	public Panel_PickFolder m_PickFolder;

	[Header("Rows")]
	public SettingsRow m_CuratedReplaysToggleRow;

	public SettingsRow m_ReplaysToggleRow;

	public SettingsRow m_ReplayQualityDropdownRow;

	public SettingsRow m_ReplayLengthSliderRow;

	private static string[] m_AllowedExtensionsDir = new string[0];

	private PointerEvents m_CuratedReplaysTogglePointerEvents;

	private PointerEvents m_ReplaysTogglePointerEvents;

	public void Start()
	{
		m_CuratedReplaysTogglePointerEvents = m_CuratedReplaysToggle.GetComponent<PointerEvents>();
		m_CuratedReplaysTogglePointerEvents.RegisterOnClickedDelegate(InterfaceAudio.PlayToggleAudio);
		m_ReplaysTogglePointerEvents = m_ReplaysToggle.GetComponent<PointerEvents>();
		m_ReplaysTogglePointerEvents.RegisterOnClickedDelegate(InterfaceAudio.PlayToggleAudio);
		m_ReplayQualityDropdown.alphaFadeSpeed = 0f;
		m_ReplayLengthSlider.minValue = Replays.MIN_SECONDS_PER_REPLAY;
		m_ReplayLengthSlider.maxValue = Replays.MAX_SECONDS_PER_REPLAY;
		m_OpenReplaysFolderButton.onClick.AddListener(OnOpenReplaysFolder);
		m_ChangeReplaysFolderButton.onClick.AddListener(OnChangeReplaysFolder);
		m_ResetToDefaults.onClick.AddListener(OnResetToDefaults);
		m_PickFolder.gameObject.SetActive(value: false);
		SetRowCallbacks();
	}

	public void OnEnableManual()
	{
		m_ReplayLengthSlider.onValueChanged.AddListener(OnReplayLengthChanged);
		PopulateReplayQualityDropdown();
		m_ReplayQualityDropdown.value = (int)Profiles.m_ActiveProfile.m_ReplayQuality;
		m_CuratedReplaysToggle.isOn = Profiles.m_ActiveProfile.m_CuratedReplays;
		m_ReplaysToggle.isOn = Profiles.m_ActiveProfile.m_Replays;
		m_ReplayLengthSlider.value = Profiles.m_ActiveProfile.m_ReplayLengthSeconds;
		m_ReplayLengthPreview.text = Localize.Get("UI_SETTINGS_REPLAY_SECONDS", GetReplayLengthSecondsFromSlider().ToString());
		m_ReplayLocation.text = Replays.GetReplaysPath();
		GameUI.m_Instance.m_Settings.SetRowsColor(m_Content);
		GameObject[] hideOnSteamDeck = m_HideOnSteamDeck;
		for (int i = 0; i < hideOnSteamDeck.Length; i++)
		{
			hideOnSteamDeck[i].SetActive(!Game.IsRunningOnSteamDeck());
		}
		m_NoSyncWarning.SetActive(!Game.IsMobile());
	}

	public void OnDisableManual()
	{
	}

	public void OnEnable()
	{
		GameUI.m_Instance.m_Settings.ClearRows();
		GameUI.m_Instance.m_Settings.AddRow(m_CuratedReplaysToggleRow);
		GameUI.m_Instance.m_Settings.AddRow(m_ReplaysToggleRow);
		GameUI.m_Instance.m_Settings.AddRow(m_ReplayQualityDropdownRow);
		GameUI.m_Instance.m_Settings.AddRow(m_ReplayLengthSliderRow);
	}

	public void Apply(bool updateProfileOnly)
	{
		bool flag = Profiles.m_ActiveProfile.m_ReplayQuality != (AsyncCaptureQuality)m_ReplayQualityDropdown.value;
		Profiles.m_ActiveProfile.m_CuratedReplays = m_CuratedReplaysToggle.isOn;
		Profiles.m_ActiveProfile.m_Replays = m_ReplaysToggle.isOn;
		Profiles.m_ActiveProfile.m_ReplayQuality = (AsyncCaptureQuality)m_ReplayQualityDropdown.value;
		Profiles.m_ActiveProfile.m_ReplayLengthSeconds = GetReplayLengthSecondsFromSlider();
		Profiles.m_ActiveProfile.m_ReplaysFolderOverride = m_ReplayLocation.text;
		if (!updateProfileOnly && Profiles.m_ActiveProfile.m_Replays && (!Cameras.m_AsyncCapture.m_Initialized || flag))
		{
			Cameras.m_AsyncCapture.Init(Profiles.m_ActiveProfile.m_ReplayQuality, Profiles.m_ActiveProfile.m_ReplayLengthSeconds);
		}
	}

	public void OnResetToDefaults()
	{
		InterfaceAudio.Play("ui_settings_reset");
		PopUpMessage.DisplayConfirmation(Localize.Get("POPUP_RESET_REPLAY"), useYesNoLabels: true, ConfirmOnDefaults);
	}

	private void OnReplayLengthChanged(float value)
	{
		m_ReplayLengthPreview.text = Localize.Get("UI_SETTINGS_REPLAY_SECONDS", GetReplayLengthSecondsFromSlider().ToString());
	}

	private int GetReplayLengthSecondsFromSlider()
	{
		return (int)m_ReplayLengthSlider.value;
	}

	private void PopulateReplayQualityDropdown()
	{
		List<string> listLocalizedQualityLevelNames = Replays.GetListLocalizedQualityLevelNames();
		m_ReplayQualityDropdown.ClearOptions();
		m_ReplayQualityDropdown.AddOptions(listLocalizedQualityLevelNames);
	}

	private void OnOpenReplaysFolder()
	{
		InterfaceAudio.Play("ui_menubar_gen_on");
		Utils.OpenLocalPath(Replays.GetReplaysPath());
	}

	private void OnChangeReplaysFolder()
	{
		InterfaceAudio.Play("ui_window_open");
		string text = Replays.GetReplaysPath();
		if (!Utils.DirectoryExists(text))
		{
			text = Application.persistentDataPath;
		}
		m_PickFolder.Open(text, m_AllowedExtensionsDir, Localize.Get("UI_SELECT_REPLAYS_FOLDER"), PickedFolderCallback);
	}

	private void PickedFolderCallback(string fullpath)
	{
		if (!string.IsNullOrEmpty(fullpath))
		{
			m_ReplayLocation.text = fullpath;
			Profiles.m_ActiveProfile.m_ReplaysFolderOverride = m_ReplayLocation.text;
		}
	}

	private void ConfirmOnDefaults()
	{
		m_CuratedReplaysToggle.isOn = false;
		m_ReplaysToggle.isOn = true;
		m_ReplayQualityDropdown.value = 2;
		m_ReplayLengthSlider.value = Replays.DEFAULT_SECONDS_PER_REPLAY;
		m_ReplayLocation.text = Replays.GetDefaultReplaysPath();
	}

	private void SetRowCallbacks()
	{
		m_CuratedReplaysToggleRow.m_Action = CuratedReplaysToggleCallback;
		m_ReplaysToggleRow.m_Action = ReplaysToggleCallback;
		m_ReplayQualityDropdownRow.m_Action = ReplayQualityDropdownCallback;
		m_ReplayLengthSliderRow.m_Action = ReplayLengthSliderCallback;
	}

	private void CuratedReplaysToggleCallback(GamepadButtonType button)
	{
		SettingsRow.ToggleProcessInput(m_CuratedReplaysToggle, button);
	}

	private void ReplaysToggleCallback(GamepadButtonType button)
	{
		SettingsRow.ToggleProcessInput(m_ReplaysToggle, button);
	}

	private void ReplayQualityDropdownCallback(GamepadButtonType button)
	{
		SettingsRow.DropdownProcessInput(m_ReplayQualityDropdown, button);
	}

	private void ReplayLengthSliderCallback(GamepadButtonType button)
	{
		SettingsRow.SliderProcessInput(m_ReplayLengthSlider, button);
	}
}
