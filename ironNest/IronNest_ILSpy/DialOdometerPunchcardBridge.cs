using System;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.Events;

public class DialOdometerPunchcardBridge : MonoBehaviour
{
	[Serializable]
	public class FloatEvent : UnityEvent<float>
	{
	}

	private DialInteractable bearingDial;

	private DialInteractable distanceDial;

	private OdometerDisplay bearingOdometer;

	private OdometerDisplay distanceOdometer;

	public FloatEvent bearingPunchcardSetFloat;

	public FloatEvent distancePunchcardSetFloat;

	private bool wrapBearing = true;

	private float bearingMin;

	private float bearingMax = 360f;

	private float bearingRoundStep;

	private bool clampDistanceMin = true;

	private float distanceMin;

	private bool clampDistanceMax;

	private float distanceMax = 999f;

	private float distanceRoundStep;

	private float changeEpsilon = 0.0001f;

	private float bearing;

	private float distance;

	private bool _subscribed;

	public float Bearing => bearing;

	public float Distance => distance;

	private void OnEnable()
	{
		//IL_0155: Expected O, but got I4
		//IL_0073: Expected O, but got I4
		//IL_00e6: Expected O, but got I4
		//IL_02ea: Invalid comparison between F4 and I4
		if (!_subscribed)
		{
			if (bearingDial != null)
			{
				DialInteractable dialInteractable = bearingDial;
				UnityAction<float> call = HandleBearingDialValueChanged;
				dialInteractable.OnValueChanged.AddListener(call);
				object obj = 0;
			}
			if (distanceDial != null)
			{
				DialInteractable dialInteractable2 = distanceDial;
				UnityAction<float> call2 = HandleDistanceDialValueChanged;
				dialInteractable2.OnValueChanged.AddListener(call2);
				object obj = 0;
			}
			_subscribed = true;
		}
		if (!(bearingDial != null))
		{
			ApplyBearingOutputs(force: true);
		}
		else
		{
			DialInteractable dialInteractable3 = bearingDial;
			SetBearingInternal(dialInteractable3.accumulatedValue, force: true);
			object obj = 0;
		}
		if (distanceDial != null)
		{
			DialInteractable dialInteractable4 = distanceDial;
			bool flag = !clampDistanceMin;
			float num = dialInteractable4.accumulatedValue;
			if (!flag)
			{
				float accumulatedValue = distanceMin;
				if (distanceMin < dialInteractable4.accumulatedValue)
				{
					accumulatedValue = dialInteractable4.accumulatedValue;
				}
				num = accumulatedValue;
			}
			if (clampDistanceMax)
			{
				float num2 = distanceMax;
				if (distanceMax > num)
				{
					num2 = num;
				}
				num = num2;
			}
			if (distanceRoundStep > 0f)
			{
				float num3 = num / distanceRoundStep;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1800033E0");
				num = num3 * distanceRoundStep;
			}
			distance = num;
		}
		ApplyDistanceOutputs(force: true);
	}

	private void OnDisable()
	{
		if (_subscribed)
		{
			if (bearingDial != null)
			{
				DialInteractable dialInteractable = bearingDial;
				UnityAction<float> call = HandleBearingDialValueChanged;
				dialInteractable.OnValueChanged.RemoveListener(call);
			}
			if (distanceDial != null)
			{
				DialInteractable dialInteractable2 = distanceDial;
				UnityAction<float> call2 = HandleDistanceDialValueChanged;
				dialInteractable2.OnValueChanged.RemoveListener(call2);
			}
			_subscribed = false;
		}
	}

	private void OnValidate()
	{
		//IL_00c6: Invalid comparison between I4 and F4
		//IL_00a3: Invalid comparison between I4 and F4
		//IL_00e8: Invalid comparison between I4 and F4
		if (bearingMin > bearingMax)
		{
			bearingMax = bearingMin;
		}
		if (distanceMin > distanceMax)
		{
			distanceMax = distanceMin;
		}
		if (0f > changeEpsilon)
		{
			changeEpsilon = 0f;
		}
		if (0f > bearingRoundStep)
		{
			bearingRoundStep = 0f;
		}
		if (0f > distanceRoundStep)
		{
			distanceRoundStep = 0f;
		}
	}

