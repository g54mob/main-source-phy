using UnityEngine;

public class PlaySoundRandomTimes : MonoBehaviour
{
	public RandomSoundController randomSoundController;

	public float timeMin = 0.5f;

	public float timeMax = 3f;

	private AudioSource audioSource;

	protected void Start()
	{
		audioSource = randomSoundController.GetComponent<AudioSource>();
		if (StatMaster.levelSimulating)
		{
			Invoke("PlaySound", Random.Range(timeMin, timeMax));
		}
	}

	public void PlaySound()
	{
		if (StatMaster.levelSimulating)
		{
			if (randomSoundController.gameObject.activeInHierarchy && !audioSource.isPlaying)
			{
				randomSoundController.Play();
			}
			Invoke("PlaySound", Random.Range(timeMin, timeMax));
		}
	}
}
