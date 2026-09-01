using System;
using System.IO;
using I2.Loc;
using UnityEngine;

public class Localize
{
	public static readonly string[] m_BuiltInLaguageCodes = new string[12]
	{
		"en", "fr", "es", "de", "ja", "ko", "zh-CN", "zh-TW", "it", "tr",
		"ru", "pt"
	};

	public static void SwitchToLanguage(string languageCode)
	{
		try
		{
			if (LocalizationManager.GetAllLanguagesCode().Contains(languageCode))
			{
				LocalizationManager.CurrentLanguageCode = languageCode;
				return;
			}
			string pathToMod = Mods.GetPathToMod(languageCode);
			string languageCSVFileName = ModApi.GetLanguageCSVFileName(languageCode);
			if (!string.IsNullOrEmpty(pathToMod) && !string.IsNullOrEmpty(languageCSVFileName))
			{
				ModApi.LoadLanguageCSV(Path.Combine(pathToMod, languageCSVFileName), languageCode);
				LocalizationManager.CurrentLanguageCode = languageCode;
			}
		}
		catch (Exception ex)
		{
			Debug.Log("HANDLED: " + ex.Message);
		}
	}

	public static Language GetSystemLanguage()
	{
		return Application.systemLanguage switch
		{
			SystemLanguage.French => Language.FRENCH, 
			SystemLanguage.Spanish => Language.SPANISH, 
			SystemLanguage.German => Language.GERMAN, 
			SystemLanguage.Japanese => Language.JAPANESE, 
			SystemLanguage.Korean => Language.KOREAN, 
			SystemLanguage.ChineseSimplified => Language.CHINESE_SIMPLIFIED, 
			SystemLanguage.ChineseTraditional => Language.CHINESE_TRADITIONAL, 
			SystemLanguage.Italian => Language.ITALIAN, 
			SystemLanguage.Turkish => Language.TURKISH, 
			SystemLanguage.Russian => Language.RUSSIAN, 
			SystemLanguage.Portuguese => Language.PORTUGUESE, 
			_ => Language.ENGLISH, 
		};
	}

	public static string GetSystemLanguageCode()
	{
		return Application.systemLanguage switch
		{
			SystemLanguage.French => m_BuiltInLaguageCodes[1], 
			SystemLanguage.Spanish => m_BuiltInLaguageCodes[2], 
			SystemLanguage.German => m_BuiltInLaguageCodes[3], 
			SystemLanguage.Japanese => m_BuiltInLaguageCodes[4], 
			SystemLanguage.Korean => m_BuiltInLaguageCodes[5], 
			SystemLanguage.ChineseSimplified => m_BuiltInLaguageCodes[6], 
			SystemLanguage.ChineseTraditional => m_BuiltInLaguageCodes[7], 
			SystemLanguage.Italian => m_BuiltInLaguageCodes[8], 
			SystemLanguage.Turkish => m_BuiltInLaguageCodes[9], 
			SystemLanguage.Russian => m_BuiltInLaguageCodes[10], 
			SystemLanguage.Portuguese => m_BuiltInLaguageCodes[11], 
			_ => m_BuiltInLaguageCodes[0], 
		};
	}

	public static string GetLanguageName(string languageCode)
	{
		LanguageData languageDataFromCode = LocalizationManager.GetLanguageDataFromCode(languageCode);
		if (languageDataFromCode == null)
		{
			return string.Empty;
		}
		return languageDataFromCode.Name;
	}

	public static string GetLanguageNameLocalized(string languageCode)
	{
		return languageCode switch
		{
			"en" => Get("LANG_ENGLISH"), 
			"fr" => Get("LANG_FRENCH"), 
			"es" => Get("LANG_SPANISH"), 
			"de" => Get("LANG_GERMAN"), 
			"ja" => Get("LANG_JAPANESE"), 
			"ko" => Get("LANG_KOREAN"), 
			"zh-CN" => Get("LANG_CHINESE_SIMPLIFIED"), 
			"zh-TW" => Get("LANG_CHINESE_TRADITIONAL"), 
			"it" => Get("LANG_ITALIAN"), 
			"tr" => Get("LANG_TURKISH"), 
			"ru" => Get("LANG_RUSSIAN"), 
			"pt" => Get("LANG_PORTUGUESE"), 
			_ => GetLanguageName(languageCode), 
		};
	}

	public static string GetLanguageNameLocalized(Language language)
	{
		return language switch
		{
			Language.ENGLISH => Get("LANG_ENGLISH"), 
			Language.FRENCH => Get("LANG_FRENCH"), 
			Language.SPANISH => Get("LANG_SPANISH"), 
			Language.GERMAN => Get("LANG_GERMAN"), 
			Language.JAPANESE => Get("LANG_JAPANESE"), 
			Language.KOREAN => Get("LANG_KOREAN"), 
			Language.CHINESE_SIMPLIFIED => Get("LANG_CHINESE_SIMPLIFIED"), 
			Language.CHINESE_TRADITIONAL => Get("LANG_CHINESE_TRADITIONAL"), 
			Language.ITALIAN => Get("LANG_ITALIAN"), 
			Language.TURKISH => Get("LANG_TURKISH"), 
			Language.RUSSIAN => Get("LANG_RUSSIAN"), 
			Language.PORTUGUESE => Get("LANG_PORTUGUESE"), 
			_ => "#ERROR", 
		};
	}

	public static string Get(string key)
	{
		return LocalizationManager.GetTranslation(key);
	}

	public static string Get(string key, string parameter)
	{
		return LocalizationManager.GetTranslation(key).Replace("{0}", parameter);
	}

	public static string Get(string key, string parameter1, string parameter2)
	{
		return LocalizationManager.GetTranslation(key).Replace("{0}", parameter1).Replace("{1}", parameter2);
	}

	public static bool IsBuiltInLanguage(Language language)
	{
		return (int)language < Enum.GetValues(typeof(Language)).Length;
	}

	public static bool IsBuiltInLanguageCode(string languageCode)
	{
		string[] builtInLaguageCodes = m_BuiltInLaguageCodes;
		for (int i = 0; i < builtInLaguageCodes.Length; i++)
		{
			if (builtInLaguageCodes[i] == languageCode)
			{
				return true;
			}
		}
		return false;
	}
}
