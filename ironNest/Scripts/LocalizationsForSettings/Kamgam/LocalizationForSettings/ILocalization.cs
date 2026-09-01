using System;
using System.Collections.Generic;

namespace Kamgam.LocalizationForSettings
{
	public interface ILocalization
	{
		void SetDynamicLocalizationCallback(TranslateTermCallback translateTermCallback);

		[Obsolete("Please use Get(string term, bool ignoreDynamic) instead.")]
		void SetLocalizationSourceBehaviour(LocalizationSourceBehaviour behaviour);

		int DetectLanguage(bool setAsCurrent = true);

		string GetLanguage();

		int GetLanguageIndex();

		void SetLanguageIndex(int languageIndex);

		string GetLanguageAt(int landuageIndex);

		int GetLanguageIndex(string language);

		List<string> GetLanguages();

		int GetLanguageCount();

		int AddLanguage(string newLanguage);

		void SetLanguage(string language);

		void SetLanguage(int languageIndex);

		int CreateOrUpdateTranslation(string term, string language, string text);

		void DeleteTranslation(string term);

		int GetTranslationCount();

		Translation GetTranslationAt(int index);

		bool HasTerm(string term);

		string Get(string term);

		string Get(string term, bool ignoreDynamic);

		void TriggerLanguageChangeEvent();

		T LocalizeListAsCopy<T>(T terms) where T : new();

		void LocalizeList<T>(T terms, T target) where T : new();

		void AddOnLanguageChangedListener(OnLanguageChangedDelegate listener);

		void RemoveOnLanguageChangedListener(OnLanguageChangedDelegate listener);

		void Sort();
	}
}
