using System.Globalization;
using UnityEngine;

namespace Selectors
{
	public class ColourHolder : TextHolder
	{
		public static bool IsTypingColour;

		private Color _valueColour;

		public event ColourChangeHandler ColourChanged;

		private void Awake()
		{
			CharLimit = ((!StatMaster.KeyMapper.disableSliderLimits) ? 6 : 9);
			prefix = "#";
			base.TextChanged += delegate
			{
				OnValueChanged();
			};
		}

		protected override bool ValidateValue(string text, out string validatedText, bool isExternalSet = false)
		{
			Color value = default(Color);
			if (!TryGetValue(text, out value))
			{
				validatedText = null;
				return false;
			}
			_valueColour = value;
			validatedText = ColorToHex(value);
			return true;
		}

		protected override bool ValidateInput(char c)
		{
			return char.IsDigit(c) || c == 'A' || c == 'B' || c == 'C' || c == 'D' || c == 'E' || c == 'F' || c == 'a' || c == 'b' || c == 'c' || c == 'd' || c == 'e' || c == 'f';
		}

		protected override string GetInputTextForEditing()
		{
			return ColorToHex(_valueColour);
		}

		public void SetText(Color value)
		{
			SetText(ColorToHex(value));
			_valueColour = value;
		}

		private void OnValueChanged()
		{
			if (this.ColourChanged != null && !IsTypingColour)
			{
				IsTypingColour = true;
				this.ColourChanged(_valueColour);
				IsTypingColour = false;
			}
		}

		private string ColorToHex(Color color)
		{
			bool extended = StatMaster.KeyMapper.disableSliderLimits && (color.r > 1f || color.g > 1f || color.b > 1f);
			string text = ChannelToHex(color.r, extended);
			string text2 = ChannelToHex(color.g, extended);
			string text3 = ChannelToHex(color.b, extended);
			return string.Format("{0}", text + text2 + text3);
		}

		private string ChannelToHex(float c, bool extended = false)
		{
			if (!extended && c > 1f)
			{
				c = 1f;
			}
			return ((int)(c * 255f)).ToString((!extended) ? "X2" : "X3");
		}

		private bool TryGetValue(string t, out Color value)
		{
			if (t.Length > 0 && t.Length <= 6)
			{
				t = t.PadRight(6, '0');
				float r = (float)int.Parse(string.Empty + t[0] + t[1], NumberStyles.HexNumber) / 255f;
				float g = (float)int.Parse(string.Empty + t[2] + t[3], NumberStyles.HexNumber) / 255f;
				float b = (float)int.Parse(string.Empty + t[4] + t[5], NumberStyles.HexNumber) / 255f;
				value = new Color(r, g, b, 1f);
				return true;
			}
			if (StatMaster.KeyMapper.disableSliderLimits && t.Length == 9)
			{
				float r2 = (float)int.Parse(string.Empty + t[0] + t[1] + t[2], NumberStyles.HexNumber) / 255f;
				float g2 = (float)int.Parse(string.Empty + t[3] + t[4] + t[5], NumberStyles.HexNumber) / 255f;
				float b2 = (float)int.Parse(string.Empty + t[6] + t[7] + t[8], NumberStyles.HexNumber) / 255f;
				value = new Color(r2, g2, b2, 1f);
				return true;
			}
			value = default(Color);
			return false;
		}
	}
}
