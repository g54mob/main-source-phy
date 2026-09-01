using System;
using System.Globalization;
using Localisation;
using UnityEngine;

namespace Selectors
{
	public class ValueHolder : TextHolder
	{
		private float _valueNumber;

		private bool isSettingNumber;

		[Tooltip("Decimals to display when not editing.")]
		public int Decimals = 2;

		[Tooltip("Decimal places the value can have.")]
		public int MaxDecimals;

		public bool splitThousands;

		[Tooltip("Whether negative numbers can be typed in.")]
		public bool negativeNumbers = true;

		public float maxValue = float.PositiveInfinity;

		public float minValue = float.NegativeInfinity;

		private bool valueInitialised;

		public float ValueNumber
		{
			get
			{
				return _valueNumber;
			}
			set
			{
				SetValue(value);
			}
		}

		public bool IsSettingNumber
		{
			get
			{
				return isSettingNumber;
			}
		}

		public event ValueChangeHandler ValueChanged;

		public event Action<float> ValueChangedExternal;

		protected override void Start()
		{
			base.Start();
			allowCopy = true;
			if (!valueInitialised && !string.IsNullOrEmpty(base.ValueText))
			{
				float result;
				if (float.TryParse(base.ValueText, NumberStyles.Float, StaticSettings.Culture, out result) || IsInfinityText(base.ValueText, out result))
				{
					_valueNumber = result;
					valueInitialised = true;
				}
				else
				{
					Debug.LogError("[ValueHolder] Could not parse initial value '" + base.ValueText + "' to float.");
				}
			}
			base.TextChanged += delegate
			{
				OnValueChanged();
			};
		}

		protected void OnValueChanged()
		{
			if (this.ValueChanged != null && !IsSettingNumber)
			{
				isSettingNumber = true;
				this.ValueChanged(ValueNumber);
				isSettingNumber = false;
			}
		}

		public float GetValue()
		{
			return ValueNumber;
		}

		[Obsolete("Use ValueNumber instead.")]
		public bool TryGetValue(out float value)
		{
			value = ValueNumber;
			return true;
		}

		public void SetValue(float value)
		{
			if (ValidateValue(value, out value, true))
			{
				SetText(GetValueString(value));
				_valueNumber = value;
				valueInitialised = true;
				if (this.ValueChangedExternal != null)
				{
					this.ValueChangedExternal(value);
				}
			}
		}

		public void SetText(float value)
		{
			SetValue(value);
		}

		protected override bool ValidateInput(char input)
		{
			return char.IsDigit(input) || (negativeNumbers && input == '-') || input == '*' || input == ',' || input == '.' || input == 'E' || input == 'e';
		}

		protected override bool ValidateValue(string text, out string validatedText, bool isExternalSet = false)
		{
			float infinityValue;
			if (IsInfinityText(text, out infinityValue) && ValidateValue(infinityValue, out infinityValue, isExternalSet))
			{
				validatedText = text;
				_valueNumber = infinityValue;
				return true;
			}
			text = text.Replace(",", ".");
			string[] array = text.Split('*');
			if (array.Length > 1)
			{
				infinityValue = 1f;
				string[] array2 = array;
				foreach (string s in array2)
				{
					float result;
					if (!float.TryParse(s, NumberStyles.Float, StaticSettings.Culture, out result))
					{
						validatedText = null;
						return false;
					}
					infinityValue *= result;
				}
				if (ValidateValue(infinityValue, out infinityValue, isExternalSet))
				{
					validatedText = GetValueString(infinityValue);
					_valueNumber = infinityValue;
					return true;
				}
			}
			if (!float.TryParse(text, NumberStyles.Float, StaticSettings.Culture, out infinityValue) || !ValidateValue(infinityValue, out infinityValue, isExternalSet))
			{
				validatedText = null;
				return false;
			}
			_valueNumber = infinityValue;
			validatedText = GetValueString(infinityValue);
			return true;
		}

		protected virtual bool ValidateValue(float newValue, out float validatedValue, bool isExternalSet = false)
		{
			if (isExternalSet || StatMaster.KeyMapper.disableSliderLimits)
			{
				validatedValue = newValue;
				return true;
			}
			if (float.IsNaN(newValue))
			{
				validatedValue = float.NaN;
				return false;
			}
			newValue = Mathf.Clamp(newValue, minValue, maxValue);
			if (MaxDecimals > Decimals && !StatMaster.KeyMapper.disableSliderLimits)
			{
				newValue = (float)Math.Round(newValue, MaxDecimals);
			}
			validatedValue = newValue;
			return true;
		}

		protected override string GetInputTextForEditing()
		{
			return GetValueString(ValueNumber);
		}

		protected virtual string GetValueString(float value)
		{
			if (float.IsNaN(value))
			{
				return "NaN";
			}
			if (float.IsInfinity(value))
			{
				return ((!float.IsPositiveInfinity(value)) ? "-" : string.Empty) + LocalisationManager.GetTranslation(2158);
			}
			NumberFormatInfo numberFormatInfo = StaticSettings.Culture.NumberFormat.Clone() as NumberFormatInfo;
			if (splitThousands)
			{
				numberFormatInfo.NumberGroupSeparator = ",";
			}
			return value.ToString("0.".PadRight(2 + Decimals, '0').PadRight(2 + MaxDecimals, '#'), numberFormatInfo);
		}

		protected override bool ShowPrefixAndSuffix(string text)
		{
			float infinityValue;
			return !IsInfinityText(text, out infinityValue);
		}

		protected override void CopySelection(string selection)
		{
			if (base.IsSelecting)
			{
				if (base.IsSelectingAll && selection == base.ValueText)
				{
					ReferenceMaster.Clipboard.value = _valueNumber;
					string valueText = (GUIUtility.systemCopyBuffer = base.ValueText + '\u200b');
					ReferenceMaster.Clipboard.valueText = valueText;
				}
				else
				{
					base.CopySelection(selection);
				}
			}
		}

		protected override void PasteSelection(string pastedText = null)
		{
			if (base.IsSelectingAll && ReferenceMaster.Clipboard.valueText == GUIUtility.systemCopyBuffer)
			{
				float validatedValue = ReferenceMaster.Clipboard.value;
				if (StatMaster.KeyMapper.disableSliderLimits || float.IsNaN(validatedValue) || ValidateValue(validatedValue, out validatedValue))
				{
					SetValue(validatedValue);
					OnValueChanged();
					base.IsFocused = true;
				}
			}
			else if (base.IsSelectingAll && GUIUtility.systemCopyBuffer == "NaN")
			{
				SetValue(float.NaN);
				OnValueChanged();
				base.IsFocused = true;
			}
			else
			{
				base.PasteSelection(pastedText);
			}
		}

		private bool IsInfinityText(string t, out float infinityValue)
		{
			if (!t.EndsWith(LocalisationManager.GetTranslation(2158)))
			{
				infinityValue = float.NaN;
				return false;
			}
			infinityValue = ((!t.StartsWith("-")) ? float.PositiveInfinity : float.NegativeInfinity);
			return true;
		}

		public override void ResetDelegate()
		{
			base.ResetDelegate();
			this.ValueChanged = null;
			base.TextChanged += delegate
			{
				OnValueChanged();
			};
		}
	}
}
