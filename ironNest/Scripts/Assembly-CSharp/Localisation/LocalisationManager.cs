using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

namespace Localisation
{
	[DefaultExecutionOrder(-100)]
	public class LocalisationManager : MonoBehaviour
	{
		public static LocalisationManager Instance;

		public const string PlayerPrefs_Language = "CurrentLanguage";

		public const string DefaultLanguage = "English";

		public LocalisationLangData LangData;

		public LocalisationFontData FontData;

		public string CurrentLanguage;

		public Dictionary<string, TextEntry> CurrentLoadedStrings;

		private bool _popupShown;

		private bool _cutscenePopupShown;

		public bool IsReady { get; private set; }

		public static event Action OnLanguageChanged
		{
			[CompilerGenerated]
			add
			{
			}
			[CompilerGenerated]
			remove
			{
			}
		}

		private void Awake()
		{
		}

		private void Start()
		{
		}

		public void ShowLanguagePopupIfNeeded()
		{
		}

		private void Load()
		{
		}

		public string Get(string Key)
		{
			return null;
		}

		public bool TryGet(string Key, out string text)
		{
			text = null;
			return false;
		}

		public TMP_FontAsset GetFont(TMP_FontAsset original)
		{
			return null;
		}

		public void DetectLanguage()
		{
		}

		public void SwitchLanguage(string language, bool save = false)
		{
		}
	}
}
