using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;

[DisallowMultipleComponent]
public class PrinterAlertLight : MonoBehaviour
{
	public enum PlayMode
	{
		Inactive = 0,
		AlertCurve = 1,
		IdleCurve = 2
	}

	[Header("Point Light")]
	[Tooltip("The Unity Point Light to drive. If left empty, the component searches this GameObject for a Light component on Awake.")]
	public Light pointLight;

	[Tooltip("Light intensity when the active curve evaluates to 1.0.")]
	public float peakIntensity;

	[Tooltip("Light intensity when the active curve evaluates to 0.0. Usually 0, but raise this for a light that dims rather than fully cuts out.")]
	public float baseIntensity;

	[Header("Emissive Material (URP Lit)")]
	[Tooltip("Renderer whose material emissive channel will be driven. If left empty, the component searches this GameObject for a Renderer on Awake.")]
	public Renderer emissiveRenderer;

	[Tooltip("Index of the material on the Renderer to target. 0 = first (default).")]
	public int materialIndex;

	[ColorUsage(false, true)]
	[Tooltip("Emissive colour when the active curve evaluates to 1.0. HDR — boost the exposure value in the colour picker to control bloom intensity.")]
	public Color peakEmissiveColor;

	[ColorUsage(false, true)]
	[Tooltip("Emissive colour when the active curve evaluates to 0.0. Usually Color.black (emission fully off).")]
	public Color baseEmissiveColor;

	[Header("Lens Flare (SRP)")]
	[Tooltip("The SRP Lens Flare component to drive. If left empty, the component searches this GameObject for a LensFlareComponentSRP on Awake.")]
	public LensFlareComponentSRP lensFlare;

	[Tooltip("Lens flare intensity scale applied on top of the active curve sample.\nFlare intensity = curve sample × this value.\nDefault: 0.1 (curve at 1.0 → flare at 0.1). Set to 0 to disable.")]
	public float lensFlareIntensityScale;

	[Header("Alert Curve")]
	[Tooltip("Played ONCE in full when this alert is triggered (initial attention-grab).\n\n• X axis = time in seconds. Duration is read from the last key — no separate field needed.\n• Y axis = normalised brightness (0 = base, 1 = peak).\n\nExamples:\n  Single flash  — (0,0) (0.05,1) (0.15,1) (0.2,0)\n  Triple pulse  — three quick 0→1→0 bumps over ~0.6s\n  Slow swell    — smooth ease from 0 to 1 over 1s")]
	public AnimationCurve alertCurve;

	[Header("Idle Curve")]
	[Tooltip("Looped continuously after the alert curve completes, representing the persistent active alert state.\n\n• X axis = time in seconds. Duration is read from the last key.\n• Y axis = normalised brightness (0 = base, 1 = peak).\n\nExamples:\n  Always on     — flat line at Y=1 from X=0 to X=1\n  Slow pulse    — smooth 0→1→0 sine over X=0 to X=2\n  Sharp strobe  — stepped keys: (0,1) (0.1,1) (0.1,0) (0.5,0) (0.5,1)…")]
	public AnimationCurve idleCurve;

	[Header("Events")]
	[Tooltip("Invoked when PlayAlertCurve() is called — the alert curve begins playing.\nUse this to play a one-shot alert sound.")]
	public UnityEvent onAlertStart;

	[Tooltip("Invoked when the alert curve finishes its single pass and the idle curve begins.\nUse this to play a looping idle sound, stop the one-shot sound, etc.")]
	public UnityEvent onAlertCurveComplete;

	[Tooltip("Invoked when Deactivate() is called — the light powers off entirely.\nUse this to stop any looping idle sound.")]
	public UnityEvent onAlertStop;

	[Header("Debug")]
	[Tooltip("Manually set the play mode at runtime for testing. Has no effect in Edit mode.\nInactive     — powers the light off.\nAlertCurve   — plays the alert curve once then transitions to IdleCurve.\nIdleCurve    — starts the idle curve looping immediately.")]
	[SerializeField]
	private PlayMode debugPlayMode;

	private PlayMode _mode;

	private float _elapsed;

	private float _alertCurveDuration;

	private float _idleCurveDuration;

	private Material _materialInstance;

	private Action _onAlertCurveDone;

	private static readonly int EmissionColorId;

	public bool IsActive => false;

	public bool IsPlayingAlertCurve => false;

	public bool IsPlayingIdleCurve => false;

	private void Awake()
	{
	}

	private void OnValidate()
	{
	}

	private void Update()
	{
	}

	public void PlayAlertCurve(Action onDone = null)
	{
	}

	public void PlayIdleCurve()
	{
	}

	public void Deactivate()
	{
	}

	private void ApplyBrightness(float t)
	{
	}

	private void SetLightEnabled(bool state)
	{
	}

	private static float GetCurveDuration(AnimationCurve curve)
	{
		return 0f;
	}
}
