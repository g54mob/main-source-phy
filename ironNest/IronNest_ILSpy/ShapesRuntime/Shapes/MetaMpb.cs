using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using UnityEngine;

namespace Shapes;

internal abstract class MetaMpb : IDisposable
{
	private bool initialized;

	private int instanceCount;

	private ShapeDrawState drawState;

	public MaterialPropertyBlock mpbOverride;

	private Matrix4x4[] matrices;

	private bool directMaterialApply;

	internal List<Vector4> color;

	private ShapeDrawCall sdc;

	public bool HasContent => initialized;

	private bool HasMultipleInstances
	{
		get
		{
			//IL_0010: Expected O, but got I4
			//IL_002a: Unknown result type (might be due to invalid IL or missing references)
			//IL_002f: Expected I4, but got Unknown
			object obj = instanceCount - 1;
			int num = instanceCount ^ 1;
			int num2 = instanceCount ^ obj;
			int num3 = num & num2;
			bool flag = num3 < 0;
			bool flag2 = (nint)obj < 0;
			bool flag3 = obj == null;
			bool flag4 = flag2 == flag;
			bool flag5 = !flag3;
			return flag5 & flag4;
		}
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	internal unsafe static void ApplyColorOrFill<T>(T fillable, Color baseColor)
	{
		//IL_0008: Expected O, but got Ref
		//IL_03d0: Expected I, but got O
		//IL_001b: Expected O, but got Ref
		//IL_0188: Expected O, but got Ref
		//IL_0056: Expected O, but got Ref
		//IL_0078: Expected O, but got I
		//IL_01c6: Expected O, but got Ref
		//IL_01e8: Expected O, but got I
		//IL_0256: Expected O, but got I
		//IL_00fd: Expected O, but got Ref
		//IL_012d: Expected O, but got Ref
		//IL_028a: Expected O, but got Ref
		//IL_015d: Expected O, but got Ref
		//IL_02aa: Expected O, but got Ref
		//IL_02de: Expected O, but got Ref
		//IL_031c: Expected O, but got Ref
		//IL_0385: Expected O, but got Ref
		object obj2 = default(object);
		object obj = (object)(&obj2);
		_ = 0;
		_ = 0;
		_ = 0;
		_ = 0;
		_ = 0;
		nint num = (nint)typeof(Draw);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v109 @ rcx_v11 (Il2CppClass<Shapes.Draw>)+B8]");
		nint num2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v55 @ rax_v3 (Il2CppStaticFields<Shapes.Draw>)+134]");
		if ((nint)0 == 0)
		{
			object obj3 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 73));
			_ = baseColor.r;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18105E070");
			Vector4 item = (Vector4)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 73));
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-49]");
			_ = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [fillable @ rcx (T)+48]");
			((List<Vector4>)0).Add(item);
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
			float item2 = (float)(ref obj2) + 127f;
			_ = 3212836864L;
			List<float> list = default(List<float>);
			list.Add(item2);
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
			float item3 = (float)(ref obj2) + 127f;
			_ = 0;
			List<float> list2 = default(List<float>);
			list2.Add(item3);
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
			Vector4 item4 = (Vector4)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 55));
			_ = 0;
			List<Vector4> list3 = default(List<Vector4>);
			list3.Add(item4);
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
			Vector4 item5 = (Vector4)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 41));
			_ = 0;
			List<Vector4> list4 = default(List<Vector4>);
			list4.Add(item5);
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
			Vector4 item6 = (Vector4)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 57));
			_ = 0;
			List<Vector4> list5 = default(List<Vector4>);
			list5.Add(item6);
			return;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v109 @ rcx_v11 (Il2CppClass<Shapes.Draw>)+B8]");
		nint num3 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v107 @ rax_v18 (Il2CppStaticFields<Shapes.Draw>)+138]");
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v107 @ rax_v18 (Il2CppStaticFields<Shapes.Draw>)+148]");
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v107 @ rax_v18 (Il2CppStaticFields<Shapes.Draw>)+158]");
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v107 @ rax_v18 (Il2CppStaticFields<Shapes.Draw>)+168]");
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v107 @ rax_v18 (Il2CppStaticFields<Shapes.Draw>)+178]");
		_ = 0;
		object obj4 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 57));
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-11]");
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18105E070");
		Vector4 item7 = (Vector4)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 57));
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-49]");
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [fillable @ rcx (T)+48]");
		((List<Vector4>)0).Add(item7);
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
		float item8 = (float)(ref obj2) + 127f;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-19]");
		_ = 0;
		List<float> list6 = default(List<float>);
		list6.Add(item8);
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
		float item9 = (float)(ref obj2) + 127f;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-19]");
		object obj5 = (nint)0 >> 32;
		List<float> list7 = default(List<float>);
		list7.Add(item9);
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
		GradientFill gradientFill = (GradientFill)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 25));
		Vector4 shaderStartVector = ((GradientFill*)gradientFill)->GetShaderStartVector();
		Vector4 item10 = (Vector4)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 41));
		_ = shaderStartVector.x;
		List<Vector4> list8 = default(List<Vector4>);
		list8.Add(item10);
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
		object obj6 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 57));
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-1]");
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18105E070");
		Vector4 item11 = (Vector4)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 57));
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-49]");
		_ = 0;
		List<Vector4> list9 = default(List<Vector4>);
		list9.Add(item11);
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1+1B]");
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1+23]");
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1+1F]");
		_ = 0;
		_ = 0;
		Vector4 item12 = (Vector4)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 41));
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-49]");
		_ = 0;
		List<Vector4> list10 = default(List<Vector4>);
		list10.Add(item12);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	internal unsafe static void ApplyDashSettings<T>(T dashable, float thickness)
	{
		//IL_0018: Expected I, but got O
		//IL_006d: Expected I, but got O
		//IL_008d: Expected F4, but got I
		//IL_00cd: Expected I, but got O
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		//IL_0126: Expected O, but got Unknown
		//IL_020a: Expected O, but got I
		//IL_02a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ab: Expected O, but got Unknown
		_ = 0;
		_ = 0;
		_ = 0;
		nint num = (nint)typeof(Draw);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v87 @ rcx_v5 (Il2CppClass<Shapes.Draw>)+B8]");
		nint num2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v91 @ rax_v6 (Il2CppStaticFields<Shapes.Draw>)+114]");
		bool flag = (nint)0 == 0;
		float num3 = thickness;
		object obj = default(object);
		float item7;
		List<float> list7;
		if (!flag)
		{
			nint num4 = (nint)typeof(Draw);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v118 @ rcx_v25 (Il2CppClass<Shapes.Draw>)+B8]");
			nint num5 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v116 @ rax_v28 (Il2CppStaticFields<Shapes.Draw>)+124]");
			num3 = 0f;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v116 @ rax_v28 (Il2CppStaticFields<Shapes.Draw>)+124]");
			if ((nint)0 > (nint)0)
			{
				nint num6 = (nint)typeof(Draw);
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v524 @ rax_v32 (Il2CppClass<Shapes.Draw>)+B8]");
				nint num7 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v525 @ rax_v33 (Il2CppStaticFields<Shapes.Draw>)+118]");
				_ = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v525 @ rax_v33 (Il2CppStaticFields<Shapes.Draw>)+128]");
				_ = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v525 @ rax_v33 (Il2CppStaticFields<Shapes.Draw>)+130]");
				_ = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
				DashStyle dashStyle = (DashStyle)(obj - 96);
				float netAbsoluteSize = ((DashStyle*)dashStyle)->GetNetAbsoluteSize(dashed: true, thickness);
				float item = (float)obj + 40f;
				List<float> list = default(List<float>);
				list.Add(item);
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
				float item2 = (float)obj + 40f;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v5 @ rsp-60]");
				_ = 0;
				List<float> list2 = default(List<float>);
				list2.Add(item2);
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
				float item3 = (float)obj + 40f;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v5 @ rsp-48]");
				_ = 0;
				List<float> list3 = default(List<float>);
				list3.Add(item3);
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
				float item4 = (float)obj + 40f;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v5 @ rsp-60]");
				object obj2 = (nint)0 >> 32;
				List<float> list4 = default(List<float>);
				list4.Add(item4);
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
				float item5 = (float)obj + 40f;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v5 @ rsp-58]");
				_ = 0;
				List<float> list5 = default(List<float>);
				list5.Add(item5);
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
				float item6 = (float)obj + 40f;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v5 @ rsp-4C]");
				_ = 0;
				List<float> list6 = default(List<float>);
				list6.Add(item6);
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
				DashStyle dashStyle2 = (DashStyle)(obj - 96);
				float netAbsoluteSpacing = ((DashStyle*)dashStyle2)->GetNetAbsoluteSpacing(dashed: true, thickness);
				item7 = (float)obj + 40f;
				List<float> list8 = default(List<float>);
				list7 = list8;
				goto IL_046e;
			}
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
		float item8 = (float)obj + 40f;
		_ = 0;
		List<float> list9 = default(List<float>);
		list9.Add(item8);
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
		float item9 = (float)obj + 40f;
		_ = 0;
		List<float> list10 = default(List<float>);
		list10.Add(item9);
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
		float item10 = (float)obj + 40f;
		_ = 0;
		List<float> list11 = default(List<float>);
		list11.Add(item10);
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
		float item11 = (float)obj + 40f;
		_ = 0;
		List<float> list12 = default(List<float>);
		list12.Add(item11);
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
		float item12 = (float)obj + 40f;
		_ = 0;
		List<float> list13 = default(List<float>);
		list13.Add(item12);
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
		float item13 = (float)obj + 40f;
		_ = 0;
		List<float> list14 = default(List<float>);
		list14.Add(item13);
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
		item7 = (float)obj + 40f;
		_ = 0;
		List<float> list15 = default(List<float>);
		list7 = list15;
		goto IL_046e;
		IL_046e:
		list7.Add(item7);
	}

	internal static List<T> InitList<T>()
	{
		return new List<T>(1023);
	}

	protected abstract void TransferShapeProperties();

	protected unsafe void Transfer(int propertyID, List<Vector4> listVec)
	{
		//IL_009c: Expected O, but got Ref
		//IL_009c: Expected O, but got I
		//IL_006a: Expected O, but got I
		//IL_004c: Expected O, but got Ref
		//IL_004c: Expected O, but got I
		//IL_0126: Expected O, but got I
		object obj = default(object);
		if (!directMaterialApply)
		{
			if (instanceCount <= 1)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.MetaMpb)+68]");
				((MaterialPropertyBlock)0).SetVector(propertyID, (Vector4)(&obj));
			}
			else
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.MetaMpb)+68]");
				((MaterialPropertyBlock)0).SetVectorArray(propertyID, listVec);
			}
		}
		else
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.MetaMpb)+20]");
			((Material)0).SetVector(propertyID, (Vector4)(&obj));
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [listVec @ r8 (System.Collections.Generic.List`1<UnityEngine.Vector4>)+1C]");
		_ = (nint)0 + (nint)1;
		if (!RuntimeHelpers.IsReferenceOrContainsReferences<Vector4>())
		{
			_ = 0;
			return;
		}
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [listVec @ r8 (System.Collections.Generic.List`1<UnityEngine.Vector4>)+18]");
		if ((nint)0 > (nint)0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [listVec @ r8 (System.Collections.Generic.List`1<UnityEngine.Vector4>)+10]");
			nint num = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [listVec @ r8 (System.Collections.Generic.List`1<UnityEngine.Vector4>)+18]");
			Array.Clear((Array)num, 0, 0);
		}
	}

	protected void Transfer(int propertyID, List<float> listFloat)
	{
		//IL_009c: Expected O, but got I
		//IL_006a: Expected O, but got I
		//IL_004c: Expected O, but got I
		//IL_0126: Expected O, but got I
		float value = default(float);
		if (!directMaterialApply)
		{
			if (instanceCount <= 1)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.MetaMpb)+68]");
				((MaterialPropertyBlock)0).SetFloatImpl(propertyID, value);
			}
			else
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.MetaMpb)+68]");
				((MaterialPropertyBlock)0).SetFloatArray(propertyID, listFloat);
			}
		}
		else
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.MetaMpb)+20]");
			((Material)0).SetFloat(propertyID, value);
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [listFloat @ r8 (System.Collections.Generic.List`1<System.Single>)+1C]");
		_ = (nint)0 + (nint)1;
		if (!RuntimeHelpers.IsReferenceOrContainsReferences<float>())
		{
			_ = 0;
			return;
		}
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [listFloat @ r8 (System.Collections.Generic.List`1<System.Single>)+18]");
		if ((nint)0 > (nint)0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [listFloat @ r8 (System.Collections.Generic.List`1<System.Single>)+10]");
			nint num = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [listFloat @ r8 (System.Collections.Generic.List`1<System.Single>)+18]");
			Array.Clear((Array)num, 0, 0);
		}
	}

	protected unsafe void Transfer(int propertyID, ref Texture tex)
	{
		//IL_005b: Expected O, but got I
		//IL_003c: Expected O, but got I
		if (!directMaterialApply)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.MetaMpb)+68]");
			((MaterialPropertyBlock)0).SetTextureImpl(propertyID, tex);
		}
		else
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.MetaMpb)+20]");
			((Material)0).SetTexture(propertyID, tex);
		}
		ref Texture reference = ref *(Texture*)null;
	}

	public unsafe bool PreAppendCheck(ShapeDrawState additionDrawState, Matrix4x4 mtx)
	{
		//IL_015b: Expected I4, but got O
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Expected O, but got Unknown
		//IL_0062: Expected O, but got Ref
		bool result;
		if (initialized)
		{
			bool flag = instanceCount >= 1023;
			result = false;
			if (!flag)
			{
				ShapeDrawState shapeDrawState = (ShapeDrawState)(this + 24);
				object obj = default(object);
				bool flag2 = ((ShapeDrawState*)shapeDrawState)->CompatibleWith((ShapeDrawState)(&obj));
				bool flag3 = !flag2;
				result = false;
				if (!flag3)
				{
					goto IL_00b6;
				}
			}
			goto IL_0148;
		}
		initialized = true;
		drawState = (ShapeDrawState)additionDrawState.mesh;
		_ = additionDrawState.submesh;
		goto IL_00b6;
		IL_00b6:
		Matrix4x4[] array = matrices;
		int num = instanceCount + 1;
		instanceCount = num;
		if (instanceCount < array.Length)
		{
			int num2 = instanceCount << 6;
			_ = mtx.m00;
			_ = mtx.m01;
			_ = mtx.m02;
			_ = mtx.m03;
			result = true;
			goto IL_0148;
		}
		IndexOutOfRangeException ex = new IndexOutOfRangeException();
		return (byte)(int)ex != 0;
		IL_0148:
		return result;
	}

	public unsafe ShapeDrawCall ExtractDrawCall()
	{
		//IL_0008: Expected O, but got Ref
		//IL_0029: Expected I, but got O
		//IL_002e: Expected I, but got O
		//IL_003e: Expected O, but got I
		//IL_00be: Expected O, but got I4
		//IL_007a: Expected O, but got I
		//IL_00b0: Expected O, but got I4
		//IL_037d: Expected O, but got Ref
		//IL_0184: Expected O, but got I4
		//IL_019f: Expected native int or pointer, but got O
		//IL_01c1: Expected native int or pointer, but got O
		//IL_020a: Expected native int or pointer, but got O
		//IL_0293: Expected O, but got Ref
		//IL_02a7: Expected O, but got Ref
		//IL_02dd: Expected O, but got I
		object obj2 = default(object);
		object obj = (object)(&obj2);
		bool flag;
		if (mpbOverride == null)
		{
			flag = false;
			goto IL_0236;
		}
		nint num = (nint)typeof(MpbCustomMesh);
		nint num2 = (nint)this;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v52 @ rdx_v12 (Il2CppClass<Shapes.MpbCustomMesh>)+130]");
		object obj3 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v53 @ r8_v6 (Il2CppClass<Shapes.MetaMpb>)+130]");
		nint num3 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v52 @ rdx_v12 (Il2CppClass<Shapes.MpbCustomMesh>)+130]");
		object obj5;
		if (num3 >= 0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v53 @ r8_v6 (Il2CppClass<Shapes.MetaMpb>)+C8]");
			object obj4 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v111 @ rax_v29+FFFFFFF8+v54 @ rax_v23*8]");
			if (0 == (nint)typeof(MpbCustomMesh))
			{
				obj5 = 1;
				goto IL_0255;
			}
		}
		obj5 = 0;
		goto IL_0255;
		IL_0255:
		bool flag2 = obj5 == null;
		MetaMpb metaMpb = null;
		if (!flag2)
		{
			metaMpb = this;
		}
		bool flag3 = metaMpb == null;
		flag = !flag3;
		goto IL_0236;
		IL_0236:
		if (instanceCount <= 1)
		{
			Matrix4x4[] array = matrices;
			if (matrices == null)
			{
				return (ShapeDrawCall)new NullReferenceException();
			}
			MaterialPropertyBlock materialPropertyBlock = ((!flag) ? null : mpbOverride);
			Matrix4x4 matrix = (Matrix4x4)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 48));
			_ = 0;
			ShapeDrawState shapeDrawState = (ShapeDrawState)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 80));
			_ = 0;
			_ = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v137 @ rax_v17 (UnityEngine.Matrix4x4[])+30]");
			_ = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v137 @ rax_v17 (UnityEngine.Matrix4x4[])+20]");
			_ = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v137 @ rax_v17 (UnityEngine.Matrix4x4[])+50]");
			obj = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v137 @ rax_v17 (UnityEngine.Matrix4x4[])+40]");
			_ = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rdx (Shapes.MetaMpb)+28]");
			_ = 0;
			_ = drawState;
			ShapeDrawCall shapeDrawCall = new ShapeDrawCall(shapeDrawState, matrix, materialPropertyBlock);
			sdc = shapeDrawCall;
			_ = 0;
			_ = 0;
			_ = 0;
			_ = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-80]");
			_ = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-70]");
			_ = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-60]");
			_ = 0;
		}
		else
		{
			if (flag)
			{
			}
			ShapeDrawState shapeDrawState2 = (ShapeDrawState)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 80));
			_ = 0;
			_ = 0;
			_ = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rdx (Shapes.MetaMpb)+28]");
			_ = 0;
			_ = drawState;
			MaterialPropertyBlock materialPropertyBlock2 = default(MaterialPropertyBlock);
			ShapeDrawCall shapeDrawCall = new ShapeDrawCall(shapeDrawState2, instanceCount, matrices, materialPropertyBlock2);
			sdc = shapeDrawCall;
			_ = 0;
			_ = 0;
			_ = 0;
			_ = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-80]");
			_ = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-70]");
			_ = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-60]");
			_ = 0;
			Matrix4x4[] array2 = ArrayPool<Matrix4x4>.Alloc(1023);
			matrices = array2;
		}
		if (!flag)
		{
			TransferAllProperties();
		}
		initialized = false;
		drawState = (ShapeDrawState)0;
		_ = 0;
		instanceCount = 0;
		ShapeDrawCall shapeDrawCall2 = default(ShapeDrawCall);
		System.Runtime.CompilerServices.Unsafe.Write(&((ShapeDrawCall*)(nint)shapeDrawCall2)->drawState, (ShapeDrawState)sdc);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rdx (Shapes.MetaMpb)+60]");
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rdx (Shapes.MetaMpb)+70]");
		((ShapeDrawCall*)(nint)shapeDrawCall2)->usingOverrideMpb = false;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rdx (Shapes.MetaMpb)+80]");
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rdx (Shapes.MetaMpb)+90]");
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rdx (Shapes.MetaMpb)+A0]");
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rdx (Shapes.MetaMpb)+B0]");
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rdx (Shapes.MetaMpb)+C0]");
		((ShapeDrawCall*)(nint)shapeDrawCall2)->instanced = false;
		return shapeDrawCall2;
	}

	public void ApplyDirectlyToMaterial()
	{
		//IL_0032: Expected O, but got I4
		directMaterialApply = true;
		TransferAllProperties();
		directMaterialApply = false;
		initialized = false;
		drawState = (ShapeDrawState)0;
		_ = 0;
		instanceCount = 0;
	}

	internal void TransferAllProperties()
	{
		//IL_02ea: Expected O, but got I4
		//IL_0013: Expected I, but got O
		//IL_0018: Expected I, but got O
		//IL_0028: Expected O, but got I
		//IL_009c: Expected I, but got O
		//IL_00ac: Expected O, but got I
		//IL_0064: Expected O, but got I
		//IL_00e8: Expected O, but got I
		//IL_010c: Expected O, but got I
		//IL_01bd: Expected O, but got I4
		List<Vector4> list;
		if (this != null)
		{
			nint num = (nint)typeof(MpbCustomMesh);
			nint num2 = (nint)this;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v41 @ rdx_v47 (Il2CppClass<Shapes.MpbCustomMesh>)+130]");
			object obj = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v42 @ r8_v31 (Il2CppClass<Shapes.MetaMpb>)+130]");
			nint num3 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v41 @ rdx_v47 (Il2CppClass<Shapes.MpbCustomMesh>)+130]");
			if (num3 >= 0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v42 @ r8_v31 (Il2CppClass<Shapes.MetaMpb>)+C8]");
				object obj2 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v112 @ rax_v59+FFFFFFF8+v43 @ rax_v55*8]");
				if (0 == (nint)typeof(MpbCustomMesh))
				{
					return;
				}
			}
			nint num4 = (nint)typeof(MpbText);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v58 @ rdx_v48 (Il2CppClass<Shapes.MpbText>)+130]");
			object obj3 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v42 @ r8_v31 (Il2CppClass<Shapes.MetaMpb>)+130]");
			nint num5 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v58 @ rdx_v48 (Il2CppClass<Shapes.MpbText>)+130]");
			if (num5 >= 0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v42 @ r8_v31 (Il2CppClass<Shapes.MetaMpb>)+C8]");
				object obj4 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v60 @ rax_v58+FFFFFFF8+v61 @ rax_v57*8]");
				bool flag = 0 == (nint)typeof(MpbText);
				list = (List<Vector4>)num2;
				if (flag)
				{
					goto IL_011f;
				}
			}
		}
		list = color;
		Transfer(ShapesMaterialUtils.propColor, color);
		object obj5 = 0;
		goto IL_011f;
		IL_011f:
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
		object obj6 = default(object);
		if (obj6 != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
			List<float> listFloat = default(List<float>);
			Transfer(ShapesMaterialUtils.propFillType, listFloat);
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
			List<float> listFloat2 = default(List<float>);
			Transfer(ShapesMaterialUtils.propFillSpace, listFloat2);
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
			List<Vector4> listVec = default(List<Vector4>);
			Transfer(ShapesMaterialUtils.propFillStart, listVec);
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
			List<Vector4> listVec2 = default(List<Vector4>);
			Transfer(ShapesMaterialUtils.propColorEnd, listVec2);
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
			List<Vector4> list2 = default(List<Vector4>);
			Transfer(ShapesMaterialUtils.propFillEnd, list2);
			obj5 = 0;
			list = list2;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
		object obj7 = default(object);
		if (obj7 != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
			List<float> listFloat3 = default(List<float>);
			Transfer(ShapesMaterialUtils.propDashSize, listFloat3);
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
			List<float> listFloat4 = default(List<float>);
			Transfer(ShapesMaterialUtils.propDashType, listFloat4);
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
			List<float> listFloat5 = default(List<float>);
			Transfer(ShapesMaterialUtils.propDashShapeModifier, listFloat5);
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
			List<float> listFloat6 = default(List<float>);
			Transfer(ShapesMaterialUtils.propDashSpace, listFloat6);
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
			List<float> listFloat7 = default(List<float>);
			Transfer(ShapesMaterialUtils.propDashSnap, listFloat7);
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
			List<float> listFloat8 = default(List<float>);
			Transfer(ShapesMaterialUtils.propDashOffset, listFloat8);
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
			List<float> listFloat9 = default(List<float>);
			Transfer(ShapesMaterialUtils.propDashSpacing, listFloat9);
		}
		TransferShapeProperties();
	}

	public void Dispose()
	{
		//IL_0016: Expected O, but got I4
		initialized = false;
		drawState = (ShapeDrawState)0;
		_ = 0;
		instanceCount = 0;
	}

	protected MetaMpb()
	{
		Matrix4x4[] array = ArrayPool<Matrix4x4>.Alloc(1023);
		matrices = array;
		List<Vector4> list = InitList<Vector4>();
		color = list;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803797D0");
	}
}
