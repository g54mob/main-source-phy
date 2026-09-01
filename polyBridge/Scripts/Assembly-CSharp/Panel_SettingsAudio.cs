using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Panel_SettingsAudio : MonoBehaviour
{
	public Transform m_Content;

	[Header("Master Volume")]
	public Slider m_MasterVolumeSlider;

	public TextMeshProUGUI m_MasterVolumePreview;

	[Header("Music Volume")]
	public Slider m_MusicVolumeSlider;

	public TextMeshProUGUI m_MusicVolumePreview;

	[Header("SFX Volume")]
	public Slider m_SFXVolumeSlider;

	public TextMeshProUGUI m_SFXVolumePreview;

	[Header("Ambient Volume")]
	public Slider m_AmbientVolumeSlider;

	public TextMeshProUGUI m_AmbientVolumePreview;

	[Header("UI Volume")]
	public Slider m_UIVolumeSlider;

	public TextMeshProUGUI m_UIVolumePreview;

	[Header("Toggles")]
	public Toggle m_MuteToggle;

	public Toggle m_MuteInBackgroundToggle;

	public Toggle m_OverBudgetAlertToggle;

	[Header("Footer")]
	public Button m_ResetToDefaults;

	public GameObject m_NoSyncWarning;

	[Header("Rows")]
	public SettingsRow m_MuteToggleRow;

	public SettingsRow m_MuteInBackgroundToggleRow;

	public SettingsRow m_OverBudgetAlertToggleRow;

	public SettingsRow m_MasterVolumeSliderRow;

	public SettingsRow m_MusicVolumeSliderRow;

	public SettingsRow m_SFXVolumeSliderRow;

	public SettingsRow m_AmbientVolumeSliderRow;

	public SettingsRow m_UIVolumeSliderRow;

	private PointerEvents m_MuteTogglePointerEvents;

	private PointerEvents m_MuteInBackgroundTogglePointerEvents;

	private PointerEvents m_OverBudgetAlertTogglePointerEvents;

	private float m_LastMasterVolume;

	private float m_LastAmbientVolume;

	private float m_LastSFXVolume;

	private float m_LastMusicVolume;

	private float m_LastUIVolume;

	private void Awake()
	{
		m_MuteTogglePointerEvents = m_MuteToggle.GetComponent<PointerEvents>();
		m_MuteTogglePointerEvents.RegisterOnClickedDelegate(OnMuteToggle);
		m_MuteInBackgroundTogglePointerEvents = m_MuteInBackgroundToggle.GetComponent<PointerEvents>();
		m_MuteInBackgroundTogglePointerEvents.RegisterOnClickedDelegate(OnMuteInBackgroundToggle);
		m_OverBudgetAlertTogglePointerEvents = m_OverBudgetAlertToggle.GetComponent<PointerEvents>();
		m_OverBudgetAlertTogglePointerEvents.RegisterOnClickedDelegate(OnOverBudgetAlertToggle);
	}

	private void Start()
	{
		m_MasterVolumeSlider.onValueChanged.AddListener(OnMasterVolumeChanged);
		m_AmbientVolumeSlider.onValueChanged.AddListener(OnAmbientVolumeChanged);
		m_SFXVolumeSlider.onValueChanged.AddListener(OnSFXVolumeChanged);
		m_MusicVolumeSlider.onValueChanged.AddListener(OnMusicVolumeChanged);
		m_UIVolumeSlider.onValueChanged.AddListener(OnUIVolumeChanged);
		m_ResetToDefaults.onClick.AddListener(OnResetToDefaults);
		SetRowCallbacks();
	}

	public void OnEnableManual()
	{
		m_MasterVolumeSlider.value = Profiles.m_ActiveProfile.m_MasterVolume;
		m_AmbientVolumeSlider.value = Profiles.m_ActiveProfile.m_AmbientVolume;
		m_SFXVolumeSlider.value = Profiles.m_ActiveProfile.m_SFXVolume;
		m_MusicVolumeSlider.value = Profiles.m_ActiveProfile.m_MusicVolume;
		m_UIVolumeSlider.value = Profiles.m_ActiveProfile.m_UIVolume;
		m_MuteToggle.isOn = Profiles.m_ActiveProfile.m_Mute;
		m_MuteInBackgroundToggle.isOn = Profiles.m_ActiveProfile.m_MuteInBackground;
		m_OverBudgetAlertToggle.isOn = Profiles.m_ActiveProfile.m_OverBudgetAlert;
		m_MasterVolumePreview.text = Utils.FormatPercentage(Mathf.Clamp01(m_MasterVolumeSlider.value / 100f));
		m_AmbientVolumePreview.text = Utils.FormatPercentage(Mathf.Clamp01(m_AmbientVolumeSlider.value / 100f));
		m_SFXVolumePreview.text = Utils.FormatPercentage(Mathf.Clamp01(m_SFXVolumeSlider.value / 100f));
		m_MusicVolumePreview.text = Utils.FormatPercentage(Mathf.Clamp01(m_MusicVolumeSlider.value / 100f));
		m_UIVolumePreview.text = Utils.FormatPercentage(Mathf.Clamp01(m_UIVolumeSlider.value / 100f));
		m_LastMasterVolume = m_MasterVolumeSlider.value;
		m_LastAmbientVolume = m_AmbientVolumeSlider.value;
		m_LastSFXVolume = m_SFXVolumeSlider.value;
		m_LastMusicVolume = m_MusicVolumeSlider.value;
		m_LastUIVolume = m_UIVolumeSlider.value;
		m_NoSyncWarning.SetActive(!Game.IsMobile());
		Utils.EnableToggle(m_MuteInBackgroundToggle.gameObject, !Game.IsSteamDeckOrMobile());
		GameUI.m_Instance.m_Settings.SetRowsColor(m_Content);
	}

	public void OnDisableManual()
	{
	}

	public void OnEnable()
	{
		GameUI.m_Instance.m_Settings.ClearRows();
		GameUI.m_Instance.m_Settings.AddRow(m_MuteToggleRow);
		if (!Game.IsRunningOnSteamDeck())
		{
			GameUI.m_Instance.m_Settings.AddRow(m_MuteInBackgroundToggleRow);
		}
		GameUI.m_Instance.m_Settings.AddRow(m_OverBudgetAlertToggleRow);
		GameUI.m_Instance.m_Settings.AddRow(m_MasterVolumeSliderRow);
		GameUI.m_Instance.m_Settings.AddRow(m_MusicVolumeSliderRow);
		GameUI.m_Instance.m_Settings.AddRow(m_SFXVolumeSliderRow);
		GameUI.m_Instance.m_Settings.AddRow(m_AmbientVolumeSliderRow);
		GameUI.m_Instance.m_Settings.AddRow(m_UIVolumeSliderRow);
	}

	public void Update()
	{
		if (m_LastMasterVolume != m_MasterVolumeSlider.value || m_LastAmbientVolume != m_AmbientVolumeSlider.value || m_LastSFXVolume != m_SFXVolumeSlider.value || m_LastMusicVolume != m_MusicVolumeSlider.value || m_LastUIVolume != m_UIVolumeSlider.value)
		{
			UpdateAudioVolumeFromSliders();
			m_LastMasterVolume = m_MasterVolumeSlider.value;
			m_LastAmbientVolume = m_AmbientVolumeSlider.value;
			m_LastSFXVolume = m_SFXVolumeSlider.value;
			m_LastMusicVolume = m_MusicVolumeSlider.value;
			m_LastUIVolume = m_UIVolumeSlider.value;
		}
	}

	public void Apply(bool updateProfileOnly)
	{
		Profiles.m_ActiveProfile.m_MasterVolume = Mathf.FloorToInt(m_MasterVolumeSlider.value);
		Profiles.m_ActiveProfile.m_AmbientVolume = Mathf.FloorToInt(m_AmbientVolumeSlider.value);
		Profiles.m_ActiveProfile.m_SFXVolume = Mathf.FloorToInt(m_SFXVolumeSlider.value);
		Profiles.m_ActiveProfile.m_MusicVolume = Mathf.FloorToInt(m_MusicVolumeSlider.value);
		Profiles.m_ActiveProfile.m_UIVolume = Mathf.FloorToInt(m_UIVolumeSlider.value);
		Profiles.m_ActiveProfile.m_Mute = m_MuteToggle.isOn;
		Profiles.m_ActiveProfile.m_MuteInBackground = m_MuteInBackgroundToggle.isOn;
		Profiles.m_ActiveProfile.m_OverBudgetAlert = m_OverBudgetAlertToggle.isOn;
		if (!updateProfileOnly)
		{
			AudioVolume.Mute(m_MuteToggle.isOn);
		}
	}

	public void OnResetToDefaults()
	{
		InterfaceAudio.Play("ui_settings_reset");
		PopUpMessage.DisplayConfirmation(Localize.Get("POPUP_RESET_AUDIO"), useYesNoLabels: true, ConfirmOnDefaults);
	}

	private void OnMasterVolumeChanged(float value)
	{
		m_MasterVolumePreview.text = Utils.FormatPercentage(Mathf.Clamp01(m_MasterVolumeSlider.value / 100f));
	}

	private void OnAmbientVolumeChanged(float value)
	{
		m_AmbientVolumePreview.text = Utils.FormatPercentage(Mathf.Clamp01(m_AmbientVolumeSlider.value / 100f));
	}

	private void OnSFXVolumeChanged(float value)
	{
		m_SFXVolumePreview.text = Utils.FormatPercentage(Mathf.Clamp01(m_SFXVolumeSlider.value / 100f));
	}

	private void OnMusicVolumeChanged(float value)
	{
		m_MusicVolumePreview.text = Utils.FormatPercentage(Mathf.Clamp01(m_MusicVolumeSlider.value / 100f));
	}

	private void OnUIVolumeChanged(float value)
	{
		m_UIVolumePreview.text = Utils.FormatPercentage(Mathf.Clamp01(m_UIVolumeSlider.value / 100f));
	}

	private void UpdateAudioVolumeFromSliders()
	{
		float num = (float)Mathf.FloorToInt(m_MasterVolumeSlider.value) / 100f;
		float num2 = (float)Mathf.FloorToInt(m_AmbientVolumeSlider.value) / 100f;
		float num3 = (float)Mathf.FloorToInt(m_SFXVolumeSlider.value) / 100f;
		float music = (float)Mathf.FloorToInt(m_MusicVolumeSlider.value) / 100f;
		float ui = (float)Mathf.FloorToInt(m_UIVolumeSlider.value) / 100f;
		AudioMixerManager.Set(m_MuteToggle.isOn ? 0f : num, (GameStateManager.GetState() == GameState.MAIN_MENU) ? 0f : num2, (GameStateManager.GetState() == GameState.MAIN_MENU) ? 0f : num3, music, ui);
	}

	private void OnMuteToggle()
	{
		UpdateAudioVolumeFromSliders();
		InterfaceAudio.Play("ui_settings_toggle");
	}

	private void OnMuteInBackgroundToggle()
	{
		AudioVolume.MuteInBackground(m_MuteInBackgroundToggle.isOn);
		InterfaceAudio.Play("ui_settings_toggle");
	}

	private void OnOverBudgetAlertToggle()
	{
		InterfaceAudio.Play("ui_settings_toggle");
	}

	private void ConfirmOnDefaults()
	{
		m_MuteToggle.isOn = false;
		m_MuteInBackgroundToggle.isOn = false;
		m_OverBudgetAlertToggle.isOn = false;
		m_MasterVolumeSlider.value = AudioMixerManager.DEFAULT_MASTER_VOLUME;
		m_AmbientVolumeSlider.value = AudioMixerManager.DEFAULT_AMBIENT_VOLUME;
		m_SFXVolumeSlider.value = AudioMixerManager.DEFAULT_SFX_VOLUME;
		m_MusicVolumeSlider.value = AudioMixerManager.DEFAULT_MUSIC_VOLUME;
		m_UIVolumeSlider.value = AudioMixerManager.DEFAULT_UI_VOLUME;
		Apply(updateProfileOnly: false);
	}

	private void SetRowCallbacks()
	{
		m_MuteToggleRow.m_Action = MuteToggleRowCallback;
		m_MuteInBackgroundToggleRow.m_Action = MuteInBackgroundToggleRowCallback;
		m_OverBudgetAlertToggleRow.m_Action = OverBudgetAlertToggleRowCallback;
		m_MasterVolumeSliderRow.m_Action = MasterVolumeSliderRowCallback;
		m_MusicVolumeSliderRow.m_Action = MusicVolumeSliderRowCallback;
		m_SFXVolumeSliderRow.m_Action = SFXVolumeSliderRowCallback;
		m_AmbientVolumeSliderRow.m_Action = AmbientVolumeSliderRowCallback;
		m_UIVolumeSliderRow.m_Action = UIVolumeSliderRowCallback;
	}

	private void MuteToggleRowCallback(GamepadButtonType button)
	{
		SettingsRow.ToggleProcessInput(m_MuteToggle, button);
	}

	private void MuteInBackgroundToggleRowCallback(GamepadButtonType button)
	{
		SettingsRow.ToggleProcessInput(m_MuteInBackgroundToggle, button);
	}

	private void OverBudgetAlertToggleRowCallback(GamepadButtonType button)
	{
		SettingsRow.ToggleProcessInput(m_OverBudgetAlertToggle, button);
	}

	private void MasterVolumeSliderRowCallback(GamepadButtonType button)
	{
		SettingsRow.SliderProcessInput(m_MasterVolumeSlider, button);
	}

	private void MusicVolumeSliderRowCallback(GamepadButtonType button)
	{
		SettingsRow.SliderProcessInput(m_MusicVolumeSlider, button);
	}

	private void SFXVolumeSliderRowCallback(GamepadButtonType button)
	{
		SettingsRow.SliderProcessInput(m_SFXVolumeSlider, button);
	}

	private void AmbientVolumeSliderRowCallback(GamepadButtonType button)
	{
		SettingsRow.SliderProcessInput(m_AmbientVolumeSlider, button);
	}

	private void UIVolumeSliderRowCallback(GamepadButtonType button)
	{
		SettingsRow.SliderProcessInput(m_UIVolumeSlider, button);
	}
}
