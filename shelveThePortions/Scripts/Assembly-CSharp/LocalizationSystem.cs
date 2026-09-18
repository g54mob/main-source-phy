using System;
using System.Collections.Generic;
using RTLTMPro;
using Steamworks;
using TMPro;
using UnityEngine;

public class LocalizationSystem : Singleton<LocalizationSystem>
{
	public bool DisableLocalizationSystem;

	private Languages currentLanguage = Languages.English;

	private Dictionary<string, string> languageIDStrings;

	[SerializeField]
	private CSVReader cSVReader;

	private FastStringBuilder finalText = new FastStringBuilder(2048);

	public event Action OnLanguageChange;

	public event Action OnLanguageChangeUIUpdate;

	private void OnEnable()
	{
		if (DisableLocalizationSystem)
		{
			SaveSystem.SaveLanguageSetting(1);
		}
		LoadLanguage();
	}

	private void LoadLanguage()
	{
		LoadSavedLanguage();
		LoadDictionary();
	}

	private void LoadDictionary()
	{
		languageIDStrings = new Dictionary<string, string>();
		languageIDStrings = cSVReader.LoadData((int)currentLanguage);
	}

	private Dictionary<string, string> GetLanguageDictionary(Languages language)
	{
		return cSVReader.LoadData((int)language);
	}

	private void LoadSavedLanguage()
	{
		if (DisableLocalizationSystem)
		{
			currentLanguage = Languages.English;
		}
		else
		{
			currentLanguage = (Languages)SaveSystem.GetLanguageSetting(getDefaultDesktopLanguage());
		}
	}

	private void SaveLanguage(Languages newLanguage)
	{
		SaveSystem.SaveLanguageSetting((int)newLanguage);
	}

	private int getDefaultDesktopLanguage()
	{
		if (DisableLocalizationSystem)
		{
			return 1;
		}
		if (!SteamManager.Initialized)
		{
			return 1;
		}
		return SteamApps.GetCurrentGameLanguage() switch
		{
			"english" => 1, 
			"schinese" => 3, 
			"tchinese" => 4, 
			"russian" => 2, 
			"german" => 7, 
			"spanish" => 10, 
			"french" => 11, 
			"polish" => 12, 
			"japanese" => 5, 
			"koreana" => 6, 
			"brazilian" => 8, 
			"arabic" => 9, 
			_ => 1, 
		};
	}

	public void UpdateText(TextMeshProUGUI textMeshPro, string IDString, Languages language, bool changeTextAlignment = false)
	{
		SetText(textMeshPro, GetIDString(IDString, language), language, changeTextAlignment);
	}

	public void UpdateText(TextMeshPro textMeshPro, string IDString, Languages language, bool changeTextAlignment = false)
	{
		SetText(textMeshPro, GetIDString(IDString, language), language, changeTextAlignment);
	}

	public void UpdateFormatText(TextMeshProUGUI textMeshPro, string IDString, string FormatIDString1, bool LocalizeFormatString1, string FormatIDString2, bool LocalizeFormatString2, bool changeTextAlignment = false)
	{
		string arg = FormatIDString1;
		string arg2 = FormatIDString2;
		if (LocalizeFormatString1)
		{
			arg = GetIDString(FormatIDString1);
		}
		if (LocalizeFormatString2)
		{
			arg2 = GetIDString(FormatIDString2);
		}
		string text = string.Format(GetIDString(IDString), arg, arg2);
		SetText(textMeshPro, text, currentLanguage, changeTextAlignment);
	}

	public void UpdateFormatText(TextMeshPro textMeshPro, string IDString, string FormatIDString1, bool LocalizeFormatString1, string FormatIDString2, bool LocalizeFormatString2, bool changeTextAlignment = false)
	{
		string arg = FormatIDString1;
		string arg2 = FormatIDString2;
		if (LocalizeFormatString1)
		{
			arg = GetIDString(FormatIDString1);
		}
		if (LocalizeFormatString2)
		{
			arg2 = GetIDString(FormatIDString2);
		}
		string text = string.Format(GetIDString(IDString), arg, arg2);
		SetText(textMeshPro, text, currentLanguage, changeTextAlignment);
	}

