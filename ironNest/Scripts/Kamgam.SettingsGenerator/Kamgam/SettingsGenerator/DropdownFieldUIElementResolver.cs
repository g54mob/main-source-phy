using System.Collections.Generic;
using UnityEngine.UIElements;

namespace Kamgam.SettingsGenerator
{
	public class DropdownFieldUIElementResolver : SettingResolverForVisualElement, ISettingResolver
	{
		protected DropdownField _dropDown;

		protected SettingData.DataType[] supportedDataTypes;

		protected bool stopPropagation;

		protected List<string> _localizedOptionLabels;

		public DropdownField DropDown => null;

		public override SettingData.DataType[] GetSupportedDataTypes()
		{
			return null;
		}

		public override void Start()
		{
		}

		public override void OnDisable()
		{
		}

		public override void OnDestroy()
		{
		}

		protected void onLanguageChanged(string language)
		{
		}

		protected void onSelectionChanged(ChangeEvent<string> evt)
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
