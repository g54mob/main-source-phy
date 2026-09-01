using Cpp2ILInjected;
using UnityEngine;

public class FootstepSurfaceMarker : MonoBehaviour
{
	public bool isSpecialSurface;

	public string debugName;

	public FootstepSurfaceMarker()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3A8CB]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		isSpecialSurface = true;
		debugName = "SpecialSurface";
		base._002Ector();
	}
}
