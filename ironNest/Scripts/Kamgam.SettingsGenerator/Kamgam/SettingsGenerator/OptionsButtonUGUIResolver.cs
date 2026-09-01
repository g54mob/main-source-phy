using System.Collections.Generic;
using Kamgam.UGUIComponentsForSettings;
using UnityEngine;

namespace Kamgam.SettingsGenerator
{
	[AddComponentMenu("UI/Settings/OptionsButtonUGUIResolver")]
	[RequireComponent(typeof(OptionsButtonUGUI))]
	public class OptionsButtonUGUIResolver : SettingResolver
	{
		protected OptionsButtonUGUI optionsButtonUGUI;

		protected SettingData.DataType[] supportedDataTypes;

		protected bool stopPropagation;

		protected List<string> _localizedOptionLabels;

		public OptionsButtonUGUI OptionsButtonUGUI => null;

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

		private void onValueChanged(int selectedIndex)
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
