using Cpp2ILInjected;
using TMPro;
using UnityEngine;

public class DropdownSample : MonoBehaviour
{
	private TextMeshProUGUI text;

	private TMP_Dropdown dropdownWithoutPlaceholder;

	private TMP_Dropdown dropdownWithPlaceholder;

	public void OnButtonClick()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B39C41]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		TMP_Dropdown tMP_Dropdown = dropdownWithPlaceholder;
		string text4;
		if (tMP_Dropdown.m_Value > -1)
		{
			int num = default(int);
			string text = num.ToString();
			string text2 = num.ToString();
			string text3 = "Selected values:\n" + text + " - " + text2;
			text4 = text3;
		}
		else
		{
			text4 = "Error: Please make a selection";
		}
		this.text.text = text4;
	}
}
