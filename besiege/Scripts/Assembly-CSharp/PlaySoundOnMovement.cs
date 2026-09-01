using UnityEngine;

public class PlaySoundOnMovement : MonoBehaviour
{
	public float maxVolume = 0.4f;

	public AudioSource audioSource;

	public AnimationCurve curve;

	private float volumeLerpSpeed = 3f;

	private float fallOff;

	private float fallOn;

	[HideInInspector]
	public bool moving;

	private void Start()
	{
		audioSource = GetComponent<AudioSource>();
	}

	private void Update()
	{
		if (moving)
		{
			if (!audioSource.isPlaying)
			{
				audioSource.Play();
			}
			fallOn += Time.deltaTime * volumeLerpSpeed;
			float volume = maxVolume * curve.Evaluate(Mathf.Clamp(fallOn, 0f, 1f));
			audioSource.volume = volume;
			fallOff = 0f;
		}
		else if (audioSource.isPlaying)
		{
			fallOn = 0f;
			fallOff += Time.deltaTime * volumeLerpSpeed;
			audioSource.volume = Mathf.Lerp(audioSource.volume, 0f, Mathf.Clamp01(fallOff));
		}
	}
}
