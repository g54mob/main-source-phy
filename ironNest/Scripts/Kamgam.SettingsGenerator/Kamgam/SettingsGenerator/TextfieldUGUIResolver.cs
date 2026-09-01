using Kamgam.UGUIComponentsForSettings;
using UnityEngine;

namespace Kamgam.SettingsGenerator
{
	[AddComponentMenu("UI/Settings/TextfieldUGUIResolver")]
	[RequireComponent(typeof(TextfieldUGUI))]
	public class TextfieldUGUIResolver : SettingResolver, ISettingResolver
	{
		protected TextfieldUGUI textfieldUGUI;

		protected SettingData.DataType[] supportedDataTypes;

		protected bool stopPropagation;

		public TextfieldUGUI TextfieldUGUI => null;

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

		private void onTextChanged(string text)
		{
		}

		public override void Refresh()
		{
		}
	}
}
