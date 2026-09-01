using System.Collections.Generic;
using Kamgam.LocalizationForSettings;
using UnityEngine;
using UnityEngine.Serialization;

namespace Kamgam.SettingsGenerator
{
	public abstract class SettingResolver : MonoBehaviour, ISettingResolver
	{
		[Tooltip("The global settings provider asset.")]
		[FormerlySerializedAs("SettingsProvider")]
		[SerializeField]
		protected SettingsProvider _settingsProvider;

		[Tooltip("The global localization provider asset.")]
		public LocalizationProvider LocalizationProvider;

		[Tooltip("The ID of the setting within the Settings asset.")]
		public string ID;

		private static bool _isQuitting;

		public SettingsProvider SettingsProvider
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		public SettingsProvider GetProviderAsset()
		{
			return null;
		}

		public SettingsProvider SetProviderAsset(SettingsProvider provider)
		{
			return null;
		}

		public SettingsProvider GetProvider()
		{
			return null;
		}

		public SettingsProvider SetProvider(SettingsProvider provider)
		{
			return null;
		}

		public string GetID()
		{
			return null;
		}

		public abstract SettingData.DataType[] GetSupportedDataTypes();

		public virtual void Start()
		{
		}

		public virtual void OnEnable()
		{
		}

		private void OnApplicationQuit()
		{
		}

		public virtual void OnDisable()
		{
		}

		public virtual void OnDestroy()
		{
		}

		public bool HasValidSettingForID(string id, params SettingData.DataType[] allowedTypes)
		{
			return false;
		}

		public bool HasSettingForID(string id)
		{
			return false;
		}

		public bool HasActiveSettingForID(string id)
		{
			return false;
		}

		public SettingData.DataType GetDataType()
		{
			return default(SettingData.DataType);
		}

		public void RegisterAsActivated()
		{
		}

		public void Unregister()
		{
		}

		public abstract void Refresh();

		public static List<ISettingResolver> FindResolversInLoadedScenes(bool includeInactive = true)
		{
			return null;
		}
	}
}
