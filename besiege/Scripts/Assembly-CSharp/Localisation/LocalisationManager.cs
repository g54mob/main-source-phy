using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using InternalModding.LevelEntities;
using ModIO;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Localisation
{
	public class LocalisationManager : SingleInstance<LocalisationManager>
	{
		public class LanguageInfo
		{
			public string LanguageFile;

			public string LanguageName;

			public bool isAsian;

			public string LanguageCode;
		}

		public const string BuiltinLocalisation = "Localisations/localisation_text";

		public static readonly List<LanguageInfo> BuiltinLocalisations = new List<LanguageInfo>
		{
			new LanguageInfo
			{
				LanguageFile = "English",
				LanguageName = "English",
				LanguageCode = "en"
			},
			new LanguageInfo
			{
				LanguageFile = "French",
				LanguageName = "Français",
				LanguageCode = "fr"
			},
			new LanguageInfo
			{
				LanguageFile = "German",
				LanguageName = "Deutsch",
				LanguageCode = "de"
			},
			new LanguageInfo
			{
				LanguageFile = "Spanish",
				LanguageName = "Español",
				LanguageCode = "es"
			},
			new LanguageInfo
			{
				LanguageFile = "Portuguese",
				LanguageName = "Português",
				LanguageCode = "pt"
			},
			new LanguageInfo
			{
				LanguageFile = "Russian",
				LanguageName = "русский",
				LanguageCode = "ru"
			},
			new LanguageInfo
			{
				LanguageFile = "Italian",
				LanguageName = "Italiano",
				LanguageCode = "it"
			},
			new LanguageInfo
			{
				LanguageFile = "Polish",
				LanguageName = "Polski",
				LanguageCode = "pl"
			},
			new LanguageInfo
			{
				LanguageFile = "Turkish",
				LanguageName = "Türkçe",
				LanguageCode = "tr"
			},
			new LanguageInfo
			{
				LanguageFile = "Japanese",
				LanguageName = "日本語",
				isAsian = true,
				LanguageCode = "ja"
			},
			new LanguageInfo
			{
				LanguageFile = "Korean",
				LanguageName = "한국어",
				isAsian = true,
				LanguageCode = "ko"
			},
			new LanguageInfo
			{
				LanguageFile = "ChineseSimplified",
				LanguageName = "简体中文",
				isAsian = true,
				LanguageCode = "zh-CN"
			},
			new LanguageInfo
			{
				LanguageFile = "ChineseTraditional",
				LanguageName = "台灣繁體中文",
				isAsian = true,
				LanguageCode = "zh-TW"
			},
			new LanguageInfo
			{
				LanguageFile = "ChineseHongKong",
				LanguageName = "香港繁體中文",
				isAsian = true,
				LanguageCode = "zh-HK"
			}
		};

		public static List<TranslationFile> ExternalLocalisations = new List<TranslationFile>();

		private bool isInitialized;

		public List<TranslationFile> availableLanguages = new List<TranslationFile>();

		private TranslationFile currentTranslationFile;

		private List<LocalisationChild> sceneLocalisationChildren = new List<LocalisationChild>();

		private List<ILocalisationAware> sceneLocalisationAwares = new List<ILocalisationAware>();

		public Font mediumCJKFont;

		public Font boldCJKFont;

		public TranslationFile CurrentTranslationFile
		{
			get
			{
				return currentTranslationFile;
			}
		}

		public bool IsAsianLanguage { get; private set; }

		public string currLangISO
		{
			get
			{
				return currentTranslationFile.SystemLanguage;
			}
		}

		public string currLangName
		{
			get
			{
				return currentTranslationFile.LanguageName;
			}
		}

		public override string Name
		{
			get
			{
				return "LocalisationManager";
			}
		}

		public static event Action LanguageChanged;

		public TranslationFile GetDefaultTranslationFile()
		{
			return availableLanguages.Where((TranslationFile x) => x.SystemLanguage == "English").FirstOrDefault();
		}

		public static TranslationFile DefaultTranslationFile()
		{
			if (Application.isPlaying)
			{
				return SingleInstance<LocalisationManager>.Instance.GetDefaultTranslationFile();
			}
			return GetTranslationFile(BuiltinLocalisations[0].LanguageFile);
		}

		public static bool UsingDefault()
		{
			return SingleInstance<LocalisationManager>.Instance.CurrentTranslationFile.SystemLanguage == "English";
		}

		public static bool GetTranslation(int id, out string translation)
		{
			return SingleInstance<LocalisationManager>.Instance.GetTranslationById(id, out translation);
		}

		public static string GetTranslation(int id)
		{
			if (SingleInstance<LocalisationManager>.Instance != null)
			{
				return SingleInstance<LocalisationManager>.Instance.GetTranslationById(id);
			}
			return "[[" + id + "]]";
		}

		private void CacheLocalisationChildren(Scene activeScene)
		{
			if (!activeScene.isLoaded)
			{
				return;
			}
			GameObject[] rootGameObjects = activeScene.GetRootGameObjects();
			GameObject[] array = rootGameObjects;
			foreach (GameObject gameObject in array)
			{
				List<LocalisationChild> collection = gameObject.GetComponentsInChildren<LocalisationChild>(true).ToList();
				sceneLocalisationChildren.AddRange(collection);
				List<ILocalisationAware> collection2 = (from a in gameObject.GetComponentsInChildren<MonoBehaviour>(true)
					where a != null && a.GetType().GetInterfaces().Any((Type k) => k == typeof(ILocalisationAware))
					select (ILocalisationAware)a).ToList();
				sceneLocalisationAwares.AddRange(collection2);
			}
		}

		public int GetSystemLanguageIndex()
		{
			int index;
			GetSystemLanguage(out index);
			return index;
		}

		public LanguageInfo GetSystemLanguage(out int index)
		{
			SystemLanguage systemLanguage = Application.systemLanguage;
			if (systemLanguage == SystemLanguage.Chinese)
			{
				systemLanguage = SystemLanguage.ChineseTraditional;
			}
			index = BuiltinLocalisations.FindIndex((LanguageInfo x) => x.LanguageFile.Equals(systemLanguage.ToString()));
			if (index == -1)
			{
				index = BuiltinLocalisations.FindIndex((LanguageInfo x) => x.LanguageFile.Equals("English"));
			}
			return BuiltinLocalisations[index];
		}

		public bool GetTranslationById(int id, out string translation)
		{
			if (id < 0)
			{
				translation = SingleInstanceFindOnly<EntityLoader>.Instance.GetEntityName(-id);
				return true;
			}
			if (currentTranslationFile == null)
			{
				translation = string.Format("[[{0}]]", id);
				return false;
			}
			if (currentTranslationFile.ContainsTranslation(id))
			{
				translation = currentTranslationFile.GetTranslationString(id).Replace("\\n", "\n");
				return true;
			}
			translation = string.Format("[[{0}]]", id);
			return false;
		}

		public string GetTranslationById(int id)
		{
			if (id < 0)
			{
				return SingleInstanceFindOnly<EntityLoader>.Instance.GetEntityName(-id);
			}
			if (currentTranslationFile == null || !currentTranslationFile.ContainsTranslation(id))
			{
				return string.Empty;
			}
			return currentTranslationFile.GetTranslationString(id).Replace("\\n", "\n");
		}

		public static string GetTranslation(TranslationFile language, int id)
		{
			if (language == null || !language.ContainsTranslation(id))
			{
				return string.Empty;
			}
			return language.GetTranslationString(id).Replace("\\n", "\n");
		}

		public static List<string> GetLocalisations(int[] localisations, bool toUpper = true)
		{
			List<string> list = new List<string>(localisations.Length);
			for (int i = 0; i < localisations.Length; i++)
			{
				string text = GetTranslation(localisations[i]);
				if (!string.IsNullOrEmpty(text))
				{
					if (toUpper)
					{
						text = text.ToUpper();
					}
					list.Add(text);
				}
			}
			return list;
		}

		public void SetTranslation(int id, string translation)
		{
			if (currentTranslationFile == null)
			{
				Debug.Log("Current translation file is null");
			}
			else if (id == 0 || !currentTranslationFile.ContainsTranslation(id))
			{
				Debug.Log("Invalid translation id or not found.");
			}
			else if (string.IsNullOrEmpty(translation))
			{
				Debug.Log("Translation is null or empty.");
			}
			else
			{
				currentTranslationFile.SetTranslationString(id, translation);
			}
		}

		public Dictionary<int, int> GenerateDuplicateReplaceMap()
		{
			Dictionary<int, int> dictionary = new Dictionary<int, int>();
			Dictionary<string, List<int>> dictionary2 = currentTranslationFile.FindDoubleTranslations();
			foreach (KeyValuePair<string, List<int>> item in dictionary2)
			{
				if (item.Value.Count != 1)
				{
					for (int i = 1; i < item.Value.Count; i++)
					{
						dictionary.Add(item.Value[i], item.Value[0]);
					}
				}
			}
			return dictionary;
		}

		public void InitializeManager()
		{
			if (!isInitialized)
			{
				LoadLocalisationsFromUser();
				DetermineSystemLanguage();
				LoadDefaultLanguage();
				isInitialized = true;
			}
		}

		private void Awake()
		{
			InitializeManager();
		}

		private void LoadDefaultLanguage()
		{
			string preferredLanguage = OptionsMaster.BesiegeConfig.Language;
			if (BuiltinLocalisations.FindIndex((LanguageInfo x) => x.LanguageName.Equals(preferredLanguage)) == -1)
			{
				int num = ExternalLocalisations.FindIndex((TranslationFile x) => x.LanguageName.Equals(preferredLanguage));
				if (num >= 0)
				{
					preferredLanguage = Path.GetFileNameWithoutExtension(ExternalLocalisations[num].FileName);
				}
			}
			if (LoadLanguage(preferredLanguage) != null || LoadLanguage("English") != null)
			{
				if (!preferredLanguage.Equals(currentTranslationFile.SystemLanguage))
				{
					OptionsMaster.BesiegeConfig.Language = currentTranslationFile.SystemLanguage;
					ReferenceMaster.SaveConfig();
				}
			}
			else
			{
				currentTranslationFile = TranslationFile.CreateDummyFile();
			}
		}

		private void DetermineSystemLanguage()
		{
			if (OptionsMaster.BesiegeConfig.AutoSetLocalisation)
			{
				int index;
				string languageFile = GetSystemLanguage(out index).LanguageFile;
				OptionsMaster.BesiegeConfig.Language = languageFile.ToString();
				OptionsMaster.BesiegeConfig.AutoSetLocalisation = true;
				LoadLanguage(languageFile);
				ReferenceMaster.SaveConfig();
			}
		}

		public TranslationFile LoadLanguage(string languageFile)
		{
			if (currentTranslationFile != null && currentTranslationFile.SystemLanguage.Equals(languageFile))
			{
				return currentTranslationFile;
			}
			TranslationFile translationFile = null;
			int num = availableLanguages.FindIndex((TranslationFile x) => x.SystemLanguage.Equals(languageFile));
			if (num == -1)
			{
				translationFile = LoadTranslationFile(languageFile);
				availableLanguages.Add(translationFile);
			}
			else
			{
				translationFile = availableLanguages[num];
			}
			int num2 = BuiltinLocalisations.FindIndex((LanguageInfo x) => x.LanguageFile.Equals(languageFile));
			if (num2 != -1)
			{
				APIClient.languageCode = BuiltinLocalisations[num2].LanguageCode;
			}
			currentTranslationFile = translationFile;
			ResetTranslations();
			return translationFile;
		}

		private static TranslationFile LoadTranslationFile(string languageFile)
		{
			string text = Path.Combine(StaticSettings.LocalisationPath, languageFile + ".txt");
			if (!File.Exists(text))
			{
				Debug.LogError("Failed to load the localisation file at '" + text + "', this should not happen.");
				return null;
			}
			string fileContent = File.ReadAllText(text);
			TranslationFile translationFile = new TranslationFile();
			translationFile.LoadFromString(fileContent, languageFile);
			return translationFile;
		}

		private void LoadLocalisationsFromUser()
		{
			DirectoryInfo directoryInfo = new DirectoryInfo(StaticSettings.LocalisationPath);
			FileInfo[] files = directoryInfo.GetFiles("*.txt");
			FileInfo[] array = files;
			foreach (FileInfo fileInfo in array)
			{
				if (IgnoreDefaultLanguages(fileInfo.Name))
				{
					continue;
				}
				string text = File.ReadAllText(fileInfo.FullName);
				if (text.Contains("[Besiege Language File]"))
				{
					TranslationFile translationFile = new TranslationFile();
					translationFile.LoadFromString(text, fileInfo.FullName);
					if (!ExternalLocalisations.Contains(translationFile))
					{
						ExternalLocalisations.Add(translationFile);
					}
				}
			}
		}

		private static bool IgnoreDefaultLanguages(string fileName)
		{
			for (int i = 0; i < BuiltinLocalisations.Count; i++)
			{
				if (fileName == BuiltinLocalisations[i].LanguageFile + ".txt")
				{
					return true;
				}
			}
			return false;
		}

		private static TranslationFile GetTranslationFile(string languageFile)
		{
			string text = Path.Combine(StaticSettings.LocalisationPath, languageFile + ".txt");
			if (!File.Exists(text))
			{
				Debug.LogError("Failed to load the localisation file at '" + text + "', this should not happen.");
				return null;
			}
			string fileContent = File.ReadAllText(text);
			TranslationFile translationFile = new TranslationFile();
			translationFile.LoadFromString(fileContent, languageFile);
			return translationFile;
		}

		public Font GetFont(Font originalFont)
		{
			int num = BuiltinLocalisations.FindIndex((LanguageInfo x) => x.LanguageFile.Equals(currentTranslationFile.FileName));
			if (num != -1 && BuiltinLocalisations[num].isAsian)
			{
				if (originalFont.name.Contains("GOST"))
				{
					return mediumCJKFont;
				}
				return boldCJKFont;
			}
			return originalFont;
		}

		public void ResetTranslations()
		{
			if (LocalisationManager.LanguageChanged != null)
			{
				LocalisationManager.LanguageChanged();
			}
			sceneLocalisationChildren.Clear();
			sceneLocalisationAwares.Clear();
			for (int i = 0; i < SceneManager.sceneCount; i++)
			{
				CacheLocalisationChildren(SceneManager.GetSceneAt(i));
			}
			foreach (LocalisationChild sceneLocalisationChild in sceneLocalisationChildren)
			{
				if (!(sceneLocalisationChild == null))
				{
					sceneLocalisationChild.Recaption();
				}
			}
			foreach (ILocalisationAware sceneLocalisationAware in sceneLocalisationAwares)
			{
				if (sceneLocalisationAware != null && Application.isPlaying)
				{
					sceneLocalisationAware.OnLocalisationChange();
				}
			}
		}

		public static int FindIndex(string language)
		{
			int num = BuiltinLocalisations.FindIndex((LanguageInfo x) => x.LanguageFile.Equals(language));
			if (num == -1)
			{
				num = ExternalLocalisations.FindIndex((TranslationFile x) => x.LanguageName.Equals(language)) + BuiltinLocalisations.Count;
			}
			return num;
		}
	}
}
