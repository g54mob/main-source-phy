using UnityEngine;

public class PIDLookAtTorquePOS : MonoBehaviour
{
	public float pFactor;

	public float iFactor;

	public float dFactor;

	public Vector3 integral;

	public Vector3 lastError;

	public float power = 1f;

	public Transform target;

	public Vector3 targetVector = Vector3.up;

	private void Start()
	{
		GetComponent<Rigidbody>().inertiaTensor = Vector3.one;
	}

	private Vector3 UpdateLoopy(Vector3 currentError, float timeFrame)
	{
		integral += currentError * timeFrame;
		Vector3 vector = (currentError - lastError) / timeFrame;
		lastError = currentError;
		return currentError * pFactor + integral * iFactor + vector * dFactor;
	}

	private void FixedUpdate()
	{
		Vector3 currentError = GetComponent<Rigidbody>().angularVelocity * -1f;
		Vector3 torque = UpdateLoopy(currentError, Time.deltaTime);
		GetComponent<Rigidbody>().AddTorque(torque);
		Vector3 rhs = target.position - base.transform.position;
		Vector3 up = base.transform.up;
		Vector3 currentError2 = Vector3.Cross(up, rhs);
		Vector3 vector = UpdateLoopy(currentError2, Time.deltaTime);
		GetComponent<Rigidbody>().AddTorque(vector * power);
	}
}
