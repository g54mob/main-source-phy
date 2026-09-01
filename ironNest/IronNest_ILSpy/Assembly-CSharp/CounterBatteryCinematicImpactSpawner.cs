using System;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using UnityEngine;

public class CounterBatteryCinematicImpactSpawner : MonoBehaviour
{
	private CounterBatteryTimer timer;

	private GameObject impactPrefab;

	private bool parentSpawnedImpactsToThis;

	private bool randomizeYaw;

	private bool onlyWhileTimerRunning;

	private AnimationCurve meanRadiusMetersBySecondsRemaining;

	private AnimationCurve impactRatePerSecondBySecondsRemaining;

	private bool usePercentVariance;

	private float variancePercent;

	private bool useMultiplierBandInstead;

	private float radiusMultiplierMin;

	private float radiusMultiplierMax;

	private float absoluteMinRadius;

	private float absoluteMaxRadius;

	private bool uniformAngle;

	private float forwardConeAngleDegrees;

	private float spawnYOffset;

	private float minDelaySeconds;

	private float maxDelaySeconds;

	private float delayJitter;

	private bool verbose;

	private float _nextSpawnTime;

	private bool _scheduled;

	private void OnEnable()
	{
		_scheduled = false;
		_nextSpawnTime = 1f / 0f;
	}

