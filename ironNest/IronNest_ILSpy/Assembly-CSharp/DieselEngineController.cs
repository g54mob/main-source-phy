using System;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.Events;

public class DieselEngineController : MonoBehaviour
{
	private DialInteractable fuelMixtureDial;

	private DialInteractable injectionTimingDial;

	private DialGaugeDisplay fuelMixtureGauge;

	private DialGaugeDisplay injectionTimingGauge;

	private float fuelMixtureTarget = 0.55f;

	private float fuelMixtureTolerance = 0.08f;

	private float injectionTimingTarget = 0.5f;

	private float injectionTimingTolerance = 0.08f;

	private float fuelOperatingMin = 0.3f;

	private float fuelOperatingMax = 0.8f;

	private float timingOperatingMin = 0.25f;

	private float timingOperatingMax = 0.75f;

	private float warningShutdownSeconds = 15f;

	private float fuelHardShutoffFloor = 0.1f;

	private float couplingStrength = 0.15f;

	private float driftDecaySeconds = 1f;

	private float maxDriftOffset = 0.1f;

	private float ignitionCooldownSeconds = 2f;

	private bool debugLog;

	private bool forceEngineOn;

	private bool forceEngineOff;

	private float _debugFuelMixtureValue;

	private float _debugInjectionTimingValue;

	private bool _debugBothInBalance;

	private bool _debugInOperatingRange;

	private float _debugWarningCountdown;

	private UnityEvent OnEngineStartSuccess;

	private UnityEvent OnManualStartupSequence;

	private UnityEvent OnEngineStartFailure;

	private UnityEvent OnBothValuesInBalance;

	private UnityEvent OnValuesOutOfBalance;

	private UnityEvent OnEnterWarning;

	private UnityEvent OnExitWarning;

	private UnityEvent OnEngineShutdown;

	private float _003CFuelMixtureSystemValue_003Ek__BackingField;

	private float _003CInjectionTimingSystemValue_003Ek__BackingField;

	private bool _003CBothInBalance_003Ek__BackingField;

	private bool _003CEnginesRunning_003Ek__BackingField;

	private bool _003CInWarningState_003Ek__BackingField;

	private float _003CWarningCountdownRemaining_003Ek__BackingField;

	private float _prevFuelDialValue;

	private float _prevTimingDialValue;

	private float _fuelDriftOffset;

	private float _timingDriftOffset;

	private float _ignitionCooldownRemaining;

	private bool _prevForceEngineOn;

	private bool _prevForceEngineOff;

	public float FuelMixtureSystemValue
	{
		get
		{
			return _003CFuelMixtureSystemValue_003Ek__BackingField;
		}
		private set
		{
			_003CFuelMixtureSystemValue_003Ek__BackingField = value;
		}
	}

	public float InjectionTimingSystemValue
	{
		get
		{
			return _003CInjectionTimingSystemValue_003Ek__BackingField;
		}
		private set
		{
			_003CInjectionTimingSystemValue_003Ek__BackingField = value;
		}
	}

	public bool BothInBalance
	{
		get
		{
			return _003CBothInBalance_003Ek__BackingField;
		}
		private set
		{
			_003CBothInBalance_003Ek__BackingField = value;
		}
	}

	public bool EnginesRunning
	{
		get
		{
			return _003CEnginesRunning_003Ek__BackingField;
		}
		private set
		{
			_003CEnginesRunning_003Ek__BackingField = value;
		}
	}

	public bool InWarningState
	{
		get
		{
			return _003CInWarningState_003Ek__BackingField;
		}
		private set
		{
			_003CInWarningState_003Ek__BackingField = value;
		}
	}

	public float WarningCountdownRemaining
	{
		get
		{
			return _003CWarningCountdownRemaining_003Ek__BackingField;
		}
		private set
		{
			_003CWarningCountdownRemaining_003Ek__BackingField = value;
		}
	}

	private void Awake()
	{
		if (fuelMixtureGauge == null)
		{
			Debug.LogWarning("[DieselEngineController] Fuel Mixture Gauge not assigned.", this);
		}
		else
		{
			DialGaugeDisplay dialGaugeDisplay = fuelMixtureGauge;
			dialGaugeDisplay.floatValueProvider = null;
		}
		if (!(injectionTimingGauge != null))
		{
			Debug.LogWarning("[DieselEngineController] Injection Timing Gauge not assigned.", this);
			return;
		}
		DialGaugeDisplay dialGaugeDisplay2 = injectionTimingGauge;
		dialGaugeDisplay2.floatValueProvider = null;
	}

