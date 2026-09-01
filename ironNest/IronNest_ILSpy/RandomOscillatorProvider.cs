using System;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using UnityEngine;

public class RandomOscillatorProvider : MonoBehaviour, IFloatValueProvider
{
	public float outputMin;

	public float outputMax = 100f;

	public float driftSpeedMin = 0.5f;

	public float driftSpeedMax = 2f;

	public float arrivalThreshold = 0.5f;

	public float holdDurationMin;

	public float holdDurationMax;

	public bool debugLogging;

	private float _currentValue;

	private float _targetValue;

	private float _activeDriftSpeed;

	private float _holdTimer;

	private bool _isHolding;

	public float CurrentValue => _currentValue;

	public float TargetValue => _targetValue;

	public float ActiveDriftSpeed => _activeDriftSpeed;

	private void Awake()
	{
		float currentValue = UnityEngine.Random.Range(outputMin, outputMax);
		_currentValue = currentValue;
		PickNewTarget();
	}

	private void Update()
	{
		//IL_01ba: Invalid comparison between I4 and F4
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Expected O, but got Unknown
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Expected O, but got Unknown
		//IL_005a: Invalid comparison between I4 and F4
		//IL_00a5: Expected F4, but got I4
		//IL_0260: Unknown result type (might be due to invalid IL or missing references)
		//IL_0265: Expected O, but got Unknown
		//IL_026f: Invalid comparison between F4 and O
		//IL_028c: Invalid comparison between F4 and I4
		if (!_isHolding)
		{
			float deltaTime = Time.deltaTime;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C80]");
			object obj = deltaTime ^ 0;
			object obj2 = obj / _activeDriftSpeed;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033F3D0");
			float num = 1f - (float)obj2;
			if (!(0f > num))
			{
				if (num > 1f)
				{
					num = 1f;
				}
			}
			else
			{
				num = 0f;
			}
			float num2 = _targetValue - _currentValue;
			float num3 = num2 * num;
			float num4 = (_currentValue = num3 + _currentValue) - _targetValue;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
			object obj3 = num4 & 0;
			float num5 = arrivalThreshold;
			if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)num5) < System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj3))
			{
				return;
			}
			float num6 = holdDurationMin;
			_currentValue = _targetValue;
			if (holdDurationMax > holdDurationMin)
			{
				float num7 = UnityEngine.Random.Range(holdDurationMin, holdDurationMax);
				num = holdDurationMax;
				num6 = num7;
			}
			if (!(num6 > 0f))
			{
				PickNewTarget();
				return;
			}
			bool flag = !debugLogging;
			_holdTimer = num6;
			_isHolding = true;
			if (!flag)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
				Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
				object arg = default(object);
				object arg2 = default(object);
				string message = $"[RandomOscillator] Arrived at {arg:F2}. Holding for {arg2:F2}s.";
				Debug.Log(message, this);
			}
		}
		else
		{
			float deltaTime2 = Time.deltaTime;
			if (!(0f < (_holdTimer -= deltaTime2)))
			{
				_isHolding = false;
				PickNewTarget();
			}
		}
	}

	public float GetFloatValue()
	{
		return _currentValue;
	}

	private void PickNewTarget()
	{
		//IL_00ce: Expected O, but got I4
		//IL_0023: Expected O, but got I4
		float num = outputMax;
		float targetValue = UnityEngine.Random.Range(outputMin, outputMax);
		float activeDriftSpeed = driftSpeedMin;
		_targetValue = targetValue;
		bool flag = !(driftSpeedMax > driftSpeedMin);
		object obj = 0;
		if (!flag)
		{
			float num2 = UnityEngine.Random.Range(driftSpeedMin, driftSpeedMax);
			obj = 0;
			num = driftSpeedMax;
			activeDriftSpeed = num2;
		}
		_activeDriftSpeed = activeDriftSpeed;
		if (debugLogging)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
			object arg = default(object);
			object arg2 = default(object);
			string message = $"[RandomOscillator] New target: {arg:F2}, tau: {arg2:F2}s";
			Debug.Log(message, this);
		}
	}

	private void OnValidate()
	{
		//IL_004c: Expected O, but got I4
		//IL_0055: Expected O, but got I4
		//IL_01b8: Expected O, but got I4
		//IL_01c1: Expected O, but got I4
		if (!(outputMin < outputMax))
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
			object arg = default(object);
			object arg2 = default(object);
			string message = $"[RandomOscillator] outputMin ({arg}) must be less than outputMax ({arg2}).";
			Debug.LogWarning(message, this);
			object obj = 0;
			object obj2 = 0;
			float num = outputMax;
			float num2 = outputMin;
		}
		if (driftSpeedMin > driftSpeedMax)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
			object arg3 = default(object);
			object arg4 = default(object);
			string message2 = $"[RandomOscillator] driftSpeedMin ({arg3}) exceeds driftSpeedMax ({arg4}). Clamping.";
			Debug.LogWarning(message2, this);
			float num3 = driftSpeedMin;
			if (driftSpeedMin < driftSpeedMax)
			{
				num3 = driftSpeedMax;
			}
			driftSpeedMax = num3;
			object obj = 0;
			object obj2 = 0;
			float num = driftSpeedMax;
			float num2 = driftSpeedMin;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
		object arg5 = default(object);
		object arg6 = default(object);
		string message3 = $"[RandomOscillator] holdDurationMin ({arg5}) exceeds holdDurationMax ({arg6}). Clamping in runtime.";
		Debug.LogWarning(message3, this);
		float num4 = holdDurationMin;
		if (holdDurationMin < holdDurationMax)
		{
			num4 = holdDurationMax;
		}
		holdDurationMax = num4;
	}
}
