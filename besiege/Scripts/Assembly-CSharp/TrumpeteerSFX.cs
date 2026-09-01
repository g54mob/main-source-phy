using UnityEngine;

public class TrumpeteerSFX : MonoBehaviour
{
	public float timer;

	public float timeToWait = 1f;

	public float minTimeBetweenNotes = 1f;

	public float maxTimeBetweenNotes = 3f;

	public float minPitch = 1f;

	public float maxPitch = 2f;

	private void Start()
	{
		timeToWait = Random.Range(minTimeBetweenNotes, maxTimeBetweenNotes);
	}

	private void Update()
	{
		timer += Time.deltaTime;
		if (timer > timeToWait)
		{
			PlayNote();
		}
	}

	private void PlayNote()
	{
		timer = 0f;
		GetComponent<AudioSource>().pitch = Random.Range(minPitch, maxPitch);
		GetComponent<AudioSource>().Play();
		timeToWait = Random.Range(minTimeBetweenNotes, maxTimeBetweenNotes);
	}
}
