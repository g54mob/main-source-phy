using System;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using UnityEngine;

namespace MTAssets.UltimateLODSystem.MeshSimplifier.Internal;

internal class BlendShapeFrameContainer
{
	private readonly float frameWeight;

	private readonly ResizableArray<Vector3> deltaVertices;

	private readonly ResizableArray<Vector3> deltaNormals;

	private readonly ResizableArray<Vector3> deltaTangents;

	public BlendShapeFrameContainer(BlendShapeFrame frame)
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803797D0");
		frameWeight = frame.FrameWeight;
		ResizableArray<Vector3> resizableArray = new ResizableArray<Vector3>(frame.DeltaVertices);
		deltaVertices = resizableArray;
		ResizableArray<Vector3> resizableArray2 = new ResizableArray<Vector3>(frame.DeltaNormals);
		deltaNormals = resizableArray2;
		ResizableArray<Vector3> resizableArray3 = new ResizableArray<Vector3>(frame.DeltaTangents);
		deltaTangents = resizableArray3;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public unsafe void MoveVertexElement(int dst, int src)
	{
		//IL_0022: Expected O, but got Ref
		//IL_0044: Expected O, but got Ref
		//IL_0066: Expected O, but got Ref
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803BA490");
		object obj = default(object);
		deltaVertices.set_Item(dst, (Vector3)(&obj));
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803BA490");
		object obj2 = default(object);
		deltaNormals.set_Item(dst, (Vector3)(&obj2));
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803BA490");
		object obj3 = default(object);
		deltaTangents.set_Item(dst, (Vector3)(&obj3));
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public unsafe void InterpolateVertexAttributes(int dst, int i0, int i1, int i2, ref Vector3 barycentricCoord)
	{
		//IL_0008: Expected O, but got Ref
		//IL_001b: Expected O, but got Ref
		//IL_0035: Expected O, but got I
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Expected O, but got Unknown
		//IL_005d: Expected O, but got Ref
		//IL_0084: Expected O, but got I
		//IL_00a4: Expected O, but got Ref
		//IL_00bc: Expected O, but got Ref
		//IL_00d9: Expected O, but got I
		//IL_0116: Expected O, but got Ref
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_0135: Expected O, but got Unknown
		//IL_0148: Expected O, but got Ref
		//IL_016f: Expected O, but got I
		//IL_01b6: Expected O, but got I
		//IL_0363: Invalid comparison between O and F4
		//IL_01ff: Expected I, but got O
		//IL_0225: Expected O, but got I
		//IL_0388: Expected O, but got Ref
		//IL_0238: Expected O, but got Ref
		//IL_0252: Unknown result type (might be due to invalid IL or missing references)
		//IL_0257: Expected O, but got Unknown
		//IL_026a: Expected O, but got Ref
		//IL_0291: Expected O, but got I
		//IL_02d8: Expected O, but got I
		//IL_03c1: Invalid comparison between O and F4
		//IL_0321: Expected I, but got O
		//IL_0347: Expected O, but got I
		//IL_03e6: Expected O, but got Ref
		object obj2 = default(object);
		object obj = (object)(&obj2);
		object obj3 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 97));
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803BA490");
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v12 @ rbp_v1+77]");
		object obj4 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v12 @ rbp_v1-59]");
		object obj5 = 0 * obj4;
		object obj6 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 97));
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803BA490");
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v12 @ rbp_v1-59]");
		nint num = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v134 @ rdi_v4+4]");
		object obj7 = num * 0;
		object obj8 = obj7 + obj5;
		object obj9 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 97));
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803BA490");
		Vector3 value = (Vector3)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 81));
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v12 @ rbp_v1-59]");
		nint num2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v134 @ rdi_v4+8]");
		object obj10 = num2 * 0;
		object obj11 = obj10 + obj8;
		deltaVertices.set_Item(dst, value);
		object obj12 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 97));
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803BA490");
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v12 @ rbp_v1-59]");
		object obj13 = 0 * obj4;
		object obj14 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 97));
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803BA490");
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v12 @ rbp_v1-59]");
		nint num3 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v134 @ rdi_v4+4]");
		object obj15 = num3 * 0;
		object obj16 = obj15 + obj13;
		int index = (int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 97));
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803BA490");
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v12 @ rbp_v1-59]");
		nint num4 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v134 @ rdi_v4+8]");
		object obj17 = num4 * 0;
		object obj18 = obj17 + obj16;
		Vector3 vector = ((ResizableArray<Vector3>)null).get_Item(index);
		object obj19 = default(object);
		if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj19) > System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)1E-05f))
		{
			object obj20 = obj18 / obj19;
			object obj21 = obj20;
		}
		else
		{
			nint num5 = (nint)typeof(Vector3);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v526 @ rax_v29 (Il2CppClass<UnityEngine.Vector3>)+B8]");
			nint num6 = 0;
			_ = Vector3.zeroVector;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v527 @ rcx_v25 (Il2CppStaticFields<UnityEngine.Vector3>)+8]");
			object obj21 = 0;
		}
		Vector3 value2 = (Vector3)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 65));
		deltaNormals.set_Item(dst, value2);
		object obj22 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 97));
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803BA490");
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v12 @ rbp_v1-59]");
		object obj23 = 0 * obj4;
		object obj24 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 97));
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803BA490");
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v134 @ rdi_v4+4]");
		nint num7 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v12 @ rbp_v1-59]");
		object obj25 = num7 * 0;
		object obj26 = obj25 + obj23;
		int index2 = (int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 97));
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803BA490");
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v134 @ rdi_v4+8]");
		nint num8 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v12 @ rbp_v1-59]");
		object obj27 = num8 * 0;
		object obj28 = obj27 + obj26;
		Vector3 vector2 = ((ResizableArray<Vector3>)null).get_Item(index2);
		if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj19) > System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)1E-05f))
		{
			object obj29 = obj28 / obj19;
			object obj30 = obj29;
		}
		else
		{
			nint num9 = (nint)typeof(Vector3);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v614 @ rax_v26 (Il2CppClass<UnityEngine.Vector3>)+B8]");
			nint num10 = 0;
			_ = Vector3.zeroVector;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v615 @ rcx_v23 (Il2CppStaticFields<UnityEngine.Vector3>)+8]");
			object obj30 = 0;
		}
		Vector3 value3 = (Vector3)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 81));
		deltaTangents.set_Item(dst, value3);
	}

	public void Resize(int length, bool trimExess = false)
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808F8090");
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808F8090");
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808F8090");
	}

	public unsafe BlendShapeFrame ToBlendShapeFrame()
	{
		//IL_007b: Expected native int or pointer, but got O
		//IL_0085: Expected native int or pointer, but got O
		//IL_0094: Expected native int or pointer, but got O
		//IL_00a1: Expected native int or pointer, but got O
		//IL_00b3: Expected native int or pointer, but got O
		//IL_00c0: Expected native int or pointer, but got O
		if (deltaVertices != null)
		{
			Vector3[] array = deltaVertices.ToArray();
			if (deltaNormals != null)
			{
				Vector3[] array2 = deltaNormals.ToArray();
				if (deltaTangents != null)
				{
					Vector3[] array3 = deltaTangents.ToArray();
					_ = 0;
					BlendShapeFrame blendShapeFrame = default(BlendShapeFrame);
					System.Runtime.CompilerServices.Unsafe.Write(&((BlendShapeFrame*)(nint)blendShapeFrame)->DeltaNormals, null);
					System.Runtime.CompilerServices.Unsafe.Write(&((BlendShapeFrame*)(nint)blendShapeFrame)->DeltaTangents, null);
					((BlendShapeFrame*)(nint)blendShapeFrame)->FrameWeight = frameWeight;
					System.Runtime.CompilerServices.Unsafe.Write(&((BlendShapeFrame*)(nint)blendShapeFrame)->DeltaVertices, array);
					System.Runtime.CompilerServices.Unsafe.Write(&((BlendShapeFrame*)(nint)blendShapeFrame)->DeltaNormals, array2);
					System.Runtime.CompilerServices.Unsafe.Write(&((BlendShapeFrame*)(nint)blendShapeFrame)->DeltaTangents, array3);
					return blendShapeFrame;
				}
			}
		}
		return (BlendShapeFrame)new NullReferenceException();
	}
}
