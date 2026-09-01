using UnityEngine;

[DisallowMultipleComponent]
public class CorrectionDirectionTierConfig : MonoBehaviour
{
	[Header("Pointer Visual")]
	[Tooltip("The GameObject representing this tier's pointer graphics (child of the shared arrow root). Only one tier's pointer visual is enabled at a time.")]
	public GameObject pointerVisual;

	[Header("Directional Error")]
	[Tooltip("Half-angle in degrees of the inaccuracy cone. 0 = perfectly accurate pointer; larger values produce wider error arcs.")]
	public float errorAngleDegrees;

	[Tooltip("If enabled, a new random angular error is rolled every liveUpdate tick instead of only when the target changes.")]
	public bool reRollEachLiveUpdate;

	[Tooltip("If true, the same error offset is kept as long as the same target is selected (prevents jitter).")]
	public bool stablePerTarget;

	private void OnEnable()
	{
	}

	private void OnDisable()
	{
	}
}
