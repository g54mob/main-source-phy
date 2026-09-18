using System.Collections.Generic;
using UnityEngine;

public class UILocalizationManager : SceneSingleton<UILocalizationManager>
{
	[Space]
	[SerializeField]
	private List<LanguageSelectionToggle> languageSelectionTogglesList;

	[SerializeField]
	private List<Languages> languagesList;

	private bool isLoaded;

	private void LoadData()
	{
		isLoaded = true;
		for (int i = 0; i < languagesList.Count; i++)
		{
			languageSelectionTogglesList[i].SetText(languagesList[i]);
			languageSelectionTogglesList[i].gameObject.SetActive(value: true);
		}
	}

	public void loadSettings()
	{
		if (!isLoaded)
		{
			LoadData();
		}
		foreach (LanguageSelectionToggle languageSelectionToggles in languageSelectionTogglesList)
		{
			languageSelectionToggles.SetToggle(languageSelectionToggles.toggleLanguage == Singleton<LocalizationSystem>.Instance.GetCurrentLanguage());
			languageSelectionToggles.SetSelectionInteractable(!Singleton<LocalizationSystem>.Instance.DisableLocalizationSystem);
		}
	}

	public void ApplyLanguage(Languages language)
	{
		if (language != (Languages)SaveSystem.GetLanguageSetting())
		{
			Singleton<LocalizationSystem>.Instance.ChangeLanguage(language);
		}
		SaveSystem.SaveLanguageSetting((int)language);
		loadSettings();
	}
}
