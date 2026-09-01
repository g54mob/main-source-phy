using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Kamgam.SettingsGenerator
{
	[Serializable]
	public class SettingBool : SettingWithValue<bool>, ISettingWithConnection<bool>, ISettingWithValue<bool>, ISetting, ISerializationCallbackReceiver, IQualityChangeReceiver, ISettingWithConnectionSO
	{
		public const SettingData.DataType DataType = SettingData.DataType.Bool;

		[NonSerialized]
		public IConnection<bool> Connection;

		[SerializeField]
		private BoolConnectionSO ConnectionObject;

		[NonSerialized]
		protected bool _value;

		protected bool _valueInitialized;

		public event Action<SettingBool> OnSettingBoolChanged
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

		public event Action<bool> OnValueChanged
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

		public override bool GetValue()
		{
			return false;
		}

		public override void SetValue(bool value, bool propagateChange = true)
		{
		}

		public SettingBool(SettingData data, List<string> groups = null)
			: base((SettingData)null, (List<string>)null)
		{
		}

		public SettingBool(string path, bool value, List<string> groups = null)
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

		protected void extractConnectionFromObject()
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

		public void SetConnection(IConnection<bool> connection)
		{
		}

		public IConnection<bool> GetConnection()
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
