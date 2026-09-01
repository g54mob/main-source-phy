using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.Events;

public class ValveController : MonoBehaviour, IValve
{
	[Serializable]
	private struct QualityMultiplierEntry
	{
		public int qualityLevelIndex;

		public float multiplier;
	}

	private struct ParticleBaseline
	{
		public ParticleSystem system;

		public float initialRate;
	}

	private struct ScaleBaseline
	{
		public Transform target;

		public Vector3 initialLocalScale;
	}

	private HighPressureSystemManager systemManager;

	private string systemId;

	private DialInteractable dial;

	private float fixedValue;

	private float brokenValue;

	private bool continuousEffectUpdate;

	private bool startDamaged;

	private AudioSource loopAudio;

	private AnimationCurve volumeOverDamage;

	private float minVolume;

	private float maxVolume;

	private bool usePitch;

	private float pitchMin;

	private float pitchMax;

	private ParticleSystem[] particleSystems;

	private AnimationCurve emissionOverDamage;

	private bool useQualityEmissionMultiplier;

	private float defaultQualityEmissionMultiplier;

	private QualityMultiplierEntry[] qualityEmissionMultipliers;

	private bool autoRefreshQualityMultiplier;

	private float qualityRefreshIntervalSeconds;

	private Transform[] scaleTargets;

	private AnimationCurve scaleMultiplierOverDamage;

	private bool useScaleFeedback;

	public UnityEvent<float> OnDamageChanged01Unity;

	public UnityEvent OnValveDamaged;

	public UnityEvent OnValveFixed;

	public UnityEvent<float> OnFirstDamageTaken01;

	private Action<float> m_DamageChanged01;

	private Action<float> m_FirstDamageTaken01;

	private float currentDamage01;

	private bool lastWasBroken;

	private HighPressureSystemManager boundManager;

	private float initialAudioVolume;

	private bool audioWasPlayingOnStart;

	private float previousParticleEmissionMultiplier;

	private float cachedQualityEmissionMultiplier;

	private int lastQualityLevelIndex;

	private float nextQualityPollTime;

	private readonly List<ParticleBaseline> particleBaselines;

	private readonly List<ScaleBaseline> scaleBaselines;

	private const float kZeroThreshold = 0.001f;

	public float Damage01 => currentDamage01;

	public float CurrentDialValue
	{
		get
		{
			//IL_004d: Expected F4, but got I4
			if (dial != null)
			{
				DialInteractable dialInteractable = dial;
				return dialInteractable.accumulatedValue;
			}
			return 0f;
		}
	}

