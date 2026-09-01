using UnityEngine;

public class IgnoreCollider : MonoBehaviour
{
	public Collider targt;

	private void Start()
	{
		Physics.IgnoreCollision(targt, GetComponent<Collider>(), true);
	}

	private void OnDisable()
	{
		Physics.IgnoreCollision(targt, GetComponent<Collider>(), false);
	}
}