	public void UpdateFormatText(TextMeshPro textMeshPro, string IDString, string FormatIDString, bool LocalizeFormatString, bool changeTextAlignment = false)
	{
		string text = ((!LocalizeFormatString) ? string.Format(GetIDString(IDString), FormatIDString) : string.Format(GetIDString(IDString), GetIDString(FormatIDString)));
		SetText(textMeshPro, text, currentLanguage, changeTextAlignment);
	}

	public void UpdateFormatText(TextMeshProUGUI textMeshPro, string IDString, string FormatIDString, bool LocalizeFormatString, bool changeTextAlignment = false)
	{
		string text = ((!LocalizeFormatString) ? string.Format(GetIDString(IDString), FormatIDString) : string.Format(GetIDString(IDString), GetIDString(FormatIDString)));
		SetText(textMeshPro, text, currentLanguage, changeTextAlignment);
	}

	public void UpdateText(TextMeshPro textMeshPro, string IDString, bool changeTextAlignment = false)
	{
		SetText(textMeshPro, GetIDString(IDString), currentLanguage, changeTextAlignment);
	}

	public void UpdateText(TextMeshProUGUI textMeshPro, string IDString, bool changeTextAlignment = false)
	{
		SetText(textMeshPro, GetIDString(IDString), currentLanguage, changeTextAlignment);
	}

	private void SetText(TextMeshProUGUI textMeshPro, string text, Languages language, bool changeTextAlignment = false)
	{
		if (!(textMeshPro == null))
		{
			textMeshPro.isRightToLeftText = language == Languages.Arabic;
			if (changeTextAlignment)
			{
				textMeshPro.alignment = (textMeshPro.isRightToLeftText ? TextAlignmentOptions.MidlineRight : TextAlignmentOptions.MidlineLeft);
			}
			if (textMeshPro.isRightToLeftText)
			{
				textMeshPro.text = GetArabicTest(text);
			}
			else
			{
				textMeshPro.text = text;
			}
			textMeshPro.font = Singleton<LocalizationFontStore>.Instance?.GetTMP_FontAsset(language);
		}
	}

	private void SetText(TextMeshPro textMeshPro, string text, Languages language, bool changeTextAlignment = false)
	{
		if (!(textMeshPro == null))
		{
			textMeshPro.isRightToLeftText = language == Languages.Arabic;
			textMeshPro.alignment = (textMeshPro.isRightToLeftText ? TextAlignmentOptions.MidlineRight : TextAlignmentOptions.MidlineLeft);
			if (textMeshPro.isRightToLeftText)
			{
				textMeshPro.text = GetArabicTest(text);
			}
			else
			{
				textMeshPro.text = text;
			}
			textMeshPro.font = Singleton<LocalizationFontStore>.Instance?.GetTMP_FontAsset(language);
		}
	}

	public string GetArabicTest(string input)
	{
		if (string.IsNullOrEmpty(input))
		{
			return input;
		}
		finalText.Clear();
		RTLSupport.FixRTL(input, finalText, farsi: false, fixTextTags: true, preserveNumbers: true);
		finalText.Reverse();
		return finalText.ToString();
	}

	public void ChangeLanguage(Languages newLanguage)
	{
		if (newLanguage != currentLanguage)
		{
			SaveLanguage(newLanguage);
			LoadLanguage();
			if (this.OnLanguageChange != null)
			{
				this.OnLanguageChange();
			}
			if (this.OnLanguageChangeUIUpdate != null)
			{
				this.OnLanguageChangeUIUpdate();
			}
		}
	}

	public string GetIDString(string ID)
	{
		ID = ID.Trim();
		ID = ID.ToLower();
		if (languageIDStrings.ContainsKey(ID))
		{
			return languageIDStrings[ID];
		}
		return ID;
	}

	public string GetIDString(string ID, Languages language)
	{
		ID = ID.Trim();
		ID = ID.ToLower();
		Dictionary<string, string> languageDictionary = GetLanguageDictionary(language);
		if (languageDictionary.ContainsKey(ID))
		{
			return languageDictionary[ID];
		}
		return ID;
	}

	public bool HasIDString(string ID)
	{
		ID = ID.Trim();
		ID = ID.ToLower();
		return languageIDStrings.ContainsKey(ID);
	}

	public bool CheckLanguage(Languages checkedLanguage)
	{
		return checkedLanguage == currentLanguage;
	}

	public Languages GetCurrentLanguage()
	{
		return currentLanguage;
	}

	public Dictionary<string, string> GetDictionary()
	{
		return languageIDStrings;
	}
}
