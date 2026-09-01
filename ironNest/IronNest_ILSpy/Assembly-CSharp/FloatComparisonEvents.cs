using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.Events;

public class FloatComparisonEvents : MonoBehaviour
{
	private enum ComparisonState
	{
		Equal,
		FirstGreater,
		SecondGreater
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

	public MonoBehaviour firstValueProvider;

	public string firstProviderPropertyName;

	public float firstFallbackValue;

	public MonoBehaviour secondValueProvider;

	public string secondProviderPropertyName;

	public float secondFallbackValue;

	public float equalityEpsilon;

	public bool compareAbsoluteValues;

	public bool invokeOnStart;

	public bool logWarnings;

	public UnityEvent onEqual;

	public UnityEvent onNotEqual;

	public UnityEvent onFirstGreater;

	public UnityEvent onSecondGreater;

	private IFloatValueProvider firstResolved;

	private IFloatValueProvider secondResolved;

	private PropertyInfo firstPropInfo;

	private PropertyInfo secondPropInfo;

	private bool hasState;

	private ComparisonState currentState;

	private float lastA;

	private float lastB;

	private void Awake()
	{
		ResolveProviders();
	}

	private void Start()
	{
		//IL_024b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0250: Expected O, but got Unknown
		//IL_025b: Invalid comparison between I4 and F4
		//IL_026d: Expected F4, but got I4
		//IL_01db: Invalid comparison between F4 and O
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Expected F4, but got Unknown
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Expected F4, but got Unknown
		//IL_00b5: Invalid comparison between F4 and I4
		//IL_00c8: Expected O, but got I4
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Expected I4, but got Unknown
		//IL_0106: Expected O, but got I4
		float num;
		float num2 = default(float);
		if (firstResolved == null)
		{
			num = firstFallbackValue;
		}
		else
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180005E40");
			num = num2;
		}
		float num3;
		if (secondResolved == null)
		{
			num3 = secondFallbackValue;
		}
		else
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180005E40");
			num3 = num2;
		}
		if (compareAbsoluteValues)
		{
			float num4 = num;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
			num = num4 & 0;
			float num5 = num3;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
			num3 = num5 & 0;
		}
		lastA = num;
		lastB = num3;
		float num6 = num - num3;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
		object obj = num6 & 0;
		bool flag = !(0f < equalityEpsilon);
		float num7 = 0f;
		if (!flag)
		{
			num7 = equalityEpsilon;
		}
		bool flag2 = System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)num7) >= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj);
		ComparisonState comparisonState = ComparisonState.Equal;
		if (!flag2)
		{
			bool flag3 = num < num3;
			float num8 = num - num3;
			bool flag4 = num8 == 0f;
			object obj2 = flag3 | flag4;
			comparisonState = (ComparisonState)(obj2 + 1);
		}
		bool flag5 = !invokeOnStart;
		hasState = true;
		currentState = comparisonState;
		if (flag5)
		{
			return;
		}
		bool flag6 = comparisonState == ComparisonState.Equal;
		UnityEvent unityEvent;
		if (!flag6)
		{
			object obj3 = comparisonState - 1;
			if (!flag6)
			{
				if ((nint)obj3 != 1)
				{
					return;
				}
				unityEvent = onSecondGreater;
			}
			else
			{
				unityEvent = onFirstGreater;
			}
		}
		else
		{
			unityEvent = onEqual;
		}
		unityEvent?.Invoke();
	}

	private void Update()
	{
		//IL_02d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d6: Expected O, but got Unknown
		//IL_02e1: Invalid comparison between I4 and F4
		//IL_02f3: Expected F4, but got I4
		//IL_0228: Invalid comparison between F4 and O
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Expected F4, but got Unknown
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Expected F4, but got Unknown
		//IL_0273: Expected I4, but got O
		//IL_00b5: Invalid comparison between F4 and I4
		//IL_00c8: Expected O, but got I4
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Expected O, but got Unknown
		//IL_0103: Expected I4, but got O
		//IL_0145: Unknown result type (might be due to invalid IL or missing references)
		//IL_014a: Expected O, but got Unknown
		float num;
		float num2 = default(float);
		if (firstResolved == null)
		{
			num = firstFallbackValue;
		}
		else
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180005E40");
			num = num2;
		}
		float num3;
		if (secondResolved == null)
		{
			num3 = secondFallbackValue;
		}
		else
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180005E40");
			num3 = num2;
		}
		if (compareAbsoluteValues)
		{
			float num4 = num;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
			num = num4 & 0;
			float num5 = num3;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
			num3 = num5 & 0;
		}
		lastA = num;
		lastB = num3;
		float num6 = num - num3;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
		object obj = num6 & 0;
		bool flag = !(0f < equalityEpsilon);
		float num7 = 0f;
		if (!flag)
		{
			num7 = equalityEpsilon;
		}
		bool flag2 = System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)num7) >= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj);
		FloatComparisonEvents floatComparisonEvents = null;
		if (!flag2)
		{
			bool flag3 = num < num3;
			float num8 = num - num3;
			bool flag4 = num8 == 0f;
			object obj2 = flag3 | flag4;
			floatComparisonEvents = (FloatComparisonEvents)(obj2 + 1);
		}
		bool flag5 = !hasState;
		FloatComparisonEvents floatComparisonEvents2 = this;
		if (!flag5)
		{
			if ((nint)floatComparisonEvents == (nint)currentState)
			{
				return;
			}
			currentState = (ComparisonState)floatComparisonEvents;
			Cpp2ILHelpers.NoteDecompilerIssue("Invalid instruction: 158 Invalid \"Jump target not found in method: 0x1803EADA0\"");
			floatComparisonEvents = this;
			FloatComparisonEvents floatComparisonEvents3 = default(FloatComparisonEvents);
			floatComparisonEvents2 = floatComparisonEvents3;
		}
		bool flag6 = !floatComparisonEvents2.invokeOnStart;
		floatComparisonEvents2.hasState = true;
		floatComparisonEvents2.currentState = (ComparisonState)floatComparisonEvents;
		if (flag6)
		{
			return;
		}
		bool flag7 = (object)floatComparisonEvents == null;
		UnityEvent unityEvent;
		if (!flag7)
		{
			object obj3 = floatComparisonEvents - 1;
			if (!flag7)
			{
				if ((nint)obj3 != 1)
				{
					return;
				}
				unityEvent = floatComparisonEvents2.onSecondGreater;
			}
			else
			{
				unityEvent = floatComparisonEvents2.onFirstGreater;
			}
		}
		else
		{
			unityEvent = floatComparisonEvents2.onEqual;
		}
		unityEvent?.Invoke();
	}

	public void RefreshBindings()
	{
		//IL_02cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d2: Expected O, but got Unknown
		//IL_02dd: Invalid comparison between I4 and F4
		//IL_02ef: Expected F4, but got I4
		//IL_025d: Invalid comparison between F4 and O
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Expected F4, but got Unknown
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Expected F4, but got Unknown
		//IL_00bb: Invalid comparison between F4 and I4
		//IL_00ce: Expected O, but got I4
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Expected I4, but got Unknown
		//IL_0188: Expected O, but got I4
		ResolveProviders();
		float num;
		float num2 = default(float);
		if (firstResolved == null)
		{
			num = firstFallbackValue;
		}
		else
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180005E40");
			num = num2;
		}
		float num3;
		if (secondResolved == null)
		{
			num3 = secondFallbackValue;
		}
		else
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180005E40");
			num3 = num2;
		}
		if (compareAbsoluteValues)
		{
			float num4 = num;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
			num = num4 & 0;
			float num5 = num3;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
			num3 = num5 & 0;
		}
		lastA = num;
		lastB = num3;
		float num6 = num - num3;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
		object obj = num6 & 0;
		bool flag = !(0f < equalityEpsilon);
		float num7 = 0f;
		if (!flag)
		{
			num7 = equalityEpsilon;
		}
		bool flag2 = System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)num7) >= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj);
		ComparisonState comparisonState = ComparisonState.Equal;
		if (!flag2)
		{
			bool flag3 = num < num3;
			float num8 = num - num3;
			bool flag4 = num8 == 0f;
			object obj2 = flag3 | flag4;
			comparisonState = (ComparisonState)(obj2 + 1);
		}
		if (hasState)
		{
			if (comparisonState != currentState)
			{
				bool fromWasEqual = currentState == ComparisonState.Equal;
				currentState = comparisonState;
				InvokeForStateEntry(comparisonState, fromWasEqual);
			}
			return;
		}
		bool flag5 = !invokeOnStart;
		hasState = true;
		currentState = comparisonState;
		if (flag5)
		{
			return;
		}
		bool flag6 = comparisonState == ComparisonState.Equal;
		UnityEvent unityEvent;
		if (!flag6)
		{
			object obj3 = comparisonState - 1;
			if (!flag6)
			{
				if ((nint)obj3 != 1)
				{
					return;
				}
				unityEvent = onSecondGreater;
			}
			else
			{
				unityEvent = onFirstGreater;
			}
		}
		else
		{
			unityEvent = onEqual;
		}
		unityEvent?.Invoke();
	}

	public void ForceEvaluate()
	{
		//IL_02d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d6: Expected O, but got Unknown
		//IL_02e1: Invalid comparison between I4 and F4
		//IL_02f3: Expected F4, but got I4
		//IL_0228: Invalid comparison between F4 and O
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Expected F4, but got Unknown
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Expected F4, but got Unknown
		//IL_0273: Expected I4, but got O
		//IL_00b5: Invalid comparison between F4 and I4
		//IL_00c8: Expected O, but got I4
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Expected O, but got Unknown
		//IL_0103: Expected I4, but got O
		//IL_0145: Unknown result type (might be due to invalid IL or missing references)
		//IL_014a: Expected O, but got Unknown
		float num;
		float num2 = default(float);
		if (firstResolved == null)
		{
			num = firstFallbackValue;
		}
		else
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180005E40");
			num = num2;
		}
		float num3;
		if (secondResolved == null)
		{
			num3 = secondFallbackValue;
		}
		else
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180005E40");
			num3 = num2;
		}
		if (compareAbsoluteValues)
		{
			float num4 = num;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
			num = num4 & 0;
			float num5 = num3;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
			num3 = num5 & 0;
		}
		lastA = num;
		lastB = num3;
		float num6 = num - num3;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
		object obj = num6 & 0;
		bool flag = !(0f < equalityEpsilon);
		float num7 = 0f;
		if (!flag)
		{
			num7 = equalityEpsilon;
		}
		bool flag2 = System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)num7) >= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj);
		FloatComparisonEvents floatComparisonEvents = null;
		if (!flag2)
		{
			bool flag3 = num < num3;
			float num8 = num - num3;
			bool flag4 = num8 == 0f;
			object obj2 = flag3 | flag4;
			floatComparisonEvents = (FloatComparisonEvents)(obj2 + 1);
		}
		bool flag5 = !hasState;
		FloatComparisonEvents floatComparisonEvents2 = this;
		if (!flag5)
		{
			if ((nint)floatComparisonEvents == (nint)currentState)
			{
				return;
			}
			currentState = (ComparisonState)floatComparisonEvents;
			Cpp2ILHelpers.NoteDecompilerIssue("Invalid instruction: 158 Invalid \"Jump target not found in method: 0x1803EADA0\"");
			floatComparisonEvents = this;
			FloatComparisonEvents floatComparisonEvents3 = default(FloatComparisonEvents);
			floatComparisonEvents2 = floatComparisonEvents3;
		}
		bool flag6 = !floatComparisonEvents2.invokeOnStart;
		floatComparisonEvents2.hasState = true;
		floatComparisonEvents2.currentState = (ComparisonState)floatComparisonEvents;
		if (flag6)
		{
			return;
		}
		bool flag7 = (object)floatComparisonEvents == null;
		UnityEvent unityEvent;
		if (!flag7)
		{
			object obj3 = floatComparisonEvents - 1;
			if (!flag7)
			{
				if ((nint)obj3 != 1)
				{
					return;
				}
				unityEvent = floatComparisonEvents2.onSecondGreater;
			}
			else
			{
				unityEvent = floatComparisonEvents2.onFirstGreater;
			}
		}
		else
		{
			unityEvent = floatComparisonEvents2.onEqual;
		}
		unityEvent?.Invoke();
	}

	public unsafe void GetLastValues(out float a, out float b)
	{
		//IL_000a: Expected Ref, but got F4
		//IL_0014: Expected Ref, but got F4
		ref float reference = ref *(float*)lastA;
		ref float reference2 = ref *(float*)lastB;
	}

	private void ResolveProviders()
	{
		//IL_00aa: Expected O, but got I4
		//IL_00db: Expected O, but got I4
		//IL_0361: Expected O, but got I4
		//IL_0392: Expected O, but got I4
		//IL_0113: Expected O, but got I
		//IL_014a: Expected O, but got I4
		//IL_03ca: Expected O, but got I
		//IL_0401: Expected O, but got I4
		firstResolved = null;
		secondResolved = null;
		firstPropInfo = null;
		secondPropInfo = null;
		if (firstValueProvider != null && !string.IsNullOrEmpty(firstProviderPropertyName))
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_vm_object_is_inst\"");
			Type type = default(Type);
			PropertyInfo property = type.GetProperty(firstProviderPropertyName);
			firstPropInfo = property;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180D16FE0");
			object obj = default(object);
			bool flag = obj == null;
			object obj2 = 0;
			if (!flag)
			{
				bool canRead = firstPropInfo.CanRead;
				bool flag2 = !canRead;
				obj2 = 0;
				if (!flag2)
				{
					Type propertyType = firstPropInfo.PropertyType;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B502B8]");
					RuntimeTypeHandle handle = (RuntimeTypeHandle)((nint)0 + (nint)32);
					Type typeFromHandle = Type.GetTypeFromHandle(handle);
					bool flag3 = ((object)propertyType).Equals((object)typeFromHandle);
					bool flag4 = !flag3;
					obj2 = 0;
					if (!flag4)
					{
						ReflectionFloatValueProvider reflectionFloatValueProvider = new ReflectionFloatValueProvider(firstValueProvider, firstPropInfo);
						firstResolved = reflectionFloatValueProvider;
						goto IL_02c8;
					}
				}
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
			IFloatValueProvider floatValueProvider = default(IFloatValueProvider);
			if (floatValueProvider == null)
			{
				if (logWarnings)
				{
					string text = firstValueProvider.name;
					string message = "[FloatComparisonEvents] First provider '" + text + "' does not expose a readable public float property '" + firstProviderPropertyName + "' and does not implement IFloatValueProvider. Using 'First Fallback Value'.";
					Debug.LogWarning(message, firstValueProvider);
				}
			}
			else
			{
				firstResolved = floatValueProvider;
				firstPropInfo = null;
			}
		}
		else
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
			IFloatValueProvider floatValueProvider2 = default(IFloatValueProvider);
			if (floatValueProvider2 != null)
			{
				firstResolved = floatValueProvider2;
			}
		}
		goto IL_02c8;
		IL_02c8:
		if (secondValueProvider != null && !string.IsNullOrEmpty(secondProviderPropertyName))
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_vm_object_is_inst\"");
			Type type2 = default(Type);
			PropertyInfo property2 = type2.GetProperty(secondProviderPropertyName);
			secondPropInfo = property2;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180D16FE0");
			object obj3 = default(object);
			bool flag5 = obj3 == null;
			object obj4 = 0;
			if (!flag5)
			{
				bool canRead2 = secondPropInfo.CanRead;
				bool flag6 = !canRead2;
				obj4 = 0;
				if (!flag6)
				{
					Type propertyType2 = secondPropInfo.PropertyType;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B502B8]");
					RuntimeTypeHandle handle2 = (RuntimeTypeHandle)((nint)0 + (nint)32);
					Type typeFromHandle2 = Type.GetTypeFromHandle(handle2);
					bool flag7 = ((object)propertyType2).Equals((object)typeFromHandle2);
					bool flag8 = !flag7;
					obj4 = 0;
					if (!flag8)
					{
						ReflectionFloatValueProvider reflectionFloatValueProvider2 = new ReflectionFloatValueProvider(secondValueProvider, secondPropInfo);
						secondResolved = reflectionFloatValueProvider2;
						return;
					}
				}
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
			IFloatValueProvider floatValueProvider3 = default(IFloatValueProvider);
			if (floatValueProvider3 == null)
			{
				if (logWarnings)
				{
					string text2 = secondValueProvider.name;
					string message2 = "[FloatComparisonEvents] Second provider '" + text2 + "' does not expose a readable public float property '" + secondProviderPropertyName + "' and does not implement IFloatValueProvider. Using 'Second Fallback Value'.";
					Debug.LogWarning(message2, secondValueProvider);
				}
			}
			else
			{
				secondResolved = floatValueProvider3;
				secondPropInfo = null;
			}
		}
		else
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
			IFloatValueProvider floatValueProvider4 = default(IFloatValueProvider);
			if (floatValueProvider4 != null)
			{
				secondResolved = floatValueProvider4;
			}
		}
	}

	private unsafe void ReadValues(out float a, out float b)
	{
		//IL_002b: Expected Ref, but got F4
		//IL_0073: Expected Ref, but got F4
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Expected O, but got Unknown
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Expected O, but got Unknown
		float num = default(float);
		if (firstResolved == null)
		{
			num = firstFallbackValue;
		}
		else
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180005E40");
		}
		ref float reference = ref *(float*)num;
		if (secondResolved == null)
		{
			num = secondFallbackValue;
		}
		else
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180005E40");
		}
		ref float reference2 = ref *(float*)num;
		if (compareAbsoluteValues)
		{
			float num2 = a;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
			object obj = num2 & 0;
			reference = ref *(float*)obj;
			float num3 = b;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
			object obj2 = num3 & 0;
			reference2 = ref *(float*)obj2;
		}
	}

	private ComparisonState Classify(float a, float b)
	{
		//IL_000b: Invalid comparison between I4 and F4
		//IL_001d: Expected F4, but got I4
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Expected O, but got Unknown
		//IL_00b2: Invalid comparison between F4 and O
		//IL_0060: Invalid comparison between F4 and I4
		//IL_0073: Expected O, but got I4
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Expected I4, but got Unknown
		bool flag = !(0f < equalityEpsilon);
		float num = 0f;
		if (!flag)
		{
			num = equalityEpsilon;
		}
		float num2 = a - b;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
		object obj = num2 & 0;
		bool flag2 = System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)num) >= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj);
		ComparisonState result = ComparisonState.Equal;
		if (!flag2)
		{
			bool flag3 = a < b;
			float num3 = a - b;
			bool flag4 = num3 == 0f;
			object obj2 = flag3 | flag4;
			result = (ComparisonState)(obj2 + 1);
		}
		return result;
	}

	private void InvokeForStateEntry(ComparisonState state, bool fromWasEqual)
	{
		//IL_002b: Expected O, but got I4
		bool flag = state == ComparisonState.Equal;
		UnityEvent unityEvent;
		if (!flag)
		{
			object obj = state - 1;
			if (!flag)
			{
				if ((nint)obj != 1)
				{
					return;
				}
				if (fromWasEqual && onNotEqual != null)
				{
					onNotEqual.Invoke();
				}
				unityEvent = onSecondGreater;
			}
			else
			{
				if (fromWasEqual && onNotEqual != null)
				{
					onNotEqual.Invoke();
				}
				unityEvent = onFirstGreater;
			}
		}
		else
		{
			unityEvent = onEqual;
		}
		unityEvent?.Invoke();
	}

	public FloatComparisonEvents()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B39FC6]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		firstProviderPropertyName = "CurrentValue";
		secondProviderPropertyName = "CurrentValue";
		equalityEpsilon = 0.001f;
		invokeOnStart = true;
		base._002Ector();
	}
}
