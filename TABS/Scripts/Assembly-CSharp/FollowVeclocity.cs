using UnityEngine;

public class FollowVeclocity : MonoBehaviour
{
	public AnimationCurve curve;

	public float multiplier = 1f;

	private Rigidbody rig;

	private void Start()
	{
		rig = GetComponent<Rigidbody>();
	}

	private void FixedUpdate()
	{
		Vector3 vector = -Vector3.Cross(rig.velocity, base.transform.forward).normalized * Vector3.Angle(base.transform.forward, rig.velocity);
		rig.AddTorque(vector * curve.Evaluate(rig.velocity.magnitude), ForceMode.Acceleration);
	}
}
