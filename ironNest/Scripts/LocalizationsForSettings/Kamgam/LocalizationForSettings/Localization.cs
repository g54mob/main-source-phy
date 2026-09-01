using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Kamgam.LocalizationForSettings
{
	[Serializable]
	public class Localization : ILocalization
	{
		[Tooltip("The default language used if language detection fails to find a language (or if auto detect is disabled.")]
		public string DefaultLanguage;

		[Tooltip("Should the language be detected at start? If disabled then the default language is used.")]
		public bool AutoDetectLanguage;

		[SerializeField]
		protected List<string> _languages;

		[SerializeField]
		[HideInInspector]
		protected List<Translation> _translations;

		public OnLanguageChangedDelegate OnLanguageChanged;

		public UnityEvent<string> OnLanguageChangedEvent;

		[Header("Debug")]
		public bool LogMissingTerms;

		[NonSerialized]
		protected TranslateTermCallback _translateTermCallback;

		[NonSerialized]
		protected LocalizationSourceBehaviour _sourceBehaviour;

		[NonSerialized]
		protected int _currentLanguageIndex;

		public void SetDynamicLocalizationCallback(TranslateTermCallback translateTermCallback)
		{
		}

		[Obsolete("Please use Get(string term, bool ignoreDynamic) instead.")]
		public void SetLocalizationSourceBehaviour(LocalizationSourceBehaviour behaviour)
		{
		}

		private void autoDetectLanguage()
		{
		}

		public int DetectLanguage(bool setAsCurrent = true)
		{
			return 0;
		}

		public int AddLanguage(string newLanguage)
		{
			return 0;
		}

		public string GetLanguage()
		{
			return null;
		}

		public int GetLanguageIndex()
		{
			return 0;
		}

		public string GetLanguageAt(int languageIndex)
		{
			return null;
		}

		public int GetLanguageIndex(string language)
		{
			return 0;
		}

		public void SetLanguageIndex(int languageIndex)
		{
		}

		public void SetLanguage(string language)
		{
		}

		public void SetLanguage(int languageIndex)
		{
		}

		public List<string> GetLanguages()
		{
			return null;
		}

		public int GetLanguageCount()
		{
			return 0;
		}

		public int GetTranslationCount()
		{
			return 0;
		}

		public Translation GetTranslationAt(int index)
		{
			return null;
		}

		public int CreateOrUpdateTranslation(string term, string language, string text)
		{
			return 0;
		}

		public void DeleteTranslation(string term)
		{
		}

		public bool HasTerm(string term)
		{
			return false;
		}

		public string Get(string term)
		{
			return null;
		}

		public string Get(string term, bool ignoreDynamic)
		{
			return null;
		}

		public T LocalizeListAsCopy<T>(T terms) where T : new()
		{
			return default(T);
		}

		public void LocalizeList<T>(T terms, T target) where T : new()
		{
		}

		public void AddOnLanguageChangedListener(OnLanguageChangedDelegate listener)
		{
		}

		public void RemoveOnLanguageChangedListener(OnLanguageChangedDelegate listener)
		{
		}

		public void Sort()
		{
		}

		protected int sortByTerm(Translation a, Translation b)
		{
			return 0;
		}

		public void TriggerLanguageChangeEvent()
		{
		}
	}
}
