using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Panel_SettingsGamepad : MonoBehaviour
{
	public Transform m_Content;

	[Header("Block Gamepad Input")]
	public Toggle m_BlockGamepadInputToggle;

	[Header("Gamepad Cursor Acceleration")]
	public Toggle m_GamepadAccelerationToggle;

	[Header("Gamepad Cursor Speed")]
	public Slider m_GamepadCursorSpeedSlider;

	public TextMeshProUGUI m_GamepadCursorSpeedPreview;

	[Header("Gamepad Zoom Speed")]
	public Slider m_GamepadZoomSpeedSlider;

	public TextMeshProUGUI m_GamepadZoomSpeedPreview;

	[Header("Gamepad Rotate Camera Speed")]
	public Slider m_GamepadRotateCameraSpeedSlider;

	public TextMeshProUGUI m_GamepadRotateCameraSpeedPreview;

	[Header("Button Icons")]
	public TMP_Dropdown m_ButtonIconsDropdown;

	[Header("Footer")]
	public Button m_ResetToDefaults;

	[Header("Rows")]
	public SettingsRow m_BlockGamepadInputToggleRow;

	public SettingsRow m_GamepadAccelerationToggleRow;

	public SettingsRow m_GamepadCursorSpeedSliderRow;

	public SettingsRow m_GamepadZoomSpeedSliderRow;

	public SettingsRow m_GamepadRotateCameraSpeedSliderRow;

	public SettingsRow m_ButtonIconsDropdownRow;

	private PointerEvents m_BlockGamepadInputTogglePointerEvents;

	private PointerEvents m_GamepadAccelerationTogglePointerEvents;

	private float m_RestoreGamepadCursorSpeed;

	public void Start()
	{
		m_BlockGamepadInputTogglePointerEvents = m_BlockGamepadInputToggle.GetComponent<PointerEvents>();
		m_BlockGamepadInputTogglePointerEvents.RegisterOnClickedDelegate(InterfaceAudio.PlayToggleAudio);
		m_GamepadAccelerationTogglePointerEvents = m_GamepadAccelerationToggle.GetComponent<PointerEvents>();
		m_GamepadAccelerationTogglePointerEvents.RegisterOnClickedDelegate(OnGamepadAccelerationChanged);
		m_ResetToDefaults.onClick.AddListener(OnResetToDefaults);
		m_ButtonIconsDropdown.alphaFadeSpeed = 0f;
		SetRowCallbacks();
	}

	public void OnEnableManual()
	{
		m_BlockGamepadInputToggle.isOn = Profiles.m_ActiveProfile.m_BlockGamepadInput;
		m_GamepadAccelerationToggle.isOn = Profiles.m_ActiveProfile.m_GamepadAcceleration;
		m_GamepadCursorSpeedSlider.onValueChanged.AddListener(OnGamepadCursorSpeedChanged);
		m_GamepadZoomSpeedSlider.onValueChanged.AddListener(OnGamepadZoomSpeedChanged);
		m_GamepadRotateCameraSpeedSlider.onValueChanged.AddListener(OnGamepadRotateCameraSpeedChanged);
		m_GamepadCursorSpeedSlider.value = Mathf.Round(Profiles.m_ActiveProfile.m_GamepadCursorSpeedNormalized * 100f);
		m_GamepadZoomSpeedSlider.value = Mathf.Round(Profiles.m_ActiveProfile.m_GamepadZoomSpeedNormalized * 100f);
		m_GamepadRotateCameraSpeedSlider.value = Mathf.Round(Profiles.m_ActiveProfile.m_GamepadRotateCameraSpeedNormalized * 100f);
		m_RestoreGamepadCursorSpeed = Profiles.m_ActiveProfile.m_GamepadCursorSpeedNormalized;
		Utils.EnableToggle(m_BlockGamepadInputToggle.gameObject, !Game.IsRunningOnSteamDeck());
		PopulateButtonIconsDropdown();
		SelectButtonIconsChoice(Profiles.m_ActiveProfile.m_GamepadButtonIconsChoice);
		GameUI.m_Instance.m_Settings.SetRowsColor(m_Content);
	}

	public void OnEnable()
	{
		GameUI.m_Instance.m_Settings.ClearRows();
		if (!Game.IsRunningOnSteamDeck())
		{
			GameUI.m_Instance.m_Settings.AddRow(m_BlockGamepadInputToggleRow);
		}
		GameUI.m_Instance.m_Settings.AddRow(m_GamepadAccelerationToggleRow);
		GameUI.m_Instance.m_Settings.AddRow(m_GamepadCursorSpeedSliderRow);
		GameUI.m_Instance.m_Settings.AddRow(m_GamepadZoomSpeedSliderRow);
		GameUI.m_Instance.m_Settings.AddRow(m_GamepadRotateCameraSpeedSliderRow);
		GameUI.m_Instance.m_Settings.AddRow(m_ButtonIconsDropdownRow);
	}

	public void RestoreOriginalCursorSpeed()
	{
		Profiles.m_ActiveProfile.m_GamepadCursorSpeedNormalized = m_RestoreGamepadCursorSpeed;
	}

	public void Apply()
	{
		Profiles.m_ActiveProfile.m_BlockGamepadInput = m_BlockGamepadInputToggle.isOn;
		if (Profiles.m_ActiveProfile.m_BlockGamepadInput)
		{
			GameInput.ChangeActiveGameDevice(GameDevice.KeyboardAndMouse);
		}
		ApplyGamepadAcceleration();
		ApplyGamepadCursorSpeed();
		ApplyGamepadRotateCameraSpeed();
		ApplyGamepadZoomSpeed();
		ApplyGamepadButtonIconsChoice();
	}

	public void OnDisableManual()
	{
		m_GamepadCursorSpeedSlider.onValueChanged.RemoveAllListeners();
		m_GamepadZoomSpeedSlider.onValueChanged.RemoveAllListeners();
	}

	public void OnResetToDefaults()
	{
		InterfaceAudio.Play("ui_settings_reset");
		PopUpMessage.DisplayConfirmation(Localize.Get("POPUP_RESET_GAMEPAD"), useYesNoLabels: true, ConfirmOnDefaults);
	}

	private void OnGamepadCursorSpeedChanged(float value)
	{
		m_GamepadCursorSpeedPreview.text = Utils.FormatPercentage(m_GamepadCursorSpeedSlider.value / 100f);
		ApplyGamepadCursorSpeed();
	}

	private void OnGamepadZoomSpeedChanged(float value)
	{
		m_GamepadZoomSpeedPreview.text = Utils.FormatPercentage(m_GamepadZoomSpeedSlider.value / 100f);
	}

	private void OnGamepadRotateCameraSpeedChanged(float value)
	{
		m_GamepadRotateCameraSpeedPreview.text = Utils.FormatPercentage(m_GamepadRotateCameraSpeedSlider.value / 100f);
	}

	private void ConfirmOnDefaults()
	{
		m_BlockGamepadInputToggle.isOn = false;
		m_GamepadAccelerationToggle.isOn = true;
		m_GamepadCursorSpeedSlider.value = Mathf.Round(GamepadManager.GetDefaultCursorSpeedNormalized() * 100f);
		m_GamepadRotateCameraSpeedSlider.value = Mathf.Round(GamepadManager.GetDefaultRotateCameraSpeedNormalized() * 100f);
		m_GamepadZoomSpeedSlider.value = Mathf.Round(GamepadManager.GetDefaultZoomSpeedNormalized() * 100f);
		ApplyGamepadCursorSpeed();
	}

	private void ApplyGamepadAcceleration()
	{
		Profiles.m_ActiveProfile.m_GamepadAcceleration = m_GamepadAccelerationToggle.isOn;
	}

	private void ApplyGamepadCursorSpeed()
	{
		Profiles.m_ActiveProfile.m_GamepadCursorSpeedNormalized = Mathf.Clamp01(m_GamepadCursorSpeedSlider.value / 100f);
	}

	private void ApplyGamepadRotateCameraSpeed()
	{
		Profiles.m_ActiveProfile.m_GamepadRotateCameraSpeedNormalized = Mathf.Clamp01(m_GamepadRotateCameraSpeedSlider.value / 100f);
	}

	private void ApplyGamepadZoomSpeed()
	{
		Profiles.m_ActiveProfile.m_GamepadZoomSpeedNormalized = Mathf.Clamp01(m_GamepadZoomSpeedSlider.value / 100f);
	}

	private void ApplyGamepadButtonIconsChoice()
	{
		Profiles.m_ActiveProfile.m_GamepadButtonIconsChoice = (GamepadButtonIconsChoice)m_ButtonIconsDropdown.value;
	}

	private void PopulateButtonIconsDropdown()
	{
		List<TMP_Dropdown.OptionData> list = new List<TMP_Dropdown.OptionData>();
		list.Add(new TMP_Dropdown.OptionData(Localize.Get("UI_DETECTED", GamepadManager.GetLocalizedGamepadType(GamepadManager.DetectGamepadType()))));
		list.Add(new TMP_Dropdown.OptionData(Localize.Get("UI_STEAMDECK")));
		list.Add(new TMP_Dropdown.OptionData(Localize.Get("UI_PLAYSTATION_SERIES")));
		list.Add(new TMP_Dropdown.OptionData(Localize.Get("UI_XBOX_SERIES")));
		list.Add(new TMP_Dropdown.OptionData(Localize.Get("UI_SWITCH_SERIES")));
		m_ButtonIconsDropdown.options = list;
	}

	private void SelectButtonIconsChoice(GamepadButtonIconsChoice choice)
	{
		m_ButtonIconsDropdown.SetValueWithoutNotify((int)choice);
	}

	private void SetRowCallbacks()
	{
		m_BlockGamepadInputToggleRow.m_Action = BlockGamepadInputToggleCallback;
		m_GamepadAccelerationToggleRow.m_Action = GamepadAccelerationToggleCallback;
		m_GamepadCursorSpeedSliderRow.m_Action = GamepadCursorSpeedSliderCallback;
		m_GamepadZoomSpeedSliderRow.m_Action = GamepadZoomSpeedSliderCallback;
		m_GamepadRotateCameraSpeedSliderRow.m_Action = GamepadRotateCameraSpeedSliderCallback;
		m_ButtonIconsDropdownRow.m_Action = ButtonIconsDropdownCallback;
	}

	private void BlockGamepadInputToggleCallback(GamepadButtonType button)
	{
		SettingsRow.ToggleProcessInput(m_BlockGamepadInputToggle, button);
	}

	private void GamepadAccelerationToggleCallback(GamepadButtonType button)
	{
		SettingsRow.ToggleProcessInput(m_GamepadAccelerationToggle, button);
		OnGamepadAccelerationChanged();
	}

	private void GamepadCursorSpeedSliderCallback(GamepadButtonType button)
	{
		SettingsRow.SliderProcessInput(m_GamepadCursorSpeedSlider, button);
		ApplyGamepadCursorSpeed();
	}

	private void GamepadRotateCameraSpeedSliderCallback(GamepadButtonType button)
	{
		SettingsRow.SliderProcessInput(m_GamepadRotateCameraSpeedSlider, button);
	}

	private void GamepadZoomSpeedSliderCallback(GamepadButtonType button)
	{
		SettingsRow.SliderProcessInput(m_GamepadZoomSpeedSlider, button);
	}

	private void ButtonIconsDropdownCallback(GamepadButtonType button)
	{
		SettingsRow.DropdownProcessInput(m_ButtonIconsDropdown, button);
	}

	private void OnGamepadAccelerationChanged()
	{
		InterfaceAudio.PlayToggleAudio();
		ApplyGamepadAcceleration();
	}
}
