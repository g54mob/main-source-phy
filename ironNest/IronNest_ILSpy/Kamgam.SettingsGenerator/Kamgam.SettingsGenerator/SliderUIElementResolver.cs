using System;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.UIElements;

namespace Kamgam.SettingsGenerator;

public class SliderUIElementResolver : SettingResolverForVisualElement, ISettingResolver
{
	public float StepSize = 10f;

	public bool WholeNumbers;

	[NonSerialized]
	protected float _value;

	public string ValueFormat = "{0:N0} %";

	public bool UseMoveCommandToChangeValue = true;

	protected Slider _slider;

	protected TextField _valueTf;

	protected SettingData.DataType[] supportedDataTypes;

	protected bool stopPropagation;

	public float Value
	{
		get
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Invalid instruction: 12 Invalid \"Jump target not found in method: 0x1800033E0\"");
			return _value;
		}
		set
		{
			//IL_0013: Expected I, but got O
			//IL_0044: Unknown result type (might be due to invalid IL or missing references)
			//IL_0049: Expected O, but got Unknown
			//IL_0052: Invalid comparison between F4 and O
			//IL_026e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0273: Expected O, but got Unknown
			//IL_027c: Invalid comparison between F4 and O
			//IL_0165: Unknown result type (might be due to invalid IL or missing references)
			//IL_016a: Expected O, but got Unknown
			//IL_0173: Invalid comparison between O and F4
			//IL_0133: Expected F4, but got I4
			Slider slider = Slider;
			float num8;
			if (slider != null)
			{
				nint num = (nint)typeof(Mathf);
				float num2 = _value - value;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v48 @ rax_v6 (Il2CppClass<UnityEngine.Mathf>)+B8]");
				nint num3 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
				object obj = num2 & 0;
				if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)Mathf.Epsilon) >= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj))
				{
					return;
				}
				bool flag = !WholeNumbers;
				float value2 = value;
				if (!flag)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1800033E0");
					value2 = value;
				}
				float num4 = ConvertToStepValue(value2);
				Slider slider2 = Slider;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1807B7AC0");
				Slider slider3 = Slider;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1807B7980");
				float num5 = default(float);
				bool flag2 = num5 > num4;
				float num6 = num5;
				if (!flag2)
				{
					float num7 = default(float);
					bool flag3 = !(num4 > num7);
					num6 = num7;
					num8 = num4;
					if (flag3)
					{
						goto IL_025e;
					}
				}
				num8 = num6;
				goto IL_025e;
			}
			float num9 = value / StepSize;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1800033E0");
			bool flag4 = !WholeNumbers;
			float value3 = (_value = num9 * StepSize);
			if (!flag4)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1800033E0");
				_value = value3;
			}
			return;
			IL_025e:
			float num10 = num8;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
			object obj2 = num10 & 0;
			if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)0.0001f) > System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj2))
			{
				num8 = 0f;
			}
			Slider slider4 = Slider;
			float value4 = slider4.value;
			float num11 = 0.0001f - num8;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
			object obj3 = num11 & 0;
			if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj3) > System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)1E-45f))
			{
				Slider slider5 = Slider;
				slider5.value = num8;
			}
			_value = num8;
		}
	}

	public Slider Slider
	{
		get
		{
			//IL_0095: Expected I, but got O
			//IL_00a3: Expected I, but got O
			//IL_00b3: Expected O, but got I
			//IL_00ef: Expected O, but got I
			//IL_0114: Expected O, but got I4
			//IL_02a5: Expected I, but got O
			//IL_02ad: Expected I, but got O
			//IL_02bd: Expected O, but got I
			//IL_0149: Expected O, but got I
			//IL_016e: Expected O, but got I4
			if (_slider == null)
			{
				VisualElement visualElement = base.VisualElement;
				if (visualElement != null)
				{
					goto IL_0057;
				}
			}
			VisualElement visualElement2 = base.VisualElement;
			if (_slider != visualElement2)
			{
				goto IL_0057;
			}
			goto IL_01fd;
			IL_0234:
			if (_slider != null)
			{
				EventCallback<ChangeEvent<float>> callback = onValueChanged;
				bool flag = INotifyValueChangedExtensions.RegisterValueChangedCallback(_slider, callback);
				EventCallback<NavigationMoveEvent> callback2 = onMove;
				if (_slider == null)
				{
					return (Slider)(object)new NullReferenceException();
				}
				_slider.RegisterCallback(callback2);
			}
			goto IL_01fd;
			IL_01fd:
			return _slider;
			IL_0057:
			VisualElement visualElement3 = base.VisualElement;
			if (visualElement3 == null)
			{
				_slider = null;
				goto IL_0234;
			}
			nint num = (nint)visualElement3;
			nint num2 = (nint)typeof(Slider);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v215 @ r10_v3 (Il2CppClass<UnityEngine.UIElements.Slider>)+130]");
			object obj = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v213 @ r11_v3 (Il2CppClass<UnityEngine.UIElements.VisualElement>)+130]");
			nint num3 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v215 @ r10_v3 (Il2CppClass<UnityEngine.UIElements.Slider>)+130]");
			Slider slider;
			if (num3 >= 0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v213 @ r11_v3 (Il2CppClass<UnityEngine.UIElements.VisualElement>)+C8]");
				object obj2 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v259 @ rax_v25+FFFFFFF8+v216 @ rax_v15*8]");
				bool flag2 = 0 == (nint)typeof(Slider);
				slider = (Slider)1;
				if (flag2)
				{
					goto IL_0253;
				}
			}
			slider = null;
			goto IL_0253;
			IL_0253:
			bool flag3 = slider == null;
			VisualElement slider2 = null;
			if (!flag3)
			{
				slider2 = visualElement3;
			}
			Slider slider3;
			do
			{
				_slider = (Slider)slider2;
				nint num4 = (nint)typeof(Slider);
				nint num5 = (nint)visualElement3;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v229 @ r10_v4 (Il2CppClass<UnityEngine.UIElements.Slider>)+130]");
				object obj3 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v231 @ r11_v4 (Il2CppClass<UnityEngine.UIElements.VisualElement>)+130]");
				nint num6 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v229 @ r10_v4 (Il2CppClass<UnityEngine.UIElements.Slider>)+130]");
				if (num6 >= 0)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v231 @ r11_v4 (Il2CppClass<UnityEngine.UIElements.VisualElement>)+C8]");
					object obj4 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v330 @ rax_v22+FFFFFFF8+v312 @ rax_v19*8]");
					bool flag4 = 0 == (nint)typeof(Slider);
					slider3 = (Slider)1;
					if (flag4)
					{
						continue;
					}
				}
				slider3 = null;
			}
			while (slider3 != null);
			goto IL_0234;
		}
	}

	public TextField ValueTf
	{
		get
		{
			if (_valueTf == null)
			{
				VisualElement visualElement = base.VisualElement;
				TextField valueTf = UQueryExtensions.Q<TextField>(visualElement);
				_valueTf = valueTf;
			}
			return _valueTf;
		}
	}

	public override SettingData.DataType[] GetSupportedDataTypes()
	{
		return supportedDataTypes;
	}

	public override void OnEnable()
	{
		//IL_0059: Expected I, but got O
		base.OnEnable();
		SettingData.DataType[] allowedTypes = GetSupportedDataTypes();
		if (HasValidSettingForID(ID, allowedTypes))
		{
			SettingsProvider settingsProvider = base.SettingsProvider;
			Settings settings = settingsProvider.Settings;
			ISetting setting = settings.GetSetting(ID);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v169 @ r8_v5 (Il2CppClass<Kamgam.SettingsGenerator.SliderUIElementResolver>)+240]");
			Action action = new Action(this, (IntPtr)0);
			nint num = (nint)this;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180003ED0");
			Refresh();
		}
	}

	public override void OnDisable()
	{
		_slider = null;
		base.resetUIElements();
		StopAllCoroutines();
		((SettingResolver)this).OnDisable();
	}

	public override void OnDestroy()
	{
		base.resetUIElements();
		BindingClass = null;
		StopAllCoroutines();
		((SettingResolver)this).OnDestroy();
		Slider slider = Slider;
		if (slider != null)
		{
			Slider slider2 = Slider;
			EventCallback<ChangeEvent<float>> callback = onValueChanged;
			bool flag = INotifyValueChangedExtensions.UnregisterValueChangedCallback(slider2, callback);
		}
	}

	protected void onValueChanged(ChangeEvent<float> evt)
	{
		//IL_01a8: Expected I, but got O
		float value = default(float);
		int value2 = default(int);
		while (!stopPropagation)
		{
			SettingData.DataType[] allowedTypes = GetSupportedDataTypes();
			if (!HasValidSettingForID(ID, allowedTypes) || !HasActiveSettingForID(ID))
			{
				break;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18077C4C0");
			Value = value;
			SettingsProvider settingsProvider = base.SettingsProvider;
			Settings settings = settingsProvider.Settings;
			SettingFloat settingFloat = settings.GetFloat(ID);
			if (settingFloat == null)
			{
				SettingsProvider settingsProvider2 = base.SettingsProvider;
				Settings settings2 = settingsProvider2.Settings;
				SettingInt settingInt = settings2.GetInt(ID);
				if (settingInt != null)
				{
					if (WholeNumbers)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1800033E0");
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803B9370");
					settingInt.SetValue(value2);
				}
				break;
			}
			if (WholeNumbers)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1800033E0");
			}
			nint num = (nint)settingFloat;
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: [v309 @ r9_v4 (Il2CppClass<Kamgam.SettingsGenerator.SettingFloat>)+4E8] (should have been resolved before IL gen)");
		}
	}

	public void onMove(NavigationMoveEvent evt)
	{
		//IL_0029: Expected I, but got O
		//IL_0061: Expected O, but got I
		//IL_006a: Expected O, but got I4
		//IL_01ab: Expected O, but got I
		//IL_01c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c7: Expected O, but got Unknown
		//IL_01cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d4: Expected O, but got Unknown
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Expected O, but got Unknown
		Slider slider = Slider;
		IPanel panel = slider.panel;
		nint num = (nint)panel;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v55 @ r10_v2 (Il2CppClass<UnityEngine.UIElements.IPanel>)+12E]");
		if ((nint)0 >= (nint)0)
		{
			goto IL_00a1;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v55 @ r10_v2 (Il2CppClass<UnityEngine.UIElements.IPanel>)+B0]");
		object obj = 0;
		object obj2 = 0;
		while (true)
		{
			object obj3 = obj2 + obj2;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v168 @ r8_v8+v195 @ rax_v22*8]");
			if (0 == (nint)typeof(IPanel))
			{
				break;
			}
			obj2++;
			object obj4 = obj2;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v55 @ r10_v2 (Il2CppClass<UnityEngine.UIElements.IPanel>)+12E]");
			if ((nint)obj4 < 0)
			{
				continue;
			}
			goto IL_00a1;
		}
		object obj5 = obj2 + obj2;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v168 @ r8_v8+8+v226 @ rcx_v15*8]");
		object obj6 = (nint)0 + (nint)3;
		object obj7 = obj6 << 4;
		object obj8 = obj7 + 312;
		object obj9 = obj8 + num;
		goto IL_00b0;
		IL_00a1:
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
		goto IL_00b0;
		IL_00b0:
		FocusController focusController = panel.focusController;
		Focusable focusedElement = focusController.focusedElement;
		if (slider != focusedElement)
		{
			return;
		}
		float value;
		if (evt._003Cdirection_003Ek__BackingField == NavigationMoveEvent.Direction.Left)
		{
			if (!UseMoveCommandToChangeValue)
			{
				return;
			}
			if (WholeNumbers)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1800033E0");
			}
			float num2 = StepSize * -1f;
			value = num2 + _value;
		}
		else
		{
			if (evt._003Cdirection_003Ek__BackingField != NavigationMoveEvent.Direction.Right || !UseMoveCommandToChangeValue)
			{
				return;
			}
			if (WholeNumbers)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1800033E0");
			}
			float num3 = _value + StepSize;
			value = num3;
		}
		Value = value;
		evt.StopPropagation();
	}

	protected bool isFocused(VisualElement ele)
	{
		//IL_009a: Expected I4, but got O
		if (ele != null)
		{
			IPanel panel = ele.panel;
			if (panel != null)
			{
				FocusController focusController = panel.focusController;
				if (focusController != null)
				{
					Focusable focusedElement = focusController.focusedElement;
					object obj = (object)ele - (object)focusedElement;
					return obj == null;
				}
			}
		}
		NullReferenceException ex = new NullReferenceException();
		return (byte)(int)ex != 0;
	}

	public float ConvertToStepValue(float value)
	{
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Expected O, but got Unknown
		//IL_008a: Invalid comparison between F8 and I4
		//IL_01e2: Invalid comparison between F8 and I4
		//IL_0209: Expected I, but got O
		//IL_00c6: Expected O, but got F8
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Expected I, but got Unknown
		//IL_00e2: Expected O, but got I
		//IL_0243: Expected O, but got I4
		//IL_0251: Expected I, but got O
		//IL_041c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0421: Expected F4, but got Unknown
		//IL_02db: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e0: Expected F4, but got Unknown
		//IL_0474: Unknown result type (might be due to invalid IL or missing references)
		//IL_0479: Expected F4, but got Unknown
		//IL_04d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d8: Expected F4, but got Unknown
		//IL_032d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0332: Expected F4, but got Unknown
		//IL_0525: Unknown result type (might be due to invalid IL or missing references)
		//IL_052a: Expected F4, but got Unknown
		//IL_037f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0384: Expected F4, but got Unknown
		//IL_0577: Unknown result type (might be due to invalid IL or missing references)
		//IL_057c: Expected F4, but got Unknown
		//IL_03d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d6: Expected F4, but got Unknown
		//IL_01b8: Invalid comparison between O and F8
		Slider slider = Slider;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1807B7AC0");
		Slider slider2 = Slider;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1807B7980");
		Slider slider3 = Slider;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1807B7AC0");
		float num = default(float);
		object obj2 = default(object);
		object obj = num - obj2;
		double a = (double)obj / (double)StepSize;
		double num2 = Math.Ceiling(a);
		double num3 = num2 + 1.0;
		object obj4;
		float num6;
		float num8;
		float num33;
		float result;
		float stepSize;
		if (!(num3 < 8.0))
		{
			stepSize = StepSize;
			double num4 = num3 - 8.0;
			object obj3 = num4 >> 3;
			nint num5 = (nint)(obj3 + 1);
			obj4 = num5 * 8;
			num6 = value;
			float num7 = 3.4028235E+38f;
			num8 = num;
			nint num9 = num5;
			bool flag2;
			do
			{
				float num10 = value - num8;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
				float num11 = num10 & 0;
				if (num7 > num11)
				{
					num6 = num8;
					num7 = num11;
				}
				float num12 = num8 + stepSize;
				float num13 = value - num12;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
				float num14 = num13 & 0;
				if (num7 > num14)
				{
					num6 = num12;
					num7 = num14;
				}
				float num15 = num12 + stepSize;
				float num16 = value - num15;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
				float num17 = num16 & 0;
				if (num7 > num17)
				{
					num6 = num15;
					num7 = num17;
				}
				float num18 = num15 + stepSize;
				float num19 = value - num18;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
				float num20 = num19 & 0;
				if (num7 > num20)
				{
					num6 = num18;
					num7 = num20;
				}
				float num21 = num18 + stepSize;
				float num22 = value - num21;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
				float num23 = num22 & 0;
				if (num7 > num23)
				{
					num6 = num21;
					num7 = num23;
				}
				float num24 = num21 + stepSize;
				float num25 = value - num24;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
				float num26 = num25 & 0;
				if (num7 > num26)
				{
					num6 = num24;
					num7 = num26;
				}
				float num27 = num24 + stepSize;
				float num28 = value - num27;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
				float num29 = num28 & 0;
				if (num7 > num29)
				{
					num6 = num27;
					num7 = num29;
				}
				float num30 = num27 + stepSize;
				float num31 = value - num30;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
				float num32 = num31 & 0;
				bool flag = !(num7 > num32);
				num33 = num7;
				if (!flag)
				{
					num6 = num30;
					num33 = num32;
				}
				num8 = num30 + stepSize;
				num9--;
				flag2 = num7 != num32;
				num7 = num33;
			}
			while (flag2);
			if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj4) >= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)num3))
			{
				result = num6;
				goto IL_026b;
			}
		}
		else
		{
			bool flag3 = !(num3 > 0.0);
			result = value;
			stepSize = value;
			nint num9 = (nint)typeof(Math);
			if (flag3)
			{
				goto IL_026b;
			}
			stepSize = StepSize;
			num6 = value;
			num33 = 3.4028235E+38f;
			num8 = num;
			obj4 = 0;
			num9 = (nint)typeof(Math);
		}
		num3 -= (double)obj4;
		bool flag5;
		do
		{
			float num34 = value - num8;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
			float num35 = num34 & 0;
			bool flag4 = !(num33 > num35);
			float num36 = num33;
			if (!flag4)
			{
				num6 = num8;
				num36 = num35;
			}
			num8 += stepSize;
			flag5 = num33 != num35;
			result = num6;
			num33 = num36;
		}
		while (flag5);
		goto IL_026b;
		IL_026b:
		if (WholeNumbers)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1800033E0");
		}
		return result;
	}

	public void Increase()
	{
		if (WholeNumbers)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1800033E0");
		}
		float value = _value + StepSize;
		Value = value;
	}

	public void Decrease()
	{
		if (WholeNumbers)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1800033E0");
		}
		float num = StepSize * -1f;
		float value = num + _value;
		Value = value;
	}

	public void Step(int steps)
	{
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Expected O, but got Unknown
		if (WholeNumbers)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1800033E0");
		}
		object obj = steps * StepSize;
		float value = (float)obj + _value;
		Value = value;
	}

	public override void Refresh()
	{
		//IL_0179: Expected F4, but got I4
		SettingData.DataType[] allowedTypes = GetSupportedDataTypes();
		if (!HasValidSettingForID(ID, allowedTypes) || !HasActiveSettingForID(ID))
		{
			return;
		}
		_ = 1;
		SettingResolver settingResolver = default(SettingResolver);
		SettingsProvider settingsProvider = settingResolver.SettingsProvider;
		if ((object)settingsProvider != null)
		{
			Settings settings = settingsProvider.Settings;
			if ((object)settings != null)
			{
				SettingFloat settingFloat = settings.GetFloat(settingResolver.ID);
				if (settingFloat == null)
				{
					SettingsProvider settingsProvider2 = settingResolver.SettingsProvider;
					if ((object)settingsProvider2 == null)
					{
						throw new NullReferenceException();
					}
					Settings settings2 = settingsProvider2.Settings;
					SettingInt settingInt = settings2.GetInt(settingResolver.ID);
					if (settingInt != null)
					{
						int value = settingInt.GetValue();
						((SliderUIElementResolver)settingResolver).Value = value;
					}
					_ = 0;
				}
				else
				{
					float value2 = settingFloat.GetValue();
					float value3 = default(float);
					((SliderUIElementResolver)settingResolver).Value = value3;
					_ = 0;
				}
				return;
			}
			throw new NullReferenceException();
		}
		throw new NullReferenceException();
	}

	public SliderUIElementResolver()
	{
		SettingData.DataType[] array = new SettingData.DataType[2];
		_ = 1;
		_ = 2;
		supportedDataTypes = array;
		((SettingResolver)this)._002Ector();
	}
}
