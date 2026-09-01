using Cpp2ILInjected;
using TMPro;
using UnityEngine;

public class CatNameSetting : MonoBehaviour
{
	private TMP_InputField Input;

	private CatCustomizationController catCustomization;

	private void OnEnable()
	{
		//IL_00ae: Expected O, but got I
		//IL_00be: Expected O, but got I
		//IL_00e0: Expected O, but got I
		//IL_00f0: Expected O, but got I
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3A880]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B502D0]");
		object obj = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v31 @ rax_v2+B8]");
		object defaultValue = 0;
		string text = PlayerPrefs.GetString("cat", (string)defaultValue);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B502D0]");
		object obj2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v37 @ rcx_v3+B8]");
		object obj3 = 0;
		bool flag = text == (string)obj3;
		bool flag2 = !flag;
		string text2 = text;
		if (!flag2)
		{
			PlayerPrefs.SetString("cat", "Hastur");
			CatCustomizationController catCustomizationController = catCustomization;
			catCustomizationController.catName = "Hastur";
			text2 = "Hastur";
		}
		CatCustomizationController catCustomizationController2 = catCustomization;
		catCustomizationController2.catName = text2;
		Input.text = text2;
	}

	public void Save()
	{
		//IL_00c6: Expected O, but got I
		//IL_00d6: Expected O, but got I
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3A881]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B502D0]");
		object obj = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v27 @ rax_v2+B8]");
		object defaultValue = 0;
		string text = PlayerPrefs.GetString("cat", (string)defaultValue);
		TMP_InputField input = Input;
		if (input.m_Text != text)
		{
			TMP_InputField input2 = Input;
			PlayerPrefs.SetString("cat", input2.m_Text);
			TMP_InputField input3 = Input;
			CatCustomizationController catCustomizationController = catCustomization;
			catCustomizationController.catName = input3.m_Text;
		}
	}
}
