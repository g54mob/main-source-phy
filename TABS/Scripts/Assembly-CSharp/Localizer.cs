using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading;
using TMPro;
using UnityEngine;

public static class Localizer
{
	public delegate void UpdateText();

	public enum Language
	{
		LANG_EN_US = 0,
		LANG_FR = 1,
		LANG_IT = 2,
		LANG_DE = 3,
		LANG_ES = 4,
		LANG_PT_BR = 5,
		LANG_RU = 6,
		LANG_CH = 7,
		LANG_JA = 8
	}

	private static string PATH = "Localization/tabs_";

	private static string CULTURE_CODE_SIMPLIFIED_CHINESE = "zh-Hans";

	private static string CULTURE_CODE_US_ENGLISH = "en-us";

	private static Dictionary<MonoBehaviour, UpdateText> m_callbacks = new Dictionary<MonoBehaviour, UpdateText>();

	private static Dictionary<Language, Dictionary<string, string>> m_localization = new Dictionary<Language, Dictionary<string, string>>();

	private static Dictionary<string, string> m_currentLanguage;

	private static Language m_locale = Language.LANG_EN_US;

	private static LocalizationSettings m_settings;

	public static CultureInfo ActiveCultureInfo { get; private set; }

	public static void Initialize(Language lang)
	{
		m_settings = Resources.Load<LocalizationSettings>("Localization/LocalizationSettings");
		for (int i = 0; i <= 8; i++)
		{
			LoadLanguage((Language)i);
		}
		SetLanguage(lang);
	}

	public static void InitializeWithDefaultSystemLanguage()
	{
		Initialize(GetSystemLanguage());
	}

	public static void LoadLanguage(Language lang)
	{
		if (!m_localization.ContainsKey(lang))
		{
			m_localization.Add(lang, GetLanguage(lang));
		}
	}

	public static Dictionary<string, string> GetLanguage(Language lang)
	{
		Dictionary<string, string> dictionary = new Dictionary<string, string>();
		string pATH = PATH;
		pATH += lang.ToString().Remove(0, 5).ToLowerInvariant();
		string[] array = Resources.Load<TextAsset>(pATH).text.Split('\n');
		for (int i = 0; i < array.Length; i++)
		{
			string[] array2 = array[i].Split(new char[1] { '=' }, 2);
			if (array2.Length == 2)
			{
				array2[0] = array2[0].Trim('\r', '\n');
				array2[1] = array2[1].Trim('\r', '\n');
				dictionary.Add(array2[0], array2[1]);
			}
			else
			{
				string text = ((array2.Length != 0) ? array2[0] : "");
				Debug.LogWarning("Something wrong in localization: " + pATH + " Error: " + text);
			}
		}
		return dictionary;
	}

	public static void SetLanguage(Language lang)
	{
		if (!m_localization.ContainsKey(lang))
		{
			return;
		}
		m_locale = lang;
		m_currentLanguage = m_localization[lang];
		foreach (KeyValuePair<MonoBehaviour, UpdateText> callback in m_callbacks)
		{
			callback.Value();
		}
		try
		{
			if (m_locale == Language.LANG_CH)
			{
				ActiveCultureInfo = new CultureInfo(CULTURE_CODE_SIMPLIFIED_CHINESE);
			}
			ActiveCultureInfo = new CultureInfo(m_locale.ToString().Remove(0, 5).ToLowerInvariant()
				.Replace("_", "-"));
		}
		catch (CultureNotFoundException)
		{
			ActiveCultureInfo = new CultureInfo(CULTURE_CODE_US_ENGLISH);
		}
		Thread.CurrentThread.CurrentCulture = ActiveCultureInfo;
		Thread.CurrentThread.CurrentUICulture = ActiveCultureInfo;
	}

	public static void RegisterCallback(MonoBehaviour owner, UpdateText callback)
	{
		if (!m_callbacks.ContainsKey(owner))
		{
			m_callbacks.Add(owner, callback);
		}
	}