	private void Start()
	{
		//IL_0058: Expected F4, but got I4
		//IL_0081: Expected F4, but got I4
		float prevFuelDialValue;
		if (fuelMixtureDial != null)
		{
			DialInteractable dialInteractable = fuelMixtureDial;
			prevFuelDialValue = dialInteractable.accumulatedValue;
		}
		else
		{
			prevFuelDialValue = 0f;
		}
		_prevFuelDialValue = prevFuelDialValue;
		bool flag = injectionTimingDial != null;
		bool flag2 = !flag;
		float prevTimingDialValue = 0f;
		if (!flag2)
		{
			DialInteractable dialInteractable2 = injectionTimingDial;
			prevTimingDialValue = dialInteractable2.accumulatedValue;
		}
		_prevTimingDialValue = prevTimingDialValue;
	}

	private void Update()
	{
		//IL_08c6: Invalid comparison between I4 and F4
		//IL_08d5: Invalid comparison between I4 and F4
		//IL_08e7: Expected F4, but got I4
		//IL_012b: Expected F4, but got I4
		//IL_0a14: Expected O, but got F4
		//IL_0a1e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a23: Expected O, but got Unknown
		//IL_0a77: Invalid comparison between I4 and F4
		//IL_0177: Expected F4, but got I4
		//IL_038f: Expected F4, but got I4
		//IL_0aad: Invalid comparison between I4 and F4
		//IL_03cb: Expected F4, but got I4
		//IL_0565: Invalid comparison between F4 and I4
		//IL_048b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0490: Expected O, but got Unknown
		//IL_049a: Invalid comparison between F4 and O
		//IL_04ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ef: Expected O, but got Unknown
		//IL_04f9: Invalid comparison between F4 and O
		//IL_07bf: Invalid comparison between I4 and F4
		float deltaTime = Time.deltaTime;
		float num = _ignitionCooldownRemaining - deltaTime;
		bool flag = 0f == num;
		bool flag2 = !(0f < num);
		float ignitionCooldownRemaining = 0f;
		if (!flag2)
		{
			ignitionCooldownRemaining = num;
		}
		_ignitionCooldownRemaining = ignitionCooldownRemaining;
		if (!flag && !_prevForceEngineOn)
		{
			forceEngineOff = false;
			_prevForceEngineOff = false;
			ForceStart();
		}
		if (forceEngineOff && !_prevForceEngineOff)
		{
			forceEngineOn = false;
			_prevForceEngineOn = false;
			if (!_003CEnginesRunning_003Ek__BackingField && debugLog)
			{
				Debug.Log("[DieselEngineController] ForceStop ignored — engine already stopped.");
			}
			if (fuelMixtureDial != null)
			{
				fuelMixtureDial.SetDialValue(0f);
				ignitionCooldownRemaining = 0f;
			}
			if (injectionTimingDial != null)
			{
				injectionTimingDial.SetDialValue(0f);
				ignitionCooldownRemaining = 0f;
			}
			_prevFuelDialValue = 0f;
			_fuelDriftOffset = 0f;
			if (debugLog)
			{
				Debug.Log("[DieselEngineController] ForceStop — dials zeroed, engine off.");
			}
			_003CEnginesRunning_003Ek__BackingField = false;
			_003CWarningCountdownRemaining_003Ek__BackingField = 0f;
			if (OnEngineShutdown != null)
			{
				OnEngineShutdown.Invoke();
			}
		}
		_prevForceEngineOn = forceEngineOn;
		_prevForceEngineOff = forceEngineOff;
		float num2;
		if (fuelMixtureDial != null)
		{
			DialInteractable dialInteractable = fuelMixtureDial;
			num2 = dialInteractable.accumulatedValue;
		}
		else
		{
			num2 = _prevFuelDialValue;
		}
		float num3;
		if (injectionTimingDial != null)
		{
			DialInteractable dialInteractable2 = injectionTimingDial;
			num3 = dialInteractable2.accumulatedValue;
		}
		else
		{
			num3 = _prevTimingDialValue;
		}
		float num4 = maxDriftOffset ^ -0f;
		float num5 = num2 - _prevFuelDialValue;
		_prevFuelDialValue = num2;
		float num6 = num3 - _prevTimingDialValue;
		_prevTimingDialValue = num3;
		float num7 = num5 * couplingStrength;
		float num8 = num6 * couplingStrength;
		float num9 = num7 + _timingDriftOffset;
		float num10 = num8 + _fuelDriftOffset;
		if (!(num4 > num10))
		{
			num4 = maxDriftOffset;
			if (!(num10 > maxDriftOffset))
			{
				goto IL_09da;
			}
		}
		num10 = num4;
		goto IL_09da;
		IL_0839:
		_debugFuelMixtureValue = _003CFuelMixtureSystemValue_003Ek__BackingField;
		_debugInjectionTimingValue = _003CInjectionTimingSystemValue_003Ek__BackingField;
		_debugBothInBalance = _003CBothInBalance_003Ek__BackingField;
		bool flag3 = !_003CEnginesRunning_003Ek__BackingField;
		bool debugInOperatingRange = false;
		if (!flag3)
		{
			bool flag4 = !_003CInWarningState_003Ek__BackingField;
			debugInOperatingRange = flag4;
		}
		_debugInOperatingRange = debugInOperatingRange;
		_debugWarningCountdown = _003CWarningCountdownRemaining_003Ek__BackingField;
		return;
		IL_09da:
		float num11 = maxDriftOffset ^ -0f;
		if (!(num11 > num9))
		{
			ignitionCooldownRemaining = maxDriftOffset;
			if (num9 > maxDriftOffset)
			{
				num9 = maxDriftOffset;
			}
		}
		else
		{
			num9 = num11;
		}
		object obj = deltaTime ^ -0f;
		object obj2 = obj / driftDecaySeconds;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033F3D0");
		float num12 = num10 * (float)obj2;
		float num13 = num9 * (float)obj2;
		_fuelDriftOffset = num12;
		float num14 = num12 + num2;
		_timingDriftOffset = num13;
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
		float num15 = num13 + num3;
		_003CFuelMixtureSystemValue_003Ek__BackingField = num14;
		if (!(0f > num15))
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
		_003CInjectionTimingSystemValue_003Ek__BackingField = num15;
		if (fuelMixtureGauge != null)
		{
			DialGaugeDisplay dialGaugeDisplay = fuelMixtureGauge;
			dialGaugeDisplay.targetNumber = _003CFuelMixtureSystemValue_003Ek__BackingField;
		}
		if (injectionTimingGauge != null)
		{
			DialGaugeDisplay dialGaugeDisplay2 = injectionTimingGauge;
			dialGaugeDisplay2.targetNumber = _003CInjectionTimingSystemValue_003Ek__BackingField;
		}
		if (!_003CEnginesRunning_003Ek__BackingField)
		{
			float num16 = _003CFuelMixtureSystemValue_003Ek__BackingField - fuelMixtureTarget;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
			object obj3 = num16 & 0;
			float num17 = fuelMixtureTolerance;
			bool flag5;
			if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)num17) < System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj3))
			{
				flag5 = false;
			}
			else
			{
				float num18 = _003CInjectionTimingSystemValue_003Ek__BackingField - injectionTimingTarget;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
				object obj4 = num18 & 0;
				float num19 = injectionTimingTolerance;
				bool flag6 = System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)num19) < System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj4);
				flag5 = !flag6;
			}
			_003CBothInBalance_003Ek__BackingField = flag5;
			(flag5 ? OnBothValuesInBalance : OnValuesOutOfBalance)?.Invoke();
			if (!_003CEnginesRunning_003Ek__BackingField)
			{
				goto IL_0839;
			}
		}
		if (fuelHardShutoffFloor > 0f && fuelHardShutoffFloor > _003CFuelMixtureSystemValue_003Ek__BackingField)
		{
			if (debugLog)
			{
				Debug.Log("[DieselEngineController] HARD SHUTOFF — fuel below floor.");
			}
			_003CEnginesRunning_003Ek__BackingField = false;
			_003CWarningCountdownRemaining_003Ek__BackingField = 0f;
			if (OnEngineShutdown != null)
			{
				OnEngineShutdown.Invoke();
			}
			return;
		}
		bool flag10;
		UnityEvent unityEvent;
		if (!(_003CFuelMixtureSystemValue_003Ek__BackingField < fuelOperatingMin) && !(fuelOperatingMax < _003CFuelMixtureSystemValue_003Ek__BackingField))
		{
			bool flag7 = _003CInjectionTimingSystemValue_003Ek__BackingField < timingOperatingMin;
			bool flag8 = false;
			if (!flag7)
			{
				bool flag9 = timingOperatingMax < _003CInjectionTimingSystemValue_003Ek__BackingField;
				flag8 = !flag9;
			}
			flag10 = _003CInWarningState_003Ek__BackingField;
			if (flag8)
			{
				if (~(_003CInWarningState_003Ek__BackingField ? 1u : 0u) != 0)
				{
					goto IL_0839;
				}
				_003CInWarningState_003Ek__BackingField = false;
				_003CWarningCountdownRemaining_003Ek__BackingField = 0f;
				if (debugLog)
				{
					Debug.Log("[DieselEngineController] Warning cancelled — values back in operating range.");
				}
				unityEvent = OnExitWarning;
				goto IL_0b50;
			}
		}
		else
		{
			flag10 = _003CInWarningState_003Ek__BackingField;
		}
		if (!flag10)
		{
			_003CWarningCountdownRemaining_003Ek__BackingField = warningShutdownSeconds;
			_003CInWarningState_003Ek__BackingField = true;
			if (debugLog)
			{
				Debug.Log("[DieselEngineController] WARNING — values outside operating range.");
			}
			if (OnEnterWarning != null)
			{
				OnEnterWarning.Invoke();
			}
		}
		if (0f < (_003CWarningCountdownRemaining_003Ek__BackingField -= deltaTime))
		{
			goto IL_0839;
		}
		if (debugLog)
		{
			Debug.Log("[DieselEngineController] SHUTDOWN — warning countdown expired.");
		}
		unityEvent = OnEngineShutdown;
		_003CEnginesRunning_003Ek__BackingField = false;
		_003CWarningCountdownRemaining_003Ek__BackingField = 0f;
		goto IL_0b50;
		IL_0b50:
		unityEvent?.Invoke();
		goto IL_0839;
	}

	public void AttemptIgnition()
	{
		//IL_0010: Invalid comparison between F4 and I4
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Expected O, but got Unknown
		//IL_0084: Invalid comparison between F4 and O
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Expected O, but got Unknown
		//IL_00bc: Invalid comparison between F4 and O
		if (!_003CEnginesRunning_003Ek__BackingField)
		{
			if (!(_ignitionCooldownRemaining > 0f))
			{
				_ignitionCooldownRemaining = ignitionCooldownSeconds;
				UnityEvent unityEvent;
				if (!_003CBothInBalance_003Ek__BackingField)
				{
					float num = _003CFuelMixtureSystemValue_003Ek__BackingField - fuelMixtureTarget;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
					object obj = num & 0;
					float num2 = fuelMixtureTolerance;
					bool flag = System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)num2) < System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj);
					float num3 = _003CInjectionTimingSystemValue_003Ek__BackingField - injectionTimingTarget;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
					object obj2 = num3 & 0;
					float num4 = injectionTimingTolerance;
					bool flag2 = System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)num4) < System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj2);
					bool flag3 = !flag2;
					if (debugLog)
					{
						bool flag4 = !flag;
						string text = "OK";
						if (!flag4)
						{
							text = "OUT";
						}
						string text2 = "OK";
						if (!flag3)
						{
							text2 = "OUT";
						}
						string message = "[DieselEngineController] Ignition FAILED — Fuel:" + text + " Timing:" + text2;
						Debug.Log(message);
					}
					unityEvent = OnEngineStartFailure;
				}
				else
				{
					bool flag5 = !debugLog;
					_003CEnginesRunning_003Ek__BackingField = true;
					if (!flag5)
					{
						Debug.Log("[DieselEngineController] Ignition SUCCESS.");
					}
					if (OnEngineStartSuccess != null)
					{
						OnEngineStartSuccess.Invoke();
					}
					unityEvent = OnManualStartupSequence;
				}
				unityEvent?.Invoke();
			}
			else if (debugLog)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
				object arg = default(object);
				string message2 = $"[DieselEngineController] Ignition ignored — cooldown {arg:F2}s remaining.";
				Debug.Log(message2);
			}
		}
		else if (debugLog)
		{
			Debug.Log("[DieselEngineController] Ignition ignored — engines already running.");
		}
	}

	public bool IsFuelBalancedForStart()
	{
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Expected O, but got Unknown
		//IL_0032: Invalid comparison between F4 and O
		float num = _003CFuelMixtureSystemValue_003Ek__BackingField - fuelMixtureTarget;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
		object obj = num & 0;
		float num2 = fuelMixtureTolerance;
		bool flag = System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)num2) < System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj);
		return !flag;
	}

	public bool IsFuelInRangeForRunning()
	{
		if (_003CFuelMixtureSystemValue_003Ek__BackingField < fuelOperatingMin)
		{
			return false;
		}
		bool flag = fuelOperatingMax < _003CFuelMixtureSystemValue_003Ek__BackingField;
		return !flag;
	}

	public bool IsInjectionTimingBalancedForStart()
	{
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Expected O, but got Unknown
		//IL_0032: Invalid comparison between F4 and O
		float num = _003CInjectionTimingSystemValue_003Ek__BackingField - injectionTimingTarget;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
		object obj = num & 0;
		float num2 = injectionTimingTolerance;
		bool flag = System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)num2) < System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj);
		return !flag;
	}

	public bool IsInjectionTimingInRangeForRunning()
	{
		if (_003CInjectionTimingSystemValue_003Ek__BackingField < timingOperatingMin)
		{
			return false;
		}
		bool flag = timingOperatingMax < _003CInjectionTimingSystemValue_003Ek__BackingField;
		return !flag;
	}

	private void ForceStart()
	{
		//IL_00be: Expected O, but got I4
		//IL_00ec: Expected O, but got I4
		if (!_003CEnginesRunning_003Ek__BackingField)
		{
			float num = fuelOperatingMax + fuelOperatingMin;
			float num2 = timingOperatingMax + timingOperatingMin;
			float num3 = num * 0.5f;
			float num4 = num2 * 0.5f;
			if (fuelMixtureDial != null)
			{
				fuelMixtureDial.SetDialValue(num3);
				float num5 = num3;
			}
			bool flag = injectionTimingDial != null;
			bool flag2 = !flag;
			object obj = 0;
			if (!flag2)
			{
				injectionTimingDial.SetDialValue(num4);
				float num5 = num4;
				obj = 0;
			}
			_prevFuelDialValue = num3;
			_prevTimingDialValue = num4;
			_fuelDriftOffset = 0f;
			_003CEnginesRunning_003Ek__BackingField = true;
			_003CWarningCountdownRemaining_003Ek__BackingField = 0f;
			if (debugLog)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
				Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
				object arg = default(object);
				object arg2 = default(object);
				string message = $"[DieselEngineController] ForceStart — dials set to Fuel:{arg:F2} Timing:{arg2:F2}.";
				Debug.Log(message);
			}
			if (OnEngineStartSuccess != null)
			{
				OnEngineStartSuccess.Invoke();
			}
		}
		else if (debugLog)
		{
			Debug.Log("[DieselEngineController] ForceStart ignored — engine already running.");
		}
	}

	private void ForceStop()
	{
		if (!_003CEnginesRunning_003Ek__BackingField && debugLog)
		{
			Debug.Log("[DieselEngineController] ForceStop ignored — engine already stopped.");
		}
		if (fuelMixtureDial != null)
		{
			fuelMixtureDial.SetDialValue(0f);
		}
		if (injectionTimingDial != null)
		{
			injectionTimingDial.SetDialValue(0f);
		}
		_prevFuelDialValue = 0f;
		_fuelDriftOffset = 0f;
		if (debugLog)
		{
			Debug.Log("[DieselEngineController] ForceStop — dials zeroed, engine off.");
		}
		_003CEnginesRunning_003Ek__BackingField = false;
		_003CWarningCountdownRemaining_003Ek__BackingField = 0f;
		if (OnEngineShutdown != null)
		{
			OnEngineShutdown.Invoke();
		}
	}

	private void ShutdownEngine()
	{
		_003CEnginesRunning_003Ek__BackingField = false;
		_003CWarningCountdownRemaining_003Ek__BackingField = 0f;
		if (OnEngineShutdown != null)
		{
			OnEngineShutdown.Invoke();
		}
	}
}
