using UnityEngine;

public class Artillery : MonoBehaviour
{
	public Transform target;

	public Rigidbody projectile;

	public float gravity = 9.81f;

	public float force = 50f;

	private bool hasFired;

	private void Update()
	{
		Fire();
	}

	private void Fire()
	{
		float num = force;
		float z = CalculateProjectileFiringSolution(num, 0f);
		float num2 = GetYRotation() - 90f;
		MonoBehaviour.print("Rot " + num2);
		if (!hasFired)
		{
			Vector3 vector = new Vector3(num, 0f, 0f);
			Vector3 vector2 = Quaternion.Euler(0f, num2, z) * vector;
			projectile.AddRelativeForce(vector2, ForceMode.Impulse);
			hasFired = true;
		}
	}

	private float GetYRotation()
	{
		Vector3 vector = base.transform.InverseTransformPoint(target.position);
		float x = vector.x;
		float z = vector.z;
		return Mathf.Atan2(x, z) * 57.29578f;
	}

	private float CalculateProjectileFiringSolution(float vel, float alt)
	{
		Vector2 a = new Vector2(base.transform.position.x, base.transform.position.z);
		Vector2 b = new Vector2(target.position.x, target.position.z);
		float num = Vector2.Distance(a, b);
		float num2 = gravity;
		float num3 = num * num;
		float num4 = vel * vel;
		float num5 = vel * vel * vel * vel;
		float num6 = num4 + Mathf.Sqrt(num5 - num2 * (num2 * num3 + 2f * alt * num4));
		float num7 = num2 * num;
		float num8 = Mathf.Atan(num6 / num7);
		return num8 * 57.29578f;
	}
}
