using UnityEngine;

public class LeanIntoTurns : MonoBehaviour
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
		if (!data.Dead && !(data.ragdollControl < 0.2f))
		{
			rig.AddTorque(force * rig.angularVelocity.y * rig.transform.forward, ForceMode.Acceleration);
		}
	}
}
