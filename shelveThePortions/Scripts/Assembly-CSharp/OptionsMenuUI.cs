using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;
using VInspector;

public class OptionsMenuUI : PanelUI<OptionsMenuUI>
{
	[SerializeField]
	private List<GameObject> panelsList;

	[ReadOnly]
	[SerializeField]
	private Volume globalVolume;

	private ColorAdjustments colorAdjustments;

	[Space]
	[Header("Resolution")]
	[SerializeField]
	private ResolutionManager resolutionManager;

	[SerializeField]
	private UISliderPanel FOVSlider;

	[SerializeField]
	private UISliderPanel brightnessSlider;

	[SerializeField]
	private float minBrightness = -1.5f;

	[SerializeField]
	private float maxBrightness = 1.5f;

	[Space]
	[Header("Language")]
	[SerializeField]
	private UILocalizationManager uILocalizationManager;

	[Space]
	[Header("Accesbility")]
	[SerializeField]
	private UISliderPanel reticaleSlider;

	[SerializeField]
	private Image colorblindToggle;

	[Space]
	[Header("Game")]
	[SerializeField]
	private UISliderPanel mouseSlider;

	[SerializeField]
	private Image mouseXToggle;

	[SerializeField]
	private Image mouseYToggle;

	[SerializeField]
	private Image mouseWheelToggle;

	[Space]
	[SerializeField]
	private UISliderPanel controllerSlider;

	[SerializeField]
	private Image controllerXToggle;

	[SerializeField]
	private Image controllerYToggle;

	[Space]
	[SerializeField]
	private Image headBobToggle;

	[SerializeField]
	private Image disableHUDToggle;

	[SerializeField]
	private Image inGameTimerToggle;

	[SerializeField]
	private Image disableBatToggle;

	[SerializeField]
	private Image disableAbilitiesToggle;

	[SerializeField]
	private Image hoverSolutionToggle;

	[Space]
	[Header("Buttons")]
	[SerializeField]
	private Button applyButton;

	[SerializeField]
	private Button resetButton;

	[Space]
	[SerializeField]
	private Sprite unTickSprite;

	[SerializeField]
	private Sprite TickSprite;

	[Space]
	[Header("Audio Sliders")]
	[SerializeField]
	private UISliderPanel MasterUISliderPanel;

	[SerializeField]
	private UISliderPanel MusicUISliderPanel;

	[SerializeField]
	private UISliderPanel SFXUISliderPanel;

	[SerializeField]
	private UISliderPanel AmbientSliderPanel;

	private void Start()
	{
		globalVolume = Object.FindAnyObjectByType<Volume>();
		if (globalVolume == null)
		{
			Debug.LogError("Can't find the global Volume in the sceen");
		}
		else
		{
			globalVolume.profile.TryGet<ColorAdjustments>(out colorAdjustments);
		}
		if (colorAdjustments != null)
		{
			colorAdjustments.postExposure.value = Mathf.LerpUnclamped(minBrightness, maxBrightness, SaveSystem.GetBrightnessSlider());
		}
	}

	public override void SetSelectedButton()
	{
		LoadOptions();
		base.SetSelectedButton();
	}

	public void OpenPanel(int index)
	{
		foreach (GameObject panels in panelsList)
		{
			panels.SetActive(value: false);
		}
		panelsList[index].SetActive(value: true);
	}

	public void ExitMenu()
	{
		UIBackKeyManager.BackUI();
	}

	public void LoadOptions()
	{
		LoadAudioSlidersValues();
		LoadGameOptions();
		LoadAccesbility();
		resolutionManager.loadSettings();
		uILocalizationManager.loadSettings();
		resetButton.gameObject.SetActive(value: false);
		applyButton.gameObject.SetActive(value: false);
		FOVSlider.SetSliderValue(SaveSystem.GetFOVSlider(), SceneSingleton<FirstPersonController>.Instance.GetFOV());
		brightnessSlider.SetSliderValue(SaveSystem.GetBrightnessSlider());
		EventSystem.current.SetSelectedGameObject(selectedButton);
	}

	public void ApplyOptions()
	{
		Singleton<AudioManager>.Instance.PlayClip(AudioClipTypes.UI_Click);
		resolutionManager.ApplySettings();
		LoadOptions();
		UpdateApplyChanges();
		EventSystem.current.SetSelectedGameObject(selectedButton);
	}

	public void OnFOVSliderPanelChange(float value)
	{
		SaveSystem.SetFOVSlider(value);
		SceneSingleton<FirstPersonController>.Instance.UpdateOptions();
		FOVSlider.SetSliderValue(SaveSystem.GetFOVSlider(), SceneSingleton<FirstPersonController>.Instance.GetFOV());
	}

