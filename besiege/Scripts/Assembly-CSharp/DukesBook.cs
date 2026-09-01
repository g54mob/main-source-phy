using System.Collections;
using UnityEngine;

public class DukesBook : MonoBehaviour
{
	public ParticleSystem[] particles;

	private bool hasIgnited;

	private void Update()
	{
		if (WinCondition.hasWon && !hasIgnited)
		{
			StartCoroutine(IgniteBook());
		}
	}

	private IEnumerator IgniteBook()
	{
		hasIgnited = true;
		yield return new WaitForSeconds(0.4f);
		GetComponent<AudioSource>().Play();
		for (int i = 0; i < particles.Length; i++)
		{
			particles[i].Play();
		}
	}
}
