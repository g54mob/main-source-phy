using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Kamgam.UGUIComponentsForSettings;

public class StepperUGUI : MonoBehaviour
{
	public delegate void OnValueChangedDelegate(float value);

	public UnityEvent<float> OnValueChangedEvent;

	public OnValueChangedDelegate OnValueChanged;

	public float MinValue;

	public float MaxValue = 100f;

	public float StepSize = 10f;

	public bool WholeNumbers = true;

	public GameObject StepTemplate;

	public GameObject StepsContainer;

	[NonSerialized]
	protected List<StepperStepConsoleUGUI> _steps;

	public string ValueFormat;

	public bool DisableButtons;

	public Button DecreaseButton;

	public Button IncreaseButton;

	protected AutoNavigationOverrides decreaseButtonNavigationOverrides;

	protected AutoNavigationOverrides increaseButtonNavigationOverrides;

	protected float _value;

	public TextMeshProUGUI TextTf;

	public TextMeshProUGUI ValueTf;

	protected bool _enableButtonControls;

	protected AutoNavigationOverrides _autoNavigationOverrides;

	protected Selectable _selectable;

	public bool ShowSteps
	{
		get
		{
			bool flag = StepsContainer != null;
			if (!flag)
			{
				return flag;
			}
			return StepTemplate != null;
		}
	}

	public float StepCountFloat
	{
		get
		{
			float num = MaxValue - MinValue;
			return num / StepSize;
		}
	}

	public int StepCount
	{
		get
		{
			//IL_004b: Expected I4, but got F8
			float num = MaxValue - MinValue;
			float num2 = num - 0.001f;
			float num3 = num2 / StepSize;
			double num4 = Math.Ceiling(num3);
			return (int)num4;
		}
	}

