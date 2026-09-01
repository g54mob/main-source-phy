using UnityEngine;

[AddComponentMenu("Audio/FMODOps/Float Ramp 0→1")]
public class FloatRamp01 : MonoBehaviour, IFloatValueProvider
{
	[Header("Ramp Settings")]
	[Tooltip("How long (in seconds) it takes the value to travel from 0 to 1 after this component is enabled.\n\n• Any value > 0 is valid.\n• Setting this to a very small number (e.g. 0.01) produces a near-instant snap to 1.\n\nExamples:\n  5   → reaches 1 after five seconds\n  30  → reaches 1 after thirty seconds")]
	[Min(0.001f)]
	[SerializeField]
	private float rampDuration;

	[Header("Diagnostics (Read-Only)")]
	[Tooltip("Current ramp value in the 0–1 range. This is the value FMODParameterSetter reads each frame.")]
	[SerializeField]
	private float currentValue;

	[Tooltip("Elapsed time (seconds) since the component was last enabled. Resets to 0 on each OnEnable.")]
	[SerializeField]
	private float elapsedTime;

	public float GetFloatValue()
	{
		return 0f;
	}

	private void OnEnable()
	{
	}

	private void Update()
	{
	}
}
