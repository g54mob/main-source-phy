using UnityEngine;

public class LeafBlower : MonoBehaviour
{
	public float windAmount = 10f;

	public Transform myTransform;

	private void OnTriggerStay(Collider other)
	{
		if ((bool)other.attachedRigidbody)
		{
			other.attachedRigidbody.AddForce(myTransform.forward * windAmount);
		}
	}
}
