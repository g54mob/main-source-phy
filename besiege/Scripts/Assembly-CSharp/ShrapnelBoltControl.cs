using UnityEngine;

public class ShrapnelBoltControl : MonoBehaviour
{
	public Rigidbody[] bodies;

	public float[] speed;

	public void Fire(float power)
	{
		for (int i = 0; i < bodies.Length; i++)
		{
			bodies[i].AddForce(bodies[i].transform.forward * speed[i] * power);
		}
	}
}
