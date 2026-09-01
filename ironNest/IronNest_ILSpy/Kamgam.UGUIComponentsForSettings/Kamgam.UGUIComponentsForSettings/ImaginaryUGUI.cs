using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

namespace Kamgam.UGUIComponentsForSettings;

public class ImaginaryUGUI : MaskableGraphic
{
	public bool Circular;

	public float Radius;

	public override bool Raycast(Vector2 sp, Camera eventCamera)
	{
		//IL_0143: Expected O, but got I4
		//IL_0042: Invalid comparison between I4 and F4
		//IL_01a3: Invalid comparison between F4 and O
		//IL_01c1: Invalid comparison between F4 and I4
		//IL_0122: Expected I4, but got O
		bool flag = base.Raycast(sp, eventCamera);
		object obj = Circular & flag;
		if (obj == null)
		{
			goto IL_0160;
		}
		RectTransform rect = base.rectTransform;
		bool flag2 = RectTransformUtility.ScreenPointToLocalPointInRectangle(rect, sp, eventCamera, out var localPoint);
		float num = Radius;
		if (!(0f < Radius))
		{
			RectTransform rectTransform = base.rectTransform;
			if ((object)rectTransform != null)
			{
				Rect rect2 = rectTransform.rect;
				RectTransform rectTransform2 = base.rectTransform;
				if ((object)rectTransform2 != null)
				{
					float num2 = rectTransform2.rect.m_Height * 0.5f;
					object obj2 = default(object);
					num = (float)obj2 * 0.5f;
					if (num > num2)
					{
						num = num2;
					}
					goto IL_0165;
				}
			}
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
		}
		goto IL_0165;
		IL_0160:
		return flag;
		IL_0165:
		object obj4 = default(object);
		object obj3 = obj4 * obj4;
		object obj5 = localPoint * localPoint;
		float num3 = num * num;
		object obj6 = obj3 + obj5;
		bool flag3 = System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)num3) < System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj6);
		float num4 = num3 - (float)obj6;
		bool flag4 = num4 == 0f;
		bool flag5 = !flag3;
		bool flag6 = !flag4;
		flag = flag6 & flag5;
		goto IL_0160;
	}

	protected override void OnPopulateMesh(VertexHelper vh)
	{
		vh.Clear();
	}
}
