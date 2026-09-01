using System;
using System.Runtime.InteropServices;
using Cpp2ILInjected;

namespace MTAssets.UltimateLODSystem.MeshSimplifier;

[Serializable]
[StructLayout((LayoutKind)3)]
public struct SimplificationOptions
{
	public static readonly SimplificationOptions Default;

	public bool PreserveBorderEdges;

	public bool PreserveUVSeamEdges;

	public bool PreserveUVFoldoverEdges;

	public bool PreserveSurfaceCurvature;

	public bool EnableSmartLink;

	public double VertexLinkDistance;

	public int MaxIterationCount;

	public double Agressiveness;

	public bool ManualUVComponentCount;

	public int UVComponentCount;

	static SimplificationOptions()
	{
		//IL_0013: Expected I, but got O
		//IL_0037: Expected O, but got I4
		nint num = (nint)typeof(SimplificationOptions);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ rax_v3 (Il2CppClass<MTAssets.UltimateLODSystem.MeshSimplifier.SimplificationOptions>)+B8]");
		nint num2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"unpcklpd xmm0,xmm1\"");
		Default = (SimplificationOptions)0;
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"unpcklpd xmm1,xmm2\"");
		_ = 100;
		_ = 0;
	}
}
