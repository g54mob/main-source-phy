using UnityEngine;

public class RunAwaySound : MonoBehaviour
{
	public EnemyAISimple aiCode;

	public float pitchRange = 0.4f;

	public float playRate = 0.4f;

	public float randomRate;

	public RandomSoundController sfx;

	public bool whenNotRunningAway;

	private void Start()
	{
		InvokeRepeating("PlaySound", Random.value, playRate + Random.Range(0f, randomRate));
	}

	private void PlaySound()
	{
		if (((!whenNotRunningAway) ? aiCode.isRunningAway : (!aiCode.isRunningAway)) && !aiCode.isDead)
		{
			sfx.Play();
		}
	}
}
