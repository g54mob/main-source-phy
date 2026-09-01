using Cpp2ILInjected;
using UnityEngine;

namespace Kamgam.SettingsGenerator.Examples;

public class PostProFixer : MonoBehaviour
{
	public GameObject PostProVolumeParent;

	public string id;

	public PostProFixer()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3F15B]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		id = "";
		base._002Ector();
	}
}
