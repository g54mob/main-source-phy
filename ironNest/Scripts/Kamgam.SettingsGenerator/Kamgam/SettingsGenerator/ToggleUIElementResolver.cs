using UnityEngine.UIElements;

namespace Kamgam.SettingsGenerator
{
	public class ToggleUIElementResolver : SettingResolverForVisualElement, ISettingResolver
	{
		protected Toggle _toggle;

		protected SettingData.DataType[] supportedDataTypes;

		protected bool stopPropagation;

		public Toggle Toggle => null;

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

		protected void onValueChanged(ChangeEvent<bool> evt)
		{
		}

		public override void Refresh()
		{
		}
	}
}
