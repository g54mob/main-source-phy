using System.Collections;
using UnityEngine;

public class EnemyShrapnelCannon : MonoBehaviour
{
	public float knockbackSpeed = 100f;

	public Rigidbody parentRigidbody;

	public ParticleSystem[] particles;

	public bool hasShot;

	public float randomDelay = 0.1f;

	public Transform shrapnel;

	public Transform spawnPos;

	public float randomTimer = 0.5f;

	private IEnumerator Start()
	{
		if (StatMaster.levelSimulating)
		{
			yield return new WaitForSeconds(Random.Range(0f, randomTimer));
			Shoot();
		}
	}

	private void Shoot()
	{
		for (int i = 0; i < particles.Length; i++)
		{
			particles[i].Play();
		}
		GetComponent<AudioSource>().pitch = 1f + Random.Range(-0.1f, 0.1f);
		GetComponent<AudioSource>().Play();
		Object.Instantiate(shrapnel.gameObject, spawnPos.position, spawnPos.rotation);
	}
}
