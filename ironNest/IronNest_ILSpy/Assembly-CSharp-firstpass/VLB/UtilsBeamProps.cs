using System;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using UnityEngine;

namespace VLB;

public static class UtilsBeamProps
{
	public static bool CanChangeDuringPlaytime(VolumetricLightBeamAbstractBase self)
	{
		//IL_0013: Expected I, but got O
		//IL_001b: Expected I, but got O
		//IL_002b: Expected O, but got I
		//IL_010a: Expected I4, but got O
		//IL_0067: Expected O, but got I
		//IL_008c: Expected O, but got I4
		bool flag = (object)self == null;
		UnityEngine.Object obj = null;
		UnityEngine.Object obj4;
		if (!flag)
		{
			nint num = (nint)typeof(VolumetricLightBeamSD);
			nint num2 = (nint)self;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v42 @ rdx_v3 (Il2CppClass<VLB.VolumetricLightBeamSD>)+130]");
			object obj2 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v43 @ r8_v2 (Il2CppClass<VLB.VolumetricLightBeamAbstractBase>)+130]");
			nint num3 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v42 @ rdx_v3 (Il2CppClass<VLB.VolumetricLightBeamSD>)+130]");
			if (num3 >= 0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v43 @ r8_v2 (Il2CppClass<VLB.VolumetricLightBeamAbstractBase>)+C8]");
				object obj3 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v99 @ rax_v14+FFFFFFF8+v44 @ rax_v10*8]");
				bool flag2 = 0 == (nint)typeof(VolumetricLightBeamSD);
				obj4 = (UnityEngine.Object)1;
				if (flag2)
				{
					goto IL_012c;
				}
			}
			obj4 = null;
			goto IL_012c;
		}
		goto IL_00b1;
		IL_012c:
		bool flag3 = (object)obj4 == null;
		obj = null;
		if (!flag3)
		{
			obj = self;
		}
		goto IL_00b1;
		IL_00b1:
		if (!obj)
		{
			return true;
		}
		if ((object)obj != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v57 @ rbx_v2 (UnityEngine.Object)+120]");
			return false;
		}
		NullReferenceException ex = new NullReferenceException();
		return (byte)(int)ex != 0;
	}

	public unsafe static Quaternion GetInternalLocalRotation(VolumetricLightBeamAbstractBase self)
	{
		//IL_001d: Expected I, but got O
		//IL_0025: Expected I, but got O
		//IL_0035: Expected O, but got I
		//IL_0071: Expected O, but got I
		//IL_0096: Expected O, but got I4
		//IL_0106: Expected I, but got O
		//IL_010e: Expected I, but got O
		//IL_011e: Expected O, but got I
		//IL_02c6: Expected native int or pointer, but got O
		//IL_015a: Expected O, but got I
		//IL_017f: Expected O, but got I4
		//IL_02b9: Expected F4, but got O
		UnityEngine.Object obj;
		if ((object)self == null)
		{
			obj = null;
			goto IL_00bb;
		}
		nint num = (nint)typeof(VolumetricLightBeamSD);
		nint num2 = (nint)self;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v54 @ rdx_v12 (Il2CppClass<VLB.VolumetricLightBeamSD>)+130]");
		object obj2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v55 @ r8_v10 (Il2CppClass<VLB.VolumetricLightBeamAbstractBase>)+130]");
		nint num3 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v54 @ rdx_v12 (Il2CppClass<VLB.VolumetricLightBeamSD>)+130]");
		UnityEngine.Object obj4;
		if (num3 >= 0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v55 @ r8_v10 (Il2CppClass<VLB.VolumetricLightBeamAbstractBase>)+C8]");
			object obj3 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v112 @ rax_v29+FFFFFFF8+v56 @ rax_v25*8]");
			bool flag = 0 == (nint)typeof(VolumetricLightBeamSD);
			obj4 = (UnityEngine.Object)1;
			if (flag)
			{
				goto IL_0267;
			}
		}
		obj4 = null;
		goto IL_0267;
		IL_02be:
		Quaternion quaternion = default(Quaternion);
		float x;
		((Quaternion*)(nint)quaternion)->x = x;
		return quaternion;
		IL_0234:
		return (Quaternion)new NullReferenceException();
		IL_01a4:
		UnityEngine.Object obj5;
		Quaternion beamInternalLocalRotation;
		if ((bool)obj5)
		{
			if ((object)obj5 != null)
			{
				beamInternalLocalRotation = ((VolumetricLightBeamHD)obj5).beamInternalLocalRotation;
				goto IL_01f3;
			}
			goto IL_0234;
		}
		x = (float)Quaternion.identityQuaternion;
		goto IL_02be;
		IL_0267:
		bool flag2 = (object)obj4 == null;
		obj = null;
		if (!flag2)
		{
			obj = self;
		}
		goto IL_00bb;
		IL_01f3:
		x = beamInternalLocalRotation.x;
		goto IL_02be;
		IL_028e:
		UnityEngine.Object obj6;
		bool flag3 = (object)obj6 == null;
		obj5 = null;
		if (!flag3)
		{
			obj5 = self;
		}
		goto IL_01a4;
		IL_00bb:
		if (!obj)
		{
			bool flag4 = (object)self == null;
			obj5 = null;
			if (!flag4)
			{
				nint num4 = (nint)typeof(VolumetricLightBeamHD);
				nint num5 = (nint)self;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v183 @ rdx_v10 (Il2CppClass<VLB.VolumetricLightBeamHD>)+130]");
				object obj7 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v184 @ r8_v8 (Il2CppClass<VLB.VolumetricLightBeamAbstractBase>)+130]");
				nint num6 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v183 @ rdx_v10 (Il2CppClass<VLB.VolumetricLightBeamHD>)+130]");
				if (num6 >= 0)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v184 @ r8_v8 (Il2CppClass<VLB.VolumetricLightBeamAbstractBase>)+C8]");
					object obj8 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v279 @ rax_v22+FFFFFFF8+v185 @ rax_v19*8]");
					bool flag5 = 0 == (nint)typeof(VolumetricLightBeamHD);
					obj6 = (UnityEngine.Object)1;
					if (flag5)
					{
						goto IL_028e;
					}
				}
				obj6 = null;
				goto IL_028e;
			}
			goto IL_01a4;
		}
		if ((object)obj != null)
		{
			beamInternalLocalRotation = ((VolumetricLightBeamSD)obj).beamInternalLocalRotation;
			goto IL_01f3;
		}
		goto IL_0234;
	}

	public static void SetIntensityFromLight(VolumetricLightBeamAbstractBase self, bool fromLight)
	{
		//IL_001d: Expected I, but got O
		//IL_0025: Expected I, but got O
		//IL_0035: Expected O, but got I
		//IL_0071: Expected O, but got I
		//IL_0096: Expected O, but got I4
		//IL_00f9: Expected I, but got O
		//IL_0101: Expected I, but got O
		//IL_0111: Expected O, but got I
		//IL_014d: Expected O, but got I
		//IL_0172: Expected O, but got I4
		UnityEngine.Object obj;
		if ((object)self == null)
		{
			obj = null;
			goto IL_00bb;
		}
		nint num = (nint)typeof(VolumetricLightBeamSD);
		nint num2 = (nint)self;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v54 @ rdx_v10 (Il2CppClass<VLB.VolumetricLightBeamSD>)+130]");
		object obj2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v55 @ r8_v8 (Il2CppClass<VLB.VolumetricLightBeamAbstractBase>)+130]");
		nint num3 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v54 @ rdx_v10 (Il2CppClass<VLB.VolumetricLightBeamSD>)+130]");
		UnityEngine.Object obj4;
		if (num3 >= 0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v55 @ r8_v8 (Il2CppClass<VLB.VolumetricLightBeamAbstractBase>)+C8]");
			object obj3 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v112 @ rax_v23+FFFFFFF8+v56 @ rax_v19*8]");
			bool flag = 0 == (nint)typeof(VolumetricLightBeamSD);
			obj4 = (UnityEngine.Object)1;
			if (flag)
			{
				goto IL_01f5;
			}
		}
		obj4 = null;
		goto IL_01f5;
		IL_0239:
		UnityEngine.Object obj5;
		bool flag2 = (object)obj5 == null;
		UnityEngine.Object obj6 = null;
		if (!flag2)
		{
			obj6 = self;
		}
		goto IL_0197;
		IL_0197:
		if ((bool)obj6)
		{
			((VolumetricLightBeamHD)obj6).useIntensityFromAttachedLightSpot = fromLight;
		}
		return;
		IL_00bb:
		if ((bool)obj)
		{
		}
		bool flag3 = (object)self == null;
		obj6 = null;
		if (!flag3)
		{
			nint num4 = (nint)typeof(VolumetricLightBeamHD);
			nint num5 = (nint)self;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v227 @ rdx_v7 (Il2CppClass<VLB.VolumetricLightBeamHD>)+130]");
			object obj7 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v228 @ r8_v5 (Il2CppClass<VLB.VolumetricLightBeamAbstractBase>)+130]");
			nint num6 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v227 @ rdx_v7 (Il2CppClass<VLB.VolumetricLightBeamHD>)+130]");
			if (num6 >= 0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v228 @ r8_v5 (Il2CppClass<VLB.VolumetricLightBeamAbstractBase>)+C8]");
				object obj8 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v282 @ rax_v14+FFFFFFF8+v229 @ rax_v11*8]");
				bool flag4 = 0 == (nint)typeof(VolumetricLightBeamHD);
				obj5 = (UnityEngine.Object)1;
				if (flag4)
				{
					goto IL_0239;
				}
			}
			obj5 = null;
			goto IL_0239;
		}
		goto IL_0197;
		IL_01f5:
		bool flag5 = (object)obj4 == null;
		obj = null;
		if (!flag5)
		{
			obj = self;
		}
		goto IL_00bb;
	}

	public static float GetThickness(VolumetricLightBeamAbstractBase self)
	{
		//IL_001d: Expected I, but got O
		//IL_0025: Expected I, but got O
		//IL_0035: Expected O, but got I
		//IL_0071: Expected O, but got I
		//IL_0096: Expected O, but got I4
		//IL_01f2: Expected O, but got I
		//IL_02ca: Invalid comparison between I4 and F4
		//IL_022e: Expected F4, but got I4
		//IL_0106: Expected I, but got O
		//IL_010e: Expected I, but got O
		//IL_011e: Expected O, but got I
		//IL_01dd: Expected O, but got I
		//IL_01c8: Expected F4, but got I4
		//IL_015a: Expected O, but got I
		//IL_017f: Expected O, but got I4
		UnityEngine.Object obj;
		if ((object)self == null)
		{
			obj = null;
			goto IL_00bb;
		}
		nint num = (nint)typeof(VolumetricLightBeamSD);
		nint num2 = (nint)self;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v51 @ rdx_v10 (Il2CppClass<VLB.VolumetricLightBeamSD>)+130]");
		object obj2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v52 @ r8_v8 (Il2CppClass<VLB.VolumetricLightBeamAbstractBase>)+130]");
		nint num3 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v51 @ rdx_v10 (Il2CppClass<VLB.VolumetricLightBeamSD>)+130]");
		UnityEngine.Object obj4;
		if (num3 >= 0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v52 @ r8_v8 (Il2CppClass<VLB.VolumetricLightBeamAbstractBase>)+C8]");
			object obj3 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v109 @ rax_v23+FFFFFFF8+v53 @ rax_v19*8]");
			bool flag = 0 == (nint)typeof(VolumetricLightBeamSD);
			obj4 = (UnityEngine.Object)1;
			if (flag)
			{
				goto IL_0258;
			}
		}
		obj4 = null;
		goto IL_0258;
		IL_00bb:
		UnityEngine.Object obj5;
		UnityEngine.Object obj8;
		if (!obj)
		{
			bool flag2 = (object)self == null;
			obj5 = null;
			if (!flag2)
			{
				nint num4 = (nint)typeof(VolumetricLightBeamHD);
				nint num5 = (nint)self;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v180 @ rdx_v8 (Il2CppClass<VLB.VolumetricLightBeamHD>)+130]");
				object obj6 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v181 @ r8_v6 (Il2CppClass<VLB.VolumetricLightBeamAbstractBase>)+130]");
				nint num6 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v180 @ rdx_v8 (Il2CppClass<VLB.VolumetricLightBeamHD>)+130]");
				if (num6 >= 0)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v181 @ r8_v6 (Il2CppClass<VLB.VolumetricLightBeamAbstractBase>)+C8]");
					object obj7 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v272 @ rax_v16+FFFFFFF8+v182 @ rax_v13*8]");
					bool flag3 = 0 == (nint)typeof(VolumetricLightBeamHD);
					obj8 = (UnityEngine.Object)1;
					if (flag3)
					{
						goto IL_027f;
					}
				}
				obj8 = null;
				goto IL_027f;
			}
			goto IL_01a4;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v68 @ rsi_v1 (UnityEngine.Object)+D4]");
		object obj9 = 0;
		goto IL_02a1;
		IL_01a4:
		if (!obj5)
		{
			return 0f;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v195 @ rbx_v6 (UnityEngine.Object)+88]");
		obj9 = 0;
		goto IL_02a1;
		IL_02a1:
		float num7 = (float)obj9 / 10f;
		float num8 = 1f - num7;
		if (!(0f > num8))
		{
			if (num8 > 1f)
			{
				return 1f;
			}
		}
		else
		{
			num8 = 0f;
		}
		return num8;
		IL_0258:
		bool flag4 = (object)obj4 == null;
		obj = null;
		if (!flag4)
		{
			obj = self;
		}
		goto IL_00bb;
		IL_027f:
		bool flag5 = (object)obj8 == null;
		obj5 = null;
		if (!flag5)
		{
			obj5 = self;
		}
		goto IL_01a4;
	}

	public static void SetThickness(VolumetricLightBeamAbstractBase self, float value)
	{
		//IL_001d: Expected I, but got O
		//IL_0025: Expected I, but got O
		//IL_0035: Expected O, but got I
		//IL_0071: Expected O, but got I
		//IL_0096: Expected O, but got I4
		//IL_0106: Expected I, but got O
		//IL_010e: Expected I, but got O
		//IL_011e: Expected O, but got I
		//IL_015a: Expected O, but got I
		//IL_017f: Expected O, but got I4
		UnityEngine.Object obj;
		if ((object)self == null)
		{
			obj = null;
			goto IL_00bb;
		}
		nint num = (nint)typeof(VolumetricLightBeamSD);
		nint num2 = (nint)self;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v54 @ rdx_v9 (Il2CppClass<VLB.VolumetricLightBeamSD>)+130]");
		object obj2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v55 @ r8_v8 (Il2CppClass<VLB.VolumetricLightBeamAbstractBase>)+130]");
		nint num3 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v54 @ rdx_v9 (Il2CppClass<VLB.VolumetricLightBeamSD>)+130]");
		UnityEngine.Object obj4;
		if (num3 >= 0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v55 @ r8_v8 (Il2CppClass<VLB.VolumetricLightBeamAbstractBase>)+C8]");
			object obj3 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v112 @ rax_v23+FFFFFFF8+v56 @ rax_v19*8]");
			bool flag = 0 == (nint)typeof(VolumetricLightBeamSD);
			obj4 = (UnityEngine.Object)1;
			if (flag)
			{
				goto IL_024c;
			}
		}
		obj4 = null;
		goto IL_024c;
		IL_0273:
		UnityEngine.Object obj5;
		bool flag2 = (object)obj5 == null;
		UnityEngine.Object obj6 = null;
		if (!flag2)
		{
			obj6 = self;
		}
		goto IL_01a4;
		IL_01a4:
		if ((bool)obj6)
		{
			float num4 = 1f - value;
			float sideSoftness = num4 * 10f;
			((VolumetricLightBeamHD)obj6).sideSoftness = sideSoftness;
		}
		return;
		IL_00bb:
		if (!obj)
		{
			bool flag3 = (object)self == null;
			obj6 = null;
			if (!flag3)
			{
				nint num5 = (nint)typeof(VolumetricLightBeamHD);
				nint num6 = (nint)self;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v183 @ rdx_v7 (Il2CppClass<VLB.VolumetricLightBeamHD>)+130]");
				object obj7 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v184 @ r8_v6 (Il2CppClass<VLB.VolumetricLightBeamAbstractBase>)+130]");
				nint num7 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v183 @ rdx_v7 (Il2CppClass<VLB.VolumetricLightBeamHD>)+130]");
				if (num7 >= 0)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v184 @ r8_v6 (Il2CppClass<VLB.VolumetricLightBeamAbstractBase>)+C8]");
					object obj8 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v276 @ rax_v16+FFFFFFF8+v185 @ rax_v13*8]");
					bool flag4 = 0 == (nint)typeof(VolumetricLightBeamHD);
					obj5 = (UnityEngine.Object)1;
					if (flag4)
					{
						goto IL_0273;
					}
				}
				obj5 = null;
				goto IL_0273;
			}
			goto IL_01a4;
		}
		float num8 = 1f - value;
		float num9 = num8 * 10f;
		return;
		IL_024c:
		bool flag5 = (object)obj4 == null;
		obj = null;
		if (!flag5)
		{
			obj = self;
		}
		goto IL_00bb;
	}

	public static float GetFallOffEnd(VolumetricLightBeamAbstractBase self)
	{
		//IL_001d: Expected I, but got O
		//IL_0025: Expected I, but got O
		//IL_0035: Expected O, but got I
		//IL_0071: Expected O, but got I
		//IL_0096: Expected O, but got I4
		//IL_01f2: Expected F4, but got I
		//IL_0106: Expected I, but got O
		//IL_010e: Expected I, but got O
		//IL_011e: Expected O, but got I
		//IL_01dd: Expected F4, but got I
		//IL_01c8: Expected F4, but got I4
		//IL_015a: Expected O, but got I
		//IL_017f: Expected O, but got I4
		UnityEngine.Object obj;
		if ((object)self == null)
		{
			obj = null;
			goto IL_00bb;
		}
		nint num = (nint)typeof(VolumetricLightBeamSD);
		nint num2 = (nint)self;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v51 @ rdx_v9 (Il2CppClass<VLB.VolumetricLightBeamSD>)+130]");
		object obj2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v52 @ r8_v7 (Il2CppClass<VLB.VolumetricLightBeamAbstractBase>)+130]");
		nint num3 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v51 @ rdx_v9 (Il2CppClass<VLB.VolumetricLightBeamSD>)+130]");
		UnityEngine.Object obj4;
		if (num3 >= 0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v52 @ r8_v7 (Il2CppClass<VLB.VolumetricLightBeamAbstractBase>)+C8]");
			object obj3 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v109 @ rax_v22+FFFFFFF8+v53 @ rax_v18*8]");
			bool flag = 0 == (nint)typeof(VolumetricLightBeamSD);
			obj4 = (UnityEngine.Object)1;
			if (flag)
			{
				goto IL_021c;
			}
		}
		obj4 = null;
		goto IL_021c;
		IL_0243:
		UnityEngine.Object obj5;
		bool flag2 = (object)obj5 == null;
		UnityEngine.Object obj6 = null;
		if (!flag2)
		{
			obj6 = self;
		}
		goto IL_01a4;
		IL_01a4:
		if (!obj6)
		{
			return 0f;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v195 @ rbx_v5 (UnityEngine.Object)+7C]");
		return 0f;
		IL_00bb:
		if (!obj)
		{
			bool flag3 = (object)self == null;
			obj6 = null;
			if (!flag3)
			{
				nint num4 = (nint)typeof(VolumetricLightBeamHD);
				nint num5 = (nint)self;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v180 @ rdx_v7 (Il2CppClass<VLB.VolumetricLightBeamHD>)+130]");
				object obj7 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v181 @ r8_v5 (Il2CppClass<VLB.VolumetricLightBeamAbstractBase>)+130]");
				nint num6 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v180 @ rdx_v7 (Il2CppClass<VLB.VolumetricLightBeamHD>)+130]");
				if (num6 >= 0)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v181 @ r8_v5 (Il2CppClass<VLB.VolumetricLightBeamAbstractBase>)+C8]");
					object obj8 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v272 @ rax_v15+FFFFFFF8+v182 @ rax_v12*8]");
					bool flag4 = 0 == (nint)typeof(VolumetricLightBeamHD);
					obj5 = (UnityEngine.Object)1;
					if (flag4)
					{
						goto IL_0243;
					}
				}
				obj5 = null;
				goto IL_0243;
			}
			goto IL_01a4;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v68 @ rdi_v1 (UnityEngine.Object)+B8]");
		return 0f;
		IL_021c:
		bool flag5 = (object)obj4 == null;
		obj = null;
		if (!flag5)
		{
			obj = self;
		}
		goto IL_00bb;
	}

	public static ColorMode GetColorMode(VolumetricLightBeamAbstractBase self)
	{
		//IL_001d: Expected I, but got O
		//IL_0025: Expected I, but got O
		//IL_0035: Expected O, but got I
		//IL_0071: Expected O, but got I
		//IL_0096: Expected O, but got I4
		//IL_0289: Expected I4, but got O
		//IL_0106: Expected I, but got O
		//IL_010e: Expected I, but got O
		//IL_011e: Expected O, but got I
		//IL_0304: Expected I4, but got O
		//IL_015a: Expected O, but got I
		//IL_017f: Expected O, but got I4
		//IL_0276: Expected O, but got I
		UnityEngine.Object obj;
		if ((object)self == null)
		{
			obj = null;
			goto IL_00bb;
		}
		nint num = (nint)typeof(VolumetricLightBeamSD);
		nint num2 = (nint)self;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v51 @ rdx_v10 (Il2CppClass<VLB.VolumetricLightBeamSD>)+130]");
		object obj2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v52 @ r8_v7 (Il2CppClass<VLB.VolumetricLightBeamAbstractBase>)+130]");
		nint num3 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v51 @ rdx_v10 (Il2CppClass<VLB.VolumetricLightBeamSD>)+130]");
		UnityEngine.Object obj4;
		if (num3 >= 0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v52 @ r8_v7 (Il2CppClass<VLB.VolumetricLightBeamAbstractBase>)+C8]");
			object obj3 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v109 @ rax_v26+FFFFFFF8+v53 @ rax_v22*8]");
			bool flag = 0 == (nint)typeof(VolumetricLightBeamSD);
			obj4 = (UnityEngine.Object)1;
			if (flag)
			{
				goto IL_02ae;
			}
		}
		obj4 = null;
		goto IL_02ae;
		IL_00bb:
		UnityEngine.Object obj5;
		UnityEngine.Object obj8;
		if (!obj)
		{
			bool flag2 = (object)self == null;
			obj5 = null;
			if (!flag2)
			{
				nint num4 = (nint)typeof(VolumetricLightBeamHD);
				nint num5 = (nint)self;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v180 @ rdx_v8 (Il2CppClass<VLB.VolumetricLightBeamHD>)+130]");
				object obj6 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v181 @ r8_v5 (Il2CppClass<VLB.VolumetricLightBeamAbstractBase>)+130]");
				nint num6 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v180 @ rdx_v8 (Il2CppClass<VLB.VolumetricLightBeamHD>)+130]");
				if (num6 >= 0)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v181 @ r8_v5 (Il2CppClass<VLB.VolumetricLightBeamAbstractBase>)+C8]");
					object obj7 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v283 @ rax_v19+FFFFFFF8+v182 @ rax_v16*8]");
					bool flag3 = 0 == (nint)typeof(VolumetricLightBeamHD);
					obj8 = (UnityEngine.Object)1;
					if (flag3)
					{
						goto IL_02d5;
					}
				}
				obj8 = null;
				goto IL_02d5;
			}
			goto IL_01a4;
		}
		if ((object)obj != null)
		{
			Config instance = Config.Instance;
			if ((object)instance != null)
			{
				bool flag4 = instance.featureEnabledColorGradient == FeatureEnabledColorGradient.Off;
				UnityEngine.Object obj9 = null;
				if (!flag4)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v68 @ rdi_v1 (UnityEngine.Object)+3C]");
					obj9 = (UnityEngine.Object)0;
				}
				return (ColorMode)obj9;
			}
		}
		goto IL_027b;
		IL_02d5:
		bool flag5 = (object)obj8 == null;
		obj5 = null;
		if (!flag5)
		{
			obj5 = self;
		}
		goto IL_01a4;
		IL_01a4:
		if (!obj5)
		{
			return ColorMode.Flat;
		}
		if ((object)obj5 != null)
		{
			return ((VolumetricLightBeamHD)obj5).colorMode;
		}
		goto IL_027b;
		IL_02ae:
		bool flag6 = (object)obj4 == null;
		obj = null;
		if (!flag6)
		{
			obj = self;
		}
		goto IL_00bb;
		IL_027b:
		NullReferenceException ex = new NullReferenceException();
		return (ColorMode)ex;
	}

	public unsafe static Color GetColorFlat(VolumetricLightBeamAbstractBase self)
	{
		//IL_001d: Expected I, but got O
		//IL_0025: Expected I, but got O
		//IL_0035: Expected O, but got I
		//IL_0071: Expected O, but got I
		//IL_0096: Expected O, but got I4
		//IL_0233: Expected F4, but got I
		//IL_02bc: Expected native int or pointer, but got O
		//IL_0106: Expected I, but got O
		//IL_010e: Expected I, but got O
		//IL_011e: Expected O, but got I
		//IL_01cf: Expected F4, but got I
		//IL_015a: Expected O, but got I
		//IL_017f: Expected O, but got I4
		//IL_0201: Expected F4, but got I
		UnityEngine.Object obj;
		if ((object)self == null)
		{
			obj = null;
			goto IL_00bb;
		}
		nint num = (nint)typeof(VolumetricLightBeamSD);
		nint num2 = (nint)self;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v54 @ rdx_v9 (Il2CppClass<VLB.VolumetricLightBeamSD>)+130]");
		object obj2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v55 @ r8_v7 (Il2CppClass<VLB.VolumetricLightBeamAbstractBase>)+130]");
		nint num3 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v54 @ rdx_v9 (Il2CppClass<VLB.VolumetricLightBeamSD>)+130]");
		UnityEngine.Object obj4;
		if (num3 >= 0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v55 @ r8_v7 (Il2CppClass<VLB.VolumetricLightBeamAbstractBase>)+C8]");
			object obj3 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v112 @ rax_v23+FFFFFFF8+v56 @ rax_v19*8]");
			bool flag = 0 == (nint)typeof(VolumetricLightBeamSD);
			obj4 = (UnityEngine.Object)1;
			if (flag)
			{
				goto IL_026b;
			}
		}
		obj4 = null;
		goto IL_026b;
		IL_0238:
		return (Color)new NullReferenceException();
		IL_01a4:
		UnityEngine.Object obj5;
		float r;
		if (!obj5)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C60]");
			r = 0f;
		}
		else
		{
			if ((object)obj5 == null)
			{
				goto IL_0238;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v198 @ rbx_v5 (UnityEngine.Object)+40]");
			r = 0f;
		}
		goto IL_02b4;
		IL_02b4:
		Color color = default(Color);
		((Color*)(nint)color)->r = r;
		return color;
		IL_026b:
		bool flag2 = (object)obj4 == null;
		obj = null;
		if (!flag2)
		{
			obj = self;
		}
		goto IL_00bb;
		IL_0292:
		UnityEngine.Object obj6;
		bool flag3 = (object)obj6 == null;
		obj5 = null;
		if (!flag3)
		{
			obj5 = self;
		}
		goto IL_01a4;
		IL_00bb:
		if (!obj)
		{
			bool flag4 = (object)self == null;
			obj5 = null;
			if (!flag4)
			{
				nint num4 = (nint)typeof(VolumetricLightBeamHD);
				nint num5 = (nint)self;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v183 @ rdx_v7 (Il2CppClass<VLB.VolumetricLightBeamHD>)+130]");
				object obj7 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v184 @ r8_v5 (Il2CppClass<VLB.VolumetricLightBeamAbstractBase>)+130]");
				nint num6 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v183 @ rdx_v7 (Il2CppClass<VLB.VolumetricLightBeamHD>)+130]");
				if (num6 >= 0)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v184 @ r8_v5 (Il2CppClass<VLB.VolumetricLightBeamAbstractBase>)+C8]");
					object obj8 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v275 @ rax_v16+FFFFFFF8+v185 @ rax_v13*8]");
					bool flag5 = 0 == (nint)typeof(VolumetricLightBeamHD);
					obj6 = (UnityEngine.Object)1;
					if (flag5)
					{
						goto IL_0292;
					}
				}
				obj6 = null;
				goto IL_0292;
			}
			goto IL_01a4;
		}
		if ((object)obj == null)
		{
			goto IL_0238;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v71 @ rdi_v1 (UnityEngine.Object)+40]");
		r = 0f;
		goto IL_02b4;
	}

	public static Gradient GetColorGradient(VolumetricLightBeamAbstractBase self)
	{
		//IL_001d: Expected I, but got O
		//IL_0025: Expected I, but got O
		//IL_0035: Expected O, but got I
		//IL_0071: Expected O, but got I
		//IL_0096: Expected O, but got I4
		//IL_0228: Expected O, but got I
		//IL_0106: Expected I, but got O
		//IL_010e: Expected I, but got O
		//IL_011e: Expected O, but got I
		//IL_015a: Expected O, but got I
		//IL_017f: Expected O, but got I4
		//IL_01f6: Expected O, but got I
		UnityEngine.Object obj;
		if ((object)self == null)
		{
			obj = null;
			goto IL_00bb;
		}
		nint num = (nint)typeof(VolumetricLightBeamSD);
		nint num2 = (nint)self;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v51 @ rdx_v9 (Il2CppClass<VLB.VolumetricLightBeamSD>)+130]");
		object obj2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v52 @ r8_v7 (Il2CppClass<VLB.VolumetricLightBeamAbstractBase>)+130]");
		nint num3 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v51 @ rdx_v9 (Il2CppClass<VLB.VolumetricLightBeamSD>)+130]");
		UnityEngine.Object obj4;
		if (num3 >= 0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v52 @ r8_v7 (Il2CppClass<VLB.VolumetricLightBeamAbstractBase>)+C8]");
			object obj3 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v109 @ rax_v25+FFFFFFF8+v53 @ rax_v21*8]");
			bool flag = 0 == (nint)typeof(VolumetricLightBeamSD);
			obj4 = (UnityEngine.Object)1;
			if (flag)
			{
				goto IL_0260;
			}
		}
		obj4 = null;
		goto IL_0260;
		IL_00bb:
		UnityEngine.Object obj5;
		UnityEngine.Object obj8;
		if (!obj)
		{
			bool flag2 = (object)self == null;
			obj5 = null;
			if (!flag2)
			{
				nint num4 = (nint)typeof(VolumetricLightBeamHD);
				nint num5 = (nint)self;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v180 @ rdx_v7 (Il2CppClass<VLB.VolumetricLightBeamHD>)+130]");
				object obj6 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v181 @ r8_v5 (Il2CppClass<VLB.VolumetricLightBeamAbstractBase>)+130]");
				nint num6 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v180 @ rdx_v7 (Il2CppClass<VLB.VolumetricLightBeamHD>)+130]");
				if (num6 >= 0)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v181 @ r8_v5 (Il2CppClass<VLB.VolumetricLightBeamAbstractBase>)+C8]");
					object obj7 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v272 @ rax_v18+FFFFFFF8+v182 @ rax_v15*8]");
					bool flag3 = 0 == (nint)typeof(VolumetricLightBeamHD);
					obj8 = (UnityEngine.Object)1;
					if (flag3)
					{
						goto IL_0287;
					}
				}
				obj8 = null;
				goto IL_0287;
			}
			goto IL_01a4;
		}
		if ((object)obj != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v68 @ rdi_v1 (UnityEngine.Object)+50]");
			return (Gradient)0;
		}
		goto IL_022d;
		IL_022d:
		return (Gradient)(object)new NullReferenceException();
		IL_01a4:
		if (!obj5)
		{
			return null;
		}
		if ((object)obj5 != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v195 @ rbx_v5 (UnityEngine.Object)+50]");
			return (Gradient)0;
		}
		goto IL_022d;
		IL_0260:
		bool flag4 = (object)obj4 == null;
		obj = null;
		if (!flag4)
		{
			obj = self;
		}
		goto IL_00bb;
		IL_0287:
		bool flag5 = (object)obj8 == null;
		obj5 = null;
		if (!flag5)
		{
			obj5 = self;
		}
		goto IL_01a4;
	}

	public static void SetColorFromLight(VolumetricLightBeamAbstractBase self, bool fromLight)
	{
		//IL_001d: Expected I, but got O
		//IL_0025: Expected I, but got O
		//IL_0035: Expected O, but got I
		//IL_0071: Expected O, but got I
		//IL_0096: Expected O, but got I4
		//IL_00f9: Expected I, but got O
		//IL_0101: Expected I, but got O
		//IL_0111: Expected O, but got I
		//IL_014d: Expected O, but got I
		//IL_0172: Expected O, but got I4
		UnityEngine.Object obj;
		if ((object)self == null)
		{
			obj = null;
			goto IL_00bb;
		}
		nint num = (nint)typeof(VolumetricLightBeamSD);
		nint num2 = (nint)self;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v54 @ rdx_v10 (Il2CppClass<VLB.VolumetricLightBeamSD>)+130]");
		object obj2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v55 @ r8_v8 (Il2CppClass<VLB.VolumetricLightBeamAbstractBase>)+130]");
		nint num3 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v54 @ rdx_v10 (Il2CppClass<VLB.VolumetricLightBeamSD>)+130]");
		UnityEngine.Object obj4;
		if (num3 >= 0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v55 @ r8_v8 (Il2CppClass<VLB.VolumetricLightBeamAbstractBase>)+C8]");
			object obj3 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v112 @ rax_v23+FFFFFFF8+v56 @ rax_v19*8]");
			bool flag = 0 == (nint)typeof(VolumetricLightBeamSD);
			obj4 = (UnityEngine.Object)1;
			if (flag)
			{
				goto IL_01f5;
			}
		}
		obj4 = null;
		goto IL_01f5;
		IL_0239:
		UnityEngine.Object obj5;
		bool flag2 = (object)obj5 == null;
		UnityEngine.Object obj6 = null;
		if (!flag2)
		{
			obj6 = self;
		}
		goto IL_0197;
		IL_0197:
		if ((bool)obj6)
		{
			((VolumetricLightBeamHD)obj6).colorFromLight = fromLight;
		}
		return;
		IL_00bb:
		if ((bool)obj)
		{
		}
		bool flag3 = (object)self == null;
		obj6 = null;
		if (!flag3)
		{
			nint num4 = (nint)typeof(VolumetricLightBeamHD);
			nint num5 = (nint)self;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v227 @ rdx_v7 (Il2CppClass<VLB.VolumetricLightBeamHD>)+130]");
			object obj7 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v228 @ r8_v5 (Il2CppClass<VLB.VolumetricLightBeamAbstractBase>)+130]");
			nint num6 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v227 @ rdx_v7 (Il2CppClass<VLB.VolumetricLightBeamHD>)+130]");
			if (num6 >= 0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v228 @ r8_v5 (Il2CppClass<VLB.VolumetricLightBeamAbstractBase>)+C8]");
				object obj8 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v282 @ rax_v14+FFFFFFF8+v229 @ rax_v11*8]");
				bool flag4 = 0 == (nint)typeof(VolumetricLightBeamHD);
				obj5 = (UnityEngine.Object)1;
				if (flag4)
				{
					goto IL_0239;
				}
			}
			obj5 = null;
			goto IL_0239;
		}
		goto IL_0197;
		IL_01f5:
		bool flag5 = (object)obj4 == null;
		obj = null;
		if (!flag5)
		{
			obj = self;
		}
		goto IL_00bb;
	}

	public static float GetConeAngle(VolumetricLightBeamAbstractBase self)
	{
		//IL_001d: Expected I, but got O
		//IL_0025: Expected I, but got O
		//IL_0035: Expected O, but got I
		//IL_0071: Expected O, but got I
		//IL_0096: Expected O, but got I4
		//IL_0214: Expected O, but got I
		//IL_0231: Expected O, but got I
		//IL_02fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0302: Expected O, but got Unknown
		//IL_0329: Expected O, but got I
		//IL_0346: Expected O, but got I
		//IL_039b: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a0: Expected O, but got Unknown
		//IL_0106: Expected I, but got O
		//IL_010e: Expected I, but got O
		//IL_011e: Expected O, but got I
		//IL_01c8: Expected F4, but got I4
		//IL_015a: Expected O, but got I
		//IL_017f: Expected O, but got I4
		UnityEngine.Object obj;
		if ((object)self == null)
		{
			obj = null;
			goto IL_00bb;
		}
		nint num = (nint)typeof(VolumetricLightBeamSD);
		nint num2 = (nint)self;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v51 @ rdx_v10 (Il2CppClass<VLB.VolumetricLightBeamSD>)+130]");
		object obj2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v66 @ r8_v1 (Il2CppClass<VLB.VolumetricLightBeamAbstractBase>)+130]");
		nint num3 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v51 @ rdx_v10 (Il2CppClass<VLB.VolumetricLightBeamSD>)+130]");
		UnityEngine.Object obj4;
		if (num3 >= 0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v66 @ r8_v1 (Il2CppClass<VLB.VolumetricLightBeamAbstractBase>)+C8]");
			object obj3 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v109 @ rax_v24+FFFFFFF8+v53 @ rax_v20*8]");
			bool flag = 0 == (nint)typeof(VolumetricLightBeamSD);
			obj4 = (UnityEngine.Object)1;
			if (flag)
			{
				goto IL_029f;
			}
		}
		obj4 = null;
		goto IL_029f;
		IL_01a4:
		UnityEngine.Object obj5;
		if (!obj5)
		{
			return 0f;
		}
		return ((VolumetricLightBeamHD)obj5).coneAngle;
		IL_02c6:
		UnityEngine.Object obj6;
		bool flag2 = (object)obj6 == null;
		obj5 = null;
		if (!flag2)
		{
			obj5 = self;
		}
		goto IL_01a4;
		IL_00bb:
		if (!obj)
		{
			bool flag3 = (object)self == null;
			obj5 = null;
			if (!flag3)
			{
				nint num4 = (nint)typeof(VolumetricLightBeamHD);
				nint num5 = (nint)self;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v180 @ rdx_v8 (Il2CppClass<VLB.VolumetricLightBeamHD>)+130]");
				object obj7 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v181 @ r8_v5 (Il2CppClass<VLB.VolumetricLightBeamAbstractBase>)+130]");
				nint num6 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v180 @ rdx_v8 (Il2CppClass<VLB.VolumetricLightBeamHD>)+130]");
				if (num6 >= 0)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v181 @ r8_v5 (Il2CppClass<VLB.VolumetricLightBeamAbstractBase>)+C8]");
					object obj8 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v291 @ rax_v17+FFFFFFF8+v182 @ rax_v14*8]");
					bool flag4 = 0 == (nint)typeof(VolumetricLightBeamHD);
					obj6 = (UnityEngine.Object)1;
					if (flag4)
					{
						goto IL_02c6;
					}
				}
				obj6 = null;
				goto IL_02c6;
			}
			goto IL_01a4;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v68 @ rdi_v1 (UnityEngine.Object)+74]");
		float num7 = 0f * ((float)Math.PI / 180f);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v68 @ rdi_v1 (UnityEngine.Object)+100]");
		nint num8 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
		object obj9 = num8 & 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v68 @ rdi_v1 (UnityEngine.Object)+FC]");
		nint num9 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
		object obj10 = num9 & 0;
		float num10 = num7 * 0.5f;
		if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj10) < System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj9))
		{
			obj10 = obj9;
		}
		object obj11 = obj10;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v68 @ rdi_v1 (UnityEngine.Object)+B8]");
		object obj12 = obj11 + 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033EBD0");
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v68 @ rdi_v1 (UnityEngine.Object)+FC]");
		nint num11 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
		object obj13 = num11 & 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v68 @ rdi_v1 (UnityEngine.Object)+100]");
		nint num12 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
		object obj14 = num12 & 0;
		float num13 = (float)obj12 * num10;
		if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj13) < System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj14))
		{
			obj13 = obj14;
		}
		float num14 = num13;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v68 @ rdi_v1 (UnityEngine.Object)+7C]");
		float num15 = num14 - 0f;
		object obj15 = obj13;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v68 @ rdi_v1 (UnityEngine.Object)+B8]");
		object obj16 = obj15 + 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033D7B0");
		float num16 = num15 * 57.29578f;
		return num16 + num16;
		IL_029f:
		bool flag5 = (object)obj4 == null;
		obj = null;
		if (!flag5)
		{
			obj = self;
		}
		goto IL_00bb;
	}

	public static void SetSpotAngleFromLight(VolumetricLightBeamAbstractBase self, bool fromLight)
	{
		//IL_001d: Expected I, but got O
		//IL_0025: Expected I, but got O
		//IL_0035: Expected O, but got I
		//IL_0071: Expected O, but got I
		//IL_0096: Expected O, but got I4
		//IL_00f9: Expected I, but got O
		//IL_0101: Expected I, but got O
		//IL_0111: Expected O, but got I
		//IL_014d: Expected O, but got I
		//IL_0172: Expected O, but got I4
		UnityEngine.Object obj;
		if ((object)self == null)
		{
			obj = null;
			goto IL_00bb;
		}
		nint num = (nint)typeof(VolumetricLightBeamSD);
		nint num2 = (nint)self;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v54 @ rdx_v10 (Il2CppClass<VLB.VolumetricLightBeamSD>)+130]");
		object obj2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v55 @ r8_v8 (Il2CppClass<VLB.VolumetricLightBeamAbstractBase>)+130]");
		nint num3 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v54 @ rdx_v10 (Il2CppClass<VLB.VolumetricLightBeamSD>)+130]");
		UnityEngine.Object obj4;
		if (num3 >= 0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v55 @ r8_v8 (Il2CppClass<VLB.VolumetricLightBeamAbstractBase>)+C8]");
			object obj3 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v112 @ rax_v23+FFFFFFF8+v56 @ rax_v19*8]");
			bool flag = 0 == (nint)typeof(VolumetricLightBeamSD);
			obj4 = (UnityEngine.Object)1;
			if (flag)
			{
				goto IL_01f5;
			}
		}
		obj4 = null;
		goto IL_01f5;
		IL_0239:
		UnityEngine.Object obj5;
		bool flag2 = (object)obj5 == null;
		UnityEngine.Object obj6 = null;
		if (!flag2)
		{
			obj6 = self;
		}
		goto IL_0197;
		IL_0197:
		if ((bool)obj6)
		{
			((VolumetricLightBeamHD)obj6).useSpotAngleFromAttachedLightSpot = fromLight;
		}
		return;
		IL_00bb:
		if ((bool)obj)
		{
		}
		bool flag3 = (object)self == null;
		obj6 = null;
		if (!flag3)
		{
			nint num4 = (nint)typeof(VolumetricLightBeamHD);
			nint num5 = (nint)self;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v227 @ rdx_v7 (Il2CppClass<VLB.VolumetricLightBeamHD>)+130]");
			object obj7 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v228 @ r8_v5 (Il2CppClass<VLB.VolumetricLightBeamAbstractBase>)+130]");
			nint num6 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v227 @ rdx_v7 (Il2CppClass<VLB.VolumetricLightBeamHD>)+130]");
			if (num6 >= 0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v228 @ r8_v5 (Il2CppClass<VLB.VolumetricLightBeamAbstractBase>)+C8]");
				object obj8 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v282 @ rax_v14+FFFFFFF8+v229 @ rax_v11*8]");
				bool flag4 = 0 == (nint)typeof(VolumetricLightBeamHD);
				obj5 = (UnityEngine.Object)1;
				if (flag4)
				{
					goto IL_0239;
				}
			}
			obj5 = null;
			goto IL_0239;
		}
		goto IL_0197;
		IL_01f5:
		bool flag5 = (object)obj4 == null;
		obj = null;
		if (!flag5)
		{
			obj = self;
		}
		goto IL_00bb;
	}

	public static float GetConeRadiusStart(VolumetricLightBeamAbstractBase self)
	{
		//IL_001d: Expected I, but got O
		//IL_0025: Expected I, but got O
		//IL_0035: Expected O, but got I
		//IL_0071: Expected O, but got I
		//IL_0096: Expected O, but got I4
		//IL_01f2: Expected F4, but got I
		//IL_0106: Expected I, but got O
		//IL_010e: Expected I, but got O
		//IL_011e: Expected O, but got I
		//IL_01dd: Expected F4, but got I
		//IL_01c8: Expected F4, but got I4
		//IL_015a: Expected O, but got I
		//IL_017f: Expected O, but got I4
		UnityEngine.Object obj;
		if ((object)self == null)
		{
			obj = null;
			goto IL_00bb;
		}
		nint num = (nint)typeof(VolumetricLightBeamSD);
		nint num2 = (nint)self;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v51 @ rdx_v9 (Il2CppClass<VLB.VolumetricLightBeamSD>)+130]");
		object obj2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v52 @ r8_v7 (Il2CppClass<VLB.VolumetricLightBeamAbstractBase>)+130]");
		nint num3 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v51 @ rdx_v9 (Il2CppClass<VLB.VolumetricLightBeamSD>)+130]");
		UnityEngine.Object obj4;
		if (num3 >= 0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v52 @ r8_v7 (Il2CppClass<VLB.VolumetricLightBeamAbstractBase>)+C8]");
			object obj3 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v109 @ rax_v22+FFFFFFF8+v53 @ rax_v18*8]");
			bool flag = 0 == (nint)typeof(VolumetricLightBeamSD);
			obj4 = (UnityEngine.Object)1;
			if (flag)
			{
				goto IL_021c;
			}
		}
		obj4 = null;
		goto IL_021c;
		IL_0243:
		UnityEngine.Object obj5;
		bool flag2 = (object)obj5 == null;
		UnityEngine.Object obj6 = null;
		if (!flag2)
		{
			obj6 = self;
		}
		goto IL_01a4;
		IL_01a4:
		if (!obj6)
		{
			return 0f;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v195 @ rbx_v5 (UnityEngine.Object)+70]");
		return 0f;
		IL_00bb:
		if (!obj)
		{
			bool flag3 = (object)self == null;
			obj6 = null;
			if (!flag3)
			{
				nint num4 = (nint)typeof(VolumetricLightBeamHD);
				nint num5 = (nint)self;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v180 @ rdx_v7 (Il2CppClass<VLB.VolumetricLightBeamHD>)+130]");
				object obj7 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v181 @ r8_v5 (Il2CppClass<VLB.VolumetricLightBeamAbstractBase>)+130]");
				nint num6 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v180 @ rdx_v7 (Il2CppClass<VLB.VolumetricLightBeamHD>)+130]");
				if (num6 >= 0)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v181 @ r8_v5 (Il2CppClass<VLB.VolumetricLightBeamAbstractBase>)+C8]");
					object obj8 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v272 @ rax_v15+FFFFFFF8+v182 @ rax_v12*8]");
					bool flag4 = 0 == (nint)typeof(VolumetricLightBeamHD);
					obj5 = (UnityEngine.Object)1;
					if (flag4)
					{
						goto IL_0243;
					}
				}
				obj5 = null;
				goto IL_0243;
			}
			goto IL_01a4;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v68 @ rdi_v1 (UnityEngine.Object)+7C]");
		return 0f;
		IL_021c:
		bool flag5 = (object)obj4 == null;
		obj = null;
		if (!flag5)
		{
			obj = self;
		}
		goto IL_00bb;
	}

	public static float GetConeRadiusEnd(VolumetricLightBeamAbstractBase self)
	{
		//IL_001d: Expected I, but got O
		//IL_0025: Expected I, but got O
		//IL_0035: Expected O, but got I
		//IL_0071: Expected O, but got I
		//IL_0096: Expected O, but got I4
		//IL_0214: Expected O, but got I
		//IL_0231: Expected O, but got I
		//IL_02f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f5: Expected O, but got Unknown
		//IL_0106: Expected I, but got O
		//IL_010e: Expected I, but got O
		//IL_011e: Expected O, but got I
		//IL_01c8: Expected F4, but got I4
		//IL_015a: Expected O, but got I
		//IL_017f: Expected O, but got I4
		UnityEngine.Object obj;
		if ((object)self == null)
		{
			obj = null;
			goto IL_00bb;
		}
		nint num = (nint)typeof(VolumetricLightBeamSD);
		nint num2 = (nint)self;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v51 @ rdx_v10 (Il2CppClass<VLB.VolumetricLightBeamSD>)+130]");
		object obj2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v66 @ r8_v1 (Il2CppClass<VLB.VolumetricLightBeamAbstractBase>)+130]");
		nint num3 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v51 @ rdx_v10 (Il2CppClass<VLB.VolumetricLightBeamSD>)+130]");
		UnityEngine.Object obj4;
		if (num3 >= 0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v66 @ r8_v1 (Il2CppClass<VLB.VolumetricLightBeamAbstractBase>)+C8]");
			object obj3 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v109 @ rax_v23+FFFFFFF8+v53 @ rax_v19*8]");
			bool flag = 0 == (nint)typeof(VolumetricLightBeamSD);
			obj4 = (UnityEngine.Object)1;
			if (flag)
			{
				goto IL_0292;
			}
		}
		obj4 = null;
		goto IL_0292;
		IL_02b9:
		UnityEngine.Object obj5;
		bool flag2 = (object)obj5 == null;
		UnityEngine.Object obj6 = null;
		if (!flag2)
		{
			obj6 = self;
		}
		goto IL_01a4;
		IL_01a4:
		if (!obj6)
		{
			return 0f;
		}
		return ((VolumetricLightBeamHD)obj6).coneRadiusEnd;
		IL_00bb:
		if (!obj)
		{
			bool flag3 = (object)self == null;
			obj6 = null;
			if (!flag3)
			{
				nint num4 = (nint)typeof(VolumetricLightBeamHD);
				nint num5 = (nint)self;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v180 @ rdx_v8 (Il2CppClass<VLB.VolumetricLightBeamHD>)+130]");
				object obj7 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v181 @ r8_v5 (Il2CppClass<VLB.VolumetricLightBeamAbstractBase>)+130]");
				nint num6 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v180 @ rdx_v8 (Il2CppClass<VLB.VolumetricLightBeamHD>)+130]");
				if (num6 >= 0)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v181 @ r8_v5 (Il2CppClass<VLB.VolumetricLightBeamAbstractBase>)+C8]");
					object obj8 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v291 @ rax_v16+FFFFFFF8+v182 @ rax_v13*8]");
					bool flag4 = 0 == (nint)typeof(VolumetricLightBeamHD);
					obj5 = (UnityEngine.Object)1;
					if (flag4)
					{
						goto IL_02b9;
					}
				}
				obj5 = null;
				goto IL_02b9;
			}
			goto IL_01a4;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v68 @ rdi_v1 (UnityEngine.Object)+74]");
		float num7 = 0f * ((float)Math.PI / 180f);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v68 @ rdi_v1 (UnityEngine.Object)+100]");
		nint num8 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
		object obj9 = num8 & 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v68 @ rdi_v1 (UnityEngine.Object)+FC]");
		nint num9 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
		object obj10 = num9 & 0;
		float num10 = num7 * 0.5f;
		if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj10) < System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj9))
		{
			obj10 = obj9;
		}
		object obj11 = obj10;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v68 @ rdi_v1 (UnityEngine.Object)+B8]");
		object obj12 = obj11 + 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033EBD0");
		return (float)obj12 * num10;
		IL_0292:
		bool flag5 = (object)obj4 == null;
		obj = null;
		if (!flag5)
		{
			obj = self;
		}
		goto IL_00bb;
	}

	public static int GetSortingLayerID(VolumetricLightBeamAbstractBase self)
	{
		//IL_001d: Expected I, but got O
		//IL_0025: Expected I, but got O
		//IL_0035: Expected O, but got I
		//IL_0071: Expected O, but got I
		//IL_0096: Expected O, but got I4
		//IL_0249: Expected I4, but got O
		//IL_0106: Expected I, but got O
		//IL_010e: Expected I, but got O
		//IL_011e: Expected O, but got I
		//IL_015a: Expected O, but got I
		//IL_017f: Expected O, but got I4
		//IL_01fa: Expected I, but got O
		UnityEngine.Object obj;
		if ((object)self == null)
		{
			obj = null;
			goto IL_00bb;
		}
		nint num = (nint)typeof(VolumetricLightBeamSD);
		nint num2 = (nint)self;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v51 @ rdx_v11 (Il2CppClass<VLB.VolumetricLightBeamSD>)+130]");
		object obj2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v199 @ r8_v4 (Il2CppClass<VLB.VolumetricLightBeamAbstractBase>)+130]");
		nint num3 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v51 @ rdx_v11 (Il2CppClass<VLB.VolumetricLightBeamSD>)+130]");
		UnityEngine.Object obj4;
		if (num3 >= 0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v199 @ r8_v4 (Il2CppClass<VLB.VolumetricLightBeamAbstractBase>)+C8]");
			object obj3 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v109 @ rax_v26+FFFFFFF8+v53 @ rax_v22*8]");
			bool flag = 0 == (nint)typeof(VolumetricLightBeamSD);
			obj4 = (UnityEngine.Object)1;
			if (flag)
			{
				goto IL_026e;
			}
		}
		obj4 = null;
		goto IL_026e;
		IL_00bb:
		UnityEngine.Object obj5;
		nint num5;
		UnityEngine.Object obj8;
		if (!obj)
		{
			bool flag2 = (object)self == null;
			obj5 = null;
			if (!flag2)
			{
				nint num4 = (nint)typeof(VolumetricLightBeamHD);
				num5 = (nint)self;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v180 @ rdx_v9 (Il2CppClass<VLB.VolumetricLightBeamHD>)+130]");
				object obj6 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v181 @ r8_v5 (Il2CppClass<VLB.VolumetricLightBeamAbstractBase>)+130]");
				nint num6 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v180 @ rdx_v9 (Il2CppClass<VLB.VolumetricLightBeamHD>)+130]");
				if (num6 >= 0)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v181 @ r8_v5 (Il2CppClass<VLB.VolumetricLightBeamAbstractBase>)+C8]");
					object obj7 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v272 @ rax_v19+FFFFFFF8+v182 @ rax_v16*8]");
					bool flag3 = 0 == (nint)typeof(VolumetricLightBeamHD);
					obj8 = (UnityEngine.Object)1;
					if (flag3)
					{
						goto IL_0295;
					}
				}
				obj8 = null;
				goto IL_0295;
			}
			goto IL_01ac;
		}
		if ((object)obj != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v68 @ rdi_v1 (UnityEngine.Object)+124]");
			return 0;
		}
		goto IL_023b;
		IL_023b:
		NullReferenceException ex = new NullReferenceException();
		return (int)ex;
		IL_01ac:
		if (!obj5)
		{
			return 0;
		}
		if ((object)obj5 != null)
		{
			nint num7 = (nint)obj5;
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v362 @ rdx_v7 (Il2CppClass<UnityEngine.Object>)+1F8] (should have been resolved before IL gen)");
			int result = default(int);
			return result;
		}
		goto IL_023b;
		IL_026e:
		bool flag4 = (object)obj4 == null;
		obj = null;
		if (!flag4)
		{
			obj = self;
		}
		goto IL_00bb;
		IL_0295:
		bool flag5 = (object)obj8 == null;
		obj5 = null;
		num2 = num5;
		if (!flag5)
		{
			obj5 = self;
			num2 = num5;
		}
		goto IL_01ac;
	}

	public static int GetSortingOrder(VolumetricLightBeamAbstractBase self)
	{
		//IL_001d: Expected I, but got O
		//IL_0025: Expected I, but got O
		//IL_0035: Expected O, but got I
		//IL_0071: Expected O, but got I
		//IL_0096: Expected O, but got I4
		//IL_0249: Expected I4, but got O
		//IL_0106: Expected I, but got O
		//IL_010e: Expected I, but got O
		//IL_011e: Expected O, but got I
		//IL_015a: Expected O, but got I
		//IL_017f: Expected O, but got I4
		//IL_01fa: Expected I, but got O
		UnityEngine.Object obj;
		if ((object)self == null)
		{
			obj = null;
			goto IL_00bb;
		}
		nint num = (nint)typeof(VolumetricLightBeamSD);
		nint num2 = (nint)self;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v51 @ rdx_v11 (Il2CppClass<VLB.VolumetricLightBeamSD>)+130]");
		object obj2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v199 @ r8_v4 (Il2CppClass<VLB.VolumetricLightBeamAbstractBase>)+130]");
		nint num3 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v51 @ rdx_v11 (Il2CppClass<VLB.VolumetricLightBeamSD>)+130]");
		UnityEngine.Object obj4;
		if (num3 >= 0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v199 @ r8_v4 (Il2CppClass<VLB.VolumetricLightBeamAbstractBase>)+C8]");
			object obj3 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v109 @ rax_v26+FFFFFFF8+v53 @ rax_v22*8]");
			bool flag = 0 == (nint)typeof(VolumetricLightBeamSD);
			obj4 = (UnityEngine.Object)1;
			if (flag)
			{
				goto IL_026e;
			}
		}
		obj4 = null;
		goto IL_026e;
		IL_00bb:
		UnityEngine.Object obj5;
		nint num5;
		UnityEngine.Object obj8;
		if (!obj)
		{
			bool flag2 = (object)self == null;
			obj5 = null;
			if (!flag2)
			{
				nint num4 = (nint)typeof(VolumetricLightBeamHD);
				num5 = (nint)self;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v180 @ rdx_v9 (Il2CppClass<VLB.VolumetricLightBeamHD>)+130]");
				object obj6 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v181 @ r8_v5 (Il2CppClass<VLB.VolumetricLightBeamAbstractBase>)+130]");
				nint num6 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v180 @ rdx_v9 (Il2CppClass<VLB.VolumetricLightBeamHD>)+130]");
				if (num6 >= 0)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v181 @ r8_v5 (Il2CppClass<VLB.VolumetricLightBeamAbstractBase>)+C8]");
					object obj7 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v272 @ rax_v19+FFFFFFF8+v182 @ rax_v16*8]");
					bool flag3 = 0 == (nint)typeof(VolumetricLightBeamHD);
					obj8 = (UnityEngine.Object)1;
					if (flag3)
					{
						goto IL_0295;
					}
				}
				obj8 = null;
				goto IL_0295;
			}
			goto IL_01ac;
		}
		if ((object)obj != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v68 @ rdi_v1 (UnityEngine.Object)+128]");
			return 0;
		}
		goto IL_023b;
		IL_023b:
		NullReferenceException ex = new NullReferenceException();
		return (int)ex;
		IL_01ac:
		if (!obj5)
		{
			return 0;
		}
		if ((object)obj5 != null)
		{
			nint num7 = (nint)obj5;
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v362 @ rdx_v7 (Il2CppClass<UnityEngine.Object>)+208] (should have been resolved before IL gen)");
			int result = default(int);
			return result;
		}
		goto IL_023b;
		IL_026e:
		bool flag4 = (object)obj4 == null;
		obj = null;
		if (!flag4)
		{
			obj = self;
		}
		goto IL_00bb;
		IL_0295:
		bool flag5 = (object)obj8 == null;
		obj5 = null;
		num2 = num5;
		if (!flag5)
		{
			obj5 = self;
			num2 = num5;
		}
		goto IL_01ac;
	}

	public static bool GetFadeOutEnabled(VolumetricLightBeamAbstractBase self)
	{
		//IL_0013: Expected I, but got O
		//IL_001b: Expected I, but got O
		//IL_002b: Expected O, but got I
		//IL_0149: Expected I4, but got O
		//IL_0067: Expected O, but got I
		//IL_008c: Expected O, but got I4
		bool flag = (object)self == null;
		UnityEngine.Object obj = null;
		UnityEngine.Object obj4;
		if (!flag)
		{
			nint num = (nint)typeof(VolumetricLightBeamSD);
			nint num2 = (nint)self;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v42 @ rdx_v3 (Il2CppClass<VLB.VolumetricLightBeamSD>)+130]");
			object obj2 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v43 @ r8_v2 (Il2CppClass<VLB.VolumetricLightBeamAbstractBase>)+130]");
			nint num3 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v42 @ rdx_v3 (Il2CppClass<VLB.VolumetricLightBeamSD>)+130]");
			if (num3 >= 0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v43 @ r8_v2 (Il2CppClass<VLB.VolumetricLightBeamAbstractBase>)+C8]");
				object obj3 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v99 @ rax_v14+FFFFFFF8+v44 @ rax_v10*8]");
				bool flag2 = 0 == (nint)typeof(VolumetricLightBeamSD);
				obj4 = (UnityEngine.Object)1;
				if (flag2)
				{
					goto IL_016b;
				}
			}
			obj4 = null;
			goto IL_016b;
		}
		goto IL_00b1;
		IL_016b:
		bool flag3 = (object)obj4 == null;
		obj = null;
		if (!flag3)
		{
			obj = self;
		}
		goto IL_00b1;
		IL_00b1:
		if ((bool)obj)
		{
			if ((object)obj == null)
			{
				NullReferenceException ex = new NullReferenceException();
				return (byte)(int)ex != 0;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v57 @ rbx_v2 (UnityEngine.Object)+12C]");
			if ((nint)0 >= (nint)0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v57 @ rbx_v2 (UnityEngine.Object)+130]");
				bool flag4 = (nint)0 < (nint)0;
				return !flag4;
			}
		}
		return false;
	}

	public static float GetFadeOutEnd(VolumetricLightBeamAbstractBase self)
	{
		//IL_0013: Expected I, but got O
		//IL_001b: Expected I, but got O
		//IL_002b: Expected O, but got I
		//IL_00df: Expected F4, but got I
		//IL_00d2: Expected F4, but got I4
		//IL_0067: Expected O, but got I
		//IL_008c: Expected O, but got I4
		bool flag = (object)self == null;
		UnityEngine.Object obj = null;
		UnityEngine.Object obj4;
		if (!flag)
		{
			nint num = (nint)typeof(VolumetricLightBeamSD);
			nint num2 = (nint)self;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v42 @ rdx_v3 (Il2CppClass<VLB.VolumetricLightBeamSD>)+130]");
			object obj2 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v43 @ r8_v2 (Il2CppClass<VLB.VolumetricLightBeamAbstractBase>)+130]");
			nint num3 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v42 @ rdx_v3 (Il2CppClass<VLB.VolumetricLightBeamSD>)+130]");
			if (num3 >= 0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v43 @ r8_v2 (Il2CppClass<VLB.VolumetricLightBeamAbstractBase>)+C8]");
				object obj3 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v99 @ rax_v12+FFFFFFF8+v44 @ rax_v8*8]");
				bool flag2 = 0 == (nint)typeof(VolumetricLightBeamSD);
				obj4 = (UnityEngine.Object)1;
				if (flag2)
				{
					goto IL_0101;
				}
			}
			obj4 = null;
			goto IL_0101;
		}
		goto IL_00b1;
		IL_0101:
		bool flag3 = (object)obj4 == null;
		obj = null;
		if (!flag3)
		{
			obj = self;
		}
		goto IL_00b1;
		IL_00b1:
		if ((bool)obj)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v57 @ rbx_v2 (UnityEngine.Object)+130]");
			return 0f;
		}
		return 0f;
	}

	public static void SetFallOffEndFromLight(VolumetricLightBeamAbstractBase self, bool fromLight)
	{
		//IL_001d: Expected I, but got O
		//IL_0025: Expected I, but got O
		//IL_0035: Expected O, but got I
		//IL_0071: Expected O, but got I
		//IL_0096: Expected O, but got I4
		//IL_00f9: Expected I, but got O
		//IL_0101: Expected I, but got O
		//IL_0111: Expected O, but got I
		//IL_014d: Expected O, but got I
		//IL_0172: Expected O, but got I4
		UnityEngine.Object obj;
		if ((object)self == null)
		{
			obj = null;
			goto IL_00bb;
		}
		nint num = (nint)typeof(VolumetricLightBeamSD);
		nint num2 = (nint)self;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v54 @ rdx_v10 (Il2CppClass<VLB.VolumetricLightBeamSD>)+130]");
		object obj2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v55 @ r8_v8 (Il2CppClass<VLB.VolumetricLightBeamAbstractBase>)+130]");
		nint num3 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v54 @ rdx_v10 (Il2CppClass<VLB.VolumetricLightBeamSD>)+130]");
		UnityEngine.Object obj4;
		if (num3 >= 0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v55 @ r8_v8 (Il2CppClass<VLB.VolumetricLightBeamAbstractBase>)+C8]");
			object obj3 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v112 @ rax_v23+FFFFFFF8+v56 @ rax_v19*8]");
			bool flag = 0 == (nint)typeof(VolumetricLightBeamSD);
			obj4 = (UnityEngine.Object)1;
			if (flag)
			{
				goto IL_01f5;
			}
		}
		obj4 = null;
		goto IL_01f5;
		IL_0239:
		UnityEngine.Object obj5;
		bool flag2 = (object)obj5 == null;
		UnityEngine.Object obj6 = null;
		if (!flag2)
		{
			obj6 = self;
		}
		goto IL_0197;
		IL_0197:
		if ((bool)obj6)
		{
			((VolumetricLightBeamHD)obj6).useFallOffEndFromAttachedLightSpot = fromLight;
		}
		return;
		IL_00bb:
		if ((bool)obj)
		{
		}
		bool flag3 = (object)self == null;
		obj6 = null;
		if (!flag3)
		{
			nint num4 = (nint)typeof(VolumetricLightBeamHD);
			nint num5 = (nint)self;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v227 @ rdx_v7 (Il2CppClass<VLB.VolumetricLightBeamHD>)+130]");
			object obj7 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v228 @ r8_v5 (Il2CppClass<VLB.VolumetricLightBeamAbstractBase>)+130]");
			nint num6 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v227 @ rdx_v7 (Il2CppClass<VLB.VolumetricLightBeamHD>)+130]");
			if (num6 >= 0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v228 @ r8_v5 (Il2CppClass<VLB.VolumetricLightBeamAbstractBase>)+C8]");
				object obj8 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v282 @ rax_v14+FFFFFFF8+v229 @ rax_v11*8]");
				bool flag4 = 0 == (nint)typeof(VolumetricLightBeamHD);
				obj5 = (UnityEngine.Object)1;
				if (flag4)
				{
					goto IL_0239;
				}
			}
			obj5 = null;
			goto IL_0239;
		}
		goto IL_0197;
		IL_01f5:
		bool flag5 = (object)obj4 == null;
		obj = null;
		if (!flag5)
		{
			obj = self;
		}
		goto IL_00bb;
	}

	public static Dimensions GetDimensions(VolumetricLightBeamAbstractBase self)
	{
		//IL_001d: Expected I, but got O
		//IL_0025: Expected I, but got O
		//IL_0035: Expected O, but got I
		//IL_0071: Expected O, but got I
		//IL_0096: Expected O, but got I4
		//IL_0249: Expected I4, but got O
		//IL_0106: Expected I, but got O
		//IL_010e: Expected I, but got O
		//IL_011e: Expected O, but got I
		//IL_015a: Expected O, but got I
		//IL_017f: Expected O, but got I4
		//IL_01fa: Expected I, but got O
		UnityEngine.Object obj;
		if ((object)self == null)
		{
			obj = null;
			goto IL_00bb;
		}
		nint num = (nint)typeof(VolumetricLightBeamSD);
		nint num2 = (nint)self;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v51 @ rdx_v11 (Il2CppClass<VLB.VolumetricLightBeamSD>)+130]");
		object obj2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v199 @ r8_v4 (Il2CppClass<VLB.VolumetricLightBeamAbstractBase>)+130]");
		nint num3 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v51 @ rdx_v11 (Il2CppClass<VLB.VolumetricLightBeamSD>)+130]");
		UnityEngine.Object obj4;
		if (num3 >= 0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v199 @ r8_v4 (Il2CppClass<VLB.VolumetricLightBeamAbstractBase>)+C8]");
			object obj3 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v109 @ rax_v26+FFFFFFF8+v53 @ rax_v22*8]");
			bool flag = 0 == (nint)typeof(VolumetricLightBeamSD);
			obj4 = (UnityEngine.Object)1;
			if (flag)
			{
				goto IL_026e;
			}
		}
		obj4 = null;
		goto IL_026e;
		IL_00bb:
		UnityEngine.Object obj5;
		nint num5;
		UnityEngine.Object obj8;
		if (!obj)
		{
			bool flag2 = (object)self == null;
			obj5 = null;
			if (!flag2)
			{
				nint num4 = (nint)typeof(VolumetricLightBeamHD);
				num5 = (nint)self;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v180 @ rdx_v9 (Il2CppClass<VLB.VolumetricLightBeamHD>)+130]");
				object obj6 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v181 @ r8_v5 (Il2CppClass<VLB.VolumetricLightBeamAbstractBase>)+130]");
				nint num6 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v180 @ rdx_v9 (Il2CppClass<VLB.VolumetricLightBeamHD>)+130]");
				if (num6 >= 0)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v181 @ r8_v5 (Il2CppClass<VLB.VolumetricLightBeamAbstractBase>)+C8]");
					object obj7 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v272 @ rax_v19+FFFFFFF8+v182 @ rax_v16*8]");
					bool flag3 = 0 == (nint)typeof(VolumetricLightBeamHD);
					obj8 = (UnityEngine.Object)1;
					if (flag3)
					{
						goto IL_0295;
					}
				}
				obj8 = null;
				goto IL_0295;
			}
			goto IL_01ac;
		}
		if ((object)obj != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v68 @ rdi_v1 (UnityEngine.Object)+F8]");
			return Dimensions.Dim3D;
		}
		goto IL_023b;
		IL_023b:
		NullReferenceException ex = new NullReferenceException();
		return (Dimensions)ex;
		IL_01ac:
		if (!obj5)
		{
			return Dimensions.Dim3D;
		}
		if ((object)obj5 != null)
		{
			nint num7 = (nint)obj5;
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v362 @ rdx_v7 (Il2CppClass<UnityEngine.Object>)+1D8] (should have been resolved before IL gen)");
			Dimensions result = default(Dimensions);
			return result;
		}
		goto IL_023b;
		IL_026e:
		bool flag4 = (object)obj4 == null;
		obj = null;
		if (!flag4)
		{
			obj = self;
		}
		goto IL_00bb;
		IL_0295:
		bool flag5 = (object)obj8 == null;
		obj5 = null;
		num2 = num5;
		if (!flag5)
		{
			obj5 = self;
			num2 = num5;
		}
		goto IL_01ac;
	}

	public static int GetGeomSides(VolumetricLightBeamAbstractBase self)
	{
		//IL_0013: Expected I, but got O
		//IL_001b: Expected I, but got O
		//IL_002b: Expected O, but got I
		//IL_0167: Expected I4, but got O
		//IL_0067: Expected O, but got I
		//IL_008c: Expected O, but got I4
		bool flag = (object)self == null;
		UnityEngine.Object obj = null;
		UnityEngine.Object obj4;
		if (!flag)
		{
			nint num = (nint)typeof(VolumetricLightBeamSD);
			nint num2 = (nint)self;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v42 @ rdx_v3 (Il2CppClass<VLB.VolumetricLightBeamSD>)+130]");
			object obj2 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v43 @ r8_v2 (Il2CppClass<VLB.VolumetricLightBeamAbstractBase>)+130]");
			nint num3 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v42 @ rdx_v3 (Il2CppClass<VLB.VolumetricLightBeamSD>)+130]");
			if (num3 >= 0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v43 @ r8_v2 (Il2CppClass<VLB.VolumetricLightBeamAbstractBase>)+C8]");
				object obj3 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v99 @ rax_v16+FFFFFFF8+v44 @ rax_v12*8]");
				bool flag2 = 0 == (nint)typeof(VolumetricLightBeamSD);
				obj4 = (UnityEngine.Object)1;
				if (flag2)
				{
					goto IL_0189;
				}
			}
			obj4 = null;
			goto IL_0189;
		}
		goto IL_00b1;
		IL_0189:
		bool flag3 = (object)obj4 == null;
		obj = null;
		if (!flag3)
		{
			obj = self;
		}
		goto IL_00b1;
		IL_00b1:
		if ((bool)obj)
		{
			if ((object)obj == null)
			{
				goto IL_0159;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v57 @ rbx_v2 (UnityEngine.Object)+84]");
			if ((nint)0 == 1)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v57 @ rbx_v2 (UnityEngine.Object)+88]");
				return 0;
			}
		}
		Config instance = Config.Instance;
		if ((object)instance != null)
		{
			return instance.sharedMeshSides;
		}
		goto IL_0159;
		IL_0159:
		NullReferenceException ex = new NullReferenceException();
		return (int)ex;
	}

	public static AttenuationEquation ConvertAttenuation(AttenuationEquationHD value)
	{
		return (AttenuationEquation)value;
	}

	public static AttenuationEquationHD ConvertAttenuation(AttenuationEquation value)
	{
		bool flag = value == AttenuationEquation.Blend;
		AttenuationEquationHD result = AttenuationEquationHD.Linear;
		if (!flag)
		{
			result = (AttenuationEquationHD)value;
		}
		return result;
	}
}
