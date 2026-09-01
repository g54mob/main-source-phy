using UnityEngine;

public class LookAtVelocityArrow : MonoBehaviour
{
	public Transform projectile;

	public Rigidbody myRigidbody;

	public float lookAtTorquePower = 100f;

	private Vector3 angley;

	private Transform myTransform;

	private void Start()
	{
		projectile = base.transform;
		ResetBody();
	}

	public void ResetBody()
	{
		myRigidbody = GetComponent<Rigidbody>();
	}

	private void FixedUpdate()
	{
		myRigidbody.AddTorque(Vector3.Cross(projectile.forward, myRigidbody.velocity) * lookAtTorquePower);
	}
}
