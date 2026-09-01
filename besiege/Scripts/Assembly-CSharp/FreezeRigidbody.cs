using UnityEngine;

public class FreezeRigidbody : MonoBehaviour
{
	public Rigidbody rigidbody;

	public float verticalPosition;

	private void Update()
	{
		if (base.transform.position.y >= verticalPosition)
		{
			rigidbody.constraints = RigidbodyConstraints.FreezeAll;
		}
	}
}
