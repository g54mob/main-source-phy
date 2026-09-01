using Cpp2ILInjected;
using UnityEngine;

public class CounterBatteryTimerFMODBridge : MonoBehaviour, IFloatValueProvider
{
	private CounterBatteryTimer timerSource;

	private AnimationCurve responseCurve;

	private bool outputLastValueWhenStopped;

	private bool zeroOutputBeforeTimerStarts;

	private bool verbose;

	private float inspectorNormalisedTimeRemaining;

	private float inspectorCurveOutput;

	private float _lastOutput;

	private void Awake()
	{
		ResolveTimerSource();
	}

	private void Update()
	{
		float num = EvaluateCurve();
	}

	public float GetFloatValue()
	{
		return EvaluateCurve();
	}

	private void ResolveTimerSource()
	{
		if (!(timerSource == null))
		{
			return;
		}
		timerSource = CounterBatteryTimer._003CInstance_003Ek__BackingField;
		bool flag = timerSource != null;
		if (verbose)
		{
			if (flag)
			{
				GameObject gameObject = timerSource.gameObject;
				string text = gameObject.name;
				string message = "[CounterBatteryTimerFMODBridge] Resolved timer source via singleton: " + text;
				Debug.Log(message, this);
			}
			else
			{
				Debug.LogWarning("[CounterBatteryTimerFMODBridge] No timer source assigned and CounterBatteryTimer.Instance is null. Bridge will output 0 until a timer becomes available.", this);
			}
		}
	}

	private float EvaluateCurve()
	{
		//IL_0110: Expected F4, but got I4
		//IL_031e: Invalid comparison between F4 and I4
		//IL_02a7: Expected F4, but got I4
		//IL_025c: Invalid comparison between I4 and F4
		//IL_01fb: Invalid comparison between I4 and F8
		//IL_020d: Expected F4, but got I4
		if (timerSource == null)
		{
			ResolveTimerSource();
			if (!(timerSource != null))
			{
				goto IL_00f4;
			}
		}
		CounterBatteryTimer counterBatteryTimer = timerSource;
		if (!counterBatteryTimer._running && !counterBatteryTimer._expired)
		{
			if (counterBatteryTimer._permanentlyStopped)
			{
				goto IL_0132;
			}
			if (zeroOutputBeforeTimerStarts)
			{
				goto IL_00f4;
			}
		}
		if (counterBatteryTimer._permanentlyStopped)
		{
			goto IL_0132;
		}
		goto IL_0164;
		IL_0132:
		if (outputLastValueWhenStopped)
		{
			inspectorCurveOutput = _lastOutput;
			return _lastOutput;
		}
		goto IL_0164;
		IL_02e7:
		bool flag = responseCurve == null;
		float num;
		inspectorNormalisedTimeRemaining = num;
		if (!flag)
		{
			num = responseCurve.Evaluate(num);
		}
		_lastOutput = num;
		inspectorCurveOutput = num;
		return num;
		IL_0164:
		CounterBatteryTimer counterBatteryTimer2 = timerSource;
		float num2;
		if (counterBatteryTimer2._running && !counterBatteryTimer2._expired && !counterBatteryTimer2._permanentlyStopped)
		{
			double timeAsDouble = Time.timeAsDouble;
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"subsd xmm6,xmm0\"");
			bool flag2 = !(0.0 < counterBatteryTimer2.endTime);
			num2 = 0f;
			if (!flag2)
			{
				num2 = (float)counterBatteryTimer2.endTime;
			}
		}
		else
		{
			num2 = counterBatteryTimer2._remainingSeconds;
		}
		if (counterBatteryTimer2.totalDurationSeconds > 0f)
		{
			num = num2 / counterBatteryTimer2.totalDurationSeconds;
			if (!(0f > num))
			{
				if (num > 1f)
				{
					num = 1f;
				}
				goto IL_02e7;
			}
		}
		num = 0f;
		goto IL_02e7;
		IL_00f4:
		inspectorCurveOutput = 0f;
		inspectorNormalisedTimeRemaining = 0f;
		return 0f;
	}

	public CounterBatteryTimerFMODBridge()
	{
		Keyframe[] keys = new Keyframe[2];
		float outTangent = default(float);
		Keyframe keyframe = new Keyframe(0f, 1f, -1f, outTangent);
		_ = 0;
		_ = 0;
		_ = 0;
		Keyframe keyframe2 = new Keyframe(1f, 0f, -1f, outTangent);
		_ = 0;
		_ = 0;
		_ = 0;
		responseCurve = new AnimationCurve(keys);
		outputLastValueWhenStopped = true;
		base._002Ector();
	}
}
