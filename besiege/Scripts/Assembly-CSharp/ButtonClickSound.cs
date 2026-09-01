using UnityEngine;

public class ButtonClickSound : ClickBehaviour
{
	public float mouseUpPitch;

	private float startPitch;

	public AudioSource audioSource;

	private void Start()
	{
		if (audioSource == null)
		{
			audioSource = GetComponent<AudioSource>();
		}
		startPitch = audioSource.pitch;
	}

	public override void OnClicked()
	{
		if (base.enabled)
		{
			audioSource.pitch = startPitch;
			audioSource.Play();
		}
	}

	public override void OnClickReleased()
	{
		if (base.enabled)
		{
			audioSource.pitch = mouseUpPitch;
			audioSource.Play();
		}
	}
}
