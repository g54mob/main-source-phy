using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.Rendering;

namespace Shapes;

internal struct ShapeDrawCall
{
	public ShapeDrawState drawState;

	public MaterialPropertyBlock mpb;

	private bool usingOverrideMpb;

	public int count;

	public Matrix4x4 matrix;

	public Matrix4x4[] matrices;

	private bool instanced;

	public ShapeDrawCall(ShapeDrawState drawState, Matrix4x4 matrix, MaterialPropertyBlock mpbOverride = null)
	{
		//IL_0083: Expected O, but got F4
		this.drawState = (ShapeDrawState)drawState.mesh;
		count = 1;
		_ = drawState.submesh;
		bool flag = mpbOverride == null;
		instanced = false;
		bool flag2 = !flag;
		usingOverrideMpb = flag2;
		this.matrix = (Matrix4x4)matrix.m00;
		_ = matrix.m01;
		_ = matrix.m02;
		_ = matrix.m03;
		bool flag3 = mpbOverride != null;
		MaterialPropertyBlock materialPropertyBlock = mpbOverride;
		if (!flag3)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808CDF10");
			MaterialPropertyBlock materialPropertyBlock2 = default(MaterialPropertyBlock);
			materialPropertyBlock = materialPropertyBlock2;
		}
		mpb = materialPropertyBlock;
		matrices = null;
	}

	public ShapeDrawCall(ShapeDrawState drawState, int count, Matrix4x4[] matrices, MaterialPropertyBlock mpbOverride = null)
	{
		//IL_00ba: Expected O, but got I4
		this.drawState = (ShapeDrawState)drawState.mesh;
		this.count = count;
		_ = drawState.submesh;
		this.matrices = matrices;
		MaterialPropertyBlock materialPropertyBlock = default(MaterialPropertyBlock);
		bool flag = materialPropertyBlock == null;
		instanced = true;
		bool flag2 = !flag;
		usingOverrideMpb = flag2;
		bool flag3 = materialPropertyBlock != null;
		MaterialPropertyBlock materialPropertyBlock2 = materialPropertyBlock;
		if (!flag3)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808CDF10");
			materialPropertyBlock2 = materialPropertyBlock;
		}
		mpb = materialPropertyBlock2;
		matrix = (Matrix4x4)0;
		_ = 0;
		_ = 0;
		_ = 0;
	}

	public unsafe void AddToCommandBuffer(RasterCommandBuffer cmd)
	{
		//IL_00b9: Expected I4, but got O
		//IL_00b9: Expected O, but got I4
		//IL_00b9: Expected O, but got I
		//IL_0081: Expected O, but got I
		//IL_0081: Expected O, but got Ref
		int num = default(int);
		int num2 = default(int);
		MaterialPropertyBlock materialPropertyBlock = default(MaterialPropertyBlock);
		if (!instanced)
		{
			_ = 0;
			_ = matrix;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.ShapeDrawCall)+38]");
			_ = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.ShapeDrawCall)+48]");
			_ = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.ShapeDrawCall)+58]");
			_ = 0;
			ShapeDrawState mesh = drawState;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.ShapeDrawCall)+8]");
			object obj = default(object);
			cmd.DrawMesh((Mesh)mesh, (Matrix4x4)(&obj), (Material)0, num, num2, materialPropertyBlock);
		}
		else
		{
			ShapeDrawState mesh2 = drawState;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.ShapeDrawCall)+10]");
			nint num3 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.ShapeDrawCall)+8]");
			MaterialPropertyBlock properties = default(MaterialPropertyBlock);
			cmd.DrawMeshInstanced((Mesh)mesh2, (int)num3, (Material)0, num, (Matrix4x4[])num2, (int)materialPropertyBlock, properties);
		}
	}

	public unsafe void AddToCommandBuffer(CommandBuffer cmd)
	{
		//IL_00b9: Expected I4, but got O
		//IL_00b9: Expected O, but got I4
		//IL_00b9: Expected O, but got I
		//IL_0081: Expected O, but got I
		//IL_0081: Expected O, but got Ref
		int num = default(int);
		int num2 = default(int);
		MaterialPropertyBlock materialPropertyBlock = default(MaterialPropertyBlock);
		if (!instanced)
		{
			_ = 0;
			_ = matrix;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.ShapeDrawCall)+38]");
			_ = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.ShapeDrawCall)+48]");
			_ = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.ShapeDrawCall)+58]");
			_ = 0;
			ShapeDrawState mesh = drawState;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.ShapeDrawCall)+8]");
			object obj = default(object);
			cmd.DrawMesh((Mesh)mesh, (Matrix4x4)(&obj), (Material)0, num, num2, materialPropertyBlock);
		}
		else
		{
			ShapeDrawState mesh2 = drawState;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.ShapeDrawCall)+10]");
			nint num3 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.ShapeDrawCall)+8]");
			MaterialPropertyBlock properties = default(MaterialPropertyBlock);
			cmd.DrawMeshInstanced((Mesh)mesh2, (int)num3, (Material)0, num, (Matrix4x4[])num2, (int)materialPropertyBlock, properties);
		}
	}

	public void Cleanup()
	{
		//IL_0076: Expected O, but got I4
		if (usingOverrideMpb)
		{
			mpb = null;
		}
		else
		{
			mpb.Clear();
			ObjectPool<MaterialPropertyBlock>.Free(mpb);
		}
		if (instanced)
		{
			ArrayPool<Matrix4x4>.Free(matrices);
		}
		_ = 0;
		drawState = (ShapeDrawState)0;
	}
}
