using System.Collections;
using UnityEngine;

public class PIDLookAtTorque : MonoBehaviour
{
	public float pFactor;

	public float iFactor;

	public float dFactor;

	public Vector3 integral;

	public Vector3 lastError;

	public float power = 1f;

	public float actualPower = 1f;

	public Transform target;

	public Vector3 targetVector = Vector3.up;

	private void Start()
	{
		actualPower = power;
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
		Vector3 forward = target.forward;
		Vector3 forward2 = base.transform.forward;
		Vector3 currentError2 = Vector3.Cross(forward2, forward);
		Vector3 vector = UpdateLoopy(currentError2, Time.deltaTime);
		GetComponent<Rigidbody>().AddTorque(vector * actualPower);
	}

	private IEnumerator LerpPowerIn(float speedy)
	{
		float cTime = 0f;
		float rate = 1f / speedy;
		while (cTime < 1f)
		{
			cTime += Time.deltaTime * rate;
			actualPower = Mathf.Lerp(0f, power, cTime);
			yield return null;
		}
	}
}