	public event Action<float> DamageChanged01
	{
		add
		{
			//IL_006f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0074: Expected O, but got Unknown
			object obj = this + 208;
			Delegate obj2 = this.m_DamageChanged01;
			Delegate obj5 = default(Delegate);
			while (true)
			{
				Delegate obj3 = Delegate.Combine(obj2, value);
				bool flag = (object)obj3 == null;
				Delegate obj4 = obj3;
				if (!flag)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
					if ((object)obj4 == null)
					{
						break;
					}
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802AC5F0");
				bool flag2 = (object)obj5 != obj2;
				obj2 = obj5;
				if (!flag2)
				{
					return;
				}
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
		}
		remove
		{
			//IL_006f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0074: Expected O, but got Unknown
			object obj = this + 208;
			Delegate obj2 = this.m_DamageChanged01;
			Delegate obj5 = default(Delegate);
			while (true)
			{
				Delegate obj3 = Delegate.Remove(obj2, value);
				bool flag = (object)obj3 == null;
				Delegate obj4 = obj3;
				if (!flag)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
					if ((object)obj4 == null)
					{
						break;
					}
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802AC5F0");
				bool flag2 = (object)obj5 != obj2;
				obj2 = obj5;
				if (!flag2)
				{
					return;
				}
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
		}
	}

	public event Action<float> FirstDamageTaken01
	{
		add
		{
			//IL_006f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0074: Expected O, but got Unknown
			object obj = this + 216;
			Delegate obj2 = this.m_FirstDamageTaken01;
			Delegate obj5 = default(Delegate);
			while (true)
			{
				Delegate obj3 = Delegate.Combine(obj2, value);
				bool flag = (object)obj3 == null;
				Delegate obj4 = obj3;
				if (!flag)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
					if ((object)obj4 == null)
					{
						break;
					}
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802AC5F0");
				bool flag2 = (object)obj5 != obj2;
				obj2 = obj5;
				if (!flag2)
				{
					return;
				}
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
		}
		remove
		{
			//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
			//IL_00e8: Expected O, but got Unknown
			//IL_007e: Expected O, but got I
			//IL_0086: Expected I, but got O
			//IL_00ad: Expected O, but got I
			object obj = this + 216;
			Delegate obj2 = this.m_FirstDamageTaken01;
			Delegate obj3;
			Delegate obj5 = default(Delegate);
			while (true)
			{
				obj3 = Delegate.Remove(obj2, value);
				bool flag = (object)obj3 == null;
				Delegate obj4 = obj3;
				if (!flag)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
					if ((object)obj4 == null)
					{
						break;
					}
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802AC5F0");
				bool flag2 = (object)obj5 != obj2;
				obj2 = obj5;
				if (!flag2)
				{
					return;
				}
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v81 @ rax_v3 (System.Delegate)+8]");
			object obj6 = 0;
			nint num = (nint)obj3;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
			object obj7 = default(object);
			obj6 = obj7;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v81 @ rax_v3 (System.Delegate)+8]");
			((IDisposable)(object)0)?.Dispose();
		}
	}

	event Action<float> IValve.DamageChanged01
	{
		add
		{
			DamageChanged01 += value;
		}
		remove
		{
			DamageChanged01 -= value;
		}
	}

	private void Reset()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696150");
		DialInteractable dialInteractable = default(DialInteractable);
		dial = dialInteractable;
	}

	private void OnValidate()
	{
	}

	private void Awake()
	{
		if (dial != null)
		{
			DialInteractable dialInteractable = dial;
			if (dialInteractable.dialMode != DialInteractable.DialMode.Limited)
			{
				Debug.LogWarning("ValveController: DialInteractable is not in Limited mode. Set it to Limited for correct behavior.", dialInteractable);
			}
		}
		else
		{
			Debug.LogError("ValveController: No DialInteractable assigned. Please assign a Dial in the inspector.", this);
		}
		if (loopAudio != null)
		{
			float volume = loopAudio.volume;
			initialAudioVolume = volume;
			bool isPlaying = loopAudio.isPlaying;
			audioWasPlayingOnStart = isPlaying;
		}
		CacheParticleBaselines();
		CacheScaleBaselines();
	}

	private void Start()
	{
		throw new Exception("Decompilation failed: Stack state not settling! (500001 blocks already visited)");
	}

	private void OnDisable()
	{
		//IL_006d: Expected O, but got I
		if (boundManager != null)
		{
			object obj = boundManager;
			if (this != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v68 @ rsi_v8 (System.Object)+58]");
				if (((List<ValveController>)0).Remove(this))
				{
					Action<float> value = ((HighPressureSystemManager)obj).HandleValveDamageChanged;
					DamageChanged01 -= value;
					((HighPressureSystemManager)obj).RecomputeHealthAndNotify(false);
				}
			}
		}
		if (dial != null)
		{
			DialInteractable dialInteractable = dial;
			UnityAction<float> call = HandleDialValueChanged;
			dialInteractable.OnValueChanged.RemoveListener(call);
			Action value2 = HandleBeginDialDrag;
			dial.OnBeginDialDrag -= value2;
			Action value3 = HandleEndDialDrag;
			dial.OnEndDialDrag -= value3;
		}
	}

	private void Update()
	{
		if (useQualityEmissionMultiplier && autoRefreshQualityMultiplier)
		{
			float unscaledTime = Time.unscaledTime;
			if (!(unscaledTime < nextQualityPollTime))
			{
				float unscaledTime2 = Time.unscaledTime;
				float num = unscaledTime2 + qualityRefreshIntervalSeconds;
				nextQualityPollTime = num;
				RefreshQualityEmissionMultiplier(force: false);
			}
		}
		if (continuousEffectUpdate)
		{
			UpdateFromDial();
		}
	}

	private void HandleBeginDialDrag()
	{
	}

	private void HandleEndDialDrag()
	{
	}

	private void HandleDialValueChanged(float _)
	{
		if (!continuousEffectUpdate)
		{
			UpdateFromDial();
		}
	}

