using UnityEngine;

[DisallowMultipleComponent]
public class CorrectionPointerVariantSelector : MonoBehaviour
{
	[Tooltip("Parent that contains one child GameObject per direction tier (in ascending order: Tier1, Tier2, Tier3, ...).")]
	public Transform arrowRoot;

	[Tooltip("If true and the controller is not yet available this frame, will retry for a short time until found.")]
	public bool retryUntilControllerFound;

	[Tooltip("Seconds between retry attempts when controller isn't ready.")]
	public float retryInterval;

	[Tooltip("Optional debug logging.")]
	public bool debugLogs;

	private bool _applied;

	private float _nextRetryTime;

	private void OnEnable()
	{
	}

	private void Update()
	{
	}

	private void TryApply()
	{
	}

	private int GetActiveDirectionTierSiblingIndex(ImpactCorrectionTierController controller)
	{
		return 0;
	}

	private void ActivateChild(int index)
	{
	}
}
