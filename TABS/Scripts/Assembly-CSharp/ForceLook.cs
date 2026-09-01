using UnityEngine;

public class ForceLook : MonoBehaviour
{
	public Vector3 targetDirection;

	public Vector3 transformDirection;

	public float force;

	private Rigidbody rig;

	private void Start()
	{
		rig = GetComponent<Rigidbody>();
	}

	private void FixedUpdate()
	{
		rig.AddTorque((0f - force) * Vector3.Angle(targetDirection, base.transform.TransformDirection(transformDirection)) * Vector3.Cross(targetDirection, base.transform.TransformDirection(transformDirection)), ForceMode.Acceleration);
	}
}
