using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Kamgam.SettingsGenerator
{
	[Serializable]
	public class SettingKeyCombination : SettingWithValue<KeyCombination>, ISettingWithConnection<KeyCombination>, ISettingWithValue<KeyCombination>, ISetting, ISerializationCallbackReceiver, IQualityChangeReceiver, ISettingWithConnectionSO
	{
		public const SettingData.DataType DataType = SettingData.DataType.KeyCombination;

		[NonSerialized]
		public IConnection<KeyCombination> Connection;

		[SerializeField]
		private KeyCombinationConnectionSO ConnectionObject;

		[NonSerialized]
		protected KeyCombination _keyCombination;

		protected bool _valueInitialized;

		public event Action<SettingKeyCombination> OnSettingKeyCombinationChanged
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

		public event Action<KeyCombination> OnValueChanged
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

		public override ConnectionSO GetConnectionSO()
		{
			return null;
		}

		public override void SetConnectionSO(ConnectionSO connectionSO)
		{
		}

		public override KeyCombination GetValue()
		{
			return default(KeyCombination);
		}

		public override void SetValue(KeyCombination keyCombination, bool propagateChange = true)
		{
		}

		public SettingKeyCombination(SettingData data, List<string> groups = null)
			: base((SettingData)null, (List<string>)null)
		{
		}

		public SettingKeyCombination(string path, KeyCombination keyCombination, List<string> groups = null)
			: base((SettingData)null, (List<string>)null)
		{
		}

		protected override void triggerOnSettingChanged()
		{
		}

		public override void ResetToDefault()
		{
		}

		public override object GetValueAsObject()
		{
			return null;
		}

		public override void SetValueFromObject(object value, bool propagateChange = true)
		{
		}

		public override SettingData.DataType GetDataType()
		{
			return default(SettingData.DataType);
		}

		public override SettingData SerializeValueToData()
		{
			return null;
		}

		public override void DeserializeValueFromData(SettingData data)
		{
		}

		public override bool HasConnection()
		{
			return false;
		}

		public override bool HasConnectionObject()
		{
			return false;
		}

		public override void PullFromConnection()
		{
		}

		public override void PullFromConnection(bool propagateChange)
		{
		}

		public override void PushToConnection()
		{
		}

		public override int GetConnectionOrder()
		{
			return 0;
		}

		public void SetConnection(IConnection<KeyCombination> connection)
		{
		}

		public IConnection<KeyCombination> GetConnection()
		{
			return null;
		}

		public override IConnection GetConnectionInterface()
		{
			return null;
		}

		public override void OnQualityChanged(int qualityLevel)
		{
		}
	}
}
