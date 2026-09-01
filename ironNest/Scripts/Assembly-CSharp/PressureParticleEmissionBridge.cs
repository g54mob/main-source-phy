using UnityEngine;

[AddComponentMenu("Espresso/Pressure Particle Emission Bridge")]
public class PressureParticleEmissionBridge : MonoBehaviour
{
	[Header("References")]
	[Tooltip("The EspressoBrewingController whose SimPressure value is watched.\n\nRequired. The bridge does nothing if this is null.")]
	[SerializeField]
	private EspressoBrewingController brewingController;

	[Tooltip("The ParticleSystem whose emission rate over time and start size\nwill be driven by SimPressure.\n\nRequired. The bridge does nothing if this is null.\n\nNote: the ParticleSystem's Emission module must be enabled for\nthe rate override to have any visible effect.")]
	[SerializeField]
	private ParticleSystem targetParticleSystem;

	[Header("Pressure Range")]
	[Tooltip("SimPressure value (bar) at or below which both emission rate and\nstart size are clamped to their respective minimum values.\n\nSafe default: 0.0")]
	[SerializeField]
	private float pressureMin;

	[Tooltip("SimPressure value (bar) at or above which both emission rate and\nstart size are clamped to their respective maximum values.\n\nShould match or be less than EspressoBrewingController.pressureMax.\n\nSafe default: 15.0")]
	[SerializeField]
	private float pressureMax;

	[Header("Emission Rate")]
	[Tooltip("Emission rate over time (particles/second) when SimPressure is at\nor below pressureMin.\n\nSet to 0 to fully suppress particles at low pressure.\n\nSafe default: 0.0")]
	[SerializeField]
	private float minEmissionRate;

	[Tooltip("Emission rate over time (particles/second) when SimPressure is at\nor above pressureMax.\n\nSafe default: 50.0")]
	[SerializeField]
	private float maxEmissionRate;

	[Header("Start Size")]
	[Tooltip("Particle start size when SimPressure is at or below pressureMin.\n\nSafe default: 0.05")]
	[SerializeField]
	private float minStartSize;

	[Tooltip("Particle start size when SimPressure is at or above pressureMax.\n\nSafe default: 0.25")]
	[SerializeField]
	private float maxStartSize;

	[Header("Smoothing")]
	[Tooltip("Time in seconds over which the emission rate smoothly follows the\ntarget value. Uses SmoothDamp — higher values = slower response.\n\n0 = instant snap (no smoothing).\nPlaytest range: 0.0–1.0.  Safe default: 0.15")]
	[SerializeField]
	private float emissionSmoothTime;

	[Tooltip("Time in seconds over which the start size smoothly follows the\ntarget value. Uses SmoothDamp — higher values = slower response.\n\n0 = instant snap (no smoothing).\nPlaytest range: 0.0–1.0.  Safe default: 0.15")]
	[SerializeField]
	private float sizeSmoothTime;

	[Header("Debug")]
	[Tooltip("If true, logs the current pressure, mapped t, target emission rate,\nand target start size every frame to the Console.\nVery verbose — disable in production.\n\nSafe default: false")]
	[SerializeField]
	private bool debugLogs;

	private ParticleSystem.EmissionModule _emission;

	private ParticleSystem.MainModule _main;

	private float _currentEmissionRate;

	private float _currentStartSize;

	private float _emissionVelocity;

	private float _sizeVelocity;

	private bool _isReady;

	private void Awake()
	{
	}

	private void Update()
	{
	}
}
