using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using UnityEngine;

public class OdometerDisplay : MonoBehaviour
{
	private class DrumState
	{
		public int currentDigit;

		public int targetDigit;

		public float currentAngle;

		public float stepProgress;

		public DrumState(int current, int target)
		{
			currentDigit = current;
			currentAngle = 0f;
			targetDigit = target;
		}
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

	public float targetNumber;

	public Transform[] drums;

	public float maxRevolutionSpeed;

	public float countSpeed;

	public Vector3 rotationAxis;

	public bool invertRotation;

	public MonoBehaviour floatValueProvider;

	public string providerPropertyName;

	public int integerDigits;

	public int decimalDigits;

	public bool showDecimalPoint;

	public bool useConsistentDirection;

	public bool flipConsistentDirection;

	public bool useAbsoluteValue;

	public bool enableSmoothing;

	public float smoothingTimeConstant;

	public float maxPerFrameInputDelta;

	private IFloatValueProvider provider;

	private float currentNumber;

	private float displayTargetNumber;

	private bool smoothingInitialized;

	private float smoothedValue;

	private DrumState[] drumStates;

	private int drumCount;

	private const int DigitsOnDrum = 10;

	private const float DegreesPerDigit = 36f;

	public float DisplayedNumber
	{
		get
		{
			//IL_001d: Expected F4, but got I4
			//IL_0174: Expected F4, but got I4
			//IL_0040: Unknown result type (might be due to invalid IL or missing references)
			//IL_0045: Expected O, but got Unknown
			//IL_0067: Expected O, but got I4
			//IL_0070: Expected O, but got I4
			//IL_0197: Unknown result type (might be due to invalid IL or missing references)
			//IL_019c: Expected O, but got Unknown
			//IL_01ac: Expected O, but got I4
			//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
			//IL_00da: Expected O, but got Unknown
			//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
			//IL_00e8: Expected O, but got Unknown
			//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
			//IL_00f6: Expected O, but got Unknown
			//IL_0244: Unknown result type (might be due to invalid IL or missing references)
			//IL_0249: Expected O, but got Unknown
			bool flag = integerDigits <= 0;
			float num = 0f;
			if (flag)
			{
				goto IL_014c;
			}
			DrumState[] array = drumStates;
			object obj = drumStates + 32;
			bool flag2 = drumStates == null;
			float num3 = default(float);
			float num2 = num3;
			object obj2 = 0;
			object obj3 = 0;
			if (flag2)
			{
				goto IL_02a2;
			}
			while ((nint)obj3 < array.Length)
			{
				object obj4 = obj;
				bool flag3 = obj == null;
				num3 = num2;
				if (!flag3)
				{
					obj3++;
					obj2++;
					obj += 8;
					float num4 = 0f * 10f;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v38 @ rcx_v9+10]");
					num2 = 0f + num4;
					bool flag4 = (nint)obj2 < integerDigits;
					num3 = num2;
					num = num2;
					if (flag4)
					{
						continue;
					}
					goto IL_014c;
				}
				goto IL_02a2;
			}
			goto IL_02b6;
			IL_014c:
			int num5 = integerDigits;
			bool flag5 = integerDigits >= drumCount;
			float num6 = 0f;
			if (flag5)
			{
				goto IL_028e;
			}
			DrumState[] array2 = drumStates;
			object obj5 = drumStates + 32;
			object obj6 = integerDigits * 8;
			object obj7 = obj5 + obj6;
			float num7 = 1f;
			int num8 = integerDigits;
			while (true)
			{
				num7 *= 10f;
				if (drumStates == null)
				{
					break;
				}
				bool flag6 = num5 >= array2.Length;
				num2 = num3;
				if (!flag6)
				{
					object obj8 = obj7;
					if (obj7 == null)
					{
						break;
					}
					num5++;
					num8++;
					obj7 += 8;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v124 @ rcx_v7+10]");
					num3 = 0f / num7;
					num6 = 0f + num3;
					if (num8 < drumCount)
					{
						continue;
					}
					goto IL_028e;
				}
				goto IL_02b6;
			}
			goto IL_02a2;
			IL_028e:
			return num6 + num;
			IL_02a2:
			throw new NullReferenceException();
			IL_02b6:
			throw new IndexOutOfRangeException();
		}
	}

	public float DisplayTargetNumber => displayTargetNumber;

	public float CurrentNumber => currentNumber;

	private void Awake()
	{
		//IL_015f: Expected O, but got I
		int num = decimalDigits + integerDigits;
		Transform[] array = drums;
		drumCount = num;
		if (array.Length != num)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
			object arg = default(object);
			object arg2 = default(object);
			string message = $"[OdometerDisplay] Drum count ({arg}) does not match required digits ({arg2}). Assign all drum transforms.";
			Debug.LogWarning(message);
		}
		DrumState[] array2 = new DrumState[drumCount];
		drumStates = array2;
		SetValueInstant(targetNumber);
		bool flag = this.floatValueProvider != null;
		bool flag2 = !flag;
		PropertyInfo propertyInfo = null;
		if (!flag2)
		{
			bool flag3 = string.IsNullOrEmpty(providerPropertyName);
			propertyInfo = null;
			if (!flag3)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_vm_object_is_inst\"");
				Type type = default(Type);
				PropertyInfo property = type.GetProperty(providerPropertyName);
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180D16FE0");
				object obj = default(object);
				bool flag4 = obj == null;
				propertyInfo = null;
				if (!flag4)
				{
					Type propertyType = property.PropertyType;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B502B8]");
					RuntimeTypeHandle handle = (RuntimeTypeHandle)((nint)0 + (nint)32);
					Type typeFromHandle = Type.GetTypeFromHandle(handle);
					bool flag5 = ((object)propertyType).Equals((object)typeFromHandle);
					bool flag6 = !flag5;
					propertyInfo = null;
					if (!flag6)
					{
						ReflectionFloatValueProvider reflectionFloatValueProvider = new ReflectionFloatValueProvider(null, null);
						reflectionFloatValueProvider.target = this.floatValueProvider;
						reflectionFloatValueProvider.prop = property;
						provider = reflectionFloatValueProvider;
						return;
					}
				}
			}
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
		IFloatValueProvider floatValueProvider = default(IFloatValueProvider);
		if (floatValueProvider != null)
		{
			provider = floatValueProvider;
		}
	}

	private unsafe void Update()
	{
		//IL_0690: Invalid comparison between I4 and F4
		//IL_0035: Expected I, but got O
		//IL_0180: Expected F4, but got I4
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Expected F4, but got Unknown
		//IL_06d8: Expected F4, but got I
		//IL_06f3: Invalid comparison between F4 and I4
		//IL_0706: Expected F4, but got I4
		//IL_00e1: Expected I, but got O
		//IL_006d: Expected O, but got I
		//IL_0076: Expected O, but got I4
		//IL_01a2: Expected O, but got I4
		//IL_0157: Expected O, but got I
		//IL_0160: Unknown result type (might be due to invalid IL or missing references)
		//IL_0165: Expected O, but got Unknown
		//IL_016d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0172: Expected O, but got Unknown
		//IL_04e7: Expected O, but got I4
		//IL_0202: Invalid comparison between F4 and I4
		//IL_0757: Expected O, but got F4
		//IL_0761: Invalid comparison between O and F4
		//IL_0773: Expected O, but got I4
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Expected O, but got Unknown
		//IL_01b9: Expected O, but got I4
		//IL_03b6: Expected O, but got F4
		//IL_03c5: Expected O, but got F4
		//IL_03ea: Invalid comparison between F4 and I4
		//IL_03f9: Invalid comparison between F4 and I4
		//IL_01c7: Invalid comparison between F4 and I4
		//IL_08ae: Expected O, but got I4
		//IL_08b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_08bb: Expected O, but got Unknown
		//IL_0339: Expected O, but got F4
		//IL_0346: Expected O, but got F4
		//IL_036b: Invalid comparison between F4 and I4
		//IL_037a: Invalid comparison between F4 and I4
		//IL_0953: Expected F4, but got I
		//IL_07b2: Expected O, but got I4
		//IL_080f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0814: Expected O, but got Unknown
		//IL_0846: Invalid comparison between I4 and F4
		//IL_053e: Expected O, but got I
		//IL_02b9: Expected F4, but got I4
		//IL_043e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0443: Expected O, but got Unknown
		//IL_0455: Expected O, but got I4
		//IL_055a: Expected O, but got I
		//IL_046a: Expected O, but got I
		//IL_0582: Expected F4, but got I
		//IL_0478: Unknown result type (might be due to invalid IL or missing references)
		//IL_047d: Expected O, but got Unknown
		//IL_0486: Unknown result type (might be due to invalid IL or missing references)
		//IL_048b: Expected O, but got Unknown
		//IL_05ad: Expected O, but got Ref
		//IL_05ad: Expected O, but got I
		//IL_05bc: Expected I, but got O
		//IL_05ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_05cf: Expected O, but got Unknown
		bool flag = provider == null;
		IFloatValueProvider floatValueProvider = (IFloatValueProvider)this;
		IFloatValueProvider floatValueProvider2;
		if (!flag)
		{
			floatValueProvider2 = provider;
			nint num = (nint)floatValueProvider2;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v48 @ r10_v9 (Il2CppClass<IFloatValueProvider>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_00ad;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v48 @ r10_v9 (Il2CppClass<IFloatValueProvider>)+B0]");
			object obj = 0;
			object obj2 = 0;
			while (true)
			{
				object obj3 = obj2 + obj2;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v106 @ r8_v19+v133 @ rax_v50*8]");
				if (0 == (nint)typeof(IFloatValueProvider))
				{
					break;
				}
				obj2++;
				object obj4 = obj2;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v48 @ r10_v9 (Il2CppClass<IFloatValueProvider>)+12E]");
				if ((nint)obj4 < 0)
				{
					continue;
				}
				goto IL_00ad;
			}
			object obj5 = obj2 + obj2;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v106 @ r8_v19+8+v172 @ rcx_v35*8]");
			object obj6 = (nint)0 << 4;
			object obj7 = obj6 + 312;
			object obj8 = obj7 + num;
			goto IL_00bc;
		}
		goto IL_08ec;
		IL_00bc:
		float floatValue = floatValueProvider2.GetFloatValue();
		float num2 = default(float);
		targetNumber = num2;
		nint num3 = (nint)typeof(IFloatValueProvider);
		floatValueProvider = floatValueProvider2;
		goto IL_08ec;
		IL_08ec:
		float num4 = targetNumber;
		if (useAbsoluteValue)
		{
			float num5 = num4;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
			num4 = num5 & 0;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033FCB0");
		int num6 = -decimalDigits;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033FCB0");
		float num7 = 10f - 10f;
		if (!(0f > num4))
		{
			if (num4 > num7)
			{
				num4 = num7;
			}
		}
		else
		{
			num4 = 0f;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033FCB0");
		float num8 = 10f * num4;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1800033E0");
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
		float num9 = 0f;
		float num10 = num8 / 10f;
		bool flag2 = !(maxPerFrameInputDelta > 0f);
		float num11 = decimalDigits;
		if (!flag2)
		{
			bool flag3 = smoothingInitialized;
			IFloatValueProvider floatValueProvider3 = (IFloatValueProvider)132;
			if (!flag3)
			{
				floatValueProvider3 = (IFloatValueProvider)124;
			}
			float num12 = num10;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v311 @ rax_v42 (IFloatValueProvider)+v32 @ rcx_v1 (OdometerDisplay)]");
			num11 = num12 - 0f;
			object obj9 = num11 & num9;
			bool flag4 = System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj9) <= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)maxPerFrameInputDelta);
			floatValueProvider = (IFloatValueProvider)124;
			if (!flag4)
			{
				float num13 = ((num11 < 0f) ? (-1f) : 1f);
				float num14 = num13 * maxPerFrameInputDelta;
				float num15 = num14;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v311 @ rax_v42 (IFloatValueProvider)+v32 @ rcx_v1 (OdometerDisplay)]");
				num10 = num15 + 0f;
				floatValueProvider = (IFloatValueProvider)124;
			}
		}
		if (enableSmoothing && smoothingTimeConstant > 0f)
		{
			if (smoothingInitialized)
			{
				float deltaTime = Time.deltaTime;
				num9 = smoothingTimeConstant;
				bool flag5 = 0.0001f > smoothingTimeConstant;
				float num16 = 0.0001f;
				if (!flag5)
				{
					num16 = smoothingTimeConstant;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C80]");
				object obj10 = deltaTime ^ 0;
				float num17 = (float)obj10 / num16;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033F3D0");
				float num18 = 1f - num17;
				if (!(0f > num18))
				{
					if (num18 > 1f)
					{
						num18 = 1f;
					}
				}
				else
				{
					num18 = 0f;
				}
				float num19 = num10 - smoothedValue;
				float num20 = num19 * num18;
				num10 = num20 + smoothedValue;
				floatValueProvider = null;
			}
			else
			{
				smoothingInitialized = true;
			}
			smoothedValue = num10;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033FCB0");
			float num21 = 10f * num10;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1800033E0");
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
			num9 = 0f;
			num10 = num21 / 10f;
		}
		displayTargetNumber = num10;
		float num22 = currentNumber - num10;
		float num23 = num22 & num9;
		if (num23 > 0.0009f)
		{
			float deltaTime2 = Time.deltaTime;
			float num24 = deltaTime2 * countSpeed;
			float num25;
			bool flag6;
			bool flag7;
			bool flag8;
			if (!(displayTargetNumber > currentNumber))
			{
				num25 = currentNumber - num24;
				float num26 = num25 - displayTargetNumber;
				object obj11 = num25 ^ displayTargetNumber;
				object obj12 = num25 ^ num26;
				object obj13 = obj11 & obj12;
				flag6 = (nint)obj13 < 0;
				flag7 = num26 < 0f;
				flag8 = num26 == 0f;
			}
			else
			{
				num25 = currentNumber + num24;
				float num27 = displayTargetNumber - num25;
				object obj14 = displayTargetNumber ^ num25;
				object obj15 = displayTargetNumber ^ num27;
				object obj16 = obj14 & obj15;
				flag6 = (nint)obj16 < 0;
				flag7 = num27 < 0f;
				flag8 = num27 == 0f;
			}
			bool flag9 = flag7 == flag6;
			object obj17 = !flag8;
			object obj18 = flag9 & obj17;
			if (obj18 == null)
			{
				num25 = displayTargetNumber;
			}
			currentNumber = num25;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033FCB0");
			float num28 = 10f * num25;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1800033E0");
			int[] array = ExtractDigits(currentNumber = num28 / 10f);
			if (drumCount > 0)
			{
				object obj19 = array + 32;
				nint num29 = 32;
				object obj20 = 0;
				bool flag10;
				do
				{
					DrumState[] array2 = drumStates;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v781 @ r9_v10 (Il2CppClass<IFloatValueProvider>)+v428 @ rdx_v12 (DrumState[])]");
					object obj21 = 0;
					obj20++;
					obj19 += 4;
					num3 = num29 + 8;
					flag10 = (nint)obj20 < drumCount;
					num29 = num3;
				}
				while (flag10);
			}
		}
		AnimateDrums();
		bool flag11 = drumCount <= 0;
		object obj22 = 0;
		nint num30 = 32;
		if (flag11)
		{
			return;
		}
		Vector3 axis = default(Vector3);
		float num31 = default(float);
		do
		{
			Transform[] array3 = drums;
			if ((nint)obj22 < array3.Length)
			{
				DrumState[] array4 = drumStates;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v606 @ rsi_v5 (Il2CppClass<IFloatValueProvider>)+v867 @ rax_v20 (DrumState[])]");
				object obj23 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v606 @ rsi_v5 (Il2CppClass<IFloatValueProvider>)+v646 @ rax_v19 (UnityEngine.Transform[])]");
				if ((UnityEngine.Object)0 != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v786 @ rcx_v10+18]");
					num23 = Quaternion.Internal_AngleAxis(0f, ref axis).x;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v606 @ rsi_v5 (Il2CppClass<IFloatValueProvider>)+v646 @ rax_v19 (UnityEngine.Transform[])]");
					((Transform)0).localRotation = (Quaternion)(&num31);
					axis = rotationAxis;
					num3 = unchecked((nint)null);
				}
				obj22++;
				num30 += 8;
				continue;
			}
			break;
		}
		while ((nint)obj22 < drumCount);
		return;
		IL_00ad:
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
		goto IL_00bc;
	}

	public unsafe void SetValueInstant(float value)
	{
		//IL_056a: Invalid comparison between I4 and F4
		//IL_0042: Expected F4, but got I4
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Expected O, but got Unknown
		//IL_00a8: Expected F4, but got I4
		//IL_00b9: Expected O, but got I4
		//IL_00f7: Expected I4, but got O
		//IL_0104: Expected I4, but got O
		//IL_015b: Expected I, but got O
		//IL_01da: Unknown result type (might be due to invalid IL or missing references)
		//IL_01df: Expected O, but got Unknown
		//IL_01f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fd: Expected O, but got Unknown
		//IL_0206: Unknown result type (might be due to invalid IL or missing references)
		//IL_020b: Expected O, but got Unknown
		//IL_0215: Invalid comparison between F4 and I4
		//IL_0263: Expected I4, but got O
		//IL_026c: Expected O, but got I4
		//IL_0275: Expected F4, but got I4
		//IL_0295: Invalid comparison between F4 and I4
		//IL_038d: Expected O, but got I
		//IL_03c9: Expected O, but got I
		//IL_03ef: Expected O, but got I
		//IL_0407: Expected F4, but got O
		//IL_0417: Expected F4, but got I4
		//IL_043b: Expected O, but got I4
		//IL_0456: Expected F4, but got I4
		//IL_045e: Expected O, but got Ref
		//IL_04f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_04fc: Expected O, but got Unknown
		//IL_0506: Invalid comparison between F4 and I4
		//IL_0481: Expected O, but got Ref
		//IL_0481: Expected O, but got I
		//IL_04a1: Expected O, but got I4
		//IL_04bc: Expected F4, but got I4
		//IL_04d9: Expected O, but got I
		targetNumber = value;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033FCB0");
		int num = -decimalDigits;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033FCB0");
		float num2 = 10f - 10f;
		float num3;
		if (!(0f > value))
		{
			bool flag = !(value > num2);
			num3 = value;
			if (!flag)
			{
				num3 = num2;
			}
		}
		else
		{
			num3 = 0f;
		}
		currentNumber = num3;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033FCB0");
		float num4 = 10f * num3;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1800033E0");
		float num5 = num4 / 10f;
		smoothingInitialized = false;
		currentNumber = num5;
		displayTargetNumber = num5;
		smoothedValue = num5;
		int[] array = ExtractDigits(num5);
		if (drumCount <= 0)
		{
			return;
		}
		object obj = array + 32;
		bool flag2 = array == null;
		int num6 = 0;
		float num7 = num5;
		int[] array2 = array;
		float num8 = 0f;
		object obj2 = obj;
		object obj3 = 32;
		if (!flag2)
		{
			object obj4 = default(object);
			Vector3 axis = default(Vector3);
			object obj7 = default(object);
			float x = default(float);
			object obj8 = default(object);
			while (true)
			{
				DrumState[] array3 = drumStates;
				DrumState drumState = new DrumState(0, 0);
				drumState.currentDigit = (int)obj;
				drumState.targetDigit = (int)obj;
				drumState.currentAngle = 0f;
				bool flag3 = drumStates == null;
				int num9 = 0;
				num6 = 0;
				num7 = num5;
				array2 = (int[])(object)drumState;
				if (flag3)
				{
					break;
				}
				nint num10 = (nint)array3;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v607 @ rdx_v4 (Il2CppClass<DrumState[]>)+40]");
				num9 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
				bool flag4 = obj4 == null;
				num6 = 0;
				num7 = num5;
				array2 = (int[])(object)drumState;
				if (!flag4)
				{
					float num11 = num8 + 4f;
					float num12 = num11 * 8f;
					array2 = (int[])(drumStates + num12);
					num8++;
					obj = obj2 + 4;
					obj3 += 8;
					if (num8 < (float)drumCount)
					{
						obj2 = obj;
						continue;
					}
					if (drumCount <= 0)
					{
						return;
					}
					num9 = (int)drumState;
					object obj5 = 32;
					float num13 = 0f;
					num6 = 0;
					num7 = num5;
					while (true)
					{
						Transform[] array4 = drums;
						if (drums == null)
						{
							break;
						}
						if (!(num13 < (float)array4.Length))
						{
							return;
						}
						DrumState[] array5 = drumStates;
						if (drumStates == null)
						{
							break;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v183 @ r12_v4+v297 @ rax_v22 (DrumState[])]");
						num9 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v183 @ r12_v4+v297 @ rax_v22 (DrumState[])]");
						if ((nint)0 == 0)
						{
							break;
						}
						bool flag5 = !invertRotation;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v587 @ rdx_v12 (System.Int32)+10]");
						num5 = 0f * 36f;
						if (!flag5)
						{
							num5 ^= -0f;
						}
						Transform[] array6 = drums;
						if (drums == null)
						{
							break;
						}
						DrumState[] array7 = drumStates;
						if (drumStates == null)
						{
							break;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v183 @ r12_v4+v298 @ rax_v24 (DrumState[])]");
						array2 = (int[])0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v183 @ r12_v4+v298 @ rax_v24 (DrumState[])]");
						if ((nint)0 == 0)
						{
							break;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v183 @ r12_v4+v300 @ rax_v23 (UnityEngine.Transform[])]");
						bool flag6 = (UnityEngine.Object)0 != null;
						num9 = 0;
						num6 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v183 @ r12_v4+v300 @ rax_v23 (UnityEngine.Transform[])]");
						array2 = (int[])0;
						if (flag6)
						{
							num5 = (float)rotationAxis;
							Quaternion quaternion = Quaternion.Internal_AngleAxis((float)array2.Length, ref axis);
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v183 @ r12_v4+v300 @ rax_v23 (UnityEngine.Transform[])]");
							bool flag7 = (nint)0 == 0;
							object obj6 = 0;
							num9 = 0;
							num6 = (int)(&axis);
							num7 = array2.Length;
							array2 = (int[])(&obj7);
							if (flag7)
							{
								break;
							}
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v183 @ r12_v4+v300 @ rax_v23 (UnityEngine.Transform[])]");
							((Transform)0).localRotation = (Quaternion)(&x);
							x = quaternion.x;
							axis = rotationAxis;
							obj6 = 0;
							num9 = (int)(&x);
							num6 = 0;
							num7 = array2.Length;
							num5 = quaternion.x;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v183 @ r12_v4+v300 @ rax_v23 (UnityEngine.Transform[])]");
							array2 = (int[])0;
						}
						num13++;
						obj5 += 8;
						if (!(num13 < (float)drumCount))
						{
							return;
						}
					}
					break;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
				throw obj8;
			}
		}
		throw new NullReferenceException();
	}

	private float RoundToPrecision(float value)
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtsi2ss xmm1,dword ptr [rcx+5Ch]\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033FCB0");
		float num = 10f * value;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1800033E0");
		return num / 10f;
	}

	private void UpdateDrumTargets()
	{
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Expected O, but got Unknown
		//IL_0049: Expected O, but got I4
		//IL_0052: Expected O, but got I4
		//IL_0067: Expected O, but got I
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Expected O, but got Unknown
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Expected O, but got Unknown
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Expected O, but got Unknown
		int[] array = ExtractDigits(currentNumber);
		if (drumCount > 0)
		{
			object obj = array + 32;
			object obj2 = 32;
			object obj3 = 0;
			do
			{
				DrumState[] array2 = drumStates;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v63 @ r9_v3+v25 @ rcx_v3 (DrumState[])]");
				object obj4 = 0;
				obj3++;
				obj += 4;
				obj2 += 8;
			}
			while ((nint)obj3 < drumCount);
		}
	}

	private int[] ExtractDigits(float value)
	{
		//IL_0341: Expected I, but got O
		//IL_0069: Expected O, but got I4
		//IL_0072: Expected O, but got I4
		//IL_01e6: Expected O, but got I4
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Expected O, but got Unknown
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Expected O, but got Unknown
		//IL_00b3: Expected O, but got I4
		//IL_020d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0212: Expected O, but got Unknown
		//IL_021b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0220: Expected O, but got Unknown
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Expected O, but got Unknown
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Expected O, but got Unknown
		//IL_0161: Expected O, but got F8
		//IL_0174: Unknown result type (might be due to invalid IL or missing references)
		//IL_0179: Expected O, but got Unknown
		//IL_0243: Unknown result type (might be due to invalid IL or missing references)
		//IL_0248: Expected O, but got Unknown
		//IL_0284: Unknown result type (might be due to invalid IL or missing references)
		//IL_0289: Expected O, but got Unknown
		//IL_02cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d0: Expected O, but got Unknown
		int[] array = new int[drumCount];
		nint num = (nint)typeof(Math);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v53 @ rcx_v4 (Il2CppClass<System.Math>)+E4]");
		bool flag = (nint)0 < (nint)0;
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtss2sd xmm0,xmm6\"");
		double num2 = Math.Floor(0.0);
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033FCB0");
		float num3 = value - (float)num2;
		float num4 = 10f * num3;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803B9370");
		object obj = integerDigits - 1;
		object obj2 = 0;
		if (flag)
		{
			goto IL_01d6;
		}
		object obj3 = obj + 8;
		object obj4 = obj3 * 4;
		object obj5 = (object)array + obj4;
		object obj6 = 0;
		double num5 = num2;
		while ((nint)obj < array.Length)
		{
			object obj7 = obj - 1;
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"imul edi\"");
			object obj8 = obj6 >> 2;
			object obj9 = obj8 >> 31;
			obj6 = obj8 + obj9;
			object obj10 = obj6 * 4;
			object obj11 = obj6 + obj10;
			object obj12 = obj11 + obj11;
			double num6 = num5 - (double)obj12;
			obj5 = num6;
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"imul edi\"");
			obj5 -= 4;
			object obj13 = obj6 >> 2;
			object obj14 = obj13 >> 31;
			num5 = (double)obj13 + (double)obj14;
			bool flag2 = (nint)obj >= array.Length;
			obj = obj7;
			obj2 = obj6;
			if (flag2)
			{
				continue;
			}
			goto IL_01d6;
		}
		goto IL_035d;
		IL_01d6:
		object obj15 = drumCount - 1;
		if ((nint)obj15 < integerDigits)
		{
			goto IL_031a;
		}
		object obj16 = obj15 + 8;
		object obj17 = obj16 * 4;
		object obj18 = (object)array + obj17;
		object obj20 = default(object);
		object obj19 = obj20;
		while ((nint)obj15 < array.Length)
		{
			obj15--;
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"imul r9d\"");
			object obj21 = obj2 >> 2;
			object obj22 = obj21 >> 31;
			obj2 = obj21 + obj22;
			object obj23 = obj2 * 4;
			object obj24 = obj2 + obj23;
			object obj25 = obj24 + obj24;
			object obj26 = obj19 - obj25;
			obj18 = obj26;
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"imul r9d\"");
			obj18 -= 4;
			object obj27 = obj2 >> 2;
			object obj28 = obj27 >> 31;
			obj19 = obj27 + obj28;
			if ((nint)obj15 >= integerDigits)
			{
				continue;
			}
			goto IL_031a;
		}
		goto IL_035d;
		IL_035d:
		return (int[])(object)new IndexOutOfRangeException();
		IL_031a:
		return array;
	}

	private void AnimateDrums()
	{
		//IL_002b: Expected F4, but got I4
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Expected F4, but got Unknown
		//IL_007a: Invalid comparison between F4 and I4
		//IL_009b: Expected F4, but got I4
		//IL_0124: Expected F4, but got I4
		//IL_0131: Expected F4, but got I8
		//IL_00cb: Expected F4, but got I8
		//IL_0146: Expected O, but got I
		//IL_00e2: Expected F4, but got I4
		//IL_023b: Expected O, but got I
		//IL_0244: Unknown result type (might be due to invalid IL or missing references)
		//IL_0249: Expected O, but got Unknown
		//IL_0261: Expected O, but got I
		//IL_027c: Expected I, but got O
		//IL_028a: Expected O, but got I
		//IL_0292: Unknown result type (might be due to invalid IL or missing references)
		//IL_0297: Expected O, but got Unknown
		//IL_01fd: Invalid comparison between F4 and I4
		//IL_04ba: Expected O, but got I4
		//IL_04eb: Expected O, but got I4
		//IL_0538: Expected F4, but got I4
		//IL_0545: Expected F4, but got I8
		//IL_0302: Expected O, but got I
		//IL_0329: Expected O, but got I
		//IL_0344: Expected I, but got O
		//IL_0352: Expected O, but got I
		//IL_035a: Unknown result type (might be due to invalid IL or missing references)
		//IL_035f: Expected O, but got Unknown
		//IL_0374: Unknown result type (might be due to invalid IL or missing references)
		//IL_0379: Expected O, but got Unknown
		bool flag = (useConsistentDirection ? 1 : 0) < (false ? 1 : 0);
		bool flag2 = !useConsistentDirection;
		float num = 0f;
		if (!flag2)
		{
			float num2 = displayTargetNumber - currentNumber;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
			float num3 = num2 & 0;
			float num4 = num3 - 0.0001f;
			flag = num4 < 0f;
			bool flag3 = !(num3 > 0.0001f);
			num = 0f;
			if (!flag3)
			{
				bool flag4 = !(displayTargetNumber > currentNumber);
				num = 4.2949673E+09f;
				if (!flag4)
				{
					num = 1f;
				}
				flag = (flipConsistentDirection ? 1 : 0) < (false ? 1 : 0);
				if (flipConsistentDirection)
				{
					num = 0f - num;
				}
			}
		}
		float num5 = (float)drumCount - 1f;
		if (flag)
		{
			return;
		}
		float num6 = num5 * 8f;
		float num7 = num6 + 32f;
		OdometerDisplay odometerDisplay = this;
		float num8 = 1f;
		float num9 = 4.2949673E+09f;
		nint num14 = default(nint);
		object obj17;
		do
		{
			DrumState[] array = drumStates;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v207 @ r12_v4 (System.Single)+v225 @ rbx_v3 (DrumState[])]");
			object obj = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v206 @ rbx_v6+10]");
			nint num10 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v206 @ rbx_v6+14]");
			bool flag5;
			if (num10 == 0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v206 @ rbx_v6+10]");
				float num11 = 0f * 36f;
				flag5 = (invertRotation ? 1 : 0) < (false ? 1 : 0);
				if (invertRotation)
				{
					num11 ^= -0f;
				}
				_ = 0;
			}
			else
			{
				float num12;
				if (useConsistentDirection && num != 0f)
				{
					num12 = num;
				}
				else
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v206 @ rbx_v6+14]");
					nint num13 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v206 @ rbx_v6+10]");
					object obj2 = num13 - 0;
					object obj3 = obj2 + 10;
					Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"imul ecx\"");
					object obj4 = num14 >> 2;
					object obj5 = obj4 >> 31;
					num14 = (nint)(obj4 + obj5);
					object obj6 = num14 * 4;
					object obj7 = num14 + obj6;
					object obj8 = obj7 + obj7;
					object obj9 = obj3 - obj8;
					bool flag6 = (nint)obj9 <= 5;
					num12 = num8;
					if (!flag6)
					{
						num12 = num9;
					}
				}
				float num15 = maxRevolutionSpeed / 36f;
				float deltaTime = Time.deltaTime;
				float num16 = num15 * deltaTime;
				float num17 = num16;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v206 @ rbx_v6+1C]");
				float num18 = num17 + 0f;
				bool flag7 = num18 < 1f;
				odometerDisplay = null;
				if (!flag7)
				{
					_ = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v206 @ rbx_v6+10]");
					object obj10 = (nint)0 + (nint)10;
					float num19 = (float)obj10 + num12;
					Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"imul ecx\"");
					object obj11 = num14 >> 2;
					object obj12 = obj11 >> 31;
					num14 = (nint)(obj11 + obj12);
					object obj13 = num14 * 4;
					object obj14 = num14 + obj13;
					object obj15 = obj14 + obj14;
					odometerDisplay = (OdometerDisplay)(num19 - obj15);
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v206 @ rbx_v6+10]");
				float num20 = 0f * 36f;
				flag5 = (invertRotation ? 1 : 0) < (false ? 1 : 0);
				if (invertRotation)
				{
					num20 ^= -0f;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v206 @ rbx_v6+1C]");
				float num21 = 0f * 36f;
				float num22 = num21 * num12;
				float num11 = num22 + num20;
				num8 = 1f;
				num9 = 4.2949673E+09f;
			}
			object obj16 = 24;
			num5--;
			num7 -= 8f;
			obj17 = !flag5;
		}
		while (obj17 != null);
	}

	private float GetDigitBaseAngle(int digit)
	{
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Expected F4, but got Unknown
		bool flag = !invertRotation;
		float num = (float)digit * 36f;
		if (!flag)
		{
			float num2 = num;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C80]");
			num = num2 ^ 0;
		}
		return num;
	}

	private unsafe void ApplyDrumRotation(Transform drum, float angle)
	{
		//IL_0045: Expected O, but got Ref
		if (drum != null)
		{
			Vector3 axis = default(Vector3);
			Quaternion quaternion = Quaternion.Internal_AngleAxis(angle, ref axis);
			drum.localRotation = (Quaternion)(&axis);
		}
	}

	private unsafe void SetAllDrumsInstant()
	{
		//IL_0049: Expected O, but got I4
		//IL_0052: Expected O, but got I4
		//IL_0097: Expected O, but got I
		//IL_0105: Expected O, but got I
		//IL_0121: Expected O, but got I
		//IL_0149: Expected F4, but got I
		//IL_0168: Expected O, but got Ref
		//IL_0168: Expected O, but got I
		//IL_0181: Unknown result type (might be due to invalid IL or missing references)
		//IL_0186: Expected O, but got Unknown
		//IL_018f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0194: Expected O, but got Unknown
		if (drumCount <= 0)
		{
			return;
		}
		object obj = 32;
		object obj2 = 0;
		Vector3 axis = default(Vector3);
		float num2 = default(float);
		do
		{
			Transform[] array = drums;
			if ((nint)obj2 < array.Length)
			{
				DrumState[] array2 = drumStates;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v116 @ rsi_v3+v313 @ rax_v10 (DrumState[])]");
				object obj3 = 0;
				bool flag = !invertRotation;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v235 @ rdx_v6+10]");
				float num = 0f * 36f;
				if (!flag)
				{
					num ^= -0f;
				}
				Transform[] array3 = drums;
				DrumState[] array4 = drumStates;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v116 @ rsi_v3+v314 @ rax_v12 (DrumState[])]");
				object obj4 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v116 @ rsi_v3+v316 @ rax_v11 (UnityEngine.Transform[])]");
				if ((UnityEngine.Object)0 != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v228 @ rcx_v6+18]");
					Quaternion quaternion = Quaternion.Internal_AngleAxis(0f, ref axis);
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v116 @ rsi_v3+v316 @ rax_v11 (UnityEngine.Transform[])]");
					((Transform)0).localRotation = (Quaternion)(&num2);
					axis = rotationAxis;
				}
				obj2++;
				obj += 8;
				continue;
			}
			break;
		}
		while ((nint)obj2 < drumCount);
	}

	private float MaxOdometerValue()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033FCB0");
		int num = -decimalDigits;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033FCB0");
		return 10f - 10f;
	}

	public OdometerDisplay()
	{
		//IL_005c: Expected I, but got O
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3A087]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		maxRevolutionSpeed = 360f;
		countSpeed = 100f;
		nint num = (nint)typeof(Vector3);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v42 @ rax_v3 (Il2CppClass<UnityEngine.Vector3>)+B8]");
		nint num2 = 0;
		rotationAxis = Vector3.upVector;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v43 @ rcx_v3 (Il2CppStaticFields<UnityEngine.Vector3>)+20]");
		_ = 0;
		providerPropertyName = "CurrentAngle";
		integerDigits = 3;
		decimalDigits = 2;
		showDecimalPoint = true;
		smoothingTimeConstant = 0.25f;
		base._002Ector();
	}
}
