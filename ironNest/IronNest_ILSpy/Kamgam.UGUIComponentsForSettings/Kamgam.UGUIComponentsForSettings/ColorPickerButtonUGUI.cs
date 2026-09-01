using UnityEngine;
using UnityEngine.UI;

namespace Kamgam.UGUIComponentsForSettings;

public class ColorPickerButtonUGUI : MonoBehaviour
{
	public Image ColorImage;

	protected Color _color;

	public unsafe Color Color
	{
		get
		{
			//IL_000f: Expected F4, but got O
			//IL_000a: Expected native int or pointer, but got O
			Color color = default(Color);
			((Color*)(nint)color)->r = (float)_color;
			return color;
		}
		set
		{
			//IL_00cc: Expected O, but got F4
			//IL_00e0: Expected O, but got Ref
			object obj2 = default(object);
			object obj = obj2 - obj2;
			float num = (float)_color - value.r;
			object obj3 = obj2 - obj2;
			object obj4 = obj2 - obj2;
			object obj5 = obj * obj;
			float num2 = num * num;
			object obj6 = obj3 * obj3;
			float num3 = (float)obj5 + num2;
			object obj7 = obj4 * obj4;
			float num4 = num3 + (float)obj6;
			float num5 = num4 + (float)obj7;
			if (!(9.9999994E-11f > num5))
			{
				_color = (Color)value.r;
				object obj8 = default(object);
				ColorImage.color = (Color)(&obj8);
			}
		}
	}

	public unsafe void Start()
	{
		//IL_000f: Expected O, but got Ref
		object obj = default(object);
		ColorImage.color = (Color)(&obj);
	}

	protected unsafe void updateImageColor()
	{
		//IL_000f: Expected O, but got Ref
		object obj = default(object);
		ColorImage.color = (Color)(&obj);
	}
}