	public void OnBrightnessSliderPanelChange(float value)
	{
		SaveSystem.SetBrightnessSlider(value);
		if (colorAdjustments != null)
		{
			colorAdjustments.postExposure.value = Mathf.LerpUnclamped(minBrightness, maxBrightness, SaveSystem.GetBrightnessSlider());
		}
		brightnessSlider.SetSliderValue(SaveSystem.GetBrightnessSlider());
	}

	public void UpdateApplyChanges()
	{
		bool flag = false;
		flag |= resolutionManager.IsSettingsChanged();
		resetButton.gameObject.SetActive(flag);
		applyButton.gameObject.SetActive(flag);
	}

	public void LoadAudioSlidersValues()
	{
		MasterUISliderPanel.SetSliderValue(SaveSystem.GetMasterSlider());
		MusicUISliderPanel.SetSliderValue(SaveSystem.GetMusicSlider());
		SFXUISliderPanel.SetSliderValue(SaveSystem.GetSFXSlider());
		AmbientSliderPanel.SetSliderValue(SaveSystem.GetEnvironmentSlider());
	}

	public void OnMasterUISliderPanelChange(float value)
	{
		AudioSlidersController.SetMasterSlider(value);
		MasterUISliderPanel.SetSliderValue(SaveSystem.GetMasterSlider());
	}

	public void OnMusicUISliderPanelChange(float value)
	{
		AudioSlidersController.SetMusicSlider(value);
		MusicUISliderPanel.SetSliderValue(SaveSystem.GetMusicSlider());
	}

	public void OnSFXUISliderPanelChange(float value)
	{
		AudioSlidersController.SetSFXSlider(value);
		SFXUISliderPanel.SetSliderValue(SaveSystem.GetSFXSlider());
	}

	public void OnAmbientUISliderPanelChange(float value)
	{
		AudioSlidersController.SetEnvironmentSlider(value);
		AmbientSliderPanel.SetSliderValue(SaveSystem.GetEnvironmentSlider());
	}

	public void LoadAccesbility()
	{
		headBobToggle.GetComponent<Image>().sprite = (SaveSystem.GetHeadBobSetting() ? TickSprite : unTickSprite);
		hoverSolutionToggle.GetComponent<Image>().sprite = (SaveSystem.GetHoverSolutionToggleSetting() ? TickSprite : unTickSprite);
		reticaleSlider.SetSliderValue(SaveSystem.GetReticleSlider());
		colorblindToggle.GetComponent<Image>().sprite = (SaveSystem.GetIsColorBlindSetting() ? TickSprite : unTickSprite);
	}

	public void OnReticaleSliderPanelChange(float value)
	{
		SaveSystem.SetReticleSlider(value);
		SceneSingleton<UIManager>.Instance.UpdateReticaleSize();
		reticaleSlider.SetSliderValue(SaveSystem.GetReticleSlider());
	}

	public void TogglColorBlind()
	{
		SaveSystem.SetIsColorBlindSetting(!SaveSystem.GetIsColorBlindSetting());
		SceneSingleton<PotionsManager>.Instance.UpdateColorBlind();
		SceneSingleton<CabinetsManager>.Instance.UpdateColorBlind();
		SceneSingleton<SolvedPuzzlesPanelUI>.Instance.UpdateColorBlind();
		colorblindToggle.GetComponent<Image>().sprite = (SaveSystem.GetIsColorBlindSetting() ? TickSprite : unTickSprite);
	}

	public void ToggleHeadBob()
	{
		SaveSystem.SetHeadBobSetting(!SaveSystem.GetHeadBobSetting());
		SceneSingleton<FirstPersonController>.Instance.UpdateOptions();
		headBobToggle.GetComponent<Image>().sprite = (SaveSystem.GetHeadBobSetting() ? TickSprite : unTickSprite);
	}

	public void ToggleHoverSolutionToggle()
	{
		SaveSystem.SetHoverSolutionToggleSetting(!SaveSystem.GetHoverSolutionToggleSetting());
		hoverSolutionToggle.GetComponent<Image>().sprite = (SaveSystem.GetHoverSolutionToggleSetting() ? TickSprite : unTickSprite);
		SceneSingleton<QuickPuzzleSolutionPanelUI>.Instance.UpdateIsToggle();
	}

