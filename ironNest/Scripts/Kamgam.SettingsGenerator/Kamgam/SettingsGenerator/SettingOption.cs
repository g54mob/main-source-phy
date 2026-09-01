using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Kamgam.SettingsGenerator
{
	[Serializable]
	public class SettingOption : SettingWithValue<int>, ISettingWithOptions<string>, ISettingWithConnection<int>, ISettingWithValue<int>, ISetting, ISerializationCallbackReceiver, IQualityChangeReceiver, ISettingWithConnectionSO
	{
		public const SettingData.DataType DataType = SettingData.DataType.Option;

		[SerializeField]
		[Tooltip("The names of the options.\nThese define which options the user can choose from. The resulting value will the index of the selected option label (index is starting with 0).\nThese are IGNORED if a connection is set.")]
		protected List<string> _optionLabels;

		[SerializeField]
		[Tooltip("Should the option labels be taken from the static ones defined here even if a connection is set?\n\nUsually if a connection is set then the labels are fetched dynamically from the connection.\nBe aware that if you set the labels here then the number of labels has to match the connection options. Otherwise this will have no effect.")]
		[DisableIf(/*Could not decode attribute arguments.*/)]
		protected bool _overrideConnectionLabels;

		[NonSerialized]
		public IConnectionWithOptions<string> Connection;

		[SerializeField]
		[Tooltip("The connection which is used to dynamically fill the option names and value.\nIf this is set then the OptionLabels and DefaultIndex fields are ignored.\nYou can override this by enabling 'overrideConnectionLabels'.")]
		private OptionConnectionSO ConnectionObject;

		[NonSerialized]
		protected int _selectedIndex;

		protected bool _valueInitialized;

		public event Action<SettingOption> OnSettingOptionChanged
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

		public override void SetValue(int selectedIndex, bool propagateChange = true)
		{
		}

		public SettingOption(SettingData data, List<string> groups = null, List<string> optionNames = null)
			: base((SettingData)null, (List<string>)null)
		{
		}

		public SettingOption(string path, int selectedIndex, List<string> groups = null, List<string> optionNames = null)
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

		public bool HasOptions()
		{
			return false;
		}

		public List<string> GetOptionLabels()
		{
			return null;
		}

		public void SetOptionLabels(List<string> options)
		{
		}

		public void ClearOptionLabels()
		{
		}

		public void AddOptionLabels(IEnumerable<string> optionsToAdd)
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

		public void SetConnection(IConnectionWithOptions<string> connection)
		{
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
