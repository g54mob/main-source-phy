using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using FMOD;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;

public class FMODParameterSetter : MonoBehaviour
{
	public enum ParameterTarget
	{
		Global,
		Local,
		Both
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

	private ParameterTarget target;

	private string parameterName = "Rotation_Motor_RPM";

	private MonoBehaviour floatValueProvider;

	private string providerPropertyName = "";

	private float testSliderValue;

	private float inputRangeMin;

	private float inputRangeMax = 4f;

	private float fmodParamMin;

	private float fmodParamMax = 1000f;

	private bool convertNegativeToPositive = true;

	private bool clampInputToMappingRange;

	private bool smoothOutputMappedValue;

	private float outputSmoothing = 0.15f;

	private bool outputSmoothingUseUnscaledTime;

	private bool retryGlobalUntilReady = true;

	private float globalRetryIntervalSeconds = 0.25f;

	private StudioEventEmitter[] targetEmitters;

	private bool ignoreSeekSpeedWhenSettingLocal;

	private bool verboseLogging;

	private float inspectorRawInput;

	private float inspectorAbsInput;

	private float inspectorMappedValue;

	private float inspectorSmoothedMappedValue;

	private bool inspectorGlobalParamReady;

	private RESULT inspectorLastGlobalResult;

	private int inspectorLocalAttemptedCount;

	private int inspectorLocalSucceededCount;

	private IFloatValueProvider provider;

	private ReflectionFloatValueProvider reflectionProvider;

	private bool globalParamEverSucceeded;

	private float nextGlobalRetryAt;

	private bool hasSmoothedValue;

	private float smoothedMappedValue;

	private void Awake()
	{
		BindProvider();
		ApplyOnce();
	}

	private void Update()
	{
		ApplyOnce();
	}

