using UnityEngine;

public class SoundOnMouseEvents : ClickBehaviour
{
	public AudioSource mouseOverSFX;

	public AudioSource mouseDownSFX;

	public AudioSource mouseUpSFX;

	private void Awake()
	{
		releaseOnlyOver = true;
	}

	private void OnMouseEnter()
	{
		if (mouseOverSFX != null)
		{
			mouseOverSFX.Play();
		}
	}

	public override void OnClicked()
	{
		if (mouseDownSFX != null)
		{
			mouseDownSFX.Play();
		}
	}

	public override void OnClickReleased()
	{
		if (mouseUpSFX != null)
		{
			mouseUpSFX.Play();
		}
	}
}
