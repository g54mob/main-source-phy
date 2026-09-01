using UnityEngine;

public class InterpolatedTransformController : MonoBehaviour
{
	public enum TriggerMode
	{
		Off,
		Triggered
	}

	public enum TriggerDirection
	{
		ToEnd_0To1,
		ToStart_1To0,
		Toggle
	}

	public enum EaseMode
	{
		Linear,
		SmoothStep
	}

	public Transform targetObject;

	public float interpolant;

	public Vector3 startLocalPosition;

	public Vector3 startLocalEulerAngles;

	public Vector3 endLocalPosition;

	public Vector3 endLocalEulerAngles;

	public TriggerMode triggerMode;

	public TriggerDirection defaultTriggerDirection = TriggerDirection.Toggle;

	public float triggeredDurationSeconds = 0.25f;

	public EaseMode triggeredEase = EaseMode.SmoothStep;

	public bool useUnscaledTime;

	public bool allowRetriggerWhilePlaying = true;

	private bool _isTriggerPlaying;

	private float _triggerStartInterpolant;

	private float _triggerTargetInterpolant;

	private float _triggerElapsed;

	private float _lastTriggerTarget = 1f;

	private void Reset()
	{
		//IL_004c: Expected O, but got F4
		//IL_0079: Expected O, but got F4
		//IL_00a6: Expected O, but got F4
		//IL_00d3: Expected O, but got F4
		if (targetObject != null)
		{
			Vector3 localPosition = targetObject.localPosition;
			startLocalPosition = (Vector3)localPosition.x;
			_ = localPosition.z;
			Vector3 localEulerAngles = targetObject.localEulerAngles;
			startLocalEulerAngles = (Vector3)localEulerAngles.x;
			_ = localEulerAngles.z;
			Vector3 localPosition2 = targetObject.localPosition;
			endLocalPosition = (Vector3)localPosition2.x;
			_ = localPosition2.z;
			Vector3 localEulerAngles2 = targetObject.localEulerAngles;
			endLocalEulerAngles = (Vector3)localEulerAngles2.x;
			_ = localEulerAngles2.z;
		}
		triggerMode = TriggerMode.Off;
		defaultTriggerDirection = TriggerDirection.Toggle;
		triggeredDurationSeconds = 0.25f;
		triggeredEase = EaseMode.SmoothStep;
		useUnscaledTime = false;
	}

