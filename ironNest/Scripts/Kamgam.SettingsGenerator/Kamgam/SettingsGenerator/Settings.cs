using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Kamgam.SettingsGenerator
{
	[CreateAssetMenu(fileName = "Settings", menuName = "SettingsGenerator/Settings", order = 2)]
	public class Settings : ScriptableObject, ISerializationCallbackReceiver
	{
		public delegate void CustomStorageMethod(string key, Settings settings);

		protected bool _isLoading;

		protected List<ISetting> _settingsCache;

		[SerializeField]
		protected List<SettingBool> _bools;

		[SerializeField]
		protected List<SettingOption> _options;

		[SerializeField]
		protected List<SettingInt> _integers;

		[SerializeField]
		protected List<SettingFloat> _floats;

		[SerializeField]
		protected List<SettingString> _strings;

		[SerializeField]
		protected List<SettingColor> _colors;

		[SerializeField]
		protected List<SettingColorOption> _colorOptions;

		[SerializeField]
		protected List<SettingKeyCombination> _keyCombinations;

		[NonSerialized]
		public static List<string> DeactivateBeforeInit;

		[NonSerialized]
		public static CustomStorageMethod CustomSaveMethod;

		[NonSerialized]
		public static CustomStorageMethod CustomLoadMethod;

		[NonSerialized]
		public static CustomStorageMethod CustomDeleteMethod;

		private static List<string> _tmpExistingIdsBeforeLoad;

		protected List<ISetting> _tmpSettingsSortedByConnectionOrder;

		protected List<ISetting> _tmpSettingsSortedByName;

		private static List<SettingOption> s_tmpRefreshSettingOptionConnectionAndResolversList;

		[NonSerialized]
		public List<ISettingResolver> RegisteredResolvers;

		[NonSerialized]
		public int ActiveResolverCount;

		public event Action<ISetting> OnSettingChanged
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

		public static void AddToDeactivateBeforeInit(params string[] ids)
		{
		}

		public void RebuildSettingsCache()
		{
		}

		public List<ISetting> GetAllSettings()
		{
			return null;
		}

		public List<ISetting> GetUnappliedSettings(List<ISetting> results = null)
		{
			return null;
		}

		public bool HasUnappliedSettings()
		{
			return false;
		}

		protected void onSettingChanged(ISetting setting)
		{
		}

		public void RemoveSetting(ISetting setting)
		{
		}

		public void RemoveSetting(string id)
		{
		}

		protected void removeSetting<T>(List<T> list, string id)
		{
		}

		public void OnBeforeSerialize()
		{
		}

		public void OnAfterDeserialize()
		{
		}

		public void Load(string key, SettingsSaverBase settingsSaver)
		{
		}

		public void Load(string key, SettingsSaverBase settingsSaver, bool removeUnknownSettingsAfterLoad, SettingsProvider provider)
		{
		}

		protected void postLoad(SettingsProvider provider)
		{
		}

		protected void deactivateBeforeInitialization()
		{
		}

		public void Save(string key, SettingsSaverBase settingsSaver)
		{
		}

		public void Delete(string key, SettingsSaverBase settingsSaver)
		{
		}

		public static void DeletePlayerPrefs(string playerPrefsKey)
		{
		}

		public void Apply(bool changedOnly = true, bool triggerChangeEvents = false)
		{
		}

		public void TriggerChangeEvent(bool skipSettingsWithConnections = true)
		{
		}

		public void PullFromConnection(IConnection connection, bool exceptUnapplied = false, bool propagateChange = false)
		{
		}

		public void PushToConnection(IConnection connection, bool exceptUnapplied = false)
		{
		}

		public void PullFromConnections(bool exceptUnapplied = false, bool propagateChange = false)
		{
		}

		public void PushToConnections()
		{
		}

		public void PushToConnections(params string[] groups)
		{
		}

		protected List<ISetting> getSettingsOrderedByConnectionOrderASC(IEnumerable<ISetting> settings)
		{
			return null;
		}

		protected int compareByConnectionOrder(ISetting a, ISetting b)
		{
			return 0;
		}

		protected List<ISetting> getSettingsOrderedByID(IEnumerable<ISetting> settings)
		{
			return null;
		}

		protected int compareByID(ISetting a, ISetting b)
		{
			return 0;
		}

		public bool HasID(string id)
		{
			return false;
		}

		public bool HasActiveID(string id)
		{
			return false;
		}

		public ISetting GetSetting(string id)
		{
			return null;
		}

		public ISetting GetActiveSetting(string id)
		{
			return null;
		}

		protected bool doesOtherSettingExist(string id, SettingData.DataType dataType)
		{
			return false;
		}

		public ISetting GetOrCreate(string id, SettingData.DataType dataType)
		{
			return null;
		}

		public SettingBool GetOrCreateBool(string id, bool defaultValue = false, List<string> groups = null, IConnection<bool> connection = null, SettingsProvider provider = null)
		{
			return null;
		}

		protected void initConnectionForSetting<T>(ISettingWithConnection<T> setting, IConnection<T> connection, SettingsProvider provider)
		{
		}

		public SettingBool GetBool(string id)
		{
			return null;
		}

		protected SettingBool addBool(string id, bool value, List<string> groups = null)
		{
			return null;
		}

		public SettingBool AddBoolFromSerializedData(SettingData data, List<string> groups = null)
		{
			return null;
		}

		public SettingColor GetOrCreateColor(string id, Color defaultValue, List<string> groups = null, IConnection<Color> connection = null, SettingsProvider provider = null)
		{
			return null;
		}

		public SettingColor GetColor(string id)
		{
			return null;
		}

		protected SettingColor addColor(string id, Color value, List<string> groups = null)
		{
			return null;
		}

		public SettingColor AddColorFromSerializedData(SettingData data, List<string> groups = null)
		{
			return null;
		}

		public SettingColorOption GetOrCreateColorOption(string id, int defaultOption = 0, List<string> groups = null, List<Color> options = null, IConnectionWithOptions<Color> connection = null, SettingsProvider provider = null)
		{
			return null;
		}

		public SettingColorOption GetColorOption(string id)
		{
			return null;
		}

		protected SettingColorOption addColorOption(string id, int selectedIndex, List<string> groups = null, List<Color> options = null)
		{
			return null;
		}

		public SettingColorOption AddColorOptionFromSerializedData(SettingData data, List<string> groups = null, List<Color> options = null)
		{
			return null;
		}

		public SettingFloat GetOrCreateFloat(string id, float defaultValue = 0f, List<string> groups = null, IConnection<float> connection = null, SettingsProvider provider = null)
		{
			return null;
		}

		public SettingFloat GetFloat(string id)
		{
			return null;
		}

		protected SettingFloat addFloat(string id, float value, List<string> groups = null)
		{
			return null;
		}

		public SettingFloat AddFloatFromSerializedData(SettingData data, List<string> groups = null)
		{
			return null;
		}

		public SettingInt GetOrCreateInt(string id, int defaultValue = 0, List<string> groups = null, IConnection<int> connection = null, SettingsProvider provider = null)
		{
			return null;
		}

		public SettingInt GetInt(string id)
		{
			return null;
		}

		protected SettingInt addInt(string id, int value, List<string> groups = null)
		{
			return null;
		}

		public SettingInt AddIntFromSerializedData(SettingData data, List<string> groups = null)
		{
			return null;
		}

		public SettingKeyCombination GetOrCreateKeyCombination(string id, KeyCombination defaultValue, List<string> groups = null, IConnection<KeyCombination> connection = null, SettingsProvider provider = null)
		{
			return null;
		}

		protected SettingKeyCombination addKeyCombination(string id, KeyCombination value, List<string> groups = null)
		{
			return null;
		}

		public SettingKeyCombination AddKeyCombinationFromSerializedData(SettingData data, List<string> groups = null)
		{
			return null;
		}

		public SettingKeyCombination GetKeyCombination(string id)
		{
			return null;
		}

		public SettingOption GetOrCreateOption(string id, int defaultOption = 0, List<string> groups = null, List<string> options = null, IConnectionWithOptions<string> connection = null, SettingsProvider provider = null)
		{
			return null;
		}

		public SettingOption GetOption(string id)
		{
			return null;
		}

		protected SettingOption addOption(string id, int selectedIndex, List<string> groups = null, List<string> options = null)
		{
			return null;
		}

		public SettingOption AddOptionFromSerializedData(SettingData data, List<string> groups = null, List<string> options = null)
		{
			return null;
		}

		public SettingString GetOrCreateString(string id, string defaultValue = "", List<string> groups = null, IConnection<string> connection = null, SettingsProvider provider = null)
		{
			return null;
		}

		public SettingString GetString(string id)
		{
			return null;
		}

		protected SettingString addString(string id, string value, List<string> groups = null)
		{
			return null;
		}

		public SettingString AddStringFromSerializedData(SettingData data, List<string> groups = null)
		{
			return null;
		}

		public object GetValue(string id)
		{
			return null;
		}

		public T GetValue<T>(string id)
		{
			return default(T);
		}

		public void SetValue(string id, object value)
		{
		}

		public void SetActive(string id, bool active)
		{
		}

		public void SetAllActive(bool active)
		{
		}

		public void OnQualityChanged(int qualityLevel, bool excludeChanged = false)
		{
		}

		public string[] GetSettingIDsOrderedByName(bool filterByDataType = false, params SettingData.DataType[] dataTypes)
		{
			return null;
		}

		public IList<TSetting> GetSettingsWithConnectionByType<TSetting, TConnection>(IList<TSetting> results = null) where TSetting : class where TConnection : class
		{
			return null;
		}

		public TSetting GetFirstSettingWithConnectionByType<TSetting, TConnection>() where TSetting : class where TConnection : class
		{
			return null;
		}

		public TConnection GetFirstConnectionByType<TConnection>() where TConnection : class
		{
			return null;
		}

		public IList<TConnection> GetConnectionsByType<TConnection>(IList<TConnection> results = null) where TConnection : class
		{
			return null;
		}

		public IList<TSetting> GetSettingsWithConnection<TSetting>(IConnection connection, IList<TSetting> results = null) where TSetting : class
		{
			return null;
		}

		public IList<ISetting> GetSettingsWithConnection(IConnection connection, IList<ISetting> results = null)
		{
			return null;
		}

		public ISetting GetFirstSettingWithConnectionSO(ConnectionSO connectionSO)
		{
			return null;
		}

		public void RefreshSettingOptionConnectionAndResolvers<TConnection>(bool refreshResolvers = true)
		{
		}

		public void RefreshSettingOptionConnectionAndResolvers<TConnection, TOption>(bool refreshResolvers = true)
		{
		}

		public void SetInputActionAsset(InputActionAsset asset, bool applyImmediately = true)
		{
		}

		public InputActionAsset GetInputActionAsset()
		{
			return null;
		}

		public void RegisterResolver(ISettingResolver resolver)
		{
		}

		public void UnregisterResolver(ISettingResolver resolver)
		{
		}

		public void DefragRegisteredResolvers()
		{
		}

		public void RefreshRegisteredResolvers()
		{
		}

		public void RefreshRegisteredResolvers(string id)
		{
		}

		public void RefreshRegisteredResolvers(ISetting setting)
		{
		}

		public void RefreshRegisteredResolversWithConnection<T>()
		{
		}

		public void RefreshRegisteredResolversWithConnection(IConnection connection)
		{
		}

		public void Reset()
		{
		}

		public void Reset(params string[] ids)
		{
		}

		public void ResetControls()
		{
		}

		public void ResetWrongControls()
		{
		}

		public void ResetGroups(params string[] groups)
		{
		}

		public void ResetWithoutGroups()
		{
		}

		public void ResetToUnappliedValues()
		{
		}

		public void ResetToUnappliedValues(bool propagateChange)
		{
		}

		public void ResetToUnappliedValues(params string[] ids)
		{
		}

		public void ResetToUnappliedValues(bool propagateChange, params string[] ids)
		{
		}
	}
}
