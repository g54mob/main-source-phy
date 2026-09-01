using System.Threading.Tasks;
using Cpp2ILInjected;
using TMPro;
using UnityEngine;

public class UILeaderboardUsernameSetting : MonoBehaviour
{
	public TMP_InputField Input;

	private void OnEnable()
	{
		//IL_0053: Expected O, but got I
		//IL_0063: Expected O, but got I
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3AD27]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B502D0]");
		object obj = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v27 @ rax_v2+B8]");
		object defaultValue = 0;
		string text = PlayerPrefs.GetString("username", (string)defaultValue);
		Input.text = text;
	}

	public void Save()
	{
		//IL_007b: Expected O, but got I
		//IL_008b: Expected O, but got I
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B502D0]");
		object obj = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v29 @ rax_v2+B8]");
		object defaultValue = 0;
		string text = PlayerPrefs.GetString("username", (string)defaultValue);
		TMP_InputField input = Input;
		if (input.m_Text != text)
		{
			TMP_InputField input2 = Input;
			PlayerPrefs.SetString("username", input2.m_Text);
			Task task = LeaderboardManager.Instance.RegisterUser();
		}
	}
}
