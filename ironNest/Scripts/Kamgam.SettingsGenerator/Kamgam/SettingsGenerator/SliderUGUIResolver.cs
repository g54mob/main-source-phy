using System;
using Kamgam.UGUIComponentsForSettings;
using UnityEngine;

namespace Kamgam.SettingsGenerator
{
	[AddComponentMenu("UI/Settings/SliderUGUIResolver")]
	[RequireComponent(typeof(SliderUGUI))]
	public class SliderUGUIResolver : SettingResolver, ISettingResolver
	{
		protected SliderUGUI _sliderUGUI;

		[NonSerialized]
		protected SettingData.DataType[] supportedDataTypes;

		protected float _lastValue;

		protected bool stopPropagation;

		public SliderUGUI SliderUGUI => null;

		public override SettingData.DataType[] GetSupportedDataTypes()
		{
			return null;
		}

		public override void Start()
		{
		}

		public override void OnDestroy()
		{
		}

		private void onValueChanged(float value)
		{
		}

		public override void Refresh()
		{
		}
	}
}
