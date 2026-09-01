using System.Collections.Generic;
using I2.Loc;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Panel_SettingsOther : MonoBehaviour
{
	public Transform m_Content;

	[Header("Language")]
	public TMP_Dropdown m_LanguageDropdown;

	[Header("Mouse Wheel Speed")]
	public Slider m_MouseWheelSpeedSlider;

	public TextMeshProUGUI m_MouseWheelSpeedPreview;

	[Header("Auto Saves")]
	public Toggle m_LoadAutoSaveToggle;

	[Header("Edge Bisect")]
	public Toggle m_EdgeBisectToggle;

	public TextMeshProUGUI m_EdgeBisectText;

	[Header("First Break")]
	public Toggle m_FirstBreakToggle;

	[Header("Lock Build Camera")]
	public Toggle m_LockBuildCameraToggle;

	public TextMeshProUGUI m_LockBuildCameraText;

	[Header("Disable Tooltips")]
	public Toggle m_EnableTooltipsToggle;

	public Toggle m_EnableBuildDataTooltipsToggle;

	public Toggle m_EnableBuildHelpTooltipsToggle;

	[Header("Camera Rotate Speed")]
	public Slider m_CameraRotateSpeedSlider;

	public TextMeshProUGUI m_CameraRotateSpeedPreview;

	[Header("Camera Pan Speed")]
	public Slider m_CameraPanSpeedSlider;

	public TextMeshProUGUI m_CameraPanSpeedPreview;

	[Header("Footer")]
	public Button m_ResetToDefaults;

	[Header("Rows")]
	public SettingsRow m_LanguageDropdownRow;

	public SettingsRow m_LoadAutoSaveToggleRow;

	public SettingsRow m_LockBuildCameraToggleRow;

	public SettingsRow m_EnableTooltipsToggleRow;

	public SettingsRow m_EnableBuildDataTooltipsToggleRow;

	public SettingsRow m_EnableBuildHelpTooltipsToggleRow;

	public SettingsRow m_EdgeBisectToggleRow;

	public SettingsRow m_FirstBreakToggleRow;

	public SettingsRow m_CameraRotateSpeedSliderRow;

	public SettingsRow m_MouseWheelSpeedSliderRow;

	public SettingsRow m_CameraPanSpeedSliderRow;

	private PointerEvents m_LoadAutoSaveTogglePointerEvents;

	private PointerEvents m_EdgeBisectTogglePointerEvents;

	private PointerEvents m_FirstBreakTogglePointerEvents;

	private PointerEvents m_LockBuildCameraTogglePointerEvents;

	private PointerEvents m_EnableTooltipsTogglePointerEvents;

	private PointerEvents m_EnableBuildDataTooltipsTogglePointerEvents;

	private PointerEvents m_EnableBuildHelpTooltipsTogglePointerEvents;

	private Dictionary<int, string> m_LanguagesMap = new Dictionary<int, string>();

	public void Start()
	{
		m_LoadAutoSaveTogglePointerEvents = m_LoadAutoSaveToggle.GetComponent<PointerEvents>();
		m_LoadAutoSaveTogglePointerEvents.RegisterOnClickedDelegate(InterfaceAudio.PlayToggleAudio);
		m_EdgeBisectTogglePointerEvents = m_EdgeBisectToggle.GetComponent<PointerEvents>();
		m_EdgeBisectTogglePointerEvents.RegisterOnClickedDelegate(InterfaceAudio.PlayToggleAudio);
		m_FirstBreakTogglePointerEvents = m_FirstBreakToggle.GetComponent<PointerEvents>();
		m_FirstBreakTogglePointerEvents.RegisterOnClickedDelegate(InterfaceAudio.PlayToggleAudio);
		m_LockBuildCameraTogglePointerEvents = m_LockBuildCameraToggle.GetComponent<PointerEvents>();
		m_LockBuildCameraTogglePointerEvents.RegisterOnClickedDelegate(InterfaceAudio.PlayToggleAudio);
		m_EnableTooltipsTogglePointerEvents = m_EnableTooltipsToggle.GetComponent<PointerEvents>();
		m_EnableTooltipsTogglePointerEvents.RegisterOnClickedDelegate(InterfaceAudio.PlayToggleAudio);
		m_EnableBuildDataTooltipsTogglePointerEvents = m_EnableBuildDataTooltipsToggle.GetComponent<PointerEvents>();
		m_EnableBuildDataTooltipsTogglePointerEvents.RegisterOnClickedDelegate(InterfaceAudio.PlayToggleAudio);
		m_EnableBuildHelpTooltipsTogglePointerEvents = m_EnableBuildHelpTooltipsToggle.GetComponent<PointerEvents>();
		m_EnableBuildHelpTooltipsTogglePointerEvents.RegisterOnClickedDelegate(InterfaceAudio.PlayToggleAudio);
		m_LanguageDropdown.onValueChanged.AddListener(delegate
		{
			OnLanguageChanged();
		});
		m_LanguageDropdown.alphaFadeSpeed = 0f;
		SetRowCallbacks();
		m_ResetToDefaults.onClick.AddListener(OnResetToDefaults);
	}

	public void OnEnableManual()
	{
		m_MouseWheelSpeedSlider.onValueChanged.AddListener(OnMouseWheelSpeedChanged);
		m_CameraRotateSpeedSlider.onValueChanged.AddListener(OnCameraRotateSpeedChanged);
		m_CameraPanSpeedSlider.onValueChanged.AddListener(OnCameraPanSpeedChanged);
		PopulateLanguageDropdown();
		for (int i = 0; i < Localize.m_BuiltInLaguageCodes.Length; i++)
		{
			if (Localize.m_BuiltInLaguageCodes[i] == Profiles.m_ActiveProfile.m_LanguageCode)
			{
				m_LanguageDropdown.SetValueWithoutNotify(i);
				break;
			}
		}
		m_LoadAutoSaveToggle.isOn = Profiles.m_ActiveProfile.m_AutomatiallyLoadAutoSave;
		m_EdgeBisectToggle.isOn = Profiles.m_ActiveProfile.m_EdgeBisectEnabled;
		m_FirstBreakToggle.isOn = Profiles.m_ActiveProfile.m_FirstBreakEnabled;
		m_LockBuildCameraToggle.isOn = Profiles.m_ActiveProfile.m_LockBuildCamera;
		m_EnableTooltipsToggle.isOn = !Profiles.m_ActiveProfile.m_DisableTooltips;
		m_EnableBuildDataTooltipsToggle.isOn = !Profiles.m_ActiveProfile.m_DisableBuildDataTooltips;
		m_EnableBuildHelpTooltipsToggle.isOn = !Profiles.m_ActiveProfile.m_DisableBuildHelpTooltips;
		m_MouseWheelSpeedSlider.value = Mathf.Round(Profiles.m_ActiveProfile.m_MouseWheelSpeedNormalized * 100f);
		m_CameraRotateSpeedSlider.value = Mathf.Round(Profiles.m_ActiveProfile.m_CameraRotateSpeedNormalized * 100f);
		m_CameraPanSpeedSlider.value = Mathf.Round(Profiles.m_ActiveProfile.m_CameraPanSpeedNormalized * 100f);
		UpdateLock2DText();
		UpdateEdgeBisectText();
		Utils.EnableToggle(m_EnableBuildHelpTooltipsToggle.gameObject, !Game.IsRunningOnSteamDeck());
		Utils.EnableSlider(m_CameraPanSpeedSlider.gameObject, !Game.IsRunningOnSteamDeck());
		Utils.EnableSlider(m_CameraRotateSpeedSlider.gameObject, !Game.IsRunningOnSteamDeck());
		Utils.EnableSlider(m_MouseWheelSpeedSlider.gameObject, !Game.IsRunningOnSteamDeck());
		GameUI.m_Instance.m_Settings.SetRowsColor(m_Content);
	}

	public void OnDisableManual()
	{
		m_MouseWheelSpeedSlider.onValueChanged.RemoveAllListeners();
		m_CameraRotateSpeedSlider.onValueChanged.RemoveAllListeners();
		m_CameraPanSpeedSlider.onValueChanged.RemoveAllListeners();
	}

	public void OnEnable()
	{
		GameUI.m_Instance.m_Settings.ClearRows();
		GameUI.m_Instance.m_Settings.AddRow(m_LanguageDropdownRow);
		GameUI.m_Instance.m_Settings.AddRow(m_LoadAutoSaveToggleRow);
		GameUI.m_Instance.m_Settings.AddRow(m_LockBuildCameraToggleRow);
		GameUI.m_Instance.m_Settings.AddRow(m_EnableTooltipsToggleRow);
		GameUI.m_Instance.m_Settings.AddRow(m_EnableBuildDataTooltipsToggleRow);
		if (!Game.IsRunningOnSteamDeck())
		{
			GameUI.m_Instance.m_Settings.AddRow(m_EnableBuildHelpTooltipsToggleRow);
		}
		GameUI.m_Instance.m_Settings.AddRow(m_EdgeBisectToggleRow);
		GameUI.m_Instance.m_Settings.AddRow(m_FirstBreakToggleRow);
		if (!Game.IsRunningOnSteamDeck())
		{
			GameUI.m_Instance.m_Settings.AddRow(m_CameraRotateSpeedSliderRow);
			GameUI.m_Instance.m_Settings.AddRow(m_MouseWheelSpeedSliderRow);
			GameUI.m_Instance.m_Settings.AddRow(m_CameraPanSpeedSliderRow);
		}
	}

	public void Update()
	{
		UpdateLock2DText();
		UpdateEdgeBisectText();
	}

	public void Apply()
	{
		ApplyLanguage();
		ApplyMouseWheel();
		ApplyAutoSave();
		ApplyEdgeBisect();
		ApplyFirstBreak();
		ApplyLockBuildCamera();
		ApplyDisableTooltips();
		ApplyCameraControls();
	}

	public void OnResetToDefaults()
	{
		InterfaceAudio.Play("ui_settings_reset");
		PopUpMessage.DisplayConfirmation(Localize.Get("POPUP_RESET_OTHER"), useYesNoLabels: true, ConfirmOnDefaults);
	}

	private void SwitchLanguageImmediate()
	{
		if (m_LanguagesMap.ContainsKey(m_LanguageDropdown.value))
		{
			Profiles.m_ActiveProfile.m_LanguageCode = m_LanguagesMap[m_LanguageDropdown.value];
			Localize.SwitchToLanguage(Profiles.m_ActiveProfile.m_LanguageCode);
		}
	}

	private void ApplyLanguage()
	{
		SwitchLanguageImmediate();
		GameUI.m_Instance.m_TopBar.ForceBudgetTextToUpdate();
		GameUI.m_Instance.m_SandboxEditVehicle.ForceVehicleDropdownRefresh();
		GameUI.m_Instance.m_SandboxEditZedAxisVehicle.ForceVehicleDropdownRefresh();
		CampaignWorlds.m_Instance.RefreshCachedCampaignLevelStrings();
		SandboxThumbnails.RefreshLocalization();
	}

	private void ApplyMouseWheel()
	{
		Profiles.m_ActiveProfile.m_MouseWheelSpeedNormalized = Mathf.Clamp01(m_MouseWheelSpeedSlider.value / 100f);
	}

	private void ApplyAutoSave()
	{
		Profiles.m_ActiveProfile.m_AutomatiallyLoadAutoSave = m_LoadAutoSaveToggle.isOn;
	}

	private void ApplyEdgeBisect()
	{
		Profiles.m_ActiveProfile.m_EdgeBisectEnabled = m_EdgeBisectToggle.isOn;
	}

	private void ApplyFirstBreak()
	{
		if (Profiles.m_ActiveProfile.m_FirstBreakEnabled == m_FirstBreakToggle.isOn)
		{
			return;
		}
		Profiles.m_ActiveProfile.m_FirstBreakEnabled = m_FirstBreakToggle.isOn;
		if (GameStateManager.GetState() == GameState.BUILD)
		{
			if (Profiles.m_ActiveProfile.m_FirstBreakEnabled)
			{
				GameStateBuild.ShowFirstBreak();
			}
			else
			{
				GameStateBuild.DestroyFirstBreakBox();
			}
		}
	}

	private void ApplyLockBuildCamera()
	{
		if (m_LockBuildCameraToggle.isOn && !Profiles.m_ActiveProfile.m_LockBuildCamera && GameStateManager.GetState() == GameState.SIM)
		{
			PointsOfView.SnapTo(PointOfViewType.SIM_CENTER);
		}
		Profiles.m_ActiveProfile.m_LockBuildCamera = m_LockBuildCameraToggle.isOn;
	}

	private void ApplyDisableTooltips()
	{
		Profiles.m_ActiveProfile.m_DisableTooltips = !m_EnableTooltipsToggle.isOn;
		Profiles.m_ActiveProfile.m_DisableBuildDataTooltips = !m_EnableBuildDataTooltipsToggle.isOn;
		Profiles.m_ActiveProfile.m_DisableBuildHelpTooltips = !m_EnableBuildHelpTooltipsToggle.isOn;
	}

	private void ApplyCameraControls()
	{
		Profiles.m_ActiveProfile.m_CameraRotateSpeedNormalized = Mathf.Clamp01(m_CameraRotateSpeedSlider.value / 100f);
		Profiles.m_ActiveProfile.m_CameraPanSpeedNormalized = Mathf.Clamp01(m_CameraPanSpeedSlider.value / 100f);
	}

	private void OnMouseWheelSpeedChanged(float value)
	{
		m_MouseWheelSpeedPreview.text = Utils.FormatPercentage(m_MouseWheelSpeedSlider.value / 100f);
	}

	private void OnCameraRotateSpeedChanged(float value)
	{
		m_CameraRotateSpeedPreview.text = Utils.FormatPercentage(m_CameraRotateSpeedSlider.value / 100f);
	}

	private void OnCameraPanSpeedChanged(float value)
	{
		m_CameraPanSpeedPreview.text = Utils.FormatPercentage(m_CameraPanSpeedSlider.value / 100f);
	}

	private void PopulateLanguageDropdown()
	{
		m_LanguagesMap.Clear();
		List<TMP_Dropdown.OptionData> list = new List<TMP_Dropdown.OptionData>();
		List<string> allLanguagesCode = LocalizationManager.GetAllLanguagesCode();
		for (int i = 0; i < allLanguagesCode.Count; i++)
		{
			if (!string.IsNullOrEmpty(allLanguagesCode[i]))
			{
				list.Add(new TMP_Dropdown.OptionData(Localize.GetLanguageNameLocalized(allLanguagesCode[i])));
				m_LanguagesMap.Add(list.Count - 1, allLanguagesCode[i]);
			}
		}
		m_LanguageDropdown.options = list;
	}

	private void UpdateLock2DText()
	{
		Binding binding = Bindings.GetBinding(BindingType.LOCK_2D);
		if (Game.IsSteamDeckOrMobile())
		{
			m_LockBuildCameraText.text = Localize.Get("UI_SETTINGS_LOCK_BUILD_CAMERA");
		}
		else if (binding.IsUnBound())
		{
			m_LockBuildCameraText.text = Localize.Get("UI_SETTINGS_LOCK_BUILD_CAMERA") + " " + Localize.Get("UI_UNBOUND").Replace('[', '(').Replace(']', ')');
		}
		else
		{
			m_LockBuildCameraText.text = Localize.Get("UI_SETTINGS_LOCK_BUILD_CAMERA") + " (" + binding.GetTooltipBindingString() + ")";
		}
	}

	private void UpdateEdgeBisectText()
	{
		Binding binding = Bindings.GetBinding(BindingType.EDGE_BISECT);
		if (Game.IsSteamDeckOrMobile())
		{
			m_EdgeBisectText.text = Localize.Get("BINDING_EDGE_BISECT");
		}
		else if (binding.IsUnBound())
		{
			m_EdgeBisectText.text = Localize.Get("BINDING_EDGE_BISECT") + " " + Localize.Get("UI_UNBOUND").Replace('[', '(').Replace(']', ')');
		}
		else
		{
			m_EdgeBisectText.text = Localize.Get("BINDING_EDGE_BISECT") + " (" + binding.GetTooltipBindingString() + ")";
		}
	}

	private void ConfirmOnDefaults()
	{
		m_LanguageDropdown.value = (int)Localize.GetSystemLanguage();
		m_LoadAutoSaveToggle.isOn = true;
		m_LockBuildCameraToggle.isOn = false;
		m_EnableTooltipsToggle.isOn = true;
		m_EnableBuildDataTooltipsToggle.isOn = true;
		m_EnableBuildHelpTooltipsToggle.isOn = true;
		m_EdgeBisectToggle.isOn = true;
		m_FirstBreakToggle.isOn = true;
		m_MouseWheelSpeedSlider.value = Mathf.Round(GameSettings.DefaultMouseWheelSpeedNormalized() * 100f);
		m_CameraRotateSpeedSlider.value = Mathf.Round(GameSettings.DefaultCameraRotateSpeedNormalized() * 100f);
		m_CameraPanSpeedSlider.value = Mathf.Round(GameSettings.DefaultCameraPanSpeedNormalized() * 100f);
	}

	private void SetRowCallbacks()
	{
		m_LanguageDropdownRow.m_Action = LanguageDropdownCallback;
		m_LoadAutoSaveToggleRow.m_Action = LoadAutoSaveToggleCallback;
		m_LockBuildCameraToggleRow.m_Action = LockBuildCameraToggleCallback;
		m_EnableTooltipsToggleRow.m_Action = EnableTooltipsToggleCallback;
		m_EnableBuildDataTooltipsToggleRow.m_Action = EnableBuildDataTooltipsToggleCallback;
		m_EnableBuildHelpTooltipsToggleRow.m_Action = EnableBuildHelpTooltipsToggleCallback;
		m_EdgeBisectToggleRow.m_Action = EdgeBisectToggleCallback;
		m_FirstBreakToggleRow.m_Action = FirstBreakToggleCallback;
		m_CameraRotateSpeedSliderRow.m_Action = CameraRotateSpeedSliderCallback;
		m_MouseWheelSpeedSliderRow.m_Action = MouseWheelSpeedSliderCallback;
		m_CameraPanSpeedSliderRow.m_Action = CameraPanSpeedSliderCallback;
	}

	private void LanguageDropdownCallback(GamepadButtonType button)
	{
		SettingsRow.DropdownProcessInput(m_LanguageDropdown, button);
		SwitchLanguageImmediate();
	}

	private void LoadAutoSaveToggleCallback(GamepadButtonType button)
	{
		SettingsRow.ToggleProcessInput(m_LoadAutoSaveToggle, button);
	}

	private void LockBuildCameraToggleCallback(GamepadButtonType button)
	{
		SettingsRow.ToggleProcessInput(m_LockBuildCameraToggle, button);
	}

	private void EnableTooltipsToggleCallback(GamepadButtonType button)
	{
		SettingsRow.ToggleProcessInput(m_EnableTooltipsToggle, button);
	}

	private void EnableBuildDataTooltipsToggleCallback(GamepadButtonType button)
	{
		SettingsRow.ToggleProcessInput(m_EnableBuildDataTooltipsToggle, button);
	}

	private void EnableBuildHelpTooltipsToggleCallback(GamepadButtonType button)
	{
		SettingsRow.ToggleProcessInput(m_EnableBuildHelpTooltipsToggle, button);
	}

	private void EdgeBisectToggleCallback(GamepadButtonType button)
	{
		SettingsRow.ToggleProcessInput(m_EdgeBisectToggle, button);
	}

	private void FirstBreakToggleCallback(GamepadButtonType button)
	{
		SettingsRow.ToggleProcessInput(m_FirstBreakToggle, button);
	}

	private void CameraRotateSpeedSliderCallback(GamepadButtonType button)
	{
		SettingsRow.SliderProcessInput(m_CameraRotateSpeedSlider, button);
	}

	private void MouseWheelSpeedSliderCallback(GamepadButtonType button)
	{
		SettingsRow.SliderProcessInput(m_MouseWheelSpeedSlider, button);
	}

	private void CameraPanSpeedSliderCallback(GamepadButtonType button)
	{
		SettingsRow.SliderProcessInput(m_CameraPanSpeedSlider, button);
	}

	private void OnLanguageChanged()
	{
		SwitchLanguageImmediate();
	}
}
