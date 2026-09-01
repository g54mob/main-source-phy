using UnityEngine;

public class HitData
{
	public Transform transform;

	public Rigidbody rigidbody;

	public Collider collider;

	public Vector3 normal;

	public Vector3 point;

	public float distance;

	public HitData(Transform transform, Rigidbody rigidbody, Collider collider, Vector3 normal, Vector3 point, float distance)
	{
		this.transform = transform;
		this.rigidbody = rigidbody;
		this.collider = collider;
		this.normal = normal;
		this.point = point;
		this.distance = distance;
	}
}
