using System;
using System.Collections.Generic;
using Cpp2ILInjected;
using UnityEngine;

namespace Shapes;

public class ProceduralTree : ImmediateModeShapeDrawer
{
	public float lineThickness;

	public Color lineColor;

	public int seed;

	public int lineCount;

	public int branchesMin;

	public int branchesMax;

	public float branchLengthMin;

	public float branchLengthMax;

	public float maxAngDeviation;

	public bool use3D;

	private int currentLineCount;

	private readonly Queue<Matrix4x4> mtxQueue;

	public unsafe override void DrawShapes(Camera cam)
	{
		//IL_007b: Expected I, but got O
		//IL_008e: Expected I, but got O
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		//IL_011b: Expected O, but got Unknown
		//IL_0039: Expected I, but got O
		//IL_00a6: Expected I, but got O
		//IL_00b9: Expected I, but got O
		//IL_00cc: Expected I, but got O
		//IL_0188: Expected O, but got Ref
		DrawCommand drawCommand = Draw.Command(cam);
		Draw.ResetAllDrawStates();
		nint num = (nint)typeof(Draw);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v106 @ rax_v9 (Il2CppClass<Shapes.Draw>)+B8]");
		nint num2 = 0;
		_ = 2;
		nint num3 = (nint)typeof(Draw);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v140 @ rax_v13 (Il2CppClass<Shapes.Draw>)+B8]");
		nint num4 = 0;
		_ = lineThickness;
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"sbb edi,edi\"");
		object obj = cam & 2;
		nint num5 = (nint)typeof(Draw);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v190 @ rax_v20 (Il2CppClass<Shapes.Draw>)+B8]");
		nint num6 = 0;
		nint num7 = (nint)typeof(Draw);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v221 @ rax_v23 (Il2CppClass<Shapes.Draw>)+B8]");
		nint num8 = 0;
		_ = 0;
		nint num9 = (nint)typeof(Draw);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v255 @ rax_v27 (Il2CppClass<Shapes.Draw>)+B8]");
		nint num10 = 0;
		_ = lineColor;
		UnityEngine.Random.InitState(seed);
		currentLineCount = 0;
		nint num11 = (nint)typeof(Draw);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v291 @ rax_v32 (Il2CppClass<Shapes.Draw>)+B8]");
		nint num12 = 0;
		Matrix4x4 matrix4x = default(Matrix4x4);
		BranchFrom((Matrix4x4)(&matrix4x));
		object obj2 = default(object);
		if (obj2 != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
		}
	}

	private unsafe void BranchFrom(Matrix4x4 mtx)
	{
		//IL_0047: Expected I, but got O
		//IL_0065: Expected O, but got F4
		//IL_0095: Invalid comparison between I4 and F4
		//IL_00e1: Expected I, but got O
		//IL_0104: Expected I, but got O
		//IL_014f: Expected O, but got Ref
		//IL_014f: Expected O, but got Ref
		//IL_018a: Expected O, but got F4
		//IL_019c: Expected O, but got I4
		//IL_0303: Expected O, but got Ref
		//IL_01d7: Expected O, but got F4
		//IL_01e0: Invalid comparison between I4 and F4
		//IL_022b: Expected F4, but got I4
		//IL_03f2: Expected O, but got Ref
		//IL_026d: Expected O, but got I4
		//IL_0248: Expected O, but got I4
		//IL_02cd: Expected O, but got Ref
		//IL_0405: Unknown result type (might be due to invalid IL or missing references)
		//IL_040a: Expected O, but got Unknown
		int num = currentLineCount + 1;
		currentLineCount = num;
		if (currentLineCount >= lineCount)
		{
			return;
		}
		nint num2 = (nint)typeof(Draw);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v345 @ rax_v8 (Il2CppClass<Shapes.Draw>)+B8]");
		nint num3 = 0;
		Draw.matrix = (Matrix4x4)mtx.m00;
		_ = mtx.m01;
		_ = mtx.m02;
		_ = mtx.m03;
		float value = UnityEngine.Random.value;
		if (0f > value || value > 1f)
		{
		}
		nint num4 = (nint)typeof(Draw);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v474 @ rax_v15 (Il2CppClass<Shapes.Draw>)+B8]");
		nint num5 = 0;
		nint num6 = (nint)typeof(Draw);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v506 @ rax_v18 (Il2CppClass<Shapes.Draw>)+B8]");
		nint num7 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v475 @ rcx_v15 (Il2CppStaticFields<Shapes.Draw>)+19C]");
		nint num8 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v507 @ rcx_v18 (Il2CppStaticFields<Shapes.Draw>)+190]");
		Vector3 vector = default(Vector3);
		float num9 = default(float);
		Color colorStart = default(Color);
		Color colorEnd = default(Color);
		float thickness = default(float);
		Draw.Line_Internal((LineEndCap)num8, ThicknessSpace.Meters, (Vector3)(&vector), (Vector3)(&num9), colorStart, colorEnd, thickness);
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180F9BD60");
		int maxExclusive = branchesMax + 1;
		int num10 = UnityEngine.Random.Range(branchesMin, maxExclusive);
		float num11 = default(float);
		vector = (Vector3)num11;
		num9 = num11;
		object obj = 0;
		object obj4 = default(object);
		MatrixStack matrixStack = default(MatrixStack);
		object obj5 = default(object);
		while (true)
		{
			if ((nint)obj < num10)
			{
				MatrixStack matrixScope = Draw.MatrixScope;
				float num12 = ShapesMath.RandomGaussian();
				object obj2 = maxAngDeviation ^ -0f;
				if (!(0f > num12))
				{
					if (num12 > 1f)
					{
						num12 = 1f;
					}
				}
				else
				{
					num12 = 0f;
				}
				float num13 = maxAngDeviation - (float)obj2;
				float num14 = num13 * num12;
				float num15 = num14 + (float)obj2;
				if (!use3D)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18106D5C0");
					object obj3 = 0;
					float num16 = num15;
				}
				else
				{
					Vector3 randomPerpendicularVector = ShapesMath.GetRandomPerpendicularVector((Vector3)(&vector));
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18106D630");
					object obj3 = 0;
					vector = Vector3.upVector;
					num9 = randomPerpendicularVector.x;
					float num16 = num15;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180F9BE20");
				if (mtxQueue == null)
				{
					break;
				}
				mtxQueue.Enqueue((Matrix4x4)(&obj4));
				matrixStack.Dispose();
				obj++;
				continue;
			}
			Queue<Matrix4x4> queue = mtxQueue;
			while (true)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v203 @ rax_v37 (System.Collections.Generic.Queue`1<UnityEngine.Matrix4x4>)+20]");
				if ((nint)0 > (nint)0)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808DFE00");
					BranchFrom((Matrix4x4)(&obj5));
					queue = mtxQueue;
					continue;
				}
				break;
			}
			return;
		}
		throw new NullReferenceException();
	}

	public ProceduralTree()
	{
		//IL_001e: Expected O, but got I
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C60]");
		lineColor = (Color)0;
		lineThickness = 0.1f;
		lineCount = 100;
		branchesMin = 1;
		branchesMax = 5;
		branchLengthMin = 0.25f;
		branchLengthMax = 1f;
		maxAngDeviation = (float)Math.PI / 3f;
		Queue<Matrix4x4> queue = new Queue<Matrix4x4>();
		mtxQueue = queue;
		base._002Ector();
	}
}
