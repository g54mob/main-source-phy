using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Kamgam.SettingsGenerator
{
	[Serializable]
	public class SettingColorOption : SettingWithValue<int>, ISettingWithOptions<Color>, ISettingWithConnection<int>, ISettingWithValue<int>, ISetting, ISerializationCallbackReceiver, IQualityChangeReceiver, ISettingWithConnectionSO
	{
		public const SettingData.DataType DataType = SettingData.DataType.ColorOption;

		[SerializeField]
		[Tooltip("The color options.\nThese define which colors the user can choose from. The resulting value will the selected color.\nThese are IGNORED if a connection is set.")]
		protected List<Color> _options;

		[SerializeField]
		[Tooltip("Should the option labels be taken from the static ones defined here even if a connection is set?\n\nUsually if a connection is set then the labels are fetched dynamically from the connection.\nBe aware that if you set the labels here then the number of labels has to match the connection options. Otherwise this will have no effect.")]
		[DisableIf(/*Could not decode attribute arguments.*/)]
		protected bool _overrideConnectionLabels;

		[NonSerialized]
		public IConnectionWithOptions<Color> Connection;

		[SerializeField]
		private ColorOptionConnectionSO ConnectionObject;

		[NonSerialized]
		protected int _selectedIndex;

		protected bool _valueInitialized;

		public event Action<SettingColorOption> OnSettingColorOptionChanged
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

		public Color GetColorValue()
		{
			return default(Color);
		}

		public Color GetColorValue(Color defaultColor)
		{
			return default(Color);
		}

		public override void SetValue(int selectedIndex, bool propagateChange = true)
		{
		}

		public SettingColorOption(SettingData data, List<string> groups = null, List<Color> options = null)
			: base((SettingData)null, (List<string>)null)
		{
		}

		public SettingColorOption(string path, int selectedIndex, List<string> groups = null, List<Color> options = null)
			: base((SettingData)null, (List<string>)null)
		{
		}

		protected override void triggerOnSettingChanged()
		{
		}

		public override void OnAfterDeserialize()
		{
		}

		public void SetOverrideConnectionLabels(bool overrideLabels)
		{
		}

		protected void initLabelOverridesAfterNewConnection()
		{
		}

		public bool GetOverrideConnectionLabels()
		{
			return false;
		}

		public override void ResetToDefault()
		{
		}

		public void SetOptionLabels(List<Color> options)
		{
		}

		public bool HasOptions()
		{
			return false;
		}

		public List<Color> GetOptionLabels()
		{
			return null;
		}

		public void ClearOptions()
		{
		}

		public void AddOptions(IEnumerable<Color> optionsToAdd)
		{
		}

		public override void SetValueFromObject(object value, bool propagateChange = true)
		{
		}

		public override object GetValueAsObject()
		{
			return null;
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

		public void SetConnection(IConnectionWithOptions<Color> connection)
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

		public void SetConnection(IConnection<int> connection)
		{
		}

		public override void OnQualityChanged(int qualityLevel)
		{
		}
	}
}