	private void Subscribe()
	{
		if (!_subscribed)
		{
			if (bearingDial != null)
			{
				DialInteractable dialInteractable = bearingDial;
				UnityAction<float> call = HandleBearingDialValueChanged;
				dialInteractable.OnValueChanged.AddListener(call);
			}
			if (distanceDial != null)
			{
				DialInteractable dialInteractable2 = distanceDial;
				UnityAction<float> call2 = HandleDistanceDialValueChanged;
				dialInteractable2.OnValueChanged.AddListener(call2);
			}
			_subscribed = true;
		}
	}

	private void Unsubscribe()
	{
		if (_subscribed)
		{
			if (bearingDial != null)
			{
				DialInteractable dialInteractable = bearingDial;
				UnityAction<float> call = HandleBearingDialValueChanged;
				dialInteractable.OnValueChanged.RemoveListener(call);
			}
			if (distanceDial != null)
			{
				DialInteractable dialInteractable2 = distanceDial;
				UnityAction<float> call2 = HandleDistanceDialValueChanged;
				dialInteractable2.OnValueChanged.RemoveListener(call2);
			}
			_subscribed = false;
		}
	}

	public void ForceRefreshAll()
	{
		//IL_01c4: Invalid comparison between F4 and I4
		if (!(bearingDial != null))
		{
			ApplyBearingOutputs(force: true);
		}
		else
		{
			DialInteractable dialInteractable = bearingDial;
			SetBearingInternal(dialInteractable.accumulatedValue, force: true);
		}
		if (distanceDial != null)
		{
			DialInteractable dialInteractable2 = distanceDial;
			bool flag = !clampDistanceMin;
			float num = dialInteractable2.accumulatedValue;
			if (!flag)
			{
				float accumulatedValue = distanceMin;
				if (distanceMin < dialInteractable2.accumulatedValue)
				{
					accumulatedValue = dialInteractable2.accumulatedValue;
				}
				num = accumulatedValue;
			}
			if (clampDistanceMax)
			{
				float num2 = distanceMax;
				if (distanceMax > num)
				{
					num2 = num;
				}
				num = num2;
			}
			if (distanceRoundStep > 0f)
			{
				float num3 = num / distanceRoundStep;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1800033E0");
				num = num3 * distanceRoundStep;
			}
			distance = num;
		}
		ApplyDistanceOutputs(force: true);
	}

	private void HandleBearingDialValueChanged(float raw)
	{
		SetBearingInternal(raw, force: false);
	}

