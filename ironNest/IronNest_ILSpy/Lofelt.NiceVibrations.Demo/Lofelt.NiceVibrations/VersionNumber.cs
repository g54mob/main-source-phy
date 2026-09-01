using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.UI;

namespace Lofelt.NiceVibrations;

public class VersionNumber : MonoBehaviour
{
	public string Version;

	protected Text _text;

	protected virtual void Awake()
	{
		GameObject gameObject = base.gameObject;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1806D9540");
		Text text = default(Text);
		_text = text;
	}

	protected virtual void Start()
	{
		//IL_0075: Expected I, but got O
		//IL_0085: Expected O, but got I
		//IL_0095: Expected O, but got I
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3F568]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		while (true)
		{
			Text text = _text;
			string text2 = Version.Replace("-alpha.", "a");
			string text3 = text2.Replace("-beta.", "b");
			nint num = (nint)text;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v115 @ r8_v4 (Il2CppClass<UnityEngine.UI.Text>)+5E8]");
			object obj = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v115 @ r8_v4 (Il2CppClass<UnityEngine.UI.Text>)+5F0]");
			object obj2 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v97 @ r9_v4 (should have been resolved before IL gen)");
		}
	}

	public VersionNumber()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3F569]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		Version = "v3.3";
		base._002Ector();
	}
}
