using System.Collections.Generic;

public class SandboxInputFields
{
	public static List<SandboxInputField> m_InputFields = new List<SandboxInputField>();

	public static bool InputFieldHasFocus()
	{
		foreach (SandboxInputField inputField in m_InputFields)
		{
			if ((bool)inputField && inputField.gameObject.activeInHierarchy && inputField.m_InputField.isFocused)
			{
				return true;
			}
		}
		return false;
	}

	public static void UpdateForCurrentDevice()
	{
		foreach (SandboxInputField inputField in m_InputFields)
		{
			if ((bool)inputField && inputField.gameObject.activeInHierarchy)
			{
				inputField.UpdateForCurrentDevice();
			}
		}
	}
}
