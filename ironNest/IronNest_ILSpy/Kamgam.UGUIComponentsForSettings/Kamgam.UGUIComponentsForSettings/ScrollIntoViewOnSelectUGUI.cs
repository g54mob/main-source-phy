using System;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Kamgam.UGUIComponentsForSettings;

public class ScrollIntoViewOnSelectUGUI : MonoBehaviour, ISelectHandler, IEventSystemHandler
{
	public bool Enabled = true;

	public Vector4 MarginTRBL;

	public unsafe void OnSelect(BaseEventData eventData)
	{
		//IL_00e3: Expected O, but got Ref
		if (!Enabled)
		{
			return;
		}
		Transform transform = base.transform;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696530");
		UnityEngine.Object obj = default(UnityEngine.Object);
		if (!(obj != null))
		{
			return;
		}
		Transform transform2 = base.transform;
		bool flag = (object)transform2 == null;
		RectTransform child = null;
		if (!flag)
		{
			bool flag2 = (object)transform2.GetType() != typeof(RectTransform);
			child = null;
			if (!flag2)
			{
				child = (RectTransform)transform2;
			}
		}
		Vector4 vector = default(Vector4);
		BringChildIntoView((ScrollRect)obj, child, (Vector4)(&vector));
	}

	public unsafe static void BringChildIntoView(ScrollRect instance, RectTransform child, Vector4 margin)
	{
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Expected O, but got Unknown
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Expected O, but got Unknown
		//IL_018d: Expected O, but got I4
		//IL_01c0: Expected O, but got I4
		//IL_030d: Invalid comparison between O and F4
		//IL_01e5: Expected O, but got I4
		//IL_0228: Invalid comparison between F4 and O
		//IL_01f3: Expected O, but got I4
		//IL_0273: Expected O, but got Ref
		instance.m_Content.ForceUpdateRectTransforms();
		instance.m_Viewport.ForceUpdateRectTransforms();
		Rect rect = TransformRectFrom(instance.m_Viewport, child);
		float num = rect.m_XMin - margin.w;
		object obj = default(object);
		float num2 = (float)obj + rect.m_XMin;
		float num3 = num2 - num;
		float num4 = num3 + num;
		float num5 = num4 + margin.y;
		float num6 = num5 - num;
		object obj3 = default(object);
		object obj2 = obj3 - margin.z;
		object obj5 = default(object);
		object obj4 = obj3 + obj5;
		object obj6 = obj4 - obj2;
		object obj7 = obj6 + obj2;
		object obj8 = obj7 + margin.x;
		object obj9 = obj8 - obj2;
		Rect rect2 = instance.m_Viewport.rect;
		Vector3 localPosition = instance.m_Content.localPosition;
		float num7 = rect2.m_XMin - num;
		bool flag = !(num7 > 0.001f);
		float num8 = localPosition.x;
		float num9 = localPosition.x;
		object obj10 = 0;
		if (!flag)
		{
			num8 = localPosition.x + num7;
			num9 = num8;
			obj10 = 1;
		}
		float num10 = num6 + num;
		object obj11 = default(object);
		float num11 = (float)obj11 + rect2.m_XMin;
		float num12 = num11 - num10;
		if (-0.001f > num12)
		{
			float num13 = num8 + num12;
			num9 = num13;
			obj10 = 1;
		}
		object obj12 = obj11 - obj2;
		if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj12) > System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)0.001f))
		{
			obj10 = 1;
		}
		object obj13 = obj9 + obj2;
		object obj14 = obj11 + obj11;
		object obj15 = obj14 - obj13;
		if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)(-0.001f)) > System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj15) || obj10 != null)
		{
			instance.m_Content.localPosition = (Vector3)(&num9);
			instance.m_Content.ForceUpdateRectTransforms();
		}
	}

	public unsafe static Rect TransformRectFrom(Transform to, Transform from)
	{
		//IL_0008: Expected O, but got Ref
		//IL_001b: Expected O, but got Ref
		//IL_0038: Expected O, but got Ref
		//IL_0059: Expected O, but got I
		//IL_02a7: Expected F4, but got I4
		//IL_02e3: Expected native int or pointer, but got O
		//IL_008d: Expected O, but got I
		//IL_00f2: Expected O, but got I
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Expected O, but got Unknown
		//IL_011b: Expected O, but got I4
		//IL_0124: Expected O, but got I4
		//IL_0139: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Expected O, but got Unknown
		//IL_015d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0162: Expected O, but got Unknown
		//IL_01be: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c3: Expected O, but got Unknown
		//IL_01cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d1: Expected O, but got Unknown
		//IL_01d9: Expected O, but got F4
		//IL_01e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ec: Expected O, but got Unknown
		object obj2 = default(object);
		object obj = (object)(&obj2);
		object obj3 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 128));
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
		object obj4 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 144));
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rbp_v1+80]");
		float xMin;
		if ((UnityEngine.Object)0 != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rbp_v1+90]");
			if ((UnityEngine.Object)0 != null)
			{
				Vector3[] array = new Vector3[4];
				Vector3[] array2 = new Vector3[4];
				Matrix4x4 worldToLocalMatrix = to.worldToLocalMatrix;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rbp_v1+80]");
				((RectTransform)0).GetWorldCorners(array);
				object obj5 = (object)array - (object)array2;
				object obj6 = array2 + 32;
				object obj7 = 0;
				object obj8 = 0;
				object obj10 = default(object);
				float num2 = default(float);
				object obj11 = default(object);
				object obj13 = default(object);
				object obj14 = default(object);
				while (true)
				{
					if ((nint)obj8 < array.Length)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v125 @ r9_v5+v151 @ rdx_v14]");
						object obj9 = 0 * obj10;
						float num = num2 * (float)obj11;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v125 @ r9_v5+8+v151 @ rdx_v14]");
						object obj12 = 0 * obj13;
						float num3 = (float)obj9 + num;
						float num4 = num3 + (float)obj12;
						float num5 = num4 + (float)obj14;
						if ((nint)obj8 < array2.Length)
						{
							obj8++;
							obj7++;
							obj6 = num2;
							obj6 += 12;
							if ((nint)obj7 < 4)
							{
								continue;
							}
							if (array2.Length > 0 && array2.Length > 2 && array2.Length > 1 && array2.Length > 0)
							{
								break;
							}
						}
					}
					return (Rect)new IndexOutOfRangeException();
				}
				xMin = num2;
				goto IL_02db;
			}
		}
		xMin = 0f;
		goto IL_02db;
		IL_02db:
		Rect rect = default(Rect);
		((Rect*)(nint)rect)->m_XMin = xMin;
		return rect;
	}
}
