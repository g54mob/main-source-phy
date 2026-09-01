using UnityEngine;

public class SoundOnClick : ClickBehaviour
{
	public bool mouseUp;

	public float mouseUpPitchMultiplier = 0.75f;

	public int mask = -1;

	public AudioSource audioSource;

	private float startPitch;

	private void Start()
	{
		if (audioSource == null)
		{
			audioSource = GetComponent<AudioSource>();
		}
		startPitch = audioSource.pitch;
		releaseOnlyOver = true;
	}

	public override void OnClicked()
	{
		if (base.enabled && UIMask.InsideMask(mask, base.transform.position) && audioSource.gameObject.activeInHierarchy)
		{
			audioSource.pitch = startPitch;
			audioSource.Play();
		}
	}

	public override void OnClickReleased()
	{
		if (mouseUp && base.enabled && UIMask.InsideMask(mask, base.transform.position) && base.gameObject.activeInHierarchy)
		{
			audioSource.pitch = startPitch * mouseUpPitchMultiplier;
			audioSource.Play();
		}
	}
}
