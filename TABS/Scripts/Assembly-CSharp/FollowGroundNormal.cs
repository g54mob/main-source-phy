using UnityEngine;

public class FollowGroundNormal : MonoBehaviour
{
	public float force;

	private DataHandler data;

	private Rigidbody rig;

	private void Start()
	{
		rig = GetComponent<Rigidbody>();
		data = GetComponentInParent<DataHandler>();
	}

	private void FixedUpdate()
	{
		if (!(data.sinceGrounded > 0.05f))
		{
			rig.AddTorque(force * Vector3.Angle(base.transform.up, data.groundNormal) * Vector3.Cross(base.transform.up, data.groundNormal).normalized, ForceMode.Acceleration);
		}
	}
}
