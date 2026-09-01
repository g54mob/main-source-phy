using System.Collections;
using UnityEngine;

public class AudioTrackTransition : MonoBehaviour
{
	public AudioClip firstClip;

	public AudioClip secondClip;

	public float pitch1 = 1f;

	public float pitch2 = 1f;

	public AudioSource audio;

	public bool LoopSecondClip = true;

	private void Start()
	{
		audio = GetComponent<AudioSource>();
		StartCoroutine(PlaySound());
	}

	private IEnumerator PlaySound()
	{
		audio.pitch = pitch1;
		audio.clip = firstClip;
		audio.Play();
		float lenght = firstClip.length - 0.1f;
		yield return new WaitForSeconds(lenght);
		if (audio.isPlaying)
		{
			audio.Stop();
		}
		audio.pitch = pitch2;
		audio.clip = secondClip;
		audio.Play();
	}
}
