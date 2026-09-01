using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;

[DisallowMultipleComponent]
public class ValveController : MonoBehaviour, IValve
{
	[Serializable]
	private struct QualityMultiplierEntry
	{
		[Tooltip("Unity Quality level index (0..QualitySettings.names.Length-1).\nThis maps to the order in Project Settings > Quality.")]
		public int qualityLevelIndex;

		[Tooltip("Emission multiplier applied when this quality level is active.\n1 = no change; 0.5 = half emission; 0 = no emission.\nRecommended example:\n- Low (index 0): 0.35\n- Medium (index 1): 0.65\n- High (index 2): 1.0")]
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

	[Header("System Binding")]
	[Tooltip("Optional explicit reference to the pressure system manager for this valve.\nIf left empty, the valve will try to auto-bind using the System ID below at runtime.")]
	[SerializeField]
	private HighPressureSystemManager systemManager;

	[Tooltip("Identifier of the high pressure system this valve belongs to.\nIf no explicit manager reference is set, the valve will try to find a manager with this System ID.\nRules: Case-sensitive, non-empty string. Keep short and consistent across all valves in the same system.\nExamples: \"Default\", \"ReactorA\", \"UpperDeck\"")]
	[SerializeField]
	private string systemId;

	[Header("Dial")]
	[Tooltip("Reference to the DialInteractable that physically represents this valve.\nRequired. Must be configured in Limited mode. The Dial's Min/Max Output Value range should match Fixed Value and Broken Value below.")]
	[SerializeField]
	private DialInteractable dial;

	[Tooltip("The DialInteractable output value that represents a fully fixed valve.\nThis should match the DialInteractable's Min Output Value to keep damage normalization correct.\nExample: 0")]
	[SerializeField]
	private float fixedValue;

	[Tooltip("The DialInteractable output value that represents a fully broken (damaged) valve.\nThis should match the DialInteractable's Max Output Value to keep damage normalization correct.\nExample: 100")]
	[SerializeField]
	private float brokenValue;

	[Tooltip("If true, the ValveController will continuously sample the Dial value every frame to update effects.\nRecommended: true. Ensures smooth audio/visual feedback even when the dial uses detent smoothing.")]
	[SerializeField]
	private bool continuousEffectUpdate;

	[Header("Initial State")]
	[Tooltip("If true, the valve starts damaged at play (Dial is set to Broken Value). If false, starts fixed.")]
	[SerializeField]
	private bool startDamaged;

	[Header("Audio Feedback")]
	[Tooltip("Optional looping AudioSource representing leaking/pressure noise.\nVolume and pitch are controlled by damage below.")]
	[SerializeField]
	private AudioSource loopAudio;

	[Tooltip("Volume curve (Y) over damage (X in [0..1]). 0 damage => typically 0 volume, 1 damage => max volume.\nDefault: linear.")]
	[SerializeField]
	private AnimationCurve volumeOverDamage;

	[Tooltip("Final volume range applied to the AudioSource after curve evaluation.\nMinVolume at 0 damage, MaxVolume at 1 damage (modulated by Volume Curve).")]
	[SerializeField]
	[Min(0f)]
	private float minVolume;

	[Tooltip("Final volume range applied to the AudioSource after curve evaluation.\nMinVolume at 0 damage, MaxVolume at 1 damage (modulated by Volume Curve).")]
	[SerializeField]
	[Min(0f)]
	private float maxVolume;

	[Tooltip("Optional pitch range to add urgency as damage increases.\nPitch at 0 damage = PitchMin, at 1 damage = PitchMax (modulated by the same curve as Volume).")]
	[SerializeField]
	private bool usePitch;

	[Tooltip("Pitch value used when valve is fully fixed (damage = 0).")]
	[SerializeField]
	private float pitchMin;

	[Tooltip("Pitch value used when valve is fully broken (damage = 1).")]
	[SerializeField]
	private float pitchMax;

	[Header("Particle Feedback")]
	[Tooltip("Optional particle systems representing steam/sparks/leaks.\nTheir emission rate will be scaled by the Emission Over Damage curve as a MULTIPLIER of their configured baseline emission.\nSetup: configure each ParticleSystem's emission rate (inspector). The controller will read that baseline at Awake() and multiply it.\nNotes: The controller samples a representative baseline value for each ParticleSystem (constant or curve sample at time=0).\nIf you use Min/Max curves, the sampled baseline uses a simple representative value (average or sample) — adjust in editor if you want different.\nExample: Particle A baseline = 100, Particle B baseline = 5. EmissionOverDamage curve 0->0, 1->1 will cause A->100 and B->5 at full damage.")]
	[SerializeField]
	private ParticleSystem[] particleSystems;

	[Tooltip("Multiplier curve (Y) over damage (X in [0..1]). The evaluated value multiplies each particle system's baseline emission.\nExample: a curve (0->0, 1->1) will make no emission at zero damage and full baseline emission at full damage.\nDefault: linear from 0 to 1. Use values >1 to boost above baseline, or values between 0..1 to reduce.")]
	[SerializeField]
	private AnimationCurve emissionOverDamage;

