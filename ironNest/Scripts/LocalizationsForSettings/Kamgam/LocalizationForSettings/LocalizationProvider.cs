using System;
using UnityEngine;

namespace Kamgam.LocalizationForSettings
{
	[Serializable]
	[CreateAssetMenu(fileName = "LocalizationProvider", menuName = "SettingsGenerator/LocalizationProvider", order = 5)]
	public class LocalizationProvider : ScriptableObject, ILocalizationProvider
	{
		[SerializeField]
		protected Localization _localization;

		public static bool IsUsable(LocalizationProvider provider)
		{
			return false;
		}

		public bool HasLocalization()
		{
			return false;
		}

		public ILocalization GetLocalization()
		{
			return null;
		}

		public string Get(string term)
		{
			return null;
		}

		public string Get(string term, string defaultValue)
		{
			return null;
		}
	}
}
