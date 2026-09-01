using UnityEngine;

public class SliderController : SimBehaviour
{
	public ConfigurableJoint joint;

	public float limitStrength = 200f;

	private void FixedUpdate()
	{
		if (base.SimPhysics && base.isSimulating && joint != null)
		{
			Vector3 vector = base.transform.position - joint.connectedBody.position;
			if (vector.sqrMagnitude - 1f > 8f)
			{
				GetComponent<Rigidbody>().AddForce(vector * limitStrength);
			}
		}
	}
}
