using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Panel_SettingsGraphics : MonoBehaviour
{
	public Transform m_Content;

	[Header("Maximum Framerate")]
	public Slider m_VsyncIntervalSlider;

	public TextMeshProUGUI m_VsyncIntervalPreview;

	[Header("Resolution")]
	public Slider m_ResolutionSlider;

	public TextMeshProUGUI m_ResolutionPreview;

	[Header("Vsync")]
	public Toggle m_VsyncToggle;

	[Header("Toggles")]
	public Toggle m_ColorBlindToggle;

	public Toggle m_FullScreenToggle;

	public Toggle m_ColorGradingToggle;

	public Toggle m_SSAOToggle;

	public Toggle m_BloomToggle;

	public Toggle m_VignetteToggle;

	public Toggle m_CustomResolutionToggle;

	[Header("UI Scaling")]
	public TMP_Dropdown m_UIScaleModeDropdown;

	public TMP_InputField m_UIScaleFactorInputField;

	public Button m_UIScaleFactorInputFieldGamepadButton;

	[Header("Shadow Resolution")]
	public Slider m_ShadowResolutionSlider;

	public TextMeshProUGUI m_ShadowResolutionPreview;

	[Header("AntiAliasing")]
	public Slider m_AntiAliasingQualitySlider;

	public TextMeshProUGUI m_AntiAliasingPreview;

	[Header("Footer")]
	public Button m_ResetToDefaults;

	public GameObject m_NoSyncWarning;

	[Header("Rows")]
	public SettingsRow m_ColorBlindToggleRow;

	public SettingsRow m_FullScreenToggleRow;

	public SettingsRow m_VsyncToggleRow;

	public SettingsRow m_BloomToggleRow;

	public SettingsRow m_VignetteToggleRow;

	public SettingsRow m_CustomResolutionToggleRow;

	public SettingsRow m_VsyncIntervalSliderRow;

	public SettingsRow m_ResolutionSliderRow;

	public SettingsRow m_ShadowResolutionSliderRow;

	public SettingsRow m_AntiAliasingQualitySliderRow;

	public SettingsRow m_UIScaleModeDropdownRow;

	public SettingsRow m_UIScaleFactorRow;

	private PointerEvents m_ColorBlindTogglePointerEvents;

	private PointerEvents m_FullScreenTogglePointerEvents;

	private PointerEvents m_ColorGradingTogglePointerEvents;

	private PointerEvents m_SSAOTogglePointerEvents;

	private PointerEvents m_BloomTogglePointerEvents;

	private PointerEvents m_VignetteTogglePointerEvents;

	private PointerEvents m_CustomResolutionTogglePointerEvents;

	private PointerEvents m_VsyncTogglePointerEvents;

	private bool m_OriginalFullscreen;

	private int m_OriginalScreenWidth;

	private int m_OriginalScreenHeight;

	private void Awake()
	{
		m_ColorBlindTogglePointerEvents = m_ColorBlindToggle.GetComponent<PointerEvents>();
		m_FullScreenTogglePointerEvents = m_FullScreenToggle.GetComponent<PointerEvents>();
		m_ColorGradingTogglePointerEvents = m_ColorGradingToggle.GetComponent<PointerEvents>();
		m_SSAOTogglePointerEvents = m_SSAOToggle.GetComponent<PointerEvents>();
		m_BloomTogglePointerEvents = m_BloomToggle.GetComponent<PointerEvents>();
		m_VignetteTogglePointerEvents = m_VignetteToggle.GetComponent<PointerEvents>();
		m_CustomResolutionTogglePointerEvents = m_CustomResolutionToggle.GetComponent<PointerEvents>();
		m_VsyncTogglePointerEvents = m_VsyncToggle.GetComponent<PointerEvents>();
		m_ColorBlindTogglePointerEvents.RegisterOnClickedDelegate(InterfaceAudio.PlayToggleAudio);
		m_FullScreenTogglePointerEvents.RegisterOnClickedDelegate(InterfaceAudio.PlayToggleAudio);
		m_ColorGradingTogglePointerEvents.RegisterOnClickedDelegate(InterfaceAudio.PlayToggleAudio);
		m_SSAOTogglePointerEvents.RegisterOnClickedDelegate(InterfaceAudio.PlayToggleAudio);
		m_BloomTogglePointerEvents.RegisterOnClickedDelegate(InterfaceAudio.PlayToggleAudio);
		m_VignetteTogglePointerEvents.RegisterOnClickedDelegate(InterfaceAudio.PlayToggleAudio);
		m_CustomResolutionTogglePointerEvents.RegisterOnClickedDelegate(InterfaceAudio.PlayToggleAudio);
		m_VsyncTogglePointerEvents.RegisterOnClickedDelegate(InterfaceAudio.PlayToggleAudio);
		m_VsyncIntervalSlider.onValueChanged.AddListener(OnMaxFramerateChanged);
		m_ResolutionSlider.onValueChanged.AddListener(OnResolutionChanged);
		m_ShadowResolutionSlider.onValueChanged.AddListener(OnShadowResolutionChanged);
		m_AntiAliasingQualitySlider.onValueChanged.AddListener(OnAntiAliasingQualityChanged);
		m_ResetToDefaults.onClick.AddListener(OnResetToDefaults);
		m_UIScaleModeDropdown.onValueChanged.AddListener(delegate
		{
			OnUIScaleModeChanged();
		});
		m_UIScaleModeDropdown.alphaFadeSpeed = 0f;
		m_UIScaleFactorInputField.onEndEdit.AddListener(delegate
		{
			ApplyUIScaleFactor();
		});
		m_UIScaleFactorInputFieldGamepadButton.onClick.AddListener(OnUIScaleFactorInputFieldGamepadButton);
		SetRowCallbacks();
	}

	public void OnEnableManual()
	{
		m_FullScreenToggle.isOn = Profiles.m_ActiveProfile.m_FullScreen;
		m_ColorBlindToggle.isOn = Profiles.m_ActiveProfile.m_ColorBlindModeOn;
		m_VsyncToggle.isOn = Profiles.m_ActiveProfile.m_Vsync;
		m_BloomToggle.isOn = Profiles.m_ActiveProfile.m_Bloom;
		m_SSAOToggle.isOn = Profiles.m_ActiveProfile.m_SSAO;
		m_VignetteToggle.isOn = Profiles.m_ActiveProfile.m_Vignette;
		m_CustomResolutionToggle.isOn = Profiles.m_ActiveProfile.m_CustomResolution;
		m_ResolutionSlider.minValue = 0f;
		m_ResolutionSlider.maxValue = GameResolutions.m_Resolutions.Count - 1;
		int num = GetSliderValueFromResolution(Profiles.m_ActiveProfile.m_ScreenWidth, Profiles.m_ActiveProfile.m_ScreenHeight);
		if (num == -1)
		{
			num = GameResolutions.m_Resolutions.Count - 1;
		}
		m_ResolutionSlider.value = num;
		m_ResolutionPreview.text = GameResolutions.GetResolutionLabel(Mathf.FloorToInt(m_ResolutionSlider.value));
		m_ShadowResolutionSlider.minValue = 0f;
		m_ShadowResolutionSlider.maxValue = 4f;
		m_ShadowResolutionSlider.value = (float)Profiles.m_ActiveProfile.m_ShadowResolution;
		m_ShadowResolutionPreview.text = GetShadowResolutionLabel(Mathf.FloorToInt(m_ShadowResolutionSlider.value));
		m_AntiAliasingQualitySlider.minValue = 0f;
		m_AntiAliasingQualitySlider.maxValue = 4f;
		m_AntiAliasingQualitySlider.value = (float)Profiles.m_ActiveProfile.m_AntiAliasingQuality;
		m_AntiAliasingPreview.text = GetAntiAliasingQualityLabel(Mathf.FloorToInt(m_AntiAliasingQualitySlider.value));
		m_VsyncIntervalSlider.minValue = 0f;
		m_VsyncIntervalSlider.maxValue = 2f;
		m_VsyncIntervalSlider.value = 2 - Profiles.m_ActiveProfile.m_VsyncInterval;
		m_VsyncIntervalPreview.text = Game.GetLocalizedTargetFramerate(2 - Mathf.FloorToInt(m_VsyncIntervalSlider.value));
		m_OriginalFullscreen = Profiles.m_ActiveProfile.m_FullScreen;
		m_OriginalScreenWidth = Profiles.m_ActiveProfile.m_ScreenWidth;
		m_OriginalScreenHeight = Profiles.m_ActiveProfile.m_ScreenHeight;
		m_NoSyncWarning.SetActive(!Game.IsMobile());
		PopulateUIScaleModeDropdown();
		SelectUIScaleModeChoice(Profiles.m_ActiveProfile.m_UIScaleMode);
		m_UIScaleFactorInputField.characterLimit = 6;
		m_UIScaleFactorInputField.caretWidth = 1;
		m_UIScaleFactorInputField.selectionColor = GameUI.m_Instance.m_InputFieldSelectColor;
		m_UIScaleFactorInputField.text = Utils.FormatTwoDecimalPlaces(Profiles.m_ActiveProfile.m_UIScaleFactor);
		Utils.EnableToggle(m_FullScreenToggle.gameObject, !Game.IsSteamDeckOrMobile());
		Utils.EnableToggle(m_VsyncToggle.gameObject, !Game.IsSteamDeckOrMobile());
		Utils.EnableToggle(m_CustomResolutionToggle.gameObject, !Game.IsSteamDeckOrMobile());
		Utils.EnableSlider(m_VsyncIntervalSlider.gameObject, !Game.IsMobile());
		Utils.EnableSlider(m_ResolutionSlider.gameObject, !Game.IsSteamDeckOrMobile());
		Utils.EnableDropdown(m_UIScaleModeDropdown.gameObject, !Game.IsSteamDeckOrMobile());
		Utils.EnableInputField(m_UIScaleFactorInputField.gameObject, !Game.IsSteamDeckOrMobile());
		GameUI.m_Instance.m_Settings.SetRowsColor(m_Content);
	}

	public void OnDisableManual()
	{
	}

	public void OnEnable()
	{
		GameUI.m_Instance.m_Settings.ClearRows();
		if (Game.IsRunningOnSteamDeck())
		{
			GameUI.m_Instance.m_Settings.AddRow(m_ColorBlindToggleRow);
			GameUI.m_Instance.m_Settings.AddRow(m_BloomToggleRow);
			GameUI.m_Instance.m_Settings.AddRow(m_VignetteToggleRow);
			GameUI.m_Instance.m_Settings.AddRow(m_VsyncIntervalSliderRow);
			GameUI.m_Instance.m_Settings.AddRow(m_ShadowResolutionSliderRow);
			GameUI.m_Instance.m_Settings.AddRow(m_AntiAliasingQualitySliderRow);
			return;
		}
		GameUI.m_Instance.m_Settings.AddRow(m_ColorBlindToggleRow);
		GameUI.m_Instance.m_Settings.AddRow(m_FullScreenToggleRow);
		GameUI.m_Instance.m_Settings.AddRow(m_VsyncToggleRow);
		GameUI.m_Instance.m_Settings.AddRow(m_BloomToggleRow);
		GameUI.m_Instance.m_Settings.AddRow(m_VignetteToggleRow);
		GameUI.m_Instance.m_Settings.AddRow(m_CustomResolutionToggleRow);
		GameUI.m_Instance.m_Settings.AddRow(m_UIScaleModeDropdownRow);
		GameUI.m_Instance.m_Settings.AddRow(m_UIScaleFactorRow);
		GameUI.m_Instance.m_Settings.AddRow(m_VsyncIntervalSliderRow);
		GameUI.m_Instance.m_Settings.AddRow(m_ResolutionSliderRow);
		GameUI.m_Instance.m_Settings.AddRow(m_ShadowResolutionSliderRow);
		GameUI.m_Instance.m_Settings.AddRow(m_AntiAliasingQualitySliderRow);
	}

	public void UpdateForCurrentDevice()
	{
		m_UIScaleFactorInputField.interactable = !GamepadVirtualKeyboard.IsSupported();
		m_UIScaleFactorInputFieldGamepadButton.gameObject.SetActive(GamepadVirtualKeyboard.IsSupported());
	}

	public bool IsDirty()
	{
		Resolution resolutionFromSlider = GetResolutionFromSlider();
		if (m_OriginalScreenWidth == resolutionFromSlider.width && m_OriginalScreenHeight == resolutionFromSlider.height)
		{
			return m_OriginalFullscreen != m_FullScreenToggle.isOn;
		}
		return true;
	}

	public void Revert()
	{
		Screen.SetResolution(m_OriginalScreenWidth, m_OriginalScreenHeight, m_OriginalFullscreen);
		m_FullScreenToggle.isOn = m_OriginalFullscreen;
		int sliderValueFromResolution = GetSliderValueFromResolution(m_OriginalScreenWidth, m_OriginalScreenHeight);
		m_ResolutionSlider.value = sliderValueFromResolution;
		m_ResolutionPreview.text = GameResolutions.GetResolutionLabel(Mathf.FloorToInt(m_ResolutionSlider.value));
	}

	public void Apply(bool updateProfileOnly)
	{
		if (Profiles.m_ActiveProfile.m_ColorBlindModeOn != m_ColorBlindToggle.isOn)
		{
			BridgeEdges.ForceStressVisualizationRefresh();
		}
		Profiles.m_ActiveProfile.m_ColorBlindModeOn = m_ColorBlindToggle.isOn;
		Profiles.m_ActiveProfile.m_Vsync = m_VsyncToggle.isOn;
		Profiles.m_ActiveProfile.m_CustomResolution = m_CustomResolutionToggle.isOn;
		Profiles.m_ActiveProfile.m_VsyncInterval = 2 - GetVsyncIntervalFromSlider();
		Profiles.m_ActiveProfile.m_ShadowResolution = GetShadowResolutionFromSlider();
		Profiles.m_ActiveProfile.m_AntiAliasingQuality = GetAntiAliasingQualityFromSlider();
		if (!updateProfileOnly)
		{
			GameRenderSettings.SetQualitySettings(Profiles.m_ActiveProfile.m_Vsync, Profiles.m_ActiveProfile.m_VsyncInterval, Profiles.m_ActiveProfile.m_ShadowResolution);
		}
		Profiles.m_ActiveProfile.m_SSAO = m_SSAOToggle.isOn;
		Profiles.m_ActiveProfile.m_Bloom = m_BloomToggle.isOn;
		Profiles.m_ActiveProfile.m_Vignette = m_VignetteToggle.isOn;
		if (!updateProfileOnly)
		{
			GameRenderSettings.SetPostFXSettings(Profiles.m_ActiveProfile.m_SSAO, Profiles.m_ActiveProfile.m_Bloom, Profiles.m_ActiveProfile.m_Vignette, Profiles.m_ActiveProfile.m_AntiAliasingQuality);
		}
		if (!Game.IsRunningOnSteamDeck())
		{
			ApplyUIScaleMode();
			ApplyUIScaleFactor();
		}
	}

	private void ApplyUIScaleMode()
	{
		UIScaleMode uIScaleMode = Profiles.m_ActiveProfile.m_UIScaleMode;
		UIScaleMode value = (UIScaleMode)m_UIScaleModeDropdown.value;
		if (value != uIScaleMode)
		{
			Profiles.m_ActiveProfile.m_UIScaleMode = value;
			GameUI.ApplyUIScaleMode(Profiles.m_ActiveProfile.m_UIScaleMode);
			float num = 1f;
			num = ((value != UIScaleMode.CONSTANT_PIXEL_SIZE) ? (Profiles.m_ActiveProfile.m_UIScaleFactor / ((float)Screen.height / GameUI.m_Instance.m_CanvasScaler.referenceResolution.y)) : ((float)Screen.height / GameUI.REFERENCE_RESOLUTION_Y_DEFAULT * Profiles.m_ActiveProfile.m_UIScaleFactor));
			GameUI.ApplyUIScaleFactor(num);
			m_UIScaleFactorInputField.text = Utils.FormatTwoDecimalPlaces(Profiles.m_ActiveProfile.m_UIScaleFactor);
		}
	}

	private void ApplyUIScaleFactor()
	{
		if (!float.TryParse(m_UIScaleFactorInputField.text.Trim().Replace(',', '.'), out var result))
		{
			InterfaceAudio.PlayErrorBeep();
			return;
		}
		GameUI.ApplyUIScaleFactor(result);
		m_UIScaleFactorInputField.text = Utils.FormatTwoDecimalPlaces(Profiles.m_ActiveProfile.m_UIScaleFactor);
	}

	public void ApplyAfterConfirmation()
	{
		Profiles.m_ActiveProfile.m_FullScreen = m_FullScreenToggle.isOn;
		Resolution resolutionFromSlider = GetResolutionFromSlider();
		Profiles.m_ActiveProfile.m_ScreenWidth = resolutionFromSlider.width;
		Profiles.m_ActiveProfile.m_ScreenHeight = resolutionFromSlider.height;
	}

	public Resolution GetResolutionFromSlider()
	{
		int index = Mathf.FloorToInt(m_ResolutionSlider.value);
		return GameResolutions.m_Resolutions[index];
	}

	public int GetVsyncIntervalFromSlider()
	{
		return Mathf.FloorToInt(m_VsyncIntervalSlider.value);
	}

	public ShadowResolution GetShadowResolutionFromSlider()
	{
		return (ShadowResolution)Mathf.FloorToInt(m_ShadowResolutionSlider.value);
	}

	public AntiAliasingQuality GetAntiAliasingQualityFromSlider()
	{
		return (AntiAliasingQuality)Mathf.FloorToInt(m_AntiAliasingQualitySlider.value);
	}

	public bool GetFullScreenFromToggle()
	{
		return m_FullScreenToggle.isOn;
	}

	public void OnResetToDefaults()
	{
		InterfaceAudio.Play("ui_settings_reset");
		PopUpMessage.DisplayConfirmation(Localize.Get("POPUP_RESET_GRAPHICS"), useYesNoLabels: true, ConfirmOnDefaults);
	}

	private void OnMaxFramerateChanged(float value)
	{
		m_VsyncIntervalPreview.text = Game.GetLocalizedTargetFramerate(2 - Mathf.FloorToInt(m_VsyncIntervalSlider.value));
	}

	private void OnResolutionChanged(float value)
	{
		m_ResolutionPreview.text = GameResolutions.GetResolutionLabel(Mathf.FloorToInt(m_ResolutionSlider.value));
	}

	private void OnShadowResolutionChanged(float value)
	{
		m_ShadowResolutionPreview.text = GetShadowResolutionLabel(Mathf.FloorToInt(m_ShadowResolutionSlider.value));
	}

	private void OnAntiAliasingQualityChanged(float value)
	{
		m_AntiAliasingPreview.text = GetAntiAliasingQualityLabel(Mathf.FloorToInt(m_AntiAliasingQualitySlider.value));
	}

	private int GetSliderValueFromResolution(int width, int height)
	{
		return GameResolutions.GetResolutionIndex(width, height);
	}

	private string GetShadowResolutionLabel(int index)
	{
		switch ((ShadowResolution)index)
		{
		case ShadowResolution.HIGH:
			return Localize.Get("UI_SHADOWS_HIGH");
		case ShadowResolution.LOW:
			return Localize.Get("UI_SHADOWS_LOW");
		case ShadowResolution.MEDIUM:
			return Localize.Get("UI_SHADOWS_MEDIUM");
		case ShadowResolution.OFF:
			return Localize.Get("UI_SHADOWS_OFF");
		case ShadowResolution.VERY_HIGH:
			return Localize.Get("UI_SHADOWS_VERY_HIGH");
		default:
			Debug.LogWarningFormat("Unexpected ShadowResolution index: {0}", index);
			return string.Empty;
		}
	}

	private string GetAntiAliasingQualityLabel(int index)
	{
		switch ((AntiAliasingQuality)index)
		{
		case AntiAliasingQuality.DEFAULT:
			return Localize.Get("UI_ANTIALIASING_DEFAULT");
		case AntiAliasingQuality.EXTREME_PERFORMANCE:
			return Localize.Get("UI_SHADOWS_OFF");
		case AntiAliasingQuality.EXTREME_QUALITY:
			return Localize.Get("UI_ANTIALIASING_EXTREME_QUALITY");
		case AntiAliasingQuality.PERFORMANCE:
			return Localize.Get("UI_ANTIALIASING_PERFORMANCE");
		case AntiAliasingQuality.QUALITY:
			return Localize.Get("UI_ANTIALIASING_QUALITY");
		default:
			Debug.LogWarningFormat("Unexpected AntiAliasingQuality index: {0}", index);
			return string.Empty;
		}
	}

	private void ConfirmOnDefaults()
	{
		m_ColorBlindToggle.isOn = false;
		m_FullScreenToggle.isOn = true;
		m_VsyncToggle.isOn = true;
		m_SSAOToggle.isOn = true;
		m_BloomToggle.isOn = true;
		m_VignetteToggle.isOn = true;
		m_CustomResolutionToggle.isOn = false;
		m_ShadowResolutionSlider.value = 3f;
		m_ShadowResolutionPreview.text = GetShadowResolutionLabel(Mathf.FloorToInt(m_ShadowResolutionSlider.value));
		m_AntiAliasingQualitySlider.value = 3f;
		m_AntiAliasingPreview.text = GetAntiAliasingQualityLabel(Mathf.FloorToInt(m_AntiAliasingQualitySlider.value));
		int num = GetSliderValueFromResolution(Screen.currentResolution.width, Screen.currentResolution.height);
		if (num == -1)
		{
			num = GameResolutions.m_Resolutions.Count - 1;
		}
		m_ResolutionSlider.value = num;
		m_ResolutionPreview.text = GameResolutions.GetResolutionLabel(Mathf.FloorToInt(m_ResolutionSlider.value));
		SelectUIScaleModeChoice(UIScaleMode.SCALE_WITH_SCREEN_SIZE);
		m_UIScaleFactorInputField.text = Utils.FormatTwoDecimalPlaces(1f);
		if (!Game.IsRunningOnSteamDeck())
		{
			ApplyUIScaleMode();
			ApplyUIScaleFactor();
		}
	}

	private void SetRowCallbacks()
	{
		m_ColorBlindToggleRow.m_Action = ColorBlindToggleCallback;
		m_FullScreenToggleRow.m_Action = FullScreenToggleCallback;
		m_VsyncToggleRow.m_Action = VsyncToggleCcallback;
		m_BloomToggleRow.m_Action = BloomToggleCallback;
		m_VignetteToggleRow.m_Action = VignetteToggleCallback;
		m_CustomResolutionToggleRow.m_Action = CustomResolutionToggleCallback;
		m_UIScaleModeDropdownRow.m_Action = UIScaleModeDropdownCallback;
		m_UIScaleFactorRow.m_Action = UIScaleFactorCallback;
		m_VsyncIntervalSliderRow.m_Action = VsyncIntervalSliderCallback;
		m_ResolutionSliderRow.m_Action = ResolutionSliderCallback;
		m_ShadowResolutionSliderRow.m_Action = ShadowResolutionSliderCallback;
		m_AntiAliasingQualitySliderRow.m_Action = AntiAliasingQualitySliderCallback;
	}

	private void ColorBlindToggleCallback(GamepadButtonType button)
	{
		SettingsRow.ToggleProcessInput(m_ColorBlindToggle, button);
	}

	private void FullScreenToggleCallback(GamepadButtonType button)
	{
		SettingsRow.ToggleProcessInput(m_FullScreenToggle, button);
	}

	private void VsyncToggleCcallback(GamepadButtonType button)
	{
		SettingsRow.ToggleProcessInput(m_VsyncToggle, button);
	}

	private void BloomToggleCallback(GamepadButtonType button)
	{
		SettingsRow.ToggleProcessInput(m_BloomToggle, button);
	}

	private void VignetteToggleCallback(GamepadButtonType button)
	{
		SettingsRow.ToggleProcessInput(m_VignetteToggle, button);
	}

	private void CustomResolutionToggleCallback(GamepadButtonType button)
	{
		SettingsRow.ToggleProcessInput(m_CustomResolutionToggle, button);
	}

	private void VsyncIntervalSliderCallback(GamepadButtonType button)
	{
		SettingsRow.SliderProcessInput(m_VsyncIntervalSlider, button);
	}

	private void ResolutionSliderCallback(GamepadButtonType button)
	{
		SettingsRow.SliderProcessInput(m_ResolutionSlider, button);
	}

	private void ShadowResolutionSliderCallback(GamepadButtonType button)
	{
		SettingsRow.SliderProcessInput(m_ShadowResolutionSlider, button);
	}

	private void AntiAliasingQualitySliderCallback(GamepadButtonType button)
	{
		SettingsRow.SliderProcessInput(m_AntiAliasingQualitySlider, button);
	}

	private void UIScaleModeDropdownCallback(GamepadButtonType button)
	{
	}

	private void UIScaleFactorCallback(GamepadButtonType button)
	{
	}

	private void PopulateUIScaleModeDropdown()
	{
		List<TMP_Dropdown.OptionData> list = new List<TMP_Dropdown.OptionData>();
		list.Add(new TMP_Dropdown.OptionData(Localize.Get("UI_SCALE_WITH_SCREEN_SIZE")));
		list.Add(new TMP_Dropdown.OptionData(Localize.Get("UI_CONSTANT_PIXEL_SIZE")));
		m_UIScaleModeDropdown.options = list;
	}

	private void SelectUIScaleModeChoice(UIScaleMode choice)
	{
		m_UIScaleModeDropdown.SetValueWithoutNotify((int)choice);
	}

	private void OnUIScaleModeChanged()
	{
		ApplyUIScaleMode();
	}

	private void OnUIScaleFactorInputFieldGamepadButton()
	{
		GamepadVirtualKeyboard.MaybeOpenVirtualKeyboard(m_UIScaleFactorInputField.text, m_UIScaleFactorInputField.characterLimit, Localize.Get("UI_SCALE_FACTOR").Replace(":", string.Empty), multiline: true, OnUIScaleFactorEntered);
	}

	private void OnUIScaleFactorEntered(string text)
	{
		if (text != null)
		{
			m_UIScaleFactorInputField.text = text;
		}
	}
}
