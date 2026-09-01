using System;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using UnityEngine;

public class EngineControlsLightsController : MonoBehaviour
{
	private DieselEngineController _engineController;

	private GameObject _fuelLightRed;

	private GameObject _fuelLightYellow;

	private GameObject _fuelLightGreen;

	private GameObject _injectionLightRed;

	private GameObject _injectionLightYellow;

	private GameObject _injectionLightGreen;

	private bool _wasEngineRunning;

	private bool _wasFuelOk;

	private bool _wasInjectionTimingOk;

	private void Start()
	{
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Expected O, but got Unknown
		//IL_00d3: Invalid comparison between F4 and O
		//IL_015b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0160: Expected O, but got Unknown
		//IL_016d: Invalid comparison between F4 and O
		DieselEngineController engineController = _engineController;
		DieselEngineController engineController2 = _engineController;
		_wasEngineRunning = engineController._003CEnginesRunning_003Ek__BackingField;
		bool flag;
		bool flag2;
		if (~(engineController._003CEnginesRunning_003Ek__BackingField ? 1u : 0u) == 0)
		{
			if (engineController2._003CFuelMixtureSystemValue_003Ek__BackingField < engineController2.fuelOperatingMin)
			{
				flag = false;
				goto IL_017f;
			}
			flag2 = engineController2.fuelOperatingMax < engineController2._003CFuelMixtureSystemValue_003Ek__BackingField;
		}
		else
		{
			float num = engineController2._003CFuelMixtureSystemValue_003Ek__BackingField - engineController2.fuelMixtureTarget;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
			object obj = num & 0;
			float fuelMixtureTolerance = engineController2.fuelMixtureTolerance;
			flag2 = System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)fuelMixtureTolerance) < System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj);
		}
		flag = !flag2;
		goto IL_017f;
		IL_017f:
		_wasFuelOk = flag;
		bool wasInjectionTimingOk;
		bool flag3;
		if (~(engineController._003CEnginesRunning_003Ek__BackingField ? 1u : 0u) == 0)
		{
			if (engineController2._003CInjectionTimingSystemValue_003Ek__BackingField < engineController2.timingOperatingMin)
			{
				wasInjectionTimingOk = false;
				goto IL_01b5;
			}
			flag3 = engineController2.timingOperatingMax < engineController2._003CInjectionTimingSystemValue_003Ek__BackingField;
		}
		else
		{
			float num2 = engineController2._003CInjectionTimingSystemValue_003Ek__BackingField - engineController2.injectionTimingTarget;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
			object obj2 = num2 & 0;
			float injectionTimingTolerance = engineController2.injectionTimingTolerance;
			flag3 = System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)injectionTimingTolerance) < System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj2);
		}
		wasInjectionTimingOk = !flag3;
		goto IL_01b5;
		IL_01b5:
		_wasInjectionTimingOk = wasInjectionTimingOk;
		UpdateFuelLightState(_wasEngineRunning, flag);
		Cpp2ILHelpers.NoteDecompilerIssue("Invalid instruction: 182 Invalid \"Jump target not found in method: 0x180536BD0\"");
		throw new NullReferenceException();
	}

	private void Update()
	{
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Expected O, but got Unknown
		//IL_00fa: Invalid comparison between F4 and O
		//IL_0134: Unknown result type (might be due to invalid IL or missing references)
		//IL_0139: Expected O, but got Unknown
		//IL_0146: Invalid comparison between F4 and O
		DieselEngineController engineController = _engineController;
		DieselEngineController engineController2 = _engineController;
		bool flag;
		bool flag3;
		bool flag4;
		if (~(engineController._003CEnginesRunning_003Ek__BackingField ? 1u : 0u) == 0)
		{
			if (engineController2._003CFuelMixtureSystemValue_003Ek__BackingField < engineController2.fuelOperatingMin)
			{
				flag = false;
			}
			else
			{
				bool flag2 = engineController2.fuelOperatingMax < engineController2._003CFuelMixtureSystemValue_003Ek__BackingField;
				flag = !flag2;
			}
			if (engineController2._003CInjectionTimingSystemValue_003Ek__BackingField < engineController2.timingOperatingMin)
			{
				flag3 = false;
				goto IL_022a;
			}
			flag4 = engineController2.timingOperatingMax < engineController2._003CInjectionTimingSystemValue_003Ek__BackingField;
		}
		else
		{
			float num = engineController2._003CFuelMixtureSystemValue_003Ek__BackingField - engineController2.fuelMixtureTarget;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
			object obj = num & 0;
			float fuelMixtureTolerance = engineController2.fuelMixtureTolerance;
			bool flag5 = System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)fuelMixtureTolerance) < System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj);
			float num2 = engineController2._003CInjectionTimingSystemValue_003Ek__BackingField - engineController2.injectionTimingTarget;
			flag = !flag5;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
			object obj2 = num2 & 0;
			float injectionTimingTolerance = engineController2.injectionTimingTolerance;
			flag4 = System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)injectionTimingTolerance) < System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj2);
		}
		flag3 = !flag4;
		goto IL_022a;
		IL_022a:
		if (_wasEngineRunning == engineController._003CEnginesRunning_003Ek__BackingField)
		{
			if (_wasFuelOk != flag)
			{
				UpdateFuelLightState(engineController._003CEnginesRunning_003Ek__BackingField, flag);
				_wasFuelOk = flag;
			}
			if (_wasInjectionTimingOk == flag3)
			{
				return;
			}
			UpdateInjectionTimingLightState(engineController._003CEnginesRunning_003Ek__BackingField, flag3);
		}
		else
		{
			UpdateFuelLightState(engineController._003CEnginesRunning_003Ek__BackingField, flag);
			UpdateInjectionTimingLightState(engineController._003CEnginesRunning_003Ek__BackingField, flag3);
			_wasEngineRunning = engineController._003CEnginesRunning_003Ek__BackingField;
			_wasFuelOk = flag;
		}
		_wasInjectionTimingOk = flag3;
	}

	private bool IsFuelOk(bool isEngineRunning)
	{
		//IL_0117: Expected I4, but got O
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Expected O, but got Unknown
		//IL_00f3: Invalid comparison between F4 and O
		DieselEngineController engineController = _engineController;
		if (isEngineRunning)
		{
			if ((object)_engineController != null)
			{
				if (engineController._003CFuelMixtureSystemValue_003Ek__BackingField < engineController.fuelOperatingMin)
				{
					return false;
				}
				bool flag = engineController.fuelOperatingMax < engineController._003CFuelMixtureSystemValue_003Ek__BackingField;
				return !flag;
			}
		}
		else if ((object)_engineController != null)
		{
			float num = engineController._003CFuelMixtureSystemValue_003Ek__BackingField - engineController.fuelMixtureTarget;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
			object obj = num & 0;
			float fuelMixtureTolerance = engineController.fuelMixtureTolerance;
			bool flag2 = System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)fuelMixtureTolerance) < System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj);
			return !flag2;
		}
		NullReferenceException ex = new NullReferenceException();
		return (byte)(int)ex != 0;
	}

	private bool IsInjectionTimingOk(bool isEngineRunning)
	{
		//IL_0117: Expected I4, but got O
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Expected O, but got Unknown
		//IL_00f3: Invalid comparison between F4 and O
		DieselEngineController engineController = _engineController;
		if (isEngineRunning)
		{
			if ((object)_engineController != null)
			{
				if (engineController._003CInjectionTimingSystemValue_003Ek__BackingField < engineController.timingOperatingMin)
				{
					return false;
				}
				bool flag = engineController.timingOperatingMax < engineController._003CInjectionTimingSystemValue_003Ek__BackingField;
				return !flag;
			}
		}
		else if ((object)_engineController != null)
		{
			float num = engineController._003CInjectionTimingSystemValue_003Ek__BackingField - engineController.injectionTimingTarget;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
			object obj = num & 0;
			float injectionTimingTolerance = engineController.injectionTimingTolerance;
			bool flag2 = System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)injectionTimingTolerance) < System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj);
			return !flag2;
		}
		NullReferenceException ex = new NullReferenceException();
		return (byte)(int)ex != 0;
	}

	private void UpdateFuelLightState(bool isEngineRunning, bool isFuelOk)
	{
		bool active = ((!isEngineRunning) ? ((byte)((isFuelOk ? 1u : 0u) ^ 1u) != 0) : false);
		_fuelLightRed.SetActive(active);
		bool flag = !isEngineRunning;
		bool active2 = false;
		if (!flag)
		{
			active2 = (byte)((isFuelOk ? 1u : 0u) ^ 1u) != 0;
		}
		_fuelLightYellow.SetActive(active2);
		_fuelLightGreen.SetActive(isFuelOk);
	}

	private void UpdateInjectionTimingLightState(bool isEngineRunning, bool isInjectionTimingOk)
	{
		bool active = ((!isEngineRunning) ? ((byte)((isInjectionTimingOk ? 1u : 0u) ^ 1u) != 0) : false);
		_injectionLightRed.SetActive(active);
		bool flag = !isEngineRunning;
		bool active2 = false;
		if (!flag)
		{
			active2 = (byte)((isInjectionTimingOk ? 1u : 0u) ^ 1u) != 0;
		}
		_injectionLightYellow.SetActive(active2);
		_injectionLightGreen.SetActive(isInjectionTimingOk);
	}
}
