using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using UnityEngine;

public class DialGaugeDisplay : MonoBehaviour
{
	public enum GaugeMovementMode
	{
		DirectLerp,
		ValueRateLimited,
		AngleRateLimited
	}

	private class ReflectionFloatValueProvider : IFloatValueProvider
	{
		private readonly object target;

		private readonly PropertyInfo prop;

		public ReflectionFloatValueProvider(object target, PropertyInfo prop)
		{
			this.target = target;
			this.prop = prop;
		}

		public float GetFloatValue()
		{
			//IL_0044: Expected O, but got I
			//IL_0073: Expected I, but got O
			//IL_00b9: Expected F4, but got O
			if ((object)prop != null)
			{
				object value = prop.GetValue(target);
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B502B8]");
				object obj = 0;
				bool flag = value == null;
				ReflectionFloatValueProvider reflectionFloatValueProvider = (ReflectionFloatValueProvider)(object)prop;
				if (!flag)
				{
					nint num = (nint)value;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v50 @ rdx_v5 (Il2CppClass<System.Object>)+40]");
					nint num2 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v66 @ r8_v1+40]");
					bool flag2 = num2 != 0;
					reflectionFloatValueProvider = (ReflectionFloatValueProvider)value;
					if (!flag2)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_object_unbox\"");
						object obj2 = default(object);
						return (float)obj2;
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
					float result = default(float);
					return result;
				}
			}
			throw new NullReferenceException();
		}
	}

	public Transform needleTransform;

	public float targetNumber;

	public MonoBehaviour floatValueProvider;

	public string providerPropertyName;

	public bool useAbsoluteValue;

	public bool clampToRange;

	public bool enableRounding;

	public int decimalDigits;

	public bool enableSmoothing;

	public float smoothingTimeConstant;

	public float maxPerFrameInputDelta;

	public float minValue;

	public float maxValue;

	public float minAngle;

	public float maxAngle;

	public bool invertRotation;

	public Vector3 rotationAxis;

	public AnimationCurve valueToNormalized;

	public GaugeMovementMode movementMode;

	public float rotationSpeed;

	public float valueChaseSpeed;

	public float needleMaxDegreesPerSecond;

	public bool snapWhenLargeJump;

	public float snapThresholdPercentOfRange;

	public bool logEverySecond;

	public bool drawGizmos;

	public int gizmoDivisions;

	private float rawValue;

	private float processedValue;

	private float displayTargetValue;

	private float currentValue;

	private float currentAngle;

	private bool smoothingInitialized;

	private float smoothedValue;

	private int lastLogFrame;

	private IFloatValueProvider provider;

	private float previousForClamp;

	private float RangeSpan
	{
		get
		{
			float num = maxValue - minValue;
			bool flag = !(1E-05f < num);
			float result = 1E-05f;
			if (!flag)
			{
				result = num;
			}
			return result;
		}
	}

	public float RawValue => rawValue;

	public float ProcessedValue => processedValue;

	public float DisplayTargetValue => displayTargetValue;

	public float CurrentValue => currentValue;

	public float CurrentAngle => currentAngle;

	private void Awake()
	{
		if (needleTransform == null)
		{
			Transform transform = base.transform;
			needleTransform = transform;
		}
		InitializeProvider();
		float num = minValue;
		float value = targetNumber;
		smoothingInitialized = false;
		smoothedValue = targetNumber;
		previousForClamp = targetNumber;
		rawValue = targetNumber;
		processedValue = targetNumber;
		displayTargetValue = targetNumber;
		if (!(minValue > targetNumber))
		{
			num = maxValue;
			if (!(targetNumber > maxValue))
			{
				goto IL_0107;
			}
		}
		value = num;
		goto IL_0107;
		IL_0107:
		currentValue = value;
		ApplyNeedleRotation(currentAngle = ComputeTargetAngle(value));
	}

	private void OnValidate()
	{
		//IL_0173: Invalid comparison between I4 and F4
		if (!(minValue < maxValue))
		{
			float num = minValue + 1f;
			maxValue = num;
		}
		if (valueToNormalized != null)
		{
			Keyframe[] keys = valueToNormalized.keys;
			if (keys.Length != 0)
			{
				goto IL_00a3;
			}
		}
		AnimationCurve animationCurve = AnimationCurve.Linear(0f, 0f, 1f, 1f);
		valueToNormalized = animationCurve;
		goto IL_00a3;
		IL_00a3:
		if (decimalDigits < 0)
		{
			decimalDigits = 0;
		}
		if (decimalDigits > 6)
		{
			decimalDigits = 6;
		}
		if (0f > snapThresholdPercentOfRange)
		{
			snapThresholdPercentOfRange = 0f;
		}
		if (snapThresholdPercentOfRange > 1f)
		{
			snapThresholdPercentOfRange = 1f;
		}
	}

	private void Update()
	{
		//IL_0f03: Expected O, but got I
		//IL_0017: Expected I, but got O
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Expected F4, but got Unknown
		//IL_0f28: Invalid comparison between F4 and I4
		//IL_004f: Expected O, but got I
		//IL_0058: Expected O, but got I4
		//IL_0237: Expected O, but got I4
		//IL_0157: Expected O, but got I
		//IL_0160: Unknown result type (might be due to invalid IL or missing references)
		//IL_0165: Expected O, but got Unknown
		//IL_016d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0172: Expected O, but got Unknown
		//IL_02a1: Invalid comparison between F4 and I4
		//IL_0baf: Expected F4, but got I
		//IL_0bce: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bd3: Expected F4, but got Unknown
		//IL_020d: Expected O, but got I
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Expected O, but got Unknown
		//IL_024e: Expected O, but got I4
		//IL_06c6: Invalid comparison between I4 and F4
		//IL_04b7: Expected O, but got I4
		//IL_040a: Unknown result type (might be due to invalid IL or missing references)
		//IL_040f: Expected F4, but got Unknown
		//IL_025c: Invalid comparison between F4 and I4
		//IL_0711: Expected F4, but got I4
		//IL_0613: Unknown result type (might be due to invalid IL or missing references)
		//IL_0618: Expected O, but got Unknown
		//IL_0620: Invalid comparison between F4 and O
		//IL_03d1: Invalid comparison between F4 and I4
		//IL_0651: Invalid comparison between F4 and I4
		//IL_0c52: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c57: Expected O, but got Unknown
		//IL_0c89: Invalid comparison between I4 and F4
		//IL_038b: Expected O, but got I
		//IL_0358: Expected F4, but got I4
		//IL_072e: Expected O, but got I4
		//IL_053f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0544: Expected O, but got Unknown
		//IL_054c: Invalid comparison between F4 and O
		//IL_0ccf: Expected O, but got I
		//IL_057d: Invalid comparison between F4 and I4
		//IL_07a5: Expected I, but got O
		//IL_081c: Expected I, but got O
		//IL_082c: Expected O, but got I
		//IL_08ab: Expected I, but got O
		//IL_08bb: Expected O, but got I
		//IL_093a: Expected I, but got O
		//IL_094a: Expected O, but got I
		//IL_09c9: Expected I, but got O
		//IL_09d9: Expected O, but got I
		//IL_0a58: Expected I, but got O
		//IL_0a68: Expected O, but got I
		bool flag = provider == null;
		IFloatValueProvider floatValueProvider = (IFloatValueProvider)this;
		IFloatValueProvider floatValueProvider2;
		if (!flag)
		{
			floatValueProvider2 = provider;
			nint num = (nint)floatValueProvider2;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v54 @ r10_v2 (Il2CppClass<IFloatValueProvider>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_008f;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v54 @ r10_v2 (Il2CppClass<IFloatValueProvider>)+B0]");
			object obj = 0;
			object obj2 = 0;
			while (true)
			{
				object obj3 = obj2 + obj2;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v116 @ r8_v21+v119 @ rax_v81*8]");
				if (0 == (nint)typeof(IFloatValueProvider))
				{
					break;
				}
				obj2++;
				object obj4 = obj2;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v54 @ r10_v2 (Il2CppClass<IFloatValueProvider>)+12E]");
				if ((nint)obj4 < 0)
				{
					continue;
				}
				goto IL_008f;
			}
			object obj5 = obj2 + obj2;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v116 @ r8_v21+8+v189 @ rcx_v76*8]");
			object obj6 = (nint)0 << 4;
			object obj7 = obj6 + 312;
			object obj8 = obj7 + num;
			goto IL_009e;
		}
		goto IL_0ed8;
		IL_009e:
		float floatValue = floatValueProvider2.GetFloatValue();
		float num2 = default(float);
		targetNumber = num2;
		floatValueProvider = floatValueProvider2;
		goto IL_0ed8;
		IL_0ed8:
		bool flag2 = !useAbsoluteValue;
		float num3 = targetNumber;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
		object obj9 = 0;
		rawValue = targetNumber;
		if (!flag2)
		{
			float num4 = num3;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
			num3 = num4 & 0;
		}
		if (clampToRange)
		{
			if (!(minValue > num3))
			{
				float num5 = maxValue;
				if (num3 > maxValue)
				{
					num3 = maxValue;
				}
			}
			else
			{
				num3 = minValue;
			}
		}
		if (enableRounding)
		{
			float num7;
			if (decimalDigits > 0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033FCB0");
				float num6 = 10f * num3;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1800033E0");
				num7 = num6 / 10f;
			}
			else
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1800033E0");
				num7 = num3;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
			obj9 = 0;
			num3 = num7;
		}
		bool flag3 = !(maxPerFrameInputDelta > 0f);
		float num8 = maxPerFrameInputDelta;
		if (!flag3)
		{
			bool flag4 = smoothingInitialized;
			object obj10 = 184;
			if (!flag4)
			{
				obj10 = 200;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v334 @ rax_v69+v35 @ rcx_v1 (DialGaugeDisplay)]");
			float num9 = 0f;
			float num10 = num3;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v334 @ rax_v69+v35 @ rcx_v1 (DialGaugeDisplay)]");
			float num5 = num10 - 0f;
			num8 = num5 & obj9;
			if (num8 > maxPerFrameInputDelta)
			{
				float num11 = ((num5 < 0f) ? (-1f) : 1f);
				float num12 = num11 * maxPerFrameInputDelta;
				num3 = num12 + num9;
			}
		}
		bool flag5 = !enableSmoothing;
		processedValue = num3;
		previousForClamp = num3;
		if (!flag5)
		{
			num8 = smoothingTimeConstant;
			if (smoothingTimeConstant > 0f)
			{
				if (smoothingInitialized)
				{
					float deltaTime = Time.deltaTime;
					float num5 = smoothingTimeConstant;
					bool flag6 = 0.0001f > smoothingTimeConstant;
					float num13 = 0.0001f;
					if (!flag6)
					{
						num13 = smoothingTimeConstant;
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C80]");
					object obj11 = deltaTime ^ 0;
					num8 = (float)obj11 / num13;
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033F3D0");
					float num14 = 1f - num8;
					if (!(0f > num14))
					{
						if (num14 > 1f)
						{
							num14 = 1f;
						}
					}
					else
					{
						num14 = 0f;
					}
					float num15 = processedValue - smoothedValue;
					float num16 = num15 * num14;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
					obj9 = 0;
					num3 = num16 + smoothedValue;
				}
				else
				{
					smoothingInitialized = true;
				}
				smoothedValue = num3;
				if (enableRounding)
				{
					num8 = RoundToPrecision(num3);
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
					obj9 = 0;
					num3 = num8;
				}
			}
		}
		displayTargetValue = num3;
		if (snapWhenLargeJump)
		{
			float num5 = maxValue - minValue;
			if (!(1E-05f > num5))
			{
				bool flag7 = !(num5 > 0f);
				num8 = 1E-05f;
				if (flag7)
				{
					goto IL_0488;
				}
			}
			float num17 = num3 - currentValue;
			num5 = num17 & obj9;
			float num18 = maxValue - minValue;
			bool flag8 = 1E-05f > num18;
			float num19 = 1E-05f;
			if (!flag8)
			{
				num19 = num18;
			}
			num8 = num19 * snapThresholdPercentOfRange;
			if (num5 > num8)
			{
				currentValue = displayTargetValue;
				ApplyNeedleRotation(currentAngle = ComputeTargetAngle(num3));
			}
		}
		goto IL_0488;
		IL_0488:
		bool flag9 = movementMode == GaugeMovementMode.DirectLerp;
		float angle;
		if (!flag9)
		{
			object obj12 = movementMode - 1;
			if (!flag9)
			{
				if ((nint)obj12 == 1)
				{
					float num20 = ComputeTargetAngle(displayTargetValue);
					float deltaTime2 = Time.deltaTime;
					float num5 = currentAngle;
					float num21 = num20 - currentAngle;
					float num22 = deltaTime2 * needleMaxDegreesPerSecond;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
					object obj13 = num21 & 0;
					if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)num22) < System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj13))
					{
						float num23 = num20 - currentAngle;
						bool flag10 = !(num23 < 0f);
						float num24 = 1f;
						if (!flag10)
						{
							num24 = -1f;
						}
						float num25 = num24 * num22;
						angle = (currentAngle = num25 + num5);
					}
					else
					{
						currentAngle = num20;
						angle = num20;
					}
					goto IL_0d75;
				}
			}
			else
			{
				float deltaTime3 = Time.deltaTime;
				float num5 = displayTargetValue;
				float num9 = currentValue;
				float num26 = displayTargetValue - currentValue;
				float num27 = deltaTime3 * valueChaseSpeed;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
				object obj14 = num26 & 0;
				float value;
				if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)num27) < System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj14))
				{
					num5 -= currentValue;
					bool flag11 = !(num5 < 0f);
					float num28 = 1f;
					if (!flag11)
					{
						num28 = -1f;
					}
					float num29 = num28 * num27;
					value = num29 + num9;
				}
				else
				{
					value = displayTargetValue;
				}
				currentValue = value;
				num8 = ComputeTargetAngle(value);
				currentAngle = num8;
			}
			goto IL_0d15;
		}
		float num30 = ComputeTargetAngle(displayTargetValue);
		float deltaTime4 = Time.deltaTime;
		float num31 = deltaTime4 * rotationSpeed;
		if (!(0f > num31))
		{
			if (num31 > 1f)
			{
				num31 = 1f;
			}
		}
		else
		{
			num31 = 0f;
		}
		float num32 = num30 - currentAngle;
		float num33 = num32 * num31;
		angle = (currentAngle = num33 + currentAngle);
		goto IL_0d75;
		IL_0d75:
		num8 = InverseMapAngleToValue(angle);
		currentValue = num8;
		goto IL_0d15;
		IL_0d15:
		ApplyNeedleRotation(currentAngle);
		if (!logEverySecond)
		{
			return;
		}
		int frameCount = Time.frameCount;
		object obj15 = frameCount - lastLogFrame;
		if ((nint)obj15 < 60)
		{
			return;
		}
		int frameCount2 = Time.frameCount;
		lastLogFrame = frameCount2;
		object[] array = new object[6];
		string text = base.name;
		if (text != null)
		{
			nint num34 = (nint)array;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
			object obj16 = default(object);
			if (obj16 == null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
				object obj17 = default(object);
				throw obj17;
			}
		}
		array[0] = text;
		num8 = rawValue;
		Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
		object obj18 = default(object);
		if (obj18 != null)
		{
			nint num35 = (nint)array;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1212 @ rdx_v45 (Il2CppClass<System.Object[]>)+40]");
			object obj19 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
			object obj20 = default(object);
			bool flag12 = obj20 == null;
			object obj21 = obj18;
			if (flag12)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
				object obj22 = default(object);
				throw obj22;
			}
		}
		array[1] = obj18;
		num8 = processedValue;
		Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
		object obj23 = default(object);
		if (obj23 != null)
		{
			nint num36 = (nint)array;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1306 @ rdx_v43 (Il2CppClass<System.Object[]>)+40]");
			object obj24 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
			object obj25 = default(object);
			bool flag13 = obj25 == null;
			object obj26 = obj23;
			if (flag13)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
				object obj27 = default(object);
				throw obj27;
			}
		}
		array[2] = obj23;
		num8 = displayTargetValue;
		Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
		object obj28 = default(object);
		if (obj28 != null)
		{
			nint num37 = (nint)array;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1373 @ rdx_v41 (Il2CppClass<System.Object[]>)+40]");
			object obj29 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
			object obj30 = default(object);
			bool flag14 = obj30 == null;
			object obj31 = obj28;
			if (flag14)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
				object obj32 = default(object);
				throw obj32;
			}
		}
		array[3] = obj28;
		num8 = currentValue;
		Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
		object obj33 = default(object);
		if (obj33 != null)
		{
			nint num38 = (nint)array;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1438 @ rdx_v39 (Il2CppClass<System.Object[]>)+40]");
			object obj34 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
			object obj35 = default(object);
			bool flag15 = obj35 == null;
			object obj36 = obj33;
			if (flag15)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
				object obj37 = default(object);
				throw obj37;
			}
		}
		array[4] = obj33;
		num8 = currentAngle;
		Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
		object obj38 = default(object);
		if (obj38 != null)
		{
			nint num39 = (nint)array;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1483 @ rdx_v37 (Il2CppClass<System.Object[]>)+40]");
			object obj39 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
			object obj40 = default(object);
			bool flag16 = obj40 == null;
			object obj41 = obj38;
			if (flag16)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
				object obj42 = default(object);
				throw obj42;
			}
		}
		array[5] = obj38;
		string message = string.Format("[DialGaugeDisplay:{0}] Raw={1:F3} Proc={2:F3} DispTarget={3:F3} CurrVal={4:F3} Angle={5:F2}", array);
		Debug.Log(message);
		return;
		IL_008f:
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
		goto IL_009e;
	}

	private void InitializeProvider()
	{
		//IL_00eb: Expected O, but got I
		provider = null;
		if (!(this.floatValueProvider != null))
		{
			return;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
		IFloatValueProvider floatValueProvider = default(IFloatValueProvider);
		if (floatValueProvider == null)
		{
			if (string.IsNullOrEmpty(providerPropertyName))
			{
				return;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_vm_object_is_inst\"");
			Type type = default(Type);
			PropertyInfo property = type.GetProperty(providerPropertyName);
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180D16FE0");
			object obj = default(object);
			if (obj != null)
			{
				Type propertyType = property.PropertyType;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B502B8]");
				RuntimeTypeHandle handle = (RuntimeTypeHandle)((nint)0 + (nint)32);
				Type typeFromHandle = Type.GetTypeFromHandle(handle);
				if (((object)propertyType).Equals((object)typeFromHandle))
				{
					ReflectionFloatValueProvider reflectionFloatValueProvider = new ReflectionFloatValueProvider(null, null);
					reflectionFloatValueProvider.target = this.floatValueProvider;
					reflectionFloatValueProvider.prop = property;
					provider = reflectionFloatValueProvider;
				}
			}
		}
		else
		{
			provider = floatValueProvider;
		}
	}

	private void InitializeState()
	{
		float num = minValue;
		float value = targetNumber;
		smoothingInitialized = false;
		smoothedValue = targetNumber;
		previousForClamp = targetNumber;
		rawValue = targetNumber;
		processedValue = targetNumber;
		displayTargetValue = targetNumber;
		if (!(minValue > targetNumber))
		{
			num = maxValue;
			if (!(targetNumber > maxValue))
			{
				goto IL_00b5;
			}
		}
		value = num;
		goto IL_00b5;
		IL_00b5:
		currentValue = value;
		ApplyNeedleRotation(currentAngle = ComputeTargetAngle(value));
	}

	private float RoundToPrecision(float value)
	{
		if (decimalDigits > 0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033FCB0");
			float num = 10f * value;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1800033E0");
			return num / 10f;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1800033E0");
		return value;
	}

	private float NormalizeValue(float value)
	{
		//IL_00ac: Invalid comparison between I4 and F4
		//IL_00be: Expected F4, but got I4
		float num = maxValue - minValue;
		bool flag = !(1E-05f < num);
		float num2 = 1E-05f;
		if (!flag)
		{
			num2 = num;
		}
		bool flag2 = !(0f < num2);
		float result = 0f;
		if (!flag2)
		{
			float num3 = maxValue - minValue;
			float num4 = value - minValue;
			bool flag3 = !(1E-05f < num3);
			float num5 = 1E-05f;
			if (!flag3)
			{
				num5 = num3;
			}
			float num6 = num4 / num5;
			result = num6;
		}
		return result;
	}

	private float DenormalizeValue(float normalized)
	{
		float num = maxValue - minValue;
		bool flag = !(1E-05f < num);
		float num2 = 1E-05f;
		if (!flag)
		{
			num2 = num;
		}
		float num3 = num2 * normalized;
		return num3 + minValue;
	}

	private float ComputeTargetAngle(float value)
	{
		//IL_016c: Invalid comparison between I4 and F4
		//IL_00ac: Expected F4, but got I4
		//IL_0211: Invalid comparison between I4 and F4
		//IL_00e8: Expected F4, but got I4
		//IL_022e: Invalid comparison between I4 and F4
		//IL_0144: Expected F4, but got I4
		//IL_0159: Unknown result type (might be due to invalid IL or missing references)
		//IL_015e: Expected F4, but got Unknown
		float num = maxValue - minValue;
		bool flag = !(1E-05f < num);
		float num2 = 1E-05f;
		if (!flag)
		{
			num2 = num;
		}
		float num6;
		if (0f < num2)
		{
			float num3 = maxValue - minValue;
			float num4 = value - minValue;
			bool flag2 = !(1E-05f < num3);
			float num5 = 1E-05f;
			if (!flag2)
			{
				num5 = num3;
			}
			num6 = num4 / num5;
		}
		else
		{
			num6 = 0f;
		}
		if (!(0f > num6))
		{
			if (num6 > 1f)
			{
				num6 = 1f;
			}
		}
		else
		{
			num6 = 0f;
		}
		if (valueToNormalized != null)
		{
			float num7 = valueToNormalized.Evaluate(num6);
			num6 = num7;
		}
		if (!(0f > num6))
		{
			if (num6 > 1f)
			{
				num6 = 1f;
			}
		}
		else
		{
			num6 = 0f;
		}
		bool flag3 = !invertRotation;
		float num8 = maxAngle - minAngle;
		float num9 = num8 * num6;
		float num10 = num9 + minAngle;
		if (!flag3)
		{
			float num11 = num10;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C80]");
			num10 = num11 ^ 0;
		}
		return num10;
	}

	private float InverseMapAngleToValue(float angle)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Expected O, but got Unknown
		//IL_0453: Invalid comparison between F4 and I4
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Expected F4, but got Unknown
		//IL_0473: Invalid comparison between I4 and F4
		//IL_006c: Expected F4, but got I4
		//IL_00e0: Expected F4, but got I4
		//IL_033f: Expected F4, but got I4
		//IL_0351: Expected O, but got I4
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Expected O, but got Unknown
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Expected O, but got Unknown
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Expected O, but got Unknown
		//IL_0136: Unknown result type (might be due to invalid IL or missing references)
		//IL_013b: Expected O, but got Unknown
		//IL_0167: Expected O, but got I
		//IL_0170: Unknown result type (might be due to invalid IL or missing references)
		//IL_0175: Expected O, but got Unknown
		//IL_052f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0534: Expected O, but got Unknown
		//IL_01da: Expected O, but got I
		//IL_01e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e8: Expected O, but got Unknown
		//IL_024d: Expected O, but got I
		//IL_0256: Unknown result type (might be due to invalid IL or missing references)
		//IL_025b: Expected O, but got Unknown
		//IL_02c8: Expected O, but got I
		//IL_02d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d6: Expected O, but got Unknown
		object obj2 = default(object);
		object obj = obj2 - 95;
		bool flag = !invertRotation;
		float num = angle;
		if (!flag)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C80]");
			num = angle ^ 0;
		}
		float num2 = maxAngle - minAngle;
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"jp short 000000018059BF1Fh\"");
		float num3;
		if (num2 == 0f)
		{
			num3 = 0f;
		}
		else
		{
			float num4 = maxAngle - minAngle;
			float num5 = num - minAngle;
			num3 = num5 / num4;
		}
		if (!(0f > num3))
		{
			if (num3 > 1f)
			{
				num3 = 1f;
			}
		}
		else
		{
			num3 = 0f;
		}
		if (valueToNormalized != null)
		{
			_ = 0;
			_ = 0;
			_ = 0;
			_ = 0;
			int length = valueToNormalized.length;
			if (length == 2)
			{
				object obj3 = obj - 57;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808BF2F0");
				object obj4 = obj - 57;
				object obj5 = obj - 41;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808BF690");
				Span<Keyframe> keys = (Span<Keyframe>)(obj - 41);
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v5 @ rbp_v1-29]");
				_ = 0;
				valueToNormalized.GetKeys(keys);
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v5 @ rbp_v1-39]");
				object obj6 = 0;
				object obj7 = obj - 25;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v443 @ rax_v12+10]");
				_ = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v443 @ rax_v12+18]");
				_ = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181D72B80");
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18038AA80");
				object obj8 = default(object);
				if (obj8 != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v5 @ rbp_v1-39]");
					object obj9 = 0;
					object obj10 = obj - 25;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v463 @ rax_v15+10]");
					_ = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v463 @ rax_v15+18]");
					_ = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181D38490");
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18038AA80");
					object obj11 = default(object);
					if (obj11 != null)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v5 @ rbp_v1-39]");
						object obj12 = 0;
						object obj13 = obj - 25;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v469 @ rax_v18+1C]");
						_ = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v469 @ rax_v18+2C]");
						_ = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v469 @ rax_v18+34]");
						_ = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181D72B80");
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18038AA80");
						object obj14 = default(object);
						if (obj14 != null)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v5 @ rbp_v1-39]");
							object obj15 = 0;
							object obj16 = obj - 25;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v473 @ rax_v21+1C]");
							_ = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v473 @ rax_v21+2C]");
							_ = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v473 @ rax_v21+34]");
							_ = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181D38490");
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18038AA80");
							object obj17 = default(object);
							if (obj17 != null)
							{
								goto IL_04ed;
							}
						}
					}
				}
			}
			float num6 = 0f;
			float num7 = 1f;
			object obj18 = 0;
			do
			{
				float num8 = num7 + num6;
				float num9 = num8 * 0.5f;
				float num10 = valueToNormalized.Evaluate(num9);
				if (!(num3 > num10))
				{
					num7 = num9;
				}
				else
				{
					num6 = num9;
				}
				obj18++;
			}
			while ((nint)obj18 < 8);
			float num11 = num7 + num6;
			num3 = num11 * 0.5f;
		}
		goto IL_04ed;
		IL_04ed:
		float num12 = maxValue - minValue;
		bool flag2 = 1E-05f > num12;
		float num13 = 1E-05f;
		if (!flag2)
		{
			num13 = num12;
		}
		bool flag3 = !clampToRange;
		float num14 = num13 * num3;
		float num15 = num14 + minValue;
		if (!flag3)
		{
			if (!(minValue > num15))
			{
				if (num15 > maxValue)
				{
					num15 = maxValue;
				}
			}
			else
			{
				num15 = minValue;
			}
		}
		return num15;
	}

	private bool IsCurveLinear(AnimationCurve curve)
	{
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Expected O, but got Unknown
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Expected O, but got Unknown
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Expected O, but got Unknown
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Expected O, but got Unknown
		//IL_00ad: Expected O, but got I
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Expected O, but got Unknown
		//IL_0120: Expected O, but got I
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Expected O, but got Unknown
		//IL_0193: Expected O, but got I
		//IL_019c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a1: Expected O, but got Unknown
		//IL_020e: Expected O, but got I
		//IL_0217: Unknown result type (might be due to invalid IL or missing references)
		//IL_021c: Expected O, but got Unknown
		_ = 0;
		_ = 0;
		_ = 0;
		_ = 0;
		if (curve != null)
		{
			int length = curve.length;
			if (length == 2)
			{
				object obj2 = default(object);
				object obj = obj2 - 64;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808BF2F0");
				object obj3 = obj2 - 64;
				object obj4 = obj2 - 48;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808BF690");
				Span<Keyframe> keys = (Span<Keyframe>)(obj2 - 48);
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v5 @ rsp-30]");
				_ = 0;
				curve.GetKeys(keys);
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v5 @ rsp-40]");
				object obj5 = 0;
				object obj6 = obj2 - 32;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v179 @ rax_v10+10]");
				_ = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v179 @ rax_v10+18]");
				_ = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181D72B80");
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18038AA80");
				object obj7 = default(object);
				if (obj7 != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v5 @ rsp-40]");
					object obj8 = 0;
					object obj9 = obj2 - 32;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v183 @ rax_v13+10]");
					_ = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v183 @ rax_v13+18]");
					_ = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181D38490");
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18038AA80");
					object obj10 = default(object);
					if (obj10 != null)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v5 @ rsp-40]");
						object obj11 = 0;
						object obj12 = obj2 - 32;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v186 @ rax_v16+1C]");
						_ = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v186 @ rax_v16+2C]");
						_ = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v186 @ rax_v16+34]");
						_ = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181D72B80");
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18038AA80");
						object obj13 = default(object);
						if (obj13 != null)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v5 @ rsp-40]");
							object obj14 = 0;
							object obj15 = obj2 - 32;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v190 @ rax_v19+1C]");
							_ = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v190 @ rax_v19+2C]");
							_ = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v190 @ rax_v19+34]");
							_ = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181D38490");
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18038AA80");
							bool result = default(bool);
							return result;
						}
					}
				}
			}
		}
		return false;
	}

	private float ApproximateCurveInverse(AnimationCurve curve, float y, int iterations)
	{
		//IL_003e: Expected F4, but got I4
		//IL_0050: Expected F4, but got I4
		//IL_0062: Expected O, but got I4
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Expected O, but got Unknown
		bool flag = iterations <= 0;
		float num = 0f;
		float num2 = 1f;
		float num3 = 0f;
		float num4 = 1f;
		object obj = 0;
		if (!flag)
		{
			bool flag2;
			do
			{
				float num5 = num4 + num3;
				float num6 = num5 * 0.5f;
				float num7 = curve.Evaluate(num6);
				if (!(y > num7))
				{
					num4 = num6;
				}
				else
				{
					num3 = num6;
				}
				obj++;
				flag2 = (nint)obj < iterations;
				num = num3;
				num2 = num4;
			}
			while (flag2);
		}
		float num8 = num2 + num;
		return num8 * 0.5f;
	}

	private float MoveTowards(float current, float target, float maxDelta)
	{
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Expected O, but got Unknown
		//IL_002c: Invalid comparison between F4 and O
		//IL_005b: Invalid comparison between F4 and I4
		float num = target - current;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
		object obj = num & 0;
		if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)maxDelta) < System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj))
		{
			float num2 = target - current;
			if (!(num2 < 0f))
			{
				float num3 = 1f * maxDelta;
				return num3 + current;
			}
			float num4 = -1f * maxDelta;
			return num4 + current;
		}
		return target;
	}

	private unsafe void ApplyNeedleRotation(float angle)
	{
		//IL_0049: Expected O, but got Ref
		if (needleTransform != null)
		{
			Vector3 axis = default(Vector3);
			Quaternion quaternion = Quaternion.Internal_AngleAxis(angle, ref axis);
			needleTransform.localRotation = (Quaternion)(&axis);
		}
	}

	public DialGaugeDisplay()
	{
		//IL_0094: Expected I, but got O
		//IL_0134: Expected I4, but got I8
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3AD74]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		providerPropertyName = "CurrentValue";
		clampToRange = true;
		decimalDigits = 2;
		smoothingTimeConstant = 0.25f;
		maxValue = 100f;
		minAngle = -45f;
		maxAngle = 45f;
		nint num = (nint)typeof(Vector3);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v46 @ rax_v5 (Il2CppClass<UnityEngine.Vector3>)+B8]");
		nint num2 = 0;
		rotationAxis = Vector3.forwardVector;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v51 @ rcx_v4 (Il2CppStaticFields<UnityEngine.Vector3>)+50]");
		_ = 0;
		valueToNormalized = AnimationCurve.Linear(0f, 0f, 1f, 1f);
		rotationSpeed = 5f;
		valueChaseSpeed = 100f;
		needleMaxDegreesPerSecond = 180f;
		snapThresholdPercentOfRange = 0.35f;
		drawGizmos = true;
		gizmoDivisions = 5;
		lastLogFrame = -1;
		base._002Ector();
	}
}
