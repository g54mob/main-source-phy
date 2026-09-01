using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Serialization;

namespace Kamgam.SettingsGenerator
{
	[Serializable]
	public abstract class SettingWithValue<TValue> : ISettingWithValue<TValue>, ISetting, ISerializationCallbackReceiver, IQualityChangeReceiver, ISettingWithConnectionSO
	{
		[NonSerialized]
		protected bool _hasUserData;

		[Tooltip("If a settings is disabled then the settings system will ignore it.\nThis is useful if you want to keep a setting in the list but disable it.\nYou should not change this at runtime.")]
		[SerializeField]
		protected bool _isActive;

		public string ID;

		public const string _IdFieldName = "ID";

		[Tooltip("If true then any changes made are immediately sent to the connection. If false then you need call Apply() to push the value to the connection. If no connection is set then this does nothing.\n\nNOTICE: If you disable this on a setting without connection then you may want to use the applied listeners or the OnSettingApplied event to listen for changes.")]
		public bool ApplyImmediately;

		[SerializeField]
		[FormerlySerializedAs("Groups")]
		protected List<string> _groups;

		[SerializeField]
		[DisableIf(/*Could not decode attribute arguments.*/)]
		[Tooltip("The default value which will be used if no user setting data was found (first boot) and if no connection is set.\n\nNOTICE: If a connection is set then this will be ignored as the value will come from the connection (unless 'IgnoreConnectionDefaults' is enabled).")]
		protected TValue _defaultValue;

		[NonSerialized]
		public bool HasDefaultValue;

		[Tooltip("If enabled then the default value will NOT be fetched from the connection but will be set to the default value configured here on this setting.")]
		[DisableIf(/*Could not decode attribute arguments.*/)]
		public bool IgnoreConnectionDefaults;

		protected bool _hasChanged;

		protected Func<string, string> _translateFunc;

		protected List<Action<TValue>> _applyListeners;

		protected List<Action<TValue>> _changeListeners;

		protected List<Action<TValue>> _pulledFromConnectionListeners;

		protected List<Action> _genericPulledFromConnectionListeners;

		public bool IsActive
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

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

		public event Action<ISetting> OnSettingApplied
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

		public virtual ConnectionSO GetConnectionSO()
		{
			return null;
		}

		public virtual void SetConnectionSO(ConnectionSO connectionSO)
		{
		}

		public virtual SettingData.DataType GetConnectionSettingDataType()
		{
			return default(SettingData.DataType);
		}

		public SettingWithValue(SettingData data, List<string> groups)
		{
		}

		public SettingWithValue(string id, List<string> groups)
		{
		}

		public virtual void OnBeforeSerialize()
		{
		}

		public virtual void OnAfterDeserialize()
		{
		}

		public virtual void InitializeConnection()
		{
		}

		public string GetID()
		{
			return null;
		}

		public void SetHasUserData(bool loaded)
		{
		}

		public bool HasUserData()
		{
			return false;
		}

		public abstract SettingData.DataType GetDataType();

		public abstract TValue GetValue();

		public abstract void SetValue(TValue value, bool propagateChange = true);

		public abstract void SetValueFromObject(object value, bool propagateChange = true);

		public bool MatchesID(string id)
		{
			return false;
		}

		public virtual void SetDefault(TValue defaultValue)
		{
		}

		public virtual void SetDefaultFromConnection(IConnection<TValue> connection)
		{
		}

		public abstract void ResetToDefault();

		public void ResetToUnappliedValue(bool propagateChange = true)
		{
		}

		public bool MatchesAnyGroup(string[] groups)
		{
			return false;
		}

		public List<string> GetGroups()
		{
			return null;
		}

		public void SetGroups(List<string> groups)
		{
		}

		protected bool checkDataType(SettingData.DataType serializedDataType, SettingData.DataType dataType)
		{
			return false;
		}

		public bool MatchesAnyDataType(IList<SettingData.DataType> dataTypes)
		{
			return false;
		}

		public abstract SettingData SerializeValueToData();

		public abstract void DeserializeValueFromData(SettingData data);

		public void AddChangeListener(Action<TValue> onChanged)
		{
		}

		public void RemoveChangeListener(Action<TValue> onChanged)
		{
		}

		public void AddApplyListener(Action<TValue> onApplied)
		{
		}

		public void RemoveApplyListener(Action<TValue> onApplied)
		{
		}

		protected void invokeApplyListeners()
		{
		}

		public void Apply()
		{
		}

		public void OnChanged()
		{
		}

		protected virtual void triggerOnSettingChanged()
		{
		}

		protected virtual void triggerOnSettingApplied()
		{
		}

		public void MarkAsChanged()
		{
		}

		public void MarkAsUnchanged()
		{
		}

		public bool HasUnappliedChanges()
		{
			return false;
		}

		public void AddPulledFromConnectionListener(Action<TValue> onApply)
		{
		}

		public void RemovePulledFromConnectionListener(Action<TValue> onApply)
		{
		}

		protected void invokePulledFromConnectionListeners()
		{
		}

		public void AddPulledFromConnectionListener(Action onApply)
		{
		}

		public void RemovePulledFromConnectionListener(Action onApply)
		{
		}

		protected void invokeGenericPulledFromConnectionListeners()
		{
		}

		public void RemoveAllListeners()
		{
		}

		public abstract object GetValueAsObject();

		public abstract bool HasConnection();

		public abstract bool HasConnectionObject();

		public abstract void PullFromConnection();

		public abstract void PullFromConnection(bool propagateChange);

		public abstract IConnection GetConnectionInterface();

		public virtual void PushToConnection()
		{
		}

		public abstract int GetConnectionOrder();

		public abstract void OnQualityChanged(int qualityLevel);
	}
}
