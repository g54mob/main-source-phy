using System;
using System.Collections;
using System.Collections.Generic;
using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.Events;

public class ArtilleryComputer : MonoBehaviour
{
	[Serializable]
	public class CalculationSuccessEvent : UnityEvent<float, float, int, bool>
	{
	}

	[Serializable]
	public class CalculationErrorEvent : UnityEvent<float, int, string>
	{
	}

	private sealed class _003CInvokeSuccessWithDelay_003Ed__44 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public float delaySeconds;

		public ArtilleryComputer _003C_003E4__this;

		public float elevation;

		public float clampedRange;

		public int inputCharge;

		public bool wasClamped;

		object IEnumerator<object>.Current => _003C_003E2__current;

		object IEnumerator.Current => _003C_003E2__current;

		public _003CInvokeSuccessWithDelay_003Ed__44(int _003C_003E1__state)
		{
			((IDisposable)this).Dispose();
			this._003C_003E1__state = _003C_003E1__state;
		}

		void IDisposable.Dispose()
		{
		}

		private bool MoveNext()
		{
			//IL_0014: Expected I4, but got I8
			//IL_001f: Invalid comparison between F4 and I4
			//IL_0097: Expected I4, but got I8
			//IL_00f0: Expected I4, but got O
			ArtilleryComputer artilleryComputer = _003C_003E4__this;
			if (_003C_003E1__state == 0)
			{
				_003C_003E1__state = -1;
				if (delaySeconds > 0f)
				{
					WaitForSeconds waitForSeconds = new WaitForSeconds(delaySeconds);
					_003C_003E2__current = waitForSeconds;
					_003C_003E1__state = 1;
					return true;
				}
			}
			else
			{
				if (_003C_003E1__state != 1)
				{
					goto IL_00dc;
				}
				_003C_003E1__state = -1;
			}
			if ((object)_003C_003E4__this != null)
			{
				if (artilleryComputer.OnCalculationSuccessWithDelay != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1809424C0");
				}
				artilleryComputer.successDelayRoutine = null;
				goto IL_00dc;
			}
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
			IL_00dc:
			return false;
		}

