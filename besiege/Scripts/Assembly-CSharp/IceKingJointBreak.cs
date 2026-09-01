using System.Collections;
using UnityEngine;

public class IceKingJointBreak : MonoBehaviour
{
	public AudioSource sfx;

	public BreakOnForceNoScaling[] objectsToExplode;

	public ParticleSystem[] particles;

	public float breakTimer = 0.4f;

	public GameObject[] objectsToDisable;

	public void OnJointBreak()
	{
		sfx.Play();
		ExplodeObjects();
		PlayParticles();
		DisableObjects();
	}

	private void PlayParticles()
	{
		for (int i = 0; i < particles.Length; i++)
		{
			particles[i].Play();
		}
	}

	private void DisableObjects()
	{
		for (int i = 0; i < objectsToDisable.Length; i++)
		{
			objectsToDisable[i].SetActive(false);
		}
	}

	private IEnumerator ExplodeObjects()
	{
		yield return new WaitForSeconds(0.4f);
		for (int i = 0; i < objectsToExplode.Length; i++)
		{
			yield return new WaitForSeconds((float)i * breakTimer);
			objectsToExplode[i].BreakExplosion(1f, objectsToExplode[i].transform.position, 1f, 1f);
		}
	}
}