	private unsafe void UpdateFromDial(bool forceNotify = false)
	{
		//IL_0139: Expected O, but got I4
		//IL_067c: Expected F4, but got I4
		//IL_0689: Invalid comparison between O and F4
		//IL_0175: Expected F4, but got I4
		//IL_00d3: Invalid comparison between I4 and F4
		//IL_00e2: Expected O, but got I4
		//IL_010b: Expected O, but got I4
		//IL_0340: Invalid comparison between F4 and I4
		//IL_0369: Expected O, but got I4
		//IL_038d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0392: Expected O, but got Unknown
		//IL_03a9: Expected F4, but got O
		//IL_0477: Invalid comparison between F4 and I4
		//IL_04a0: Expected O, but got I4
		//IL_04a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ad: Expected O, but got Unknown
		//IL_0122: Expected O, but got I4
		//IL_03da: Expected F4, but got O
		//IL_0528: Expected O, but got F4
		//IL_0404: Expected F4, but got Ref
		//IL_01bd: Invalid comparison between O and F4
		//IL_04fd: Expected F4, but got I4
		//IL_0418: Expected F4, but got Ref
		//IL_042a: Expected F4, but got Ref
		//IL_0208: Expected F4, but got I4
		//IL_059d: Expected F4, but got Ref
		//IL_0233: Expected O, but got I4
		//IL_0249: Expected O, but got I4
		//IL_02d5: Expected O, but got Ref
		//IL_02e5: Expected O, but got Ref
		//IL_02fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0302: Expected O, but got Unknown
		float accumulatedValue;
		if (dial != null)
		{
			DialInteractable dialInteractable = dial;
			accumulatedValue = dialInteractable.accumulatedValue;
		}
		else
		{
			accumulatedValue = fixedValue;
		}
		float num = fixedValue;
		bool flag = !(fixedValue > brokenValue);
		float num2 = brokenValue;
		if (!flag)
		{
			num2 = fixedValue;
			num = brokenValue;
		}
		ScaleBaseline scaleBaseline = ((List<ScaleBaseline>)null).get_Item(0);
		float num5;
		object obj;
		if ((object)scaleBaseline == null)
		{
			bool flag2 = num == num2;
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"jp short 000000018046711Ch\"");
			if (!flag2)
			{
				float num3 = num2 - num;
				float num4 = accumulatedValue - num;
				num5 = num4 / num3;
				bool flag3 = 0f > num5;
				obj = 0;
				if (flag3)
				{
					goto IL_0673;
				}
				bool flag4 = !(num5 > 1f);
				obj = 0;
				if (!flag4)
				{
					obj = 0;
					num5 = 1f;
				}
				goto IL_0681;
			}
		}
		obj = 0;
		goto IL_0673;
		IL_0673:
		num5 = 0f;
		goto IL_0681;
		IL_0681:
		if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj) <= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)num5))
		{
			if (num5 > 1f)
			{
				num5 = 1f;
			}
		}
		else
		{
			num5 = 0f;
		}
		currentDamage01 = num5;
		ApplyAudioFeedback(num5);
		float num6 = currentDamage01;
		ApplyParticleFeedback(currentDamage01);
		float num7 = currentDamage01;
		bool flag5 = !useScaleFeedback;
		UnityEngine.Object obj2 = null;
		if (!flag5)
		{
			List<ScaleBaseline> list = scaleBaselines;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v170 @ rax_v31 (System.Collections.Generic.List`1<ValveController+ScaleBaseline>)+18]");
			bool flag6 = (nint)0 == 0;
			obj2 = null;
			if (!flag6)
			{
				if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj) <= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)currentDamage01))
				{
					if (num7 > 1f)
					{
						num7 = 1f;
					}
				}
				else
				{
					num7 = 0f;
				}
				float num8 = scaleMultiplierOverDamage.Evaluate(num7);
				List<ScaleBaseline> list2 = scaleBaselines;
				object obj3 = 0;
				num6 = num7;
				obj2 = null;
				object obj4 = 0;
				UnityEngine.Object obj6 = default(UnityEngine.Object);
				object obj7 = default(object);
				object obj8 = default(object);
				float num10 = default(float);
				float num11 = default(float);
				while (true)
				{
					object obj5 = obj4;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v172 @ rax_v33 (System.Collections.Generic.List`1<ValveController+ScaleBaseline>)+18]");
					if ((nint)obj5 >= 0)
					{
						break;
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
					bool flag7 = obj6 != null;
					obj2 = null;
					if (flag7)
					{
						num6 = (float)obj7 * num8;
						float num9 = (float)obj8 * num8;
						((Transform)obj6).localScale = (Vector3)(&num10);
						num10 = num11;
						obj2 = (UnityEngine.Object)(&num10);
					}
					list2 = scaleBaselines;
					obj3++;
					obj4 = obj3;
				}
			}
		}
		bool flag8 = currentDamage01 < 0.001f;
		float num12 = currentDamage01 - 0.001f;
		bool flag9 = num12 == 0f;
		bool flag10 = !flag8;
		bool flag11 = !flag9;
		UnityEvent<float> unityEvent = (UnityEvent<float>)(flag11 & flag10);
		bool flag12 = 0.001f < currentDamage01;
		bool flag13 = !flag12;
		object obj9 = flag13 & unityEvent;
		bool flag14 = obj9 == null;
		float num13 = (float)obj2;
		float num14 = default(float);
		if (!flag14)
		{
			Action<float> firstDamageTaken = this.m_FirstDamageTaken01;
			bool flag15 = this.m_FirstDamageTaken01 == null;
			num13 = (float)obj2;
			if (!flag15)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v607 @ rcx_v21 (System.Action`1<System.Single>)+18] (should have been resolved before IL gen)");
				num14 = currentDamage01;
				num13 = (nint)(&num14);
			}
			if (OnFirstDamageTaken01 != null)
			{
				OnFirstDamageTaken01.Invoke((nint)(&num14));
				num14 = currentDamage01;
				num13 = (nint)(&num14);
			}
		}
		bool flag16 = 0.001f < currentDamage01;
		bool flag17 = !flag16;
		bool flag18 = currentDamage01 < 0.001f;
		float num15 = currentDamage01 - 0.001f;
		bool flag19 = num15 == 0f;
		bool flag20 = !flag18;
		bool flag21 = !flag19;
		object obj10 = flag21 & flag20;
		object obj11 = obj10 & flag17;
		if (obj11 != null && OnValveFixed != null)
		{
			OnValveFixed.Invoke();
			num13 = 0f;
		}
		if (!forceNotify)
		{
			num6 = currentDamage01;
			ScaleBaseline scaleBaseline2 = ((List<ScaleBaseline>)num13).get_Item(0);
			if ((object)scaleBaseline2 != null)
			{
				return;
			}
		}
		Action<float> damageChanged = this.m_DamageChanged01;
		if (this.m_DamageChanged01 != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v788 @ rcx_v14 (System.Action`1<System.Single>)+18] (should have been resolved before IL gen)");
			num14 = currentDamage01;
		}
		if (OnDamageChanged01Unity != null)
		{
			OnDamageChanged01Unity.Invoke((nint)(&num14));
		}
		bool flag22 = currentDamage01 < 0.999f;
		bool flag23 = !flag22;
		if (flag23 && !lastWasBroken && OnValveDamaged != null)
		{
			OnValveDamaged.Invoke();
		}
		lastWasBroken = flag23;
	}

	private float NormalizeDamage(float dialValue)
	{
		//IL_0105: Expected F4, but got I4
		//IL_00f7: Expected F4, but got I4
		//IL_00ac: Invalid comparison between I4 and F4
		float num = brokenValue;
		bool flag = !(fixedValue > brokenValue);
		float num2 = fixedValue;
		if (!flag)
		{
			num2 = brokenValue;
			num = fixedValue;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18038AA80");
		object obj = default(object);
		float num5;
		if (obj == null)
		{
			bool flag2 = num2 == num;
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"jp short 000000018046670Eh\"");
			if (!flag2)
			{
				float num3 = dialValue - num2;
				float num4 = num - num2;
				num5 = num3 / num4;
				if (!(0f > num5))
				{
					if (num5 > 1f)
					{
						return 1f;
					}
					goto IL_0134;
				}
			}
			num5 = 0f;
			goto IL_0134;
		}
		return 0f;
		IL_0134:
		return num5;
	}

	private void ApplyAudioFeedback(float damage01)
	{
		//IL_002c: Invalid comparison between I4 and F4
		//IL_007f: Expected F4, but got I4
		//IL_00a0: Invalid comparison between I4 and F4
		//IL_00f3: Expected F4, but got I4
		//IL_02f3: Invalid comparison between I4 and F4
		//IL_0139: Expected F4, but got I4
		//IL_01b9: Invalid comparison between I4 and F4
		//IL_0204: Expected F4, but got I4
		if (!(loopAudio != null))
		{
			return;
		}
		float time;
		if (!(0f > damage01))
		{
			bool flag = !(damage01 > 1f);
			time = damage01;
			if (!flag)
			{
				time = 1f;
			}
		}
		else
		{
			time = 0f;
		}
		float num = volumeOverDamage.Evaluate(time);
		float num2;
		if (!(0f > num))
		{
			bool flag2 = !(num > 1f);
			num2 = num;
			if (!flag2)
			{
				num2 = 1f;
			}
		}
		else
		{
			num2 = 0f;
		}
		float num3 = ((0f > num2) ? 0f : ((num2 > 1f) ? 1f : num2));
		float num4 = maxVolume - minVolume;
		float num5 = num4 * num3;
		float num6 = num5 + minVolume;
		float volume = num6 * initialAudioVolume;
		loopAudio.volume = volume;
		if (usePitch)
		{
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
			float num7 = pitchMax - pitchMin;
			float num8 = num7 * num2;
			float pitch = num8 + pitchMin;
			loopAudio.pitch = pitch;
		}
		float volume2 = loopAudio.volume;
		if (volume2 > 0.001f)
		{
			if (!loopAudio.isPlaying)
			{
				loopAudio.Play();
			}
		}
		else if (loopAudio.isPlaying)
		{
			loopAudio.Stop();
		}
	}

	private unsafe void CacheParticleBaselines()
	{
		//IL_00b3: Expected O, but got I
		//IL_00e8: Expected O, but got I4
		//IL_00f1: Expected O, but got I4
		//IL_0382: Unknown result type (might be due to invalid IL or missing references)
		//IL_0387: Expected O, but got Unknown
		//IL_018e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0193: Expected O, but got Unknown
		//IL_043b: Invalid comparison between I4 and F4
		//IL_036c: Expected O, but got Ref
		//IL_0333: Expected F4, but got I4
		//IL_01aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_01af: Expected O, but got Unknown
		//IL_0358: Expected F4, but got I4
		//IL_0294: Expected F4, but got I4
		//IL_02d0: Expected F4, but got I4
		List<ParticleBaseline> list = particleBaselines;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v48 @ rbx_v1 (System.Collections.Generic.List`1<ValveController+ParticleBaseline>)+1C]");
		_ = (nint)0 + (nint)1;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73B0");
		object obj = default(object);
		if (obj == null)
		{
			_ = 0;
		}
		else
		{
			_ = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v48 @ rbx_v1 (System.Collections.Generic.List`1<ValveController+ParticleBaseline>)+18]");
			if ((nint)0 > (nint)0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v48 @ rbx_v1 (System.Collections.Generic.List`1<ValveController+ParticleBaseline>)+10]");
				nint num = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v48 @ rbx_v1 (System.Collections.Generic.List`1<ValveController+ParticleBaseline>)+18]");
				Array.Clear((Array)num, 0, 0);
			}
		}
		if (particleSystems == null)
		{
			return;
		}
		AnimationCurve animationCurve = null;
		ParticleSystem.EmissionModule emissionModule = (ParticleSystem.EmissionModule)0;
		ParticleSystem.EmissionModule emissionModule2 = (ParticleSystem.EmissionModule)0;
		object obj2 = default(object);
		float num3 = default(float);
		object obj5 = default(object);
		float num5 = default(float);
		object obj6 = default(object);
		AnimationCurve animationCurve2 = default(AnimationCurve);
		object obj7 = default(object);
		AnimationCurve animationCurve3 = default(AnimationCurve);
		object obj8 = default(object);
		object obj9 = default(object);
		AnimationCurve animationCurve4 = default(AnimationCurve);
		object obj10 = default(object);
		float num13 = default(float);
		AnimationCurve animationCurve5 = default(AnimationCurve);
		ParticleSystem.EmissionModule emissionModule3 = default(ParticleSystem.EmissionModule);
		while (true)
		{
			ParticleSystem[] array = particleSystems;
			if ((nint)emissionModule2 >= array.Length)
			{
				break;
			}
			if (array[(object)emissionModule2] != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18049AB00");
				ParticleSystem.MinMaxCurve rateOverTime = emissionModule.rateOverTime;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18091B3E0");
				bool flag = obj2 == null;
				float num2;
				if (!flag)
				{
					object obj3 = obj2 - 1;
					if (!flag)
					{
						object obj4 = obj3 - 1;
						if (!flag)
						{
							if ((nint)obj4 != 1)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181D2E690");
								num2 = num3;
							}
							else
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181D25090");
								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181D2E690");
								float num4 = (float)obj5 + num5;
								num2 = num4 * 0.5f;
								float num6 = num5;
							}
						}
						else
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180E583B0");
							float num6;
							if (obj6 != null)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180E583B0");
								float num7 = animationCurve2.Evaluate(0f);
								num6 = num7;
							}
							else
							{
								num6 = 0f;
							}
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808746E0");
							float num9;
							if (obj7 != null)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808746E0");
								float num8 = animationCurve3.Evaluate(0f);
								num9 = num8;
							}
							else
							{
								num9 = 0f;
							}
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181D38490");
							float num10 = num9 + num6;
							float num11 = num10 * 0.5f;
							num2 = num11 * (float)obj8;
						}
					}
					else
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808746E0");
						float num6;
						if (obj9 != null)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808746E0");
							float num12 = animationCurve4.Evaluate(0f);
							num6 = num12;
						}
						else
						{
							num6 = 0f;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181D38490");
						num2 = (float)obj10 * num6;
					}
				}
				else
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181D2E690");
					num2 = num13;
				}
				if (0f > num2)
				{
					num2 = 0f;
				}
				particleBaselines.Add((ParticleBaseline)(&animationCurve5));
				emissionModule = emissionModule3;
			}
			emissionModule2 = (ParticleSystem.EmissionModule)(emissionModule2 + 1);
		}
	}

	private unsafe void ApplyParticleFeedback(float damage01)
	{
		//IL_0033: Invalid comparison between I4 and F4
		//IL_0086: Expected F4, but got I4
		//IL_02ce: Invalid comparison between I4 and F4
		//IL_0366: Invalid comparison between I4 and F4
		//IL_0375: Expected F4, but got I4
		//IL_00de: Expected F4, but got I4
		//IL_03a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a9: Expected F4, but got Unknown
		//IL_03bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c0: Expected F4, but got Unknown
		//IL_03d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d5: Expected O, but got Unknown
		//IL_03f9: Invalid comparison between F4 and O
		//IL_012c: Expected O, but got I4
		//IL_0135: Expected O, but got I4
		//IL_013e: Expected O, but got I4
		//IL_01a6: Expected O, but got Ref
		//IL_029e: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a3: Expected O, but got Unknown
		//IL_0241: Expected O, but got I4
		//IL_024b: Expected O, but got I4
		//IL_01e2: Expected O, but got I4
		//IL_01ec: Expected O, but got I4
		//IL_0274: Expected O, but got I4
		//IL_027e: Expected O, but got I4
		//IL_0215: Expected O, but got I4
		//IL_021f: Expected O, but got I4
		List<ParticleBaseline> list = particleBaselines;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v34 @ rax_v2 (System.Collections.Generic.List`1<ValveController+ParticleBaseline>)+18]");
		if ((nint)0 == 0)
		{
			return;
		}
		float time;
		if (!(0f > damage01))
		{
			bool flag = !(damage01 > 1f);
			time = damage01;
			if (!flag)
			{
				time = 1f;
			}
		}
		else
		{
			time = 0f;
		}
		float num = emissionOverDamage.Evaluate(time);
		bool flag2 = !useQualityEmissionMultiplier;
		float num2 = 1f;
		if (!flag2)
		{
			num2 = cachedQualityEmissionMultiplier;
		}
		if (0f > num)
		{
			num = 0f;
		}
		bool flag3 = 0f > num2;
		float num3 = 0f;
		if (!flag3)
		{
			num3 = num2;
		}
		float num4 = num3 * num;
		float num5 = previousParticleEmissionMultiplier - num4;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
		float num6 = num4 & 0;
		float num7 = previousParticleEmissionMultiplier;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
		float num8 = num7 & 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
		object obj = num5 & 0;
		if (!(num6 > num8))
		{
			num6 = num8;
		}
		float num9 = num6 * 1E-06f;
		float num10 = Mathf.Epsilon * 8f;
		if (!(num9 > num10))
		{
			num9 = num10;
		}
		if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)num9) > System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj))
		{
			return;
		}
		List<ParticleBaseline> list2 = particleBaselines;
		previousParticleEmissionMultiplier = num4;
		ParticleSystem.EmissionModule emissionModule = (ParticleSystem.EmissionModule)0;
		ParticleSystem.EmissionModule emissionModule2 = (ParticleSystem.EmissionModule)0;
		ParticleSystem.EmissionModule emissionModule3 = (ParticleSystem.EmissionModule)0;
		UnityEngine.Object obj2 = default(UnityEngine.Object);
		object obj3 = default(object);
		ParticleSystem.MinMaxCurve minMaxCurve2 = default(ParticleSystem.MinMaxCurve);
		ParticleSystem.EmissionModule emissionModule5 = default(ParticleSystem.EmissionModule);
		while (true)
		{
			ParticleSystem.EmissionModule emissionModule4 = emissionModule3;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v132 @ rax_v11 (System.Collections.Generic.List`1<ValveController+ParticleBaseline>)+18]");
			if ((nint)emissionModule4 >= 0)
			{
				break;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
			if (obj2 != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18049AB00");
				num8 = (float)obj3 * num4;
				ParticleSystem.MinMaxCurve minMaxCurve = new ParticleSystem.MinMaxCurve(num8);
				emissionModule2.rateOverTime = (ParticleSystem.MinMaxCurve)(&minMaxCurve2);
				bool isPlaying = ((ParticleSystem)obj2).isPlaying;
				if (num8 > 0.01f)
				{
					minMaxCurve2 = (ParticleSystem.MinMaxCurve)0;
					minMaxCurve = (ParticleSystem.MinMaxCurve)0;
					emissionModule2 = emissionModule5;
					if (!isPlaying)
					{
						((ParticleSystem)obj2).Play();
						minMaxCurve2 = (ParticleSystem.MinMaxCurve)0;
						minMaxCurve = (ParticleSystem.MinMaxCurve)0;
						emissionModule2 = emissionModule5;
					}
				}
				else
				{
					bool flag4 = !isPlaying;
					minMaxCurve2 = (ParticleSystem.MinMaxCurve)0;
					minMaxCurve = (ParticleSystem.MinMaxCurve)0;
					emissionModule2 = emissionModule5;
					if (!flag4)
					{
						((ParticleSystem)obj2).Stop();
						minMaxCurve2 = (ParticleSystem.MinMaxCurve)0;
						minMaxCurve = (ParticleSystem.MinMaxCurve)0;
						emissionModule2 = emissionModule5;
					}
				}
			}
			list2 = particleBaselines;
			emissionModule = (ParticleSystem.EmissionModule)(emissionModule + 1);
			emissionModule3 = emissionModule;
		}
	}

	private unsafe void CacheScaleBaselines()
	{
		//IL_009d: Expected O, but got I
		//IL_00f6: Expected O, but got I4
		//IL_00ff: Expected O, but got I4
		//IL_0108: Expected O, but got I4
		//IL_0133: Expected O, but got I
		//IL_0156: Expected O, but got I
		//IL_0170: Expected O, but got Ref
		//IL_0188: Unknown result type (might be due to invalid IL or missing references)
		//IL_018d: Expected O, but got Unknown
		//IL_0196: Unknown result type (might be due to invalid IL or missing references)
		//IL_019b: Expected O, but got Unknown
		List<ScaleBaseline> list = scaleBaselines;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v31 @ rdi_v1 (System.Collections.Generic.List`1<ValveController+ScaleBaseline>)+1C]");
		_ = (nint)0 + (nint)1;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73B0");
		object obj = default(object);
		if (obj == null)
		{
			_ = 0;
		}
		else
		{
			_ = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v31 @ rdi_v1 (System.Collections.Generic.List`1<ValveController+ScaleBaseline>)+18]");
			if ((nint)0 > (nint)0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v31 @ rdi_v1 (System.Collections.Generic.List`1<ValveController+ScaleBaseline>)+10]");
				nint num = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v31 @ rdi_v1 (System.Collections.Generic.List`1<ValveController+ScaleBaseline>)+18]");
				Array.Clear((Array)num, 0, 0);
			}
		}
		if (scaleTargets == null)
		{
			return;
		}
		Transform[] array = scaleTargets;
		if (array.Length == 0)
		{
			return;
		}
		object obj2 = 32;
		object obj3 = 0;
		object obj4 = 0;
		UnityEngine.Object obj5 = default(UnityEngine.Object);
		while ((nint)obj4 < array.Length)
		{
			Transform[] array2 = scaleTargets;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v94 @ r14_v7+v91 @ rdi_v8 (UnityEngine.Transform[])]");
			if ((UnityEngine.Object)0 != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v94 @ r14_v7+v91 @ rdi_v8 (UnityEngine.Transform[])]");
				Vector3 localScale = ((Transform)0).localScale;
				scaleBaselines.Add((ScaleBaseline)(&obj5));
			}
			array = scaleTargets;
			obj3++;
			obj2 += 8;
			obj4 = obj3;
		}
	}

	private unsafe void ApplyScaleFeedback(float damage01)
	{
		//IL_0042: Invalid comparison between I4 and F4
		//IL_0095: Expected F4, but got I4
		//IL_00c0: Expected O, but got I4
		//IL_00c9: Expected O, but got I4
		//IL_0134: Expected O, but got Ref
		//IL_014c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0151: Expected O, but got Unknown
		if (!useScaleFeedback)
		{
			return;
		}
		List<ScaleBaseline> list = scaleBaselines;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v48 @ rax_v3 (System.Collections.Generic.List`1<ValveController+ScaleBaseline>)+18]");
		if ((nint)0 == 0)
		{
			return;
		}
		float time;
		if (!(0f > damage01))
		{
			bool flag = !(damage01 > 1f);
			time = damage01;
			if (!flag)
			{
				time = 1f;
			}
		}
		else
		{
			time = 0f;
		}
		float num = scaleMultiplierOverDamage.Evaluate(time);
		List<ScaleBaseline> list2 = scaleBaselines;
		object obj = 0;
		object obj2 = 0;
		UnityEngine.Object obj4 = default(UnityEngine.Object);
		float num2 = default(float);
		while (true)
		{
			object obj3 = obj2;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v197 @ rax_v8 (System.Collections.Generic.List`1<ValveController+ScaleBaseline>)+18]");
			if ((nint)obj3 < 0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
				if (obj4 != null)
				{
					((Transform)obj4).localScale = (Vector3)(&num2);
				}
				list2 = scaleBaselines;
				obj++;
				obj2 = obj;
				continue;
			}
			break;
		}
	}

	private void RefreshQualityEmissionMultiplier(bool force)
	{
		//IL_0129: Expected I4, but got I8
		//IL_0138: Invalid comparison between I4 and F4
		//IL_0147: Expected F4, but got I4
		//IL_009a: Expected O, but got I4
		//IL_00a3: Expected O, but got I4
		//IL_00fd: Expected F4, but got I
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Expected O, but got Unknown
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Expected O, but got Unknown
		if (useQualityEmissionMultiplier)
		{
			int qualityLevel = QualitySettings.GetQualityLevel();
			if (!force && qualityLevel == lastQualityLevelIndex)
			{
				return;
			}
			bool flag = qualityEmissionMultipliers == null;
			float num = defaultQualityEmissionMultiplier;
			lastQualityLevelIndex = qualityLevel;
			if (!flag)
			{
				QualityMultiplierEntry[] array = qualityEmissionMultipliers;
				object obj = 32;
				object obj2 = 0;
				while ((nint)obj2 < array.Length)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v150 @ rax_v6+v94 @ r10_v2 (QualityMultiplierEntry[])]");
					if ((nint)0 != qualityLevel)
					{
						obj2++;
						obj += 8;
						continue;
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v94 @ r10_v2 (QualityMultiplierEntry[])+24+v107 @ rcx_v6*8]");
					num = 0f;
					break;
				}
			}
			bool flag2 = 0f > num;
			float num2 = 0f;
			if (!flag2)
			{
				num2 = num;
			}
			cachedQualityEmissionMultiplier = num2;
			ApplyParticleFeedback(currentDamage01);
		}
		else
		{
			cachedQualityEmissionMultiplier = 1f;
			lastQualityLevelIndex = -1;
		}
	}

	public float GetDamage01()
	{
		return currentDamage01;
	}

	public void Damage()
	{
		DamageValve();
	}

	public void ForceFix()
	{
		if (dial != null)
		{
			dial.SetDialValue(fixedValue);
			UpdateFromDial();
		}
	}

	public void SetDamage01(float damage01)
	{
		//IL_002c: Invalid comparison between I4 and F4
		//IL_007f: Expected F4, but got I4
		//IL_0121: Invalid comparison between I4 and F4
		//IL_00bb: Expected F4, but got I4
		if (!(dial != null))
		{
			return;
		}
		float num;
		if (!(0f > damage01))
		{
			bool flag = !(damage01 > 1f);
			num = damage01;
			if (!flag)
			{
				num = 1f;
			}
		}
		else
		{
			num = 0f;
		}
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
		float num2 = brokenValue - fixedValue;
		float num3 = num2 * num;
		float dialValue = num3 + fixedValue;
		dial.SetDialValue(dialValue);
		UpdateFromDial();
	}

	public void DamageValve()
	{
		if (dial != null)
		{
			dial.SetDialValue(brokenValue);
			UpdateFromDial();
		}
	}

	public void ForceFixValve()
	{
		if (dial != null)
		{
			dial.SetDialValue(fixedValue);
			UpdateFromDial();
		}
	}

	public ValveController()
	{
		//IL_00f0: Expected I4, but got I8
		systemId = "Default";
		brokenValue = 100f;
		continuousEffectUpdate = true;
		AnimationCurve animationCurve = AnimationCurve.Linear(0f, 0f, 1f, 1f);
		volumeOverDamage = animationCurve;
		maxVolume = 1f;
		usePitch = true;
		pitchMin = 1f;
		pitchMax = 1.2f;
		emissionOverDamage = AnimationCurve.Linear(0f, 0f, 1f, 1f);
		useQualityEmissionMultiplier = true;
		defaultQualityEmissionMultiplier = 1f;
		autoRefreshQualityMultiplier = true;
		qualityRefreshIntervalSeconds = 1f;
		scaleMultiplierOverDamage = AnimationCurve.Linear(0f, 1f, 1f, 1f);
		useScaleFeedback = true;
		initialAudioVolume = 1f;
		previousParticleEmissionMultiplier = -1f;
		cachedQualityEmissionMultiplier = 1f;
		lastQualityLevelIndex = -1;
		particleBaselines = new List<ParticleBaseline>();
		scaleBaselines = new List<ScaleBaseline>();
		base._002Ector();
	}
}
