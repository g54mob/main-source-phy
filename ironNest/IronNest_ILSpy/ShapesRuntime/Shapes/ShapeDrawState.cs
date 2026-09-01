using Cpp2ILInjected;
using UnityEngine;

namespace Shapes;

internal struct ShapeDrawState
{
	public Mesh mesh;

	public Material mat;

	public int submesh;

	internal bool CompatibleWith(ShapeDrawState other)
	{
		if (!(mesh == other.mesh) || submesh != other.submesh)
		{
			return false;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"psrldq xmm1,8\"");
		return mat == other.mesh;
	}
}
