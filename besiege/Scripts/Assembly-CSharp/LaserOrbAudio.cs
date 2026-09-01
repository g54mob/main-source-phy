using System.Collections;
using UnityEngine;

public class LaserOrbAudio : MonoBehaviour
{
	public AudioSource charge;

	public AudioSource activate;

	public AudioSource loop;

	public AudioSource wobble;

	public AudioSource deactivate;

	private float vol1;

	private float vol2;

	protected void Awake()
	{
		vol1 = loop.volume;
		vol2 = wobble.volume;
	}

	public void Charge()
	{
		charge.Play();
	}

	public void Activate()
	{
		activate.Play();
	}

	public void Loop(float fade)
	{
		StopAllCoroutines();
		StartCoroutine(IELoop(fade));
	}

	public void Stop()
	{
		charge.Stop();
		activate.Stop();
		if (!deactivate.isPlaying)
		{
			deactivate.Play();
		}
		StopAllCoroutines();
		StartCoroutine(IEFadeOut());
	}

	public void Kill()
	{
		StopAllCoroutines();
		deactivate.Stop();
		charge.Stop();
		activate.Stop();
		loop.Stop();
		wobble.Stop();
	}

	protected IEnumerator IELoop(float fade)
	{
		loop.volume = 0f;
		wobble.volume = 0f;
		loop.Play();
		wobble.Play();
		for (float t = 0f; t < fade; t += Time.deltaTime)
		{
			loop.volume = t * vol1;
			wobble.volume = t * vol2;
			yield return null;
		}
		loop.volume = vol1;
		wobble.volume = vol2;
	}

	protected IEnumerator IEFadeOut(float fade = 1f)
	{
		float vol = loop.volume;
		float vol2 = wobble.volume;
		for (float t = fade; t > 0f; t -= Time.deltaTime)
		{
			loop.volume = t * vol;
			wobble.volume = t * vol2;
			yield return null;
		}
		loop.Stop();
		wobble.Stop();
	}
}