	public static void UnregisterCallback(MonoBehaviour owner)
	{
		if (m_callbacks.ContainsKey(owner))
		{
			m_callbacks.Remove(owner);
		}
	}

	private static string CorrectReadDirection(string phrase)
	{
		return phrase;
	}

	private static string ReversePhrase(string phrase)
	{
		char[] array = phrase.ToCharArray();
		StringBuilder stringBuilder = new StringBuilder();
		for (int i = 0; i < array.Length; i++)
		{
			char c = array[i];
			if (c == '<')
			{
				while (array[i++] != '>')
				{
				}
				if (i >= array.Length)
				{
					break;
				}
				c = array[i];
			}
			StringBuilder stringBuilder2 = new StringBuilder();
			if (c == '{')
			{
				while (true)
				{
					c = array[i];
					stringBuilder2.Append(c);
					if (c == '}')
					{
						break;
					}
					i++;
				}
			}
			string value = stringBuilder2.ToString();
			if (!string.IsNullOrEmpty(value))
			{
				stringBuilder.Insert(0, value);
			}
			else
			{
				stringBuilder.Insert(0, c);
			}
		}
		return stringBuilder.ToString();
	}

	public static string GetSinglePhrase(string phraseID, params string[] args)
	{
		return GetSinglePhrase(m_currentLanguage, m_locale, phraseID, args);
	}

	public static string GetSinglePhrase(Language language, string phraseID, params string[] args)
	{
		m_localization.TryGetValue(language, out var value);
		return GetSinglePhrase(value, m_locale, phraseID, args);
	}

	public static Language GetSystemLanguage()
	{
		Language language = Language.LANG_EN_US;
		switch ("en")
		{
		case "en":
			return Language.LANG_EN_US;
		case "fr":
			return Language.LANG_FR;
		case "it":
			return Language.LANG_IT;
		case "de":
			return Language.LANG_DE;
		case "es":
			return Language.LANG_ES;
		case "pt":
			return Language.LANG_PT_BR;
		case "ru":
			return Language.LANG_RU;
		case "zh":
			return Language.LANG_CH;
		case "ja":
			return Language.LANG_JA;
		default:
			return Language.LANG_EN_US;
		}
	}

	private static string GetSinglePhrase(Dictionary<string, string> lang, Language locale, string phraseID, params string[] args)
	{
		if (lang != null && lang.ContainsKey(phraseID))
		{
			string text = lang[phraseID];
			if (string.IsNullOrWhiteSpace(text) && m_locale != Language.LANG_EN_US && locale == m_locale)
			{
				return GetSinglePhrase(m_localization[Language.LANG_EN_US], Language.LANG_EN_US, phraseID, args);
			}
			text = CorrectReadDirection(text);
			if (args != null && args.Length != 0)
			{
				string[] array = new string[args.Length];
				args.CopyTo(array, 0);
				for (int i = 0; i < array.Length; i++)
				{
					if (array[i].Length > 1 && array[i][0] == '$')
					{
						array[i] = GetSinglePhrase(array[i].Substring(1));
					}
				}
				string format = text;
				object[] args2 = array;
				text = string.Format(format, args2);
			}
			int num = 0;
			bool flag = false;
			for (int j = 0; j < text.Length; j++)
			{
				if (!flag && text[j] == '$')
				{
					num = j;
					flag = true;
				}
				else if (flag && text[j] == '$')
				{
					string text2 = text.Substring(num, j - num + 1);
					string singlePhrase = GetSinglePhrase(text2.Substring(1, text2.Length - 2));
					text = text.Replace(text2, singlePhrase);
					flag = false;
				}
			}
			return text;
		}
		return phraseID;
	}

	public static TMP_FontAsset GetCurrentFont(int fontIndex = 0)
	{
		if (m_settings == null)
		{
			return null;
		}
		return m_settings.GetFont(m_locale, fontIndex);
	}
}
