using UnityEngine;

namespace Selectors
{
	public class ValueHolderDefaulting : ValueHolder
	{
		public string defaultText = string.Empty;

		public int defaultValue = -1;

		protected override void Start()
		{
			base.Start();
			base.ValueChanged += delegate
			{
				ShowDefaultText();
			};
			ShowDefaultText();
			base.FocusChange += delegate(bool b)
			{
				if (!b)
				{
					ShowDefaultText();
				}
			};
			base.TextChangedExternal += delegate
			{
				ShowDefaultText();
			};
		}

		private void ShowDefaultText()
		{
			if (Mathf.Approximately(base.ValueNumber, defaultValue))
			{
				SetDefaultText();
			}
		}

		protected override string GetInputTextForEditing()
		{
			if (Mathf.Approximately(base.ValueNumber, defaultValue))
			{
				return defaultText;
			}
			return base.GetInputTextForEditing();
		}

		protected override bool ValidateValue(string text, out string validatedText, bool isExternalSet = false)
		{
			if (string.IsNullOrEmpty(text))
			{
				SetText(defaultValue);
				OnValueChanged();
			}
			return base.ValidateValue(text, out validatedText, isExternalSet);
		}

		public virtual void SetDefaultText()
		{
			ReferenceMaster.SetDynamicText(text, defaultText);
		}
	}
}