	private void Update()
	{
		if (timer == null)
		{
			timer = CounterBatteryTimer._003CInstance_003Ek__BackingField;
		}
		UnityEngine.Object obj = timer;
		bool flag = timer == null;
		if (flag)
		{
			return;
		}
		if (onlyWhileTimerRunning != flag)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v107 @ rdi_v2 (UnityEngine.Object)+60]");
			if ((nint)0 == (flag ? 1 : 0))
			{
				_scheduled = flag;
				_nextSpawnTime = 1f / 0f;
				return;
			}
		}
		if (impactPrefab != null)
		{
			float time = Time.time;
			if (!_scheduled)
			{
				ScheduleNext(time, timer);
			}
			if (!(time < _nextSpawnTime))
			{
				SpawnOne(timer);
				ScheduleNext(time, timer);
			}
		}
		else if (verbose)
		{
			Debug.LogWarning("[CounterBatteryCinematicImpactSpawner] impactPrefab is not assigned.", this);
		}
	}

	private CounterBatteryTimer ResolveTimer()
	{
		if (timer == null)
		{
			timer = CounterBatteryTimer._003CInstance_003Ek__BackingField;
		}
		return timer;
	}

	private void ScheduleNext(float now, CounterBatteryTimer t)
	{
		//IL_03be: Invalid comparison between I4 and F4
		//IL_03d0: Expected F4, but got I4
		//IL_0109: Expected F4, but got I4
		//IL_02d5: Invalid comparison between I4 and F4
		//IL_02e7: Expected F4, but got I4
		//IL_03e7: Invalid comparison between I4 and F4
		//IL_0092: Invalid comparison between I4 and F8
		//IL_00a4: Expected F4, but got I4
		//IL_012d: Invalid comparison between I4 and F4
		//IL_0180: Expected F4, but got I4
		//IL_0305: Invalid comparison between F4 and I
		//IL_0432: Unknown result type (might be due to invalid IL or missing references)
		//IL_0437: Expected O, but got Unknown
		//IL_0442: Invalid comparison between F4 and I4
		//IL_0336: Invalid comparison between I4 and F4
		//IL_0348: Expected F4, but got I4
		//IL_0195: Expected F4, but got I
		float num;
		CounterBatteryTimer counterBatteryTimer = default(CounterBatteryTimer);
		if (counterBatteryTimer._running && !counterBatteryTimer._expired && !counterBatteryTimer._permanentlyStopped)
		{
			double timeAsDouble = Time.timeAsDouble;
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"subsd xmm6,xmm0\"");
			bool flag = !(0.0 < counterBatteryTimer.endTime);
			num = 0f;
			if (!flag)
			{
				num = (float)counterBatteryTimer.endTime;
			}
		}
		else
		{
			num = counterBatteryTimer._remainingSeconds;
		}
		bool flag2 = !(0f < num);
		float time = 0f;
		if (!flag2)
		{
			time = num;
		}
		float num2;
		if (impactRatePerSecondBySecondsRemaining != null)
		{
			num2 = impactRatePerSecondBySecondsRemaining.Evaluate(time);
			counterBatteryTimer = null;
		}
		else
		{
			num2 = 0f;
		}
		bool flag3 = !(0f < num2);
		float num3 = 0f;
		if (!flag3)
		{
			num3 = num2;
		}
		if (0f < num3)
		{
			float value = UnityEngine.Random.value;
			float num4;
			if (!(0f > value))
			{
				bool flag4 = !(value > 1f);
				num4 = value;
				if (!flag4)
				{
					num4 = 1f;
				}
			}
			else
			{
				num4 = 0f;
			}
			float num5 = num4;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206D0C]");
			if (num5 > 0f)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206D0C]");
				num4 = 0f;
			}
			float num6 = 1f - num4;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033FAE0");
			float num7 = delayJitter;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C80]");
			object obj = num6 ^ 0;
			bool flag5 = !(delayJitter > 0f);
			float num8 = 1f;
			if (!flag5)
			{
				num7++;
				float minInclusive = 1f - delayJitter;
				float num9 = UnityEngine.Random.Range(minInclusive, num7);
				num8 = num9;
				counterBatteryTimer = null;
			}
			float num10 = (float)obj / num3;
			bool flag6 = !(0f < minDelaySeconds);
			float num11 = 0f;
			if (!flag6)
			{
				num11 = minDelaySeconds;
			}
			float num12 = num10 * num8;
			bool flag7 = !(num11 < maxDelaySeconds);
			float num13 = num11;
			if (!flag7)
			{
				num13 = maxDelaySeconds;
			}
			if (!(num11 > num12))
			{
				if (num12 > num13)
				{
					num12 = num13;
				}
			}
			else
			{
				num12 = num11;
			}
			bool flag8 = !verbose;
			float nextSpawnTime = num12 + now;
			_scheduled = true;
			_nextSpawnTime = nextSpawnTime;
			if (!flag8)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
				Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
				Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
				object arg = default(object);
				object arg2 = default(object);
				object arg3 = default(object);
				string message = $"[CounterBatteryCinematicImpactSpawner] Next spawn in {arg:0.00}s (secondsRemaining={arg2:0.0}, rate={arg3:0.000}/s).";
				Debug.Log(message, this);
			}
		}
		else
		{
			_scheduled = false;
			_nextSpawnTime = 1f / 0f;
		}
	}

	private unsafe void SpawnOne(CounterBatteryTimer t)
	{
		//IL_00eb: Expected O, but got Ref
		//IL_069e: Invalid comparison between I4 and F4
		//IL_06b0: Expected F4, but got I4
		//IL_0130: Expected F4, but got I4
		//IL_01d1: Invalid comparison between I4 and F4
		//IL_01e3: Expected F4, but got I4
		//IL_00a4: Invalid comparison between I4 and F8
		//IL_00b6: Expected F4, but got I4
		//IL_05a5: Invalid comparison between I4 and F4
		//IL_05b7: Expected F4, but got I4
		//IL_0169: Invalid comparison between I4 and F4
		//IL_01b4: Expected F4, but got I4
		//IL_055a: Invalid comparison between I4 and F4
		//IL_056c: Expected F4, but got I4
		//IL_02e9: Expected O, but got I4
		//IL_0266: Invalid comparison between I4 and F4
		//IL_03b4: Expected F4, but got I4
		//IL_032d: Expected O, but got F4
		//IL_03d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03db: Expected F4, but got Unknown
		//IL_042e: Expected O, but got I4
		//IL_0436: Expected O, but got Ref
		//IL_038f: Expected O, but got I4
		Transform transform = base.transform;
		Vector3 position = transform.position;
		object obj = default(object);
		float num;
		AnimationCurve animationCurve;
		if (t._running && !t._expired && !t._permanentlyStopped)
		{
			double timeAsDouble = Time.timeAsDouble;
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"subsd xmm6,xmm0\"");
			bool flag = !(0.0 < t.endTime);
			num = 0f;
			if (!flag)
			{
				num = (float)t.endTime;
			}
			animationCurve = null;
		}
		else
		{
			num = t._remainingSeconds;
			animationCurve = (AnimationCurve)(&obj);
		}
		bool flag2 = !(0f < num);
		float time = 0f;
		if (!flag2)
		{
			time = num;
		}
		float num3;
		if (meanRadiusMetersBySecondsRemaining != null)
		{
			animationCurve = meanRadiusMetersBySecondsRemaining;
			float num2 = meanRadiusMetersBySecondsRemaining.Evaluate(time);
			num3 = num2;
		}
		else
		{
			num3 = 0f;
		}
		float maxInclusive;
		float num5;
		if (!useMultiplierBandInstead && usePercentVariance)
		{
			float num4 = variancePercent;
			if (!(0f > variancePercent))
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
			maxInclusive = num4 + 1f;
			num = 1f - num4;
			bool flag3 = !(0f < num);
			num5 = 0f;
			if (!flag3)
			{
				num5 = num;
			}
		}
		else
		{
			bool flag4 = !(0f < radiusMultiplierMin);
			num5 = 0f;
			if (!flag4)
			{
				num5 = radiusMultiplierMin;
			}
			bool flag5 = !(num5 < radiusMultiplierMax);
			maxInclusive = num5;
			if (!flag5)
			{
				maxInclusive = radiusMultiplierMax;
			}
		}
		float num6 = UnityEngine.Random.Range(num5, maxInclusive);
		float num7 = num6 * num3;
		bool flag6 = !(0f < absoluteMinRadius);
		float num8 = 0f;
		if (!flag6)
		{
			num8 = absoluteMinRadius;
		}
		bool flag7 = !(num8 < absoluteMaxRadius);
		float num9 = num8;
		if (!flag7)
		{
			num9 = absoluteMaxRadius;
		}
		if (num8 > num7 || num7 > num9)
		{
		}
		if (uniformAngle)
		{
			goto IL_02b4;
		}
		float num10 = forwardConeAngleDegrees;
		if (!(0f > forwardConeAngleDegrees))
		{
			if (num10 > 360f || !(num10 < 360f))
			{
				goto IL_02b4;
			}
		}
		else
		{
			num10 = 0f;
		}
		float num11 = num10 * 0.5f;
		Transform transform2 = base.transform;
		Vector3 eulerAngles = transform2.eulerAngles;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C80]");
		float minInclusive = num11 ^ 0;
		float num12 = UnityEngine.Random.Range(minInclusive, num11);
		float num13 = num12 + eulerAngles.y;
		float num14 = num13 * ((float)Math.PI / 180f);
		float num15 = num11;
		Transform transform3 = transform2;
		object obj2 = 0;
		animationCurve = (AnimationCurve)(&obj);
		goto IL_067c;
		IL_02b4:
		float num16 = UnityEngine.Random.Range(0f, (float)Math.PI * 2f);
		num14 = num16;
		num15 = (float)Math.PI * 2f;
		transform3 = transform;
		obj2 = 0;
		goto IL_067c;
		IL_067c:
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033DE70");
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033E400");
		Transform transform4 = impactPrefab.transform;
		Quaternion rotation = transform4.rotation;
		bool flag8 = !randomizeYaw;
		Vector3 euler = (Vector3)position.x;
		float z = position.z;
		if (!flag8)
		{
			float num17 = UnityEngine.Random.Range(0f, 360f);
			z = num17 * ((float)Math.PI / 180f);
			Quaternion quaternion = Quaternion.Internal_FromEulerRad(ref euler);
			num15 = 360f;
			euler = (Vector3)0;
		}
		if (parentSpawnedImpactsToThis)
		{
			Transform transform5 = base.transform;
			Transform transform6 = transform5;
		}
		else
		{
			Transform transform6 = null;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180733CA0");
		if (verbose)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
			object arg = default(object);
			object arg2 = default(object);
			object arg3 = default(object);
			string text = $"secondsRemaining={arg:0.0}, meanRadius={arg2:0.0}m, radius={arg3:0.0}m.";
			string message = "[CounterBatteryCinematicImpactSpawner] Spawned impact: " + text;
			Debug.Log(message, this);
		}
	}

	private float ComputeRandomizedRadius(float meanRadius)
	{
		//IL_00be: Invalid comparison between I4 and F4
		//IL_00d0: Expected F4, but got I4
		//IL_01f3: Expected F4, but got I4
		//IL_0100: Expected F4, but got I4
		//IL_0056: Invalid comparison between I4 and F4
		//IL_00a1: Expected F4, but got I4
		//IL_017d: Invalid comparison between I4 and F4
		//IL_018f: Expected F4, but got I4
		//IL_0247: Expected F4, but got I4
		float maxInclusive;
		float num3;
		float num4;
		if (!useMultiplierBandInstead && usePercentVariance)
		{
			float num = variancePercent;
			if (!(0f > variancePercent))
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
			maxInclusive = num + 1f;
			float num2 = 1f - num;
			bool flag = !(0f < num2);
			num3 = 0f;
			if (!flag)
			{
				num3 = num2;
			}
			num4 = 0f;
		}
		else
		{
			bool flag2 = !(0f < radiusMultiplierMin);
			num3 = 0f;
			if (!flag2)
			{
				num3 = radiusMultiplierMin;
			}
			bool flag3 = !(num3 < radiusMultiplierMax);
			maxInclusive = num3;
			num4 = 0f;
			if (!flag3)
			{
				maxInclusive = radiusMultiplierMax;
				num4 = 0f;
			}
		}
		float num5 = UnityEngine.Random.Range(num3, maxInclusive);
		if (num4 < absoluteMinRadius)
		{
			num4 = absoluteMinRadius;
		}
		float num6 = num5 * meanRadius;
		bool flag4 = !(num4 < absoluteMaxRadius);
		float num7 = num4;
		if (!flag4)
		{
			num7 = absoluteMaxRadius;
		}
		if (!(num4 > num6))
		{
			if (num6 > num7)
			{
				return num7;
			}
		}
		else
		{
			num6 = num4;
		}
		return num6;
	}

	private float SampleAngleRadians()
	{
		//IL_0037: Invalid comparison between I4 and F4
		//IL_00a6: Expected F4, but got I4
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Expected F4, but got Unknown
		if (!uniformAngle)
		{
			float num = forwardConeAngleDegrees;
			if (!(0f > forwardConeAngleDegrees))
			{
				if (num > 360f || !(num < 360f))
				{
					return UnityEngine.Random.Range(0f, (float)Math.PI * 2f);
				}
			}
			else
			{
				num = 0f;
			}
			float num2 = num * 0.5f;
			Transform transform = base.transform;
			Vector3 eulerAngles = transform.eulerAngles;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C80]");
			float minInclusive = num2 ^ 0;
			float num3 = UnityEngine.Random.Range(minInclusive, num2);
			float num4 = num3 + eulerAngles.y;
			return num4 * ((float)Math.PI / 180f);
		}
		return UnityEngine.Random.Range(0f, (float)Math.PI * 2f);
	}

	public unsafe CounterBatteryCinematicImpactSpawner()
	{
		//IL_0008: Expected O, but got Ref
		//IL_0313: Expected O, but got Ref
		//IL_0333: Expected native int or pointer, but got O
		//IL_001b: Expected O, but got Ref
		//IL_0062: Expected native int or pointer, but got O
		//IL_00d1: Expected O, but got Ref
		//IL_00f1: Expected native int or pointer, but got O
		//IL_0109: Expected O, but got Ref
		//IL_0150: Expected native int or pointer, but got O
		//IL_0168: Expected O, but got Ref
		//IL_01af: Expected native int or pointer, but got O
		//IL_01c7: Expected O, but got Ref
		//IL_020e: Expected native int or pointer, but got O
		object obj2 = default(object);
		object obj = (object)(&obj2);
		randomizeYaw = true;
		Keyframe[] keys = new Keyframe[2];
		Keyframe keyframe = (Keyframe)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 105));
		_ = 0;
		_ = 0;
		_ = 0;
		*(Keyframe*)(nint)keyframe = new Keyframe(600f, 20000f);
		Keyframe keyframe2 = (Keyframe)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 73));
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-69]");
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-59]");
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-51]");
		_ = 0;
		_ = 0;
		_ = 0;
		_ = 0;
		*(Keyframe*)(nint)keyframe2 = new Keyframe(0f, 50f);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-49]");
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-39]");
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-31]");
		_ = 0;
		meanRadiusMetersBySecondsRemaining = new AnimationCurve(keys);
		Keyframe[] keys2 = new Keyframe[4];
		Keyframe keyframe3 = (Keyframe)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 41));
		_ = 0;
		_ = 0;
		_ = 0;
		*(Keyframe*)(nint)keyframe3 = new Keyframe(600f, 0.03f);
		Keyframe keyframe4 = (Keyframe)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 9));
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-29]");
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-19]");
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-11]");
		_ = 0;
		_ = 0;
		_ = 0;
		_ = 0;
		*(Keyframe*)(nint)keyframe4 = new Keyframe(120f, 0.07f);
		Keyframe keyframe5 = (Keyframe)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 23));
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-9]");
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1+7]");
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1+F]");
		_ = 0;
		_ = 0;
		_ = 0;
		_ = 0;
		*(Keyframe*)(nint)keyframe5 = new Keyframe(30f, 0.18f);
		Keyframe keyframe6 = (Keyframe)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 55));
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1+17]");
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1+27]");
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1+2F]");
		_ = 0;
		_ = 0;
		_ = 0;
		_ = 0;
		*(Keyframe*)(nint)keyframe6 = new Keyframe(0f, 0.35f);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1+37]");
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1+47]");
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1+4F]");
		_ = 0;
		impactRatePerSecondBySecondsRemaining = new AnimationCurve(keys2);
		usePercentVariance = true;
		variancePercent = 0.3f;
		radiusMultiplierMin = 0.7f;
		radiusMultiplierMax = 1.3f;
		absoluteMinRadius = 10f;
		absoluteMaxRadius = 20000f;
		uniformAngle = true;
		forwardConeAngleDegrees = 180f;
		minDelaySeconds = 0.25f;
		maxDelaySeconds = 25f;
		delayJitter = 0.2f;
		_nextSpawnTime = 1f / 0f;
		base._002Ector();
	}
}
