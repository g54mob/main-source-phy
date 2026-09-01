using System;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Shapes;

public class TextMeshProShapes : TextMeshPro
{
	protected float curvature;

	protected Vector2 curvaturePivot;

	public float Curvature
	{
		get
		{
			return curvature;
		}
		set
		{
			//IL_002d: Expected I, but got O
			bool flag = curvature == value;
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"jp short 0000000181052F6Fh\"");
			if (!flag)
			{
				nint num = (nint)this;
				curvature = value;
				m_havePropertiesChanged = true;
				Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: [v13 @ rdx_v1 (Il2CppClass<Shapes.TextMeshProShapes>)+2F8] (should have been resolved before IL gen)");
			}
		}
	}

	public Vector2 CurvaturePivot
	{
		get
		{
			Vector2 result = default(Vector2);
			return result;
		}
		set
		{
			//IL_001f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0024: Expected O, but got Unknown
			//IL_0054: Invalid comparison between F4 and O
			//IL_006d: Expected I, but got O
			//IL_0092: Expected O, but got I
			//IL_00a2: Expected O, but got I
			object obj = curvaturePivot - value;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.TextMeshProShapes)+7A0]");
			object obj3 = default(object);
			object obj2 = 0 - obj3;
			object obj4 = obj2 * obj2;
			object obj5 = obj * obj;
			object obj6 = obj4 + obj5;
			if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)9.9999994E-11f) <= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj6))
			{
				nint num = (nint)this;
				m_havePropertiesChanged = true;
				curvaturePivot = value;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ rdx_v1 (Il2CppClass<Shapes.TextMeshProShapes>)+2F8]");
				object obj7 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ rdx_v1 (Il2CppClass<Shapes.TextMeshProShapes>)+300]");
				object obj8 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v33 @ rax_v1 (should have been resolved before IL gen)");
			}
		}
	}

	protected override void OnEnable()
	{
		//IL_000a: Expected I, but got O
		//IL_001a: Expected O, but got I
		//IL_002a: Expected O, but got I
		while (true)
		{
			base.OnEnable();
			Action<TMP_TextInfo> action = ApplyDeformation;
			nint num = (nint)this;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v40 @ r8_v2 (Il2CppClass<Shapes.TextMeshProShapes>)+618]");
			object obj = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v40 @ r8_v2 (Il2CppClass<Shapes.TextMeshProShapes>)+620]");
			object obj2 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v43 @ r9_v2 (should have been resolved before IL gen)");
		}
	}

	protected override void OnDisable()
	{
		//IL_000a: Expected I, but got O
		//IL_001a: Expected O, but got I
		//IL_002a: Expected O, but got I
		while (true)
		{
			base.OnDisable();
			Action<TMP_TextInfo> action = ApplyDeformation;
			nint num = (nint)this;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v40 @ r8_v2 (Il2CppClass<Shapes.TextMeshProShapes>)+628]");
			object obj = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v40 @ r8_v2 (Il2CppClass<Shapes.TextMeshProShapes>)+630]");
			object obj2 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v43 @ r9_v2 (should have been resolved before IL gen)");
		}
	}

	private unsafe void ApplyDeformation(TMP_TextInfo obj)
	{
		//IL_000b: Invalid comparison between F4 and I4
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Expected O, but got Unknown
		//IL_0066: Expected O, but got I4
		//IL_006f: Expected O, but got I4
		//IL_007c: Expected O, but got Ref
		//IL_0466: Unknown result type (might be due to invalid IL or missing references)
		//IL_046b: Expected O, but got Unknown
		//IL_0474: Unknown result type (might be due to invalid IL or missing references)
		//IL_0479: Expected O, but got Unknown
		//IL_00ca: Expected O, but got I
		//IL_00f9: Expected O, but got I
		//IL_010e: Expected O, but got I
		//IL_0123: Expected O, but got I
		//IL_012b: Expected O, but got Ref
		//IL_013c: Expected O, but got I4
		//IL_0504: Unknown result type (might be due to invalid IL or missing references)
		//IL_0509: Expected O, but got Unknown
		//IL_0512: Unknown result type (might be due to invalid IL or missing references)
		//IL_0517: Expected O, but got Unknown
		//IL_0580: Unknown result type (might be due to invalid IL or missing references)
		//IL_0585: Expected O, but got Unknown
		//IL_0188: Expected O, but got I
		//IL_01b7: Expected O, but got I
		//IL_01cc: Expected O, but got I
		//IL_01e1: Expected O, but got I
		//IL_043c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0441: Expected O, but got Unknown
		//IL_044a: Unknown result type (might be due to invalid IL or missing references)
		//IL_044f: Expected O, but got Unknown
		//IL_0205: Expected O, but got Ref
		//IL_05af: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b4: Expected O, but got Unknown
		//IL_05bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c2: Expected O, but got Unknown
		//IL_062e: Expected O, but got I4
		//IL_0253: Expected O, but got I
		//IL_0282: Expected O, but got I
		//IL_0297: Expected O, but got I
		//IL_02ac: Expected O, but got I
		//IL_02d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02db: Expected O, but got Unknown
		//IL_02fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0303: Expected O, but got Unknown
		//IL_030c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0311: Expected O, but got Unknown
		//IL_0327: Unknown result type (might be due to invalid IL or missing references)
		//IL_032c: Expected O, but got Unknown
		//IL_033c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0341: Expected O, but got Unknown
		//IL_0365: Unknown result type (might be due to invalid IL or missing references)
		//IL_036a: Expected O, but got Unknown
		//IL_03eb: Expected O, but got I4
		//IL_0409: Unknown result type (might be due to invalid IL or missing references)
		//IL_040e: Expected O, but got Unknown
		bool flag = curvature == 0f;
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"jp short 00000001810527FDh\"");
		if (flag)
		{
			return;
		}
		TMP_TextInfo tMP_TextInfo = base.textInfo;
		TMP_CharacterInfo[] characterInfo = tMP_TextInfo.characterInfo;
		object obj2 = tMP_TextInfo.characterInfo + 32;
		object obj3 = 0;
		object obj4 = 0;
		object obj5 = default(object);
		object obj7 = default(object);
		object obj8 = default(object);
		object obj10 = default(object);
		object obj14 = default(object);
		object obj18 = default(object);
		object obj25 = default(object);
		while ((nint)obj4 < characterInfo.Length)
		{
			TextMeshProShapes textMeshProShapes = (TextMeshProShapes)(&obj5);
			object obj6 = obj2;
			textMeshProShapes = this;
			obj6 = obj7;
			do
			{
				textMeshProShapes = (TextMeshProShapes)(textMeshProShapes + 128);
				obj6 += 128;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v489 @ rax_v10+10]");
				_ = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v489 @ rax_v10-60]");
				_ = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v489 @ rax_v10-50]");
				_ = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v489 @ rax_v10-40]");
				_ = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v489 @ rax_v10-30]");
				_ = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v489 @ rax_v10-20]");
				_ = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v489 @ rax_v10-10]");
				_ = 0;
			}
			while ((nint)obj3 != characterInfo.Length);
			textMeshProShapes = (TextMeshProShapes)obj6;
			TextMeshProShapes textMeshProShapes2 = textMeshProShapes;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v489 @ rax_v10+10]");
			((UnityEngine.Object)textMeshProShapes2).m_CachedPtr = (IntPtr)0;
			TextMeshProShapes textMeshProShapes3 = textMeshProShapes;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v489 @ rax_v10+20]");
			textMeshProShapes3.m_Material = (Material)0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v489 @ rax_v10+30]");
			_ = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v489 @ rax_v10+40]");
			_ = 0;
			TextMeshProShapes textMeshProShapes4 = textMeshProShapes;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v489 @ rax_v10+50]");
			((Graphic)textMeshProShapes4).m_RectTransform = (RectTransform)0;
			TextMeshProShapes textMeshProShapes5 = textMeshProShapes;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v489 @ rax_v10+60]");
			((Graphic)textMeshProShapes5).m_Canvas = (Canvas)0;
			TextMeshProShapes textMeshProShapes6 = textMeshProShapes;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v489 @ rax_v10+70]");
			textMeshProShapes6.m_OnDirtyLayoutCallback = (UnityAction)0;
			TextMeshProShapes textMeshProShapes7 = (TextMeshProShapes)(&obj8);
			object obj9 = obj2;
			TMP_TextInfo tMP_TextInfo2 = (TMP_TextInfo)2;
			textMeshProShapes7 = this;
			obj9 = obj7;
			tMP_TextInfo2 = obj;
			do
			{
				textMeshProShapes7 = (TextMeshProShapes)(textMeshProShapes7 + 128);
				obj9 += 128;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v520 @ rax_v14+10]");
				_ = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v520 @ rax_v14-60]");
				_ = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v520 @ rax_v14-50]");
				_ = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v520 @ rax_v14-40]");
				_ = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v520 @ rax_v14-30]");
				_ = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v520 @ rax_v14-20]");
				_ = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v520 @ rax_v14-10]");
				_ = 0;
				tMP_TextInfo2 = (TMP_TextInfo)(tMP_TextInfo2 - 1);
			}
			while ((nint)obj3 != characterInfo.Length);
			textMeshProShapes7 = (TextMeshProShapes)obj9;
			TextMeshProShapes textMeshProShapes8 = textMeshProShapes7;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v520 @ rax_v14+10]");
			((UnityEngine.Object)textMeshProShapes8).m_CachedPtr = (IntPtr)0;
			TextMeshProShapes textMeshProShapes9 = textMeshProShapes7;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v520 @ rax_v14+20]");
			textMeshProShapes9.m_Material = (Material)0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v520 @ rax_v14+30]");
			_ = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v520 @ rax_v14+40]");
			_ = 0;
			TextMeshProShapes textMeshProShapes10 = textMeshProShapes7;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v520 @ rax_v14+50]");
			((Graphic)textMeshProShapes10).m_RectTransform = (RectTransform)0;
			TextMeshProShapes textMeshProShapes11 = textMeshProShapes7;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v520 @ rax_v14+60]");
			((Graphic)textMeshProShapes11).m_Canvas = (Canvas)0;
			TextMeshProShapes textMeshProShapes12 = textMeshProShapes7;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v520 @ rax_v14+70]");
			textMeshProShapes12.m_OnDirtyLayoutCallback = (UnityAction)0;
			bool flag2 = obj10 == tMP_TextInfo2;
			if (!flag2)
			{
				TextMeshProShapes textMeshProShapes13 = (TextMeshProShapes)(&obj8);
				object obj11 = obj2;
				textMeshProShapes13 = this;
				obj11 = obj7;
				object obj12;
				do
				{
					textMeshProShapes13 = (TextMeshProShapes)(textMeshProShapes13 + 128);
					obj11 += 128;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v580 @ rax_v20+10]");
					_ = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v580 @ rax_v20-60]");
					_ = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v580 @ rax_v20-50]");
					_ = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v580 @ rax_v20-40]");
					_ = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v580 @ rax_v20-30]");
					_ = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v580 @ rax_v20-20]");
					_ = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v580 @ rax_v20-10]");
					_ = 0;
					obj12 = !flag2;
				}
				while (obj12 != null);
				textMeshProShapes13 = (TextMeshProShapes)obj11;
				TextMeshProShapes textMeshProShapes14 = textMeshProShapes13;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v580 @ rax_v20+10]");
				((UnityEngine.Object)textMeshProShapes14).m_CachedPtr = (IntPtr)0;
				TextMeshProShapes textMeshProShapes15 = textMeshProShapes13;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v580 @ rax_v20+20]");
				textMeshProShapes15.m_Material = (Material)0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v580 @ rax_v20+30]");
				_ = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v580 @ rax_v20+40]");
				_ = 0;
				TextMeshProShapes textMeshProShapes16 = textMeshProShapes13;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v580 @ rax_v20+50]");
				((Graphic)textMeshProShapes16).m_RectTransform = (RectTransform)0;
				TextMeshProShapes textMeshProShapes17 = textMeshProShapes13;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v580 @ rax_v20+60]");
				((Graphic)textMeshProShapes17).m_Canvas = (Canvas)0;
				TextMeshProShapes textMeshProShapes18 = textMeshProShapes13;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v580 @ rax_v20+70]");
				textMeshProShapes18.m_OnDirtyLayoutCallback = (UnityAction)0;
				TMP_TextInfo tMP_TextInfo3 = base.textInfo;
				TMP_MeshInfo[] meshInfo = tMP_TextInfo3.meshInfo;
				object obj13 = obj14 * 4;
				object obj15 = obj14 + obj13;
				object obj16 = obj15 + obj15;
				object obj17 = obj18 + 4;
				object obj19 = obj17 * 2;
				object obj20 = obj18 + obj19;
				object obj21 = obj20 * 4;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v297 @ r14_v7 (TMPro.TMP_MeshInfo[])+30+v312 @ rax_v26*8]");
				object obj22 = 0 + obj21;
				bool flag3;
				do
				{
					object obj23 = obj22 - (object)curvaturePivot;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.TextMeshProShapes)+7A0]");
					object obj24 = obj25 - 0;
					float num = (float)obj24 * curvature;
					float num2 = 1f - num;
					float num3 = curvature / num2;
					float num4 = (float)obj23 * num2;
					float x = num3 * num4;
					float num5 = ShapesMath.Sinc(x);
					float num6 = ShapesMath.Cosinc(x);
					object obj26 = 0 + 1;
					obj22 = obj25;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v440 @ rdi_v10+8]");
					_ = 0;
					object obj27 = obj22 + 12;
					flag3 = (nint)obj26 < 4;
					obj22 = obj27;
				}
				while (flag3);
			}
			obj3++;
			obj2 += 376;
			obj4 = obj3;
		}
	}

	private unsafe static Vector3 Bend(Vector3 p, float curvature)
	{
		//IL_0091: Expected native int or pointer, but got O
		//IL_00c1: Expected native int or pointer, but got O
		//IL_00ce: Expected native int or pointer, but got O
		float num = curvature * p.y;
		float num2 = 1f - num;
		float num3 = curvature / num2;
		float num4 = num2 * p.x;
		float x = num3 * num4;
		float num5 = ShapesMath.Sinc(x);
		float num6 = ShapesMath.Cosinc(x);
		float num7 = num4 * num6;
		Vector3 vector = default(Vector3);
		((Vector3*)(nint)vector)->z = p.z;
		float x2 = num4 * num5;
		float y = num7 + p.y;
		((Vector3*)(nint)vector)->x = x2;
		((Vector3*)(nint)vector)->y = y;
		return vector;
	}
}
