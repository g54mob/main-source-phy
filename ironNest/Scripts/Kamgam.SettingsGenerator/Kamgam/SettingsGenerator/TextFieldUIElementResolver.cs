using UnityEngine.UIElements;

namespace Kamgam.SettingsGenerator
{
	public class TextFieldUIElementResolver : SettingResolverForVisualElement, ISettingResolver
	{
		protected TextField _textfield;

		protected SettingData.DataType[] supportedDataTypes;

		protected bool stopPropagation;

		public TextField Textfield => null;

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

		protected void onValueChanged(ChangeEvent<string> evt)
		{
		}

		public override void Refresh()
		{
		}
	}
}
