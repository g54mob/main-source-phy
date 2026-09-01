using UnityEngine;

public class SoundOnClickUI : MonoBehaviour
{
	public bool mouseUp;

	public float mouseUpPitchMultiplier = 0.75f;

	public AudioSource audioSource;

	private float startPitch;

	protected void Start()
	{
		if (!audioSource)
		{
			audioSource = GetComponent<AudioSource>();
		}
		startPitch = audioSource.pitch;
	}

	public void PlaySound()
	{
		PlaySound(startPitch);
	}

	public void PlaySound(float pitch)
	{
		audioSource.pitch = pitch;
		audioSource.Play();
	}

	public void MouseUp()
	{
		if (mouseUp)
		{
			PlaySound(startPitch * mouseUpPitchMultiplier);
		}
	}
}
