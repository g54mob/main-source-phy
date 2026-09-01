using UnityEngine;

public sealed class EnginePowerController : MonoBehaviour, IFloatValueProvider
{
	[Header("Source")]
	[Tooltip("The DieselEngineController to read from.\nPower is driven by its live FuelMixtureSystemValue (0–1) while\nEnginesRunning is true. When EnginesRunning is false, the target\nis forced to 0 and Power lerps down to zero.\n\nIf left unassigned, Power remains at 0.")]
	[SerializeField]
	private DieselEngineController dieselEngine;

	[Header("Smoothing")]
	[Tooltip("How quickly Power rises toward the target value (units per second, on a 0–1 scale).\n\nThis applies whenever the target is higher than the current Power,\nincluding initial spool-up after ignition.\n\n0.1   Very slow — ~10 seconds to go from 0 to full power.\n0.5   Moderate — roughly 2 seconds from 0 to full power.\n1.0   Fast — reaches full power in about 1 second.\n10+   Near-instant.\n\nSafe default: 0.3")]
	[SerializeField]
	[Min(0.001f)]
	private float riseSpeed;

	[Tooltip("How quickly Power falls toward the target value (units per second, on a 0–1 scale).\n\nThis applies whenever the target is lower than the current Power,\nincluding the wind-down to zero after the engine stops.\n\n0.1   Very slow — engine takes ~10 seconds to fully wind down.\n0.5   Moderate — winds down in roughly 2 seconds.\n1.0   Fast — drops to zero in about 1 second.\n10+   Near-instant.\n\nSafe default: 0.5")]
	[SerializeField]
	[Min(0.001f)]
	private float fallSpeed;

	[Header("Debug")]
	[Tooltip("Live read-only. The raw target Power is aiming for this frame (0–1).\nEqual to FuelMixtureSystemValue when the engine is running, 0 when stopped.")]
	[SerializeField]
	private float _debugTargetPower;

	[Tooltip("Live read-only. The current smoothed Power value (0–1).")]
	[SerializeField]
	private float _debugCurrentPower;

	public float Power { get; private set; }

	public float ClampedPower => 0f;

	public string ProviderName => null;

	public float GetFloatValue()
	{
		return 0f;
	}

	private void Update()
	{
	}
}
