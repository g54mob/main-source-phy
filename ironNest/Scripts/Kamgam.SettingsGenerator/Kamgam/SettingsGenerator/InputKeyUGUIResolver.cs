using System;
using Kamgam.UGUIComponentsForSettings;
using UnityEngine;

namespace Kamgam.SettingsGenerator
{
	[AddComponentMenu("UI/Settings/InputKeyUGUIResolver")]
	[RequireComponent(typeof(InputKeyUGUI))]
	public class InputKeyUGUIResolver : SettingResolver, ISettingResolver
	{
		protected InputKeyUGUI inputKeyUGUI;

		[NonSerialized]
		protected SettingData.DataType[] supportedDataTypes;

		protected bool stopPropagation;

		public InputKeyUGUI InputKeyUGUI => null;

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

		protected string localizeKeyCode(UniversalKeyCode keyCode)
		{
			return null;
		}

		protected void onChanged(UniversalKeyCode key, UniversalKeyCode modifierKey)
		{
		}

		public override void Refresh()
		{
		}
	}
}