	private unsafe void Update()
	{
		//IL_0254: Invalid comparison between I4 and F4
		//IL_029e: Expected O, but got Ref
		//IL_02aa: Invalid comparison between I4 and F4
		//IL_006a: Invalid comparison between I4 and F4
		//IL_02f4: Expected O, but got Ref
		//IL_00b6: Invalid comparison between I4 and F4
		//IL_0101: Expected F4, but got I4
		//IL_032a: Invalid comparison between I4 and F4
		//IL_0147: Expected F4, but got I4
		//IL_0366: Invalid comparison between I4 and F4
		//IL_01fe: Expected F4, but got I4
		//IL_0177: Invalid comparison between I4 and F4
		//IL_01c2: Expected F4, but got I4
		if (triggerMode == TriggerMode.Triggered && _isTriggerPlaying)
		{
			float num = ((!useUnscaledTime) ? Time.deltaTime : Time.unscaledDeltaTime);
			if (0f < triggeredDurationSeconds)
			{
				float num2 = (_triggerElapsed = num + _triggerElapsed) / triggeredDurationSeconds;
				if (!(0f > num2))
				{
					if (num2 > 1f)
					{
						num2 = 1f;
					}
				}
				else
				{
					num2 = 0f;
				}
				float num3 = ((0f > num2) ? 0f : ((num2 > 1f) ? 1f : num2));
				if (triggeredEase != EaseMode.Linear && triggeredEase == EaseMode.SmoothStep)
				{
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
					float num4 = num3 * -2f;
					float num5 = num3 * 3f;
					float num6 = num4 * num3;
					float num7 = num5 * num3;
					float num8 = num6 * num3;
					float num9 = num8 + num7;
					float num10 = 1f - num9;
					float num11 = num10 * 0f;
					num3 = num11 + num9;
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
				float num12 = _triggerTargetInterpolant - _triggerStartInterpolant;
				float num13 = num12 * num3;
				float num14 = num13 + _triggerStartInterpolant;
				interpolant = num14;
				if (num2 < 1f)
				{
					goto IL_021f;
				}
			}
			interpolant = _triggerTargetInterpolant;
			_isTriggerPlaying = false;
		}
		goto IL_021f;
		IL_021f:
		if (targetObject != null)
		{
			if (0f > interpolant || interpolant > 1f)
			{
			}
			Vector3 euler = default(Vector3);
			targetObject.localPosition = (Vector3)(&euler);
			if (0f > interpolant || interpolant > 1f)
			{
			}
			Quaternion quaternion = Quaternion.Internal_FromEulerRad(ref euler);
			targetObject.localRotation = (Quaternion)(&euler);
		}
	}

	private static float ApplyEase01(float t01, EaseMode ease)
	{
		//IL_0009: Invalid comparison between I4 and F4
		//IL_0054: Expected F4, but got I4
		//IL_0082: Invalid comparison between I4 and F4
		//IL_00cd: Expected F4, but got I4
		float num = default(float);
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
		if (ease != EaseMode.Linear && ease == EaseMode.SmoothStep)
		{
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
			float num2 = num * -2f;
			float num3 = num * 3f;
			float num4 = num2 * num;
			float num5 = num3 * num;
			float num6 = num4 * num;
			float num7 = num6 + num5;
			float num8 = 1f - num7;
			float num9 = num8 * 0f;
			float num10 = num9 + num7;
			num = num10;
		}
		return num;
	}

	public void TriggerDefaultDirection()
	{
		Trigger(defaultTriggerDirection);
	}

	public void TriggerToEnd()
	{
		//IL_0075: Invalid comparison between I4 and F4
		//IL_00c0: Expected F4, but got I4
		if (triggerMode != TriggerMode.Triggered || (_isTriggerPlaying && !allowRetriggerWhilePlaying))
		{
			return;
		}
		float num = interpolant;
		if (!(0f > interpolant))
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
		_triggerStartInterpolant = num;
		_triggerTargetInterpolant = 1f;
		_isTriggerPlaying = true;
		_lastTriggerTarget = 1f;
	}

	public void TriggerToStart()
	{
		//IL_0075: Invalid comparison between I4 and F4
		//IL_00df: Expected F4, but got I4
		if (triggerMode != TriggerMode.Triggered || (_isTriggerPlaying && !allowRetriggerWhilePlaying))
		{
			return;
		}
		float num = interpolant;
		if (!(0f > interpolant))
		{
			if (num > 1f)
			{
				_triggerStartInterpolant = 1f;
				_isTriggerPlaying = true;
				_triggerTargetInterpolant = 0f;
				_lastTriggerTarget = 0f;
				return;
			}
		}
		else
		{
			num = 0f;
		}
		_triggerStartInterpolant = num;
		_isTriggerPlaying = true;
		_triggerTargetInterpolant = 0f;
		_lastTriggerTarget = 0f;
	}

	public void TriggerToggle()
	{
		//IL_0075: Invalid comparison between I4 and F4
		//IL_013e: Invalid comparison between I4 and F4
		//IL_019b: Expected F4, but got I4
		//IL_01f0: Invalid comparison between I4 and F4
		//IL_0130: Expected F4, but got I4
		//IL_01d7: Expected F4, but got I4
		//IL_0114: Expected F4, but got I4
		if (triggerMode != TriggerMode.Triggered || (_isTriggerPlaying && !allowRetriggerWhilePlaying))
		{
			return;
		}
		float num = interpolant;
		if (0f > interpolant)
		{
			goto IL_0135;
		}
		float num2;
		if (!(interpolant > 1f))
		{
			if (!(0.4999f < interpolant))
			{
				goto IL_0135;
			}
			if (interpolant < 0.5001f)
			{
				num2 = ((_lastTriggerTarget < 0.5f) ? 1f : 0f);
				goto IL_01dd;
			}
		}
		num2 = 0f;
		goto IL_015b;
		IL_01dd:
		_triggerStartInterpolant = num;
		if (!(0f > num2))
		{
			if (num2 > 1f)
			{
				num2 = 1f;
			}
		}
		else
		{
			num2 = 0f;
		}
		_triggerTargetInterpolant = num2;
		_lastTriggerTarget = num2;
		_triggerElapsed = 0f;
		_isTriggerPlaying = true;
		return;
		IL_0135:
		bool flag = 0f > num;
		num2 = 1f;
		if (!flag)
		{
			goto IL_015b;
		}
		num2 = 1f;
		num = 0f;
		goto IL_01dd;
		IL_015b:
		if (num > 1f)
		{
			num = 1f;
		}
		goto IL_01dd;
	}

	public void StopTriggeredInterpolation()
	{
		_isTriggerPlaying = false;
	}

	public void Trigger(TriggerDirection direction)
	{
		//IL_0240: Invalid comparison between I4 and F4
		//IL_01c2: Expected F4, but got I4
		//IL_0178: Expected F4, but got I4
		//IL_0217: Invalid comparison between I4 and F4
		//IL_00af: Invalid comparison between I4 and F4
		//IL_01fe: Expected F4, but got I4
		//IL_016a: Expected F4, but got I4
		//IL_014e: Expected F4, but got I4
		if (triggerMode != TriggerMode.Triggered || (_isTriggerPlaying && !allowRetriggerWhilePlaying))
		{
			return;
		}
		float num;
		float num2;
		if (direction != TriggerDirection.ToEnd_0To1)
		{
			if (direction == TriggerDirection.ToStart_1To0)
			{
				num = 0f;
				goto IL_022b;
			}
			num2 = interpolant;
			if (!(0f > interpolant))
			{
				if (!(interpolant > 1f))
				{
					if (!(0.4999f < interpolant))
					{
						goto IL_017d;
					}
					if (interpolant < 0.5001f)
					{
						num = ((_lastTriggerTarget < 0.5f) ? 1f : 0f);
						goto IL_0204;
					}
				}
				num = 0f;
				goto IL_018b;
			}
		}
		goto IL_017d;
		IL_022b:
		num2 = interpolant;
		if (0f > interpolant)
		{
			num2 = 0f;
			goto IL_0204;
		}
		goto IL_018b;
		IL_0204:
		_triggerStartInterpolant = num2;
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
		_triggerTargetInterpolant = num;
		_lastTriggerTarget = num;
		_triggerElapsed = 0f;
		_isTriggerPlaying = true;
		return;
		IL_017d:
		num = 1f;
		goto IL_022b;
		IL_018b:
		if (num2 > 1f)
		{
			num2 = 1f;
		}
		goto IL_0204;
	}

	public void SetInterpolantImmediate(float value01)
	{
		//IL_0014: Invalid comparison between I4 and F4
		//IL_0023: Expected F4, but got I4
		_isTriggerPlaying = false;
		bool flag = 0f > value01;
		float num = 0f;
		if (!flag)
		{
			bool flag2 = value01 > 1f;
			num = 1f;
			if (!flag2)
			{
				interpolant = value01;
				return;
			}
		}
		interpolant = num;
	}

	private float ResolveTarget(TriggerDirection direction)
	{
		//IL_00e1: Expected F4, but got I4
		//IL_0045: Invalid comparison between I4 and F4
		if (direction != TriggerDirection.ToEnd_0To1 && (direction == TriggerDirection.ToStart_1To0 || (!(0f > interpolant) && (interpolant > 1f || (0.4999f < interpolant && (!(interpolant < 0.5001f) || !(_lastTriggerTarget < 0.5f)))))))
		{
			return 0f;
		}
		return 1f;
	}

	public void SetStartToCurrent()
	{
		//IL_004c: Expected O, but got F4
		//IL_0079: Expected O, but got F4
		if (targetObject != null)
		{
			Vector3 localPosition = targetObject.localPosition;
			startLocalPosition = (Vector3)localPosition.x;
			_ = localPosition.z;
			Vector3 localEulerAngles = targetObject.localEulerAngles;
			startLocalEulerAngles = (Vector3)localEulerAngles.x;
			_ = localEulerAngles.z;
		}
	}

	public void SetEndToCurrent()
	{
		//IL_004c: Expected O, but got F4
		//IL_0079: Expected O, but got F4
		if (targetObject != null)
		{
			Vector3 localPosition = targetObject.localPosition;
			endLocalPosition = (Vector3)localPosition.x;
			_ = localPosition.z;
			Vector3 localEulerAngles = targetObject.localEulerAngles;
			endLocalEulerAngles = (Vector3)localEulerAngles.x;
			_ = localEulerAngles.z;
		}
	}
}