	private void BindProvider()
	{
		//IL_00e1: Expected O, but got I
		provider = null;
		reflectionProvider = null;
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
				if (((object)propertyType).Equals((object)typeFromHandle) && property.CanRead)
				{
					ReflectionFloatValueProvider reflectionFloatValueProvider = new ReflectionFloatValueProvider(null, null);
					reflectionFloatValueProvider.target = this.floatValueProvider;
					reflectionFloatValueProvider.prop = property;
					reflectionProvider = reflectionFloatValueProvider;
					return;
				}
			}
			if (verboseLogging)
			{
				string text = type.Name;
				string message = "[FMODParameterSetter] Reflection bind failed. Property '" + providerPropertyName + "' not found or not a readable float on " + text + ".";
				UnityEngine.Debug.LogWarning(message);
			}
		}
		else
		{
			provider = floatValueProvider;
		}
	}

	private void ApplyOnce()
	{
		//IL_0182: Expected I, but got O
		//IL_0053: Expected I, but got O
		//IL_00bc: Expected O, but got I
		//IL_0197: Unknown result type (might be due to invalid IL or missing references)
		//IL_019c: Expected F4, but got Unknown
		//IL_00e1: Expected I, but got O
		//IL_0283: Invalid comparison between I4 and F4
		//IL_0292: Expected O, but got I4
		//IL_05e8: Expected F4, but got I4
		//IL_0214: Expected O, but got I4
		//IL_012a: Expected F4, but got O
		//IL_05f5: Invalid comparison between O and F4
		//IL_02bb: Expected O, but got I4
		//IL_02e9: Expected F4, but got I4
		//IL_02d2: Expected O, but got I4
		//IL_02f8: Invalid comparison between O and F4
		//IL_0379: Invalid comparison between F4 and I
		//IL_061d: Invalid comparison between O and F4
		//IL_03e4: Expected F4, but got I4
		//IL_03a8: Expected F4, but got I
		//IL_0673: Invalid comparison between O and F4
		//IL_0420: Expected F4, but got I4
		if (string.IsNullOrEmpty(parameterName))
		{
			return;
		}
		float num3;
		if (provider == null)
		{
			nint num;
			if (reflectionProvider != null)
			{
				ReflectionFloatValueProvider reflectionFloatValueProvider = reflectionProvider;
				PropertyInfo prop = reflectionFloatValueProvider.prop;
				if ((object)reflectionFloatValueProvider.prop != null)
				{
					object value = reflectionFloatValueProvider.prop.GetValue(reflectionFloatValueProvider.target);
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B502B8]");
					IFloatValueProvider floatValueProvider = (IFloatValueProvider)0;
					if (value != null)
					{
						num = (nint)value;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v95 @ rdx_v5 (Il2CppClass<IFloatValueProvider>)+40]");
						nint num2 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v125 @ r8_v2 (IFloatValueProvider)+40]");
						bool flag = num2 != 0;
						prop = (PropertyInfo)value;
						if (!flag)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_object_unbox\"");
							object obj = default(object);
							num3 = (float)obj;
							goto IL_04f9;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
						return;
					}
				}
				goto IL_04ca;
			}
			num3 = testSliderValue;
			num = unchecked((nint)null);
		}
		else
		{
			IFloatValueProvider floatValueProvider = provider;
			bool flag2 = provider == null;
			PropertyInfo prop = (PropertyInfo)(object)parameterName;
			if (flag2)
			{
				goto IL_04ca;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180005E40");
			float num4 = default(float);
			num3 = num4;
			nint num = (nint)typeof(IFloatValueProvider);
		}
		goto IL_04f9;
		IL_04ca:
		throw new NullReferenceException();
		IL_04f9:
		bool flag3 = !convertNegativeToPositive;
		inspectorRawInput = num3;
		float num6;
		if (!flag3)
		{
			float num5 = num3;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
			num6 = num5 & 0;
		}
		else
		{
			num6 = num3;
		}
		bool flag4 = !clampInputToMappingRange;
		inspectorAbsInput = num6;
		if (!flag4)
		{
			if (!(inputRangeMin > num6))
			{
				if (num6 > inputRangeMax)
				{
					num6 = inputRangeMax;
				}
			}
			else
			{
				num6 = inputRangeMin;
			}
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"jp short 000000018054950Ah\"");
		object obj2;
		float num9;
		if (inputRangeMin == inputRangeMax)
		{
			obj2 = 0;
		}
		else
		{
			float num7 = num6 - inputRangeMin;
			float num8 = inputRangeMax - inputRangeMin;
			num9 = num7 / num8;
			bool flag5 = 0f > num9;
			obj2 = 0;
			if (!flag5)
			{
				bool flag6 = !(num9 > 1f);
				obj2 = 0;
				if (!flag6)
				{
					obj2 = 0;
					num9 = 1f;
				}
				goto IL_05ed;
			}
		}
		num9 = 0f;
		goto IL_05ed;
		IL_05ed:
		if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj2) <= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)num9))
		{
			if (num9 > 1f)
			{
				num9 = 1f;
			}
		}
		else
		{
			num9 = 0f;
		}
		bool flag7 = !smoothOutputMappedValue;
		float num10 = fmodParamMax - fmodParamMin;
		float num11 = num10 * num9;
		float num12 = (inspectorMappedValue = num11 + fmodParamMin);
		if (!flag7 && System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj2) < System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)outputSmoothing))
		{
			if (hasSmoothedValue)
			{
				float num13 = ((!outputSmoothingUseUnscaledTime) ? Time.deltaTime : Time.unscaledDeltaTime);
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206CEC]");
				bool flag8 = !(num13 < 0f);
				float num14 = num13;
				if (!flag8)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206CEC]");
					num14 = 0f;
				}
				float num15 = outputSmoothing;
				if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj2) <= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)outputSmoothing))
				{
					if (num15 > 1f)
					{
						num15 = 1f;
					}
				}
				else
				{
					num15 = 0f;
				}
				float num16 = num14 * 60f;
				float num17 = 1f - num15;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033FCB0");
				float num18 = 1f - num17;
				if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj2) <= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)num18))
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
				float num19 = num12 - smoothedMappedValue;
				float num20 = num19 * num18;
				num12 = num20 + smoothedMappedValue;
			}
			else
			{
				hasSmoothedValue = true;
			}
		}
		else
		{
			hasSmoothedValue = false;
		}
		smoothedMappedValue = num12;
		bool flag9 = target == ParameterTarget.Global;
		inspectorSmoothedMappedValue = num12;
		if (flag9 || target == ParameterTarget.Both)
		{
			ApplyGlobal(num12, num3);
		}
		if (target == ParameterTarget.Local || target == ParameterTarget.Both)
		{
			ApplyLocal(num12);
		}
	}

	private float SmoothMappedOutput(float mapped)
	{
		//IL_002a: Invalid comparison between I4 and F4
		//IL_00a8: Expected O, but got I4
		//IL_00bd: Invalid comparison between F4 and I
		//IL_0091: Expected O, but got I4
		//IL_019e: Invalid comparison between I4 and F4
		//IL_0128: Expected F4, but got I4
		//IL_00ec: Expected F4, but got I
		//IL_01f5: Invalid comparison between I4 and F4
		//IL_0164: Expected F4, but got I4
		float result;
		if (smoothOutputMappedValue && 0f < outputSmoothing)
		{
			if (hasSmoothedValue)
			{
				float num;
				if (outputSmoothingUseUnscaledTime)
				{
					num = Time.unscaledDeltaTime;
					object obj = 0;
				}
				else
				{
					num = Time.deltaTime;
					object obj = 0;
				}
				float num2 = num;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206CEC]");
				bool flag = !(num2 < 0f);
				float num3 = num;
				if (!flag)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206CEC]");
					num3 = 0f;
				}
				float num4 = outputSmoothing;
				if (!(0f > outputSmoothing))
				{
					if (num4 > 1f)
					{
						num4 = 1f;
					}
				}
				else
				{
					num4 = 0f;
				}
				float num5 = num3 * 60f;
				float num6 = 1f - num4;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033FCB0");
				float num7 = 1f - num6;
				if (!(0f > num7))
				{
					if (num7 > 1f)
					{
						num7 = 1f;
					}
				}
				else
				{
					num7 = 0f;
				}
				float num8 = mapped - smoothedMappedValue;
				float num9 = num8 * num7;
				result = (smoothedMappedValue = num9 + smoothedMappedValue);
				goto IL_0260;
			}
			hasSmoothedValue = true;
		}
		else
		{
			hasSmoothedValue = false;
		}
		smoothedMappedValue = mapped;
		result = mapped;
		goto IL_0260;
		IL_0260:
		return result;
	}

	private void ApplyGlobal(float valueMapped, float originalSource)
	{
		//IL_00f7: Expected I4, but got O
		//IL_01a7: Expected I4, but got O
		if (globalParamEverSucceeded)
		{
			float unscaledTime = Time.unscaledTime;
			if (unscaledTime < nextGlobalRetryAt)
			{
				return;
			}
		}
		FMOD.Studio.System studioSystem = RuntimeManager.StudioSystem;
		FMOD.Studio.System system = default(FMOD.Studio.System);
		if ((inspectorLastGlobalResult = system.setParameterByName(parameterName, valueMapped)) != RESULT.OK)
		{
			inspectorGlobalParamReady = false;
			object obj = default(object);
			if (!retryGlobalUntilReady)
			{
				if (verboseLogging)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
					object arg = (RESULT)obj;
					object arg2 = default(object);
					string message = $"[FMODParameterSetter] Global set('{parameterName}', {arg2}) => {arg}.";
					UnityEngine.Debug.LogWarning(message);
				}
				return;
			}
			float unscaledTime2 = Time.unscaledTime;
			float num = unscaledTime2 + globalRetryIntervalSeconds;
			nextGlobalRetryAt = num;
			if (verboseLogging && !globalParamEverSucceeded)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
				object arg3 = (RESULT)obj;
				object arg4 = default(object);
				string message2 = $"[FMODParameterSetter] Global set('{parameterName}', {arg4}) => {arg3}. Retrying…";
				UnityEngine.Debug.Log(message2);
			}
		}
		else
		{
			if (!globalParamEverSucceeded && verboseLogging)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
				Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
				object arg5 = default(object);
				object arg6 = default(object);
				string message3 = $"[FMODParameterSetter] Global parameter '{parameterName}' responding. Value={arg5} (from {arg6}).";
				UnityEngine.Debug.Log(message3);
			}
			globalParamEverSucceeded = true;
			inspectorGlobalParamReady = true;
		}
	}

	private void ApplyLocal(float valueMapped)
	{
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Expected O, but got Unknown
		//IL_0031: Expected O, but got I4
		//IL_0043: Expected O, but got I4
		//IL_004c: Expected O, but got I4
		//IL_0287: Unknown result type (might be due to invalid IL or missing references)
		//IL_028c: Expected O, but got Unknown
		//IL_0295: Unknown result type (might be due to invalid IL or missing references)
		//IL_029a: Expected O, but got Unknown
		//IL_0233: Expected O, but got I
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Expected O, but got Unknown
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Expected O, but got Unknown
		//IL_00f6: Expected O, but got I
		//IL_015f: Expected O, but got I
		//IL_01d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d6: Expected O, but got Unknown
		//IL_01df: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e4: Expected O, but got Unknown
		//IL_01f7: Expected I4, but got O
		//IL_0207: Expected O, but got I
		bool flag = targetEmitters == null;
		int num = 0;
		int num2 = 0;
		if (!flag)
		{
			StudioEventEmitter[] array = targetEmitters;
			object obj = targetEmitters + 32;
			num = 0;
			EventInstance eventInstance = (EventInstance)0;
			num2 = 0;
			object obj2 = 0;
			object obj3 = 0;
			RESULT rESULT2 = default(RESULT);
			while ((nint)obj3 < array.Length)
			{
				UnityEngine.Object obj4 = (UnityEngine.Object)obj;
				if ((UnityEngine.Object)obj != null)
				{
					num2++;
					bool flag2 = eventInstance.isValid();
					bool ignoreseekspeed = ignoreSeekSpeedWhenSettingLocal;
					if (!flag2)
					{
						((StudioEventEmitter)obj).SetParameter(parameterName, valueMapped, ignoreseekspeed);
						obj2++;
						obj += 8;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v152 @ rbx_v5 (UnityEngine.Object)+70]");
						eventInstance = (EventInstance)0;
						obj3 = obj2;
						continue;
					}
					RESULT rESULT = eventInstance.setParameterByName(parameterName, valueMapped, ignoreSeekSpeedWhenSettingLocal);
					if (rESULT != RESULT.OK)
					{
						bool flag3 = !verboseLogging;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v152 @ rbx_v5 (UnityEngine.Object)+70]");
						eventInstance = (EventInstance)0;
						if (!flag3)
						{
							GameObject gameObject = ((Component)obj).gameObject;
							string arg = gameObject.name;
							object arg2 = rESULT2;
							string message = $"[FMODParameterSetter] Local set by name on '{arg}' failed: {arg2} (param '{parameterName}').";
							UnityEngine.Debug.LogWarning(message);
							obj2++;
							obj += 8;
							rESULT2 = rESULT;
							ignoreseekspeed = (byte)(int)parameterName != 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v152 @ rbx_v5 (UnityEngine.Object)+70]");
							eventInstance = (EventInstance)0;
							obj3 = obj2;
							continue;
						}
					}
					else
					{
						num++;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v152 @ rbx_v5 (UnityEngine.Object)+70]");
						eventInstance = (EventInstance)0;
					}
				}
				obj2++;
				obj += 8;
				obj3 = obj2;
			}
		}
		inspectorLocalAttemptedCount = num2;
		inspectorLocalSucceededCount = num;
	}

	private float GetSourceValueOrSlider()
	{
		//IL_0092: Expected O, but got I
		//IL_00b7: Expected I, but got O
		//IL_00fd: Expected F4, but got O
		float result = default(float);
		if (provider == null)
		{
			if (reflectionProvider == null)
			{
				return testSliderValue;
			}
			ReflectionFloatValueProvider reflectionFloatValueProvider = reflectionProvider;
			PropertyInfo prop = reflectionFloatValueProvider.prop;
			if ((object)reflectionFloatValueProvider.prop != null)
			{
				object value = reflectionFloatValueProvider.prop.GetValue(reflectionFloatValueProvider.target);
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B502B8]");
				IFloatValueProvider floatValueProvider = (IFloatValueProvider)0;
				if (value != null)
				{
					nint num = (nint)value;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v110 @ rdx_v7 (Il2CppClass<System.Object>)+40]");
					nint num2 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v108 @ r8_v1 (IFloatValueProvider)+40]");
					bool flag = num2 != 0;
					prop = (PropertyInfo)value;
					if (!flag)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_object_unbox\"");
						object obj = default(object);
						return (float)obj;
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
					return result;
				}
			}
		}
		else
		{
			IFloatValueProvider floatValueProvider = provider;
			bool flag2 = provider == null;
			PropertyInfo prop = (PropertyInfo)(object)this;
			if (!flag2)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180005E40");
				return result;
			}
		}
		throw new NullReferenceException();
	}

	private float MapToFMODRange(float valueInInputRange)
	{
		//IL_00d0: Invalid comparison between I4 and F4
		//IL_00df: Expected O, but got I4
		//IL_017c: Expected F4, but got I4
		//IL_0036: Expected O, but got I4
		//IL_0189: Invalid comparison between O and F4
		//IL_0108: Expected O, but got I4
		//IL_0136: Expected F4, but got I4
		//IL_0128: Expected O, but got I4
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"jp short 0000000180549AF3h\"");
		object obj;
		float num3;
		if (inputRangeMin == inputRangeMax)
		{
			obj = 0;
		}
		else
		{
			float num = valueInInputRange - inputRangeMin;
			float num2 = inputRangeMax - inputRangeMin;
			num3 = num / num2;
			bool flag = 0f > num3;
			obj = 0;
			if (!flag)
			{
				bool flag2 = !(num3 > 1f);
				obj = 0;
				if (!flag2)
				{
					num3 = 1f;
					obj = 0;
				}
				goto IL_0181;
			}
		}
		num3 = 0f;
		goto IL_0181;
		IL_0181:
		if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj) <= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)num3))
		{
			if (num3 > 1f)
			{
				float num4 = fmodParamMax - fmodParamMin;
				float num5 = num4 * 1f;
				return num5 + fmodParamMin;
			}
		}
		else
		{
			num3 = 0f;
		}
		float num6 = fmodParamMax - fmodParamMin;
		float num7 = num6 * num3;
		return num7 + fmodParamMin;
	}

	public void SetSliderValue(float value)
	{
		float num = inputRangeMin;
		float num2 = default(float);
		if (!(inputRangeMin > num2))
		{
			num = inputRangeMax;
			if (!(num2 > inputRangeMax))
			{
			}
		}
		testSliderValue = num;
		ApplyOnce();
	}

	public void SetFMODRange(float min, float max)
	{
		fmodParamMin = min;
		fmodParamMax = max;
		ApplyOnce();
	}

	public void SetParameterName(string name)
	{
		parameterName = name;
		ApplyOnce();
	}

	public void SetProvider(MonoBehaviour providerComponent, string propertyName = "")
	{
		floatValueProvider = providerComponent;
		providerPropertyName = propertyName;
		BindProvider();
		ApplyOnce();
	}

	public void SetTargetMode(ParameterTarget newTarget)
	{
		target = newTarget;
		ApplyOnce();
	}

	public FMODParameterSetter()
	{
		StudioEventEmitter[] array = new StudioEventEmitter[0];
		targetEmitters = array;
		base._002Ector();
	}
}
