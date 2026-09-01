using Cpp2ILInjected;
using TMPro;
using UnityEngine;

namespace Shapes;

public class ShapesTextPool : ShapesObjPool<TextMeshProShapes, ShapesTextPool>
{
	public override string PoolTypeName
	{
		get
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B426B7]");
			if ((nint)0 == 0)
			{
				_ = 1;
			}
			return "Text";
		}
	}

	public override void OnCreatedNewComponent(TextMeshProShapes comp)
	{
		comp.textWrappingMode = TextWrappingModes.NoWrap;
		comp.overflowMode = TextOverflowModes.Overflow;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
		Renderer renderer = default(Renderer);
		renderer.enabled = false;
	}
}
