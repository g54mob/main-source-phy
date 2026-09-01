using System;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Kamgam.UGUIComponentsForSettings;

public class SliderUGUI : MonoBehaviour
{
	public delegate void ValueChangedDelegate(float value);

	public float StepSize;

	public string ValueFormat;

	public bool UseMoveCommandToChangeValue;

	public SliderWithEventOverridesUGUI Slider;

	private Slider.SliderEvent OnValueChangedEvent;

	public ValueChangedDelegate OnValueChanged;

	[NonSerialized]
	protected float _lastSetValue;

	public TextMeshProUGUI TextTf;

	public TextMeshProUGUI ValueTf;

	public float MinValue
	{
		get
		{
			//IL_0034: Expected F4, but got I4
			if (Slider == null)
			{
				return 0f;
			}
			SliderWithEventOverridesUGUI slider = Slider;
			return ((Slider)slider).m_MinValue;
		}
		set
		{
			if (Slider != null)
			{
				Slider.minValue = value;
			}
		}
	}

	public float MaxValue
	{
		get
		{
			if (Slider == null)
			{
				return 1f;
			}
			SliderWithEventOverridesUGUI slider = Slider;
			return ((Slider)slider).m_MaxValue;
		}
		set
		{
			if (Slider != null)
			{
				Slider.maxValue = value;
			}
		}
	}

	public bool WholeNumbers
	{
		get
		{
			//IL_006a: Expected I4, but got O
			if (Slider != null)
			{
				SliderWithEventOverridesUGUI slider = Slider;
				if ((object)Slider != null)
				{
					return ((Slider)slider).m_WholeNumbers;
				}
				NullReferenceException ex = new NullReferenceException();
				return (byte)(int)ex != 0;
			}
			return false;
		}
		set
		{
			if (Slider != null)
			{
				Slider.wholeNumbers = value;
			}
		}
	}