		bool IEnumerator.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in MoveNext
			return this.MoveNext();
		}

		void IEnumerator.Reset()
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
			NotSupportedException ex = new NotSupportedException();
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
			throw ex;
		}
	}

	public ShellBlueprint shellBlueprint;

	public DialInteractable rangeDial;

	public OdometerDisplay rangeOdometer;

	public DialInteractable powderChargeDial;

	public OdometerDisplay elevationOdometer;

	public LookAtTarget calculateButton;

	public float minElevation;

	public float maxElevation = 40f;

	public float rangeTolerance = 0.01f;

	public bool gateCalculateByMinimumRange = true;

	public float minDesiredRangeToEnableCalculate = 0.01f;

	public bool randomizeElevationOnError = true;

	public float errorRandomUpdatesPerSecond = 8f;

	public float errorRandomMinElevation = 0f / 0f;

	public float errorRandomMaxElevation = 0f / 0f;

	public float errorRandomJitterAmplitude = 0.5f;

	public float errorRandomJitterFrequency = 1.25f;

	public bool errorRandomImmediateFirstTick = true;

	public bool hardClampRandomizedElevation;

	public CalculationSuccessEvent OnCalculationSuccess;

	public CalculationErrorEvent OnCalculationError;

	public float successDelaySeconds;

	public CalculationSuccessEvent OnCalculationSuccessWithDelay;

	private float lastInputRange;

	private int lastInputCharge;

	private bool waitingForCalculation;

	private bool errorActive;

	private float nextErrorRandomTime;

	private float currentErrorBaseElevation;

	private float lastValidElevation;

	private float errorSeed;

	private Coroutine successDelayRoutine;

	private void Start()
	{
		//IL_0184: Expected F4, but got I4
		if (rangeDial != null)
		{
			DialInteractable dialInteractable = rangeDial;
			UnityAction<float> call = OnRangeInputChanged;
			dialInteractable.OnValueChanged.AddListener(call);
		}
		if (powderChargeDial != null)
		{
			DialInteractable dialInteractable2 = powderChargeDial;
			UnityAction<float> call2 = OnPowderChargeChanged;
			dialInteractable2.OnValueChanged.AddListener(call2);
		}
		if (calculateButton != null)
		{
			UnityAction action = OnCalculateButtonPressed;
			calculateButton.RegisterOnClickDown(action);
		}
		float value;
		if (rangeDial != null)
		{
			DialInteractable dialInteractable3 = rangeDial;
			value = dialInteractable3.accumulatedValue;
		}
		else
		{
			value = 0f;
		}
		UpdateRangeOdometer(value);
		waitingForCalculation = true;
		UpdateCalculateButtonState(requestedActive: true);
		float value2 = UnityEngine.Random.value;
		float num = value2 * 1000f;
		lastValidElevation = minElevation;
		errorSeed = num;
	}

	private void OnDestroy()
	{
		if (rangeDial != null)
		{
			DialInteractable dialInteractable = rangeDial;
			UnityAction<float> call = OnRangeInputChanged;
			dialInteractable.OnValueChanged.RemoveListener(call);
		}
		if (powderChargeDial != null)
		{
			DialInteractable dialInteractable2 = powderChargeDial;
			UnityAction<float> call2 = OnPowderChargeChanged;
			dialInteractable2.OnValueChanged.RemoveListener(call2);
		}
	}

	private void Update()
	{
		//IL_005d: Invalid comparison between I4 and F4
		//IL_0359: Expected O, but got I4
		//IL_0387: Invalid comparison between F4 and I4
		//IL_0399: Expected F4, but got I4
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Expected O, but got Unknown
		//IL_00e5: Expected O, but got I4
		//IL_01ed: Invalid comparison between F4 and I4
		//IL_01ff: Expected F4, but got I4
		//IL_03b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bb: Expected O, but got Unknown
		//IL_03d3: Expected O, but got I4
		//IL_0564: Expected F4, but got I
		//IL_0574: Expected F4, but got I
		//IL_00fc: Expected O, but got I4
		//IL_010a: Expected O, but got I4
		//IL_011f: Expected F4, but got I
		//IL_012f: Expected F4, but got I
		//IL_04b9: Expected O, but got I4
		//IL_04c2: Expected O, but got I4
		if (!errorActive || !randomizeElevationOnError || !(elevationOdometer != null))
		{
			return;
		}
		float num;
		if (!(0f < errorRandomUpdatesPerSecond))
		{
			num = 1f / 0f;
		}
		else
		{
			bool flag = !(0.0001f < errorRandomUpdatesPerSecond);
			float num2 = 0.0001f;
			if (!flag)
			{
				num2 = errorRandomUpdatesPerSecond;
			}
			num = 1f / num2;
		}
		float time = Time.time;
		bool flag2 = time < nextErrorRandomTime;
		UnityEngine.Object obj = null;
		object obj2 = 0;
		if (!flag2)
		{
			object obj3 = errorRandomMinElevation & -2147483649L;
			bool flag3 = (nint)obj3 > 2139095040;
			object obj4 = 80;
			if (!flag3)
			{
				obj4 = 108;
			}
			object obj5 = errorRandomMaxElevation & -2147483649L;
			bool flag4 = (nint)obj5 > 2139095040;
			UnityEngine.Object obj6 = (UnityEngine.Object)84;
			if (!flag4)
			{
				obj6 = (UnityEngine.Object)112;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v288 @ rax_v11+this @ rcx (ArtilleryComputer)]");
			nint num3 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v248 @ rax_v13 (UnityEngine.Object)+this @ rcx (ArtilleryComputer)]");
			bool flag5 = num3 <= 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v248 @ rax_v13 (UnityEngine.Object)+this @ rcx (ArtilleryComputer)]");
			float num4 = 0f;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v288 @ rax_v11+this @ rcx (ArtilleryComputer)]");
			float num5 = 0f;
			if (!flag5)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v288 @ rax_v11+this @ rcx (ArtilleryComputer)]");
				num4 = 0f;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v248 @ rax_v13 (UnityEngine.Object)+this @ rcx (ArtilleryComputer)]");
				num5 = 0f;
			}
			float num6 = minElevation;
			if (minElevation > maxElevation)
			{
				num6 = maxElevation;
			}
			float num7 = minElevation;
			if (minElevation < maxElevation)
			{
				num7 = maxElevation;
			}
			if (!(num6 > num5))
			{
				if (num5 > num7)
				{
					num5 = num7;
				}
			}
			else
			{
				num5 = num6;
			}
			float num8 = minElevation;
			if (minElevation > maxElevation)
			{
				num8 = maxElevation;
			}
			float num9 = minElevation;
			if (minElevation < maxElevation)
			{
				num9 = maxElevation;
			}
			if (!(num8 > num4))
			{
				if (num4 > num9)
				{
					num4 = num9;
				}
			}
			else
			{
				num4 = num8;
			}
			float num10 = UnityEngine.Random.Range(num5, num4);
			currentErrorBaseElevation = num10;
			float time2 = Time.time;
			float num11 = time2 + num;
			nextErrorRandomTime = num11;
			obj = (UnityEngine.Object)112;
			obj2 = 0;
		}
		bool flag6 = !(errorRandomJitterAmplitude > 0f);
		float num12 = 0f;
		if (!flag6)
		{
			bool flag7 = !(errorRandomJitterFrequency > 0f);
			num12 = 0f;
			if (!flag7)
			{
				float time3 = Time.time;
				float num13 = time3 + errorSeed;
				float num14 = num13 * errorRandomJitterFrequency;
				float num15 = num14 * (float)Math.PI;
				float num16 = num15 + num15;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033E400");
				num12 = num16 * errorRandomJitterAmplitude;
			}
		}
		bool flag8 = !hardClampRandomizedElevation;
		float num17 = num12 + currentErrorBaseElevation;
		if (!flag8)
		{
			float num18 = minElevation;
			if (minElevation > maxElevation)
			{
				num18 = maxElevation;
			}
			float num19 = minElevation;
			if (minElevation < maxElevation)
			{
				num19 = maxElevation;
			}
			if (!(num18 > num17))
			{
				if (num17 > num19)
				{
					num17 = num19;
				}
			}
			else
			{
				num17 = num18;
			}
		}
		OdometerDisplay odometerDisplay = elevationOdometer;
		odometerDisplay.targetNumber = num17;
	}

	private void OnRangeInputChanged(float value)
	{
		UpdateRangeOdometer(value);
		if (waitingForCalculation)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18038AA80");
			object obj = default(object);
			if (obj != null)
			{
				return;
			}
		}
		waitingForCalculation = true;
		UpdateCalculateButtonState(requestedActive: true);
		if (successDelayRoutine != null)
		{
			StopCoroutine(successDelayRoutine);
			successDelayRoutine = null;
		}
	}

	private void OnPowderChargeChanged(float value)
	{
		//IL_0159: Invalid comparison between F4 and I4
		//IL_0044: Expected F4, but got I4
		//IL_000e: Invalid comparison between F4 and I4
		//IL_0036: Expected F4, but got I4
		//IL_00e4: Invalid comparison between F4 and I4
		//IL_0093: Invalid comparison between O and F4
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803B9370");
		float num = default(float);
		float num2;
		if (!(num < 1f))
		{
			bool flag = !(num > 6f);
			num2 = num;
			if (!flag)
			{
				num2 = 6f;
			}
		}
		else
		{
			num2 = 1f;
		}
		if (powderChargeDial != null)
		{
			DialInteractable dialInteractable = powderChargeDial;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803B9370");
			object obj = default(object);
			if (obj != (object)num2)
			{
				powderChargeDial.SetDialValue(num2);
			}
		}
		if (!waitingForCalculation || num2 != (float)lastInputCharge)
		{
			waitingForCalculation = true;
			UpdateCalculateButtonState(requestedActive: true);
			if (successDelayRoutine != null)
			{
				StopCoroutine(successDelayRoutine);
				successDelayRoutine = null;
			}
		}
	}

	private void UpdateRangeOdometer(float value)
	{
		if (rangeOdometer != null)
		{
			OdometerDisplay odometerDisplay = rangeOdometer;
			odometerDisplay.targetNumber = value;
		}
	}

	private void UpdateCalculateButtonState(bool requestedActive)
	{
		//IL_00a2: Expected F4, but got I4
		bool flag = calculateButton == null;
		if (flag)
		{
			return;
		}
		bool flag2 = gateCalculateByMinimumRange == flag;
		bool flag3 = true;
		if (!flag2)
		{
			float num;
			if (rangeDial != null)
			{
				DialInteractable dialInteractable = rangeDial;
				num = dialInteractable.accumulatedValue;
			}
			else
			{
				num = 0f;
			}
			bool flag4 = num < minDesiredRangeToEnableCalculate;
			flag3 = !flag4;
		}
		bool active = flag3 & requestedActive;
		calculateButton.SetActive(active);
	}

	private void OnCalculateButtonPressed()
	{
		//IL_0058: Expected F4, but got I4
		//IL_01d2: Invalid comparison between F4 and I4
		//IL_01fb: Expected O, but got I4
		//IL_0221: Invalid comparison between F4 and I4
		//IL_024a: Expected O, but got I4
		//IL_04df: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e4: Expected I4, but got Unknown
		//IL_0354: Expected F4, but got I4
		//IL_0513: Invalid comparison between I4 and F4
		//IL_0309: Invalid comparison between I4 and F4
		//IL_0390: Expected F4, but got I4
		float num;
		if (rangeDial != null)
		{
			DialInteractable dialInteractable = rangeDial;
			num = dialInteractable.accumulatedValue;
		}
		else
		{
			num = 0f;
		}
		if (gateCalculateByMinimumRange && minDesiredRangeToEnableCalculate > num)
		{
			UpdateCalculateButtonState(requestedActive: true);
			return;
		}
		int num3;
		if (powderChargeDial != null)
		{
			DialInteractable dialInteractable2 = powderChargeDial;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803B9370");
			int num2 = default(int);
			if (num2 >= 1)
			{
				bool flag = num2 <= 6;
				num3 = num2;
				if (!flag)
				{
					num3 = 6;
				}
				goto IL_0136;
			}
		}
		num3 = 1;
		goto IL_0136;
		IL_0136:
		float num8;
		bool wasClamped;
		float num11;
		string reason;
		if (shellBlueprint != null)
		{
			shellBlueprint.GetRangeForCharge(num3, out var minRange, out var maxRange);
			if (minRange < maxRange)
			{
				float num4 = minRange - rangeTolerance;
				float num5 = maxRange + rangeTolerance;
				bool flag2 = num4 < num;
				float num6 = num4 - num;
				bool flag3 = num6 == 0f;
				bool flag4 = !flag2;
				bool flag5 = !flag3;
				object obj = flag5 & flag4;
				bool flag6 = num < num5;
				float num7 = num - num5;
				bool flag7 = num7 == 0f;
				bool flag8 = !flag6;
				bool flag9 = !flag7;
				object obj2 = flag9 & flag8;
				object obj3 = obj | obj2;
				if (num == num5)
				{
					if (!(minRange > num))
					{
						bool flag10 = !(num > maxRange);
						num8 = num;
						if (!flag10)
						{
							num8 = maxRange;
						}
					}
					else
					{
						num8 = minRange;
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18038AA80");
					object obj4 = default(object);
					wasClamped = (byte)(obj4 ^ 1) != 0;
					bool flag11 = minRange == maxRange;
					Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"jp short 00000001803F37FBh\"");
					if (!flag11)
					{
						float num9 = maxRange - minRange;
						float num10 = num8 - minRange;
						num11 = num10 / num9;
						if (!(0f > num11))
						{
							if (num11 > 1f)
							{
								num11 = 1f;
							}
							goto IL_050a;
						}
					}
					num11 = 0f;
					goto IL_050a;
				}
				reason = "OutOfRange";
			}
			else
			{
				reason = "InvalidRangeBand";
			}
		}
		else
		{
			reason = "NoBlueprint";
		}
		InvokeCalculationError(num, num3, reason);
		return;
		IL_050a:
		if (!(0f > num11))
		{
			if (num11 > 1f)
			{
				num11 = 1f;
			}
		}
		else
		{
			num11 = 0f;
		}
		float num12 = maxElevation - minElevation;
		float num13 = num12 * num11;
		float num14 = num13 + minElevation;
		if (elevationOdometer != null)
		{
			OdometerDisplay odometerDisplay = elevationOdometer;
			odometerDisplay.targetNumber = num14;
		}
		lastInputRange = num;
		lastInputCharge = num3;
		waitingForCalculation = false;
		UpdateCalculateButtonState(requestedActive: false);
		lastValidElevation = num14;
		errorActive = false;
		CancelSuccessDelayIfRunning();
		if (OnCalculationSuccess != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180006580");
		}
		if (OnCalculationSuccessWithDelay != null)
		{
			_003CInvokeSuccessWithDelay_003Ed__44 obj5 = new _003CInvokeSuccessWithDelay_003Ed__44(0);
			obj5._003C_003E1__state = 0;
			obj5._003C_003E4__this = this;
			obj5.elevation = num14;
			obj5.clampedRange = num8;
			obj5.delaySeconds = successDelaySeconds;
			obj5.inputCharge = num3;
			obj5.wasClamped = wasClamped;
			Coroutine coroutine = StartCoroutine(obj5);
			successDelayRoutine = coroutine;
		}
	}

	public void ResetCalculationGate()
	{
		waitingForCalculation = true;
		UpdateCalculateButtonState(requestedActive: true);
		if (successDelayRoutine != null)
		{
			StopCoroutine(successDelayRoutine);
			successDelayRoutine = null;
		}
	}

	private void InvokeCalculationError(float attemptedRange, int charge, string reason)
	{
		waitingForCalculation = true;
		UpdateCalculateButtonState(requestedActive: true);
		bool flag = successDelayRoutine == null;
		errorActive = true;
		if (!flag)
		{
			StopCoroutine(successDelayRoutine);
			successDelayRoutine = null;
		}
		if (errorRandomImmediateFirstTick)
		{
			nextErrorRandomTime = 0f;
		}
		if (OnCalculationError != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180941A60");
		}
	}

	private IEnumerator InvokeSuccessWithDelay(float elevation, float clampedRange, int inputCharge, bool wasClamped, float delaySeconds)
	{
		_003CInvokeSuccessWithDelay_003Ed__44 obj = new _003CInvokeSuccessWithDelay_003Ed__44(0);
		obj._003C_003E1__state = 0;
		obj._003C_003E4__this = this;
		obj.elevation = elevation;
		obj.clampedRange = clampedRange;
		obj.inputCharge = inputCharge;
		float delaySeconds2 = default(float);
		obj.delaySeconds = delaySeconds2;
		bool wasClamped2 = default(bool);
		obj.wasClamped = wasClamped2;
		return obj;
	}

	private void CancelSuccessDelayIfRunning()
	{
		if (successDelayRoutine != null)
		{
			StopCoroutine(successDelayRoutine);
			successDelayRoutine = null;
		}
	}
}
