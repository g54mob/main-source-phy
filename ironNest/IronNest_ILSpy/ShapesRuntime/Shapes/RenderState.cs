using System;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.Rendering;

namespace Shapes;

internal struct RenderState : IEquatable<RenderState>
{
	public Shader shader;

	public string[] keywords;

	public bool isTextMaterial;

	public CompareFunction zTest;

	public float zOffsetFactor;

	public int zOffsetUnits;

	public ColorWriteMask colorMask;

	public CompareFunction stencilComp;

	public StencilOp stencilOpPass;

	public byte stencilRefID;

	public byte stencilReadMask;

	public byte stencilWriteMask;

	public Material CreateMaterial()
	{
		Material material = new Material(shader);
		if ((object)material != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181D8CC90");
			if (isTextMaterial)
			{
			}
			material.SetInt(ShapesMaterialUtils.propZTest, (int)zTest);
			if (!isTextMaterial)
			{
				material.SetFloat(ShapesMaterialUtils.propZOffsetFactor, zOffsetFactor);
				material.SetInt(ShapesMaterialUtils.propZOffsetUnits, zOffsetUnits);
			}
			material.SetInt(ShapesMaterialUtils.propColorMask, (int)colorMask);
			material.SetInt(ShapesMaterialUtils.propStencilComp, (int)stencilComp);
			material.SetInt(ShapesMaterialUtils.propStencilOpPass, (int)stencilOpPass);
			int nameID = ((!isTextMaterial) ? ShapesMaterialUtils.propStencilID : ShapesMaterialUtils.propStencilIDTMP);
			material.SetInt(nameID, stencilRefID);
			material.SetInt(ShapesMaterialUtils.propStencilReadMask, stencilReadMask);
			material.SetInt(ShapesMaterialUtils.propStencilWriteMask, stencilWriteMask);
			material.enableInstancing = true;
			UnityEngine.Object.DontDestroyOnLoad(material);
			return material;
		}
		return (Material)(object)new NullReferenceException();
	}