	public float Value
	{
		get
		{
			//IL_007c: Expected I, but got O
			//IL_008c: Expected O, but got I
			//IL_009c: Expected O, but got I
			//IL_00ac: Expected F4, but got I4
			//IL_0058: Expected I, but got O
			if (Slider != null)
			{
				bool wholeNumbers = WholeNumbers;
				SliderWithEventOverridesUGUI slider = Slider;
				if (wholeNumbers)
				{
					nint num = (nint)slider;
					float value = slider.value;
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1800033E0");
					float result = default(float);
					return result;
				}
				nint num2 = (nint)slider;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v134 @ rdx_v3 (Il2CppClass<Kamgam.UGUIComponentsForSettings.SliderWithEventOverridesUGUI>)+418]");
				object obj = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v134 @ rdx_v3 (Il2CppClass<Kamgam.UGUIComponentsForSettings.SliderWithEventOverridesUGUI>)+420]");
				object obj2 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v85 @ rax_v6 (should have been resolved before IL gen)");
			}
			return 0f;
		}
		set
		{
			//IL_0044: Unknown result type (might be due to invalid IL or missing references)
			//IL_0049: Expected O, but got Unknown
			//IL_0052: Invalid comparison between F4 and O
			//IL_01bb: Unknown result type (might be due to invalid IL or missing references)
			//IL_01c0: Expected O, but got Unknown
			//IL_01c9: Invalid comparison between F4 and O
			//IL_0145: Unknown result type (might be due to invalid IL or missing references)
			//IL_014a: Expected O, but got Unknown
			//IL_0153: Invalid comparison between O and F4
			//IL_0111: Expected F4, but got I4
			if (!(Slider != null))
			{
				return;
			}
			float num = _lastSetValue - value;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
			object obj = num & 0;
			if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)Mathf.Epsilon) >= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj))
			{
				return;
			}
			bool wholeNumbers = WholeNumbers;
			bool flag = !wholeNumbers;
			float value2 = value;
			if (!flag)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1800033E0");
				value2 = value;
			}
			float num2 = ConvertToStepValue(value2);
			float minValue = MinValue;
			float maxValue = MaxValue;
			float num3;
			if (!(minValue > num2))
			{
				bool flag2 = !(num2 > maxValue);
				num3 = num2;
				if (!flag2)
				{
					num3 = maxValue;
				}
			}
			else
			{
				num3 = minValue;
			}
			float num4 = num3;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
			object obj2 = num4 & 0;
			if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)0.0001f) > System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj2))
			{
				num3 = 0f;
			}
			float value3 = Slider.value;
			float num5 = 0.0001f - num3;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
			object obj3 = num5 & 0;
			if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj3) > System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)1E-45f))
			{
				Slider.value = num3;
			}
			_lastSetValue = num3;
			UpdateText();
		}
	}

	public int IntValue
	{
		get
		{
			//IL_004b: Expected I4, but got O
			if ((object)Slider != null)
			{
				float value = Slider.value;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803B9370");
				int result = default(int);
				return result;
			}
			NullReferenceException ex = new NullReferenceException();
			return (int)ex;
		}
	}

	public string Text
	{
		get
		{
			//IL_0054: Expected I, but got O
			if (TextTf != null)
			{
				TextMeshProUGUI textTf = TextTf;
				if ((object)TextTf == null)
				{
					return (string)(object)new NullReferenceException();
				}
				nint num = (nint)textTf;
				Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: [v73 @ rdx_v2 (Il2CppClass<TMPro.TextMeshProUGUI>)+548] (should have been resolved before IL gen)");
			}
			return null;
		}
		set
		{
			string text = ((!(TextTf != null)) ? null : TextTf.text);
			if (value != text && TextTf != null)
			{
				TextTf.text = value;
			}
		}
	}

	public void UpdateText()
	{
		if (ValueTf != null)
		{
			if (string.IsNullOrEmpty(ValueFormat))
			{
				float value = Slider.value;
				float num = default(float);
				string text = num.ToString();
				ValueTf.text = text;
			}
			else
			{
				float value2 = Slider.value;
				Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
				object arg = default(object);
				string text2 = string.Format(ValueFormat, arg);
				ValueTf.text = text2;
			}
		}
	}

	public void Start()
	{
		SliderWithEventOverridesUGUI slider = Slider;
		UnityAction<float> call = onValueChangedHandler;
		((Slider)slider).m_OnValueChanged.AddListener(call);
		SliderWithEventOverridesUGUI slider2 = Slider;
		Func<AxisEventData, bool> onMoveOverride = onMove;
		slider2.OnMoveOverride = onMoveOverride;
		float minValue = MinValue;
		Slider.minValue = minValue;
		float maxValue = MaxValue;
		Slider.maxValue = maxValue;
		bool wholeNumbers = WholeNumbers;
		Slider.wholeNumbers = wholeNumbers;
		float value = Value;
		Slider.value = value;
		Cpp2ILHelpers.NoteDecompilerIssue("Invalid instruction: 174 Invalid \"Jump target not found in method: 0x180A70F30\"");
		throw new NullReferenceException();
	}

	private unsafe void onValueChangedHandler(float value)
	{
		//IL_001e: Expected F4, but got Ref
		Value = value;
		if (OnValueChangedEvent != null)
		{
			float value2 = Value;
			object obj = default(object);
			OnValueChangedEvent.Invoke((nint)(&obj));
		}
		ValueChangedDelegate onValueChanged = OnValueChanged;
		if (OnValueChanged != null)
		{
			float value3 = Value;
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: v62.invoke_impl (System.IntPtr) (should have been resolved before IL gen)");
		}
	}

	public bool onMove(AxisEventData eventData)
	{
		//IL_01b5: Expected I4, but got O
		GameObject gameObject = base.gameObject;
		if ((object)gameObject != null)
		{
			if (gameObject.activeInHierarchy)
			{
				if (eventData == null)
				{
					goto IL_01a7;
				}
				float value2;
				object obj = default(object);
				if (eventData._003CmoveDir_003Ek__BackingField == MoveDirection.Left)
				{
					if ((UseMoveCommandToChangeValue ? MoveDirection.Up : MoveDirection.Left) == eventData._003CmoveDir_003Ek__BackingField)
					{
						goto IL_00b5;
					}
					if ((object)Slider == null)
					{
						goto IL_01a7;
					}
					float value = Slider.value;
					float num = StepSize * -1f;
					value2 = num + (float)obj;
				}
				else
				{
					if (eventData._003CmoveDir_003Ek__BackingField != MoveDirection.Right || !UseMoveCommandToChangeValue)
					{
						goto IL_00b5;
					}
					if ((object)Slider == null)
					{
						goto IL_01a7;
					}
					float value3 = Slider.value;
					float num2 = (float)obj + StepSize;
					value2 = num2;
				}
				Value = value2;
			}
			return false;
		}
		goto IL_01a7;
		IL_01a7:
		NullReferenceException ex = new NullReferenceException();
		return (byte)(int)ex != 0;
		IL_00b5:
		return true;
	}

	public float ConvertToStepValue(float value)
	{
		//IL_005d: Invalid comparison between F8 and I4
		//IL_01ae: Invalid comparison between F8 and I4
		//IL_0099: Expected O, but got F8
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Expected O, but got Unknown
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Expected O, but got Unknown
		//IL_01e8: Expected O, but got I4
		//IL_03f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f7: Expected F4, but got Unknown
		//IL_02b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b6: Expected F4, but got Unknown
		//IL_043c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0441: Expected F4, but got Unknown
		//IL_04a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a5: Expected F4, but got Unknown
		//IL_0303: Unknown result type (might be due to invalid IL or missing references)
		//IL_0308: Expected F4, but got Unknown
		//IL_04f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f7: Expected F4, but got Unknown
		//IL_0355: Unknown result type (might be due to invalid IL or missing references)
		//IL_035a: Expected F4, but got Unknown
		//IL_0544: Unknown result type (might be due to invalid IL or missing references)
		//IL_0549: Expected F4, but got Unknown
		//IL_03a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ac: Expected F4, but got Unknown
		//IL_0184: Invalid comparison between O and F8
		float minValue = MinValue;
		float maxValue = MaxValue;
		float minValue2 = MinValue;
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtss2sd xmm0,xmm10\"");
		double num = Math.Ceiling(0.0);
		double num2 = num + 1.0;
		float stepSize;
		float num4;
		float num5;
		float num30;
		float result;
		if (!(num2 < 8.0))
		{
			stepSize = StepSize;
			double num3 = num2 - 8.0;
			object obj = num3 >> 3;
			object obj2 = obj + 1;
			object obj3 = obj2 * 8;
			num4 = minValue;
			num5 = value;
			float num6 = 3.4028235E+38f;
			bool flag2;
			do
			{
				float num7 = value - num4;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
				float num8 = num7 & 0;
				if (num6 > num8)
				{
					num5 = num4;
					num6 = num8;
				}
				float num9 = num4 + stepSize;
				float num10 = value - num9;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
				float num11 = num10 & 0;
				if (num6 > num11)
				{
					num5 = num9;
					num6 = num11;
				}
				float num12 = num9 + stepSize;
				float num13 = value - num12;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
				float num14 = num13 & 0;
				if (num6 > num14)
				{
					num5 = num12;
					num6 = num14;
				}
				float num15 = num12 + stepSize;
				float num16 = value - num15;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
				float num17 = num16 & 0;
				if (num6 > num17)
				{
					num5 = num15;
					num6 = num17;
				}
				float num18 = num15 + stepSize;
				float num19 = value - num18;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
				float num20 = num19 & 0;
				if (num6 > num20)
				{
					num5 = num18;
					num6 = num20;
				}
				float num21 = num18 + stepSize;
				float num22 = value - num21;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
				float num23 = num22 & 0;
				if (num6 > num23)
				{
					num5 = num21;
					num6 = num23;
				}
				float num24 = num21 + stepSize;
				float num25 = value - num24;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
				float num26 = num25 & 0;
				if (num6 > num26)
				{
					num5 = num24;
					num6 = num26;
				}
				float num27 = num24 + stepSize;
				float num28 = value - num27;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
				float num29 = num28 & 0;
				bool flag = !(num6 > num29);
				num30 = num6;
				if (!flag)
				{
					num5 = num27;
					num30 = num29;
				}
				num4 = num27 + stepSize;
				flag2 = num6 != num29;
				num6 = num30;
			}
			while (flag2);
			if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj3) >= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)num2))
			{
				result = num5;
				goto IL_0220;
			}
		}
		else
		{
			bool flag3 = !(num2 > 0.0);
			float num29 = value;
			result = value;
			if (flag3)
			{
				goto IL_0220;
			}
			stepSize = StepSize;
			object obj3 = 0;
			num4 = minValue;
			num5 = value;
			num30 = 3.4028235E+38f;
		}
		bool flag5;
		do
		{
			float num31 = value - num4;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
			float num29 = num31 & 0;
			bool flag4 = !(num30 > num29);
			float num32 = num30;
			if (!flag4)
			{
				num5 = num4;
				num32 = num29;
			}
			num4 += stepSize;
			flag5 = num30 != num29;
			result = num5;
			num30 = num32;
		}
		while (flag5);
		goto IL_0220;
		IL_0220:
		if (Slider != null)
		{
			SliderWithEventOverridesUGUI slider = Slider;
			if (((Slider)slider).m_WholeNumbers)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1800033E0");
			}
		}
		return result;
	}

	public void Increase()
	{
		float value = Slider.value;
		object obj = default(object);
		float value2 = (float)obj + StepSize;
		Value = value2;
	}

	public void Decrease()
	{
		float value = Slider.value;
		float num = StepSize * -1f;
		object obj = default(object);
		float value2 = num + (float)obj;
		Value = value2;
	}

	public void Step(int steps)
	{
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Expected O, but got Unknown
		float value = Slider.value;
		object obj = steps * StepSize;
		object obj2 = default(object);
		float value2 = (float)obj + (float)obj2;
		Value = value2;
	}

	public SliderUGUI()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3F423]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		StepSize = 10f;
		ValueFormat = "{0:N0} %";
		UseMoveCommandToChangeValue = true;
		_lastSetValue = -3.4028235E+38f;
		base._002Ector();
	}
}
