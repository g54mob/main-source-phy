using System.Collections.Generic;
using Kamgam.UGUIComponentsForSettings;
using UnityEngine;

namespace Kamgam.SettingsGenerator
{
	[AddComponentMenu("UI/Settings/DropDownUGUIResolver")]
	[RequireComponent(typeof(DropDownUGUI))]
	public class DropDownUGUIResolver : SettingResolver, ISettingResolver
	{
		protected DropDownUGUI dropDownUGUI;

		protected SettingData.DataType[] supportedDataTypes;

		protected bool stopPropagation;

		protected List<string> _localizedOptionLabels;

		public DropDownUGUI DropDownUGUI => null;

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

		protected void onLanguageChanged(string language)
		{
		}

		protected void onSelectionChanged(int selectedIndex)
		{
		}

		public override void Refresh()
		{
		}

		protected void refreshOptions()
		{
		}
	}
}
