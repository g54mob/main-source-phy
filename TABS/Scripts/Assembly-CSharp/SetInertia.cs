using UnityEngine;

public class SetInertia : MonoBehaviour
{
	private void Start()
	{
		GetComponentInParent<Rigidbody>().inertiaTensor = Vector3.one;
	}
}
