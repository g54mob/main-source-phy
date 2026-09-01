using UnityEngine;

public class BirdAnimationOffset : MonoBehaviour
{
	public Animator[] Flock;

	private void Start()
	{
		for (int i = 0; i < Flock.Length; i++)
		{
			Flock[i].enabled = true;
			Flock[i].SetFloat("Offset", Random.Range(0f, 1f));
			Flock[i].speed += Random.Range(-0.1f, 0.1f);
		}
	}

	private void Update()
	{
	}
}
