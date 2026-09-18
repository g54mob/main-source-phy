using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ResolutionManager : MonoBehaviour
{
	[SerializeField]
	private UISelectorPanel ResolutionSelectorPanel;

	[SerializeField]
	private UISelectorPanel ScreenSelectorPanel;

	[SerializeField]
	private UISelectorPanel fpsSelectorPanel;

	[SerializeField]
	private CanvasGroup fpsSelectorCG;

	[SerializeField]
	private Image VsyncToggle;

	[Space]
	[SerializeField]
	private Sprite unTickSprite;

	[SerializeField]
	private Sprite TickSprite;

	private int resolutionsIndex = 5;

	private int screenModesIndex = 1;

	private int fpsModesIndex = 1;

	private bool vsync;

	private void OnEnable()
	{
		Singleton<LocalizationSystem>.Instance.OnLanguageChangeUIUpdate += OnLanguageChangeUIUpdate;
	}

	private void OnDisable()
	{
		if (Singleton<LocalizationSystem>.Instance != null)
		{
			Singleton<LocalizationSystem>.Instance.OnLanguageChangeUIUpdate -= OnLanguageChangeUIUpdate;
		}
	}

	private void OnLanguageChangeUIUpdate()
	{
		ScreenSelectorPanel.SetSelectorText(GetScreenText());
	}

	public void loadSettings()
	{
		resolutionsIndex = SaveSystem.GetResulationSetting();
		ResolutionSelectorPanel.SetSelectorText(GetResulationText(), requireLocalization: false);
		screenModesIndex = SaveSystem.GetScreenModeSetting();
		ScreenSelectorPanel.SetSelectorText(GetScreenText());
		vsync = SaveSystem.GetVsyncSetting();
		LoadFPSVsyncOptions();
	}

	public bool IsSettingsChanged()
	{
		if (resolutionsIndex != SaveSystem.GetResulationSetting())
		{
			return true;
		}
		if (screenModesIndex != SaveSystem.GetScreenModeSetting())
		{
			return true;
		}
		if (fpsModesIndex != FPSLimiter.GetFPSIndex())
		{
			return true;
		}
		if (vsync != SaveSystem.GetVsyncSetting())
		{
			return true;
		}
		return false;
	}

	public void ApplySettings()
	{
		if (resolutionsIndex != SaveSystem.GetResulationSetting() || screenModesIndex != SaveSystem.GetScreenModeSetting())
		{
			Screen.SetResolution(Singleton<ScreenSettingDataStore>.Instance.GetResolution(resolutionsIndex).width, Singleton<ScreenSettingDataStore>.Instance.GetResolution(resolutionsIndex).height, Singleton<ScreenSettingDataStore>.Instance.GetScreenMode(screenModesIndex).screenMode);
		}
		SaveSystem.SaveResulationSetting(resolutionsIndex);
		SaveSystem.SaveScreenModeSetting(screenModesIndex);
		SaveSystem.SetVsyncSetting(vsync);
		FPSLimiter.SetFPSIndex(fpsModesIndex);
		StopAllCoroutines();
		StartCoroutine(ApplySettingsEndOfTheFrame());
	}

	private IEnumerator ApplySettingsEndOfTheFrame()
	{
		yield return new WaitForEndOfFrame();
		EventManager.ActivateEvent(EventTypes.OnResulationChanged);
	}

	public void ModifyResolution(int amount)
	{
		resolutionsIndex += amount;
		if (resolutionsIndex < 0)
		{
			resolutionsIndex = Singleton<ScreenSettingDataStore>.Instance.GetResolutionCount() - 1;
		}
		else if (resolutionsIndex >= Singleton<ScreenSettingDataStore>.Instance.GetResolutionCount())
		{
			resolutionsIndex = 0;
		}
		ResolutionSelectorPanel.SetSelectorText(GetResulationText());
		SceneSingleton<OptionsMenuUI>.Instance.UpdateApplyChanges();
	}

	public void ModifyScreenMode(int amount)
	{
		screenModesIndex += amount;
		if (screenModesIndex < 0)
		{
			screenModesIndex = Singleton<ScreenSettingDataStore>.Instance.GetScreenModeCount() - 1;
		}
		else if (screenModesIndex >= Singleton<ScreenSettingDataStore>.Instance.GetScreenModeCount())
		{
			screenModesIndex = 0;
		}
		ScreenSelectorPanel.SetSelectorText(GetScreenText());
		SceneSingleton<OptionsMenuUI>.Instance.UpdateApplyChanges();
	}

	public void ModifyFPS(int amount)
	{
		fpsModesIndex += amount;
		if (fpsModesIndex < 0)
		{
			fpsModesIndex = 0;
		}
		else if (fpsModesIndex >= FPSLimiter.fpsOptions.Length)
		{
			fpsModesIndex = FPSLimiter.fpsOptions.Length - 1;
		}
		fpsSelectorPanel.SetSelectorText(GetFPSText());
		SceneSingleton<OptionsMenuUI>.Instance.UpdateApplyChanges();
	}

	public void ToggleVsync()
	{
		vsync = !vsync;
		LoadFPSVsyncOptions();
		SceneSingleton<OptionsMenuUI>.Instance.UpdateApplyChanges();
	}

	private void LoadFPSVsyncOptions()
	{
		fpsModesIndex = FPSLimiter.GetFPSIndex();
		fpsSelectorPanel.SetSelectorText(GetFPSText());
		if (vsync)
		{
			VsyncToggle.GetComponent<Image>().sprite = TickSprite;
			fpsSelectorCG.alpha = 0.5f;
			fpsSelectorCG.interactable = false;
		}
		else
		{
			fpsSelectorCG.alpha = 1f;
			fpsSelectorCG.interactable = true;
			VsyncToggle.GetComponent<Image>().sprite = unTickSprite;
		}
	}

	public string GetResulationText()
	{
		return Singleton<ScreenSettingDataStore>.Instance.GetResolution(resolutionsIndex).width + " X " + Singleton<ScreenSettingDataStore>.Instance.GetResolution(resolutionsIndex).height;
	}

	public string GetScreenText()
	{
		return Singleton<ScreenSettingDataStore>.Instance.GetScreenMode(screenModesIndex).Text;
	}

	public string GetFPSText()
	{
		int num = FPSLimiter.fpsOptions[fpsModesIndex];
		if (num == -1)
		{
			return "FPS_Unlimited";
		}
		return num.ToString();
	}
}
