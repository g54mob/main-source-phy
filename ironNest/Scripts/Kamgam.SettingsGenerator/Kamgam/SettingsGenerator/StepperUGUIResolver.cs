using Kamgam.UGUIComponentsForSettings;
using UnityEngine;

namespace Kamgam.SettingsGenerator
{
	[AddComponentMenu("UI/Settings/StepperUGUIResolver")]
	[RequireComponent(typeof(StepperUGUI))]
	public class StepperUGUIResolver : SettingResolver, ISettingResolver
	{
		protected StepperUGUI stepperUGUI;

		protected SettingData.DataType[] supportedDataTypes;

		protected bool stopPropagation;

		public StepperUGUI StepperUGUI => null;

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
