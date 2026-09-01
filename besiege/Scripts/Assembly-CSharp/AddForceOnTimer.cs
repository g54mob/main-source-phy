using UnityEngine;

public class AddForceOnTimer : MonoBehaviour
{
	public Rigidbody MyRigidbody;

	public float Force = 10f;

	public float RepeTime = 2f;

	public Vector3 axis = Vector3.up;

	private void Start()
	{
		MyRigidbody.WakeUp();
		InvokeRepeating("DoForce", 2f, RepeTime);
	}

	private void DoForce()
	{
		MyRigidbody.AddForce(axis * Force);
	}
}
