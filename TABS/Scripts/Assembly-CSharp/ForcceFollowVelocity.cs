using UnityEngine;

public class ForcceFollowVelocity : MonoBehaviour
{
	public float force = 1f;

	private Rigidbody rig;

	private void Start()
	{
		rig = GetComponent<Rigidbody>();
	}

	private void FixedUpdate()
	{
		rig.AddTorque(Vector3.Angle(base.transform.forward, rig.velocity) * Vector3.Cross(base.transform.forward, rig.velocity).normalized * force, ForceMode.Acceleration);
	}
}