	[Header("Quality (Particle Emission Bridge)")]
	[Tooltip("When enabled, particle emission is multiplied by a factor based on the currently active Unity Quality level.\nFinal emission formula per ParticleSystem:\n  targetRate = baselineRate * emissionOverDamage(damage01) * qualityEmissionMultiplier\nWhere qualityEmissionMultiplier is selected from Quality Emission Multipliers.\nNotes:\n- This affects ONLY this ValveController's ParticleSystems; it does not change global Quality Settings.\n- If no multiplier is found for the current Quality level, the Default Quality Emission Multiplier is used.")]
	[SerializeField]
	private bool useQualityEmissionMultiplier;

	[Tooltip("Fallback multiplier used when the current Quality level index has no entry in Quality Emission Multipliers.\n1 = no change; 0.5 = half emission; 0 = no emission.\nSafe default: 1.")]
	[SerializeField]
	[Min(0f)]
	private float defaultQualityEmissionMultiplier;

	[Tooltip("Per-quality emission multipliers.\nHow it works:\n- The current quality level is QualitySettings.GetQualityLevel().\n- If an entry matches that index, its Multiplier is used.\n- If multiple entries match, the first match in this list is used.\nSetup tip:\n- Check Project Settings > Quality for the quality level order/indices.")]
	[SerializeField]
	private QualityMultiplierEntry[] qualityEmissionMultipliers;

	[Tooltip("If true, the script re-checks QualitySettings.GetQualityLevel() periodically at runtime and updates emission.\nEnable this if your game can change quality during play.\nIf false, the multiplier is captured at Start() and remains fixed.\nSafe default: true.")]
	[SerializeField]
	private bool autoRefreshQualityMultiplier;

	[Tooltip("How often (in seconds) to re-check the current Quality level when Auto Refresh is enabled.\nUse a small value (e.g. 0.5) if quality can change via options menu; use a larger value (e.g. 2-5) to minimize overhead.\nSafe default: 1.")]
	[SerializeField]
	[Min(0.1f)]
	private float qualityRefreshIntervalSeconds;

	[Header("Scale Feedback (new)")]
	[Tooltip("When enabled, will scale the listed target Transforms based on current damage.\nAssign one or more Transforms to affect. If empty, no scale feedback is applied.")]
	[SerializeField]
	private Transform[] scaleTargets;

	[Tooltip("Curve mapping damage [0..1] -> scale multiplier (float). The evaluated value is multiplied with each target's initial localScale.\nDefault: constant 1 (no change). Example: curve (0->1, 1->1.5) will make targets 1.5x at full damage.\nNotes: The curve Y value is treated as a scalar multiplier and can be >1 (grow) or <1 (shrink).")]
	[SerializeField]
	private AnimationCurve scaleMultiplierOverDamage;

	[Tooltip("If true, scale feedback will be applied. If false, scaling is skipped even when Scale Targets are assigned.")]
	[SerializeField]
	private bool useScaleFeedback;

	[Header("Events")]
	[Tooltip("Raised when the valve damage [0..1] changes. 0 = fixed, 1 = fully broken. Argument: damage [0..1].")]
	public UnityEvent<float> OnDamageChanged01Unity;

	[Tooltip("Raised when the valve reaches fully broken state (damage ~ 1).")]
	public UnityEvent OnValveDamaged;

	[Tooltip("Raised when the valve reaches fully fixed state (damage ~ 0).")]
	public UnityEvent OnValveFixed;

	[Tooltip("Raised the first time the valve transitions from zero damage to any non-zero damage.\nArgument: current damage [0..1]. This fires when damage goes from ~0 to >0 (thresholded), and will not re-fire while the valve remains >0.\nIf the valve returns to zero (fixed) later, the event will fire again on the next 0->>0 transition.")]
	public UnityEvent<float> OnFirstDamageTaken01;

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

	public float Damage01 => 0f;

	public float CurrentDialValue => 0f;

	public event Action<float> DamageChanged01
	{
		[CompilerGenerated]
		add
		{
		}
		[CompilerGenerated]
		remove
		{
		}
	}

	public event Action<float> FirstDamageTaken01
	{
		[CompilerGenerated]
		add
		{
		}
		[CompilerGenerated]
		remove
		{
		}
	}

	event Action<float> IValve.DamageChanged01
	{
		add
		{
		}
		remove
		{
		}
	}

	private void Reset()
	{
	}

	private void OnValidate()
	{
	}

	private void Awake()
	{
	}

	private void Start()
	{
	}

	private void OnDisable()
	{
	}

	private void Update()
	{
	}

	private void HandleBeginDialDrag()
	{
	}

	private void HandleEndDialDrag()
	{
	}

	private void HandleDialValueChanged(float _)
	{
	}

	private void UpdateFromDial(bool forceNotify = false)
	{
	}

	private float NormalizeDamage(float dialValue)
	{
		return 0f;
	}

	private void ApplyAudioFeedback(float damage01)
	{
	}

	private void CacheParticleBaselines()
	{
	}

	private void ApplyParticleFeedback(float damage01)
	{
	}

	private void CacheScaleBaselines()
	{
	}

	private void ApplyScaleFeedback(float damage01)
	{
	}

	private void RefreshQualityEmissionMultiplier(bool force)
	{
	}

	public float GetDamage01()
	{
		return 0f;
	}

	public void Damage()
	{
	}

	public void ForceFix()
	{
	}

	public void SetDamage01(float damage01)
	{
	}

	[ContextMenu("Damage Valve")]
	public void DamageValve()
	{
	}

	[ContextMenu("Force Fix Valve")]
	public void ForceFixValve()
	{
	}
}
