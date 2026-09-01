using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Events;

public class OdometerAudioController : MonoBehaviour
{
	private OdometerDisplay odometer;

	private Transform audioOrigin;

	private bool includeDecimalDrums;

	private float rateWindowSeconds;

	private int maxTicksTracked;

	private AudioClip tickOneShotClip;

	private float dryBaseVolume;

	private float dryBasePitch;

	private float dryPitchJitterCents;

	private float highRatePitchBoostMultiplier;

	private float randomPanWidth2D;

	private float randomizePositionRadius3D;

	private int dryVoices;

	private float maxExpectedTickRate;

	private float softRateLimitTps;

	private int maxOneShotsPerFrame;

	private int maxOneShotsPerSecond;

	private float loudnessCompensation;

	private AudioMixerGroup outputMixerGroup;

	private bool spatialize3D;

	private Vector2 spatialRolloff;

	private UnityEvent onAnyTick;

	private int _integerDigits;

	private int _decimalDigits;

	private int _drumCount;

	private int[] _prevDigits;

	private readonly List<float> _tickTimes;

	private float _pow10Decimals;

	private AudioSource[] _voices;

	private int _nextVoice;

	private int _playsThisFrame;

	private readonly Queue<float> _playTimestamps;

	private void Awake()
	{
		if (!odometer)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
			OdometerDisplay odometerDisplay = default(OdometerDisplay);
			odometer = odometerDisplay;
		}
		if ((bool)odometer)
		{
			bool flag = audioOrigin;
			Transform transform = null;
			if (!flag)
			{
				transform = (audioOrigin = odometer.transform);
			}
			OdometerDisplay odometerDisplay2 = odometer;
			bool flag2 = odometerDisplay2.integerDigits < 0;
			int num = 0;
			if (!flag2)
			{
				num = odometerDisplay2.integerDigits;
			}
			OdometerDisplay odometerDisplay3 = odometer;
			_integerDigits = num;
			bool flag3 = odometerDisplay3.decimalDigits < 0;
			int num2 = 0;
			if (!flag3)
			{
				num2 = odometerDisplay3.decimalDigits;
			}
			int num3 = num + num2;
			_decimalDigits = num2;
			_drumCount = num3;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033FCB0");
			OdometerDisplay odometerDisplay4 = odometer;
			_pow10Decimals = 10f;
			int[] prevDigits = new int[num3];
			_prevDigits = prevDigits;
			int[] array = ExtractDigitsFromValue(odometerDisplay4.displayTargetNumber, _prevDigits);
			EnsureVoicePool();
		}
		else
		{
			Debug.LogWarning("[OdometerAudioController] No OdometerDisplay found. Audio will be idle.");
			base.enabled = false;
		}
	}

	private void Update()
	{
		//IL_007f: Expected F4, but got I4
		//IL_0176: Expected F4, but got I4
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Expected O, but got Unknown
		//IL_01ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b1: Expected O, but got Unknown
		//IL_01c4: Expected O, but got I4
		//IL_00c5: Expected I, but got O
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Expected O, but got Unknown
		//IL_01e7: Expected I, but got O
		//IL_0319: Expected O, but got I4
		//IL_0322: Expected O, but got I4
		//IL_0209: Unknown result type (might be due to invalid IL or missing references)
		//IL_020e: Expected O, but got Unknown
		//IL_040a: Unknown result type (might be due to invalid IL or missing references)
		//IL_040f: Expected O, but got Unknown
		//IL_0419: Unknown result type (might be due to invalid IL or missing references)
		//IL_041e: Expected I4, but got Unknown
		//IL_0433: Unknown result type (might be due to invalid IL or missing references)
		//IL_0438: Expected I4, but got Unknown
		//IL_0482: Expected O, but got I4
		//IL_049c: Expected O, but got I4
		//IL_03cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d4: Expected O, but got Unknown
		//IL_03dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e2: Expected O, but got Unknown
		if (!odometer || !(tickOneShotClip != null))
		{
			return;
		}
		OdometerDisplay odometerDisplay = odometer;
		bool flag = odometerDisplay.integerDigits <= 0;
		float num = 0f;
		int num2 = 0;
		UnityEngine.Object obj = null;
		if (!flag)
		{
			UnityEngine.Object obj2 = (UnityEngine.Object)(odometerDisplay.drumStates + 32);
			int num3 = 0;
			bool flag2;
			do
			{
				nint num4 = (nint)obj2;
				num3++;
				obj2 = (UnityEngine.Object)(obj2 + 8);
				float num5 = 0f * 10f;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v271 @ rax_v32 (Il2CppClass<UnityEngine.Object>)+10]");
				float num6 = 0f + num5;
				flag2 = num3 < odometerDisplay.integerDigits;
				num = num6;
				num2 = num3;
				obj = obj2;
			}
			while (flag2);
		}
		bool flag3 = odometerDisplay.integerDigits >= odometerDisplay.drumCount;
		float num7 = 1f;
		float num8 = 0f;
		nint num9 = odometerDisplay.drumCount;
		if (!flag3)
		{
			odometerDisplay = (OdometerDisplay)(object)odometerDisplay.drumStates;
			object obj3 = odometerDisplay.drumStates + 32;
			object obj4 = odometerDisplay.integerDigits * 8;
			UnityEngine.Object obj5 = (UnityEngine.Object)(object)(obj3 + obj4);
			float num10 = 1f;
			bool flag4;
			do
			{
				num7 = num10 * 10f;
				num9 = (nint)obj5;
				num2 = odometerDisplay.integerDigits + 1;
				obj = (UnityEngine.Object)(obj5 + 8);
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v455 @ rcx_v11 (Il2CppClass<UnityEngine.Object>)+10]");
				float num11 = 0f / num7;
				num8 = 0f + num11;
				flag4 = num2 < odometerDisplay.drumCount;
				num10 = num7;
				obj5 = obj;
			}
			while (flag4);
		}
		float num12 = num8 + num;
		float value;
		if (_decimalDigits > 0)
		{
			float num13 = num12 * _pow10Decimals;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1800033E0");
			value = num13 / _pow10Decimals;
		}
		else
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1800033E0");
			value = num12;
		}
		int[] array = ExtractDigitsFromValue(value, null);
		if (_drumCount > 0)
		{
			object obj6 = 32;
			object obj7 = 0;
			do
			{
				object obj8 = obj7 - _integerDigits;
				int num14 = obj7 ^ _integerDigits;
				object obj9 = obj7 ^ obj8;
				int num15 = num14 & obj9;
				bool flag5 = num15 < 0;
				bool flag6 = (nint)obj8 < 0;
				bool flag7 = flag6 == flag5;
				bool flag8 = !includeDecimalDrums;
				object obj10 = flag8 & flag7;
				bool flag9 = obj10 == null;
				object obj11 = !flag9;
				if (obj11 == null)
				{
					int[] prevDigits = _prevDigits;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v570 @ rax_v16 (System.Int32[])+v235 @ rsi_v7]");
					nint num16 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v235 @ rsi_v7+v274 @ rax_v23 (System.Int32[])]");
					if (num16 != 0)
					{
						RegisterTick();
						int[] prevDigits2 = _prevDigits;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v570 @ rax_v16 (System.Int32[])+v235 @ rsi_v7]");
						_ = 0;
						obj = null;
					}
				}
				obj7++;
				obj6 += 4;
			}
			while ((nint)obj7 < _drumCount);
		}
		float time = Time.time;
		CullOldTickTimes(time);
	}

	private void LateUpdate()
	{
		_playsThisFrame = 0;
	}

	private unsafe void RegisterTick()
	{
		//IL_0014: Expected F4, but got Ref
		//IL_003c: Expected F4, but got Ref
		//IL_005e: Expected F4, but got I4
		//IL_01bd: Expected F4, but got I4
		//IL_00a7: Invalid comparison between I4 and F4
		//IL_00b6: Invalid comparison between I4 and F4
		//IL_03d5: Expected O, but got I4
		//IL_03e6: Expected O, but got I4
		//IL_00d6: Expected F4, but got I4
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Expected O, but got Unknown
		//IL_0495: Invalid comparison between I4 and F4
		//IL_02ed: Expected F4, but got I4
		//IL_058b: Invalid comparison between I4 and F4
		//IL_0337: Expected F4, but got I4
		//IL_04de: Invalid comparison between I4 and F4
		//IL_0373: Expected F4, but got I4
		//IL_0387: Expected F4, but got Ref
		float time = Time.time;
		float num = default(float);
		_tickTimes.Add((nint)(&num));
		CullOldTickTimes(time);
		bool flag = onAnyTick == null;
		float num2 = (nint)(&num);
		if (!flag)
		{
			onAnyTick.Invoke();
			num2 = 0f;
		}
		List<float> tickTimes = _tickTimes;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v110 @ rax_v8 (System.Collections.Generic.List`1<System.Single>)+18]");
		float num9;
		if ((nint)0 != 0)
		{
			float num3 = time - rateWindowSeconds;
			bool flag2 = 0f < num3;
			if (0f > num3)
			{
				num3 = 0f;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v110 @ rax_v8 (System.Collections.Generic.List`1<System.Single>)+18]");
			float num4 = 0f - 1f;
			object obj = 0;
			num = time;
			object obj2 = 0;
			if (!flag2)
			{
				bool flag4;
				do
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
					bool flag3 = num < num3;
					nint num5 = 0;
					obj = obj2;
					num2 = num4;
					if (flag3)
					{
						break;
					}
					obj = obj2 + 1;
					float num6 = num4 - 1f;
					flag4 = !(num < num3);
					num5 = 0;
					num2 = num4;
					num4 = num6;
					obj2 = obj;
				}
				while (flag4);
			}
			float num7 = time - num3;
			bool flag5 = 0.01f > num7;
			float num8 = 0.01f;
			if (!flag5)
			{
				num8 = num7;
			}
			num9 = (float)obj / num8;
		}
		else
		{
			num9 = 0f;
			num = time;
		}
		if (_playsThisFrame >= maxOneShotsPerFrame)
		{
			return;
		}
		PrunePlayTimestamps(time);
		Queue<float> playTimestamps = _playTimestamps;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v113 @ rcx_v12 (System.Collections.Generic.Queue`1<System.Single>)+20]");
		if ((nint)0 >= (nint)maxOneShotsPerSecond)
		{
			return;
		}
		bool flag6 = !(num9 > softRateLimitTps);
		float num10 = 1f;
		if (!flag6)
		{
			float num11 = ((!(num9 > 0.0001f)) ? 0.0001f : num9);
			num10 = softRateLimitTps / num11;
		}
		float value = UnityEngine.Random.value;
		if (value > num10)
		{
			return;
		}
		float num12;
		if (1f > num10)
		{
			if (!(num10 > 0.0001f))
			{
				num10 = 0.0001f;
			}
			num12 = 1f / num10;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033FCB0");
		}
		else
		{
			num12 = 1f;
		}
		float num13 = num12 * dryBaseVolume;
		if (!(0f > num13))
		{
			if (num13 > 1f)
			{
				num13 = 1f;
			}
		}
		else
		{
			num13 = 0f;
		}
		float num14 = maxExpectedTickRate;
		if (1f > maxExpectedTickRate)
		{
			num14 = 1f;
		}
		float num15 = num9 / num14;
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
		float num16 = highRatePitchBoostMultiplier - 1f;
		float num17 = num16 * num15;
		float ratePitchMul = num17 + 1f;
		PlayClick(num13, ratePitchMul);
		int playsThisFrame = _playsThisFrame + 1;
		_playsThisFrame = playsThisFrame;
		_playTimestamps.Enqueue((nint)(&num));
		PrunePlayTimestamps(time);
	}

	private bool CanPlayPerSecond(float now)
	{
		//IL_0097: Expected I4, but got O
		//IL_001c: Expected O, but got I
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Expected O, but got Unknown
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Expected I4, but got Unknown
		PrunePlayTimestamps(now);
		Queue<float> playTimestamps = _playTimestamps;
		if (_playTimestamps != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v33 @ rcx_v3 (System.Collections.Generic.Queue`1<System.Single>)+20]");
			object obj = -maxOneShotsPerSecond;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v33 @ rcx_v3 (System.Collections.Generic.Queue`1<System.Single>)+20]");
			int num = (int)((nint)0 ^ (nint)maxOneShotsPerSecond);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v33 @ rcx_v3 (System.Collections.Generic.Queue`1<System.Single>)+20]");
			object obj2 = 0 ^ obj;
			int num2 = num & obj2;
			bool flag = num2 < 0;
			bool flag2 = (nint)obj < 0;
			return flag2 != flag;
		}
		NullReferenceException ex = new NullReferenceException();
		return (byte)(int)ex != 0;
	}

	private void PrunePlayTimestamps(float now)
	{
		//IL_0017: Invalid comparison between F4 and O
		Queue<float> playTimestamps = _playTimestamps;
		float num = now - 1f;
		object obj = default(object);
		while (true)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v59 @ rax_v3 (System.Collections.Generic.Queue`1<System.Single>)+20]");
			if ((nint)0 > (nint)0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808E03B0");
				if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)num) > System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj))
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808DFE00");
					playTimestamps = _playTimestamps;
					continue;
				}
				break;
			}
			break;
		}
	}

	private unsafe void PlayClick(float volume01, float ratePitchMul)
	{
		//IL_005c: Expected O, but got I4
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Expected O, but got Unknown
		//IL_0075: Expected I4, but got O
		//IL_0135: Invalid comparison between F4 and I4
		//IL_00a2: Invalid comparison between F4 and I4
		//IL_00f1: Expected F4, but got I4
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Expected F4, but got Unknown
		//IL_0280: Invalid comparison between F4 and I4
		//IL_02a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ae: Expected F4, but got Unknown
		//IL_033c: Expected O, but got Ref
		//IL_0344: Expected O, but got Ref
		if (_voices == null)
		{
			return;
		}
		AudioSource[] voices = _voices;
		if (voices.Length == 0)
		{
			return;
		}
		int nextVoice = _nextVoice;
		AudioSource[] voices2 = _voices;
		object obj = _nextVoice + 1;
		Vector3 vector = (Vector3)(obj % voices2.Length);
		_nextVoice = (int)vector;
		if (!spatialize3D)
		{
			float panStereo;
			if (randomPanWidth2D > 0f)
			{
				float num = randomPanWidth2D;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C80]");
				float minInclusive = num ^ 0;
				panStereo = UnityEngine.Random.Range(minInclusive, randomPanWidth2D);
			}
			else
			{
				panStereo = 0f;
			}
			voices[nextVoice].panStereo = panStereo;
			float num2 = ratePitchMul;
			Transform transform = (Transform)(object)voices[nextVoice];
		}
		else
		{
			Transform transform;
			if (!(randomizePositionRadius3D > 0f))
			{
				Transform transform2 = voices[nextVoice].transform;
				Transform transform3;
				if ((bool)audioOrigin)
				{
					transform3 = audioOrigin;
				}
				else
				{
					Transform transform4 = base.transform;
					transform3 = transform4;
				}
				Vector3 position = transform3.position;
				float num2 = ratePitchMul;
				transform = transform2;
			}
			else
			{
				Vector3 insideUnitSphere = UnityEngine.Random.insideUnitSphere;
				float num3 = randomizePositionRadius3D * insideUnitSphere.z;
				Transform transform5 = voices[nextVoice].transform;
				Transform transform6;
				if ((bool)audioOrigin)
				{
					transform6 = audioOrigin;
				}
				else
				{
					Transform transform7 = base.transform;
					transform6 = transform7;
				}
				float num2 = transform6.position.z + num3;
				transform = transform5;
			}
			float num4 = default(float);
			transform.position = (Vector3)(&num4);
			vector = (Vector3)(&num4);
		}
		float num8;
		if (dryPitchJitterCents > 0f)
		{
			float num5 = dryPitchJitterCents;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C80]");
			float minInclusive2 = num5 ^ 0;
			float num6 = UnityEngine.Random.Range(minInclusive2, dryPitchJitterCents);
			float num7 = num6 / 1200f;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033FCB0");
			num8 = 2f;
		}
		else
		{
			num8 = 1f;
		}
		float num9 = ratePitchMul * dryBasePitch;
		float num10 = num9 * num8;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181D58E70");
		voices[nextVoice].PlayOneShot(tickOneShotClip, volume01);
	}

	private float RoundToPrecision(float value)
	{
		if (_decimalDigits > 0)
		{
			float num = value * _pow10Decimals;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1800033E0");
			return num / _pow10Decimals;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1800033E0");
		return value;
	}

	private int[] ExtractDigitsFromValue(float value, int[] reuse)
	{
		//IL_042c: Expected I, but got O
		//IL_002c: Expected I, but got O
		//IL_00d3: Expected O, but got I4
		//IL_0457: Expected O, but got I4
		//IL_02e4: Expected O, but got I4
		//IL_0115: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Expected O, but got Unknown
		//IL_0133: Invalid comparison between O and F4
		//IL_0190: Unknown result type (might be due to invalid IL or missing references)
		//IL_0195: Expected O, but got Unknown
		//IL_019e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a3: Expected O, but got Unknown
		//IL_030b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0310: Expected O, but got Unknown
		//IL_0319: Unknown result type (might be due to invalid IL or missing references)
		//IL_031e: Expected O, but got Unknown
		//IL_0179: Expected O, but got I4
		//IL_01e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ed: Expected O, but got Unknown
		//IL_022e: Expected O, but got I4
		//IL_0236: Unknown result type (might be due to invalid IL or missing references)
		//IL_023b: Expected O, but got Unknown
		//IL_025f: Expected O, but got F8
		//IL_0272: Unknown result type (might be due to invalid IL or missing references)
		//IL_0277: Expected O, but got Unknown
		//IL_0339: Unknown result type (might be due to invalid IL or missing references)
		//IL_033e: Expected O, but got Unknown
		//IL_037f: Expected O, but got I4
		//IL_0387: Unknown result type (might be due to invalid IL or missing references)
		//IL_038c: Expected O, but got Unknown
		//IL_03c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c6: Expected O, but got Unknown
		//IL_03ef: Expected O, but got I4
		bool flag = reuse != null;
		nint num = (nint)this;
		int[] array = reuse;
		int num2 = default(int);
		if (!flag)
		{
			num2 = _drumCount;
			int[] array2 = new int[_drumCount];
			num = (nint)typeof(int[]);
			array = array2;
		}
		float num4;
		if (_decimalDigits > 0)
		{
			float num3 = value * _pow10Decimals;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1800033E0");
			num4 = num3 / _pow10Decimals;
		}
		else
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1800033E0");
			num4 = value;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtss2sd xmm0,xmm7\"");
		double num5 = Math.Floor(0.0);
		bool flag2 = _decimalDigits < 0;
		bool flag3 = _decimalDigits <= 0;
		double num6 = num5;
		object obj = 0;
		if (!flag3)
		{
			float num7 = num4 - (float)num5;
			float num8 = num7 * _pow10Decimals;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803B9370");
			object obj3 = default(object);
			object obj2 = obj3 - _pow10Decimals;
			flag2 = (nint)obj2 < 0;
			bool flag4 = System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj3) < System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)_pow10Decimals);
			num6 = num5;
			obj = obj3;
			num2 = 0;
			if (!flag4)
			{
				num6 = num5 + 1.0;
				obj = 0;
				num2 = 0;
			}
		}
		object obj4 = _integerDigits - 1;
		if (flag2)
		{
			goto IL_02d4;
		}
		object obj5 = obj4 + 8;
		object obj6 = obj5 * 4;
		object obj7 = (object)array + obj6;
		int num9 = num2;
		while ((nint)obj4 < array.Length)
		{
			object obj8 = obj4 - 1;
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"imul esi\"");
			int num10 = num9 >> 2;
			int num11 = num10 >> 31;
			num9 = num10 + num11;
			object obj9 = num9 * 4;
			object obj10 = num9 + obj9;
			object obj11 = obj10 + obj10;
			double num12 = num6 - (double)obj11;
			obj7 = num12;
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"imul esi\"");
			obj7 -= 4;
			int num13 = num9 >> 2;
			int num14 = num13 >> 31;
			num6 = (double)num13 + (double)num14;
			bool flag5 = (nint)obj4 >= array.Length;
			obj4 = obj8;
			num2 = num9;
			if (flag5)
			{
				continue;
			}
			goto IL_02d4;
		}
		goto IL_0465;
		IL_0465:
		return (int[])(object)new IndexOutOfRangeException();
		IL_0410:
		return array;
		IL_02d4:
		object obj12 = _drumCount - 1;
		if ((nint)obj12 < _integerDigits)
		{
			goto IL_0410;
		}
		object obj13 = obj12 + 8;
		object obj14 = obj13 * 4;
		object obj15 = (object)array + obj14;
		while ((nint)obj12 < array.Length)
		{
			obj12--;
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"imul r8d\"");
			int num15 = num2 >> 2;
			int num16 = num15 >> 31;
			num2 = num15 + num16;
			object obj16 = num2 * 4;
			object obj17 = num2 + obj16;
			object obj18 = obj17 + obj17;
			object obj19 = obj - obj18;
			obj15 = obj19;
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"imul r8d\"");
			obj15 -= 4;
			int num17 = num2 >> 2;
			int num18 = num17 >> 31;
			obj = num17 + num18;
			if ((nint)obj12 >= _integerDigits)
			{
				continue;
			}
			goto IL_0410;
		}
		goto IL_0465;
	}

	private void CullOldTickTimes(float now)
	{
		//IL_003e: Invalid comparison between O and F4
		List<float> tickTimes = _tickTimes;
		float num = now - rateWindowSeconds;
		int num2 = 0;
		int num3 = 0;
		object obj = default(object);
		while (true)
		{
			int num4 = num3;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v63 @ rax_v5 (System.Collections.Generic.List`1<System.Single>)+18]");
			if ((nint)num4 >= (nint)0)
			{
				break;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
			if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj) >= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)num))
			{
				break;
			}
			tickTimes = _tickTimes;
			num2++;
			num3 = num2;
		}
		if (num2 > 0)
		{
			_tickTimes.RemoveRange(0, num2);
		}
		List<float> tickTimes2 = _tickTimes;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v110 @ r8_v5 (System.Collections.Generic.List`1<System.Single>)+18]");
		int num5 = (int)(-maxTicksTracked);
		if (num5 > 0)
		{
			_tickTimes.RemoveRange(0, num5);
		}
	}

	private float ComputeTickRate(float now)
	{
		//IL_0160: Expected F4, but got I4
		//IL_0044: Invalid comparison between I4 and F4
		//IL_0053: Invalid comparison between I4 and F4
		//IL_0062: Expected F4, but got I4
		//IL_01af: Expected O, but got I
		//IL_01b8: Expected O, but got I4
		//IL_01c1: Expected O, but got I4
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Expected O, but got Unknown
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Expected O, but got Unknown
		List<float> tickTimes = _tickTimes;
		float num2;
		object obj3;
		if (_tickTimes != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ rax_v2 (System.Collections.Generic.List`1<System.Single>)+18]");
			if ((nint)0 == 0)
			{
				return 0f;
			}
			float num = now - rateWindowSeconds;
			bool flag = 0f < num;
			bool flag2 = 0f > num;
			num2 = 0f;
			if (!flag2)
			{
				num2 = num;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ rax_v2 (System.Collections.Generic.List`1<System.Single>)+18]");
			object obj = -1;
			object obj2 = 0;
			obj3 = 0;
			if (flag)
			{
				goto IL_0115;
			}
			float num3 = default(float);
			while (_tickTimes != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
				bool flag3 = num3 < num2;
				obj3 = obj2;
				if (!flag3)
				{
					obj2++;
					obj--;
					bool flag4 = !(num3 < num2);
					num = num3;
					obj3 = obj2;
					if (flag4)
					{
						continue;
					}
				}
				goto IL_0115;
			}
		}
		throw new NullReferenceException();
		IL_0115:
		float num4 = now - num2;
		bool flag5 = 0.01f > num4;
		float num5 = 0.01f;
		if (!flag5)
		{
			num5 = num4;
		}
		return (float)obj3 / num5;
	}

	private void EnsureVoicePool()
	{
		//IL_014f: Expected O, but got I4
		//IL_0158: Expected O, but got I4
		//IL_0210: Unknown result type (might be due to invalid IL or missing references)
		//IL_0215: Expected O, but got Unknown
		//IL_0042: Expected O, but got I4
		//IL_004b: Expected O, but got I4
		//IL_0054: Expected O, but got I4
		//IL_0174: Unknown result type (might be due to invalid IL or missing references)
		//IL_0179: Expected O, but got Unknown
		//IL_0182: Unknown result type (might be due to invalid IL or missing references)
		//IL_0187: Expected O, but got Unknown
		//IL_007f: Expected O, but got I
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Expected O, but got Unknown
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Expected O, but got Unknown
		//IL_00bc: Expected O, but got I
		if (_voices != null)
		{
			AudioSource[] voices = _voices;
			if (voices.Length == dryVoices)
			{
				return;
			}
			AudioSource[] voices2 = _voices;
			object obj = 32;
			object obj2 = 0;
			object obj3 = 0;
			while ((nint)obj3 < voices2.Length)
			{
				AudioSource[] voices3 = _voices;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v69 @ rsi_v11+v240 @ rax_v23 (UnityEngine.AudioSource[])]");
				if ((UnityEngine.Object)0 != null)
				{
					AudioSource[] voices4 = _voices;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v69 @ rsi_v11+v320 @ rax_v28 (UnityEngine.AudioSource[])]");
					GameObject obj4 = ((Component)0).gameObject;
					UnityEngine.Object.Destroy(obj4);
				}
				voices2 = _voices;
				obj2++;
				obj += 8;
				bool flag = false;
				obj3 = obj2;
			}
		}
		AudioSource[] voices5 = new AudioSource[dryVoices];
		_voices = voices5;
		bool flag2 = dryVoices <= 0;
		object obj5 = 32;
		object obj6 = 0;
		if (!flag2)
		{
			object arg = default(object);
			bool flag3;
			do
			{
				object obj7 = obj6 + 1;
				AudioSource[] voices6 = _voices;
				Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
				string text = $"Dry_{arg}";
				AudioSource audioSource = CreateChildSource(text, loop: false);
				obj6++;
				obj5 += 8;
				flag3 = (nint)obj6 < dryVoices;
				object obj8 = obj7;
				bool flag = false;
			}
			while (flag3);
		}
		_nextVoice = 0;
	}

	private AudioSource CreateChildSource(string name, bool loop)
	{
		//IL_013d: Expected F4, but got I4
		//IL_015f: Expected F4, but got O
		//IL_0186: Invalid comparison between F4 and I
		//IL_01ad: Expected F4, but got I
		string text = "OdometerAudio_" + name;
		GameObject gameObject = new GameObject(text);
		if ((object)gameObject != null)
		{
			Transform transform = gameObject.transform;
			Transform parent = ((!audioOrigin) ? base.transform : audioOrigin);
			if ((object)transform != null)
			{
				transform.SetParent(parent, worldPositionStays: false);
				AudioSource audioSource = gameObject.AddComponent<AudioSource>();
				if ((object)audioSource != null)
				{
					audioSource.playOnAwake = false;
					audioSource.loop = loop;
					audioSource.outputAudioMixerGroup = outputMixerGroup;
					float spatialBlend = ((!spatialize3D) ? 0f : 1f);
					audioSource.spatialBlend = spatialBlend;
					if (spatialize3D)
					{
						audioSource.rolloffMode = AudioRolloffMode.Linear;
						audioSource.minDistance = (float)spatialRolloff;
						float num = (float)spatialRolloff + 0.01f;
						float num2 = num;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (OdometerAudioController)+88]");
						if (num2 < 0f)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (OdometerAudioController)+88]");
							num = 0f;
						}
						audioSource.maxDistance = num;
						audioSource.dopplerLevel = 0f;
						audioSource.spread = 0f;
					}
					return audioSource;
				}
			}
		}
		return (AudioSource)(object)new NullReferenceException();
	}

	public OdometerAudioController()
	{
		//IL_00f5: Expected O, but got I4
		includeDecimalDrums = true;
		rateWindowSeconds = 0.25f;
		maxTicksTracked = 64;
		dryBaseVolume = 0.85f;
		dryBasePitch = 1f;
		dryPitchJitterCents = 8f;
		highRatePitchBoostMultiplier = 1.1f;
		randomPanWidth2D = 0.15f;
		randomizePositionRadius3D = 0.03f;
		dryVoices = 16;
		maxExpectedTickRate = 40f;
		softRateLimitTps = 24f;
		maxOneShotsPerFrame = 6;
		maxOneShotsPerSecond = 80;
		loudnessCompensation = 0.45f;
		spatialize3D = true;
		spatialRolloff = (Vector2)1065353216;
		_ = 1097859072;
		List<float> tickTimes = new List<float>(64);
		_tickTimes = tickTimes;
		_pow10Decimals = 1f;
		_playTimestamps = new Queue<float>(128);
		base._002Ector();
	}
}
