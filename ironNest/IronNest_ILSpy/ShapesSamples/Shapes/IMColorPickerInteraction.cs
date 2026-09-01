using System;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using UnityEngine;

namespace Shapes;

public class IMColorPickerInteraction : MonoBehaviour
{
	private enum ColorPickerElement
	{
		None,
		HueStrip,
		Rectangle
	}

	public IMColorPickerRenderer picker;

	private ColorPickerElement currentInteraction;

	private unsafe void Update()
	{
		//IL_0050: Expected O, but got Ref
		//IL_009f: Expected O, but got Ref
		Camera main = Camera.main;
		if (main != null)
		{
			Camera main2 = Camera.main;
			Vector3 mousePosition = Input.mousePosition;
			object obj = default(object);
			Ray ray = main2.ScreenPointToRay((Vector3)(&obj));
			bool mouseButtonDown = Input.GetMouseButtonDown(0);
			bool mouseButton = Input.GetMouseButton(0);
			bool mouseButtonUp = Input.GetMouseButtonUp(0);
			Vector3 vector = default(Vector3);
			bool onRelease = default(bool);
			RaycastInteract((Ray)(&vector), mouseButtonDown, mouseButton, onRelease);
		}
	}

	private void OnDisable()
	{
		currentInteraction = ColorPickerElement.None;
	}

