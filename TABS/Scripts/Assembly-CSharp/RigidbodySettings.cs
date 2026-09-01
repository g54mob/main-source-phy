using UnityEngine;

public class RigidbodySettings : MonoBehaviour
{
	public bool removeTorqueCap = true;

	private Rigidbody rig;

	private void Start()
	{
		rig = GetComponent<Rigidbody>();
		if (removeTorqueCap)
		{
			rig.maxAngularVelocity = 500f;
		}
	}
}
