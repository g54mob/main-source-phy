using System;
using System.Collections.Generic;
using UnityEngine;

namespace Localisation
{
	[CreateAssetMenu(fileName = "LocalisationLangData", menuName = "Localisation/Lang Data")]
	public class LocalisationLangData : ScriptableObject
	{
		[Serializable]
		public class LangData
		{
			public string Lang;

			public SystemLanguage UnityLang;

			public string DisplayName;

			public string LangCode;
		}

		private static LocalisationLangData _instance;

		private const string DefaultLanguage = "English";

		public List<LangData> SupportedLanguages;

		private Dictionary<string, LangData> LookupByLang;

		private Dictionary<SystemLanguage, LangData> LookupByUnityLang;

		private Dictionary<string, LangData> LookupByDisplayName;

		private Dictionary<string, LangData> LookupByLangCode;

		public static LocalisationLangData Instance => null;

		public void Init()
		{
		}

		public LangData GetLanguageData(string language)
		{
			return null;
		}

		public LangData GetLanguageData(SystemLanguage language)
		{
			return null;
		}

		private static string Clean(string value)
		{
			return null;
		}
	}
}