	public unsafe void RaycastInteract(Ray ray, bool onPress, bool whileHeld, bool onRelease)
	{
		//IL_002b: Expected O, but got Ref
		//IL_0041: Expected O, but got F4
		//IL_003c: Expected native int or pointer, but got O
		//IL_0067: Expected O, but got Ref
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Expected O, but got Unknown
		//IL_008b: Expected O, but got F4
		//IL_0086: Expected native int or pointer, but got O
		//IL_01cd: Expected I, but got O
		//IL_01f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fb: Expected O, but got Unknown
		//IL_0218: Expected O, but got I
		//IL_0261: Unknown result type (might be due to invalid IL or missing references)
		//IL_0266: Expected O, but got Unknown
		//IL_029d: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a2: Expected O, but got Unknown
		//IL_02b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bc: Expected O, but got Unknown
		//IL_02c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ca: Expected O, but got Unknown
		//IL_0330: Unknown result type (might be due to invalid IL or missing references)
		//IL_0335: Expected O, but got Unknown
		//IL_02f2: Invalid comparison between F4 and O
		//IL_00b1: Expected O, but got I4
		//IL_0111: Expected O, but got I4
		object obj = default(object);
		if (obj == null)
		{
			Transform transform = base.transform;
			Vector3 vector2 = default(Vector3);
			Vector3 vector = transform.InverseTransformPoint((Vector3)(&vector2));
			((Ray*)(nint)ray)->m_Origin = (Vector3)vector.x;
			_ = vector.z;
			Transform transform2 = base.transform;
			Vector3 vector3 = transform2.InverseTransformDirection((Vector3)(&vector2));
			Vector3 vector4 = (Vector3)(ray + 12);
			((Ray*)(nint)ray)->m_Direction = (Vector3)vector3.x;
			_ = vector3.z;
			((Vector3*)vector4)->Normalize();
			nint num = (nint)typeof(Vector3);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v259 @ rax_v14 (Il2CppClass<UnityEngine.Vector3>)+B8]");
			nint num2 = 0;
			vector2.Normalize();
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [ray @ rdx (UnityEngine.Ray)+10]");
			Vector2 vector5 = default(Vector2);
			object obj2 = 0 * vector5;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [ray @ rdx (UnityEngine.Ray)+14]");
			nint num3 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v261 @ rcx_v10 (Il2CppStaticFields<UnityEngine.Vector3>)+5C]");
			object obj3 = num3 * 0;
			object obj4 = vector5 * vector5;
			object obj5 = vector5 * vector2;
			object obj6 = obj2 + obj5;
			object obj7 = (object)ray.m_Origin * (object)vector2;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v261 @ rcx_v10 (Il2CppStaticFields<UnityEngine.Vector3>)+5C]");
			object obj8 = vector5 * 0;
			object obj9 = obj4 + obj7;
			object obj10 = obj6 + obj3;
			object obj11 = obj9 + obj8;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C80]");
			object obj12 = obj11 ^ 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
			object obj13 = obj10 & 0;
			object obj14 = 0 - obj10;
			if ((nint)obj13 < 0)
			{
				obj13 = 0;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
			object obj15 = obj14 & 0;
			float num4 = Mathf.Epsilon * 8f;
			float num5 = (float)obj13 * 1E-06f;
			if (num5 < num4)
			{
				num5 = num4;
			}
			if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)num5) <= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj15))
			{
				object obj16 = obj12 / obj10;
				bool flag = (nint)obj16 < 0;
				bool flag2 = obj16 == null;
				bool flag3 = !flag;
				bool flag4 = !flag2;
				object obj17 = flag4 & flag3;
				if (obj17 != null)
				{
					if (onPress)
					{
						ColorPickerElement pickerElementAt = GetPickerElementAt(vector5);
						currentInteraction = pickerElementAt;
					}
					if (whileHeld && currentInteraction != ColorPickerElement.None)
					{
						UpdatePickerColor(vector5);
					}
				}
			}
		}
		object obj18 = default(object);
		if (obj18 != null)
		{
			currentInteraction = ColorPickerElement.None;
		}
	}

	private void UpdatePickerColor(Vector2 pt)
	{
		//IL_0158: Invalid comparison between I4 and F4
		//IL_0167: Expected O, but got I4
		//IL_0308: Expected F4, but got I4
		//IL_00a0: Expected O, but got I4
		//IL_0190: Expected O, but got I4
		//IL_01be: Expected F4, but got I4
		//IL_02eb: Invalid comparison between O and F4
		//IL_00d6: Invalid comparison between O and F4
		//IL_01b0: Expected O, but got I4
		//IL_01fa: Expected F4, but got I4
		//IL_021e: Invalid comparison between O and F4
		//IL_0269: Expected F4, but got I4
		object obj = default(object);
		object obj2;
		float num4;
		if (currentInteraction != ColorPickerElement.HueStrip)
		{
			if (currentInteraction != ColorPickerElement.Rectangle)
			{
				return;
			}
			Rect quadRect = picker.QuadRect;
			float num = (float)obj + quadRect.m_XMin;
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"jp short 0000000181072E1Fh\"");
			if (quadRect.m_XMin == num)
			{
				obj2 = 0;
			}
			else
			{
				float num2 = num - quadRect.m_XMin;
				float num3 = (float)pt - quadRect.m_XMin;
				num4 = num3 / num2;
				bool flag = 0f > num4;
				obj2 = 0;
				if (!flag)
				{
					bool flag2 = !(num4 > 1f);
					obj2 = 0;
					if (!flag2)
					{
						num4 = 1f;
						obj2 = 0;
					}
					goto IL_030d;
				}
			}
			num4 = 0f;
			goto IL_030d;
		}
		IMColorPickerRenderer iMColorPickerRenderer = picker;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033D7B0");
		object obj3 = default(object);
		float num5 = (float)obj3 / ((float)Math.PI * 2f);
		float num6 = MathF.Floor(num5);
		float hue = num5 - num6;
		iMColorPickerRenderer.hue = hue;
		return;
		IL_02d9:
		IMColorPickerRenderer iMColorPickerRenderer2 = picker;
		if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj2) <= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)num4))
		{
			if (num4 > 1f)
			{
				num4 = 1f;
			}
		}
		else
		{
			num4 = 0f;
		}
		iMColorPickerRenderer2.saturation = num4;
		IMColorPickerRenderer iMColorPickerRenderer3 = picker;
		float num7;
		if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj2) <= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)num7))
		{
			if (num7 > 1f)
			{
				num7 = 1f;
			}
		}
		else
		{
			num7 = 0f;
		}
		iMColorPickerRenderer3.value = num7;
		return;
		IL_030d:
		object obj4 = obj + obj;
		bool flag3 = obj == obj4;
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"jp short 0000000181072DFEh\"");
		if (!flag3)
		{
			object obj5 = obj4 - obj;
			object obj6 = obj3 - obj;
			num7 = (float)obj6 / (float)obj5;
			if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj2) <= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)num7))
			{
				if (num7 > 1f)
				{
					num7 = 1f;
				}
				goto IL_02d9;
			}
		}
		num7 = 0f;
		goto IL_02d9;
	}

	private ColorPickerElement GetPickerElementAt(Vector2 pt)
	{
		//IL_0270: Expected I, but got O
		//IL_0237: Expected I4, but got O
		//IL_00f3: Invalid comparison between I4 and F4
		//IL_0149: Invalid comparison between F4 and I4
		//IL_0249: Invalid comparison between O and F4
		//IL_019a: Invalid comparison between F4 and O
		nint num = (nint)typeof(Math);
		object obj2 = default(object);
		object obj = obj2 * obj2;
		object obj3 = pt * pt;
		double d = (double)obj + (double)obj3;
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"ucomisd xmm0,xmm2\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v47 @ rcx_v3 (Il2CppClass<System.Math>)+E4]");
		if ((nint)0 <= (nint)0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"sqrtpd xmm0,xmm2\"");
		}
		else
		{
			double num2 = Math.Sqrt(d);
		}
		IMColorPickerRenderer iMColorPickerRenderer = picker;
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtsd2ss xmm2,xmm0\"");
		ColorPickerElement result;
		if ((object)picker != null)
		{
			float num3 = iMColorPickerRenderer.hueStripThickness * 0.5f;
			float num4 = 1f - num3;
			float num5 = num4 - iMColorPickerRenderer.outline;
			if (!(0f < num5))
			{
				float num6 = iMColorPickerRenderer.hueStripThickness * 0.5f;
				float num7 = num6 + 1f;
				float num8 = num7 + iMColorPickerRenderer.outline;
				if (!(num8 < 0f))
				{
					return ColorPickerElement.HueStrip;
				}
			}
			Rect quadRect = picker.QuadRect;
			if (System.Runtime.CompilerServices.Unsafe.As<Vector2, UIntPtr>(ref pt) >= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)quadRect.m_XMin))
			{
				object obj4 = default(object);
				float num9 = (float)obj4 + quadRect.m_XMin;
				if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)num9) > System.Runtime.CompilerServices.Unsafe.As<Vector2, UIntPtr>(ref pt) && System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj2) >= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj4))
				{
					object obj5 = obj4 + obj4;
					bool flag = System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj5) <= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj2);
					result = ColorPickerElement.None;
					if (!flag)
					{
						result = ColorPickerElement.Rectangle;
					}
					goto IL_025d;
				}
			}
			result = ColorPickerElement.None;
			goto IL_025d;
		}
		NullReferenceException ex = new NullReferenceException();
		return (ColorPickerElement)ex;
		IL_025d:
		return result;
	}

	private bool HueStripContains(Vector2 pt)
	{
		//IL_0184: Expected I, but got O
		//IL_0176: Expected I4, but got O
		//IL_00f3: Invalid comparison between I4 and F4
		//IL_0152: Invalid comparison between F4 and I4
		nint num = (nint)typeof(Math);
		object obj2 = default(object);
		object obj = obj2 * obj2;
		object obj3 = pt * pt;
		double d = (double)obj + (double)obj3;
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"ucomisd xmm0,xmm2\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v28 @ rcx_v2 (Il2CppClass<System.Math>)+E4]");
		if ((nint)0 <= (nint)0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"sqrtpd xmm0,xmm2\"");
		}
		else
		{
			double num2 = Math.Sqrt(d);
		}
		IMColorPickerRenderer iMColorPickerRenderer = picker;
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtsd2ss xmm2,xmm0\"");
		if ((object)picker != null)
		{
			float num3 = iMColorPickerRenderer.hueStripThickness * 0.5f;
			float num4 = 1f - num3;
			float num5 = num4 - iMColorPickerRenderer.outline;
			if (0f < num5)
			{
				return false;
			}
			float num6 = iMColorPickerRenderer.hueStripThickness * 0.5f;
			float num7 = num6 + 1f;
			float num8 = num7 + iMColorPickerRenderer.outline;
			bool flag = num8 < 0f;
			return !flag;
		}
		NullReferenceException ex = new NullReferenceException();
		return (byte)(int)ex != 0;
	}
}
