using System;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using UnityEngine;

namespace Shapes;

internal static class TexturePlacement
{
	private static readonly Rect fitUvs;

	internal unsafe static (Rect, Rect) Fit(Texture texture, Rect rect, TextureFillMode mode)
	{
		//IL_0013: Expected O, but got I
		//IL_0164: Expected O, but got Ref
		//IL_0164: Expected O, but got Ref
		//IL_016d: Expected O, but got I4
		//IL_011b: Expected O, but got Ref
		//IL_00e5: Expected O, but got Ref
		//IL_00e5: Expected O, but got Ref
		//IL_00ee: Expected O, but got I4
		nint num = default(nint);
		bool flag = num == 0;
		(Rect, Rect) tuple = default((Rect, Rect));
		object obj7 = default(object);
		object obj8 = default(object);
		Texture texture2;
		if (!flag)
		{
			object obj = num - 1;
			if (!flag)
			{
				if ((nint)obj == 1)
				{
					if ((object)rect != null)
					{
						float xMin = rect.m_XMin;
						Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v367 @ rdx_v8 (System.Single)+188] (should have been resolved before IL gen)");
						float xMin2 = rect.m_XMin;
						object obj3 = default(object);
						object obj4 = default(object);
						object obj2 = obj3 / obj4;
						Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v372 @ rdx_v10 (System.Single)+1A8] (should have been resolved before IL gen)");
						object obj6 = default(object);
						object obj5 = obj3 / obj6;
						if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj2) > System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj5))
						{
						}
						tuple = ((Rect)(&obj7), (Rect)(&obj8));
						texture2 = (Texture)0;
						((UnityEngine.Object)texture).m_CachedPtr = (IntPtr)0;
						return ((Rect, Rect))texture;
					}
					return ((Rect, Rect))new NullReferenceException();
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
				Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
				object actualValue = default(object);
				ArgumentOutOfRangeException ex = new ArgumentOutOfRangeException("mode", actualValue, null);
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
				throw ex;
			}
			(Rect, Rect) tuple2 = ScaleToFit((Texture)(&tuple), rect);
			texture2 = (Texture)tuple2;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v123 @ rax_v14 (System.ValueTuple`2<UnityEngine.Rect, UnityEngine.Rect>)+10]");
			((UnityEngine.Object)texture).m_CachedPtr = (IntPtr)0;
			return ((Rect, Rect))texture;
		}
		tuple = ((Rect)(&obj8), (Rect)(&obj7));
		texture2 = (Texture)0;
		((UnityEngine.Object)texture).m_CachedPtr = (IntPtr)0;
		return ((Rect, Rect))texture;
	}

	internal static (Rect, Rect) Size(Texture texture, Vector2 c, float size, TextureSizeMode mode)
	{
		//IL_005d: Expected O, but got I8
		//IL_0077: Expected O, but got I8
		Vector2 vector = default(Vector2);
		float x = vector.x;
		Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v46 @ rdx_v5 (System.Single)+188] (should have been resolved before IL gen)");
		float x2 = vector.x;
		Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v53 @ rdx_v7 (System.Single)+1A8] (should have been resolved before IL gen)");
		Vector2 vector2 = default(Vector2);
		if ((nint)vector2 <= 5)
		{
			object obj = 6442450944L;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v79 @ r8_v2+106ACD8+v61 @ stack_28 (UnityEngine.Vector2)*4]");
			object obj2 = 0 + 6442450944L;
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v81 @ rcx_v14 (should have been resolved before IL gen)");
		}
		else
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
			Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
		}
		object actualValue = default(object);
		ArgumentOutOfRangeException ex = new ArgumentOutOfRangeException("mode", actualValue, null);
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
		throw ex;
	}

	private unsafe static (Rect, Rect) FitWidth(Vector2 c, float w, float aspect)
	{
		//IL_0016: Expected O, but got Ref
		//IL_0027: Expected F4, but got O
		//IL_0022: Expected native int or pointer, but got O
		object obj = default(object);
		(Rect, Rect) tuple = SimpleRect((Vector2)(&obj), w, aspect);
		((Vector2*)(nint)c)->x = (float)tuple;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v57 @ rax_v3 (System.ValueTuple`2<UnityEngine.Rect, UnityEngine.Rect>)+10]");
		_ = 0;
		return ((Rect, Rect))c;
	}

	private unsafe static (Rect, Rect) FitHeight(Vector2 c, float h, float aspect)
	{
		//IL_0025: Expected O, but got Ref
		//IL_0036: Expected F4, but got O
		//IL_0031: Expected native int or pointer, but got O
		object obj = default(object);
		float h2 = aspect * (float)obj;
		object obj2 = default(object);
		(Rect, Rect) tuple = SimpleRect((Vector2)(&obj2), h, h2);
		((Vector2*)(nint)c)->x = (float)tuple;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v57 @ rax_v3 (System.ValueTuple`2<UnityEngine.Rect, UnityEngine.Rect>)+10]");
		_ = 0;
		return ((Rect, Rect))c;
	}

	private unsafe static (Rect, Rect) FitRadius(Texture tex, Vector2 c, float r)
	{
		//IL_0072: Expected O, but got Ref
		if ((object)c != null)
		{
			float x = c.x;
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v48 @ rdx_v1 (System.Single)+188] (should have been resolved before IL gen)");
			float x2 = c.x;
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v55 @ rdx_v3 (System.Single)+1A8] (should have been resolved before IL gen)");
			Vector2 value = default(Vector2);
			Vector2 vector = Vector2.Normalize(ref value);
			object obj2 = default(object);
			object obj = obj2 + obj2;
			float h = (float)obj * (float)vector;
			object obj3 = default(object);
			float w = default(float);
			(Rect, Rect) tuple = SimpleRect((Vector2)(&obj3), w, h);
			Texture texture = (Texture)tuple;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v105 @ rax_v10 (System.ValueTuple`2<UnityEngine.Rect, UnityEngine.Rect>)+10]");
			((UnityEngine.Object)tex).m_CachedPtr = (IntPtr)0;
			return ((Rect, Rect))tex;
		}
		return ((Rect, Rect))new NullReferenceException();
	}

	private unsafe static (Rect, Rect) SimpleRect(Vector2 c, float w, float h)
	{
		//IL_0013: Expected native int or pointer, but got O
		//IL_002f: Expected O, but got Ref
		//IL_002f: Expected O, but got Ref
		//IL_002a: Expected native int or pointer, but got O
		((Vector2*)(nint)c)->x = 0f;
		_ = 0;
		object obj = default(object);
		object obj2 = default(object);
		*((Rect, Rect)*)(nint)c = ((Rect)(&obj), (Rect)(&obj2));
		return ((Rect, Rect))c;
	}

	private unsafe static Rect RectCnt(float cx, float cy, float w, float h)
	{
		//IL_0008: Expected native int or pointer, but got O
		//IL_0034: Expected native int or pointer, but got O
		//IL_0041: Expected native int or pointer, but got O
		//IL_006d: Expected native int or pointer, but got O
		Rect rect = default(Rect);
		((Rect*)(nint)rect)->m_Width = w;
		float num = w * 0.5f;
		float xMin = cx - num;
		((Rect*)(nint)rect)->m_XMin = xMin;
		float num2 = default(float);
		((Rect*)(nint)rect)->m_Height = num2;
		float num3 = num2 * 0.5f;
		float yMin = cy - num3;
		((Rect*)(nint)rect)->m_YMin = yMin;
		return rect;
	}

	private unsafe static Rect RectCnt(Vector2 c, float w, float h)
	{
		//IL_0018: Expected native int or pointer, but got O
		//IL_0034: Expected native int or pointer, but got O
		//IL_0051: Expected native int or pointer, but got O
		//IL_006d: Expected native int or pointer, but got O
		float num = w * 0.5f;
		Rect rect = default(Rect);
		((Rect*)(nint)rect)->m_Width = w;
		float xMin = (float)c - num;
		((Rect*)(nint)rect)->m_Height = h;
		float num2 = h * 0.5f;
		((Rect*)(nint)rect)->m_XMin = xMin;
		object obj = default(object);
		float yMin = (float)obj - num2;
		((Rect*)(nint)rect)->m_YMin = yMin;
		return rect;
	}

	private unsafe static (Rect, Rect) StretchToFill(Rect rect)
	{
		//IL_0013: Expected native int or pointer, but got O
		//IL_002f: Expected O, but got Ref
		//IL_002f: Expected O, but got Ref
		//IL_002a: Expected native int or pointer, but got O
		((Rect*)(nint)rect)->m_XMin = 0f;
		_ = 0;
		object obj = default(object);
		object obj2 = default(object);
		*((Rect, Rect)*)(nint)rect = ((Rect)(&obj), (Rect)(&obj2));
		return ((Rect, Rect))rect;
	}

	private unsafe static (Rect, Rect) ScaleToFit(Texture texture, Rect rect)
	{
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Expected O, but got Unknown
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Expected O, but got Unknown
		//IL_00e0: Expected O, but got I4
		//IL_00ff: Expected O, but got Ref
		//IL_00ff: Expected O, but got Ref
		//IL_00fa: Expected native int or pointer, but got O
		if ((object)rect != null)
		{
			float xMin = rect.m_XMin;
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v61 @ rdx_v1 (System.Single)+188] (should have been resolved before IL gen)");
			float xMin2 = rect.m_XMin;
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v76 @ rdx_v3 (System.Single)+1A8] (should have been resolved before IL gen)");
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [methodInfo @ r8 (Il2CppMethodInfo)+C]");
			object obj2 = default(object);
			object obj = 0 / obj2;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [methodInfo @ r8 (Il2CppMethodInfo)+8]");
			object obj4 = default(object);
			object obj3 = 0 / obj4;
			if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj) > System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj3))
			{
				float xMin3 = rect.m_XMin;
				Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v106 @ rdx_v5 (System.Single)+188] (should have been resolved before IL gen)");
				float xMin4 = rect.m_XMin;
				Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v118 @ rdx_v7 (System.Single)+1A8] (should have been resolved before IL gen)");
				Texture texture2 = (Texture)0;
				((UnityEngine.Object)texture).m_CachedPtr = (IntPtr)0;
				object obj5 = default(object);
				object obj6 = default(object);
				*((Rect, Rect)*)(nint)texture = ((Rect)(&obj5), (Rect)(&obj6));
				return ((Rect, Rect))texture;
			}
		}
		return ((Rect, Rect))new NullReferenceException();
	}

	private unsafe static (Rect, Rect) ScaleAndCropToFill(Texture texture, Rect rect)
	{
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Expected O, but got Unknown
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Expected O, but got Unknown
		//IL_00a7: Expected O, but got I4
		//IL_00c6: Expected O, but got Ref
		//IL_00c6: Expected O, but got Ref
		//IL_00c1: Expected native int or pointer, but got O
		if ((object)rect != null)
		{
			float xMin = rect.m_XMin;
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v61 @ rdx_v1 (System.Single)+188] (should have been resolved before IL gen)");
			float xMin2 = rect.m_XMin;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [methodInfo @ r8 (Il2CppMethodInfo)+8]");
			object obj2 = default(object);
			object obj = 0 / obj2;
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v72 @ rdx_v3 (System.Single)+1A8] (should have been resolved before IL gen)");
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [methodInfo @ r8 (Il2CppMethodInfo)+C]");
			object obj4 = default(object);
			object obj3 = 0 / obj4;
			if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj) > System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj3))
			{
			}
			Texture texture2 = (Texture)0;
			((UnityEngine.Object)texture).m_CachedPtr = (IntPtr)0;
			object obj5 = default(object);
			object obj6 = default(object);
			*((Rect, Rect)*)(nint)texture = ((Rect)(&obj5), (Rect)(&obj6));
			return ((Rect, Rect))texture;
		}
		return ((Rect, Rect))new NullReferenceException();
	}

	private unsafe static (Rect, Rect) TexelSized(Texture texture, Vector2 center, float pixelsPerMeter)
	{
		//IL_0058: Expected O, but got Ref
		if ((object)center != null)
		{
			float x = center.x;
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v46 @ rdx_v1 (System.Single)+188] (should have been resolved before IL gen)");
			float x2 = center.x;
			object obj = default(object);
			object obj2 = default(object);
			float h = (float)obj / (float)obj2;
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v55 @ rdx_v3 (System.Single)+1A8] (should have been resolved before IL gen)");
			object obj3 = default(object);
			float w = default(float);
			(Rect, Rect) tuple = SimpleRect((Vector2)(&obj3), w, h);
			Texture texture2 = (Texture)tuple;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v87 @ rax_v8 (System.ValueTuple`2<UnityEngine.Rect, UnityEngine.Rect>)+10]");
			((UnityEngine.Object)texture).m_CachedPtr = (IntPtr)0;
			return ((Rect, Rect))texture;
		}
		return ((Rect, Rect))new NullReferenceException();
	}

	static TexturePlacement()
	{
		//IL_0016: Expected O, but got I
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206E40]");
		fitUvs = (Rect)0;
	}
}
