using UnityEngine;

public class ParticleOrderByRotation : MonoBehaviour
{
	public int positive = 1;

	public int negative = -1;

	public float dot;

	public Vector3 direction = Vector3.right;

	public ParticleSystemRenderer particle;

	protected void LateUpdate()
	{
		if (Vector3.Dot(Camera.main.transform.forward, direction) > dot)
		{
			particle.sortingOrder = positive;
		}
		else
		{
			particle.sortingOrder = negative;
		}
	}
}
