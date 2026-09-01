using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Kamgam.SettingsGenerator
{
	[Serializable]
	public class SettingInt : SettingWithValue<int>, ISettingWithConnection<int>, ISettingWithValue<int>, ISetting, ISerializationCallbackReceiver, IQualityChangeReceiver, ISettingWithConnectionSO
	{
		public const SettingData.DataType DataType = SettingData.DataType.Int;

		[NonSerialized]
		public IConnection<int> Connection;

		[SerializeField]
		private IntConnectionSO ConnectionObject;

		[NonSerialized]
		protected int _value;

		protected bool _valueInitialized;

		public event Action<SettingInt> OnSettingIntChanged
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

		public event Action<int> OnValueChanged
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

		public override int GetValue()
		{
			return 0;
		}

		public override void SetValue(int value, bool propagateChange = true)
		{
		}

		public SettingInt(SettingData data, List<string> groups = null)
			: base((SettingData)null, (List<string>)null)
		{
		}

		public SettingInt(string path, int value, List<string> groups = null)
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

		public void SetConnection(IConnection<int> connection)
		{
		}

		public IConnection<int> GetConnection()
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
