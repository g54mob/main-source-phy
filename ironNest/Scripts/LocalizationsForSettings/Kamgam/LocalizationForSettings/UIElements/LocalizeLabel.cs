using System;
using UnityEngine.UIElements;

namespace Kamgam.LocalizationForSettings.UIElements
{
	public class LocalizeLabel : LocalizeVisualElement
	{
		protected Label _label;

		public Label Label => null;

		public override Type GetElementType()
		{
			return null;
		}

		public override void Awake()
		{
		}

		public override void OnDisable()
		{
		}

		public override string GetText()
		{
			return null;
		}

		public override void SetText(string text)
		{
		}
	}
}
