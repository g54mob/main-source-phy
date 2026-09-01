using UnityEngine;

public class ClothSmoothWind : MonoBehaviour
{
	public Cloth clothy;

	public float smoothy = 6f;

	public Vector3 baseDirection;

	public Vector3 randDirection;

	public Vector3 RandVec;

	public float rate = 0.2f;

	public float lastRand;

	private void Update()
	{
		lastRand += Time.deltaTime;
		if (lastRand > rate)
		{
			Rand();
			lastRand = 0f;
		}
		clothy.externalAcceleration = Vector3.Lerp(clothy.externalAcceleration, baseDirection + RandVec, Time.deltaTime * smoothy);
	}

	private void Rand()
	{
		RandVec = new Vector3(Random.Range(0f - randDirection.x, randDirection.x), Random.Range(0f - randDirection.y, randDirection.y), Random.Range(0f - randDirection.z, randDirection.z));
	}
}
