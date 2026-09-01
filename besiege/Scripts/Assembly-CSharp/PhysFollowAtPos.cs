using UnityEngine;

public class PhysFollowAtPos : MonoBehaviour
{
	public Transform target;

	public float power;

	public float posHeight;

	private void FixedUpdate()
	{
		GetComponent<Rigidbody>().AddForceAtPosition((target.position - base.transform.position).normalized * power, base.transform.up * posHeight);
	}
}
