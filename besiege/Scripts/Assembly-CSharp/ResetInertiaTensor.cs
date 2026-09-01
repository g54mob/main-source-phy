using UnityEngine;

public class ResetInertiaTensor : MonoBehaviour
{
	private void Start()
	{
		GetComponent<Rigidbody>().inertiaTensorRotation = Quaternion.identity;
		GetComponent<Rigidbody>().centerOfMass = Vector3.zero;
	}
}
