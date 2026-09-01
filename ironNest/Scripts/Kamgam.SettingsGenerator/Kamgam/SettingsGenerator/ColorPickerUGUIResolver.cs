using Kamgam.UGUIComponentsForSettings;
using UnityEngine;

namespace Kamgam.SettingsGenerator
{
	[AddComponentMenu("UI/Settings/ColorPickerUGUIResolver")]
	[RequireComponent(typeof(ColorPickerUGUI))]
	public class ColorPickerUGUIResolver : SettingResolver, ISettingResolver
	{
		protected ColorPickerUGUI colorPickerUGUI;

		protected SettingData.DataType[] supportedDataTypes;

		protected bool stopPropagation;

		public ColorPickerUGUI ColorPickerUGUI => null;

		public override SettingData.DataType[] GetSupportedDataTypes()
		{
			return null;
		}

		public override void Start()
		{
		}

		private void onSelectionChanged(int selectedIndex)
		{
		}

		public override void Refresh()
		{
		}

		private void refreshOptions()
		{
		}

		public override void OnDestroy()
		{
		}
	}
}
