using UnityEngine;

public class CorrectionDirectionTierConfig : MonoBehaviour
{
	public GameObject pointerVisual;

	public float errorAngleDegrees = 45f;

	public bool reRollEachLiveUpdate;

	public bool stablePerTarget = true;

	private void OnEnable()
	{
		ImpactCorrectionTierController.ScheduleGlobalReevaluate();
	}

	private void OnDisable()
	{
		ImpactCorrectionTierController.ScheduleGlobalReevaluate();
	}
}