	private static bool StrArrEquals(string[] a, string[] b)
	{
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Expected O, but got Unknown
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Expected O, but got Unknown
		//IL_00b1: Expected O, but got I4
		//IL_00ba: Expected O, but got I4
		//IL_0198: Expected I4, but got O
		//IL_00f3: Expected O, but got I
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Expected O, but got Unknown
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Expected O, but got Unknown
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		//IL_012f: Expected O, but got Unknown
		if (a != null && b != null)
		{
			if (a.Length == b.Length)
			{
				if (a.Length <= 0)
				{
					goto IL_014d;
				}
				object obj = b + 24;
				object obj2 = a + 32;
				object obj3 = (object)b - (object)a;
				object obj4 = 0;
				object obj5 = 0;
				while (true)
				{
					if ((nint)obj5 < a.Length && System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj5) < System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj))
					{
						object obj6 = obj2;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v149 @ r14_v7+v85 @ rsi_v6]");
						if (!((string)obj6 == (string)0))
						{
							break;
						}
						obj5++;
						obj4++;
						obj2 += 8;
						if ((nint)obj4 < a.Length)
						{
							continue;
						}
						goto IL_014d;
					}
					IndexOutOfRangeException ex = new IndexOutOfRangeException();
					return (byte)(int)ex != 0;
				}
			}
			return false;
		}
		object obj7 = (object)a - (object)b;
		return obj7 == null;
		IL_014d:
		return true;
	}

	public unsafe bool Equals(RenderState other)
	{
		//IL_01de: Expected O, but got I4
		//IL_021e: Expected Ref, but got F4
		//IL_024e: Expected O, but got F4
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Expected O, but got Unknown
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Expected O, but got Unknown
		//IL_0104: Expected O, but got I4
		//IL_010d: Expected O, but got I4
		//IL_03a1: Expected I4, but got O
		//IL_0146: Expected O, but got I
		//IL_0161: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Expected O, but got Unknown
		//IL_016f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0174: Expected O, but got Unknown
		//IL_017d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0182: Expected O, but got Unknown
		//IL_0369: Expected O, but got I4
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180D12FD0");
		object obj = default(object);
		if (obj != null)
		{
			string[] array = keywords;
			string[] array2 = other.keywords;
			if (keywords != null && other.keywords != null)
			{
				if (array.Length == array2.Length)
				{
					if (array.Length <= 0)
					{
						goto IL_01cb;
					}
					object obj2 = keywords + 32;
					object obj3 = other.keywords + 24;
					object obj4 = (object)other.keywords - (object)keywords;
					object obj5 = 0;
					object obj6 = 0;
					while (true)
					{
						if ((nint)obj6 < array.Length && System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj6) < System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj3))
						{
							object obj7 = obj2;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v62 @ r14_v6+v83 @ r13_v6]");
							if (!((string)obj7 == (string)0))
							{
								break;
							}
							obj6++;
							obj5++;
							obj2 += 8;
							if ((nint)obj5 < array.Length)
							{
								continue;
							}
							goto IL_01cb;
						}
						IndexOutOfRangeException ex = new IndexOutOfRangeException();
						return (byte)(int)ex != 0;
					}
				}
			}
			else if (keywords == other.keywords)
			{
				goto IL_01cb;
			}
		}
		goto IL_0385;
		IL_0385:
		return false;
		IL_01cb:
		object obj8 = (other.isTextMaterial ? 1 : 0) >> 32;
		if ((nint)zTest == (nint)obj8)
		{
			float num = (float)(ref this) + 24f;
			if (((float*)num)->Equals(other.zOffsetFactor))
			{
				object obj9 = other.zOffsetFactor >> 32;
				if (zOffsetUnits == (nint)obj9 && colorMask == other.colorMask)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"psrldq xmm0,4\"");
					if (stencilComp == (CompareFunction)other.colorMask && stencilOpPass == other.stencilOpPass)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"psrldq xmm0,0Ch\"");
						if ((ColorWriteMask)stencilRefID == other.colorMask)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"psrldq xmm0,0Dh\"");
							if ((ColorWriteMask)stencilReadMask == other.colorMask)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"psrldq xmm0,0Eh\"");
								object obj10 = stencilWriteMask - other.colorMask;
								return obj10 == null;
							}
						}
					}
				}
			}
		}
		goto IL_0385;
	}

	public unsafe override bool Equals(object obj)
	{
		//IL_0013: Expected I, but got O
		//IL_0057: Expected I, but got O
		//IL_009a: Expected O, but got Ref
		if (obj != null)
		{
			nint num = (nint)typeof(RenderState);
			bool flag = (object)obj.GetType() != typeof(RenderState);
			object obj2 = null;
			if (!flag)
			{
				obj2 = obj;
			}
			if (obj2 != null)
			{
				nint num2 = (nint)obj;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v130 @ rcx_v2 (Il2CppClass<System.Object>)+40]");
				nint num3 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v40 @ rdx_v2 (Il2CppClass<Shapes.RenderState>)+40]");
				if (num3 == 0)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_object_unbox\"");
					object obj3 = default(object);
					return Equals((RenderState)(&obj3));
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
				bool result = default(bool);
				return result;
			}
		}
		return false;
	}

	public unsafe override int GetHashCode()
	{
		//IL_0112: Expected O, but got I4
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Expected O, but got Unknown
		//IL_0137: Expected Ref, but got F4
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Expected O, but got Unknown
		//IL_0151: Unknown result type (might be due to invalid IL or missing references)
		//IL_0156: Expected I4, but got Unknown
		//IL_0164: Expected O, but got I4
		//IL_016e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0173: Expected I4, but got Unknown
		//IL_0181: Expected O, but got I4
		//IL_018b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0190: Expected O, but got Unknown
		//IL_0199: Unknown result type (might be due to invalid IL or missing references)
		//IL_019e: Expected O, but got Unknown
		//IL_01a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ad: Expected O, but got Unknown
		//IL_01b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bb: Expected O, but got Unknown
		//IL_01c6: Expected O, but got Ref
		//IL_01d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d5: Expected O, but got Unknown
		//IL_01ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f2: Expected O, but got Unknown
		//IL_020a: Expected O, but got Ref
		//IL_021d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0222: Expected O, but got Unknown
		//IL_023a: Expected O, but got Ref
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Expected O, but got Unknown
		//IL_008b: Expected O, but got I4
		//IL_0094: Expected O, but got I4
		//IL_0252: Unknown result type (might be due to invalid IL or missing references)
		//IL_0257: Expected O, but got Unknown
		//IL_0264: Expected I4, but got O
		//IL_029b: Expected I4, but got O
		//IL_00ff: Expected O, but got I4
		//IL_02a9: Expected O, but got I4
		//IL_02b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b7: Expected O, but got Unknown
		//IL_02c4: Expected I4, but got O
		//IL_02cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d2: Expected O, but got Unknown
		int num;
		if (this.shader != null)
		{
			Shader shader = this.shader;
			int hashCode = shader.GetHashCode();
			num = hashCode;
		}
		else
		{
			num = 0;
		}
		if (keywords != null)
		{
			string[] array = keywords;
			object obj = keywords + 32;
			object obj2 = 0;
			object obj3 = 0;
			object obj6 = default(object);
			while ((nint)obj3 < array.Length)
			{
				if ((nint)obj2 < array.Length)
				{
					object obj4 = obj;
					if (obj != null)
					{
						object obj5 = obj4;
						Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v267 @ rdx_v11+158] (should have been resolved before IL gen)");
					}
					else
					{
						obj6 = 0;
					}
					object obj7 = num * 397;
					obj2++;
					num = obj7 ^ obj6;
					obj += 8;
					obj3 = obj2;
					continue;
				}
				IndexOutOfRangeException ex = new IndexOutOfRangeException();
				return (int)ex;
			}
		}
		object obj8 = num * 397;
		float num2 = (float)(ref this) + 24f;
		object obj9 = obj8 ^ zTest;
		int hashCode2 = ((float*)num2)->GetHashCode();
		object obj10 = obj9 * 397;
		int num3 = hashCode2 ^ obj10;
		object obj11 = num3 * 397;
		int num4 = obj11 ^ zOffsetUnits;
		object obj12 = num4 * 397;
		object obj13 = obj12 ^ colorMask;
		object obj14 = obj13 * 397;
		object obj15 = obj14 ^ stencilComp;
		object obj16 = obj15 * 397;
		object obj17 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref this, 44));
		object obj18 = obj16 ^ stencilOpPass;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180D49A10");
		object obj19 = obj18 * 397;
		object obj21 = default(object);
		object obj20 = obj21 ^ obj19;
		object obj22 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref this, 45));
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180D49A10");
		object obj23 = obj20 * 397;
		object obj25 = default(object);
		object obj24 = obj25 ^ obj23;
		object obj26 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref this, 46));
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180D49A10");
		object obj27 = obj24 * 397;
		object obj28 = default(object);
		return obj28 ^ obj27;
	}
}
