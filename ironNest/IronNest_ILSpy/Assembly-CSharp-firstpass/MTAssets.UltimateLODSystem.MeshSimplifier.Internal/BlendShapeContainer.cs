using System;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using UnityEngine;

namespace MTAssets.UltimateLODSystem.MeshSimplifier.Internal;

internal class BlendShapeContainer
{
	private readonly string shapeName;

	private readonly BlendShapeFrameContainer[] frames;

	public BlendShapeContainer(BlendShape blendShape)
	{
		//IL_026f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0274: Expected O, but got Unknown
		//IL_006f: Expected O, but got I4
		//IL_00c1: Expected O, but got I4
		//IL_02dc: Expected F4, but got I
		//IL_02ed: Expected O, but got I
		//IL_00e8: Expected O, but got I
		//IL_0115: Expected O, but got I
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_0137: Expected O, but got Unknown
		//IL_0179: Expected I, but got O
		//IL_0189: Expected O, but got I
		//IL_019e: Expected O, but got I
		//IL_01e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e6: Expected O, but got Unknown
		//IL_01ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f4: Expected O, but got Unknown
		//IL_01fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0202: Expected O, but got Unknown
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803797D0");
		ResizableArray<Vector3> resizableArray = (ResizableArray<Vector3>)(this + 16);
		shapeName = blendShape.ShapeName;
		BlendShapeFrame[] array = blendShape.Frames;
		bool flag = blendShape.Frames == null;
		Vector3[] array2 = (Vector3[])(object)blendShape.Frames;
		if (!flag)
		{
			BlendShapeFrameContainer[] array3 = (frames = new BlendShapeFrameContainer[array.Length]);
			BlendShapeFrameContainer[] array4 = frames;
			bool flag2 = frames == null;
			array2 = (Vector3[])(object)array3;
			resizableArray = null;
			if (!flag2)
			{
				BlendShapeFrame[] array5 = blendShape.Frames;
				object obj = 32;
				ResizableArray<Vector3> resizableArray2 = null;
				ResizableArray<Vector3> resizableArray3 = null;
				array2 = (Vector3[])(object)array3;
				resizableArray = null;
				ResizableArray<Vector3> resizableArray5 = default(ResizableArray<Vector3>);
				object obj2 = default(object);
				while (true)
				{
					if ((nint)resizableArray < array4.Length)
					{
						BlendShapeFrameContainer[] array6 = frames;
						if (blendShape.Frames == null)
						{
							break;
						}
						BlendShapeFrameContainer blendShapeFrameContainer = new BlendShapeFrameContainer((BlendShapeFrame)0);
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v120 @ r14_v5 (MTAssets.UltimateLODSystem.MeshSimplifier.BlendShapeFrame[])+20+v93 @ r15_v5 (MTAssets.UltimateLODSystem.MeshSimplifier.ResizableArray`1<UnityEngine.Vector3>)]");
						blendShapeFrameContainer.frameWeight = 0f;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v120 @ r14_v5 (MTAssets.UltimateLODSystem.MeshSimplifier.BlendShapeFrame[])+20+v93 @ r15_v5 (MTAssets.UltimateLODSystem.MeshSimplifier.ResizableArray`1<UnityEngine.Vector3>)]");
						ResizableArray<Vector3> deltaVertices = new ResizableArray<Vector3>((Vector3[])0);
						Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"psrldq xmm6,8\"");
						blendShapeFrameContainer.deltaVertices = deltaVertices;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v120 @ r14_v5 (MTAssets.UltimateLODSystem.MeshSimplifier.BlendShapeFrame[])+30+v93 @ r15_v5 (MTAssets.UltimateLODSystem.MeshSimplifier.ResizableArray`1<UnityEngine.Vector3>)]");
						ResizableArray<Vector3> deltaNormals = new ResizableArray<Vector3>((Vector3[])0);
						blendShapeFrameContainer.deltaNormals = deltaNormals;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v120 @ r14_v5 (MTAssets.UltimateLODSystem.MeshSimplifier.BlendShapeFrame[])+30+v93 @ r15_v5 (MTAssets.UltimateLODSystem.MeshSimplifier.ResizableArray`1<UnityEngine.Vector3>)]");
						ResizableArray<Vector3> resizableArray4 = new ResizableArray<Vector3>((Vector3[])0);
						Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"psrldq xmm7,8\"");
						resizableArray = (ResizableArray<Vector3>)(blendShapeFrameContainer + 40);
						blendShapeFrameContainer.deltaTangents = resizableArray4;
						bool flag3 = frames == null;
						nint num = 0;
						array2 = (Vector3[])(object)resizableArray4;
						if (flag3)
						{
							break;
						}
						nint num2 = (nint)array6;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v360 @ rdx_v18 (Il2CppClass<MTAssets.UltimateLODSystem.MeshSimplifier.Internal.BlendShapeFrameContainer[]>)+40]");
						array2 = (Vector3[])0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v360 @ rdx_v18 (Il2CppClass<MTAssets.UltimateLODSystem.MeshSimplifier.Internal.BlendShapeFrameContainer[]>)+40]");
						((ResizableArray<Vector3>)(object)blendShapeFrameContainer)._002Ector((Vector3[])0);
						bool flag4 = resizableArray5 == null;
						num = 0;
						resizableArray = (ResizableArray<Vector3>)(object)blendShapeFrameContainer;
						if (!flag4)
						{
							array4 = frames;
							resizableArray3 = (ResizableArray<Vector3>)(resizableArray3 + 1);
							obj += 8;
							resizableArray2 = (ResizableArray<Vector3>)(resizableArray2 + 32);
							bool flag5 = frames == null;
							num = 0;
							array2 = (Vector3[])(object)blendShapeFrameContainer;
							resizableArray = resizableArray3;
							if (flag5)
							{
								break;
							}
							num = 0;
							array2 = (Vector3[])(object)blendShapeFrameContainer;
							resizableArray = resizableArray3;
							continue;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
						throw obj2;
					}
					return;
				}
			}
		}
		throw new NullReferenceException();
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public unsafe void MoveVertexElement(int dst, int src)
	{
		//IL_0035: Expected I, but got O
		//IL_0059: Expected O, but got I4
		//IL_005e: Expected I, but got O
		//IL_0063: Expected I, but got O
		//IL_00d2: Expected O, but got I
		//IL_064e: Expected O, but got I
		//IL_0127: Expected O, but got Ref
		//IL_0127: Expected O, but got I
		//IL_0163: Expected O, but got I
		//IL_019a: Expected O, but got Ref
		//IL_019a: Expected O, but got I
		//IL_01d6: Expected O, but got I
		//IL_020d: Expected O, but got Ref
		//IL_020d: Expected O, but got I
		//IL_0233: Unknown result type (might be due to invalid IL or missing references)
		//IL_0238: Expected O, but got Unknown
		//IL_0269: Expected O, but got I
		//IL_02af: Expected O, but got I
		BlendShapeFrameContainer[] array = frames;
		bool flag = frames == null;
		int num2 = default(int);
		int num = num2;
		int num3 = src;
		BlendShapeContainer blendShapeContainer = this;
		nint num4 = unchecked((nint)null);
		if (!flag)
		{
			num3 = src;
			blendShapeContainer = this;
			object obj = 32;
			num4 = unchecked((nint)null);
			nint num5 = unchecked((nint)null);
			object obj3 = default(object);
			object obj4 = default(object);
			object obj5 = default(object);
			object obj6 = default(object);
			object obj7 = default(object);
			object obj8 = default(object);
			while (true)
			{
				if (num4 < array.Length)
				{
					BlendShapeFrameContainer[] array2 = frames;
					bool flag2 = frames == null;
					num = num2;
					if (flag2)
					{
						break;
					}
					bool flag3 = num5 >= array2.Length;
					num = num2;
					if (!flag3)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v97 @ rsi_v8+v94 @ rbx_v9 (MTAssets.UltimateLODSystem.MeshSimplifier.Internal.BlendShapeFrameContainer[])]");
						object obj2 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v97 @ rsi_v8+v94 @ rbx_v9 (MTAssets.UltimateLODSystem.MeshSimplifier.Internal.BlendShapeFrameContainer[])]");
						bool flag4 = (nint)0 == 0;
						num = num2;
						if (flag4)
						{
							break;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v95 @ rbx_v10+18]");
						bool flag5 = (nint)0 == 0;
						num = num2;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v95 @ rbx_v10+18]");
						blendShapeContainer = (BlendShapeContainer)0;
						if (flag5)
						{
							break;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803BA490");
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v95 @ rbx_v10+18]");
						((ResizableArray<Vector3>)0).set_Item(num2, (Vector3)(&obj3));
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v95 @ rbx_v10+20]");
						bool flag6 = (nint)0 == 0;
						num = num2;
						num3 = (int)(&obj3);
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v95 @ rbx_v10+20]");
						blendShapeContainer = (BlendShapeContainer)0;
						num4 = 0;
						if (!flag6)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803BA490");
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v95 @ rbx_v10+20]");
							((ResizableArray<Vector3>)0).set_Item(num2, (Vector3)(&obj4));
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v95 @ rbx_v10+28]");
							bool flag7 = (nint)0 == 0;
							num = num2;
							num3 = (int)(&obj4);
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v95 @ rbx_v10+28]");
							blendShapeContainer = (BlendShapeContainer)0;
							num4 = 0;
							if (!flag7)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803BA490");
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v95 @ rbx_v10+28]");
								((ResizableArray<Vector3>)0).set_Item(num2, (Vector3)(&obj5));
								array = frames;
								num5++;
								obj += 8;
								bool flag8 = frames == null;
								num = num2;
								num3 = (int)(&obj5);
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v95 @ rbx_v10+28]");
								blendShapeContainer = (BlendShapeContainer)0;
								num4 = num5;
								if (flag8)
								{
									break;
								}
								obj5 = obj6;
								obj4 = obj7;
								obj3 = obj8;
								num3 = (int)(&obj5);
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v95 @ rbx_v10+28]");
								blendShapeContainer = (BlendShapeContainer)0;
								num4 = num5;
								continue;
							}
							throw new NullReferenceException();
						}
						throw new NullReferenceException();
					}
					throw new IndexOutOfRangeException();
				}
				return;
			}
		}
		throw new NullReferenceException();
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void InterpolateVertexAttributes(int dst, int i0, int i1, int i2, ref Vector3 barycentricCoord)
	{
		//IL_0018: Expected O, but got I4
		//IL_002b: Expected O, but got I4
		//IL_0034: Expected O, but got I4
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Expected O, but got Unknown
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Expected O, but got Unknown
		BlendShapeFrameContainer[] array = frames;
		object obj = 32;
		BlendShapeFrameContainer[] array2 = frames;
		object obj2 = 0;
		object obj3 = 0;
		while ((nint)obj2 < array.Length)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803B8CA0");
			obj3++;
			obj += 8;
			obj2 = obj3;
			array = frames;
		}
	}

	public void Resize(int length, bool trimExess = false)
	{
		//IL_0018: Expected O, but got I4
		//IL_0021: Expected O, but got I4
		//IL_002a: Expected O, but got I4
		//IL_004e: Expected O, but got I
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Expected O, but got Unknown
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Expected O, but got Unknown
		BlendShapeFrameContainer[] array = frames;
		object obj = 32;
		object obj2 = 0;
		object obj3 = 0;
		while ((nint)obj2 < array.Length)
		{
			BlendShapeFrameContainer[] array2 = frames;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v62 @ rsi_v4+v143 @ rax_v7 (MTAssets.UltimateLODSystem.MeshSimplifier.Internal.BlendShapeFrameContainer[])]");
			object obj4 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808F8090");
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808F8090");
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808F8090");
			array = frames;
			obj3++;
			obj += 8;
			obj2 = obj3;
		}
	}

	public unsafe BlendShape ToBlendShape()
	{
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Expected O, but got Unknown
		//IL_0030: Expected O, but got I4
		//IL_0039: Expected O, but got I4
		//IL_0042: Expected O, but got I4
		//IL_015e: Expected native int or pointer, but got O
		//IL_0170: Expected native int or pointer, but got O
		//IL_017d: Expected native int or pointer, but got O
		//IL_008f: Expected O, but got I
		//IL_00aa: Expected O, but got I
		//IL_00c4: Expected O, but got I
		//IL_00de: Expected O, but got I
		//IL_0118: Expected O, but got I
		//IL_0126: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Expected O, but got Unknown
		//IL_0134: Unknown result type (might be due to invalid IL or missing references)
		//IL_0139: Expected O, but got Unknown
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_0147: Expected O, but got Unknown
		BlendShapeFrameContainer[] array = frames;
		BlendShapeFrame[] array2 = new BlendShapeFrame[array.Length];
		object obj = array2 + 32;
		object obj2 = 32;
		object obj3 = 0;
		object obj4 = 0;
		BlendShapeContainer blendShapeContainer = this;
		BlendShape blendShape = default(BlendShape);
		while (true)
		{
			if ((nint)obj4 < array2.Length)
			{
				BlendShapeFrameContainer[] array3 = blendShapeContainer.frames;
				if ((nint)obj3 >= array3.Length)
				{
					break;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v87 @ r15_v5+v82 @ rdi_v6 (MTAssets.UltimateLODSystem.MeshSimplifier.Internal.BlendShapeFrameContainer[])]");
				object obj5 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v83 @ rdi_v7+18]");
				Vector3[] array4 = ((ResizableArray<Vector3>)0).ToArray();
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v83 @ rdi_v7+20]");
				Vector3[] array5 = ((ResizableArray<Vector3>)0).ToArray();
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v83 @ rdi_v7+28]");
				Vector3[] array6 = ((ResizableArray<Vector3>)0).ToArray();
				if ((nint)obj3 >= array2.Length)
				{
					break;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v83 @ rdi_v7+10]");
				obj = 0;
				obj3++;
				obj2 += 8;
				obj += 32;
				obj4 = obj3;
				blendShapeContainer = this;
				continue;
			}
			System.Runtime.CompilerServices.Unsafe.Write(&((BlendShape*)(nint)blendShape)->Frames, null);
			System.Runtime.CompilerServices.Unsafe.Write(&((BlendShape*)(nint)blendShape)->ShapeName, blendShapeContainer.shapeName);
			System.Runtime.CompilerServices.Unsafe.Write(&((BlendShape*)(nint)blendShape)->Frames, array2);
			return blendShape;
		}
		return (BlendShape)new IndexOutOfRangeException();
	}
}
