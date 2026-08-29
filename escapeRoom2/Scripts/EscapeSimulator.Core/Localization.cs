using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public static class Localization
{
	public class LocalizedLanguage
	{
		public string name;

		public Language systemLanguage;
	}

	public class OriginalTextPart
	{
		public string text;

		public bool translate;

		public OriginalTextPart(string text, bool translate)
		{
			this.text = text;
			this.translate = translate;
		}
	}

	private const int DEFAULT_LANGUAGE = 0;

	private static Dictionary<string, string> dictionary;

	private static Dictionary<string, string> backupDictionary;

	public static readonly List<LocalizedLanguage> allLanguages = new List<LocalizedLanguage>
	{
		new LocalizedLanguage
		{
			name = "English",
			systemLanguage = Language.English
		},
		new LocalizedLanguage
		{
			name = "简体中文 (Simplified Chinese)",
			systemLanguage = Language.ChineseSimplified
		},
		new LocalizedLanguage
		{
			name = "繁體中文 (Traditional Chinese)",
			systemLanguage = Language.ChineseTraditional
		},
		new LocalizedLanguage
		{
			name = "日本語 (Japanese)",
			systemLanguage = Language.Japanese
		},
		new LocalizedLanguage
		{
			name = "한국어 (Korean)",
			systemLanguage = Language.Korean
		},
		new LocalizedLanguage
		{
			name = "Deutsch (German)",
			systemLanguage = Language.German
		},
		new LocalizedLanguage
		{
			name = "Francais (French)",
			systemLanguage = Language.French
		},
		new LocalizedLanguage
		{
			name = "Türkçe (Turkish)",
			systemLanguage = Language.Turkish
		},
		new LocalizedLanguage
		{
			name = "Español (Spanish)",
			systemLanguage = Language.Spanish
		},
		new LocalizedLanguage
		{
			name = "Español (Spanish Latin America)",
			systemLanguage = Language.SpanishLatinAmerica
		},
		new LocalizedLanguage
		{
			name = "Português (Portuguese)",
			systemLanguage = Language.Portuguese
		},
		new LocalizedLanguage
		{
			name = "Português (Portuguese Brasil)",
			systemLanguage = Language.PortugueseBrasil
		},
		new LocalizedLanguage
		{
			name = "Italiano (Italian)",
			systemLanguage = Language.Italian
		},
		new LocalizedLanguage
		{
			name = "Polski (Polish)",
			systemLanguage = Language.Polish
		},
		new LocalizedLanguage
		{
			name = "Русский (Russian)",
			systemLanguage = Language.Russian
		}
	};

	private static StringBuilder sharedTextBuilder = new StringBuilder(1024);

	private static StringBuilder sharedLookupBuilder = new StringBuilder(64);

	public static void init(int languageIndex)
	{
		if (languageIndex >= allLanguages.Count)
		{
			languageIndex = 0;
		}
		dictionary = loadLanguage(allLanguages[languageIndex].systemLanguage);
		if (dictionary == null)
		{
			dictionary = new Dictionary<string, string>();
		}
		backupDictionary = ((languageIndex == 0) ? dictionary : loadLanguage(allLanguages[0].systemLanguage));
		SteamLocalization.init();
	}

	private static void validateLanguage(Language toValidate, Language language = Language.English)
	{
		Dictionary<string, string> obj = loadLanguage(language);
		Dictionary<string, string> dictionary = loadLanguage(toValidate);
		string text = "";
		string text2 = " c3hellowlrd c3bye c3txtcolor c3for c3nexti Corporation3_Bullfly Corporation3_Hypnosis Corporation3_Infogames Corporation3_14Century Corporation3_Brainscape Corporation3_MortalArts Corporation3_Similaris h3_del h3_ok ";
		foreach (KeyValuePair<string, string> item in obj)
		{
			bool flag = item.Key.Contains("$");
			bool flag2 = dictionary.ContainsKey(item.Key);
			bool flag3 = false;
			if (item.Key.EndsWith("NoteDate"))
			{
				flag3 = true;
			}
			if (item.Value.StartsWith("%"))
			{
				flag3 = true;
			}
			if (text2.Contains(" " + item.Key + " "))
			{
				flag3 = true;
			}
			if (!flag3 && ((!flag2 && !flag) || (flag2 && flag)))
			{
				text = text + item.Key + "," + item.Value + "\r\n";
			}
		}
		if (text != "")
		{
			Debug.LogError(toValidate.ToString() + ".yaml doesn't contains keys: " + text);
		}
	}

	public static Dictionary<string, string> loadLanguage(Language language)
	{
		TextAsset textAsset = Resources.Load<TextAsset>("Languages/" + language);
		if (textAsset == null)
		{
			return null;
		}
		string text = textAsset.text;
		if (language == Language.English)
		{
			TextAsset textAsset2 = Resources.Load<TextAsset>("Languages/" + language.ToString() + "Editor");
			StringBuilder stringBuilder = new StringBuilder(text);
			stringBuilder.AppendLine();
			stringBuilder.Append(textAsset2.text);
			text = stringBuilder.ToString();
		}
		return parseYaml(text, language.ToString());
	}

	public static Dictionary<string, string> parseYaml(string text, string debugLocation = "")
	{
		Dictionary<string, string> dictionary = new Dictionary<string, string>();
		sharedTextBuilder.Length = 0;
		sharedLookupBuilder.Length = 0;
		bool flag = true;
		bool flag2 = false;
		bool flag3 = false;
		bool flag4 = false;
		int num = 1;
		foreach (char c in text)
		{
			if (flag)
			{
				if (c != ' ' && c != '\t' && c != '\r')
				{
					if (c == '#' && sharedLookupBuilder.Length == 0 && sharedTextBuilder.Length == 0)
					{
						flag4 = true;
						flag = false;
					}
					else
					{
						switch (c)
						{
						case '\n':
							if (sharedLookupBuilder.Length != 0)
							{
								Debug.LogError("New line not expected at line " + num + " in file " + debugLocation + ".yaml");
								return null;
							}
							break;
						case ':':
							flag = false;
							break;
						default:
							sharedLookupBuilder.Append(c);
							break;
						}
					}
				}
			}
			else if (flag2)
			{
				if (flag3)
				{
					if (c == 'n')
					{
						sharedTextBuilder.Append('\n');
					}
					else
					{
						sharedTextBuilder.Append(c);
					}
					flag3 = false;
				}
				else
				{
					switch (c)
					{
					case '\\':
						flag3 = true;
						break;
					case '"':
						dictionary[sharedLookupBuilder.ToString()] = sharedTextBuilder.ToString();
						sharedLookupBuilder.Length = 0;
						sharedTextBuilder.Length = 0;
						flag2 = false;
						if (flag3)
						{
							Debug.LogError("Didn't expect to end string with escaping activated! At line " + num + " in file " + debugLocation + ".yaml");
							return null;
						}
						break;
					case '\n':
						Debug.LogError("New line not expected at line " + num + " in file " + debugLocation + ".yaml");
						return null;
					default:
						sharedTextBuilder.Append(c);
						break;
					}
				}
			}
			else if (flag4)
			{
				if (c == '\n')
				{
					flag4 = false;
					flag = true;
				}
			}
			else if (c == '#' && sharedLookupBuilder.Length == 0 && sharedTextBuilder.Length == 0)
			{
				flag4 = true;
			}
			else
			{
				switch (c)
				{
				case '"':
					flag2 = true;
					break;
				case '\n':
					flag = true;
					break;
				}
			}
			if (c == '\n')
			{
				num++;
			}
		}
		return dictionary;
	}

	public static void translateCurrentScene()
	{
		for (int i = 0; i < SceneManager.sceneCount; i++)
		{
			GameObject[] rootGameObjects = SceneManager.GetSceneAt(i).GetRootGameObjects();
			for (int j = 0; j < rootGameObjects.Length; j++)
			{
				translateObject(rootGameObjects[j].transform);
			}
		}
	}

	public static void translateObject(Transform transform, bool forceNewText = false)
	{
		if (transform.GetComponent<DontTranslate>() != null)
		{
			return;
		}
		if (transform.TryGetComponent<UITooltip>(out var component) && !transform.TryGetComponent<Text>(out var _))
		{
			TranslatedText translatedText = transform.GetComponent<TranslatedText>();
			if (translatedText != null && !forceNewText)
			{
				List<OriginalTextPart> list = parseOriginalText(translatedText.originalText);
				string text = "";
				foreach (OriginalTextPart item in list)
				{
					text = ((!item.translate) ? (text + item.text) : (text + translate(translatedText.originalText)));
					text += " ";
				}
				if (text.Length > 0)
				{
					text = text.Substring(0, text.Length - 1);
				}
				component.text = text;
			}
			else
			{
				if (translatedText == null)
				{
					translatedText = transform.gameObject.AddComponent<TranslatedText>();
				}
				translatedText.originalText = component.text;
				component.text = translate(component.text);
			}
		}
		if (transform.TryGetComponent<Text>(out var component3))
		{
			TranslatedText translatedText2 = transform.GetComponent<TranslatedText>();
			if (translatedText2 != null && !forceNewText)
			{
				component3.text = translate(translatedText2.originalText);
			}
			else
			{
				if (translatedText2 == null)
				{
					translatedText2 = transform.gameObject.AddComponent<TranslatedText>();
				}
				translatedText2.originalText = component3.text;
				component3.text = translate(component3.text);
			}
		}
		if (transform.TryGetComponent<Dropdown>(out var component4))
		{
			TranslatedDropdown translatedDropdown = transform.GetComponent<TranslatedDropdown>();
			if (translatedDropdown != null && !forceNewText && component4.options.Count == translatedDropdown.originalTexts.Length)
			{
				for (int i = 0; i < component4.options.Count; i++)
				{
					component4.options[i].text = translate(translatedDropdown.originalTexts[i]);
				}
			}
			else
			{
				if (translatedDropdown == null)
				{
					translatedDropdown = transform.gameObject.AddComponent<TranslatedDropdown>();
				}
				translatedDropdown.originalTexts = new string[component4.options.Count];
				for (int j = 0; j < component4.options.Count; j++)
				{
					translatedDropdown.originalTexts[j] = component4.options[j].text;
					component4.options[j].text = translate(translatedDropdown.originalTexts[j]);
				}
			}
			component4.GetComponentInChildren<Text>().text = translate(translatedDropdown.originalTexts[component4.value]);
		}
		for (int k = 0; k < transform.childCount; k++)
		{
			translateObject(transform.GetChild(k));
		}
	}

	public static string lookupInDictionary(string line, string defaultValue = "%%")
	{
		if (!dictionary.TryGetValue(line, out var value) && !backupDictionary.TryGetValue(line, out value))
		{
			if (defaultValue == "%%")
			{
				return "%" + line + "%";
			}
			return defaultValue;
		}
		return value;
	}

	public static string lookupInEnglishDictionary(string line, string defaultValue = "%%")
	{
		if (!backupDictionary.TryGetValue(line, out var value))
		{
			if (defaultValue == "%%")
			{
				return "%" + line + "%";
			}
			return defaultValue;
		}
		return value;
	}

	public static string translate(string toTranslate)
	{
		sharedTextBuilder.Length = 0;
		sharedLookupBuilder.Length = 0;
		bool flag = false;
		bool flag2 = false;
		bool flag3 = false;
		bool flag4 = false;
		foreach (char c in toTranslate)
		{
			if (c == '!' && flag)
			{
				flag2 = true;
				continue;
			}
			switch (c)
			{
			case '$':
				flag3 = true;
				continue;
			case '#':
				if (sharedLookupBuilder.Length == 0 && sharedTextBuilder.Length == 0)
				{
					flag4 = true;
					continue;
				}
				break;
			}
			if (c == '%')
			{
				if (flag)
				{
					string text = lookupInDictionary(sharedLookupBuilder.ToString());
					if (flag2)
					{
						sharedTextBuilder.Append(text.Split('\n')[0]);
					}
					else if (flag3)
					{
						string[] array = text.Split('\n');
						if (array.Length > 1)
						{
							sharedTextBuilder.Append(array[1].Replace("!", " ").Replace("+", ""));
						}
						if (array.Length > 2)
						{
							sharedTextBuilder.Append(array[2]);
						}
					}
					else if (flag4)
					{
						sharedTextBuilder.Append(text.Replace("!", ""));
					}
					else
					{
						sharedTextBuilder.Append(text);
					}
					sharedLookupBuilder.Length = 0;
					flag2 = false;
					flag3 = false;
				}
				flag = !flag;
			}
			else if (flag)
			{
				sharedLookupBuilder.Append(c);
			}
			else
			{
				sharedTextBuilder.Append(c);
			}
		}
		return sharedTextBuilder.ToString();
	}

	public static List<OriginalTextPart> parseOriginalText(string originalText)
	{
		List<OriginalTextPart> list = new List<OriginalTextPart>();
		bool flag = false;
		StringBuilder stringBuilder = new StringBuilder();
		foreach (char c in originalText)
		{
			switch (c)
			{
			case '%':
				if (flag)
				{
					list.Add(new OriginalTextPart(stringBuilder.ToString(), translate: true));
					stringBuilder.Clear();
					flag = false;
				}
				else
				{
					flag = true;
				}
				break;
			case ' ':
				list.Add(new OriginalTextPart(stringBuilder.ToString(), translate: false));
				stringBuilder.Clear();
				flag = false;
				break;
			default:
				stringBuilder.Append(c);
				break;
			}
		}
		return list;
	}
}
