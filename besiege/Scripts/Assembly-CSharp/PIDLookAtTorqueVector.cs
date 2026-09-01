using UnityEngine;

public class PIDLookAtTorqueVector : MonoBehaviour
{
	public float pFactor;

	public float iFactor;

	public float dFactor;

	public Vector3 integral;

	public Vector3 lastError;

	public float power = 1f;

	public float velocityStabilise = 1f;

	public Rigidbody pendulum;

	public Rigidbody baseEnt;

	public Vector3 targetVector = Vector3.up;

	private Vector3 UpdateLoopy(Vector3 currentError, float timeFrame)
	{
		integral += currentError * timeFrame;
		Vector3 vector = (currentError - lastError) / timeFrame;
		lastError = currentError;
		return currentError * pFactor + integral * iFactor + vector * dFactor;
	}

	private void FixedUpdate()
	{
		baseEnt.AddForce(pendulum.transform.up * power);
	}
}
