using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Kamgam.UGUIComponentsForSettings;

public class RandomColorUGUI : MonoBehaviour
{
	public delegate void OnColorChangedDelegate(Color color);

	public Image ColorImage;

	public UnityEvent<Color> OnColorChangedEvent;

	public OnColorChangedDelegate OnColorChanged;

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
			//IL_0014: Expected O, but got F4
			//IL_0028: Expected O, but got Ref
			//IL_0046: Expected O, but got F4
			//IL_0063: Expected O, but got Ref
			object obj2 = default(object);
			object obj = obj2 - obj2;
			float num = value.r - (float)_color;
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
				Color color = default(Color);
				ColorImage.color = (Color)(&color);
				bool flag = OnColorChangedEvent == null;
				color = (Color)value.r;
				if (!flag)
				{
					float num6 = default(float);
					OnColorChangedEvent.Invoke((Color)(&num6));
					color = _color;
				}
				OnColorChangedDelegate onColorChanged = OnColorChanged;
				if (OnColorChanged != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: v133.invoke_impl (System.IntPtr) (should have been resolved before IL gen)");
				}
			}
		}
	}

	public unsafe void Randomize()
	{
		//IL_002f: Expected O, but got F4
		//IL_0043: Expected O, but got Ref
		//IL_0079: Expected O, but got Ref
		//IL_0083: Expected F4, but got O
		float value = Random.value;
		float value2 = Random.value;
		float value3 = Random.value;
		float num = value - (float)_color;
		object obj = default(object);
		float num2 = value2 - (float)obj;
		float num3 = value3 - (float)obj;
		float num4 = 1f - (float)obj;
		float num5 = num2 * num2;
		float num6 = num * num;
		float num7 = num3 * num3;
		float num8 = num5 + num6;
		float num9 = num4 * num4;
		float num10 = num8 + num7;
		float num11 = num10 + num9;
		if (!(9.9999994E-11f > num11))
		{
			_color = (Color)value;
			float num12 = default(float);
			ColorImage.color = (Color)(&num12);
			bool flag = OnColorChangedEvent == null;
			num12 = value;
			if (!flag)
			{
				OnColorChangedEvent.Invoke((Color)(&num12));
				num12 = (float)_color;
			}
			OnColorChangedDelegate onColorChanged = OnColorChanged;
			if (OnColorChanged != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: v146.invoke_impl (System.IntPtr) (should have been resolved before IL gen)");
			}
		}
	}

	protected unsafe void updateColorImage(Color color)
	{
		//IL_000f: Expected O, but got Ref
		object obj = default(object);
		ColorImage.color = (Color)(&obj);
	}
}
