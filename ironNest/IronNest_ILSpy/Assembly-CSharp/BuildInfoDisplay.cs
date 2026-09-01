using Cpp2ILInjected;
using TMPro;
using UnityEngine;

public class BuildInfoDisplay : MonoBehaviour
{
	private TMP_Text _text;

	private void Start()
	{
		string version = Application.version;
		Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
		object arg = default(object);
		string text = $"Version: {version} ({arg})";
		_text.text = text;
	}
}
