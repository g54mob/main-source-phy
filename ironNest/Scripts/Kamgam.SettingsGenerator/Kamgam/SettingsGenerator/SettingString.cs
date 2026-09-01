using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Kamgam.SettingsGenerator
{
	[Serializable]
	public class SettingString : SettingWithValue<string>, ISettingWithConnection<string>, ISettingWithValue<string>, ISetting, ISerializationCallbackReceiver, IQualityChangeReceiver, ISettingWithConnectionSO
	{
		public const SettingData.DataType DataType = SettingData.DataType.String;

		[NonSerialized]
		public IConnection<string> Connection;

		[SerializeField]
		private StringConnectionSO ConnectionObject;

		[NonSerialized]
		protected string _value;

		protected bool _valueInitialized;

		public event Action<SettingString> OnSettingStringChanged
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

		public event Action<string> OnValueChanged
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

		public override string GetValue()
		{
			return null;
		}

		public override void SetValue(string value, bool propagateChange = true)
		{
		}

		public SettingString(SettingData data, List<string> groups = null)
			: base((SettingData)null, (List<string>)null)
		{
		}

		public SettingString(string path, string value, List<string> groups = null)
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

		public void SetConnection(IConnection<string> connection)
		{
		}

		public IConnection<string> GetConnection()
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
