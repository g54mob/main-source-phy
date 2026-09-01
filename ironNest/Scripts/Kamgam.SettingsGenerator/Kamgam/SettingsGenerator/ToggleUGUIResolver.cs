using Kamgam.UGUIComponentsForSettings;
using UnityEngine;

namespace Kamgam.SettingsGenerator
{
	[AddComponentMenu("UI/Settings/ToggleUGUIResolver")]
	[RequireComponent(typeof(ToggleUGUI))]
	public class ToggleUGUIResolver : SettingResolver, ISettingResolver
	{
		protected ToggleUGUI toggleUGUI;

		protected SettingData.DataType[] supportedDataTypes;

		protected bool stopPropagation;

		public ToggleUGUI ToggleUGUI => null;

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

		private void onValueChanged(bool value)
		{
		}

		public override void Refresh()
		{
		}
	}
}