	public void LoadGameOptions()
	{
		mouseSlider.SetSliderValue(SaveSystem.GetMouseSensitivitySlider());
		controllerSlider.SetSliderValue(SaveSystem.GetControllerSensitivitySlider());
		mouseXToggle.GetComponent<Image>().sprite = (SaveSystem.GetInvertMouseXSetting() ? TickSprite : unTickSprite);
		mouseYToggle.GetComponent<Image>().sprite = (SaveSystem.GetInvertMouseYSetting() ? TickSprite : unTickSprite);
		mouseWheelToggle.GetComponent<Image>().sprite = (SaveSystem.GetInvertMouseWheelSetting() ? TickSprite : unTickSprite);
		controllerXToggle.GetComponent<Image>().sprite = (SaveSystem.GetInvertControllerXSetting() ? TickSprite : unTickSprite);
		controllerYToggle.GetComponent<Image>().sprite = (SaveSystem.GetInvertControllerYSetting() ? TickSprite : unTickSprite);
		disableHUDToggle.GetComponent<Image>().sprite = (SaveSystem.GetDisableHUDSetting() ? TickSprite : unTickSprite);
		disableAbilitiesToggle.GetComponent<Image>().sprite = (SaveSystem.GetDisableAbilitiesSetting() ? TickSprite : unTickSprite);
		disableBatToggle.GetComponent<Image>().sprite = (SaveSystem.GetDisableBatsSetting() ? TickSprite : unTickSprite);
	}

	public void OnMouseSliderPanelChange(float value)
	{
		SaveSystem.SetMouseSensitivitySlider(value);
		SceneSingleton<FirstPersonController>.Instance.UpdateOptions();
		mouseSlider.SetSliderValue(SaveSystem.GetMouseSensitivitySlider());
	}

	public void OnControllerSliderPanelChange(float value)
	{
		SaveSystem.SetControllerSensitivitySlider(value);
		SceneSingleton<FirstPersonController>.Instance.UpdateOptions();
		controllerSlider.SetSliderValue(SaveSystem.GetControllerSensitivitySlider());
	}

	public void ToggleDisableAbilities()
	{
		SaveSystem.SetDisableAbilitiesSetting(!SaveSystem.GetDisableAbilitiesSetting());
		disableAbilitiesToggle.GetComponent<Image>().sprite = (SaveSystem.GetDisableAbilitiesSetting() ? TickSprite : unTickSprite);
	}

	public void ToggleDisableHUD()
	{
		SaveSystem.SetDisableHUDSetting(!SaveSystem.GetDisableHUDSetting());
		SceneSingleton<UIManager>.Instance.UpdateDisableInGameHudOption();
		disableHUDToggle.GetComponent<Image>().sprite = (SaveSystem.GetDisableHUDSetting() ? TickSprite : unTickSprite);
	}

	public void ToggleInGameTimer()
	{
		SaveSystem.SetDisableInGameTimer(!SaveSystem.GetDisableInGameTimer());
		SceneSingleton<UIManager>.Instance.UpdateDisableInGameTimer();
		inGameTimerToggle.GetComponent<Image>().sprite = (SaveSystem.GetDisableInGameTimer() ? TickSprite : unTickSprite);
	}

	public void ToggleInvertMouseX()
	{
		SaveSystem.SetInvertMouseXSetting(!SaveSystem.GetInvertMouseXSetting());
		Singleton<InputManager>.Instance.UpdateInverts();
		mouseXToggle.GetComponent<Image>().sprite = (SaveSystem.GetInvertMouseXSetting() ? TickSprite : unTickSprite);
	}

	public void ToggleInvertMouseY()
	{
		SaveSystem.SetInvertMouseYSetting(!SaveSystem.GetInvertMouseYSetting());
		Singleton<InputManager>.Instance.UpdateInverts();
		mouseYToggle.GetComponent<Image>().sprite = (SaveSystem.GetInvertMouseYSetting() ? TickSprite : unTickSprite);
	}

	public void ToggleInvertMouseWheel()
	{
		SaveSystem.SetInvertMouseWheelSetting(!SaveSystem.GetInvertMouseWheelSetting());
		Singleton<InputManager>.Instance.UpdateInverts();
		mouseWheelToggle.GetComponent<Image>().sprite = (SaveSystem.GetInvertMouseWheelSetting() ? TickSprite : unTickSprite);
	}

	public void ToggleInvertControllerX()
	{
		SaveSystem.SetInvertControllerXSetting(!SaveSystem.GetInvertControllerXSetting());
		Singleton<InputManager>.Instance.UpdateInverts();
		controllerXToggle.GetComponent<Image>().sprite = (SaveSystem.GetInvertControllerXSetting() ? TickSprite : unTickSprite);
	}

	public void ToggleInvertControllerY()
	{
		SaveSystem.SetInvertControllerYSetting(!SaveSystem.GetInvertControllerYSetting());
		Singleton<InputManager>.Instance.UpdateInverts();
		controllerYToggle.GetComponent<Image>().sprite = (SaveSystem.GetInvertControllerYSetting() ? TickSprite : unTickSprite);
	}

	public void ToggleDisableBatToggle()
	{
		SaveSystem.SetDisableBatsSetting(!SaveSystem.GetDisableBatsSetting());
		SceneSingleton<EnvironmentManager>.Instance.UpdateBats();
		disableBatToggle.GetComponent<Image>().sprite = (SaveSystem.GetDisableBatsSetting() ? TickSprite : unTickSprite);
	}
}
