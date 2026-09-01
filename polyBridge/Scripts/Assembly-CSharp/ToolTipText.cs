using UnityEngine;

public class ToolTipText : MonoBehaviour
{
	public BindingType m_BindingType;

	public BindingType m_SecondaryBindingType;

	public string m_RawLocalizationKey;

	public ToolTipLocalizationKey m_LocalizationKey;

	public GamepadButtonType m_GamepadButtonType;

	public bool m_PreserveBracesForGamepad;

	[TextArea]
	public string m_Text;

	public virtual string GetText()
	{
		if (m_LocalizationKey == ToolTipLocalizationKey.TOOLTIP_MISSING && string.IsNullOrEmpty(m_RawLocalizationKey))
		{
			return m_Text;
		}
		string text = ((m_LocalizationKey != ToolTipLocalizationKey.TOOLTIP_MISSING) ? Localize.Get(m_LocalizationKey.ToString()) : Localize.Get(m_RawLocalizationKey));
		if (!m_PreserveBracesForGamepad && (GameInput.GetActiveGameDevice() != GameDevice.KeyboardAndMouse || Game.IsRunningOnSteamDeck()))
		{
			int num = text.IndexOf('(');
			if (num == -1)
			{
				num = text.IndexOf('（');
			}
			if (num <= 0)
			{
				return text;
			}
			return text.Substring(0, num);
		}
		if (m_BindingType == BindingType.NONE)
		{
			return text;
		}
		Binding binding = Bindings.GetBinding(m_BindingType);
		Binding binding2 = Bindings.GetBinding(m_SecondaryBindingType);
		if (binding != null && binding2 == null)
		{
			string tooltipBindingString = binding.GetTooltipBindingString();
			if (m_LocalizationKey == ToolTipLocalizationKey.TOOLTIP_ROTATE_LEFT || m_LocalizationKey == ToolTipLocalizationKey.TOOLTIP_ROTATE_RIGHT)
			{
				return string.Format(text, tooltipBindingString, "Shift+" + tooltipBindingString, "Ctrl+" + tooltipBindingString).Replace("()", string.Empty);
			}
			return string.Format(text, binding.GetTooltipBindingString()).Replace("()", string.Empty);
		}
		if (binding != null && binding2 != null)
		{
			string tooltipBindingString2 = binding.GetTooltipBindingString();
			if (m_LocalizationKey == ToolTipLocalizationKey.TOOLTIP_ROTATE_LEFT || m_LocalizationKey == ToolTipLocalizationKey.TOOLTIP_ROTATE_RIGHT)
			{
				return string.Format(text, tooltipBindingString2, "Shift+" + tooltipBindingString2, "Ctrl+" + tooltipBindingString2).Replace("()", string.Empty);
			}
			return string.Format(text, binding.GetTooltipBindingString(), binding2.GetTooltipBindingString()).Replace("()", string.Empty);
		}
		return text;
	}
}
