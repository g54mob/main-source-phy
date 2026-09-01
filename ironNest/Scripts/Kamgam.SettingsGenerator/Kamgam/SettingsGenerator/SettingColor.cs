using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Kamgam.SettingsGenerator
{
	[Serializable]
	public class SettingColor : SettingWithValue<Color>, ISettingWithConnection<Color>, ISettingWithValue<Color>, ISetting, ISerializationCallbackReceiver, IQualityChangeReceiver, ISettingWithConnectionSO
	{
		public const SettingData.DataType DataType = SettingData.DataType.Color;

		[NonSerialized]
		public IConnection<Color> Connection;

		[SerializeField]
		private ColorConnectionSO ConnectionObject;

		[NonSerialized]
		protected Color _color;

		protected bool _valueInitialized;

		public event Action<SettingColor> OnSettingColorChanged
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

		public event Action<Color> OnValueChanged
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

		public override Color GetValue()
		{
			return default(Color);
		}

		public override void SetValue(Color color, bool propagateChange = true)
		{
		}

		public SettingColor(SettingData data, List<string> groups = null)
			: base((SettingData)null, (List<string>)null)
		{
		}

		public SettingColor(string path, Color value, List<string> groups = null)
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

		public void SetConnection(IConnection<Color> connection)
		{
		}

		public IConnection<Color> GetConnection()
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
