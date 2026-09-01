using UnityEngine;

public class CustomBodyBlock : BlockBehaviour
{
	public Vector3 centerOfMass = Vector3.zero;

	public Vector3 inertiaTensor = Vector3.one;

	protected override void Start()
	{
		base.Start();
		if (!noRigidbody && SimPhysics)
		{
			Rigidbody.centerOfMass = centerOfMass;
			Rigidbody.inertiaTensor = ScaleInertia(base.transform.localScale, inertiaTensor);
		}
	}

	protected static Vector3 ScaleInertia(Vector3 scale, Vector3 inertia)
	{
		Vector3 vector = scale;
		Vector3 vector2 = new Vector3(Mathf.Max(vector.y, vector.z), Mathf.Max(vector.x, vector.z), Mathf.Max(vector.x, vector.y));
		Vector3 vector3 = inertia;
		return new Vector3(vector3.x * vector2.x * vector2.x, vector3.y * vector2.y * vector2.y, vector3.z * vector2.z * vector2.z);
	}
}