	private void HandleDistanceDialValueChanged(float raw)
	{
		//IL_0123: Invalid comparison between F4 and I4
		//IL_015b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0160: Expected O, but got Unknown
		//IL_016a: Invalid comparison between F4 and O
		bool flag = !clampDistanceMin;
		float num = raw;
		if (!flag)
		{
			float num2 = distanceMin;
			if (distanceMin < raw)
			{
				num2 = raw;
			}
			num = num2;
		}
		if (clampDistanceMax)
		{
			float num3 = distanceMax;
			if (distanceMax > num)
			{
				num3 = num;
			}
			num = num3;
		}
		if (distanceRoundStep > 0f)
		{
			float num4 = num / distanceRoundStep;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1800033E0");
			num = num4 * distanceRoundStep;
		}
		float num5 = num - distance;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
		object obj = num5 & 0;
		float num6 = changeEpsilon;
		if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)num6) <= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj))
		{
			distance = num;
			ApplyDistanceOutputs(force: true);
		}
	}

	private void SetBearingInternal(float raw, bool force)
	{
		//IL_01b3: Invalid comparison between F4 and I4
		//IL_00d6: Invalid comparison between I4 and F4
		//IL_016a: Unknown result type (might be due to invalid IL or missing references)
		//IL_016f: Expected O, but got Unknown
		//IL_0179: Invalid comparison between F4 and O
		float num = default(float);
		float num2;
		if (!wrapBearing)
		{
			if (bearingMin > num)
			{
				goto IL_0109;
			}
			bool flag = !(num > bearingMax);
			num2 = num;
			if (!flag)
			{
				num2 = bearingMax;
			}
		}
		else
		{
			float num3 = bearingMax - bearingMin;
			if (!(1E-06f < num3))
			{
				goto IL_0109;
			}
			float x = num - bearingMin;
			float num4 = MathF.FMod(x, num3);
			bool flag2 = !(0f > num4);
			float num5 = num4;
			if (!flag2)
			{
				num5 = num4 + num3;
			}
			num2 = num5 + bearingMin;
		}
		goto IL_01a8;
		IL_0109:
		num2 = bearingMin;
		goto IL_01a8;
		IL_01a8:
		if (bearingRoundStep > 0f)
		{
			float num6 = num2 / bearingRoundStep;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1800033E0");
			num2 = num6 * bearingRoundStep;
		}
		if (!force)
		{
			float num7 = num2 - bearing;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
			object obj = num7 & 0;
			float num8 = changeEpsilon;
			if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)num8) > System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj))
			{
				return;
			}
		}
		bearing = num2;
		ApplyBearingOutputs(force: true);
	}

	private void SetDistanceInternal(float raw, bool force)
	{
		//IL_016b: Invalid comparison between F4 and I4
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Expected O, but got Unknown
		//IL_00f8: Invalid comparison between F4 and O
		bool flag = !clampDistanceMin;
		float num = raw;
		if (!flag)
		{
			float num2 = distanceMin;
			if (distanceMin < raw)
			{
				num2 = raw;
			}
			num = num2;
		}
		if (clampDistanceMax)
		{
			float num3 = distanceMax;
			if (distanceMax > num)
			{
				num3 = num;
			}
			num = num3;
		}
		if (distanceRoundStep > 0f)
		{
			float num4 = num / distanceRoundStep;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1800033E0");
			num = num4 * distanceRoundStep;
		}
		if (!force)
		{
			float num5 = num - distance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
			object obj = num5 & 0;
			float num6 = changeEpsilon;
			if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)num6) > System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj))
			{
				return;
			}
		}
		distance = num;
		ApplyDistanceOutputs(force: true);
	}

	private float ProcessBearing(float value)
	{
		//IL_0161: Invalid comparison between F4 and I4
		//IL_00d6: Invalid comparison between I4 and F4
		float num = default(float);
		float num2;
		if (!wrapBearing)
		{
			if (bearingMin > num)
			{
				goto IL_0109;
			}
			bool flag = !(num > bearingMax);
			num2 = num;
			if (!flag)
			{
				num2 = bearingMax;
			}
		}
		else
		{
			float num3 = bearingMax - bearingMin;
			if (!(1E-06f < num3))
			{
				goto IL_0109;
			}
			float x = num - bearingMin;
			float num4 = MathF.FMod(x, num3);
			bool flag2 = !(0f > num4);
			float num5 = num4;
			if (!flag2)
			{
				num5 = num4 + num3;
			}
			num2 = num5 + bearingMin;
		}
		goto IL_0156;
		IL_0109:
		num2 = bearingMin;
		goto IL_0156;
		IL_0156:
		if (bearingRoundStep > 0f)
		{
			float num6 = num2 / bearingRoundStep;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1800033E0");
			return num6 * bearingRoundStep;
		}
		return num2;
	}

	private float ProcessDistance(float value)
	{
		//IL_0111: Invalid comparison between F4 and I4
		bool flag = !clampDistanceMin;
		float num = value;
		if (!flag)
		{
			float num2 = distanceMin;
			if (distanceMin < value)
			{
				num2 = value;
			}
			num = num2;
		}
		if (clampDistanceMax)
		{
			float num3 = distanceMax;
			if (distanceMax > num)
			{
				num3 = num;
			}
			num = num3;
		}
		if (distanceRoundStep > 0f)
		{
			float num4 = num / distanceRoundStep;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1800033E0");
			return num4 * distanceRoundStep;
		}
		return num;
	}

	private unsafe void ApplyBearingOutputs(bool force)
	{
		//IL_0060: Expected F4, but got Ref
		if (bearingOdometer != null)
		{
			OdometerDisplay odometerDisplay = bearingOdometer;
			odometerDisplay.targetNumber = bearing;
		}
		if (bearingPunchcardSetFloat != null)
		{
			object obj = default(object);
			bearingPunchcardSetFloat.Invoke((nint)(&obj));
		}
	}

	private unsafe void ApplyDistanceOutputs(bool force)
	{
		//IL_0060: Expected F4, but got Ref
		if (distanceOdometer != null)
		{
			OdometerDisplay odometerDisplay = distanceOdometer;
			odometerDisplay.targetNumber = distance;
		}
		if (distancePunchcardSetFloat != null)
		{
			object obj = default(object);
			distancePunchcardSetFloat.Invoke((nint)(&obj));
		}
	}
}
