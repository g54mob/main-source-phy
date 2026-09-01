using UnityEngine;

public class SpawnParticleSystems : MonoBehaviour
{
	public GameObject particlesToSpawn;

	public Transform[] spawnPos;

	public float rotationDegrees;

	private ParticleSystem particles;

	private GameObject clone;

	private void Start()
	{
		clone = Object.Instantiate(particlesToSpawn, spawnPos[Random.Range(0, spawnPos.Length)].position, Quaternion.Euler(0f, rotationDegrees, 0f)) as GameObject;
		particles = clone.GetComponent<ParticleSystem>();
	}

	private void Update()
	{
		if (!particles.IsAlive())
		{
			Object.Destroy(clone);
			clone = Object.Instantiate(particlesToSpawn, spawnPos[Random.Range(0, spawnPos.Length)].position, Quaternion.Euler(0f, rotationDegrees, 0f)) as GameObject;
			particles = clone.GetComponent<ParticleSystem>();
		}
	}
}