	public AutoNavigationOverrides DecreaseButtonNavigationOverrides
	{
		get
		{
			if (DecreaseButton != null)
			{
				if (decreaseButtonNavigationOverrides == null)
				{
					if ((object)DecreaseButton == null)
					{
						return (AutoNavigationOverrides)(object)new NullReferenceException();
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
					AutoNavigationOverrides autoNavigationOverrides = default(AutoNavigationOverrides);
					decreaseButtonNavigationOverrides = autoNavigationOverrides;
				}
				return decreaseButtonNavigationOverrides;
			}
			return null;
		}
	}

	public AutoNavigationOverrides IncreaseButtonNavigationOverrides
	{
		get
		{
			if (IncreaseButton != null)
			{
				if (increaseButtonNavigationOverrides == null)
				{
					if ((object)IncreaseButton == null)
					{
						return (AutoNavigationOverrides)(object)new NullReferenceException();
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
					AutoNavigationOverrides autoNavigationOverrides = default(AutoNavigationOverrides);
					increaseButtonNavigationOverrides = autoNavigationOverrides;
				}
				return increaseButtonNavigationOverrides;
			}
			return null;
		}
	}

	public float Value
	{
		get
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Invalid instruction: 12 Invalid \"Jump target not found in method: 0x1800033E0\"");
			return _value;
		}
		set
		{
			//IL_003c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0041: Expected O, but got Unknown
			//IL_004a: Invalid comparison between F4 and O
			float num = _value - value;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
			object obj = num & 0;
			if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)Mathf.Epsilon) < System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj))
			{
				updateValue(value);
				updateButtons();
			}
		}
	}

	public int IntValue
	{
		get
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803B9370");
			int result = default(int);
			return result;
		}
	}

	public string Text
	{
		get
		{
			//IL_0031: Expected I, but got O
			TextMeshProUGUI textTf = TextTf;
			if ((object)TextTf != null)
			{
				nint num = (nint)textTf;
				Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: [v12 @ rdx_v1 (Il2CppClass<TMPro.TextMeshProUGUI>)+548] (should have been resolved before IL gen)");
			}
			return (string)(object)new NullReferenceException();
		}
		set
		{
			string text = TextTf.text;
			if (value != text)
			{
				TextTf.text = value;
				updateButtons();
			}
		}
	}

	public bool EnableButtonControls
	{
		get
		{
			return _enableButtonControls;
		}
		set
		{
			AutoNavigationOverrides autoNavigationOverrides = AutoNavigationOverrides;
			if (autoNavigationOverrides != null)
			{
				AutoNavigationOverrides autoNavigationOverrides2 = AutoNavigationOverrides;
				autoNavigationOverrides2.BlockLeft = _enableButtonControls;
				AutoNavigationOverrides autoNavigationOverrides3 = AutoNavigationOverrides;
				autoNavigationOverrides3.BlockRight = _enableButtonControls;
			}
		}
	}

	public AutoNavigationOverrides AutoNavigationOverrides
	{
		get
		{
			if (_autoNavigationOverrides == null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
				AutoNavigationOverrides autoNavigationOverrides = default(AutoNavigationOverrides);
				_autoNavigationOverrides = autoNavigationOverrides;
			}
			return _autoNavigationOverrides;
		}
	}

	public Selectable Selectable
	{
		get
		{
			if (_selectable == null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
				Selectable selectable = default(Selectable);
				_selectable = selectable;
			}
			return _selectable;
		}
	}

	protected void updateValue(float value)
	{
		//IL_00b6: Expected I, but got O
		//IL_00c6: Expected O, but got I
		//IL_01fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0201: Expected I, but got Unknown
		//IL_0214: Expected O, but got I4
		//IL_0191: Invalid comparison between I4 and F8
		//IL_01aa: Expected I, but got O
		//IL_02ff: Expected O, but got I4
		//IL_0339: Unknown result type (might be due to invalid IL or missing references)
		//IL_033e: Expected O, but got Unknown
		//IL_0346: Unknown result type (might be due to invalid IL or missing references)
		//IL_034b: Expected O, but got Unknown
		//IL_0370: Invalid comparison between F8 and I4
		//IL_03bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c4: Expected O, but got Unknown
		//IL_03cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d1: Expected O, but got Unknown
		//IL_03f6: Invalid comparison between F8 and I4
		//IL_0420: Unknown result type (might be due to invalid IL or missing references)
		//IL_0425: Expected O, but got Unknown
		if (WholeNumbers)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1800033E0");
		}
		float num = ConvertToStepValue(value);
		float num2 = MinValue;
		float maxValue;
		if (!(MinValue > num))
		{
			num2 = MaxValue;
			bool flag = !(num > MaxValue);
			maxValue = MaxValue;
			if (flag)
			{
				goto IL_042a;
			}
		}
		maxValue = num2;
		num = num2;
		goto IL_042a;
		IL_042a:
		TextMeshProUGUI valueTf = ValueTf;
		_value = num;
		Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
		object arg = default(object);
		string text = string.Format(ValueFormat, arg);
		nint num3 = (nint)valueTf;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v107 @ r8_v4 (Il2CppClass<TMPro.TextMeshProUGUI>)+558]");
		object obj = 0;
		valueTf.text = text;
		if (!(StepsContainer != null) || !(StepTemplate != null))
		{
			return;
		}
		nint num8;
		List<StepperStepConsoleUGUI> list;
		int num9;
		if (_steps != null)
		{
			float num4 = MaxValue - MinValue;
			List<StepperStepConsoleUGUI> steps = _steps;
			float num5 = num4 - 0.001f;
			float num6 = num5 / StepSize;
			double num7 = Math.Ceiling(num6);
			bool flag2 = (double)steps._size == num7;
			list = null;
			num8 = (nint)typeof(Math);
			num9 = 0;
			if (flag2)
			{
				goto IL_0229;
			}
		}
		Transform container = StepsContainer.transform;
		int stepCount = StepCount;
		List<StepperStepConsoleUGUI> list2 = StepperStepConsoleUGUI.CreateSteps(container, StepTemplate, stepCount);
		num8 = (nint)(this + 80);
		_steps = list2;
		obj = 0;
		list = list2;
		num9 = stepCount;
		goto IL_0229;
		IL_0229:
		if (WholeNumbers)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1800033E0");
		}
		float num10 = StepSize * 0.5f;
		float num11 = MaxValue - num10;
		double num16;
		if (!(_value > num11))
		{
			float num12 = _value - MinValue;
			float num13 = StepSize * 0.499f;
			float num14 = num13 + num12;
			float num15 = num14 / StepSize;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803B9370");
			double num17 = default(double);
			num16 = num17;
		}
		else
		{
			float num18 = MaxValue - MinValue;
			double a = (double)num18 / (double)StepSize;
			double num19 = Math.Ceiling(a);
			num16 = num19;
		}
		List<StepperStepConsoleUGUI> steps2 = _steps;
		if (_steps != null)
		{
			object obj2 = 0;
			GameObject gameObject = default(GameObject);
			GameObject gameObject2 = default(GameObject);
			while ((nint)obj2 < steps2._size)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180E443F0");
				double num20 = (double)obj2 - num16;
				object obj3 = obj2 ^ num16;
				object obj4 = obj2 ^ num20;
				object obj5 = obj3 & obj4;
				bool flag3 = (nint)obj5 < 0;
				bool flag4 = num20 < 0.0;
				bool active = flag4 != flag3;
				gameObject.SetActive(active);
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180E443F0");
				double num21 = (double)obj2 - num16;
				object obj6 = obj2 ^ num16;
				object obj7 = obj2 ^ num21;
				object obj8 = obj6 & obj7;
				bool flag5 = (nint)obj8 < 0;
				bool flag6 = num21 < 0.0;
				bool active2 = flag6 == flag5;
				gameObject2.SetActive(active2);
				obj2++;
			}
		}
	}

	public void Refresh()
	{
		if (WholeNumbers)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1800033E0");
		}
		updateValue(_value);
	}

	protected bool hasValidSteps()
	{
		//IL_0072: Invalid comparison between F8 and I4
		if (_steps != null)
		{
			List<StepperStepConsoleUGUI> steps = _steps;
			float num = MaxValue - MinValue;
			float num2 = num - 0.001f;
			float num3 = num2 / StepSize;
			double num4 = Math.Ceiling(num3);
			double num5 = (double)steps._size - num4;
			return num5 == 0.0;
		}
		return false;
	}

	protected void updateText(string text)
	{
		//IL_0017: Expected I, but got O
		//IL_0027: Expected O, but got I
		//IL_0037: Expected O, but got I
		TextMeshProUGUI textTf = TextTf;
		nint num = (nint)textTf;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v12 @ r8_v1 (Il2CppClass<TMPro.TextMeshProUGUI>)+558]");
		object obj = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v12 @ r8_v1 (Il2CppClass<TMPro.TextMeshProUGUI>)+560]");
		object obj2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v13 @ rax_v2 (should have been resolved before IL gen)");
		/*Error: End of method reached without returning.*/;
	}

	public void OnEnable()
	{
		//IL_0093: Expected I, but got O
		AutoNavigationOverrides autoNavigationOverrides = AutoNavigationOverrides;
		if (autoNavigationOverrides != null)
		{
			AutoNavigationOverrides autoNavigationOverrides2 = AutoNavigationOverrides;
			autoNavigationOverrides2.BlockLeft = _enableButtonControls;
			AutoNavigationOverrides autoNavigationOverrides3 = AutoNavigationOverrides;
			autoNavigationOverrides3.BlockRight = _enableButtonControls;
		}
		string text = TextTf.text;
		TextMeshProUGUI textTf = TextTf;
		nint num = (nint)textTf;
		textTf.text = text;
		if (WholeNumbers)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1800033E0");
		}
		updateValue(_value);
		updateButtons();
	}

	public virtual void Update()
	{
		//IL_0104: Expected I4, but got I8
		if (!_enableButtonControls)
		{
			return;
		}
		EventSystem current = EventSystem.current;
		if (!(current != null))
		{
			return;
		}
		EventSystem current2 = EventSystem.current;
		Selectable selectable = Selectable;
		GameObject gameObject = selectable.gameObject;
		if (!(current2.m_CurrentSelected == gameObject))
		{
			return;
		}
		if (!InputUtils.LeftPressed())
		{
			if (InputUtils.RightPressed())
			{
				Step(1);
			}
		}
		else
		{
			Step(-1);
		}
	}

	public float ConvertToStepValue(float value)
	{
		//IL_0059: Invalid comparison between F8 and I4
		//IL_01a9: Invalid comparison between F8 and I4
		//IL_01c8: Expected I, but got O
		//IL_0095: Expected O, but got F8
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Expected I, but got Unknown
		//IL_00b1: Expected O, but got I
		//IL_01f1: Expected O, but got I4
		//IL_01ff: Expected I, but got O
		//IL_03d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d6: Expected F4, but got Unknown
		//IL_0290: Unknown result type (might be due to invalid IL or missing references)
		//IL_0295: Expected F4, but got Unknown
		//IL_0429: Unknown result type (might be due to invalid IL or missing references)
		//IL_042e: Expected F4, but got Unknown
		//IL_0483: Unknown result type (might be due to invalid IL or missing references)
		//IL_0488: Expected F4, but got Unknown
		//IL_02e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e7: Expected F4, but got Unknown
		//IL_04d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_04da: Expected F4, but got Unknown
		//IL_0334: Unknown result type (might be due to invalid IL or missing references)
		//IL_0339: Expected F4, but got Unknown
		//IL_0527: Unknown result type (might be due to invalid IL or missing references)
		//IL_052c: Expected F4, but got Unknown
		//IL_0386: Unknown result type (might be due to invalid IL or missing references)
		//IL_038b: Expected F4, but got Unknown
		//IL_017f: Invalid comparison between O and F8
		float num = MinValue;
		float num2 = MaxValue - MinValue;
		float num3 = num2 / StepSize;
		double num4 = Math.Ceiling(num3);
		double num5 = num4 + 1.0;
		object obj2;
		float num9;
		float num34;
		float result;
		float stepSize;
		if (!(num5 < 8.0))
		{
			stepSize = StepSize;
			double num6 = num5 - 8.0;
			object obj = num6 >> 3;
			nint num7 = (nint)(obj + 1);
			obj2 = num7 * 8;
			nint num8 = num7;
			num9 = value;
			float num10 = 3.4028235E+38f;
			bool flag2;
			do
			{
				float num11 = value - num;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
				float num12 = num11 & 0;
				if (num10 > num12)
				{
					num9 = num;
					num10 = num12;
				}
				float num13 = num + stepSize;
				float num14 = value - num13;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
				float num15 = num14 & 0;
				if (num10 > num15)
				{
					num9 = num13;
					num10 = num15;
				}
				float num16 = num13 + stepSize;
				float num17 = value - num16;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
				float num18 = num17 & 0;
				if (num10 > num18)
				{
					num9 = num16;
					num10 = num18;
				}
				float num19 = num16 + stepSize;
				float num20 = value - num19;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
				float num21 = num20 & 0;
				if (num10 > num21)
				{
					num9 = num19;
					num10 = num21;
				}
				float num22 = num19 + stepSize;
				float num23 = value - num22;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
				float num24 = num23 & 0;
				if (num10 > num24)
				{
					num9 = num22;
					num10 = num24;
				}
				float num25 = num22 + stepSize;
				float num26 = value - num25;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
				float num27 = num26 & 0;
				if (num10 > num27)
				{
					num9 = num25;
					num10 = num27;
				}
				float num28 = num25 + stepSize;
				float num29 = value - num28;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
				float num30 = num29 & 0;
				if (num10 > num30)
				{
					num9 = num28;
					num10 = num30;
				}
				float num31 = num28 + stepSize;
				float num32 = value - num31;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
				float num33 = num32 & 0;
				bool flag = !(num10 > num33);
				num34 = num10;
				if (!flag)
				{
					num9 = num31;
					num34 = num33;
				}
				num = num31 + stepSize;
				num8--;
				flag2 = num10 != num33;
				num10 = num34;
			}
			while (flag2);
			if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj2) >= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)num5))
			{
				result = num9;
				goto IL_022a;
			}
		}
		else
		{
			bool flag3 = !(num5 > 0.0);
			stepSize = value;
			nint num8 = (nint)typeof(Math);
			result = value;
			if (flag3)
			{
				goto IL_022a;
			}
			stepSize = StepSize;
			obj2 = 0;
			num8 = (nint)typeof(Math);
			num9 = value;
			num34 = 3.4028235E+38f;
		}
		num5 -= (double)obj2;
		bool flag5;
		do
		{
			float num35 = value - num;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
			float num36 = num35 & 0;
			bool flag4 = !(num34 > num36);
			float num37 = num34;
			if (!flag4)
			{
				num9 = num;
				num37 = num36;
			}
			num += stepSize;
			flag5 = num34 != num36;
			result = num9;
			num34 = num37;
		}
		while (flag5);
		goto IL_022a;
		IL_022a:
		if (WholeNumbers)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1800033E0");
		}
		return result;
	}

	public int GetStepToDisplay(float value)
	{
		//IL_00cc: Expected I4, but got F8
		float num = StepSize * 0.5f;
		float num2 = MaxValue - num;
		if (!(value > num2))
		{
			float num3 = StepSize * 0.499f;
			float num4 = value - MinValue;
			float num5 = num3 + num4;
			float num6 = num5 / StepSize;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803B9370");
			int result = default(int);
			return result;
		}
		float num7 = MaxValue - MinValue;
		float num8 = num7 / StepSize;
		double num9 = Math.Ceiling(num8);
		return (int)num9;
	}

	public void Increase()
	{
		Step(1);
	}

	public void IncreaseLooped()
	{
		//IL_008c: Expected I4, but got F8
		float num = StepSize * 0.1f;
		float num2 = MaxValue - num;
		if (!(_value > num2))
		{
			Step(1);
			return;
		}
		float num3 = MaxValue - MinValue;
		float num4 = num3 / StepSize;
		double num5 = Math.Ceiling(num4);
		int steps = (int)(0.0 - num5);
		Step(steps);
	}

	public void Decrease()
	{
		//IL_000f: Expected I4, but got I8
		Step(-1);
	}

	public unsafe void Step(int steps)
	{
		//IL_017f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0184: Expected O, but got Unknown
		//IL_01c7: Expected I, but got O
		//IL_01f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fd: Expected O, but got Unknown
		//IL_0206: Invalid comparison between F4 and O
		//IL_021f: Expected O, but got I
		//IL_0096: Expected F4, but got I4
		//IL_00ea: Expected F4, but got Ref
		//IL_00f2: Expected F4, but got Ref
		float num = MinValue;
		object obj = steps * StepSize;
		float num2 = (float)obj + _value;
		if (!(MinValue > num2))
		{
			num = MaxValue;
			if (!(num2 > MaxValue))
			{
				goto IL_01b9;
			}
		}
		num2 = num;
		goto IL_01b9;
		IL_01b9:
		nint num3 = (nint)typeof(Mathf);
		float num4 = _value - num2;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v106 @ rax_v3 (Il2CppClass<UnityEngine.Mathf>)+B8]");
		nint num5 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
		object obj2 = num4 & 0;
		bool flag = System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)Mathf.Epsilon) >= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj2);
		int num6 = steps;
		StepperUGUI stepperUGUI = (StepperUGUI)num5;
		if (!flag)
		{
			updateValue(num2);
			updateButtons();
			num6 = 0;
			stepperUGUI = this;
		}
		if (steps == 0)
		{
			return;
		}
		bool flag2 = OnValueChangedEvent == null;
		float num7 = num6;
		UnityEvent<float> unityEvent = (UnityEvent<float>)(object)stepperUGUI;
		if (!flag2)
		{
			if (WholeNumbers)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1800033E0");
			}
			object obj3 = default(object);
			OnValueChangedEvent.Invoke((nint)(&obj3));
			num7 = (nint)(&obj3);
			unityEvent = OnValueChangedEvent;
		}
		OnValueChangedDelegate onValueChanged = OnValueChanged;
		if (OnValueChanged != null)
		{
			if (WholeNumbers)
			{
				unityEvent.Invoke(num2);
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: v174.invoke_impl (System.IntPtr) (should have been resolved before IL gen)");
		}
	}

	protected void updateButtons()
	{
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Expected O, but got Unknown
		//IL_0036: Invalid comparison between O and F4
		//IL_0055: Invalid comparison between F4 and I4
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Expected O, but got Unknown
		//IL_00f1: Invalid comparison between O and F4
		//IL_0110: Invalid comparison between F4 and I4
		if (DisableButtons)
		{
			float num = _value - MaxValue;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
			object obj = num & 0;
			bool flag = System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj) < System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)1E-45f);
			float num2 = (float)obj - 1E-45f;
			bool flag2 = num2 == 0f;
			bool flag3 = !flag;
			bool flag4 = !flag2;
			bool interactable = flag4 & flag3;
			if (IncreaseButton != null)
			{
				IncreaseButton.interactable = interactable;
			}
			float num3 = _value - MinValue;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
			object obj2 = num3 & 0;
			bool flag5 = System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj2) < System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)1E-45f);
			float num4 = (float)obj2 - 1E-45f;
			bool flag6 = num4 == 0f;
			bool flag7 = !flag5;
			bool flag8 = !flag6;
			bool interactable2 = flag8 & flag7;
			if (DecreaseButton != null)
			{
				DecreaseButton.interactable = interactable2;
			}
		}
		else
		{
			if (DecreaseButton != null)
			{
				DecreaseButton.enabled = true;
			}
			if (IncreaseButton != null)
			{
				IncreaseButton.enabled = true;
			}
		}
	}

	public void SetSelected()
	{
		Selectable selectable = Selectable;
		if (selectable != null)
		{
			EventSystem current = EventSystem.current;
			if (current != null)
			{
				EventSystem current2 = EventSystem.current;
				Selectable selectable2 = Selectable;
				GameObject selectedGameObject = selectable2.gameObject;
				current2.SetSelectedGameObject(selectedGameObject);
			}
		}
	}

	public StepperUGUI()
	{
		List<StepperStepConsoleUGUI> steps = new List<StepperStepConsoleUGUI>();
		_steps = steps;
		ValueFormat = "{0:N0} %";
		DisableButtons = true;
		base._002Ector();
	}
}
