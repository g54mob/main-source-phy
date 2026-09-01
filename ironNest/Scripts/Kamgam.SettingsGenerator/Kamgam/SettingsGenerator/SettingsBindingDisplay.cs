using System;
using Kamgam.LocalizationForSettings;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Kamgam.SettingsGenerator
{
	public class SettingsBindingDisplay : SettingResolver
	{
		[Tooltip("If enabled then the configured provider will be used.")]
		public bool PreferConfiguredProvider;

		[NonSerialized]
		protected SettingData.DataType[] _supportedDataTypes;

		private bool _searchedForText;

		private TMP_Text _textMeshProText;

		private Text _unityText;

		public override SettingData.DataType[] GetSupportedDataTypes()
		{
			return null;
		}

		protected SettingsProvider resolveSettingProvider()
		{
			return null;
		}

		public ISetting GetSetting()
		{
			return null;
		}

		public void SetText(string text)
		{
		}

		public override void OnEnable()
		{
		}

		public override void OnDisable()
		{
		}

		public void BindToSetting()
		{
		}

		public void UnbindFromSetting()
		{
		}

		private void onValueChanged(ISetting setting)
		{
		}

		public override void Refresh()
		{
		}

		public static ISetting GetSetting(SettingsProvider provider, string id)
		{
			return null;
		}

		public static string GetSettingBindingDisplayName(SettingsProvider provider, string settingId, LocalizationProvider localizationProvider = null)
		{
			return null;
		}
	}
}
