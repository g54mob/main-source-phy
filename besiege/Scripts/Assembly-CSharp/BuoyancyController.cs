using UnityEngine;

public class BuoyancyController : MonoBehaviour
{
	private void OnTriggerEnter(Collider other)
	{
		if ((bool)other.attachedRigidbody && (bool)other.attachedRigidbody.gameObject.GetComponent<BuoyancyTag>())
		{
			other.attachedRigidbody.gameObject.GetComponent<BuoyancyTag>().InWater();
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if ((bool)other.attachedRigidbody && (bool)other.attachedRigidbody.gameObject.GetComponent<BuoyancyTag>())
		{
			other.attachedRigidbody.gameObject.GetComponent<BuoyancyTag>().OutOfWater();
		}
	}
}
