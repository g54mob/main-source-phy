using UnityEngine;

public class Compensation : MonoBehaviour
{
	public float forwardCompensation;

	public float upwardsRangeCompensation;

	public float rangePow = 1.3f;

	public float velocityPrediction = 1f;

	public float positionPredictionAmount = 1f;

	public float clampDistance = float.PositiveInfinity;

	public Vector3 GetCompensation(Vector3 targetRigPosition, Vector3 targetRigVelocity, float predictionAmount = 1f)
	{
		Vector3 vector = Vector3.ClampMagnitude(targetRigVelocity, 12f);
		vector.y *= 0.5f;
		float num = Mathf.Pow(Vector3.Distance(base.transform.position, targetRigPosition + vector * velocityPrediction), rangePow);
		if (clampDistance != float.PositiveInfinity)
		{
			num = Mathf.Clamp(num, 0f, clampDistance);
		}
		return Vector3.Lerp((targetRigPosition + vector * num * 0.02f * positionPredictionAmount - base.transform.position).normalized + Vector3.up * num * upwardsRangeCompensation * 0.001f, base.transform.forward, predictionAmount);
	}
}
