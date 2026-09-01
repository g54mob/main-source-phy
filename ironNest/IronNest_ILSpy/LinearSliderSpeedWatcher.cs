using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.Events;

public class LinearSliderSpeedWatcher : MonoBehaviour
{
	private LinearSliderInteractable slider;

	private bool speedBasedOnSliderValue = true;

	private bool trackWhenNotDragging;

	private float minDeltaTime = 0.005f;

	private float smoothingTime = 0.05f;

	private bool useUnscaledTime = true;

	private bool requireDraggingForHold;

	private float holdSpeedMinInclusive;

	private float holdSpeedMaxInclusive = 2f;

	private float holdDurationSeconds = 0.5f;

	private bool fireOncePerRangeEntry = true;

	private bool resetHoldTimerWhenOutOfRange;

	private float currentSpeed;

	private float heldInRangeTime;

	private bool isSpeedInRange;

	private bool hasFiredThisSession;

	public UnityEvent<float> OnSpeedHeldInRange;

	private float _prevSample;

	private bool _hasPrevSample;

	private float _smoothVelocity;

	private float _rawSpeed;

	public float CurrentSpeed => currentSpeed;

	public float HeldInRangeTime => heldInRangeTime;

	private void Reset()
	{
		AutoAssignSliderIfNeeded();
	}

	private void Awake()
	{
		AutoAssignSliderIfNeeded();
		InitializeSampling();
	}

	private void OnEnable()
	{
		InitializeSampling();
		heldInRangeTime = 0f;
		isSpeedInRange = false;
	}

	private void AutoAssignSliderIfNeeded()
	{
		if (!(slider == null))
		{
			return;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
		LinearSliderInteractable linearSliderInteractable = default(LinearSliderInteractable);
		slider = linearSliderInteractable;
		if (slider == null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696290");
			slider = linearSliderInteractable;
			if (slider == null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696530");
				slider = linearSliderInteractable;
			}
		}
	}

	private void InitializeSampling()
	{
		if (slider != null)
		{
			LinearSliderInteractable linearSliderInteractable = slider;
			if (speedBasedOnSliderValue)
			{
				_rawSpeed = 0f;
				currentSpeed = 0f;
				_prevSample = linearSliderInteractable.accumulatedValue;
				_hasPrevSample = true;
			}
			else
			{
				_rawSpeed = 0f;
				currentSpeed = 0f;
				_prevSample = linearSliderInteractable.currentDistance;
				_hasPrevSample = true;
			}
		}
		else
		{
			_hasPrevSample = false;
			_prevSample = 0f;
			_rawSpeed = 0f;
			currentSpeed = 0f;
		}
	}

	private unsafe void Update()
	{
		//IL_012b: Expected F4, but got I4
		//IL_0476: Unknown result type (might be due to invalid IL or missing references)
		//IL_047b: Expected O, but got Unknown
		//IL_04a9: Invalid comparison between I4 and F4
		//IL_05a8: Invalid comparison between I4 and F4
		//IL_05ba: Expected F4, but got I4
		//IL_0174: Invalid comparison between F4 and I4
		//IL_05e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ec: Expected O, but got Unknown
		//IL_060f: Invalid comparison between I4 and F4
		//IL_019c: Expected F4, but got I4
		//IL_01e7: Expected F4, but got I4
		//IL_02f3: Invalid comparison between I4 and F4
		//IL_0305: Expected F4, but got I4
		//IL_02c8: Expected F4, but got I4
		//IL_039a: Expected F4, but got Ref
		if (!(slider != null))
		{
			currentSpeed = 0f;
			_rawSpeed = 0f;
			isSpeedInRange = false;
			return;
		}
		float num = ((!useUnscaledTime) ? Time.deltaTime : Time.unscaledDeltaTime);
		bool flag = !(num < minDeltaTime);
		float num2 = num;
		if (!flag)
		{
			num2 = minDeltaTime;
		}
		bool flag2 = !trackWhenNotDragging;
		LinearSliderInteractable linearSliderInteractable = slider;
		bool flag3;
		bool flag4;
		if (!flag2)
		{
			flag3 = speedBasedOnSliderValue;
			flag4 = true;
		}
		else
		{
			flag4 = linearSliderInteractable.isDragging;
			flag3 = speedBasedOnSliderValue;
		}
		float num3 = ((!flag3) ? linearSliderInteractable.currentDistance : linearSliderInteractable.accumulatedValue);
		if (!_hasPrevSample)
		{
			_prevSample = num3;
			currentSpeed = 0f;
			_rawSpeed = 0f;
			_hasPrevSample = true;
			UpdateHoldState(num2, 0f);
			return;
		}
		float num4 = ((!flag4) ? 0f : (num3 - _prevSample));
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
		object obj = num4 & 0;
		float num5 = (float)obj / num2;
		_prevSample = num3;
		_rawSpeed = num5;
		if (0f < smoothingTime)
		{
			float num6 = ((!useUnscaledTime) ? Time.deltaTime : Time.unscaledDeltaTime);
			bool flag5 = !(num6 < 0f);
			float num7 = num6;
			if (!flag5)
			{
				num7 = 0f;
			}
			bool flag6 = !(0.0001f < smoothingTime);
			float num8 = 0.0001f;
			if (!flag6)
			{
				num8 = smoothingTime;
			}
			float num9 = num7 / num8;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C80]");
			object obj2 = num9 ^ 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033F3D0");
			float num10 = 1f - (float)obj2;
			if (!(0f > num10))
			{
				if (num10 > 1f)
				{
					num10 = 1f;
				}
			}
			else
			{
				num10 = 0f;
			}
			float num11 = _rawSpeed - currentSpeed;
			float num12 = num11 * num10;
			num5 = num12 + currentSpeed;
		}
		bool flag7 = !(0f < num5);
		float num13 = 0f;
		if (!flag7)
		{
			num13 = num5;
		}
		currentSpeed = num13;
		float num14 = holdSpeedMinInclusive;
		bool flag8 = !(holdSpeedMinInclusive > holdSpeedMaxInclusive);
		float num15 = holdSpeedMaxInclusive;
		if (!flag8)
		{
			num15 = holdSpeedMinInclusive;
			num14 = holdSpeedMaxInclusive;
		}
		if (requireDraggingForHold)
		{
			LinearSliderInteractable linearSliderInteractable2 = slider;
			if (!linearSliderInteractable2.isDragging)
			{
				goto IL_0262;
			}
		}
		if (num13 < num14)
		{
			goto IL_0262;
		}
		bool flag9 = num15 < num13;
		bool flag10 = !flag9;
		bool flag11 = false;
		goto IL_055a;
		IL_0262:
		flag11 = false;
		flag10 = false;
		goto IL_055a;
		IL_055a:
		isSpeedInRange = flag10;
		if (!flag10)
		{
			if (resetHoldTimerWhenOutOfRange != flag10)
			{
				heldInRangeTime = (flag11 ? 1 : 0);
			}
			hasFiredThisSession = false;
			return;
		}
		float num16 = (heldInRangeTime = num2 + heldInRangeTime);
		bool flag12 = !(0f < holdDurationSeconds);
		float num17 = 0f;
		if (!flag12)
		{
			num17 = holdDurationSeconds;
		}
		bool flag13 = num16 < num17;
		if (!flag13 && (fireOncePerRangeEntry == flag13 || hasFiredThisSession == flag13))
		{
			hasFiredThisSession = true;
			if (OnSpeedHeldInRange != null)
			{
				object obj3 = default(object);
				OnSpeedHeldInRange.Invoke((nint)(&obj3));
			}
		}
	}

