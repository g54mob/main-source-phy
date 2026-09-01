using Cpp2ILInjected;
using TMPro;
using UnityEngine;

public class SetTextFromFile : MonoBehaviour
{
	private TextAsset _textAsset;

	private TMP_Text _textMesh;

	private void Start()
	{
		//IL_002b: Expected I, but got O
		//IL_003b: Expected O, but got I
		//IL_004b: Expected O, but got I
		TMP_Text textMesh = _textMesh;
		string text = _textAsset.text;
		nint num = (nint)textMesh;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v48 @ r8_v1 (Il2CppClass<TMPro.TMP_Text>)+558]");
		object obj = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v48 @ r8_v1 (Il2CppClass<TMPro.TMP_Text>)+560]");
		object obj2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v51 @ r9_v1 (should have been resolved before IL gen)");
		/*Error: End of method reached without returning.*/;
	}
}
