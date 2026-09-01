using UnityEngine;

public class ShowCenterOfMass : MonoBehaviour
{
	public Transform visObject;

	public Rigidbody rigidbodyObject;

	private void Update()
	{
		visObject.position = rigidbodyObject.worldCenterOfMass;
	}
}