	private float GetCurrentSample()
	{
		LinearSliderInteractable linearSliderInteractable = slider;
		if (speedBasedOnSliderValue)
		{
			return linearSliderInteractable.accumulatedValue;
		}
		return linearSliderInteractable.currentDistance;
	}

	private unsafe void UpdateHoldState(float dt, float speed)
	{
		//IL_00ff: Invalid comparison between I4 and F4
		//IL_0111: Expected F4, but got I4
		//IL_00d4: Expected F4, but got I4
		//IL_01a6: Expected F4, but got Ref
		float num = holdSpeedMinInclusive;
		bool flag = !(holdSpeedMinInclusive > holdSpeedMaxInclusive);
		float num2 = holdSpeedMaxInclusive;
		if (!flag)
		{
			num2 = holdSpeedMinInclusive;
			num = holdSpeedMaxInclusive;
		}
		if (requireDraggingForHold)
		{
			LinearSliderInteractable linearSliderInteractable = slider;
			if (!linearSliderInteractable.isDragging)
			{
				goto IL_006e;
			}
		}
		if (speed < num)
		{
			goto IL_006e;
		}
		bool flag2 = num2 < speed;
		bool flag3 = !flag2;
		bool flag4 = false;
		goto IL_01e3;
		IL_01e3:
		isSpeedInRange = flag3;
		if (!flag3)
		{
			if (resetHoldTimerWhenOutOfRange != flag3)
			{
				heldInRangeTime = (flag4 ? 1 : 0);
			}
			hasFiredThisSession = false;
			return;
		}
		float num3 = (heldInRangeTime = dt + heldInRangeTime);
		bool flag5 = !(0f < holdDurationSeconds);
		float num4 = 0f;
		if (!flag5)
		{
			num4 = holdDurationSeconds;
		}
		bool flag6 = num3 < num4;
		if (!flag6 && (fireOncePerRangeEntry == flag6 || hasFiredThisSession == flag6))
		{
			hasFiredThisSession = true;
			if (OnSpeedHeldInRange != null)
			{
				object obj = default(object);
				OnSpeedHeldInRange.Invoke((nint)(&obj));
			}
		}
		return;
		IL_006e:
		flag4 = false;
		flag3 = false;
		goto IL_01e3;
	}
}
